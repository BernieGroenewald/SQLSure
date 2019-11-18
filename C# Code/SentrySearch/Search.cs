using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using SentryDataStuff;
using SentryControls;
using SentryGeneral;

namespace SentrySearch
{
    public partial class Search : Form
    {
        string UserName = string.Empty;
        int UserID = 0;
        string DatabasesSelected = string.Empty;
        string ObjectsSelected = string.Empty;
        bool BusySearching = false;
        string ServerAlias = string.Empty;
        string XMLObjects = string.Empty;
        string XMLPath = string.Empty;
        string XMLDatabases = string.Empty;
        bool Loading = false;
        bool CancelNow = false;

        Color MatchColour = Color.LightGreen;

        DataTable dtObjects = new DataTable();
        DataTable dtColumns = new DataTable();
        DataTable dtDetail = new DataTable();

        General gs = new General();

        DBCheckBox dbCheckBox1 = new DBCheckBox();

        public string ConnectionString = string.Empty;
        public string StoreConnectionString = string.Empty;

        public Search()
        {
            InitializeComponent();
        }

        private void Search_Load(object sender, EventArgs e)
        {
            Loading = true;

            using (DataStuff sn = new DataStuff())
            {
                DataTable dt = sn.SingleUser(Environment.UserName, "Search");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        UserName = row["UserName"].ToString();
                        UserID = Convert.ToInt32(row["UserID"].ToString());
                    }
                }
            }

            cmdSearch.Text = "Go";
            cmdSearch.ImageIndex = 0;

            dbCheckBox1.ConnectionString = ConnectionString; //This one gets changed when the server alias changes.
            dbCheckBox1.DefaultConnectionString = StoreConnectionString; //This is the main connection to the source control db

            dbCheckBox1.Parent = this;
            dbCheckBox1.Left = 903;
            dbCheckBox1.Top = 2;
            dbCheckBox1.BringToFront();
            dbCheckBox1.SelectionChangedDatabase += DatabaseChanged;
            dbCheckBox1.RefreshDatabase += RedoSearchDB;

            objectSelector1.BringToFront();
            objectSelector1.SelectionChangedObjectSelector += ObjectsChanged;

            dbCheckBox1.BringToFront();

            scMain.Left = pSearch.Left;
            scMain.Width = this.Width - 20;

            scMain.Top = 40;
            scMain.Height = this.Height - scMain.Top - 5;

            pDetail.Top = pStatusBar.Top + pStatusBar.Height + 5;
            pDetail.Height = scMain.Panel2.Height - pDetail.Top;
            pDetail.Width = pStatusBar.Width;

            cmdSearch.Enabled = false;

            DatabasesSelected = dbCheckBox1.DatabaseSelection;
            ObjectsSelected = objectSelector1.ObjectsSelected;

            dgvDetail.Visible = false;
            tDetail.Visible = true;

            LoadEnvironments();
            cbEnvironment.Text = gs.ServerAliasSelected;

            Search_Resize(sender, e);

            tSearchString.Focus();

            Loading = false;
        }

        private void DatabaseChanged(object sender, EventArgs e)
        {
            DatabasesSelected = ((DBCheckBox)sender).DatabaseSelection;

            EnableDisableSearch();
        }

        private void RedoSearchDB(object sender, EventArgs e)
        {
            DatabasesSelected = ((DBCheckBox)sender).DatabaseSelection;

            EnableDisableSearch();

            if (cmdSearch.Enabled)
            {
                SearchAll();
            }
        }

        private void LoadEnvironments()
        {
            try
            {
                cbEnvironment.Items.Clear();

                using (DataStuff sn = new DataStuff())
                {
                    sn.ConnectionString = StoreConnectionString;

                    DataTable dt = sn.GetServerAliases();

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            cbEnvironment.Items.Add(row["ServerAliasDesc"].ToString());
                        }
                    }
                }
            }

            catch
            {
                throw;
            }
        }

        private void ObjectsChanged(object sender, EventArgs e)
        {
            ObjectsSelected = ((ObjectSelector)sender).ObjectsSelected;

            EnableDisableSearch();

            if (cmdSearch.Enabled)
            {
                SearchAll();
            }
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            if (cmdSearch.Text == "Go")
            {
                cmdSearch.Text = "Cancel";
                cmdSearch.ImageIndex = 1;
                Application.DoEvents();

                CancelNow = false;

                SearchAll();
            }
            else
            {
                cmdSearch.Text = "Go";
                cmdSearch.ImageIndex = 0;
                lStatusLeft.Text = "Cancelling... Please wait";

                Application.DoEvents();

                CancelNow = true;

                BusySearching = false;
                Application.UseWaitCursor = false;
                Application.DoEvents();
                dgvObjects.UseWaitCursor = false;
                dgvObjects.Cursor = Cursors.Default;
                Application.DoEvents();
                dgvDetail.UseWaitCursor = false;
                lStatusLeft.Text = "Search cancelled";
                Application.DoEvents();
            }
        }

        private void SearchAll()
        {
            List<String> DatabaseElements;

            if (BusySearching)
            {
                return;
            }

            if (cmdSearch.Text == "Go")
            {
                cmdSearch.Text = "Cancel";
                cmdSearch.ImageIndex = 1;
                Application.DoEvents();

                CancelNow = false;
            }

            BusySearching = true;
            Application.UseWaitCursor = true;
            Application.DoEvents();

            DatabaseElements = DatabasesSelected.Split(',').ToList();

            dtColumns = null;
            dtDetail = null;
            dtObjects = null;

            dgvObjects.Rows.Clear();

            for (int i = 0; i < DatabaseElements.Count; i++)
            {
                try
                {
                    if (CancelNow)
                    {
                        return;
                    }

                    DoSearch(DatabaseElements[i]);
                    Application.DoEvents();
                }

                catch
                {
                }
            }

            cmdSearch.Text = "Go";
            cmdSearch.ImageIndex = 0;
            Application.DoEvents();

            BusySearching = false;
            Application.UseWaitCursor = false;
            Application.DoEvents();
            dgvObjects.UseWaitCursor = false;
            dgvObjects.Cursor = Cursors.Default;
            Application.DoEvents();
            dgvDetail.UseWaitCursor = false;
            Application.DoEvents();
        }

        private void DoSearch(string DBName)
        {
            string ConnStr = string.Empty;
            string ObjectName = string.Empty;
            string SchemaName = string.Empty;
            string DatabaseName = string.Empty;
            string ObjectType = string.Empty;
            string MatchesOn = string.Empty;
            string ObjectDetail = string.Empty;
            int ObjectCount = 0;
            string ExactMatch = string.Empty;
            string TempType = string.Empty;
            string ObjectId = string.Empty;

            if (cExactMatch.Checked)
            {
                ExactMatch = "Y";
            }
            else
            {
                ExactMatch = "N";
            }

            GetServerDetail();

            ConnStr = ConnectionString + ";Database=" + DBName;

            try
            {
                lStatusRight.Text = "";
                lStatusLeft.Text = "Searching " + DBName + "...";
                Application.DoEvents();

                if (CancelNow)
                {
                    return;
                }

                using (DataStuff sn = new DataStuff())
                {
                    DataSet ds = sn.Search(ConnStr, tSearchString.Text, ObjectsSelected, ExactMatch);

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            if (dtObjects == null)
                            {
                                dtObjects = ds.Tables[0];
                            }
                            else
                            {
                                dtObjects.Merge(ds.Tables[0]);
                            }

                            if (ds.Tables.Count > 1)
                            {
                                if (dtColumns == null)
                                {
                                    dtColumns = ds.Tables[1];
                                }
                                else
                                {
                                    dtColumns.Merge(ds.Tables[1]);
                                }
                            }

                            if (ds.Tables.Count > 2)
                            {
                                if (dtDetail == null)
                                {
                                    dtDetail = ds.Tables[2];
                                }
                                else
                                {
                                    dtDetail.Merge(ds.Tables[2]);
                                }
                            }

                            if (dtObjects.Rows.Count > 0)
                            {
                                foreach (DataRow row in dtObjects.Rows)
                                {
                                    Application.DoEvents();

                                    if (CancelNow)
                                    {
                                        return;
                                    }

                                    ObjectName = row["ObjectName"].ToString();
                                    SchemaName = row["SchemaName"].ToString();
                                    DatabaseName = row["DatabaseName"].ToString();
                                    ObjectType = row["ObjectType"].ToString();
                                    ObjectId = row["ObjectID"].ToString();
                                    MatchesOn = "Name";

                                    //If the type is a table or view, get the detail columns

                                    switch (ObjectType)
                                    {
                                        case "SQL_STORED_PROCEDURE":
                                            ObjectType = "Stored Procedure";
                                            break;

                                        case "USER_TABLE":
                                            ObjectType = "User Table";
                                            break;

                                        case "VIEW":
                                            ObjectType = "View";
                                            break;

                                        case "SYSTEM_TABLE":
                                            ObjectType = "System Table";
                                            break;

                                        case "SQL_SCALAR_FUNCTION":
                                            ObjectType = "Function";
                                            break;

                                        case "CLR_SCALAR_FUNCTION":
                                            ObjectType = "Function";
                                            break;

                                        case "SQL_TABLE_VALUED_FUNCTION":
                                            ObjectType = "Function";
                                            break;

                                        case "AGGREGATE_FUNCTION":
                                            ObjectType = "Function";
                                            break;

                                        case "SQL_INLINE_TABLE_VALUED_FUNCTION":
                                            ObjectType = "Function";
                                            break;

                                        case "SQL_TRIGGER":
                                            ObjectType = "Trigger";
                                            break;

                                        case "DEFAULT_CONSTRAINT":
                                            ObjectType = "Constraint";
                                            break;

                                        case "FOREIGN_KEY_CONSTRAINT":
                                            ObjectType = "Constraint";
                                            break;

                                        case "PRIMARY_KEY_CONSTRAINT":
                                            ObjectType = "Constraint";
                                            break;

                                        case "UNIQUE_CONSTRAINT":
                                            ObjectType = "Constraint";
                                            break;

                                        default:
                                            break;
                                    }

                                    switch (ObjectType)
                                    {
                                        case "User Table":
                                            ObjectDetail = GetColumnDetail(ObjectName, ObjectType, ObjectId);

                                            if (ObjectName.ToUpper().Contains(tSearchString.Text.ToUpper()))
                                            {
                                                MatchesOn = "Name and Column";
                                            }
                                            else
                                            {
                                                MatchesOn = "Column";
                                            }

                                            break;

                                        case "View":
                                            ObjectDetail = GetColumnDetail(ObjectName, ObjectType, ObjectId);

                                            if (ObjectName.ToUpper().Contains(tSearchString.Text.ToUpper()))
                                            {
                                                MatchesOn = "Name and Column";
                                            }
                                            else
                                            {
                                                MatchesOn = "Column";
                                            }

                                            break;

                                        case "Stored Procedure":
                                            ObjectDetail = GetObjectDetail(ObjectName, ObjectType);

                                            if (ObjectName.ToUpper().Contains(tSearchString.Text.ToUpper()))
                                            {
                                                MatchesOn = "Name and Content";
                                            }
                                            else
                                            {
                                                MatchesOn = "Content";
                                            }

                                            break;

                                        case "Function":
                                            ObjectDetail = GetObjectDetail(ObjectName, ObjectType);

                                            if (ObjectName.ToUpper().Contains(tSearchString.Text.ToUpper()))
                                            {
                                                MatchesOn = "Name and Content";
                                            }
                                            else
                                            {
                                                MatchesOn = "Content";
                                            }

                                            break;

                                        case "Trigger":
                                            ObjectDetail = GetObjectDetail(ObjectName, ObjectType);

                                            if (ObjectName.ToUpper().Contains(tSearchString.Text.ToUpper()))
                                            {
                                                MatchesOn = "Name and Content";
                                            }
                                            else
                                            {
                                                MatchesOn = "Content";
                                            }

                                            break;

                                        case "Constraint":
                                            ObjectDetail = GetObjectDetail(ObjectName, ObjectType);

                                            MatchesOn = "Name";

                                            break;

                                        default:
                                            break;
                                    }

                                    if (ObjectType.Contains("Table"))
                                    {
                                        TempType = "Table";
                                    }
                                    else
                                    {
                                        TempType = ObjectType;
                                    }


                                    if (ObjectsSelected.Contains(TempType))
                                    {
                                        dgvObjects.Rows.Add(ObjectName, SchemaName, DatabaseName, ObjectType, MatchesOn, ObjectDetail, ObjectId);
                                        ObjectCount++;
                                    }
                                }
                            }

                            lStatusRight.Text = "Object count " + dgvObjects.Rows.Count.ToString();
                        }
                    }
                }

                if (dgvObjects.Rows.Count > 0)
                {
                    //lStatusLeft.Text = dgvObjects.Rows[0].Cells[3].Value.ToString() + " " + dgvObjects.Rows[0].Cells[0].Value.ToString();
                    lStatusLeft.Text = "Done";
                    dgvObjects.Focus();
                }
                else
                {
                    lStatusLeft.Text = "";
                }
            }

            catch
            {
            }
        }

        private string GetObjectDetail(string ObjectName, string ObjectType)
        {
            string DetailList = "";
            string DetailRow = "";

            DataTable dt = new DataTable();

            if (dtDetail != null)
            {
                dt = dtDetail;
            }
            else
            {
                return "";
            }

            if (dt.Rows.Count > 0)
            {
                lStatusLeft.Text = ObjectType + " " + ObjectName;

                foreach (DataRow row in dt.Rows)
                {
                    if (row["ObjectName"].ToString() == ObjectName)
                    {
                        DetailRow = row["ObjectText"].ToString();

                        if (DetailList.Trim() == "")
                        {
                            DetailList = DetailRow;
                        }
                        else
                        {
                            DetailList = DetailList + Environment.NewLine + DetailRow;
                        }
                    }
                }
            }

            return DetailList;
        }

        private string GetColumnDetail(string ObjectName, string ObjectType, string ObjectID)
        {
            string ColumnList = "";
            string ColumnType = string.Empty;
            string ColumnLength = string.Empty;
            string CurrentColumn = string.Empty;
            string FinalType = string.Empty;

            DataTable dt = new DataTable();

            if (dtColumns != null)
            {
                dt = dtColumns;
            }
            else
            {
                return "";
            }

            if (dt.Rows.Count > 0)
            {
                lStatusLeft.Text = ObjectType + " " + ObjectName;

                foreach (DataRow row in dt.Rows)
                {
                    if ((row["TableName"].ToString() == ObjectName) && (row["TableObjectId"].ToString() == ObjectID))
                    {
                        CurrentColumn = row["ColumnName"].ToString();
                        ColumnType = row["DataType"].ToString();
                        ColumnLength = row["ColumnLength"].ToString();

                        switch (ColumnType)
                        {
                            case "varchar":
                                FinalType = ColumnType + "(" + ColumnLength + ")";
                                break;

                            case "nvarchar":
                                FinalType = ColumnType + "(" + ColumnLength + ")";
                                break;

                            case "char":
                                FinalType = ColumnType + "(" + ColumnLength + ")";
                                break;

                            case "nchar":
                                FinalType = ColumnType + "(" + ColumnLength + ")";
                                break;

                            case "binary":
                                FinalType = ColumnType + "(" + ColumnLength + ")";
                                break;

                            case "decimal":
                                FinalType = ColumnType + "(" + ColumnLength + ")";
                                break;

                            case "numeric":
                                FinalType = ColumnType + "(" + ColumnLength + ")";
                                break;

                            default:
                                FinalType = ColumnType;
                                break;
                        }

                        if (ColumnList.Trim() == "")
                        {
                            ColumnList = row["ColumnName"].ToString() + " " + FinalType;
                        }
                        else
                        {
                            ColumnList = ColumnList + ", " + row["ColumnName"].ToString() + " " + FinalType;
                        }
                    }
                }
            }

            return ColumnList;
        }

        private bool GetServerDetail()
        {
            try
            {
                string ServerName = string.Empty;
                string UserName = string.Empty;
                byte[] Password = null;
                string IntegratedSecurity = string.Empty;

                //ConnectionString = "";

                using (DataStuff sn = new DataStuff())
                {
                    sn.ConnectionString = StoreConnectionString;

                    DataTable dt = sn.GetServerDetail(cbEnvironment.Text, UserID);

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

        private void LoadObjectDetail(string ObjectName, string ObjectType)
        {
            string DetailRow = "";

            tDetail.Visible = true;
            dgvDetail.Visible = false;

            DataTable dt = new DataTable();

            if (dtDetail != null)
            {
                dt = dtDetail;
            }
            else
            {
                return;
            }

            if (dt.Rows.Count > 0)
            {
                lStatusLeft.Text = ObjectType + " " + ObjectName;

                foreach (DataRow row in dt.Rows)
                {
                    if (row["ObjectName"].ToString() == ObjectName)
                    {
                        DetailRow = row["ObjectText"].ToString();

                        tDetail.Text = tDetail.Text + DetailRow;
                    }
                }

                Regex regExp = new Regex(tSearchString.Text, RegexOptions.IgnoreCase);

                foreach (Match match in regExp.Matches(tDetail.Text))
                {
                    tDetail.Select(match.Index, match.Length);
                    tDetail.SelectionBackColor = MatchColour;
                }
            }
        }

        private void Search_Resize(object sender, EventArgs e)
        {
            scMain.Left = pSearch.Left;
            scMain.Width = this.Width - 20;

            scMain.Top = 40;
            scMain.Height = this.Height - scMain.Top - 55;

            pDetail.Width = pStatusBar.Width;
        }

        private void EnableDisableSearch()
        {
            if (tSearchString.Text.Trim() == "")
            {
                cmdSearch.Enabled = false;
                return;
            }

            if (DatabasesSelected.Trim() == "")
            {
                cmdSearch.Enabled = false;
                return;
            }

            if (ObjectsSelected.Trim() == "")
            {
                cmdSearch.Enabled = false;
                return;
            }

            if (cbEnvironment.Text == "")
            {
                cmdSearch.Enabled = false;
                return;
            }

            cmdSearch.Enabled = true;
        }

        private void tSearchString_TextChanged(object sender, EventArgs e)
        {
            EnableDisableSearch();
        }

        private void Search_ResizeEnd(object sender, EventArgs e)
        {
            //tSearchString.Focus();
        }

        private void LoadObjectDetailGrid(string ObjectName, string ObjectType, string ObjectId)
        {
            string ColumnName = "";
            string ColumnNumber = "";
            string DataType = "";
            string DataLength = "";

            dgvDetail.Visible = true;
            tDetail.Visible = false;

            dgvDetail.Columns.Clear();
            dgvDetail.Columns.Add("ColumnOrder", "Column Order");
            dgvDetail.Columns.Add("ColumnName", "Name");
            dgvDetail.Columns.Add("DataType", "Data Type");
            dgvDetail.Columns.Add("DataLength", "Column Length");

            dgvDetail.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            DataTable dt = new DataTable();

            if (dtColumns != null)
            {
                dt = dtColumns;
            }
            else
            {
                return;
            }

            if (dt.Rows.Count > 0)
            {
                lStatusLeft.Text = ObjectType + " " + ObjectName;

                foreach (DataRow row in dt.Rows)
                {
                    if ((row["TableName"].ToString() == ObjectName) && (row["TableObjectId"].ToString() == ObjectId))
                    {
                        ColumnName = row["ColumnName"].ToString();
                        ColumnNumber = row["ColumnNo"].ToString();
                        DataType = row["DataType"].ToString();
                        DataLength = row["ColumnLength"].ToString();

                        dgvDetail.Rows.Add(ColumnNumber, ColumnName, DataType, DataLength);
                    }
                }

                for (int i = 0; i < dgvDetail.Rows.Count; i++)
                {
                    if (dgvDetail.Rows[i].Cells[1].Value.ToString().ToUpper().Contains(tSearchString.Text.ToUpper()))
                    {
                        dgvDetail.Rows[i].Cells[1].Style.BackColor = MatchColour;
                    }
                }
            }
        }

        private void dgvObjects_SelectionChanged(object sender, EventArgs e)
        {
            int CurRow = 0;
            tDetail.Text = "";

            string ObjectType = string.Empty;
            string ObjectName = string.Empty;
            string ObjectId = string.Empty;

            dgvDetail.Visible = false;
            tDetail.Text = "";

            if (dgvObjects.SelectedRows.Count > 0)
            {
                CurRow = dgvObjects.SelectedRows[0].Index;

                try
                {
                    ObjectName = Convert.ToString(dgvObjects[0, CurRow].Value);
                    ObjectType = Convert.ToString(dgvObjects[3, CurRow].Value);
                    ObjectId = Convert.ToString(dgvObjects[6, CurRow].Value);

                    switch (ObjectType)
                    {
                        case "User Table":
                            LoadObjectDetailGrid(ObjectName, ObjectType, ObjectId);
                            break;

                        case "View":
                            LoadObjectDetailGrid(ObjectName, ObjectType, ObjectId);
                            break;

                        case "Stored Procedure":
                            LoadObjectDetail(ObjectName, ObjectType);
                            break;

                        case "Function":
                            LoadObjectDetail(ObjectName, ObjectType);
                            break;

                        case "Trigger":
                            LoadObjectDetail(ObjectName, ObjectType);
                            break;

                        default:
                            lStatusLeft.Text = "";
                            break;
                    }
                }

                catch
                {
                }
            }
        }

        private void cbEnvironment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Loading)
            {
                return;
            }

            dbCheckBox1.SetDatabaseName(cbEnvironment.Text);
            gs.ServerAliasSelected = cbEnvironment.Text;
            EnableDisableSearch();
        }
    }
}
