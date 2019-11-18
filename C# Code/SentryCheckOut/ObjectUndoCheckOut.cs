using System;
using System.Data;
using System.Windows.Forms;
using SentryDataStuff;
using SentryGeneral;
using SentryProject;

namespace SentryCheckOut
{
    public partial class ObjectUndoCheckOut : Form
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

        string UserName = string.Empty;
        int UserID = 0;

        General gs = new General();
        public ObjectUndoCheckOut()
        {
            InitializeComponent();
        }

        private void ObjectUndoCheckOut_Load(object sender, EventArgs e)
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
            
            ConnectionString = GetServerConnectionString(AliasName);

            LoadObjectStatusDetail();

            GetPreviousVersion();

            if (CurrentObjectText.Trim() == "")
            {
                CurrentObjectText = GetObjectText(ConnectionString, DatabaseName, ObjectName);

                CurrentObjectText = gs.ChangeCreateToAlter(CurrentObjectText);
            }
        }

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
                            tObjectStatus.Text = row["ObjectStatusDesc"].ToString();
                            tLastStatusDate.Text = row["HistoryDate"].ToString();
                            tResponsiblePerson.Text = row["UserName"].ToString();
                            tComment.Text = row["Comment"].ToString();
                            tPartOfProject.Text = row["ProjectName"].ToString();
                        }
                    }
                    else
                    {
                        tResponsiblePerson.Text = UserName;
                    }
                }

                OriginalStatus = tObjectStatus.Text;
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

        public string AddProcComment(string ObjectTextIn, string Comment)
        {
            return Comment + Environment.NewLine + ObjectTextIn;
        }

        private bool BackupObject()
        {
            bool Result = false;
            string ProcComment = string.Empty;
            //string ProcComment = "--SVC: " + DateTime.Now.ToString() + " New Base From " + cbCheckOutFrom.Text + " - " + UserName + " - " + NewStatus + " - " + tComment.Text + Environment.NewLine;

            ProcComment = "--SVC: " + DateTime.Now.ToString() + " Backup copy of current checked out version before undoing check out " + " - " + UserName;
            CurrentObjectText = AddProcComment(CurrentObjectText, ProcComment);

            try
            {
                string BackupComment;

                BackupComment = UserName + " -  Backup copy of current checked out version before undoing check out";

                using (DataStuff sn = new DataStuff())
                {
                    Result = sn.BackupObject(AliasName, "Check-out Undo", UserName, ObjectName, DatabaseName, CurrentObjectText, BackupComment, tPartOfProject.Text, "", "", "N", "N");
                }

                return Result;
            }

            catch
            {
                return false;
            }
        }

        private bool BackupObjectNewStatus()
        {
            bool Result = false;
            string ProcComment = string.Empty;
            
            ProcComment = "--SVC: " + DateTime.Now.ToString() + " Backup copy of current checked out version - user undoing check out without restoring previous version - " + UserName;
            CurrentObjectText = AddProcComment(CurrentObjectText, ProcComment);

            try
            {
                string BackupComment;

                BackupComment = UserName + " -  Backup copy of current checked out version before undoing check out";

                using (DataStuff sn = new DataStuff())
                {
                    Result = sn.BackupObject(AliasName, "Check-out Undo", UserName, ObjectName, DatabaseName, CurrentObjectText, BackupComment, tPartOfProject.Text, "", "", "N", "N");
                }

                return Result;
            }

            catch
            {
                return false;
            }
        }

        private bool GetPreviousVersion()
        {
            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    tPreviousVersion.Text = sn.GetVersionAtCheckOut(AliasName, ObjectName, DatabaseName);
                }

                if (tPreviousVersion.Text.Trim() == "")
                {
                    cRestorePrevious.Checked = false;
                    cRestorePrevious.Enabled = false;
                    return false;
                }
                else
                {
                    cRestorePrevious.Checked = true;
                    cRestorePrevious.Enabled = true;
                    return true;
                }
            }

            catch
            {
                cRestorePrevious.Checked = false;
                cRestorePrevious.Enabled = false;
                return false;
            }
        }

        private void tsbUndoCheckOut_Click(object sender, EventArgs e)
        {
            //Restore the previous version if the user opted for this, else just update the object status

            NewStatus = OriginalStatus;

            if (cRestorePrevious.Checked)
            {
                //Make a backup of the current version

                if (BackupObject())
                {
                    if (RestorePrevious())
                    {
                        tsbUndoCheckOut.Enabled = false;
                        MessageBox.Show("The previous version of the object has been successfully restored.", "Undo Check Out", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        NewStatus = "Check-out Undo";
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Unable to create a backup of the current version.", "Undo Check Out", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                //Just change the current object status by creating a backup of the current version with the new status...

                if (BackupObjectNewStatus())
                {
                    tsbUndoCheckOut.Enabled = false;
                    MessageBox.Show("The object was successfully updated.", "Undo Check Out", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    NewStatus = "Check-out Undo";
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Unable to change the status of the current version.", "Undo Check Out", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private bool RestorePrevious()
        {
            string RetVal = string.Empty;
            string ProcComment = "--SVC: " + DateTime.Now.ToString() + " Restored version after undoing check out " + " - " + UserName;

            NewStatus = "Check-out Undo";

            NewObjectText = AddProcComment(tPreviousVersion.Text, ProcComment);

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    ConnectionString = GetServerConnectionString(AliasName);

                    ConnectionString = ConnectionString + ";Database=" + DatabaseName;

                    RetVal = sn.CreateObjectS(ConnectionString, NewObjectText);

                    if (RetVal != "")
                    {
                        MessageBox.Show("Failed - " + RetVal, "Modify Procedure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        NewStatus = OriginalStatus;
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            catch
            {
                return false;
            }
        }

        private void cmdViewPrevious_Click(object sender, EventArgs e)
        {
            this.Width = 985;
            this.Height = 720;

            this.CenterToScreen();
        }
    }
}
