using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SentryDataStuff;
using SentryGeneral;

namespace SentryBackupRestore
{
    public partial class BackupRestore : Form
    {
        string DBName = string.Empty;
        string ObjectName = string.Empty;
        string UserName = string.Empty;
        int UserID = 0;
        string ConnectionString = string.Empty;
        string ObjectsSelected = "10000";
        int ServerAliasID = 0;
        string ObjectStatus = string.Empty;
        string LastUser = string.Empty;
        string ProjectName = string.Empty;
        string DateSelected = string.Empty;
        bool SetChanged = false;
        bool Loading = false;
        int RestoreObjectCount = 0;
        bool IsNewObject = false;

        string FilterString1 = string.Empty;
        string FilterString2 = string.Empty;
        string FilterString3 = string.Empty;
        string FilterString4 = string.Empty;
        string FilterString5 = string.Empty;
        string FilterString6 = string.Empty;

        DataGridView dgvMain;
        DataGridView dgvRestore;
        DataTable dtRestoreSet;

        Color AllOkay = Color.Lime;
        Color Danger = Color.Yellow;
        Color Warn = Color.DeepSkyBlue;

        General gs = new General();

        public BackupRestore()
        {
            InitializeComponent();
        }

        private void tsbNewSet_Click(object sender, EventArgs e)
        {
            string SetName = string.Empty;

            AddBackupSet bs = new AddBackupSet();

            bs.ShowDialog();
            SetName = bs.NewBackupSetName;

            if (SetName.Trim() != "")
            {
                LoadBackupSets();
                cbBackupSetName.Text = SetName;
                SetChanged = true;
            }
        }

        private void BackupRestore_Load(object sender, EventArgs e)
        {
            using (DataStuff sn = new DataStuff())
            {
                DataTable dt = sn.SingleUser(Environment.UserName, "ReleaseProject");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        UserName = row["UserName"].ToString();
                        UserID = Convert.ToInt32(row["UserID"].ToString());
                    }
                }
            }

            tsbBackUp.Enabled = false;
            tsbFetchObjects.Enabled = false;
            tsbRestore.Enabled = false;

            GetServers(UserID);
            LoadBackupSets();
            LoadObjectStatus();
            GetUsers();

            cbObjectStatus.Text = " Include All";
            DateSelected = "1 Jan 1900";
            toolStripProgressBar1.Visible = false;
            toolStripProgressBar2.Visible = false;
        }

        private void CreateGrid()
        {
            gbGrid.Controls.Remove(dgvMain);
            dgvMain = new DataGridView();
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
            int y = gbGrid.Height - 30;

            dgvMain.Size = new Size(x, y);
            dgvMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dgvMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dgvMain.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvMain.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvMain.GridColor = Color.Black;
            dgvMain.RowHeadersVisible = false;
            dgvMain.AllowUserToAddRows = false;
            dgvMain.AllowUserToOrderColumns = false;
            dgvMain.Columns.Add("DatabaseName", "Database");

            ColNo++;
            dgvMain.Columns.Add("DBObjectName", "Name");

            ColNo++;
            dgvMain.Columns.Add("ObjectType", "Type");

            ColNo++;
            dgvMain.Columns.Add("LastUser", "User");

            ColNo++;
            dgvMain.Columns.Add("ObjectStatus", "Status");

            ColNo++;
            dgvMain.Columns.Add("ObjectText", "Text");

            ColNo++;
            dgvMain.Columns.Add("ProjectName", "Project Name");

            dgvMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMain.MultiSelect = false;

            x = 7;
            y = 20;

            gbGrid.Controls.Add(dgvMain);

            dgvMain.Location = new Point(x, y);
            dgvMain.Parent = gbGrid;

            dgvMain.CellContentClick += new DataGridViewCellEventHandler(dgvMain_CellContentClick);
        }

        //OH.HistoryDate, 
        //OH.UserName, 
        //OH.ObjectName, 
        //OH.DatabaseName, 
        //OH.ObjectText, 
        //LOS.ObjectStatusDesc, 
        //BS.BackupSetName, 
        //BSO.CreateDate, 
        //BSO.ObjectType

        private void CreateRestoreGrid()
        {
            gbRestoreGrid.Controls.Remove(dgvRestore);
            dgvRestore = new DataGridView();
            dgvRestore.Parent = gbRestoreGrid;

            gbRestoreGrid.Refresh();

            int ColNo = 0;

            dgvRestore.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
            dgvRestore.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvRestore.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvRestore.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvRestore.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 11, FontStyle.Bold);
            dgvRestore.DefaultCellStyle.Font = new Font("Tahoma", 10);

            dgvRestore.Name = "dgvRestore";

            int x = gbRestoreGrid.Width - 20;
            int y = gbRestoreGrid.Height - 30;

            dgvRestore.Size = new Size(x, y);
            dgvRestore.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dgvRestore.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dgvRestore.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvRestore.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvRestore.GridColor = Color.Black;
            dgvRestore.RowHeadersVisible = false;
            dgvRestore.AllowUserToAddRows = false;
            dgvRestore.AllowUserToOrderColumns = false;
            dgvRestore.Columns.Add("DatabaseName", "Database");

            ColNo++;
            dgvRestore.Columns.Add("DBObjectName", "Name");

            ColNo++;
            dgvRestore.Columns.Add("ObjectType", "Type");

            ColNo++;
            dgvRestore.Columns.Add("LastUser", "User");

            ColNo++;
            dgvRestore.Columns.Add("ObjectStatus", "Status");

            ColNo++;
            dgvRestore.Columns.Add("BackupDate", "Backup Date");

            ColNo++;
            dgvRestore.Columns.Add("ObjectText", "Text");

            ColNo++;
            dgvRestore.Columns.Add("ProjectName", "Project Name");

            dgvRestore.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRestore.MultiSelect = false;

            x = 7;
            y = 20;

            gbRestoreGrid.Controls.Add(dgvRestore);

            dgvRestore.Location = new Point(x, y);
            dgvRestore.Parent = gbRestoreGrid;

            dgvRestore.CellContentClick += new DataGridViewCellEventHandler(dgvRestore_CellContentClick);
        }

        //dgvMain.Columns.Add("DatabaseName", "Database");
        //dgvMain.Columns.Add("DBObjectName", "Name");
        //dgvMain.Columns.Add("ObjectType", "Type");
        //dgvMain.Columns.Add("LastUser", "User");
        //dgvMain.Columns.Add("ObjectStatus", "Status");
        //dgvMain.Columns.Add("ObjectText", "Text");

        private void dgvMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }

            if (e.RowIndex != -1)
            {
                ObjectViewer ov = new ObjectViewer();

                ov.ObjectDescription = dgvMain[2, e.RowIndex].Value.ToString();
                ov.ObjectText = dgvMain[5, e.RowIndex].Value.ToString();
                ov.ConnectionString = ConnectionString;
                ov.DatabaseName = dgvMain[0, e.RowIndex].Value.ToString();
                ov.ObjectName = dgvMain[1, e.RowIndex].Value.ToString();
                ov.AliasName = cbServers.Text;

                ov.Show();

                return;
            }
        }

        //dgvRestore.Columns.Add("DatabaseName", "Database");
        //dgvRestore.Columns.Add("DBObjectName", "Name");
        //dgvRestore.Columns.Add("ObjectType", "Type");
        //dgvRestore.Columns.Add("LastUser", "User");
        //dgvRestore.Columns.Add("ObjectStatus", "Status");
        //dgvRestore.Columns.Add("BackupDate", "Backup Date");
        //dgvRestore.Columns.Add("ObjectText", "Text");

        private void dgvRestore_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }

            if (e.RowIndex != -1)
            {
                ObjectViewer ov = new ObjectViewer();

                ov.ObjectDescription = dgvRestore[2, e.RowIndex].Value.ToString();
                ov.ObjectText = dgvRestore[6, e.RowIndex].Value.ToString();
                ov.ConnectionString = ConnectionString;
                ov.DatabaseName = dgvRestore[0, e.RowIndex].Value.ToString();
                ov.ObjectName = dgvRestore[1, e.RowIndex].Value.ToString();
                ov.AliasName = tBackupServerName.Text;

                ov.Show();

                return;
            }
        }

        private void DisableAll()
        {
            tsbBackUp.Enabled = false;
            tsbDeSelectAll.Enabled = false;
            tsbFetchObjects.Enabled = false;
            tsbNewSet.Enabled = false;
            tsbSaveSet.Enabled = false;
            tsbSelectAll.Enabled = false;
            tsbClear.Enabled = false;

            gbDatabases.Enabled = false;
            gbObjectTypes.Enabled = false;
            gbSetDetails.Enabled = false;
            gbCriteria.Enabled = false;

            ((Control)this.tpRestore).Enabled = false;
        }

        private void EnableAll()
        {
            tsbBackUp.Enabled = true;
            tsbDeSelectAll.Enabled = true;
            tsbFetchObjects.Enabled = true;
            tsbNewSet.Enabled = true;
            tsbSaveSet.Enabled = true;
            tsbSelectAll.Enabled = true;
            tsbClear.Enabled = true;

            gbDatabases.Enabled = true;
            gbObjectTypes.Enabled = true;
            gbSetDetails.Enabled = true;
            gbCriteria.Enabled = true;

            ((Control)this.tpRestore).Enabled = true;
        }

        private void LoadObjectStatus()
        {
            try
            {
                cbObjectStatus.Items.Clear();
                cbObjectStatusRestore.Items.Clear();

                cbObjectStatus.Items.Add(" Include All");
                cbObjectStatusRestore.Items.Add(" Include All");

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetObjectStatus(Environment.UserName);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            cbObjectStatus.Items.Add(row["ObjectStatusDesc"].ToString());
                            cbObjectStatusRestore.Items.Add(row["ObjectStatusDesc"].ToString());
                        }
                    }
                }
            }

            catch
            {
                throw;
            }
        }

        private void LoadGrid()
        {
            //dgvMain.Columns.Add("DatabaseName", "Database");
            //dgvMain.Columns.Add("DBObjectName", "Name");
            //dgvMain.Columns.Add("ObjectType", "Type");
            //dgvMain.Columns.Add("LastUser", "User");
            //dgvMain.Columns.Add("ObjectStatus", "Status");
            //dgvMain.Columns.Add("ObjectText", "Text");

            string ConnStr = string.Empty;
            string DateModified = string.Empty;
            string ObjName = string.Empty;
            string DBName = string.Empty;
            string XType = string.Empty;
            string TmpType = string.Empty;
            bool FoundStatus = false;
            string ObjText = string.Empty;
            bool FoundError = false;
            int RowCount = 0;
            int SuccessCount = 0;
            int FailCount = 0;
            int InnerCount = 0;

            dgvMain.Rows.Clear();

            DisableAll();

            toolStripProgressBar1.Visible = true;

            foreach (Control InnerCtl in gbDatabases.Controls)
            {
                if (InnerCtl is CheckBox)
                {
                    if (((CheckBox)InnerCtl).Checked)
                    {
                        DBName = ((CheckBox)InnerCtl).Text;
                        InnerCount = 0;

                        try
                        {
                            using (DataStuff sn = new DataStuff())
                            {
                                DataTable dt = sn.GetDatabaseObjectsForBackup(ConnectionString, DBName, ObjectsSelected, DateSelected);

                                if (dt.Rows.Count > 0)
                                {
                                    toolStripProgressBar1.Maximum = dt.Rows.Count;

                                    foreach (DataRow row in dt.Rows)
                                    {
                                        FoundError = false;

                                        ObjName = row["Name"].ToString();
                                        DateModified = row["modify_date"].ToString();
                                        XType = row["type_desc"].ToString();

                                        switch (XType)
                                        {
                                            case "SYSTEM_TABLE":
                                                TmpType = "System Table";
                                                break;

                                            case "VIEW":
                                                TmpType = "View";
                                                break;

                                            case "SQL_TABLE_VALUED_FUNCTION":
                                                TmpType = "Table Valued Function";
                                                break;

                                            case "DEFAULT_CONSTRAINT":
                                                TmpType = "Default Constraint";
                                                break;

                                            case "SQL_STORED_PROCEDURE":
                                                TmpType = "Stored Procedure";
                                                break;

                                            case "FOREIGN_KEY_CONSTRAINT":
                                                TmpType = "Foreign Key Constraint";
                                                break;

                                            case "SERVICE_QUEUE":
                                                TmpType = "Service Queue";
                                                break;

                                            case "SQL_INLINE_TABLE_VALUED_FUNCTION":
                                                TmpType = "Table Valued Function";
                                                break;

                                            case "USER_TABLE":
                                                TmpType = "User Table";
                                                break;

                                            case "PRIMARY_KEY_CONSTRAINT":
                                                TmpType = "Primary Key Constraint";
                                                break;

                                            case "INTERNAL_TABLE":
                                                TmpType = "Internal Table";
                                                break;

                                            case "TYPE_TABLE":
                                                TmpType = "Type Table";
                                                break;

                                            case "SQL_TRIGGER":
                                                TmpType = "Trigger";
                                                break;

                                            case "SQL_SCALAR_FUNCTION":
                                                TmpType = "Scalar Function";
                                                break;

                                            case "UNIQUE_CONSTRAINT":
                                                TmpType = "Unique Constraint";
                                                break;

                                            default:
                                                break;
                                        }

                                        lStatus.Text = "Getting " + TmpType + " - " + DBName + " - " + ObjName + "...";

                                        FoundStatus = GetObjectStatus(ObjName);

                                        if (cbObjectStatus.Text != " Include All")
                                        {
                                            if (cbObjectStatus.Text == ObjectStatus)
                                            {
                                                if (TmpType.Trim() == "User Table")
                                                {
                                                    ObjText = GetTableHelpText(ConnectionString, DBName, ObjName);
                                                }
                                                else
                                                {
                                                    ObjText = LoadHelpText(ConnectionString, DBName, ObjName);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (TmpType.Trim() == "User Table")
                                            {
                                                ObjText = GetTableHelpText(ConnectionString, DBName, ObjName);
                                            }
                                            else
                                            {
                                                ObjText = LoadHelpText(ConnectionString, DBName, ObjName);
                                            }
                                        }

                                        if (ObjText.Trim() != "")
                                        {
                                            SuccessCount++;
                                        }
                                        else
                                        {
                                            if ((cbObjectStatus.Text == ObjectStatus) || (cbObjectStatus.Text == " Include All"))
                                            {
                                                FoundError = true;
                                                FailCount++;
                                            }
                                        }

                                        if (cbObjectStatus.Text == " Include All")
                                        {
                                            dgvMain.Rows.Add(DBName, ObjName, TmpType, LastUser, ObjectStatus, ObjText, ProjectName);
                                            RowCount++;
                                        }
                                        else
                                        {
                                            if (cbObjectStatus.Text == ObjectStatus)
                                            {
                                                dgvMain.Rows.Add(DBName, ObjName, TmpType, LastUser, ObjectStatus, ObjText, ProjectName);
                                                RowCount++;
                                            }
                                        }

                                        InnerCount++;
                                        toolStripProgressBar1.Value = InnerCount;

                                        if (FoundError)
                                        {
                                            dgvMain.Rows[RowCount - 1].Cells[0].Style.BackColor = Danger;
                                            dgvMain.Rows[RowCount - 1].Cells[1].Style.BackColor = Danger;
                                            dgvMain.Rows[RowCount - 1].Cells[2].Style.BackColor = Danger;
                                            dgvMain.Rows[RowCount - 1].Cells[3].Style.BackColor = Danger;
                                            dgvMain.Rows[RowCount - 1].Cells[4].Style.BackColor = Danger;
                                            dgvMain.Rows[RowCount - 1].Cells[5].Style.BackColor = Danger;
                                        }

                                        Application.DoEvents();
                                    }
                                }
                            }
                        }

                        catch
                        {
                            //EnableAll();
                        }
                    }
                }
            }

            lStatus.Text = "Done - Success count: " + SuccessCount.ToString() + ", Fail count: " + FailCount.ToString();
            toolStripProgressBar1.Visible = false;
            EnableAll();
        }

        private string GetTableHelpText(string ConnString, string DBName, string ObjName)
        {
            string ObjectText = string.Empty;

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetTableHelpText(ConnString, DBName, ObjName);

                    foreach (DataRow row in dt.Rows)
                    {
                        ObjectText = row[0].ToString();
                    }
                }

                ObjectText = ObjectText.Replace("~", Environment.NewLine);

                return ObjectText;
            }

            catch
            {
                return "";
            }
        }

        private string LoadHelpText(string ConnString, string DBName, string ObjName)
        {
            string ObjectText = string.Empty;

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetObjectHelpText(ConnString, DBName, ObjName);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            ObjectText = ObjectText + row[0].ToString();
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

        private bool GetObjectStatus(string DBObjectName)
        {
            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetObjectHistoryLast(DBObjectName, ServerAliasID);

                    if (dt.Rows.Count > 0)
                    {
                        ObjectStatus = dt.Rows[0]["Status"].ToString();
                        LastUser = dt.Rows[0]["User"].ToString();
                        ProjectName = dt.Rows[0]["ProjectName"].ToString();
                        return true;
                    }
                    else
                    {
                        ObjectStatus = "";
                        LastUser = "";
                        ProjectName = "";

                        return true;
                    }
                }
            }

            catch
            {
                return false;
            }
        }

        private bool GetServerDetail()
        {
            try
            {
                string ServerName = string.Empty;
                string UserName = string.Empty;
                byte[] Password = null;
                string IntegratedSecurity = string.Empty;

                ConnectionString = "";

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetServerDetail(cbServers.Text, UserID);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            ServerName = row["ServerName"].ToString();
                            UserName = row["UserName"].ToString();
                            Password = (byte[])row["Password"];
                            IntegratedSecurity = row["IntegratedSecurity"].ToString();
                            ServerAliasID = Convert.ToInt32(row["ServerAliasID"].ToString());
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

        private bool GetRestoreServerDetail()
        {
            try
            {
                string ServerName = string.Empty;
                string UserName = string.Empty;
                byte[] Password = null;
                string IntegratedSecurity = string.Empty;

                ConnectionString = "";

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetServerDetail(tBackupServerName.Text, UserID);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            ServerName = row["ServerName"].ToString();
                            UserName = row["UserName"].ToString();
                            Password = (byte[])row["Password"];
                            IntegratedSecurity = row["IntegratedSecurity"].ToString();
                            ServerAliasID = Convert.ToInt32(row["ServerAliasID"].ToString());
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

        private void LoadDatabaseNames()
        {
            int LastX = 28;
            int LastY = 21;

            if (!GetServerDetail())
            {
                MessageBox.Show("Unable to get the connection string for the selected server.", "Backup Set", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                gbDatabases.Controls.Clear();

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetSystemDatabases(ConnectionString);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            CheckBox c = new CheckBox();
                            c.Parent = gbDatabases;
                            c.Name = "c" + row["Name"].ToString();
                            c.Text = row["Name"].ToString();
                            c.Left = LastX;
                            c.Top = LastY;
                            c.Width = 120;
                            LastY = c.Top + c.Height;

                            c.CheckedChanged += CheckBox_Clicked;

                            if ((LastY + c.Height) > gbDatabases.Height)
                            {
                                LastY = 21;

                                LastX = LastX + 125;
                            }
                        }
                    }
                }
            }

            catch
            {
                throw;
            }
        }

        private void LoadRestoreDatabaseNames()
        {
            int LastX = 28;
            int LastY = 21;

            try
            {
                gbRestoreDatabase.Controls.Clear();

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetSystemDatabases(ConnectionString);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            CheckBox cr = new CheckBox();
                            cr.Parent = gbRestoreDatabase;
                            cr.Name = "cr" + row["Name"].ToString();
                            cr.Text = row["Name"].ToString();
                            cr.Left = LastX;
                            cr.Top = LastY;
                            cr.Width = 120;
                            LastY = cr.Top + cr.Height;

                            cr.CheckedChanged += CheckBoxRestore_Clicked;

                            if ((LastY + cr.Height) > gbRestoreDatabase.Height)
                            {
                                LastY = 21;

                                LastX = LastX + 125;
                            }
                        }
                    }
                }
            }

            catch
            {
                throw;
            }
        }

        private void CheckBox_Clicked(object sender, EventArgs e)
        {
            bool AnyChecked = false;

            foreach (Control InnerCtl in gbDatabases.Controls)
            {
                if (InnerCtl is CheckBox)
                {
                    if (((CheckBox)InnerCtl).Checked)
                    {
                        AnyChecked = true;
                    }
                }
            }

            if (AnyChecked)
            {
                tsbBackUp.Enabled = false;
                tsbFetchObjects.Enabled = true;
            }
            else
            {
                tsbBackUp.Enabled = false;
                tsbFetchObjects.Enabled = false;
            }

            SetChanged = true;
        }

        private void CheckBoxRestore_Clicked(object sender, EventArgs e)
        {
            //bool AnyChecked = false;

            //foreach (Control InnerCtl in gbRestoreDatabase.Controls)
            //{
            //    if (InnerCtl is CheckBox)
            //    {
            //        if (((CheckBox)InnerCtl).Checked)
            //        {
            //            AnyChecked = true;
            //        }
            //    }
            //}

            if (Loading)
            {
                return;
            }

            RestoreDatabasesFilter();
        }

        private bool IsDBSelected()
        {
            bool AnyChecked = false;

            foreach (Control InnerCtl in gbDatabases.Controls)
            {
                if (InnerCtl is CheckBox)
                {
                    if (((CheckBox)InnerCtl).Checked)
                    {
                        AnyChecked = true;
                    }
                }
            }

            if (AnyChecked)
            {
                return true;
            }

            return false;
        }

        private void SaveDatabaseSelected()
        {
            string DoDelete = string.Empty;
            int CheckCount = 0;

            foreach (Control InnerCtl in gbDatabases.Controls)
            {
                if (InnerCtl is CheckBox)
                {
                    if (((CheckBox)InnerCtl).Checked)
                    {
                        CheckCount++;

                        if (CheckCount == 1)
                        {
                            DoDelete = "Y";
                        }
                        else
                        {
                            DoDelete = "N";
                        }

                        try
                        {
                            using (DataStuff sn = new DataStuff())
                            {
                                sn.SaveBackupSetDatabaseSelected(cbBackupSetName.Text, ((CheckBox)InnerCtl).Text, DoDelete);
                            }
                        }

                        catch
                        {
                            throw;
                        }
                    }
                }
            }
        }

        private void SaveObjectTypeSelected()
        {
            string DoDelete = string.Empty;
            int CheckCount = 0;

            foreach (Control InnerCtl in gbObjectTypes.Controls)
            {
                if (InnerCtl is CheckBox)
                {
                    if (((CheckBox)InnerCtl).Checked)
                    {
                        CheckCount++;

                        if (CheckCount == 1)
                        {
                            DoDelete = "Y";
                        }
                        else
                        {
                            DoDelete = "N";
                        }

                        try
                        {
                            using (DataStuff sn = new DataStuff())
                            {
                                sn.SaveBackupSetObjectTypeSelected(cbBackupSetName.Text, ((CheckBox)InnerCtl).Text, DoDelete);
                            }
                        }

                        catch
                        {
                            throw;
                        }
                    }
                }
            }
        }

        private bool IsObjectTypeSelected()
        {
            bool AnyChecked = false;

            foreach (Control InnerCtl in gbObjectTypes.Controls)
            {
                if (InnerCtl is CheckBox)
                {
                    if (((CheckBox)InnerCtl).Checked)
                    {
                        AnyChecked = true;
                    }
                }
            }

            if (AnyChecked)
            {
                return true;
            }

            return false;
        }

        private void GetServers(int UserID)
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

        private void GetUsers()
        {
            try
            {
                cbRestoreUserName.Items.Clear();

                cbRestoreUserName.Items.Add(" Select All Users ");

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetPersons();

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            cbRestoreUserName.Items.Add(row["UserName"].ToString());
                        }
                    }
                }
            }

            catch
            {
                throw;
            }
        }

        //OH.HistoryDate, 
        //OH.UserName, 
        //OH.ObjectName, 
        //OH.DatabaseName, 
        //OH.ObjectText, 
        //LOS.ObjectStatusDesc, 
        //BS.BackupSetName, 
        //BSO.CreateDate, 
        //BSO.ObjectType

        //dgvRestore.Columns.Add("DatabaseName", "Database");
        //dgvRestore.Columns.Add("DBObjectName", "Name");
        //dgvRestore.Columns.Add("ObjectType", "Type");
        //dgvRestore.Columns.Add("LastUser", "User");
        //dgvRestore.Columns.Add("ObjectStatus", "Status");
        //dgvRestore.Columns.Add("BackupDate", "Backup Date");
        //dgvRestore.Columns.Add("ObjectText", "Text");

        private void GetRestoreSetObjects(string Filter)
        {
            string DBName = string.Empty;
            string ObjName = string.Empty;
            string HistoryDate = string.Empty;
            string BackupUser = string.Empty;
            string ObjText = string.Empty;
            string ObjectStatusDesc = string.Empty;
            string CreateDate = string.Empty;
            string ObjectType = string.Empty;
            int RowCount = 0;
            string ProjectName = string.Empty;

            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();

            RestoreDisableAll();

            try
            {
                if (Filter.Trim() == "")
                {
                    dtRestoreSet = null;

                    using (DataStuff sn = new DataStuff())
                    {
                        dtRestoreSet = sn.GetRestoreSetObject(cbRestoreSetName.Text);

                        if (dtRestoreSet.Rows.Count > 0)
                        {
                            toolStripProgressBar2.Visible = true;
                            toolStripProgressBar2.Maximum = dtRestoreSet.Rows.Count;
                            lRestoreSetCount.Text = "Total Object Count: " + dtRestoreSet.Rows.Count.ToString();
                            lRestoreStatus.Text = "Loading restore set objects...";
                            RestoreObjectCount = dtRestoreSet.Rows.Count;

                            foreach (DataRow row in dtRestoreSet.Rows)
                            {
                                DBName = row["DatabaseName"].ToString();
                                ObjName = row["ObjectName"].ToString();
                                HistoryDate = row["HistoryDate"].ToString();
                                BackupUser = row["UserName"].ToString();
                                ObjText = row["ObjectText"].ToString();
                                ObjectStatusDesc = row["ObjectStatusDesc"].ToString();
                                CreateDate = row["CreateDate"].ToString();
                                ObjectType = row["ObjectType"].ToString();
                                ProjectName = row["ProjectName"].ToString();

                                dgvRestore.Rows.Add(DBName, ObjName, ObjectType, BackupUser, ObjectStatusDesc, CreateDate, ObjText, ProjectName);

                                RowCount++;
                                toolStripProgressBar2.Value = RowCount;

                                Application.DoEvents();
                            }

                            toolStripProgressBar2.Visible = false;
                        }
                    }
                }
                else
                {
                    DataView dv = new DataView(dtRestoreSet);
                    string TotalFilter = string.Empty;

                    if (FilterString1.Trim() != "")
                    {
                        TotalFilter = FilterString1;
                    }

                    if (FilterString2.Trim() != "")
                    {
                        if (TotalFilter.Trim() != "")
                        {
                            TotalFilter = TotalFilter + " AND " + FilterString2;
                        }
                        else
                        {
                            TotalFilter = FilterString2;
                        }
                    }

                    if (FilterString3.Trim() != "")
                    {
                        if (TotalFilter.Trim() != "")
                        {
                            TotalFilter = TotalFilter + " AND " + FilterString3;
                        }
                        else
                        {
                            TotalFilter = FilterString3;
                        }
                    }

                    if (FilterString4.Trim() != "")
                    {
                        if (TotalFilter.Trim() != "")
                        {
                            TotalFilter = TotalFilter + " AND " + FilterString4;
                        }
                        else
                        {
                            TotalFilter = FilterString4;
                        }
                    }

                    if (FilterString5.Trim() != "")
                    {
                        if (TotalFilter.Trim() != "")
                        {
                            TotalFilter = TotalFilter + " AND " + FilterString5;
                        }
                        else
                        {
                            TotalFilter = FilterString5;
                        }
                    }

                    if (TotalFilter.Trim() != "")
                    {
                        TotalFilter = "(" + TotalFilter + ")";
                    }

                    dv.RowFilter = TotalFilter;

                    DataTable dtTmp = dv.ToTable();

                    dgvRestore.Rows.Clear();

                    if (dtTmp.Rows.Count > 0)
                    {
                        lRestoreSetCount.Text = "Total Object Count: " + dtRestoreSet.Rows.Count.ToString() + " - Filter Set Count:" + dtTmp.Rows.Count.ToString();
                        lRestoreStatus.Text = "Loading filtered restore set objects...";
                        RestoreObjectCount = dtTmp.Rows.Count;

                        toolStripProgressBar2.Visible = true;
                        toolStripProgressBar2.Maximum = dtTmp.Rows.Count;

                        foreach (DataRow row in dtTmp.Rows)
                        {
                            DBName = row["DatabaseName"].ToString();
                            ObjName = row["ObjectName"].ToString();
                            HistoryDate = row["HistoryDate"].ToString();
                            BackupUser = row["UserName"].ToString();
                            ObjText = row["ObjectText"].ToString();
                            ObjectStatusDesc = row["ObjectStatusDesc"].ToString();
                            CreateDate = row["CreateDate"].ToString();
                            ObjectType = row["ObjectType"].ToString();

                            dgvRestore.Rows.Add(DBName, ObjName, ObjectType, BackupUser, ObjectStatusDesc, CreateDate, ObjText);

                            RowCount++;
                            toolStripProgressBar2.Value = RowCount;

                            Application.DoEvents();
                        }

                        toolStripProgressBar2.Visible = false;
                    }
                }

                lRestoreStatus.Text = "";
            }

            catch
            {
                lRestoreStatus.Text = "";
                RestoreEnableAll();
                throw;
            }

            RestoreEnableAll();
            Cursor.Current = Cursors.Default;
        }

        private void GetServersInServerGroup()
        {
            try
            {
                cbRestoreToServer.Items.Clear();

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetServersInServerGroup(tBackupServerName.Text, UserID);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            cbRestoreToServer.Items.Add(row["ServerAliasDesc"].ToString());
                        }
                    }
                }
            }

            catch
            {
                throw;
            }
        }

        private void LoadBackupSets()
        {
            try
            {
                cbBackupSetName.Items.Clear();
                cbRestoreSetName.Items.Clear();

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetBackupSets();

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            cbBackupSetName.Items.Add(row["BackupSetName"].ToString());
                            cbRestoreSetName.Items.Add(row["BackupSetName"].ToString());
                        }
                    }
                }
            }

            catch
            {
                throw;
            }
        }

        private void tsbSaveSet_Click(object sender, EventArgs e)
        {
            if (!SaveSet("N"))
            {
                return;
            }
        }

        private bool SaveSet(string Silent)
        {
            string IsActive = string.Empty;

            if (!IsDBSelected())
            {
                MessageBox.Show("Please select at least one database for this backup set.", "Save Backup Set", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (!IsObjectTypeSelected())
            {
                MessageBox.Show("Please select at least one object type for this backup set.", "Save Backup Set", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (rbActiveYes.Checked)
            {
                IsActive = "Y";
            }
            else
            {
                IsActive = "N";
            }

            if (cbBackupSetName.Text.Trim() == "")
            {
                MessageBox.Show("Please select a backup set name.", "Save Backup Set", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (cbServers.Text.Trim() == "")
            {
                MessageBox.Show("Please select a server alias for this backup set.", "Save Backup Set", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            SaveDatabaseSelected();

            SaveObjectTypeSelected();

            using (DataStuff sn = new DataStuff())
            {

                sn.SaveBackupSet(cbBackupSetName.Text, gs.RemoveSpecialCharacters(tComment.Text), UserID, cbServers.Text, IsActive);

                if (Silent == "N")
                {
                    MessageBox.Show("Backup set saved.", "Save Backup Set", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                SetChanged = false;
            }

            return true;
        }

        private void cbBackupSetName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DialogResult Result;

            if (SetChanged)
            {
                Result = MessageBox.Show("The backup set has changed. Do you want to save the changes?", "Save Backup Set", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (Result == System.Windows.Forms.DialogResult.Yes)
                {
                    if (!SaveSet("N"))
                    {
                        return;
                    }
                }

                if (Result == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
            }

            LoadBackupSetDetail();
            SetChanged = false;
        }

        private void LoadBackupSetDetail()
        {
            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetBackupSetDetail(cbBackupSetName.Text);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            cbServers.Text = row["ServerAliasDesc"].ToString();
                            tComment.Text = row["BackupSetDesc"].ToString();
                            tComment.Text = gs.RemoveSpecialCharacters(tComment.Text);
                            tCreateDate.Text = row["CreateDate"].ToString();

                            if (row["Active"].ToString() == "Y")
                            {
                                rbActiveYes.Checked = true;
                            }
                            else
                            {
                                rbActiveNo.Checked = true;
                            }

                            GetServersSelected();

                            GetObjectTypeSelected();
                        }
                    }
                }
            }

            catch
            {
            }
        }

        private void LoadRestoreSetDetail()
        {
            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetBackupSetDetail(cbRestoreSetName.Text);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            tBackupServerName.Text = row["ServerAliasDesc"].ToString();
                            tRestoreComment.Text = row["BackupSetDesc"].ToString();
                            tRestoreDateCreated.Text = row["CreateDate"].ToString();
                        }
                    }
                }
            }

            catch
            {
            }
        }

        private void GetServersSelected()
        {
            string DBSelected = string.Empty;

            //Reset all

            foreach (Control InnerCtl in gbDatabases.Controls)
            {
                if (InnerCtl is CheckBox)
                {
                    if (((CheckBox)InnerCtl).Checked)
                    {
                        ((CheckBox)InnerCtl).Checked = false;
                    }
                }
            }

            using (DataStuff sn = new DataStuff())
            {
                DataTable dt = sn.GetBackupSetDatabaseSelected(cbBackupSetName.Text);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        DBSelected = row["DatabaseName"].ToString();

                        foreach (Control InnerCtl in gbDatabases.Controls)
                        {
                            if (InnerCtl is CheckBox)
                            {
                                if (((CheckBox)InnerCtl).Text == DBSelected)
                                {
                                    ((CheckBox)InnerCtl).Checked = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void GetRestoreServersSelected()
        {
            string DBSelected = string.Empty;

            //Reset all

            foreach (Control InnerCtl in gbRestoreDatabase.Controls)
            {
                if (InnerCtl is CheckBox)
                {
                    ((CheckBox)InnerCtl).Checked = false;
                    ((CheckBox)InnerCtl).Enabled = false;
                }
            }

            using (DataStuff sn = new DataStuff())
            {
                DataTable dt = sn.GetBackupSetDatabaseSelected(cbRestoreSetName.Text);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        DBSelected = row["DatabaseName"].ToString();

                        foreach (Control InnerCtl in gbRestoreDatabase.Controls)
                        {
                            if (InnerCtl is CheckBox)
                            {
                                if (((CheckBox)InnerCtl).Text == DBSelected)
                                {
                                    ((CheckBox)InnerCtl).Checked = true;
                                    ((CheckBox)InnerCtl).Enabled = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void GetObjectTypeSelected()
        {
            string ObjSelected = string.Empty;

            //Reset all

            foreach (Control InnerCtl in gbObjectTypes.Controls)
            {
                if (InnerCtl is CheckBox)
                {
                    if (((CheckBox)InnerCtl).Checked)
                    {
                        ((CheckBox)InnerCtl).Checked = false;
                    }
                }
            }

            using (DataStuff sn = new DataStuff())
            {
                DataTable dt = sn.GetBackupSetObjectTypeSelected(cbBackupSetName.Text);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        ObjSelected = row["ObjectType"].ToString();

                        foreach (Control InnerCtl in gbObjectTypes.Controls)
                        {
                            if (InnerCtl is CheckBox)
                            {
                                if (((CheckBox)InnerCtl).Text == ObjSelected)
                                {
                                    ((CheckBox)InnerCtl).Checked = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void GetRestoreObjectTypeSelected()
        {
            string ObjSelected = string.Empty;

            //Reset all

            foreach (Control InnerCtl in gbRestoreObjectTypes.Controls)
            {
                if (InnerCtl is CheckBox)
                {
                    ((CheckBox)InnerCtl).Checked = false;
                    ((CheckBox)InnerCtl).Enabled = false;
                }
            }

            using (DataStuff sn = new DataStuff())
            {
                DataTable dt = sn.GetBackupSetObjectTypeSelected(cbRestoreSetName.Text);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        ObjSelected = row["ObjectType"].ToString();

                        foreach (Control InnerCtl in gbRestoreObjectTypes.Controls)
                        {
                            if (InnerCtl is CheckBox)
                            {
                                if (((CheckBox)InnerCtl).Text == ObjSelected)
                                {
                                    ((CheckBox)InnerCtl).Checked = true;
                                    ((CheckBox)InnerCtl).Enabled = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void cbServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDatabaseNames();
            SetChanged = true;
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            DateSelected = dtpDate.Value.ToShortDateString().ToString();
        }

        private void rbSpecificDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSpecificDate.Checked)
            {
                dtpDate.Enabled = true;
            }
            else
            {
                dtpDate.Enabled = false;
            }
        }

        private void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAll.Checked)
            {
                dtpDate.Enabled = false;
                DateSelected = "1 Jan 1900";
            }
        }

        private void rbOneMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOneMonth.Checked)
            {
                dtpDate.Enabled = false;
                DateSelected = DateTime.Now.AddDays(-30).ToShortDateString().ToString();
            }
        }

        private void rbThreeMonths_CheckedChanged(object sender, EventArgs e)
        {
            if (rbThreeMonths.Checked)
            {
                dtpDate.Enabled = false;
                DateSelected = DateTime.Now.AddDays(-90).ToShortDateString().ToString();
            }
        }

        private void rbSixMonths_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSixMonths.Checked)
            {
                dtpDate.Enabled = false;
                DateSelected = DateTime.Now.AddDays(-180).ToShortDateString().ToString();
            }
        }

        private void tsbSelectAll_Click(object sender, EventArgs e)
        {
            foreach (Control InnerCtl in gbDatabases.Controls)
            {
                if (InnerCtl is CheckBox)
                {
                    if (!((CheckBox)InnerCtl).Checked)
                    {
                        ((CheckBox)InnerCtl).Checked = true;
                        SetChanged = true;
                    }
                }
            }
        }

        private void tsbDeSelectAll_Click(object sender, EventArgs e)
        {
            foreach (Control InnerCtl in gbDatabases.Controls)
            {
                if (InnerCtl is CheckBox)
                {
                    if (((CheckBox)InnerCtl).Checked)
                    {
                        ((CheckBox)InnerCtl).Checked = false;
                        SetChanged = true;
                    }
                }
            }
        }

        private void tsbFetchObjects_Click(object sender, EventArgs e)
        {
            CreateGrid();
            LoadGrid();
        }

        private void cObjStoredProcedures_CheckedChanged(object sender, EventArgs e)
        {
            if (cObjStoredProcedures.Checked)
            {
                ObjectsSelected = "1" + ObjectsSelected.Substring(1, 4);
            }
            else
            {
                ObjectsSelected = "0" + ObjectsSelected.Substring(1, 4);
            }

            SetChanged = true;
        }

        private void cObjFunctions_CheckedChanged(object sender, EventArgs e)
        {
            if (cObjFunctions.Checked)
            {
                ObjectsSelected = ObjectsSelected.Substring(0, 1) + "1" + ObjectsSelected.Substring(2, 3);
            }
            else
            {
                ObjectsSelected = ObjectsSelected.Substring(0, 1) + "0" + ObjectsSelected.Substring(2, 3);
            }

            SetChanged = true;
        }

        private void cObjTables_CheckedChanged(object sender, EventArgs e)
        {
            if (cObjTables.Checked)
            {
                ObjectsSelected = ObjectsSelected.Substring(0, 2) + "1" + ObjectsSelected.Substring(3, 2);
            }
            else
            {
                ObjectsSelected = ObjectsSelected.Substring(0, 2) + "0" + ObjectsSelected.Substring(3, 2);
            }

            SetChanged = true;
        }

        private void cObjViews_CheckedChanged(object sender, EventArgs e)
        {
            if (cObjViews.Checked)
            {
                ObjectsSelected = ObjectsSelected.Substring(0, 4) + "1";
            }
            else
            {
                ObjectsSelected = ObjectsSelected.Substring(0, 4) + "0";
            }

            SetChanged = true;
        }

        private void cObjTriggers_CheckedChanged(object sender, EventArgs e)
        {
            if (cObjTriggers.Checked)
            {
                ObjectsSelected = ObjectsSelected.Substring(0, 3) + "1" + ObjectsSelected.Substring(4, 1);
            }
            else
            {
                ObjectsSelected = ObjectsSelected.Substring(0, 3) + "0" + ObjectsSelected.Substring(4, 1);
            }

            SetChanged = true;
        }

        private bool BackupObject(string AliasName, string Status, string ObjectName, string DatabaseName, string ObjectText, string LastUser, string BackupSetName, string ObjectType, string DoDelete, string ProjectName, string VersionAtCheckOut)
        {
            bool Result = false;

            try
            {
                string BackupComment;

                BackupComment = LastUser + " -  Backup Copy - " + Status + " - " + gs.RemoveSpecialCharacters(tComment.Text);

                using (DataStuff sn = new DataStuff())
                {
                    Result = sn.BackupObjectBackupSet(AliasName, Status, LastUser, ObjectName, DatabaseName, ObjectText, BackupComment, ProjectName, BackupSetName, ObjectType, DoDelete, VersionAtCheckOut);
                }

                return Result;
            }

            catch
            {
                return false;
            }
        }

        private void tsbBackUp_Click(object sender, EventArgs e)
        {
            if (!SaveSet("Y"))
            {
                return;
            }

            DoBackup();
        }

        //dgvMain.Columns.Add("DatabaseName", "Database");
        //dgvMain.Columns.Add("DBObjectName", "Name");
        //dgvMain.Columns.Add("ObjectType", "Type");
        //dgvMain.Columns.Add("LastUser", "User");
        //dgvMain.Columns.Add("ObjectStatus", "Status");
        //dgvMain.Columns.Add("ObjectText", "Text");

        private void DoBackup()
        {
            bool Result = false;
            string DatabaseName = string.Empty;
            string ObjectName = string.Empty;
            string ObjectText = string.Empty;
            string ObjectStatus = string.Empty;
            int SuccessCount = 0;
            int FailCount = 0;
            string ObjectType = string.Empty;
            string LastUser = string.Empty;
            int RowCount = 0;
            bool FoundError = false;
            string DoDelete = string.Empty;

            DisableAll();

            toolStripProgressBar1.Maximum = dgvMain.Rows.Count;
            toolStripProgressBar1.Visible = true;

            DoDelete = "Y";

            foreach (DataGridViewRow row in dgvMain.Rows)
            {
                FoundError = false;

                try
                {
                    DatabaseName = Convert.ToString(row.Cells["DatabaseName"].Value);
                    ObjectName = Convert.ToString(row.Cells["DBObjectName"].Value);
                    ObjectText = Convert.ToString(row.Cells["ObjectText"].Value);
                    ObjectStatus = Convert.ToString(row.Cells["ObjectStatus"].Value);
                    ObjectType = Convert.ToString(row.Cells["ObjectType"].Value);
                    LastUser = Convert.ToString(row.Cells["LastUser"].Value);
                    ProjectName = Convert.ToString(row.Cells["ProjectName"].Value);

                    if (ObjectStatus.Trim() == "")
                    {
                        ObjectStatus = "Backup";
                    }

                    if (LastUser.Trim() == "")
                    {
                        LastUser = UserName;
                    }

                    if (ObjectText.Trim() != "")
                    {
                        lStatus.Text = "Saving " + ObjectType + " - " + DatabaseName + " - " + ObjectName + "...";

                        Result = BackupObject(cbServers.Text, ObjectStatus, ObjectName, DatabaseName, ObjectText, LastUser, cbBackupSetName.Text, ObjectType, DoDelete, ProjectName, "N");

                        DoDelete = "N";
                    }
                    else
                    {
                        Result = false;
                    }

                    RowCount++;

                    if (Result)
                    {
                        SuccessCount++;
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
                        dgvMain.Rows[RowCount - 1].Cells[4].Style.BackColor = Danger;
                        dgvMain.Rows[RowCount - 1].Cells[5].Style.BackColor = Danger;
                    }

                    toolStripProgressBar1.Value = RowCount;
                }

                catch
                {

                }

                Application.DoEvents();
            }

            lStatus.Text = "Done - Success count: " + SuccessCount.ToString() + ", Fail count: " + FailCount.ToString();
            MessageBox.Show("Backup complete - successful object count: " + SuccessCount.ToString() + ", failed object count: " + FailCount.ToString(), "Backup Objects", MessageBoxButtons.OK, MessageBoxIcon.Information);
            toolStripProgressBar1.Visible = false;
            EnableAll();
        }

        private void rbActiveYes_CheckedChanged(object sender, EventArgs e)
        {
            SetChanged = true;
        }

        private void rbActiveNo_CheckedChanged(object sender, EventArgs e)
        {
            SetChanged = true;
        }

        private void tComment_TextChanged(object sender, EventArgs e)
        {
            SetChanged = true;
        }

        private void BackupRestore_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult Result;

            if (SetChanged)
            {
                Result = MessageBox.Show("The backup set has changed. Do you want to save the changes?", "Save Backup Set", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (Result == System.Windows.Forms.DialogResult.Yes)
                {
                    if (!SaveSet("N"))
                    {
                        e.Cancel = true;
                        return;
                    }
                }

                if (Result == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void tsbClear_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Clear the items in the grid?", "Backup Set Items", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                dgvMain.Rows.Clear();
            }
        }

        private void cbRestoreSetName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Loading = true;

            RestoreDisableAll();

            lRestoreStatus.Text = "Loading restore set details...";

            LoadRestoreSetDetail();
            Application.DoEvents();

            lRestoreStatus.Text = "Loading restore set servers...";

            GetRestoreServerDetail();
            Application.DoEvents();

            lRestoreStatus.Text = "Loading restore set object types...";

            GetRestoreObjectTypeSelected();
            Application.DoEvents();

            lRestoreStatus.Text = "Loading restore set databases...";

            LoadRestoreDatabaseNames();
            Application.DoEvents();

            lRestoreStatus.Text = "Setting selected servers...";

            GetRestoreServersSelected();
            Application.DoEvents();

            GetServersInServerGroup();
            Application.DoEvents();

            CreateRestoreGrid();
            Application.DoEvents();

            lRestoreStatus.Text = "Loading restore set objects...";

            GetRestoreSetObjects("");
            Application.DoEvents();

            RestoreEnableAll();

            Loading = false;
        }

        private void cRestoreObjStoredProcedures_CheckedChanged(object sender, EventArgs e)
        {
            if (Loading)
            {
                return;
            }

            RestoreObjectsFilter();
        }

        private void RestoreObjectsFilter()
        {
            FilterString1 = string.Empty;
            string TmpType = string.Empty;

            foreach (Control InnerCtl in gbRestoreObjectTypes.Controls)
            {
                if (InnerCtl is CheckBox)
                {
                    if (((CheckBox)InnerCtl).Checked)
                    {
                        switch (((CheckBox)InnerCtl).Text)
                        {
                            case "Tables":
                                TmpType = "'System Table', 'User Table', 'Internal Table', 'Type Table'";
                                break;

                            case "Views":
                                TmpType = "'View'";
                                break;

                            case "Functions":
                                TmpType = "'Table Valued Function', 'Scalar Function'";
                                break;

                            case "Stored Procedures":
                                TmpType = "'Stored Procedure'";
                                break;

                            case "Triggers":
                                TmpType = "'Trigger'";
                                break;

                            default:
                                break;
                        }

                        if (FilterString1.Trim() == "")
                        {
                            FilterString1 = "ObjectType in (" + TmpType;
                        }
                        else
                        {
                            FilterString1 = FilterString1 + ", " + TmpType;
                        }
                    }
                }
            }

            if (FilterString1.Trim() != "")
            {
                FilterString1 = "(" + FilterString1 + "))";
                GetRestoreSetObjects(FilterString1);
            }
        }

        private void RestoreDatabasesFilter()
        {
            FilterString2 = string.Empty;

            foreach (Control InnerCtl in gbRestoreDatabase.Controls)
            {
                if (InnerCtl is CheckBox)
                {
                    if (((CheckBox)InnerCtl).Checked)
                    {
                        if (FilterString2.Trim() == "")
                        {
                            FilterString2 = "DatabaseName in ('" + ((CheckBox)InnerCtl).Text + "'";
                        }
                        else
                        {
                            FilterString2 = FilterString2 + ", '" + ((CheckBox)InnerCtl).Text + "'";
                        }
                    }
                }
            }

            if (FilterString2.Trim() != "")
            {
                FilterString2 = "(" + FilterString2 + "))";
                GetRestoreSetObjects(FilterString2);
            }
        }

        private void cRestoreObjFunctions_CheckedChanged(object sender, EventArgs e)
        {
            if (Loading)
            {
                return;
            }

            RestoreObjectsFilter();
        }

        private void cRestoreObjTables_CheckedChanged(object sender, EventArgs e)
        {
            if (Loading)
            {
                return;
            }

            RestoreObjectsFilter();
        }

        private void cRestoreObjViews_CheckedChanged(object sender, EventArgs e)
        {
            if (Loading)
            {
                return;
            }

            RestoreObjectsFilter();
        }

        private void cRestoreObjTriggers_CheckedChanged(object sender, EventArgs e)
        {
            if (Loading)
            {
                return;
            }

            RestoreObjectsFilter();
        }

        private void cbObjectStatusRestore_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterString3 = "";

            if (cbObjectStatusRestore.Text != " Include All")
            {
                FilterString3 = "(ObjectStatusDesc = '" + cbObjectStatusRestore.Text + "')";
            }

            GetRestoreSetObjects(FilterString3);
        }

        private void cmdRestoreNameSearch_Click(object sender, EventArgs e)
        {
            if (tRestoreObjectName.Text.Trim() != "")
            {
                FilterString4 = "";

                FilterString4 = "(ObjectName like '%" + tRestoreObjectName.Text + "%')";

                GetRestoreSetObjects(FilterString4);
            }
        }

        private void cbRestoreUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterString5 = "";

            if (cbRestoreUserName.Text != " Select All Users ")
            {
                FilterString5 = "(UserName = '" + cbRestoreUserName.Text + "')";
            }

            GetRestoreSetObjects(FilterString5);
        }

        private void cbRestoreToServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            tsbRestore.Enabled = true;
        }

        private void tsbRestore_Click(object sender, EventArgs e)
        {
            //dgvMain.Columns.Add("DatabaseName", "Database");
            //dgvMain.Columns.Add("DBObjectName", "Name");
            //dgvMain.Columns.Add("ObjectType", "Type");
            //dgvMain.Columns.Add("LastUser", "User");
            //dgvMain.Columns.Add("ObjectStatus", "Status");
            //dgvMain.Columns.Add("ObjectText", "Text");

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

            DialogResult Result = MessageBox.Show("You are about to restore " + RestoreObjectCount.ToString() + " objects from " + tBackupServerName.Text + " to " + cbRestoreToServer.Text + ". Continue?", "Restore Objects", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Result == DialogResult.Yes)
            {
                RestoreDisableAll();

                toolStripProgressBar2.Maximum = dgvRestore.Rows.Count;
                toolStripProgressBar2.Visible = true;

                foreach (DataGridViewRow row in dgvRestore.Rows)
                {
                    FoundError = false;
                    RowCount++;

                    try
                    {
                        DBName = Convert.ToString(row.Cells["DatabaseName"].Value);
                        ObjectName = Convert.ToString(row.Cells["DBObjectName"].Value);
                        ObjText = Convert.ToString(row.Cells["ObjectText"].Value);
                        ObjectStatus = Convert.ToString(row.Cells["ObjectStatus"].Value);
                        ProjectName = Convert.ToString(row.Cells["ProjectName"].Value);

                        if (ObjText.Trim() != "")
                        {
                            lStatus.Text = "Saving " + ObjectType + " - " + DBName + " - " + ObjectName + "...";

                            //Normal backup before deployment

                            BackupResult = BackupReleaseToServerObject(cbRestoreToServer.Text, DBName, ObjectName, ProjectName);
                        }
                        
                        RowCount++;

                        if (BackupResult)
                        {
                            //Do the deploy to the selected server

                            ObjText = AddProcComment(ObjText, ObjectStatus, "Restore from backup set " + cbRestoreSetName.Text);

                            ObjText = gs.ChangeCreateToAlter(ObjText);

                            DeployResult = RestoreObject(cbRestoreToServer.Text, DBName, ObjectName, ObjText);

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
                            dgvRestore.Rows[RowCount - 1].Cells[0].Style.BackColor = Danger;
                            dgvRestore.Rows[RowCount - 1].Cells[1].Style.BackColor = Danger;
                            dgvRestore.Rows[RowCount - 1].Cells[2].Style.BackColor = Danger;
                            dgvRestore.Rows[RowCount - 1].Cells[3].Style.BackColor = Danger;
                            dgvRestore.Rows[RowCount - 1].Cells[4].Style.BackColor = Danger;
                            dgvRestore.Rows[RowCount - 1].Cells[5].Style.BackColor = Danger;
                        }

                        toolStripProgressBar2.Value = RowCount;
                    }

                    catch
                    {

                    }

                    Application.DoEvents();
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

                lStatus.Text = "Restored " + SuccessCount.ToString() + " objects, Failed count: " + FailCount.ToString();
                toolStripProgressBar2.Visible = false;

                RestoreEnableAll();

                return;
            }
        }

        private void RestoreDisableAll()
        {
            gbRestoreDatabase.Enabled = false;
            gbRestoreFilter.Enabled = false;
            gbRestoreGrid.Enabled = false;
            gbRestoreObjectTypes.Enabled = false;
            gbRestoreSet.Enabled = false;
            gbRestoreVarious.Enabled = false;

            tsbRestore.Enabled = false;
        }

        private void RestoreEnableAll()
        {
            gbRestoreDatabase.Enabled = true;
            gbRestoreFilter.Enabled = true;
            gbRestoreGrid.Enabled = true;
            gbRestoreObjectTypes.Enabled = true;
            gbRestoreSet.Enabled = true;
            gbRestoreVarious.Enabled = true;

            if (cbRestoreToServer.Text.Trim() != "")
            {
                tsbRestore.Enabled = true;
            }
        }

        private bool RestoreObject(string ReleaseToServer, string DatabaseName, string ObjectName, string ObjectText)
        {
            string RetVal = string.Empty;

            using (DataStuff sn = new DataStuff())
            {
                string ConnectionStr = string.Empty;

                ConnectionStr = GetServerConnectionString(ReleaseToServer, UserID);

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

        private bool BackupReleaseToServerObject(string ReleaseToServer, string DatabaseName, string ObjectName, string ProjectName)
        {
            string ObjectText = string.Empty;
            string ObjectDescription = string.Empty;
            string ConnectionStr = string.Empty;
            bool Result = false;

            ConnectionStr = GetServerConnectionString(ReleaseToServer, UserID);

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

                BackupComment = UserName + " - " + "Backup before restore from backup set " + cbRestoreSetName.Text;

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

        public string AddProcComment(string ObjectText, string ObjectStatus, string ObjectComment)
        {
            string ProcComment = "--SVC: " + DateTime.Now.ToString() + " - " + UserName + " - " + ObjectStatus + " - " + ObjectComment + Environment.NewLine;

            ObjectText = ProcComment + ObjectText;

            return ObjectText;
        }

        private string GetServerConnectionString(string ServerNameIn, int UserID)
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
                    DataTable dt = sn.GetServerDetail(ServerNameIn, UserID);

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

        private void cbObjectStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
