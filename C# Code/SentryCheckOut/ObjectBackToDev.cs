using System;
using System.Data;
using System.Windows.Forms;
using SentryDataStuff;
using SentryGeneral;
using SentryProject;

namespace SentryCheckOut
{
    public partial class ObjectBackToDev : Form
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

        General gs = new General();

        public ObjectBackToDev()
        {
            InitializeComponent();
        }

        private void ObjectBackToDev_Load(object sender, EventArgs e)
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

            if (!IsDevServer())
            {
                MessageBox.Show("This server is not a development server - Changing the status is not allowed.", "Change Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                ConnectionString = GetServerConnectionString(AliasName);

                LoadObjectStatusDetail();
            }

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
                            tCurrentStatus.Text = row["ObjectStatusDesc"].ToString();
                            tLastStatusDate.Text = row["HistoryDate"].ToString();
                            tPreviousResponsiblePerson.Text = row["UserName"].ToString();
                            tPreviousComment.Text = row["Comment"].ToString();
                            tPartOfProject.Text = row["ProjectName"].ToString();
                            ObjectVersion = Convert.ToInt32(row["ObjectVersion"].ToString());
                        }
                    }
                    else
                    {
                        tCurrentStatus.Text = "No previous status";
                        tLastStatusDate.Text = "";
                        tPreviousResponsiblePerson.Text = "";
                        tPreviousComment.Text = "";
                        tPartOfProject.Text = "";
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

                NewObjectText = CurrentObjectText;
                
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
            string ProcComment = "--SVC: " + DateTime.Now.ToString() + " Take object back to development - " + UserName + " - " + NewStatus + " - " + tComment.Text + Environment.NewLine;

            return ProcComment + ObjectTextIn;
        }

        private bool BackupObject()
        {
            bool Result = false;

            try
            {
                string BackupComment;

                BackupComment = UserName + " -  Backup Copy - Before " + NewStatus + " - " + tComment.Text;

                using (DataStuff sn = new DataStuff())
                {
                    Result = sn.BackupObject(AliasName, NewStatus, UserName, ObjectName, DatabaseName, CurrentObjectText, BackupComment, tPartOfProject.Text, "", "", "N", "Y");
                }

                return Result;
            }

            catch
            {
                return false;
            }
        }
        
        private void tsbObjectHistory_Click(object sender, EventArgs e)
        {
            ViewObjectHistory voh = new ViewObjectHistory();

            voh.DatabaseName = DatabaseName;
            voh.ObjectName = ObjectName;

            voh.ShowDialog();
        }

        
    }
}
