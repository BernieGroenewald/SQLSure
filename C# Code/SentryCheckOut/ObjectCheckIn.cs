using System;
using System.Data;
using System.Windows.Forms;
using SentryDataStuff;
using SentryGeneral;

namespace SentryCheckOut
{
    public partial class ObjectCheckIn : Form
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

        public ObjectCheckIn()
        {
            InitializeComponent();
        }

        private void ObjectCheckIn_Load(object sender, System.EventArgs e)
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
        
        private bool BackupObjectNewStatus()
        {
            bool Result = false;
            string ProcComment = string.Empty;

            ProcComment = "--SVC: " + DateTime.Now.ToString() + " Backup copy of current checked out version - user checking this version in - " + UserName;
            CurrentObjectText = AddProcComment(CurrentObjectText, ProcComment);

            try
            {
                string BackupComment;

                BackupComment = UserName + " -  Backup copy of current checked out version before check in";

                using (DataStuff sn = new DataStuff())
                {
                    Result = sn.BackupObject(AliasName, "Check In", UserName, ObjectName, DatabaseName, CurrentObjectText, BackupComment, tPartOfProject.Text, "", "", "N", "N");
                }

                return Result;
            }

            catch
            {
                return false;
            }
        }

        private void tsbCheckIn_Click(object sender, System.EventArgs e)
        {
            string RetVal = string.Empty;

            //Just change the current object status by creating a backup of the current version with the new status...

            try
            {
                NewStatus = OriginalStatus;

                if (BackupObjectNewStatus())
                {
                    using (DataStuff sn = new DataStuff())
                    {
                        ConnectionString = GetServerConnectionString(AliasName);

                        ConnectionString = ConnectionString + ";Database=" + DatabaseName;

                        RetVal = sn.CreateObjectS(ConnectionString, CurrentObjectText);

                        if (RetVal != "")
                        {
                            MessageBox.Show("Failed - " + RetVal, "Modify Procedure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            NewStatus = OriginalStatus;
                        }
                        else
                        {
                            tsbCheckIn.Enabled = false;
                            MessageBox.Show("The object was successfully updated.", "Check In", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            NewStatus = "Check In";
                            this.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Unable to change the status of the current version.", "Check In", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }

            catch
            {

            }
        }
    }
}
