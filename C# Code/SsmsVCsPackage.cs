extern alias Ssms2012;
extern alias Ssms2014;
extern alias Ssms2016;
extern alias Ssms2017;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Data;
using SentryBackupRestore;
using SentryDataStuff;
using SentryGeneral;
using SentryAdmin;
using SentryCompare;
using SentryProject;
using SentryUpdater;
using System.ComponentModel;

namespace SentrySSMS
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [Guid(SQLSentryPackage.PackageGuidString)]
    [ProvideAutoLoad("d114938f-591c-46cf-a785-500a82d97410")] //CommandGuids.ObjectExplorerToolWindowIDString
    [ProvideOptionPage(typeof(SchemaFolderOptions), "SQL Server Object Explorer", "Schema Folders", 114, 116, true)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    
    public sealed class SQLSentryPackage : Package
    {
        /// <summary>
        /// SQLSentryPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "a88a775f-7c86-4a09-b5a6-890c4c38261b";
        public static readonly Guid PackageGuid = new Guid(SQLSentryPackage.PackageGuidString);

        string UserName = string.Empty;
        string EnvironmentUserName = string.Empty;
        int UserID = 0;
        bool ServerFound = false;
        string ServerName = string.Empty;
        string ServerRole = string.Empty;
        string ServerAlias = string.Empty;
        string SelectedObject = string.Empty;
        bool IsDevServer = false;
        bool IsUATServer = false;
        bool IsPreProdServer = false;
        bool IsProdServer = false;
        string HistoryDetail = string.Empty;
        bool AllowEdit = false;
        bool InDevCycle = false;
        int StatusID = 0;
        string CheckOutUser = string.Empty;
        string ObjectUser = string.Empty;
        string DatabaseName = string.Empty;
        string CurrentServer = string.Empty;
        string CurrentServerConnectionString = "";
        string UserRole = string.Empty;
        string ObjectType = string.Empty;
        string StoreConnectionString = string.Empty;
        string PreviousServer = string.Empty;
        string ReleaseToServer = string.Empty;
        bool IsNewObjectQuickRelease = false;
        string ObjectTextQuickRelease = string.Empty;
        bool IsTrialVersion = false;
        int TrialDayNo = 0;
        bool TrialHasExpired = false;
        bool IsValidKey = false;
        string CompanyName = string.Empty;
        bool UpdateValid = false;
        int NumberOfUsers = 1000;
        string UpdatesValidTo = string.Empty;
        bool UserCountCheckDone = false;
        int SSMSVersion = 0;
        bool StrictForceCheckOutDev = false;
        bool RequireTesterApproval = false;
        bool RequireApproverApproval = false;

        AccessRights ar = new AccessRights();

        BackgroundWorker worker;

        //string ObjectsTestedOverall = string.Empty;

        DataTable dtObjects;
        TreeView tvExplorer;
        ContextMenuStrip cmsPopup;
        TreeNode tvnCurrent;

        public SchemaFolderOptions Options { get; set; }
        
        private IObjectExplorerExtender _objectExplorerExtender;

        // Ignore never assigned to warning for release build.
#pragma warning disable CS0649
        private IVsOutputWindowPane _outputWindowPane;
#pragma warning restore CS0649

        /// <summary>
        /// Initializes a new instance of the <see cref="SsmsSchemaFoldersPackage"/> class.
        /// </summary>
        public SQLSentryPackage()
        {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            // OutputWindowPane for debug messages
//#if DEBUG
//            var outputWindow = (IVsOutputWindow)GetService(typeof(SVsOutputWindow));
//            var guidPackage = new Guid(PackageGuidString);
//            outputWindow.CreatePane(guidPackage, "Schema Folders debug output", 1, 0);
//            outputWindow.GetPane(ref guidPackage, out _outputWindowPane);
//#endif

            // Link with VS options.
            object obj;
            (this as IVsPackage).GetAutomationObject("SQL Server Object Explorer.Schema Folders", out obj);
            Options = (SchemaFolderOptions)obj;

            _objectExplorerExtender = GetObjectExplorerExtender();

            if (_objectExplorerExtender != null)
            {
                AttachTreeViewEvents();
                AttachTreeviewImageList();
            }

            // Reg setting is removed after initialize. Wait short delay then recreate it.
            DelayAddSkipLoadingReg();

            worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //MessageBox.Show("Done updating");
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Updater u = new Updater();
                u.StartMonitoring();
            }

            catch
            {

            }
        }

        //protected override int QueryClose(out bool canClose)
        //{
        //    AddSkipLoadingReg();
        //    return base.QueryClose(out canClose);
        //}

        #endregion

        private void AddSkipLoadingReg()
        {
            var myPackage = this.UserRegistryRoot.CreateSubKey(@"Packages\{" + SQLSentryPackage.PackageGuidString + "}");
            myPackage.SetValue("SkipLoading", 1);
        }

        private void DelayAddSkipLoadingReg()
        {
            var delay = new Timer();
            delay.Tick += delegate (object o, EventArgs e)
            {
                delay.Stop();
                AddSkipLoadingReg();
            };

            delay.Interval = 1000;
            delay.Start();
        }
        
        private IObjectExplorerExtender GetObjectExplorerExtender()
        {
            try
            {
                var ssmsInterfacesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SqlWorkbench.Interfaces.dll");

                if (File.Exists(ssmsInterfacesPath))
                {
                    var ssmsInterfacesVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(ssmsInterfacesPath);

                    switch (ssmsInterfacesVersion.FileMajorPart)
                    {
                        case 14:
                            SSMSVersion = 2017;
                            debug_message("SsmsVersion:2017");
                            return new Ssms2017::SentrySSMS.ObjectExplorerExtender(this, Options);

                        case 13:
                            SSMSVersion = 2016;
                            debug_message("SsmsVersion:2016");
                            return new Ssms2016::SentrySSMS.ObjectExplorerExtender(this, Options);

                        case 12:
                            SSMSVersion = 2014;
                            debug_message("SsmsVersion:2014");
                            return new Ssms2014::SentrySSMS.ObjectExplorerExtender(this, Options);

                        case 11:
                            SSMSVersion = 2012;
                            debug_message("SsmsVersion:2012");
                            return new Ssms2012::SentrySSMS.ObjectExplorerExtender(this, Options);

                        default:
                            SSMSVersion = 2017;
                            //ActivityLogEntry(__ACTIVITYLOG_ENTRYTYPE.ALE_INFORMATION, String.Format("SqlWorkbench.Interfaces.dll v{0}:{1}", ssmsInterfacesVersion.FileMajorPart, ssmsInterfacesVersion.FileMinorPart));
                            break;
                    }
                }

                //ActivityLogEntry(__ACTIVITYLOG_ENTRYTYPE.ALE_WARNING, "Unknown SSMS Version. Defaulting to 2016.");
                return new Ssms2016::SentrySSMS.ObjectExplorerExtender(this, Options);
            }
            catch (Exception ex)
            {
                //ActivityLogEntry(__ACTIVITYLOG_ENTRYTYPE.ALE_ERROR, ex.ToString());
                return null;
            }
        }
        
        private void AttachTreeViewEvents()
        {
            tvExplorer = _objectExplorerExtender.GetObjectExplorerTreeView();

            if (tvExplorer != null)
            {
                tvExplorer.BeforeExpand += new TreeViewCancelEventHandler(ObjectExplorerTreeViewBeforeExpandCallback);
                tvExplorer.AfterExpand += new TreeViewEventHandler(ObjectExplorerTreeViewAfterExpandCallback);
                tvExplorer.ContextMenuStripChanged += new EventHandler(ContextMenuStripChange);
                tvExplorer.NodeMouseClick += new TreeNodeMouseClickEventHandler(NodeMouseClick);
            }
            //else
            //    ActivityLogEntry(__ACTIVITYLOG_ENTRYTYPE.ALE_ERROR, "Object Explorer TreeView == null");
        }

        private void NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                tvnCurrent = e.Node;
            }

            catch
            {

            }
            
        }
        
        void ContextMenuStripChange(object sender, EventArgs e)
        {
            try
            {
                cmsPopup = (ContextMenuStrip)tvExplorer.ContextMenuStrip;
                cmsPopup.Opening += cmsPopup_Opening;
            }

            catch
            {

            }
        }

        private void GetStrictSettings()
        {
            string SettingValue = string.Empty;

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    SettingValue = sn.GetSystemSetting("Key7");

                    if (SettingValue.Trim() == "true")
                    {
                        StrictForceCheckOutDev = true;
                    }
                    else
                    {
                        StrictForceCheckOutDev = false;
                    }

                    SettingValue = sn.GetSystemSetting("Key8");

                    if (SettingValue.Trim() == "true")
                    {
                        RequireTesterApproval = true;
                    }
                    else
                    {
                        RequireTesterApproval = false;
                    }

                    SettingValue = sn.GetSystemSetting("Key9");

                    if (SettingValue.Trim() == "true")
                    {
                        RequireApproverApproval = true;
                    }
                    else
                    {
                        RequireApproverApproval = false;
                    }
                }
            }

            catch
            {

            }
        }

        private void cmsPopup_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                ContextMenuStrip cmsTmp = (ContextMenuStrip)sender;
                ToolStripItem itemTmp = null;
                string InvariantPath = string.Empty;
                bool AllowModify = true;
                bool AllowView = true;
                int CurUserCount = 0;

                GetStrictSettings();

                if (cmsTmp != null)
                {
                    if (cmsTmp.Items.Count > 0)
                    {
                        if (tvnCurrent != null)
                        {
                            GetServerRoleOnCurrentConnection();

                            //Root menu

                            if ((tvnCurrent.Parent == null) || (tvnCurrent.Text == "Databases") || (tvnCurrent.Parent.Text == "Databases"))
                            {
                                int LastItem = 0;

                                cmsTmp.Items.Add("-");
                                ToolStripItem itemSCMain = cmsTmp.Items.Add("Source Control");

                                LastItem = cmsTmp.Items.Count - 1;

                                if ((!TrialHasExpired) || (IsValidKey))
                                {
                                    ToolStripItem itemSCMainServers = (cmsTmp.Items[LastItem] as ToolStripMenuItem).DropDownItems.Add("Server Setup");
                                    itemSCMainServers.Click += new EventHandler(itemSCMainServers_Click);

                                    ToolStripItem itemSCMainProject = (cmsTmp.Items[LastItem] as ToolStripMenuItem).DropDownItems.Add("Projects");
                                    itemSCMainProject.Click += new EventHandler(itemSCMainProject_Click);

                                    if (ar.ProjectMaintenance)
                                    {
                                        itemSCMainProject.Enabled = true;
                                    }
                                    else
                                    {
                                        itemSCMainProject.Enabled = false;
                                    }

                                    ToolStripItem itemSCMainProjectDeploy = (cmsTmp.Items[LastItem] as ToolStripMenuItem).DropDownItems.Add("Deploy Projects");
                                    itemSCMainProjectDeploy.Click += new EventHandler(itemSCMainProjectDeploy_Click);

                                    if (ar.ReleasePreProduction || ar.ReleaseUAT || ar.ReleaseProduction)
                                    {
                                        itemSCMainProjectDeploy.Enabled = true;
                                    }
                                    else
                                    {
                                        itemSCMainProjectDeploy.Enabled = false;
                                    }

                                    ToolStripItem itemSCMainProjectApproval = (cmsTmp.Items[LastItem] as ToolStripMenuItem).DropDownItems.Add("Approve Projects");
                                    itemSCMainProjectApproval.Click += new EventHandler(itemSCMainProjectApproval_Click);

                                    if (RequireApproverApproval || RequireTesterApproval)
                                    {
                                        itemSCMainProjectApproval.Enabled = true;
                                    }
                                    else
                                    {
                                        itemSCMainProjectApproval.Enabled = false;
                                    }

                                    ToolStripItem itemSCMainBackup = (cmsTmp.Items[LastItem] as ToolStripMenuItem).DropDownItems.Add("Object Backup/Restore");
                                    itemSCMainBackup.Click += new EventHandler(itemSCMainBackup_Click);

                                    if (ar.Backup)
                                    {
                                        itemSCMainBackup.Enabled = true;
                                    }
                                    else
                                    {
                                        itemSCMainBackup.Enabled = false;
                                    }

                                    ToolStripItem itemMainProjectBackup = cmsTmp.Items.Add("Project Backup");
                                    itemMainProjectBackup.Click += new EventHandler(itemProjectBackup_Click);

                                    ToolStripItem itemMainProjectRestore = cmsTmp.Items.Add("Project Restore");
                                    itemMainProjectRestore.Click += new EventHandler(itemProjectRestore_Click);

                                    ToolStripItem itemSCMainSearch = (cmsTmp.Items[LastItem] as ToolStripMenuItem).DropDownItems.Add("Search");
                                    itemSCMainSearch.Click += new EventHandler(itemSCMainSearch_Click);

                                    ToolStripItem itemSCMainAdmin = (cmsTmp.Items[LastItem] as ToolStripMenuItem).DropDownItems.Add("Administration");
                                    itemSCMainAdmin.Click += new EventHandler(itemSCMainAdmin_Click);

                                    ToolStripItem itemSCMainServerGroups = (cmsTmp.Items[LastItem] as ToolStripMenuItem).DropDownItems.Add("Server Groups");
                                    itemSCMainServerGroups.Click += new EventHandler(itemSCMainServerGroups_Click);

                                    if (ar.Administration)
                                    {
                                        itemSCMainAdmin.Enabled = true;
                                        itemSCMainServerGroups.Enabled = true;
                                    }
                                    else
                                    {
                                        itemSCMainAdmin.Enabled = false;
                                        itemSCMainServerGroups.Enabled = false;
                                    }

                                    ToolStripItem itemSCDataStore = (cmsTmp.Items[LastItem] as ToolStripMenuItem).DropDownItems.Add("Data Store Settings");
                                    itemSCDataStore.Click += new EventHandler(itemSCDataStore_Click);
                                }

                                ToolStripItem itemSCAbout = (cmsTmp.Items[LastItem] as ToolStripMenuItem).DropDownItems.Add("About");
                                itemSCAbout.Click += new EventHandler(itemSCAbout_Click);

                                if (!UserCountCheckDone)
                                {
                                    using (DataStuff sn = new DataStuff())
                                    {
                                        CurUserCount = sn.GetUserCount();

                                        if (CurUserCount > NumberOfUsers)
                                        {
                                            MessageBox.Show("Your system is registered for " + NumberOfUsers.ToString() + ", but there are currently " + CurUserCount.ToString() + " active users. Please contact our support to increase your number of user licenses.", "Registered Users", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                    }

                                    UserCountCheckDone = true;
                                }

                                return;
                            }

                            if ((TrialHasExpired) && (!IsValidKey))
                            {
                                return;
                            }

                            if ((tvnCurrent.Text == "Stored Procedures") || (tvnCurrent.Text == "Table-valued Functions") || (tvnCurrent.Text == "Scalar-valued Functions") || (tvnCurrent.Text == "Aggregate Functions"))
                            {
                                //Add to filter

                                if (tvnCurrent.IsExpanded)
                                {
                                    for (int i = 0; i < cmsTmp.Items.Count; i++)
                                    {
                                        if (cmsTmp.Items[i].Text.Contains("Re&fresh"))
                                        {
                                            itemTmp = cmsTmp.Items[i];
                                            itemTmp.Click += new EventHandler(itemShowAll_Click);
                                        }
                                    }

                                    for (int i = 0; i < cmsTmp.Items.Count; i++)
                                    {
                                        if (tvnCurrent.Tag != null)
                                        {
                                            if (tvnCurrent.Tag.ToString() == "SCOnly")
                                            {
                                                itemTmp.Text = "Show All";

                                                return;
                                            }
                                        }
                                        else
                                        {
                                            ToolStripItem itemSeperator2 = cmsTmp.Items.Add("-");
                                            ToolStripItem itemFilterSC = cmsTmp.Items.Add("Source Control Items Only");
                                            itemFilterSC.Click += new EventHandler(itemShowSC_Click);
                                            return;
                                        }
                                    }
                                }
                            }

                            if ((tvnCurrent.Parent.Text == "System Stored Procedures") ||
                                (tvnCurrent.Parent.Text == "System Stored Procedures (filtered)") ||
                                (tvnCurrent.Parent.Text == "System Tables") ||
                                (tvnCurrent.Parent.Text == "System Tables (filtered)"))
                            {
                                return;
                            }

                            if ((tvnCurrent.Parent.Text != "Stored Procedures") && (tvnCurrent.Parent.Text != "Stored Procedures (filtered)") &&
                                (tvnCurrent.Parent.Text != "Table-valued Functions") && (tvnCurrent.Parent.Text != "Table-valued Functions (filtered)") &&
                                (tvnCurrent.Parent.Text != "Scalar-valued Functions") && (tvnCurrent.Parent.Text != "Scalar-valued Functions (filtered)") &&
                                (tvnCurrent.Parent.Text != "Aggregate Functions") && (tvnCurrent.Parent.Text != "Aggregate Functions (filtered)"))
                            {
                                return;
                            }
                            else
                            {
                                switch (tvnCurrent.Parent.Text)
                                {
                                    case "Stored Procedures":
                                        ObjectType = "Stored Procedure";
                                        break;

                                    case "Stored Procedures (filtered)":
                                        ObjectType = "Stored Procedure";
                                        break;

                                    case "Table-valued Functions":
                                        ObjectType = "Function";
                                        break;

                                    case "Table-valued Functions (filtered)":
                                        ObjectType = "Function";
                                        break;

                                    case "Scalar-valued Functions":
                                        ObjectType = "Function";
                                        break;

                                    case "Scalar-valued Functions (filtered)":
                                        ObjectType = "Function";
                                        break;

                                    case "Aggregate Functions":
                                        ObjectType = "Function";
                                        break;

                                    case "Aggregate Functions (filtered)":
                                        ObjectType = "Function";
                                        break;

                                    default:
                                        break;
                                }
                            }
                            
                            SelectedObject = tvnCurrent.Text;
                            DatabaseName = GetDatabaseFromPath(tvnCurrent.FullPath);

                            //Seperator

                            ToolStripItem itemSeperator = cmsTmp.Items.Add("-");

                            //Add this to all popups

                            ToolStripItem itemViewOnly = cmsTmp.Items.Add("View");
                            itemViewOnly.Click += new EventHandler(itemView_Click);

                            ToolStripItem itemCompare = cmsTmp.Items.Add("Compare");
                            itemCompare.Click += new EventHandler(itemCompare_Click);

                            ToolStripItem itemHistory = cmsTmp.Items.Add("History");
                            itemHistory.Click += new EventHandler(itemHistory_Click);

                            //Disable "Modify "menu item

                            if (tvnCurrent.Tag == null)
                            {
                                tvnCurrent.Tag = "";
                            }

                            if ((tvnCurrent.Tag.ToString() == "Locked") || (tvnCurrent.Tag.ToString() == "DevUser"))
                            {
                                //Do not allow modify

                                AllowModify = false;
                            }

                            if ((tvnCurrent.Tag.ToString() == "Caution") || (tvnCurrent.Tag.ToString() == "AllowEdit"))
                            {
                                if (IsDevServer)
                                {
                                    if (!ar.CheckObjectOutDevelopment)
                                    {
                                        //Do not allow modify

                                        AllowModify = false;
                                    }
                                }

                                if (IsUATServer)
                                {
                                    if (!ar.CheckObjectOutUAT)
                                    {
                                        //Do not allow modify

                                        AllowModify = false;
                                    }
                                }

                                if (IsPreProdServer)
                                {
                                    if (!ar.CheckObjectOutPreProduction)
                                    {
                                        //Do not allow modify

                                        AllowModify = false;
                                    }
                                }

                                if (IsProdServer)
                                {
                                    if (!ar.CheckObjectOutProduction)
                                    {
                                        //Do not allow modify

                                        AllowModify = false;
                                    }
                                }
                            }

                            if (StrictForceCheckOutDev)
                            {
                                if ((tvnCurrent.Tag.ToString() == "") && (IsDevServer))
                                {
                                    AllowModify = false;
                                }
                            }

                            //Some objects do not allow modify, encrypted procs, etc.

                            for (int i = 0; i < cmsTmp.Items.Count; i++)
                            {
                                if (cmsTmp.Items[i].Text.Contains("Modif"))
                                {
                                    if (cmsTmp.Items[i].Enabled == false)
                                    {
                                        if (SSMSVersion < 2017)
                                        {
                                            AllowView = false;
                                        }
                                    }
                                }
                            }

                            if (!AllowModify)
                            {
                                for (int i = 0; i < cmsTmp.Items.Count; i++)
                                {
                                    if (cmsTmp.Items[i].Text.Contains("Modif"))
                                    {
                                        cmsTmp.Items[i].Enabled = false;
                                    }

                                    if (cmsTmp.Items[i].Text.Contains("Script"))
                                    {
                                        cmsTmp.Items[i].Enabled = false;
                                    }
                                }
                            }
                            
                            if ((tvnCurrent.Parent.Text == "Stored Procedures") || (tvnCurrent.Parent.Text == "Stored Procedures (filtered)") ||
                                (tvnCurrent.Parent.Text == "Table-valued Functions") || (tvnCurrent.Parent.Text == "Table-valued Functions (filtered)") ||
                                (tvnCurrent.Parent.Text == "Scalar-valued Functions") || (tvnCurrent.Parent.Text == "Scalar-valued Functions (filtered)") ||
                                (tvnCurrent.Parent.Text == "Aggregate Functions") || (tvnCurrent.Parent.Text == "Aggregate Functions (filtered)"))
                            {
                                if (!AllowView)
                                {
                                    itemViewOnly.Enabled = false;
                                    itemCompare.Enabled = false;
                                    itemHistory.Enabled = false;
                                }

                                if (IsDevServer)
                                {
                                    ToolStripItem itemCheckout = cmsTmp.Items.Add("Checkout");
                                    itemCheckout.Click += new EventHandler(itemCheckout_Click);

                                    //Check the access rights for this user

                                    if (ar.CheckObjectOutDevelopment)
                                    {
                                        itemCheckout.Enabled = true;
                                    }
                                    else
                                    {
                                        itemCheckout.Enabled = false;
                                    }

                                    if (!AllowView)
                                    {
                                        itemCheckout.Enabled = false;
                                    }

                                    if ((tvnCurrent.Tag.ToString() == "Locked") || (tvnCurrent.Tag.ToString() == "AllowEdit") || (tvnCurrent.Tag.ToString() == "Caution") || (tvnCurrent.Tag.ToString() == "DevUser"))
                                    {
                                        itemCheckout.Enabled = false;

                                        ToolStripItem itemCheckoutStatus = cmsTmp.Items.Add("Checkout Status");
                                        itemCheckoutStatus.Click += new EventHandler(itemCheckoutStatus_Click);

                                        if ((tvnCurrent.Tag.ToString() == "AllowEdit") || (ar.CheckOutOverride))
                                        {
                                            //User own lock - allow undo check in

                                            ToolStripItem itemCheckoutUndo = cmsTmp.Items.Add("Undo Checkout");
                                            itemCheckoutUndo.Click += new EventHandler(itemCheckoutUndo_Click);

                                            ToolStripItem itemCheckIn = cmsTmp.Items.Add("Check In");
                                            itemCheckIn.Click += new EventHandler(itemCheckIn_Click);

                                            if ((ar.QuickRelease) && (ar.ReleaseUAT))
                                            {
                                                ToolStripItem itemQuickReleaseUAT = cmsTmp.Items.Add("Release to UAT");
                                                itemQuickReleaseUAT.Click += new EventHandler(itemQuickReleaseUAT_Click);
                                            }

                                            if (!AllowView)
                                            {
                                                itemCheckoutUndo.Enabled = false;
                                                itemCheckIn.Enabled = false;
                                            }
                                        }

                                        if ((tvnCurrent.Tag.ToString() == "DevUser") || (ar.CheckOutOverride))
                                        {
                                            //User initially checked out - allow take back

                                            ToolStripItem itemBackToDev = cmsTmp.Items.Add("Take Back to Dev");
                                            itemBackToDev.Click += new EventHandler(itemBackToDev_Click);

                                            if (!AllowView)
                                            {
                                                itemBackToDev.Enabled = false;
                                            }
                                        }
                                    }

                                    
                                }
                                else //Not the development server...
                                {
                                    if ((tvnCurrent.Tag.ToString() == "Locked") || (tvnCurrent.Tag.ToString() == "AllowEdit") || (tvnCurrent.Tag.ToString() == "Caution") || (tvnCurrent.Tag.ToString() == "DevUser"))
                                    {
                                        ToolStripItem itemCheckoutStatus = cmsTmp.Items.Add("Checkout Status");
                                        itemCheckoutStatus.Click += new EventHandler(itemCheckoutStatus_Click);

                                        if (!AllowView)
                                        {
                                            itemCheckoutStatus.Enabled = false;
                                        }
                                    }
                                }
                            }

                            ToolStripItem itemBackup = cmsTmp.Items.Add("Quick Backup");
                            itemBackup.Click += new EventHandler(itemBackup_Click);

                            ToolStripItem itemProjectBackup = cmsTmp.Items.Add("Project Backup");
                            itemProjectBackup.Click += new EventHandler(itemProjectBackup_Click);

                            ToolStripItem itemProjectRestore = cmsTmp.Items.Add("Project Restore");
                            itemProjectRestore.Click += new EventHandler(itemProjectRestore_Click);


                            if (!AllowView)
                            {
                                itemBackup.Enabled = false;
                            }


                            if (!UserCountCheckDone)
                            {
                                using (DataStuff sn = new DataStuff())
                                {
                                    CurUserCount = sn.GetUserCount();

                                    if (CurUserCount > NumberOfUsers)
                                    {
                                        MessageBox.Show("Your system is registered for " + NumberOfUsers.ToString() + ", but there are currently " + CurUserCount.ToString() + " active users. Please contact our support to increase your number of user licenses.", "Registered Users", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }

                                UserCountCheckDone = true;
                            }
                        }
                    }
                }                
            }

            catch (Exception ex)
            {

            }
        }

        private void itemProjectRestore_Click(object sender, EventArgs e)
        {
            ProjectRestore pr = new ProjectRestore();
            pr.ShowDialog();
        }

        private void itemProjectBackup_Click(object sender, EventArgs e)
        {
            ProjectBackup pb = new ProjectBackup();
            pb.ShowDialog();
        }

        private void itemSCMainProjectApproval_Click(object sender, EventArgs e)
        {
            ProjectApproval pa = new ProjectApproval();
            pa.UserID = UserID;
            pa.ShowDialog();
        }

        private void itemSCMainServerGroups_Click(object sender, EventArgs e)
        {
            ServerGroup sg = new ServerGroup();
            sg.ShowDialog();
        }

        private void itemSCAbout_Click(object sender, EventArgs e)
        {
            About a = new About();
            a.ShowDialog();
        }

        private void itemQuickReleaseUAT_Click(object sender, EventArgs e)
        {
            DialogResult result;
            int Pos = 0;

            result = MessageBox.Show("Do you want to release this object to UAT?", "Quick Release UAT", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (ServerAlias.Trim() != "" && SelectedObject.Trim() != "")
                {
                    if (SelectedObject.Contains("."))
                    {
                        Pos = SelectedObject.IndexOf(".");
                        SelectedObject = SelectedObject.Substring(Pos + 1);
                    }
                }

                //Get the UAT server connection for the same server group and do the backup of existing object

                if (BackupReleaseToServerObject(ServerAlias, DatabaseName, SelectedObject, "UAT", "Released to UAT"))
                {
                    //Create the new UAT version

                    if (DeployObject(ServerAlias, DatabaseName, SelectedObject, "UAT", "Released to UAT"))
                    {
                        MessageBox.Show(SelectedObject + " was successfully deployed to UAT.", "Quick Release", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //Status change

                        try
                        {
                            if (tvnCurrent != null)
                            {
                                if (!tvnCurrent.IsEditing)
                                {
                                    tvnCurrent.BeginEdit();
                                }

                                //Take back to dev menu option...

                                tvnCurrent.ImageIndex = 3;
                                tvnCurrent.SelectedImageIndex = 3;

                                tvnCurrent.Tag = "DevUser";
                                tvnCurrent.EndEdit(false);

                                GetObjectHistory();
                            }
                        }

                        catch
                        {

                        }
                    }
                    else
                    {
                        MessageBox.Show(SelectedObject + " was NOT deployed to UAT.", "Quick Release", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                
                return;
            }
        }

        private void itemBackToDev_Click(object sender, EventArgs e)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;

            SentryCheckOut.ObjectBackToDev co = new SentryCheckOut.ObjectBackToDev();

            string ObjectText = string.Empty;
            string ObjectDescription = string.Empty;
            string ObjectName = string.Empty;
            string OriginalStatus = string.Empty;
            string NewStatus = string.Empty;
            int Pos = 0;

            try
            {
                if (ServerAlias.Trim() != "" && SelectedObject.Trim() != "")
                {
                    if (SelectedObject.Contains("."))
                    {
                        Pos = SelectedObject.IndexOf(".");
                        SelectedObject = SelectedObject.Substring(Pos + 1);
                    }
                }

                ObjectName = SelectedObject;

                co.ObjectDescription = ObjectDescription;
                co.ObjectText = "";
                co.DatabaseName = DatabaseName;
                co.ObjectName = ObjectName;
                co.AliasName = ServerAlias;

                co.ShowDialog();

                OriginalStatus = co.OriginalStatus;
                NewStatus = co.NewStatus;

                if ((OriginalStatus.Trim() != "") && (NewStatus.Trim() != ""))
                {
                    if (OriginalStatus.Trim() != NewStatus.Trim())
                    {
                        //Status change

                        if (tvnCurrent != null)
                        {
                            switch (NewStatus)
                            {
                                case "Check out for edit":
                                    if (!tvnCurrent.IsEditing)
                                    {
                                        tvnCurrent.BeginEdit();
                                    }

                                    tvnCurrent.ImageIndex = 1;
                                    tvnCurrent.SelectedImageIndex = 1;

                                    tvnCurrent.Tag = "AllowEdit";
                                    tvnCurrent.EndEdit(false);

                                    GetObjectHistory();

                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                }
            }

            catch
            {

            }
        }

        private void itemCheckIn_Click(object sender, EventArgs e)
        {
            SentryCheckOut.ObjectCheckIn oci = new SentryCheckOut.ObjectCheckIn();

            string ObjectText = string.Empty;
            string ObjectDescription = string.Empty;
            string ObjectName = string.Empty;
            string OriginalStatus = string.Empty;
            string NewStatus = string.Empty;
            int Pos = 0;

            try
            {
                if (ServerAlias.Trim() != "" && SelectedObject.Trim() != "")
                {
                    if (SelectedObject.Contains("."))
                    {
                        Pos = SelectedObject.IndexOf(".");
                        SelectedObject = SelectedObject.Substring(Pos + 1);
                    }
                }

                ObjectName = SelectedObject;

                oci.ObjectDescription = ObjectDescription;
                oci.ObjectText = "";
                oci.DatabaseName = DatabaseName;
                oci.ObjectName = ObjectName;
                oci.AliasName = ServerAlias;

                oci.ShowDialog();

                OriginalStatus = oci.OriginalStatus;
                NewStatus = oci.NewStatus;

                if ((OriginalStatus.Trim() != "") && (NewStatus.Trim() != ""))
                {
                    if (OriginalStatus.Trim() != NewStatus.Trim())
                    {
                        //Status change

                        if (tvnCurrent != null)
                        {
                            switch (NewStatus)
                            {
                                case "Check In":
                                    if (!tvnCurrent.IsEditing)
                                    {
                                        tvnCurrent.BeginEdit();
                                    }

                                    tvnCurrent.ImageIndex = 9;
                                    tvnCurrent.SelectedImageIndex = 9;

                                    tvnCurrent.Tag = "";
                                    tvnCurrent.EndEdit(false);

                                    GetObjectHistory();

                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                }
            }

            catch
            {

            }
        }

        private void itemSCDataStore_Click(object sender, EventArgs e)
        {
            DataStore ds = new DataStore();
            ds.ShowDialog();

            if (ds.DataStoreConnectionString.Trim() != "")
            {
                StoreConnectionString = ds.DataStoreConnectionString;
            }
        }

        private void itemCheckoutUndo_Click(object sender, EventArgs e)
        {
            SentryCheckOut.ObjectUndoCheckOut uco = new SentryCheckOut.ObjectUndoCheckOut();

            string ObjectText = string.Empty;
            string ObjectDescription = string.Empty;
            string ObjectName = string.Empty;
            string OriginalStatus = string.Empty;
            string NewStatus = string.Empty;
            int Pos = 0;

            try
            {
                if (ServerAlias.Trim() != "" && SelectedObject.Trim() != "")
                {
                    if (SelectedObject.Contains("."))
                    {
                        Pos = SelectedObject.IndexOf(".");
                        SelectedObject = SelectedObject.Substring(Pos + 1);
                    }
                }

                ObjectName = SelectedObject;

                uco.ObjectDescription = ObjectDescription;
                uco.ObjectText = "";
                uco.DatabaseName = DatabaseName;
                uco.ObjectName = ObjectName;
                uco.AliasName = ServerAlias;

                uco.ShowDialog();

                OriginalStatus = uco.OriginalStatus;
                NewStatus = uco.NewStatus;

                if ((OriginalStatus.Trim() != "") && (NewStatus.Trim() != ""))
                {
                    if (OriginalStatus.Trim() != NewStatus.Trim())
                    {
                        //Status change

                        if (tvnCurrent != null)
                        {
                            switch (NewStatus)
                            {
                                case "Check-out Undo":
                                    if (!tvnCurrent.IsEditing)
                                    {
                                        tvnCurrent.BeginEdit();
                                    }

                                    tvnCurrent.ImageIndex = 9;
                                    tvnCurrent.SelectedImageIndex = 9;

                                    tvnCurrent.Tag = "";
                                    tvnCurrent.EndEdit(false);

                                    GetObjectHistory();

                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                }
            }

            catch
            {

            }
        }

        private void itemBackup_Click(object sender, EventArgs e)
        {
            string ObjectText = string.Empty;
            string ObjectDescription = string.Empty;
            string ObjectName = string.Empty;
            string OriginalStatus = string.Empty;
            string NewStatus = string.Empty;
            string PreviousUser = string.Empty;
            string PreviousComment = string.Empty;
            string PreviousProject = string.Empty;
            int PreviousVersion = 0;

            int Pos = 0;

            try
            {
                if (CurrentServerConnectionString.Trim() == "")
                {
                    GetServerDetail(ServerAlias);
                }

                if (ServerAlias.Trim() != "" && SelectedObject.Trim() != "")
                {
                    if (SelectedObject.Contains("."))
                    {
                        Pos = SelectedObject.IndexOf(".");
                        SelectedObject = SelectedObject.Substring(Pos + 1);
                    }
                }

                ObjectName = SelectedObject;
                ObjectText = LoadHelpText(CurrentServerConnectionString, DatabaseName, ObjectName);

                if (ObjectText.Trim() != "")
                {
                    try
                    {
                        using (DataStuff sn = new DataStuff())
                        {
                            DataTable dt = sn.GetObjectStatusDetail(ServerAlias, ObjectName);

                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    OriginalStatus = "Backup";
                                    PreviousUser = row["UserName"].ToString();
                                    PreviousComment = row["Comment"].ToString();
                                    PreviousProject = row["ProjectName"].ToString();
                                    PreviousVersion = Convert.ToInt32(row["ObjectVersion"].ToString());
                                }
                            }
                            else
                            {
                                OriginalStatus = "Backup";
                                PreviousUser = UserName;
                                PreviousComment = "";
                                PreviousProject = "";
                                PreviousVersion = 0;
                            }
                        }
                    }

                    catch
                    {
                        throw;
                    }

                    using (DataStuff sn = new DataStuff())
                    {
                        //sn.ConnectionString = SQLSentry.Properties.Settings.Default.connectionString;

                        if (!sn.BackupObject(ServerAlias, OriginalStatus, PreviousUser, ObjectName, DatabaseName, ObjectText, PreviousComment, PreviousProject, "", ObjectType, "N", "N"))
                        {
                            MessageBox.Show("There was an error creating the backup for this object. Please create a manual backup of your object.", "Quick Backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("The backup of your object was successfully created.", "Quick Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("There was an error creating the backup for this object. Please create a manual backup of your object.", "Quick Backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            catch
            {
                MessageBox.Show("There was an error creating the backup for this object. Please create a manual backup of your object.", "Quick Backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void itemSCMainAdmin_Click(object sender, EventArgs e)
        {
            Administrator ad = new Administrator();
            ad.ShowDialog();
        }

        private void itemView_Click(object sender, EventArgs e)
        {
            string ObjectText = string.Empty;
            string ObjectDescription = string.Empty;
            string ObjectName = string.Empty;
            string OriginalStatus = string.Empty;
            string NewStatus = string.Empty;
            int Pos = 0;

            try
            {
                if (CurrentServerConnectionString.Trim() == "")
                {
                    GetServerDetail(ServerAlias);
                }

                ObjectViewer ov = new ObjectViewer();

                if (ServerAlias.Trim() != "" && SelectedObject.Trim() != "")
                {
                    if (SelectedObject.Contains("."))
                    {
                        Pos = SelectedObject.IndexOf(".");
                        SelectedObject = SelectedObject.Substring(Pos + 1);
                    }
                }

                ObjectName = SelectedObject;

                ov.ConnectionString = CurrentServerConnectionString;
                ov.ObjectDescription = ObjectName;
                ov.ObjectText = "";
                ov.DatabaseName = DatabaseName;
                ov.ObjectName = ObjectName;
                ov.AliasName = ServerAlias;

                ov.ShowDialog();
            }

            catch
            {

            }
        }

        private string GetDatabaseFromPath(string FullPath)
        {
            int Pos = 0;
            string TmpDBName = string.Empty;

            if (FullPath.Contains("Databases"))
            {
                Pos = FullPath.IndexOf("Databases");

                if (Pos > 0)
                {
                    FullPath = FullPath.Substring(Pos + 10);
                }

                Pos = FullPath.IndexOf("\\");

                if (Pos > 0)
                {
                    TmpDBName = FullPath.Substring(0, Pos);
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }

            return TmpDBName;
        }

        private void GetServerFromPath(string FullPath)
        {
            int Pos = 0;
            string TmpServer = string.Empty;

            if (FullPath.Contains("("))
            {
                Pos = FullPath.IndexOf("(");

                if (Pos > 0)
                {
                    TmpServer = FullPath.Substring(0, Pos - 1);
                    GetServerRole(TmpServer.Trim());
                    GetServerDetail(ServerAlias);
                }
            }
        }

        private bool GetServerDetail(string ServerAlias)
        {
            try
            {
                string ServerName = string.Empty;
                string UserName = string.Empty;
                byte[] Password = null;
                string IntegratedSecurity = string.Empty;

                using (DataStuff sn = new DataStuff())
                {
                    //sn.ConnectionString = Properties.Settings.Default.connectionString;
                    DataTable dt = sn.GetServerDetail(ServerAlias, UserID);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            ServerName = row["ServerName"].ToString();
                            UserName = row["UserName"].ToString();
                            Password = (byte[])row["Password"];
                            IntegratedSecurity = row["IntegratedSecurity"].ToString();
                        }
                    }
                }

                if (IntegratedSecurity == "Y")
                {
                    CurrentServerConnectionString = "Data Source=" + ServerName + ";Integrated Security=True";
                }
                else
                {
                    string DecryptedPassword = string.Empty;

                    using (DataStuff ds = new DataStuff())
                    {
                        DecryptedPassword = ds.Ontsyfer(Password);
                    }

                    CurrentServerConnectionString = "Data Source=" + ServerName + ";User ID=" + UserName + ";Password=" + DecryptedPassword;
                }

                if (CurrentServerConnectionString.Trim() != "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            catch
            {
                return false;
            }
        }

        private void itemSCMainSearch_Click(object sender, EventArgs e)
        {
            if (CurrentServerConnectionString.Trim() == "")
            {
                GetServerDetail(ServerAlias);
            }

            SentrySearch.Search search = new SentrySearch.Search();
            search.ConnectionString = CurrentServerConnectionString;
            search.StoreConnectionString = StoreConnectionString;
            search.ShowDialog();
        }

        private void itemSCMainProjectDeploy_Click(object sender, EventArgs e)
        {
            ProjectDeploy pd = new ProjectDeploy();
           
            pd.ShowDialog();
        }

        private void itemSCMainProject_Click(object sender, EventArgs e)
        {
            ProjectMaintenance p = new ProjectMaintenance();
            p.ShowDialog();
        }
        
        private void itemSCMainBackup_Click(object sender, EventArgs e)
        {
            BackupRestore br = new BackupRestore();
            br.ShowDialog();
        }

        private void itemSCMainServers_Click(object sender, EventArgs e)
        {
            ServerSettings ss = new ServerSettings();
            ss.ShowDialog();
        }
        
        private void itemShowAll_Click(object sender, EventArgs e)
        {
            if (tvnCurrent.Tag != null)
            {
                if (tvnCurrent.Tag.ToString() == "SCOnly")
                {
                    tvnCurrent.Tag = null;
                    return;
                }
            }
        }

        private void itemShowSC_Click(object sender, EventArgs e)
        {
            bool FoundItem = false;
            int LastItem = 0;

            if (tvnCurrent != null)
            {
                for (int i = tvnCurrent.Nodes.Count - 1; i >= 0; i--)
                {
                    if (((string)tvnCurrent.Nodes[i].Tag != "") && (tvnCurrent.Nodes[i].Tag != null))
                    {
                        FoundItem = true;
                    }
                }

                if (FoundItem)
                {
                    LastItem = -1;
                }

                for (int i = tvnCurrent.Nodes.Count - 1; i > LastItem; i--)
                {
                    if ((tvnCurrent.Nodes[i].Tag == null) || ((string)tvnCurrent.Nodes[i].Tag == ""))
                    {
                        if (!tvnCurrent.IsEditing)
                        {
                            tvnCurrent.BeginEdit();
                        }

                        tvnCurrent.Nodes[i].Remove();

                        tvnCurrent.EndEdit(false);
                    }
                }

                tvnCurrent.Tag = "SCOnly";
                return;
            }
        }

        void itemCheckout_Click(object sender, EventArgs e)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;

            SentryCheckOut.ObjectCheckOut co = new SentryCheckOut.ObjectCheckOut();

            string ObjectText = string.Empty;
            string ObjectDescription = string.Empty;
            string ObjectName = string.Empty;
            string OriginalStatus = string.Empty;
            string NewStatus = string.Empty;
            int Pos = 0;

            try
            {
                if (ServerAlias.Trim() != "" && SelectedObject.Trim() != "")
                {
                    if (SelectedObject.Contains("."))
                    {
                        Pos = SelectedObject.IndexOf(".");
                        SelectedObject = SelectedObject.Substring(Pos + 1);
                    }
                }

                ObjectName = SelectedObject;

                co.ObjectDescription = ObjectDescription;
                co.ObjectText = "";
                co.DatabaseName = DatabaseName;
                co.ObjectName = ObjectName;
                co.AliasName = ServerAlias;

                co.ShowDialog();

                OriginalStatus = co.OriginalStatus;
                NewStatus = co.NewStatus;

                if ((OriginalStatus.Trim() != "") && (NewStatus.Trim() != ""))
                {
                    if (OriginalStatus.Trim() != NewStatus.Trim())
                    {
                        //Status change

                        if (tvnCurrent != null)
                        {
                            switch (NewStatus)
                            {
                                case "Check out for edit":
                                    if (!tvnCurrent.IsEditing)
                                    {
                                        tvnCurrent.BeginEdit();
                                    }

                                    tvnCurrent.ImageIndex = 1;
                                    tvnCurrent.SelectedImageIndex = 1;
                                    
                                    tvnCurrent.Tag = "AllowEdit";
                                    tvnCurrent.EndEdit(false);

                                    GetObjectHistory();

                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                }
            }

            catch
            {

            }
        }

        private string LoadHelpText(string CnnString, string DBName, string ObjName)
        {
            string ObjectText = string.Empty;

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetObjectHelpText(CnnString, DBName, ObjName);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            ObjectText = ObjectText + row[0].ToString();
                        }
                    }
                }

                return ObjectText;
            }

            catch
            {
                return "";
            }
        }

        void itemCompare_Click(object sender, EventArgs e)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;

            int Pos = 0;

            if (ServerAlias.Trim() != "" && SelectedObject.Trim() != "")
            {
                if (SelectedObject.Contains("."))
                {
                    Pos = SelectedObject.IndexOf(".");
                    SelectedObject = SelectedObject.Substring(Pos + 1);
                }
            }

            EnvironmentSelect es = new EnvironmentSelect();

            es.Alias1 = ServerAlias;
            es.DatabaseName = DatabaseName;
            es.ObjectName = SelectedObject;
            es.ObjectType = "";

            es.ShowDialog();
        }

        void itemHistory_Click(object sender, EventArgs e)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;

            ViewObjectHistory oh = new ViewObjectHistory();

            int Pos = 0;

            if (ServerAlias.Trim() != "" && SelectedObject.Trim() != "")
            {
                if (SelectedObject.Contains("."))
                {
                    Pos = SelectedObject.IndexOf(".");
                    SelectedObject = SelectedObject.Substring(Pos + 1);
                }
            }

            oh.DatabaseName = DatabaseName;
            oh.ObjectName = SelectedObject;

            oh.ShowDialog();
        }

        void itemCheckoutStatus_Click(object sender, EventArgs e)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;
            int Pos = 0;

            if (ServerAlias.Trim() != "" && SelectedObject.Trim() != "")
            {
                if (SelectedObject.Contains("."))
                {
                    Pos = SelectedObject.IndexOf(".");
                    SelectedObject = SelectedObject.Substring(Pos + 1);
                }

                ViewObjectStatus vs = new ViewObjectStatus();
                
                vs.AliasName = ServerAlias;
                vs.ObjectName = SelectedObject;

                vs.ShowDialog();
            }
        }

        private void AttachTreeviewImageList()
        {
            ImageList myImageList = new ImageList();

            myImageList.Images.Add(SQLSentry.Properties.Resources._lock);
            myImageList.Images.Add(SQLSentry.Properties.Resources.lock_ok);
            myImageList.Images.Add(SQLSentry.Properties.Resources.lock_information);
            myImageList.Images.Add(SQLSentry.Properties.Resources.lock_edit);

            tvExplorer.ImageList = myImageList;
        }

        private bool GetServerRole(string SrvName)
        {
            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetServerRole(SrvName, UserID);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            ServerRole = row["ServerRoleDesc"].ToString();
                            ServerName = row["ServerName"].ToString();
                            ServerAlias = row["ServerAliasDesc"].ToString();
                        }

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            catch
            {
                return false;
            }
        }

        private bool IsDataStoreAvailable()
        {
            using (DataStuff sn = new DataStuff())
            {
                if (sn.ConnectionString.Trim() == "")
                {
                    //Data store connection not set

                    MessageBox.Show("No data store settings found. Please configure your data store in the form that will show shortly...", "Data Store Configuration", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DataStore ds = new DataStore();

                    ds.ShowDialog();

                    StoreConnectionString = ds.DataStoreConnectionString;
                }
                else
                {
                    StoreConnectionString = sn.ConnectionString;
                }

                if (StoreConnectionString.Trim() == "")
                {
                    return false;
                }

                if (sn.ConnectionString.Trim() == "")
                {
                    sn.ConnectionString = StoreConnectionString;
                }
                    
                //See if the server group has been defined...

                if (!sn.IsServerGroupDefined())
                {
                    MessageBox.Show("There are no server groups defined. Please add a server group in the next dialog.", "Server Group", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ServerGroup sg = new ServerGroup();
                    sg.ShowDialog();
                }

                if (sn.ConnectionString.Trim() == "")
                {
                    sn.ConnectionString = StoreConnectionString;
                }

                if (StoreConnectionString.Trim() == "")
                {
                    return false;
                }

                DataTable dt = sn.SingleUser(Environment.UserName, "Package Load");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        UserName = row["UserName"].ToString();
                        UserID = Convert.ToInt32(row["UserID"].ToString());
                        EnvironmentUserName = row["QTNumber"].ToString();
                    }
                }
                else
                {
                    AddUser au = new AddUser();
                    au.ShowDialog();

                    DataTable dt2 = sn.SingleUser(Environment.UserName, "Package Load");

                    if (dt2.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt2.Rows)
                        {
                            UserName = row["UserName"].ToString();
                            UserID = Convert.ToInt32(row["UserID"].ToString());
                            EnvironmentUserName = row["QTNumber"].ToString();
                        }
                    }
                }

                ar.UserID = UserID;
            }

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetServers(UserID);

                    if (dt.Rows.Count > 0)
                    {
                        CheckRegisteredVersion();
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("You do not have any server logins linked to your user. Please set up your servers in the next dialog.", "Server Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ServerSettings ss = new ServerSettings();
                        ss.ShowDialog();

                        return true;
                    }
                }
            }

            catch
            {
                return false;
            }
        }

        private void CheckRegisteredVersion()
        {
            string FirstReg = string.Empty;
            string RegistrationKey = string.Empty;
            string TamperKey = string.Empty;
            string Part1 = string.Empty;
            string Part2 = string.Empty;
            string Part3 = string.Empty;
            int KeyPart1 = 0;
            int KeyPart2 = 0;
            int KeyPart3 = 0;
            int DaysNow = 0;
            DateTime dt1;
            DateTime dt2;
            DateTime Date2000;

            IsValidKey = true;

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    FirstReg = sn.GetSystemSetting("Key1");
                    RegistrationKey = sn.GetSystemSetting("Key2");
                    CompanyName = sn.GetSystemSetting("Key3");

                    if (CompanyName.Trim() == "")
                    {
                        CompanyName = "Trial Version";
                    }

                    TamperKey = sn.GetSystemSetting("Key4");

                    if (FirstReg.Trim() == "")
                    {
                        FirstReg = DateTime.Now.ToString("dd MMM yyyy");

                        sn.SaveSystemSetting("Key1", FirstReg);
                    }
                }
            }

            catch
            {

            }

            try
            {
                if (RegistrationKey.Trim() == "")
                {
                    //No registration yet, trial version

                    IsTrialVersion = true;
                    UpdatesValidTo = Convert.ToDateTime(FirstReg).AddDays(14).ToString("dd MMM yyyy");

                    dt1 = Convert.ToDateTime(FirstReg);
                    dt2 = Convert.ToDateTime(DateTime.Now.ToString("dd MMM yyyy"));

                    TrialDayNo = dt2.Subtract(dt1).Days;

                    if (TrialDayNo > 14)
                    {
                        TrialHasExpired = true;
                    }
                }
            }

            catch
            {
                TrialDayNo = 99;
                IsTrialVersion = true;
                TrialHasExpired = true;
            }

            try
            { 
                {
                    if (RegistrationKey.Length == 12)
                    {
                        if (RegistrationKey != TamperKey)
                        {
                            //Invalid key

                            IsValidKey = false;
                        }
                        else
                        {
                            Part1 = RegistrationKey.Substring(0, 4);
                            Part2 = RegistrationKey.Substring(4, 4);
                            Part3 = RegistrationKey.Substring(8, 4);

                            if (int.TryParse(Part1, out KeyPart1))
                            {
                                Date2000 = Convert.ToDateTime("22 Aug 2016");
                                DaysNow = (DateTime.Now - Date2000).Days;
                                
                                if ((KeyPart1 - 1009) >= DaysNow)
                                {
                                    UpdateValid = true;
                                    UpdatesValidTo = Convert.ToDateTime(Date2000).AddDays(KeyPart1 - 1009).ToString("dd MMM yyyy");
                                }
                            }
                            else
                            {
                                UpdatesValidTo = "No information found";
                                IsValidKey = false;
                            }

                            if (int.TryParse(Part2, out KeyPart2))
                            {
                                if (KeyPart2 > 4831)
                                {
                                    NumberOfUsers = 0;
                                    IsValidKey = false;
                                }
                                else
                                {
                                    NumberOfUsers = 4831 - KeyPart2;
                                }
                            }
                            else
                            {
                                IsValidKey = false;
                            }

                            if (int.TryParse(Part3, out KeyPart3))
                            {
                                if (KeyPart3 % 22 != 0)
                                {    
                                    IsValidKey = false;
                                }
                            }
                            else
                            {
                                IsValidKey = false;
                            }
                        }
                    }
                    else
                    {
                        //Invalid key

                        IsValidKey = false;
                    }
                }
            }

            catch (Exception ex)
            {
                IsValidKey = false;
            }
        }

        private bool GetObjectHistory()
        {
            try
            {
                dtObjects = null;

                using (DataStuff sn = new DataStuff())
                {
                    dtObjects = sn.GetObjectHistorySSMS(ServerName, UserID);

                    if (dtObjects.Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Adds new nodes and move items between them
        /// </summary>
        /// <param name="node"></param>
        private void ReorganizeFolders(TreeNode node)
        {
            //return;
            //debug_message("ReorganizeFolders");

            try
            {
                // uses node.Tag to prevent this running again on already orgainsed schema folder
                if (node != null && node.Parent != null && (node.Tag == null || node.Tag.ToString() != "SchemaFolder"))
                {
                    var urnPath = _objectExplorerExtender.GetNodeUrnPath(node);

                    if (!string.IsNullOrEmpty(urnPath))
                    {
                        //debug_message(String.Format("NodeInformation\n UrnPath:{0}\n Name:{1}\n InvariantName:{2}\n Context:{3}\n NavigationContext:{4}", ni.UrnPath, ni.Name, ni.InvariantName, ni.Context, ni.NavigationContext));
                        
                        switch (urnPath)
                        {
                            case "Server/Database/UserTablesFolder":
                            case "Server/Database/ViewsFolder":
                            case "Server/Database/SynonymsFolder":
                            case "Server/Database/StoredProceduresFolder":
                            case "Server/Database/Table-valuedFunctionsFolder":
                            case "Server/Database/Scalar-valuedFunctionsFolder":
                            case "Server/Database/SystemTablesFolder":
                            case "Server/Database/SystemViewsFolder":
                            case "Server/Database/SystemStoredProceduresFolder":
                                CheckEditStatus(node);
                                
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //ActivityLogEntry(__ACTIVITYLOG_ENTRYTYPE.ALE_ERROR, ex.ToString());
            }
        }

        private void CheckEditStatus(TreeNode node)
        {
            string Status = string.Empty;
            string NodeName = string.Empty;
            string DBName = string.Empty;
            int Pos = 0;
            int TreeCounter = 0;
            int TotalNodes = 0;
            string ObjectsTested = string.Empty;
            bool FoundStatus = false;

            TotalNodes = node.Nodes.Count;

            Application.DoEvents();

            while (node.Text.Contains("expanding..."))
            {
                Application.DoEvents();
            }

            try
            {
                for (int i = node.Nodes.Count - 1; i > -1; i--)
                {
                    FoundStatus = false;

                    var tn = node.Nodes[i];

                    NodeName = tn.Text;

                    DBName = tn.Parent.Parent.Parent.Text;

                    if (DBName == "Programmability")
                    {
                        DBName = tn.Parent.Parent.Parent.Parent.Text;
                    }

                    if (NodeName.Contains("."))
                    {
                        Pos = NodeName.IndexOf(".");
                        NodeName = NodeName.Substring(Pos + 1);
                    }
                    
                    Status = ObjectStatus(DBName, NodeName);

                    Application.DoEvents();

                    if (Status != "")
                    {
                        TreeCounter++;
                        ObjectsTested = ObjectsTested + NodeName + " - ";

                        //The last status of the object in the database = "AvailableForEdit = 'N', thus AllowEdit == false

                        if (!AllowEdit)
                        {
                            if (IsDevServer)
                            {
                                if (ObjectUser.ToUpper() == UserName.ToUpper())
                                {
                                    //This is still the user that did the previous status change

                                    if (Status.Contains("Check out for edit"))
                                    {
                                        tn.ImageIndex = 1;
                                        tn.SelectedImageIndex = 1;
                                        tn.Tag = "AllowEdit";
                                        tn.ToolTipText = HistoryDetail;
                                        FoundStatus = true;
                                    }
                                }

                                if ((CheckOutUser.ToUpper() == UserName.ToUpper()) && (!FoundStatus))
                                {
                                    //Take back to dev menu option...

                                    tn.ImageIndex = 3;
                                    tn.SelectedImageIndex = 3;
                                    tn.Tag = "DevUser";
                                    FoundStatus = true;
                                }

                                if (!FoundStatus)
                                {
                                    //Released to somewhere...

                                    tn.ImageIndex = 0;
                                    tn.SelectedImageIndex = 0;
                                    tn.Tag = "Locked";
                                }
                            }
                            else  //Not the dev server
                            {
                                //We do not want to block emergency production changes, just make the person aware of the development taking place...

                                tn.ImageIndex = 2;
                                tn.SelectedImageIndex = 2;
                                tn.Tag = "Caution";
                            }
                            
                            tn.ToolTipText = HistoryDetail;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return;
        }

        private string ObjectStatus(string DatabaseName, string ObjectName)
        {
            HistoryDetail = "";
            ObjectUser = "";
            AllowEdit = false;
            InDevCycle = false;
            CheckOutUser = "";

            try
            {
                if (dtObjects != null)
                {
                    DataView dv = new DataView(dtObjects);
                    string Filter = string.Empty;

                    Filter = "([Name] = '" + ObjectName.Trim() + "') and ([Database] = '" + DatabaseName + "')  and ([AvailableForEdit] = 'N')";

                    dv.RowFilter = Filter;

                    DataTable dtTmp = dv.ToTable();

                    if (dtTmp.Rows.Count > 0)
                    {
                        HistoryDetail = CurrentServer;
                        HistoryDetail = HistoryDetail + " " + dtTmp.Rows[0]["Status"].ToString() + " by " + dtTmp.Rows[0]["User"].ToString() + " (" + dtTmp.Rows[0]["Server"].ToString() + ") on " + dtTmp.Rows[0]["Date"].ToString();
                        ObjectUser = dtTmp.Rows[0]["User"].ToString();
                        CheckOutUser = dtTmp.Rows[0]["CheckOutUser"].ToString();
                        StatusID = Convert.ToInt32(dtTmp.Rows[0]["StatusID"].ToString());


                        if (dtTmp.Rows[0]["AvailableForEdit"].ToString() == "N")
                        {
                            AllowEdit = false;
                        }
                        else
                        {
                            AllowEdit = true;
                        }

                        if (dtTmp.Rows[0]["PartOfDevCycle"].ToString() == "N")
                        {
                            InDevCycle = false;
                        }
                        else
                        {
                            InDevCycle = true;
                        }

                        return dtTmp.Rows[0]["Status"].ToString() + ":" + dtTmp.Rows[0]["User"].ToString();
                    }
                    else
                    {
                        return "";
                    }
                }
            }

            catch
            {
                
            }

            return "";
        }

        /// <summary>
        /// After expand node
        /// </summary>
        /// <param name="sender">object explorer</param>
        /// <param name="e">expanding node</param>
        void ObjectExplorerTreeViewAfterExpandCallback(object sender, TreeViewEventArgs e)
        {
            //debug_message("\nObjectExplorerTreeViewAfterExpandCallback");
            // Wait for the async node expand to finish or we could miss nodes
            try
            {
                //debug_message(String.Format("Node.Count:{0}", e.Node.GetNodeCount(false)));

                //if (!Options.Enabled)
                //    return;

                //if (_objectExplorerExtender.GetNodeExpanding(e.Node))
                //{
                //    debug_message("node.Expanding");
                //    // waitCount required for how single core cpu's handle Application.DoEvents().
                //    var waitCount = 0;
                //    while (waitCount < 10000 && _objectExplorerExtender.GetNodeExpanding(e.Node))
                //    {
                //        Application.DoEvents();
                //        waitCount++;
                //    }
                //    debug_message(String.Format("waitCount:{0}", waitCount));
                //    debug_message(String.Format("Node.Count:{0}", e.Node.GetNodeCount(false)));
                //}

                //if (tvnCurrent.FullPath != "")
                //{

                //    DatabaseName = GetDatabaseFromPath(tvnCurrent.FullPath);
                //    GetServerFromPath(tvnCurrent.FullPath);

                //    switch (ServerRole.Trim())
                //    {
                //        case "Development":
                //            IsDevServer = true;
                //            break;

                //        case "User Acceptance Testing":
                //            IsUATServer = true;
                //            break;

                //        case "Pre-Production":
                //            IsPreProdServer = true;
                //            break;

                //        case "Production":
                //            IsProdServer = true;
                //            break;

                //        default:
                //            break;
                //    }

                //    CheckEditStatus(tvnCurrent.Parent);

                //}

                
                ReorganizeFolders(e.Node);

                //MessageBox.Show(ObjectsTestedOverall);
            }
            catch (Exception ex)
            {
                //ActivityLogEntry(__ACTIVITYLOG_ENTRYTYPE.ALE_ERROR, ex.ToString());
            }
        }

        /// <summary>
        /// Object explorer tree view: event before expand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ObjectExplorerTreeViewBeforeExpandCallback(object sender, TreeViewCancelEventArgs e)
        {
            //debug_message("\nObjectExplorerTreeViewBeforeExpandCallback");
            TreeNode TmpNode = null;
            TreeNode RootNode = null;
            bool DataStoreIsAvailable = false;

            try
            {
                if (ServerFound)
                {
                    if (e.Node.Parent == null)
                    {
                        if (e.Node.Text.Contains("expanding..."))
                        {
                            return;
                        }

                        if (e.Node.Text.Trim() != CurrentServer.Trim())
                        {
                            ServerFound = false;
                        }
                    }
                }

                if (!IsDataStoreAvailable())
                {
                    return;
                }
                else
                {
                    DataStoreIsAvailable = true;
                }

                if ((e.Node.Text != "Stored Procedures") && (e.Node.Text != "Stored Procedures (filtered)") &&
                    (e.Node.Text != "Table-valued Functions") && (e.Node.Text != "Table-valued Functions (filtered)") &&
                    (e.Node.Text != "Scalar-valued Functions") && (e.Node.Text != "Scalar-valued Functions (filtered)") &&
                    (e.Node.Text != "Aggregate Functions") && (e.Node.Text != "Aggregate Functions (filtered)"))
                {
                    return;
                }

                if (DataStoreIsAvailable)
                {
                    IsDevServer = false;
                    IsUATServer = false;
                    IsPreProdServer = false;
                    IsProdServer = false;

                    TmpNode = e.Node;

                    while (TmpNode != null)
                    {
                        if (TmpNode.Parent == null)
                        {
                            RootNode = TmpNode;
                            TmpNode = null;
                        }
                        else
                        {
                            TmpNode = TmpNode.Parent;
                        }
                    }

                    CurrentServer = RootNode.Text;

                    if (GetServerRole(CurrentServer))
                    {
                        switch (ServerRole.Trim())
                        {
                            case "Development":
                                IsDevServer = true;
                                break;

                            case "User Acceptance Testing":
                                IsUATServer = true;
                                break;

                            case "Pre-Production":
                                IsPreProdServer = true;
                                break;

                            case "Production":
                                IsProdServer = true;
                                break;

                            default:
                                break;
                        }
                    }
                    else
                    {
                        ServerName = "";
                        ServerRole = "";
                    }

                    ServerFound = true;
                    GetObjectHistory();
                }
                
                if (!Options.Enabled)
                    return;

                if (e.Node.GetNodeCount(false) == 1)
                    return;
            }
            catch (Exception ex)
            {
                ActivityLogEntry(__ACTIVITYLOG_ENTRYTYPE.ALE_ERROR, ex.ToString());
            }
            
        }

        private void GetServerRoleOnCurrentConnection()
        {
            TreeNode TmpNode = null;
            TreeNode RootNode = null;
            
            
            IsDevServer = false;
            IsUATServer = false;
            IsPreProdServer = false;
            IsProdServer = false;

            TmpNode = tvnCurrent;

            while (TmpNode != null)
            {
                if (TmpNode.Parent == null)
                {
                    RootNode = TmpNode;
                    TmpNode = null;
                }
                else
                {
                    TmpNode = TmpNode.Parent;
                }
            }

            CurrentServer = RootNode.Text;

            if (GetServerRole(CurrentServer))
            {
                switch (ServerRole.Trim())
                {
                    case "Development":
                        IsDevServer = true;
                        break;

                    case "User Acceptance Testing":
                        IsUATServer = true;
                        break;

                    case "Pre-Production":
                        IsPreProdServer = true;
                        break;

                    case "Production":
                        IsProdServer = true;
                        break;

                    default:
                        break;
                }
            }
            else
            {
                ServerName = "";
                ServerRole = "";
            }
        }

        private void debug_message(string message)
        {
            if (_outputWindowPane != null)
            {
                _outputWindowPane.OutputString(message);
                _outputWindowPane.OutputString("\r\n");
            }
        }

        private void ActivityLogEntry(__ACTIVITYLOG_ENTRYTYPE entryType, string message)
        {
            debug_message(message);

            // Logs to %AppData%\Microsoft\VisualStudio\14.0\ActivityLog.XML.
            // Recommended to obtain the activity log just before writing to it. Do not cache or save the activity log for future use.
            var log = GetService(typeof(SVsActivityLog)) as IVsActivityLog;
            if (log == null) return;

            int hr = log.LogEntryGuid(
                (UInt32)entryType,
                this.ToString(),
                message,
                SQLSentryPackage.PackageGuid);
        }

        private bool BackupReleaseToServerObject(string ReleaseFromServer, string DatabaseName, string ObjectName, string EnvironmentTo, string ReleaseToStatus)
        {
            string ObjectText = string.Empty;
            string ConnectionStr = string.Empty;
            bool Result = false;
            
            //The ReleaseFromServer is the current server, the stored procedure will return the UAT, PreProd or Prod connection string for the server group

            ConnectionStr = GetServerConnectionString(ReleaseFromServer, UserID, EnvironmentTo);

            try
            {
                ObjectText = LoadHelpText(ConnectionStr, DatabaseName, ObjectName);
            }

            catch
            {
                DialogResult result = MessageBox.Show("An error occurred getting the object text for server " + ReleaseToServer + ". Release anyway?", "Backup Object", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (result == DialogResult.No)
                {
                    return false;
                }
                else
                {
                    IsNewObjectQuickRelease = true;
                    return true;
                }
            }
            
            try
            {
                string BackupComment;

                BackupComment = UserName + " - Backup copy before quick release";
                
                using (DataStuff sn = new DataStuff())
                {
                    Result = sn.BackupObject(ReleaseToServer, ReleaseToStatus, UserName, ObjectName, DatabaseName, ObjectText, BackupComment, "", "", "", "N", "N");

                    if (!Result)
                    {
                        MessageBox.Show("There was an error creating a backup for this object. Please ensure that your servers have been set up correctly.", "Change Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            catch
            {
                MessageBox.Show("An error occurred while creating the backup record.", "Backup Object", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool DeployObject(string ReleaseFromServer, string DatabaseName, string ObjectName, string EnvironmentTo, string ReleaseToStatus)
        {
            string RetVal = string.Empty;
            string ConnectionStr = string.Empty;
            string ObjectText = string.Empty;

            General gs = new General();

            if (CurrentServerConnectionString.Trim() == "")
            {
                GetServerDetail(ServerAlias);
            }
            
            ObjectText = LoadHelpText(CurrentServerConnectionString, DatabaseName, ObjectName);

            //The ReleaseFromServer is the current server, the stored procedure will return the UAT, PreProd or Prod connection string for the server group

            ConnectionStr = GetServerConnectionString(ReleaseFromServer, UserID, EnvironmentTo);

            if (ConnectionStr.Trim() == "")
            {
                return false;
            }

            try
            {
                ObjectText = AddProcComment(ObjectText, ReleaseToStatus, "Quick release to " + EnvironmentTo);

                ObjectText = gs.ChangeCreateToAlter(ObjectText);

                //Deploy to the from server to include the new comment...

                using (DataStuff sn = new DataStuff())
                {
                    CurrentServerConnectionString = CurrentServerConnectionString + ";Database=" + DatabaseName;

                    RetVal = sn.CreateObjectS(CurrentServerConnectionString, ObjectText);

                    if (RetVal != "")
                    {
                        MessageBox.Show("Failed - " + RetVal, "Modify Procedure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                if (IsNewObjectQuickRelease)
                {
                    ObjectText = gs.ChangeAlterToCreate(ObjectText);
                }
                
                using (DataStuff sn = new DataStuff())
                {
                    ConnectionStr = ConnectionStr + ";Database=" + DatabaseName;

                    RetVal = sn.CreateObjectS(ConnectionStr, ObjectText);

                    if (RetVal != "")
                    {
                        MessageBox.Show("Failed - " + RetVal, "Modify Procedure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                
            }

            catch
            {
                return false;
            }
            
            return true;
        }

        public string AddProcComment(string ObjectText, string ObjectStatus, string ObjectComment)
        {
            string ProcComment = "--SVC: " + DateTime.Now.ToString() + " - " + UserName + " - " + ObjectStatus + " - " + ObjectComment + Environment.NewLine;

            ObjectText = ProcComment + ObjectText;

            return ObjectText;
        }

        private string GetServerConnectionString(string ServerNameIn, int UserID, string Environment)
        {
            try
            {
                string ServerName = string.Empty;
                string UserName = string.Empty;
                byte[] Password = null;
                string IntegratedSecurity = string.Empty;
                string ConnectionString = string.Empty;
                DataTable dt;

                using (DataStuff sn = new DataStuff())
                {
                    dt = null;

                    switch (Environment)
                    {
                        case "UAT":
                            dt = sn.GetServerDetailUAT(ServerNameIn, UserID);
                            break;

                        case "PreProd":
                            dt = sn.GetServerDetailPreProd(ServerNameIn, UserID);
                            break;

                        case "Prod":
                            dt = sn.GetServerDetailProd(ServerNameIn, UserID);
                            break;

                        default:
                            break;
                    }

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            ServerName = row["ServerName"].ToString();
                            UserName = row["UserName"].ToString();
                            Password = (byte[])row["Password"];
                            IntegratedSecurity = row["IntegratedSecurity"].ToString();
                            ReleaseToServer = row["ServerAliasDesc"].ToString();
                        }
                    }
                }

                if (IntegratedSecurity == "Y")
                {
                    ConnectionString = "Data Source=" + ServerName + ";Integrated Security=True";
                }
                else
                {
                    string DecryptedPassword = string.Empty;

                    using (DataStuff ds = new DataStuff())
                    {
                        DecryptedPassword = ds.Ontsyfer(Password);
                    }

                    ConnectionString = "Data Source=" + ServerName + ";User ID=" + UserName + ";Password=" + DecryptedPassword;
                }

                if (ConnectionString.Trim() != "")
                {
                    return ConnectionString;
                }
                else
                {
                    return "";
                }
            }

            catch
            {
                return "";
            }
        }
    }
}
