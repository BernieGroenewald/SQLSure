using System;
using System.IO;
using System.Text;
using System.ComponentModel;

namespace SentryUpdater
{
    public class Updater
    {
        public const string DefaultConfigFile = "SentryUpdateManifest.xml";
        public string ApplicationPath = string.Empty;
        public string WorkPath = string.Empty;
        
        private volatile bool _updating;
        private readonly Manifest _localConfig;
        private Manifest _remoteConfig;
        private readonly FileInfo _localConfigFile;

        private bool ManifestSuccess = false;
        private bool PayloadSuccess = false;
        
        BackgroundWorker bwFetchManifest;
        BackgroundWorker bwFetchPayload;
        BackgroundWorker bwUpdateLocal;

        string StatusString = string.Empty;

        //public Updater() //: this(new FileInfo(DefaultConfigFile))
        //{
        //}

        public Updater()
        {
            FileInfo configFile;

            Log.Debug = true;

            ApplicationPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            WorkPath = Path.Combine(ApplicationPath, "work");

            configFile = new FileInfo(Path.Combine(ApplicationPath, DefaultConfigFile));

            _localConfigFile = configFile;

            Log.Write("Loaded.");
            
            Log.Write("Initializing using file '{0}'.", configFile.FullName);

            if (!configFile.Exists)
            {
                Log.Write("Config file '{0}' does not exist, stopping.", configFile.Name);
                return;
            }

            string data = File.ReadAllText(configFile.FullName);
            this._localConfig = new Manifest(data);
        }

        public void StartMonitoring()
        {
            Check();
        }

        
        //private void Check(object state)
        private void Check()
        {
            Log.Write("Check starting.");

            if (_updating)
            {
                Log.Write("Updater is already updating.");
                Log.Write("Check ending.");
                return;
            }

            bwFetchManifest = new BackgroundWorker();
            bwFetchManifest.DoWork += bwFetchManifest_DoWork;
            bwFetchManifest.RunWorkerCompleted += bwFetchManifest_RunWorkerCompleted;
            bwFetchManifest.RunWorkerAsync();
        }

        private void bwFetchManifest_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (ManifestSuccess)
            {
                bwFetchPayload = new BackgroundWorker();
                bwFetchPayload.DoWork += bwFetchPayload_DoWork;
                bwFetchPayload.RunWorkerCompleted += bwFetchPayload_RunWorkerCompleted;
                bwFetchPayload.RunWorkerAsync();
            }
        }

        private void bwFetchPayload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (PayloadSuccess)
            {
                bwUpdateLocal = new BackgroundWorker();
                bwUpdateLocal.DoWork += bwUpdateLocal_DoWork;
                bwUpdateLocal.RunWorkerCompleted += bwUpdateLocal_RunWorkerCompleted;
                bwUpdateLocal.RunWorkerAsync();
            }
        }

        private void bwUpdateLocal_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            return;
        }

        private void bwUpdateLocal_DoWork(object sender, DoWorkEventArgs e)
        {
            // Write out the new manifest.

            _remoteConfig.Write(Path.Combine(ApplicationPath, _localConfigFile.Name));
            string UpdateFileLocation = string.Empty;

            // Copy everything.

            var directory = new DirectoryInfo(WorkPath);
            var files = directory.GetFiles("*.*", SearchOption.AllDirectories);
            string destination = string.Empty;

            try
            {
                foreach (FileInfo file in files)
                {
                    try
                    {
                        destination = file.FullName.Replace(directory.FullName + @"\", "");
                        UpdateFileLocation = Path.Combine(ApplicationPath, destination);

                        Log.Write("installing file '{0}'.", UpdateFileLocation);

                        //Directory.CreateDirectory(new FileInfo(destination).DirectoryName);
                        file.CopyTo(UpdateFileLocation, true);
                    }

                    catch (Exception ex)
                    {
                        MoveBusyFile(UpdateFileLocation, file);

                        Log.Write(ex.Message + " " + UpdateFileLocation);
                    }
                }
            }

            catch (Exception ex)
            {
                return;
            }
            // Clean up.

            Log.Write("Deleting work directory.");
            Directory.Delete(WorkPath, true);

            return;
        }

        private void bwFetchPayload_DoWork(object sender, DoWorkEventArgs e)
        {
            _updating = true;

            Log.Write("Updating '{0}' files.", this._remoteConfig.Payloads.Length);

            // Clean up failed attempts.

            if (Directory.Exists(WorkPath))
            {
                Log.Write("WARNING: Work directory already exists.");

                try
                {
                    Directory.Delete(WorkPath, true);
                }

                catch (IOException)
                {
                    Log.Write("Cannot delete open directory '{0}'.", WorkPath);
                    return;
                }
            }

            Directory.CreateDirectory(WorkPath);

            // Download files in manifest.

            try
            {
                foreach (string update in this._remoteConfig.Payloads)
                {
                    Log.Write("Fetching '{0}'.", update);

                    var url = this._remoteConfig.BaseUri + update;
                    var file = Fetch.Get(url);

                    if (file == null)
                    {
                        Log.Write("Fetch failed.");
                    }
                    else
                    {
                        var info = new FileInfo(Path.Combine(WorkPath, update));

                        Directory.CreateDirectory(info.DirectoryName);
                        File.WriteAllBytes(Path.Combine(WorkPath, update), file);
                    }
                }
            }

            catch (Exception ex)
            {
                Log.Write("Fetch failed " + ex.Message);
                return;
            }

            PayloadSuccess = true;

            _updating = false;

            Log.Write("Check ending.");
        }

        private void bwFetchManifest_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var remoteUri = new Uri(this._localConfig.RemoteConfigUri);

                Log.Write("Fetching '{0}'.", remoteUri.AbsoluteUri);

                var http = new Fetch { Retries = 5, RetrySleep = 30000, Timeout = 30000 };

                http.Load(remoteUri.AbsoluteUri);

                if (!http.Success)
                {
                    Log.Write("Fetch error: {0}", http.Response.StatusDescription);
                    this._remoteConfig = null;
                    return;
                }

                string data = Encoding.UTF8.GetString(http.ResponseData);
                this._remoteConfig = new Manifest(data);

                if (this._remoteConfig == null)
                {
                    return;
                }

                if (this._localConfig.SecurityToken != this._remoteConfig.SecurityToken)
                {
                    Log.Write("Security token mismatch.");
                    return;
                }

                Log.Write("Remote config is valid.");
                Log.Write("Local version is  {0}.", this._localConfig.Version);
                Log.Write("Remote version is {0}.", this._remoteConfig.Version);

                if (this._remoteConfig.Version == this._localConfig.Version)
                {
                    Log.Write("Versions are the same.");
                    Log.Write("Check ending.");
                    return;
                }

                if (this._remoteConfig.Version < this._localConfig.Version)
                {
                    Log.Write("Remote version is older. That's weird.");
                    Log.Write("Check ending.");
                    return;
                }

                Log.Write("Remote version is newer. Updating.");

                ManifestSuccess = true;
            }

            catch
            {
                ManifestSuccess = false;
            }
        }

        //private void Update()
        //{
        //    Log.Write("Updating '{0}' files.", this._remoteConfig.Payloads.Length);

        //    // Clean up failed attempts.

        //    if (Directory.Exists(WorkPath))
        //    {
        //        Log.Write("WARNING: Work directory already exists.");

        //        try
        //        {
        //            Directory.Delete(WorkPath, true);
        //        }

        //        catch (IOException)
        //        {
        //            Log.Write("Cannot delete open directory '{0}'.", WorkPath);
        //            return;
        //        }
        //    }

        //    Directory.CreateDirectory(WorkPath);

        //    // Download files in manifest.

        //    try
        //    {
        //        foreach (string update in this._remoteConfig.Payloads)
        //        {
        //            Log.Write("Fetching '{0}'.", update);

        //            var url = this._remoteConfig.BaseUri + update;
        //            var file = Fetch.Get(url);

        //            if (file == null)
        //            {
        //                Log.Write("Fetch failed.");
        //                //return;
        //            }
        //            else
        //            {
        //                var info = new FileInfo(Path.Combine(WorkPath, update));

        //                Directory.CreateDirectory(info.DirectoryName);
        //                File.WriteAllBytes(Path.Combine(WorkPath, update), file);
        //            }
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        Log.Write("Fetch failed " + ex.Message);
        //        ReturnValue = "Fetch failed " + ex.Message;
        //        return;
        //    }
        //    // Change the currently running executable so it can be overwritten.

        //    //Process thisprocess = Process.GetCurrentProcess();
        //    //string me = thisprocess.MainModule.FileName;
        //    //string bak = me + ".bak";

        //    //Log.Write("Renaming running process to '{0}'.", bak);

        //    //if (File.Exists(bak))
        //    //{
        //    //    File.Delete(bak);
        //    //}

        //    //File.Move(me, bak);
        //    //File.Copy(bak, me);

        //    // Write out the new manifest.

        //    _remoteConfig.Write(Path.Combine(WorkPath, _localConfigFile.Name));

        //    // Copy everything.

        //    var directory = new DirectoryInfo(WorkPath);
        //    var files = directory.GetFiles("*.*", SearchOption.AllDirectories);
        //    string destination = string.Empty;

        //    try
        //    {
        //        foreach (FileInfo file in files)
        //        {
        //            try
        //            {
        //                destination = file.FullName.Replace(directory.FullName + @"\", "");

        //                Log.Write("installing file '{0}'.", destination);

        //                Directory.CreateDirectory(new FileInfo(destination).DirectoryName);
        //                file.CopyTo(destination, true);
        //            }

        //            catch (Exception ex)
        //            {
        //                MoveBusyFile(destination, file);

        //                Log.Write(ex.Message + " " + destination);
        //            }
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        ReturnValue = "Error installing - " + ex.Message;
        //        return;
        //    }
        //    // Clean up.

        //    Log.Write("Deleting work directory.");
        //    Directory.Delete(WorkPath, true);

        //    ReturnValue = "";
        //    return;

        //    // Restart.

        //    //Log.Write("Spawning new process.");

        //    //var spawn = Process.Start(me);

        //    //Log.Write("New process ID is {0}", spawn.Id);
        //    //Log.Write("Closing old running process {0}.", thisprocess.Id);

        //    //thisprocess.CloseMainWindow();
        //    //thisprocess.Close();
        //    //thisprocess.Dispose();
        //}

        private void MoveBusyFile(string FileName, FileInfo CurFile)
        {
            try
            {
                if (File.Exists(FileName + ".bak"))
                {
                    File.Delete(FileName + ".bak");
                }

                File.Move(FileName, FileName + ".bak");
                CurFile.CopyTo(FileName, true);
            }

            catch
            {

            }
        }
    }
}
