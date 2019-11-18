using System;
using System.Data;
using System.Windows.Forms;
using SentryDataStuff;
using SentryGeneral;
using SentryProject;

namespace SentryCheckOut
{
    public partial class ObjectCheckOut : Form
    {
        public string ObjectDescription = string.Empty;
        public string ObjectText = string.Empty;
        public string ConnectionString = string.Empty;
        public string DatabaseName = string.Empty;
        public string ObjectName = string.Empty;
        public string AliasName = string.Empty;
        public string OriginalStatus = string.Empty;
        public string NewStatus = string.Empty;

        string CurrentObjectText = string.Empty;
        string NewObjectText = string.Empty;
        int ObjectVersion = 0;

        string UserName = string.Empty;
        int UserID = 0;
        bool StrictCreateBaseFromProd = false;
        string ProductionServerAlias = string.Empty;

        General gs = new General();
        
        public ObjectCheckOut()
        {
            InitializeComponent();
        }

        private void ObjectCheckOut_Load(object sender, EventArgs e)
        {
            using (DataStuff sn = new DataStuff())
            {
                DataTable dt = sn.SingleUser(Environment.UserName, "DBObject");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        UserName = row["UserName"].ToString();
                        UserID = Convert.ToInt32(row["UserID"].ToString());
                    }
                }
            }

            GetStrictSettings();

            if (!IsDevServer())
            {
                MessageBox.Show("This server is not a development server - Changing the status is not allowed.", "Change Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                ConnectionString = GetServerConnectionString(AliasName);

                LoadObjectStatusDetail();
                LoadProjects();
                LoadCheckOutFromServers();
            }

            if (CurrentObjectText.Trim() == "")
            {
                CurrentObjectText = GetObjectText(ConnectionString, DatabaseName, ObjectName);

                CurrentObjectText = gs.ChangeCreateToAlter(CurrentObjectText);
            }
        }

        private void GetStrictSettings()
        {
            string SettingValue = string.Empty;

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    SettingValue = sn.GetSystemSetting("Key6");

                    if (SettingValue.Trim() == "true")
                    {
                        StrictCreateBaseFromProd = true;
                    }
                    else
                    {
                        StrictCreateBaseFromProd = false;
                    }
                }
            }

            catch
            {

            }
        }

        private bool IsExistingObjectInProd()
        {
            string CopyFromServerConnString = string.Empty;
            string ReleaseFromObjectText = string.Empty;

            using (DataStuff sn = new DataStuff())
            {
                DataTable dt = sn.GetCheckOutFromServersProduction(AliasName);

                if (dt.Rows.Count > 0)
                {
                    ProductionServerAlias = dt.Rows[0]["ServerAliasDesc"].ToString();
                }
            }

            if (!string.IsNullOrEmpty(ProductionServerAlias))
            {
                //Found the prod server, get the object text

                try
                {
                    CopyFromServerConnString = GetServerConnectionString(ProductionServerAlias);

                    if (CopyFromServerConnString.Trim() != "")
                    {
                        ReleaseFromObjectText = GetObjectText(CopyFromServerConnString, DatabaseName, ObjectName);
                    }
                    
                    if (!string.IsNullOrEmpty(ReleaseFromObjectText))
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

            return false;
        }

        private void LoadCheckOutFromServers()
        {
            try
            {
                cbCheckOutFrom.Items.Clear();
                DataTable dt = null;

                using (DataStuff sn = new DataStuff())
                {
                    if (!StrictCreateBaseFromProd)
                    {
                        //Load all servers

                        dt = sn.GetCheckOutFromServers(AliasName);
                    }
                    else
                    {
                        //See if the object exists in prod

                        if (IsExistingObjectInProd())
                        {
                            //Set the combo text to production and disable

                            cbCheckOutFrom.Text = ProductionServerAlias;
                            cbCheckOutFrom.Enabled = false;
                            return;
                        }
                        else
                        {
                            //Load all servers

                            dt = sn.GetCheckOutFromServers(AliasName);
                        }
                    }

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            cbCheckOutFrom.Items.Add(row["ServerAliasDesc"].ToString());
                        }
                    }

                    cbCheckOutFrom.Items.Add("Use Current Version");
                }
            }

            catch
            {
                throw;
            }
        }

        //private void LoadObjectStatus()
        //{
        //    try
        //    {
        //        cbObjectStatus.Items.Clear();

        //        using (DataStuff sn = new DataStuff())
        //        {
        //            DataTable dt = sn.GetObjectStatus(Environment.UserName);

        //            if (dt.Rows.Count > 0)
        //            {
        //                foreach (DataRow row in dt.Rows)
        //                {
        //                    cbObjectStatus.Items.Add(row["ObjectStatusDesc"].ToString());
        //                }
        //            }
        //        }
        //    }

        //    catch
        //    {
        //        throw;
        //    }
        //}

        private bool IsDevServer()
        {
            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    return sn.IsDevServer(AliasName, UserID);
                }
            }

            catch
            {
                return false;
            }
        }

        private void LoadObjectStatusDetail()
        {
            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetObjectStatusDetail(AliasName, ObjectName);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            tCurrentStatus.Text = row["ObjectStatusDesc"].ToString();
                            tLastStatusDate.Text = row["HistoryDate"].ToString();
                            tPreviousResponsiblePerson.Text = row["UserName"].ToString();
                            tPreviousComment.Text = row["Comment"].ToString();
                            cbPartOfProject.Text = row["ProjectName"].ToString();
                            ObjectVersion = Convert.ToInt32(row["ObjectVersion"].ToString());
                        }
                    }
                    else
                    {
                        tCurrentStatus.Text = "No previous status";
                        tLastStatusDate.Text = "";
                        tPreviousResponsiblePerson.Text = "";
                        tPreviousComment.Text = "";
                        cbPartOfProject.Text = "";
                        ObjectVersion = 0;
                    }
                }

                tResponsiblePerson.Text = UserName;
                OriginalStatus = tCurrentStatus.Text;
            }

            catch
            {
                throw;
            }
        }

        private void LoadProjects()
        {
            try
            {
                cbPartOfProject.Items.Clear();

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetProjects();

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            cbPartOfProject.Items.Add(row["ProjectName"].ToString());
                        }
                    }
                }
            }

            catch
            {
                throw;
            }
        }

        private string LoadHelpText()
        {
            string ObjectText = string.Empty;

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetObjectHelpText(ConnectionString, DatabaseName, ObjectName);

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

        private string GetServerConnectionString(string ServerAlias)
        {
            try
            {
                string ServerName = string.Empty;
                string UserName = string.Empty;
                byte[] Password = null;
                string IntegratedSecurity = string.Empty;
                string CnnString = string.Empty;

                using (DataStuff sn = new DataStuff())
                {
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
                    CnnString = "Data Source=" + ServerName + ";Integrated Security=True";
                }
                else
                {
                    string DecryptedPassword = string.Empty;

                    using (DataStuff ds = new DataStuff())
                    {
                        DecryptedPassword = ds.Ontsyfer(Password);
                    }

                    CnnString = "Data Source=" + ServerName + ";User ID=" + UserName + ";Password=" + DecryptedPassword;
                }

                return CnnString;
            }

            catch
            {
                return "";
            }
        }

        private string GetObjectText(string CnnString, string DBName, string ObjName)
        {
            string ObjectText = string.Empty;
            string ObjectDescription = string.Empty;

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetDatabaseObjectText(CnnString, DBName, ObjName);

                    if (dt.Rows.Count > 1)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            ObjectText = row["FullDefinition"].ToString();
                            ObjectDescription = row["ROUTINE_NAME"].ToString() + ": " + row["ROUTINE_TYPE"].ToString();
                        }
                    }
                    else
                    {
                        ObjectText = LoadHelpText(CnnString, DBName, ObjName);

                        if (ObjectText.Trim() == "")
                        {
                            dt = sn.GetTableHelpText(CnnString, DBName, ObjName);

                            foreach (DataRow row in dt.Rows)
                            {
                                ObjectText = row[0].ToString();
                            }

                            ObjectText = ObjectText.Replace("~", Environment.NewLine);
                        }
                    }

                    return ObjectText;
                }
            }

            catch
            {
                return "";
            }
        }

        private string LoadHelpText(string ConnectionString, string DatabaseName, string ObjectName)
        {
            string ObjectText = string.Empty;

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetObjectHelpText(ConnectionString, DatabaseName, ObjectName);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            ObjectText = ObjectText + row[0].ToString();
                        }
                    }
                }
            }

            catch
            {
                return "";
            }

            return ObjectText;
        }

        private void tsbCheckOut_Click(object sender, EventArgs e)
        {
            string RetVal = string.Empty;

            NewStatus = "Check out for edit";

            if (cbCheckOutFrom.Text == "")
            {
                MessageBox.Show("Please select the server to check out from.", "Change Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NewStatus = OriginalStatus;
                cbCheckOutFrom.Focus();
                return;
            }

            if (tComment.Text == "")
            {
                MessageBox.Show("Please enter a comment for this change.", "Change Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NewStatus = OriginalStatus;
                tComment.Focus();
                return;
            }

            try
            {
                if (!BackupObject())
                {
                    MessageBox.Show("There was an error creating a backup for this object. Please ensure that your servers have been set up correctly.", "Change Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    NewStatus = OriginalStatus;
                    return;
                }

                if (cbCheckOutFrom.Text == "Use Current Version")
                {
                    NewObjectText = CurrentObjectText;
                }
                else
                {
                    GetCheckoutFromVersion();
                }

                NewObjectText = AddProcComment(NewObjectText);

                using (DataStuff sn = new DataStuff())
                {
                    ConnectionString = GetServerConnectionString(AliasName);

                    ConnectionString = ConnectionString + ";Database=" + DatabaseName;

                    RetVal = sn.CreateObjectS(ConnectionString, NewObjectText);

                    if (RetVal != "")
                    {
                        MessageBox.Show("Failed - " + RetVal, "Modify Procedure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        NewStatus = OriginalStatus;
                    }
                    else
                    {
                        MessageBox.Show("Object successfully updated.", "Modify Procedure", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }

            catch
            {

            }
        }

        public string AddProcComment(string ObjectTextIn)
        {
            string ProcComment = "--SVC: " + DateTime.Now.ToString() + " New Base From " + cbCheckOutFrom.Text + " - " + UserName + " - " + NewStatus + " - " + gs.RemoveSpecialCharacters(tComment.Text) + Environment.NewLine;

            return ProcComment + ObjectTextIn;
        }

        private bool BackupObject()
        {
            bool Result = false;

            try
            {
                string BackupComment;

                BackupComment = UserName + " -  Backup Copy - Before " + NewStatus + " - " + gs.RemoveSpecialCharacters(tComment.Text);

                using (DataStuff sn = new DataStuff())
                {
                    Result = sn.BackupObject(AliasName, NewStatus, UserName, ObjectName, DatabaseName, CurrentObjectText, BackupComment, cbPartOfProject.Text, "", "", "N", "Y");
                }

                return Result;
            }

            catch
            {
                return false;
            }
        }

        private void cbObjectStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            tComment.Text = "";
        }

        private void cmdNewProject_Click(object sender, EventArgs e)
        {
            AddProject ap = new AddProject();
            ap.ShowDialog();

            if (ap.NewProjectName.Trim() != "")
            {
                LoadProjects();
                cbPartOfProject.Text = ap.NewProjectName;
            }
        }

        private void cbCheckOutFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCheckoutFromVersion();
        }

        private void GetCheckoutFromVersion()
        {
            string CopyFromServerConnString = string.Empty;
            string ReleaseFromObjectText = string.Empty;

            try
            {
                if (cbCheckOutFrom.Text == "")
                {
                    MessageBox.Show("Please select the server to check out from.", "Change Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (cbCheckOutFrom.Text == "Use Current Version")
                {
                    return;
                }

                CopyFromServerConnString = GetServerConnectionString(cbCheckOutFrom.Text);

                if (CopyFromServerConnString.Trim() == "")
                {
                    MessageBox.Show("There was an error getting the check out from server connection string. Please ensure that your servers have been set up correctly.", "Change Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ReleaseFromObjectText = GetObjectText(CopyFromServerConnString, DatabaseName, ObjectName);

                if (ReleaseFromObjectText.Trim() == "")
                {
                    MessageBox.Show("There was an error getting the check out from server's object text. Please ensure that you have sufficient permissions to view this object.", "Change Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                NewObjectText = ReleaseFromObjectText;

                NewObjectText = gs.ChangeCreateToAlter(NewObjectText);
            }

            catch
            {

            }
        }

        private void tsbObjectHistory_Click(object sender, EventArgs e)
        {
            ViewObjectHistory voh = new ViewObjectHistory();

            voh.DatabaseName = DatabaseName;
            voh.ObjectName = ObjectName;

            voh.ShowDialog();
        }

        private void tComment_TextChanged(object sender, EventArgs e)
        {
            tComment.Text = gs.RemoveSpecialCharacters(tComment.Text);
        }
    }
}
