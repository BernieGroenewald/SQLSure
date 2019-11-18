using System;
using System.Data;
using System.Windows.Forms;
using SentryDataStuff;
using System.Drawing;
using SentryGeneral;
using System.Diagnostics;
using System.IO;

namespace SentryProject
{
    public partial class ProjectRestore : Form
    {
        int UserID = 0;
        string UserName = string.Empty;
        int DBObjNameCol = 0;
        int DBNameCol = 0;
        int ReleaseFromServerCol = 0;
        int ReleaseToServerCol = 0;
        int CheckBoxCol = 0;
        string ReleaseFromServer = string.Empty;
        string ReleaseToServer = string.Empty;
        Color AllOkay = Color.Lime;
        Color Danger = Color.Yellow;
        Color Warn = Color.DeepSkyBlue;
        bool IsNewObject = false;

        DataGridView dgvMain;

        General gs = new General();
        public ProjectRestore()
        {
            InitializeComponent();
        }

        private void ProjectRestore_Load(object sender, EventArgs e)
        {
            using (DataStuff sn = new DataStuff())
            {
                DataTable dt = sn.SingleUser(Environment.UserName, "ProjectBackup");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        UserName = row["UserName"].ToString();
                        UserID = Convert.ToInt32(row["UserID"].ToString());
                    }
                }
            }

            LoadProjects();
            GetServers();

            cmdRestore.Enabled = false;
        }

        private void GetServers()
        {
            try
            {
                cbServers.Items.Clear();

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetServers(UserID);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            cbServers.Items.Add(row["ServerAliasDesc"].ToString());
                        }
                    }
                }
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
                cmdRestore.Enabled = false;

                string IncludeInactive = string.Empty;
                
                IncludeInactive = cActiveOnly.Checked ? "N" : "Y";

                cbProject.Items.Clear();

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetProjectBackupSets(IncludeInactive);

                    if (dt.Rows.Count > 0)
                    {
                        cbProject.DataSource = dt;
                        cbProject.DisplayMember = "ProjectName";
                        cbProject.ValueMember = "ProjectSetID";
                    }
                }
            }

            catch
            {
                throw;
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
                string ConnectionString = string.Empty;

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

                return ConnectionString;
            }

            catch
            {
                return "";
            }
        }

        private string LoadObjectDetail(string ConnectionString, string DBName, string DBOwner, string ObjectName)
        {
            string DateModified = string.Empty;

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetObjectDetail(ConnectionString, DBName, DBOwner, ObjectName);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            DateModified = row["modify_date"].ToString();
                        }
                    }
                    else
                    {
                        DateModified = "";
                    }
                }

                return DateModified;
            }

            catch
            {
                return "";
            }
        }

        private void LoadGrid(int SetID)
        {
            string ConnStr = string.Empty;
            string DateModFrom = string.Empty;
            string DateModTo = string.Empty;
            string ObjName = string.Empty;
            string DBName = string.Empty;
            bool FoundRow = false;
            string ObjectText = string.Empty;

            if (cbProject.Text == "")
            {
                return;
            }

            dgvMain.Rows.Clear();

            using (DataStuff sn = new DataStuff())
            {
                string ProjectName = string.Empty;
                
                ProjectName = cbProject.Text;
                
                DataTable dt = sn.GetProjectBackupSetDetail(SetID);

                if (dt.Rows.Count > 0)
                {   
                    foreach (DataRow row in dt.Rows)
                    {
                        if (ReleaseToServer.Trim() != "")
                        {
                            ConnStr = GetServerConnectionString(ReleaseToServer);
                            ObjName = row["ObjectName"].ToString();
                            DBName = row["DatabaseName"].ToString();
                            ObjectText = row["ObjectText"].ToString();

                            DateModTo = LoadObjectDetail(ConnStr, DBName, "", ObjName);
                            
                            DateModFrom = row["HistoryDate"].ToString();

                            dgvMain.Rows.Add(DBName,
                                             ObjName,
                                             DateModFrom,
                                             DateModTo,
                                             true,
                                             ObjectText);

                            FoundRow = true;
                        }
                    }
                    
                    SetDateModifiedColour(2);
                    
                }
            }

            cmdRestore.Enabled = FoundRow;
        }

        private void SetDateModifiedColour(int Cols)
        {
            DateTime BaseDate = DateTime.Now;
            DateTime RelTo = DateTime.Now;
            bool DateError = false;

            if (Cols == 2)
            {
                foreach (DataGridViewRow row in dgvMain.Rows)
                {
                    DateError = false;

                    try
                    {
                        BaseDate = Convert.ToDateTime(row.Cells["BackupDate"].Value);
                        RelTo = Convert.ToDateTime(row.Cells["ReleaseToDate"].Value);
                    }

                    catch
                    {
                        DateError = true;
                    }

                    if (!DateError)
                    {
                        if (Math.Abs((BaseDate - RelTo).Minutes) < 5)
                        {
                            row.Cells["BackupDate"].Style.BackColor = AllOkay;
                            row.Cells["ReleaseToDate"].Style.BackColor = AllOkay;
                        }
                        else
                        {
                            if (BaseDate < RelTo)
                            {
                                row.Cells["BackupDate"].Style.BackColor = Danger;
                                row.Cells["ReleaseToDate"].Style.BackColor = Danger;
                            }

                            if (BaseDate > RelTo)
                            {
                                row.Cells["BackupDate"].Style.BackColor = Warn;
                                row.Cells["ReleaseToDate"].Style.BackColor = Warn;
                            }
                        }
                    }
                }
            }
        }

        private void cbProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SetID = 0;

            int.TryParse(cbProject.SelectedValue.ToString(), out SetID);

            CreateGrid();
            LoadGrid(SetID);
        }

        private void CreateGrid()
        {
            gbGrid.Controls.Remove(dgvMain);
            dgvMain = new DataGridView();
            dgvMain.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom);
            dgvMain.Parent = gbGrid;

            gbGrid.Refresh();

            int ColNo = 0;

            dgvMain.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
            dgvMain.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMain.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvMain.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvMain.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 11, FontStyle.Bold);
            dgvMain.DefaultCellStyle.Font = new Font("Tahoma", 10);

            dgvMain.Name = "dgvMain";

            int x = gbGrid.Width - 20;
            int y = gbGrid.Height - 110;

            dgvMain.Size = new Size(x, y);
            dgvMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dgvMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dgvMain.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvMain.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvMain.GridColor = Color.Black;
            dgvMain.RowHeadersVisible = false;
            dgvMain.AllowUserToAddRows = false;
            dgvMain.AllowUserToOrderColumns = false;
            dgvMain.Columns.Add("DatabaseName", "Database Name");
            DBNameCol = ColNo;

            ColNo++;
            dgvMain.Columns.Add("DBObjectName", "Object Name");
            DBObjNameCol = ColNo;

            ColNo++;
            dgvMain.Columns.Add("BackupDate", "Backup Date");
            ReleaseFromServerCol = ColNo;

            ColNo++;
            dgvMain.Columns.Add("ReleaseToDate", ReleaseToServer);
            ReleaseToServerCol = ColNo;
            
            ColNo++;
            DataGridViewCheckBoxColumn dgCheckCell = new DataGridViewCheckBoxColumn();
            dgCheckCell.Name = "ObjectSelected";
            dgvMain.Columns.Add(dgCheckCell);
            dgvMain.Columns[ColNo].HeaderText = "Selected";
            CheckBoxCol = ColNo;

            ColNo++;
            dgvMain.Columns.Add("ObjectText", "ObjectText");
            dgvMain.Columns[ColNo].Visible = false;

            dgvMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMain.MultiSelect = false;

            x = 7;
            y = 86;

            gbGrid.Controls.Add(dgvMain);

            dgvMain.Location = new Point(x, y);
            dgvMain.Parent = gbGrid;

            dgvMain.CellContentClick += new DataGridViewCellEventHandler(dgvMain_CellContentClick);
            dgvMain.CellValueChanged += new DataGridViewCellEventHandler(dgvMain_CellValueChanged);
            dgvMain.CurrentCellDirtyStateChanged += new EventHandler(dgvMain_CurrentCellDirtyStateChanged);
            dgvMain.CellMouseEnter += new DataGridViewCellEventHandler(dgvMain_CellMouseEnter);

            dgvMain.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom);
        }

        private void dgvMain_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == CheckBoxCol && e.RowIndex != -1)
            {
                IsItemSelected();
            }
        }

        private void IsItemSelected()
        {
            bool FoundItem = false;

            foreach (DataGridViewRow row in dgvMain.Rows)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)row.Cells["ObjectSelected"];
                checkCell.TrueValue = true;

                if (Convert.ToBoolean(checkCell.Value) == Convert.ToBoolean(checkCell.TrueValue))
                {
                    FoundItem = true;
                }
            }

            if (FoundItem)
            {
                cmdRestore.Enabled = true;
            }
            else
            {
                cmdRestore.Enabled = false;
            }
        }
        
        private void dgvMain_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvMain.IsCurrentCellDirty)
            {
                dgvMain.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string ReturnText = string.Empty;
            string DBName = string.Empty;
            string ObjName = string.Empty;
            string ObjectText = string.Empty;

            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }
            
            if (e.ColumnIndex == ReleaseFromServerCol && e.RowIndex != -1)
            {
                try
                {
                    DBName = (string)dgvMain[DBNameCol, e.RowIndex].Value;
                    ObjName = (string)dgvMain[DBObjNameCol, e.RowIndex].Value;
                    ObjectText = (string)dgvMain["ObjectText", e.RowIndex].Value;

                    if ((DBName.Trim() != "") && (ObjName.Trim() != ""))
                    {
                        CompareFiles(DBName, ObjName, ObjectText);
                    }

                    return;
                }

                catch
                {
                    return;
                }
            }
        }

        private void CompareFiles(string DBName, string ObjName, string ObjectText)
        {
            string File1 = string.Empty;
            string File2 = string.Empty;
            string File1Content = string.Empty;
            string File2Content = string.Empty;
            bool FoundContent = false;
            string WinMergePath = string.Empty;
            string ServerToConnString = string.Empty;
            
            string path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)).FullName;

            File1 = path.Trim() + @"\" + "Compare1" + DateTime.Now.ToLongTimeString().Replace(":", "").Replace(" ", "") + ".txt";
            File2 = path.Trim() + @"\" + "Compare2" + DateTime.Now.ToLongTimeString().Replace(":", "").Replace(" ", "") + ".txt";

            File1Content = ObjectText;

            ServerToConnString = GetServerConnectionString(ReleaseToServer);
            File2Content = GetObjectText(ServerToConnString, DBName, ObjName);
            
            if ((File1Content.Trim() != "") && (File2Content.Trim() != ""))
            {
                try
                {
                    System.IO.File.WriteAllText(File1, File1Content);
                    System.IO.File.WriteAllText(File2, File2Content);
                    FoundContent = true;
                }

                catch
                {
                    FoundContent = false;
                }
            }

            if (FoundContent)
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();

                    WinMergePath = @"c:\Program Files (x86)\WinMerge\WinMergeU.exe";

                    if (!File.Exists(WinMergePath))
                    {
                        WinMergePath = @"c:\Program Files\WinMerge\WinMergeU.exe";
                    }

                    startInfo.FileName = WinMergePath;

                    startInfo.Arguments = "/e /dl " + "\"Backed Up\" /dr " + "\"" + ReleaseToServer + "\" " + @File1 + " " + @File2;
                    Process.Start(startInfo);
                }

                catch
                {
                    MessageBox.Show("Oops, something went wrong. Please check your WinMerge installation.", "WinMerge", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private void dgvMain_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            //Skip the Column and Row headers

            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }

            var dataGridView = (sender as DataGridView);
            
            try
            {
                if ((e.ColumnIndex == ReleaseFromServerCol))
                {
                    if (dataGridView[e.ColumnIndex, e.RowIndex].Value.ToString() != "")
                    {
                        dataGridView.Cursor = Cursors.Hand;
                        return;
                    }
                    else
                    {
                        dataGridView.Cursor = Cursors.Default;
                    }
                }
                else
                {
                    dataGridView.Cursor = Cursors.Default;
                }
            }

            catch
            {
                return;
            }

            return;
        }

        private void cbServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReleaseToServer = cbServers.Text;

            int SetID = 0;

            int.TryParse(cbProject.SelectedValue.ToString(), out SetID);

            CreateGrid();
            LoadGrid(SetID);
        }

        private void cmdRestore_Click(object sender, EventArgs e)
        {
            string ConnStr = string.Empty;
            string DateModified = string.Empty;
            string ObjName = string.Empty;
            string DBName = string.Empty;
            string XType = string.Empty;
            string TmpType = string.Empty;
            string ObjText = string.Empty;
            string ObjectType = string.Empty;
            string ProjectName = string.Empty;
            bool FoundError = false;
            int RowCount = 0;
            int SuccessCount = 0;
            int FailCount = 0;
            bool BackupResult = false;
            bool DeployResult = false;
            string ObjectName = string.Empty;
            string ObjectStatus = string.Empty;

            DialogResult result = MessageBox.Show("Restore these objects to " + cbServers.Text + "?", "Project Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgvMain.Rows)
                {
                    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)row.Cells["ObjectSelected"];
                    checkCell.TrueValue = true;

                    if (Convert.ToBoolean(checkCell.Value) == Convert.ToBoolean(checkCell.TrueValue))
                    {
                        FoundError = false;
                        RowCount++;

                        try
                        {
                            DBName = Convert.ToString(row.Cells["DatabaseName"].Value);
                            ObjectName = Convert.ToString(row.Cells["DBObjectName"].Value);
                            ObjText = Convert.ToString(row.Cells["ObjectText"].Value);
                            ObjectStatus = "Project restore";

                            if (ObjText.Trim() != "")
                            {
                                lStatus.Text = "Saving " + DBName + " - " + ObjectName + "...";

                                //Normal backup before deployment

                                BackupResult = BackupReleaseToServerObject(cbServers.Text, DBName, ObjectName, ProjectName);
                            }

                            RowCount++;

                            if (BackupResult)
                            {
                                //Do the deploy to the selected server

                                ObjText = AddProcComment(ObjText, ObjectStatus, "Restore from project backup");

                                ObjText = gs.ChangeCreateToAlter(ObjText);

                                DeployResult = RestoreObject(cbServers.Text, DBName, ObjectName, ObjText);

                                if (DeployResult)
                                {
                                    SuccessCount++;
                                }
                                else
                                {
                                    FoundError = true;
                                    FailCount++;
                                }
                            }
                            else
                            {
                                FoundError = true;
                                FailCount++;
                            }

                            if (FoundError)
                            {
                                dgvMain.Rows[RowCount - 1].Cells[0].Style.BackColor = Danger;
                                dgvMain.Rows[RowCount - 1].Cells[1].Style.BackColor = Danger;
                                dgvMain.Rows[RowCount - 1].Cells[2].Style.BackColor = Danger;
                                dgvMain.Rows[RowCount - 1].Cells[3].Style.BackColor = Danger;
                            }
                        }

                        catch
                        {

                        }

                        Application.DoEvents();
                    }
                }

                if ((FailCount == 0) && (SuccessCount > 0))
                {
                    MessageBox.Show("" + SuccessCount.ToString() + " object(s) were successfully restored.", "Restore Object", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if ((FailCount > 0) && (SuccessCount > 0))
                {
                    MessageBox.Show("" + SuccessCount.ToString() + " object(s) were successfully restored, " + FailCount.ToString() + " objects failed.", "Restore Object", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if ((FailCount > 0) && (SuccessCount == 0))
                {
                    MessageBox.Show("No object were restored, " + FailCount.ToString() + " objects failed.", "Restore Object", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if ((FailCount == 0) && (SuccessCount == 0))
                {
                    MessageBox.Show("No object were restored, no objects failed.", "Restore Object", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private bool RestoreObject(string ReleaseToServer, string DatabaseName, string ObjectName, string ObjectText)
        {
            string RetVal = string.Empty;

            using (DataStuff sn = new DataStuff())
            {
                string ConnectionStr = string.Empty;

                ConnectionStr = GetServerConnectionString(ReleaseToServer);

                ConnectionStr = ConnectionStr + ";Database=" + DatabaseName;

                if (IsNewObject)
                {
                    ObjectText = gs.ChangeAlterToCreate(ObjectText);
                }

                lStatus.Text = "Deploying object " + DatabaseName + " - " + ObjectName + " to " + ReleaseToServer + "...";

                RetVal = sn.CreateObjectS(ConnectionStr, ObjectText);

                if (RetVal != "")
                {
                    MessageBox.Show("Failed - " + RetVal, "Modify Procedure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lStatus.Text = "Error deploying object " + DatabaseName + " - " + ObjectName + " to " + ReleaseToServer + "...";

                    return false;
                }
            }

            return true;
        }

        public string AddProcComment(string ObjectText, string ObjectStatus, string ObjectComment)
        {
            string ProcComment = "--SVC: " + DateTime.Now.ToString() + " - " + UserName + " - " + ObjectStatus + " - " + ObjectComment + Environment.NewLine;

            ObjectText = ProcComment + ObjectText;

            return ObjectText;
        }


        private bool BackupReleaseToServerObject(string ReleaseToServer, string DatabaseName, string ObjectName, string ProjectName)
        {
            string ObjectText = string.Empty;
            string ObjectDescription = string.Empty;
            string ConnectionStr = string.Empty;
            bool Result = false;

            ConnectionStr = GetServerConnectionString(ReleaseToServer);

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetDatabaseObjectText(ConnectionStr, DatabaseName, ObjectName);

                    if (dt.Rows.Count > 1)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            ObjectText = row["FullDefinition"].ToString();
                            ObjectDescription = row["ROUTINE_NAME"].ToString() + ": " + row["ROUTINE_TYPE"].ToString();
                        }
                    }
                }
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
                    IsNewObject = true;
                    return true;
                }
            }

            if (ObjectText.Trim() == "")
            {
                try
                {
                    using (DataStuff sn = new DataStuff())
                    {
                        DataTable dt = sn.GetObjectHelpText(ConnectionStr, DatabaseName, ObjectName);

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
                    DialogResult result = MessageBox.Show("An error occurred getting the object text for server " + ReleaseToServer + ". Release anyway?", "Backup Object", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                    if (result == DialogResult.No)
                    {
                        return false;
                    }
                    else
                    {
                        IsNewObject = true;
                        return true;
                    }
                }
            }

            try
            {
                if (ObjectText.Trim() == "")
                {
                    DialogResult result = MessageBox.Show("An error occurred getting the object text for server " + ReleaseToServer + ". Release anyway?", "Backup Object", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                    if (result == DialogResult.No)
                    {
                        return false;
                    }
                    else
                    {
                        IsNewObject = true;
                        return true;
                    }
                }

                string BackupComment;

                BackupComment = UserName + " - " + "Backup before restore from project backup";

                using (DataStuff sn = new DataStuff())
                {
                    Result = sn.BackupObject(ReleaseToServer, "Backup", UserName, ObjectName, DatabaseName, ObjectText, BackupComment, ProjectName, "", "", "N", "N");

                    if (!Result)
                    {
                        MessageBox.Show("There was an error creating a backup for this object. Please ensure that your servers have been set up correctly.", "Change Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    lStatus.Text = "Creating backup of " + ReleaseToServer + " - " + DatabaseName + ": " + ObjectName + "...";
                }
            }

            catch
            {
                MessageBox.Show("An error occurred while creating the backup record.", "Backup Object", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lStatus.Text = "Error creating backup of " + ReleaseToServer + " - " + DatabaseName + ": " + ObjectName + ".";
                return false;
            }

            return true;
        }
    }
}
