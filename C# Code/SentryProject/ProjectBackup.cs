using System;
using System.Data;
using System.Windows.Forms;
using SentryDataStuff;
using System.Drawing;

namespace SentryProject
{
    public partial class ProjectBackup : Form
    {
        int UserID = 0;
        string UserName = string.Empty;
        int DBObjNameCol = 0;
        int DBNameCol = 0;

        DataGridView dgvMain;

        public ProjectBackup()
        {
            InitializeComponent();
        }

        private void ProjectBackup_Load(object sender, EventArgs e)
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

            GetServers();

            cmdBackup.Enabled = false;
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
        
        private string GetServerGroupFromAlias(string AliasName)
        {
            string GroupDesc = string.Empty;

            using (DataStuff sn = new DataStuff())
            {
                DataTable dt = sn.GetServerGroupFromAlias(AliasName);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        GroupDesc = row["ServerGroupDesc"].ToString();
                    }
                }
            }

            return GroupDesc;
        }

        private void LoadProjects()
        {    
            try
            {
                cmdBackup.Enabled = false;

                string GroupName = string.Empty;
                string IncludeInactive = string.Empty;

                if (cbServers.Text == "")
                {
                    return;
                }
                
                GroupName = GetServerGroupFromAlias(cbServers.Text);

                IncludeInactive = cActiveOnly.Checked ? "N" : "Y";

                cbProject.Items.Clear();
                                
                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetProjectsByServerGroup(GroupName, IncludeInactive);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            cbProject.Items.Add(row["ProjectName"].ToString());
                        }
                    }
                }
            }

            catch
            {
                throw;
            }
        }

        private void cbServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProjects();
        }
        
        private void cActiveOnly_CheckedChanged(object sender, EventArgs e)
        {
            LoadProjects();
        }

        private void CreateGrid()
        {
            if (cbProject.Text == "")
            {
                return;
            }

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
            int y = gbGrid.Height - 20;

            dgvMain.Size = new Size(x, y);
            dgvMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dgvMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dgvMain.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvMain.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvMain.GridColor = Color.Black;
            dgvMain.RowHeadersVisible = false;
            dgvMain.AllowUserToAddRows = false;
            dgvMain.AllowUserToOrderColumns = false;
            dgvMain.Columns.Add("ProjectID", "ProjectID");
            dgvMain.Columns[ColNo].Visible = false;

            ColNo++;
            dgvMain.Columns.Add("ProjectName", "ProjectName");
            dgvMain.Columns[ColNo].Visible = true;

            ColNo++;
            dgvMain.Columns.Add("DatabaseName", "Database Name");
            DBNameCol = ColNo;

            ColNo++;
            dgvMain.Columns.Add("DBObjectName", "Object Name");
            DBObjNameCol = ColNo;

            ColNo++;
            dgvMain.Columns.Add("ObjectText", "Object Text");
            DBObjNameCol = ColNo;

            ColNo++;
            dgvMain.Columns.Add("ProjectObjectID", "ProjectObjectID");
            dgvMain.Columns[ColNo].Visible = false;
            
            dgvMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMain.MultiSelect = false;

            x = 7;
            y = 20;

            gbGrid.Controls.Add(dgvMain);

            dgvMain.Location = new Point(x, y);
            dgvMain.Parent = gbGrid;
            
            dgvMain.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom);
        }
        private void LoadGrid()
        {
            string ObjectText = string.Empty;
            int EmptyCount = 0;

            if (cbProject.Text == "")
            {
                return;
            }

            dgvMain.Rows.Clear();

            using (DataStuff sn = new DataStuff())
            {
                DataTable dt = sn.GetProjectObjects(cbProject.Text);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["DBObjectName"].ToString()))
                        {
                            ObjectText = "";

                            ObjectText = LoadHelpText(row["DBName"].ToString(), row["DBObjectName"].ToString());

                            if (ObjectText.Trim() == "")
                            {
                                EmptyCount++;
                            }

                            dgvMain.Rows.Add(row["ProjectID"].ToString(),
                                             row["ProjectName"].ToString(),
                                             row["DBName"].ToString(),
                                             row["DBObjectName"].ToString(),
                                             ObjectText,
                                             row["ProjectObjectID"].ToString());

                        }
                    }   
                }
            }

            cmdBackup.Enabled = true;

            if (EmptyCount > 0)
            {
                MessageBox.Show(EmptyCount.ToString() + " objects do not have valid object text. Please make sure that you are backing up from the correct server.", "Project Backup", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
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

        private string LoadHelpText(string DBName, string ObjName)
        {
            string ObjectText = string.Empty;

            string ConnString = GetServerConnectionString(cbServers.Text, UserID);
            
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

        private void cbProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateGrid();
            LoadGrid();
        }
        
        private void cmdBackup_Click(object sender, EventArgs e)
        {
            bool Success = false;
            bool FoundError = false;
            string DatabaseName = string.Empty;
            string ObjectName = string.Empty;
            string ObjectText = string.Empty;
            int SuccessCount = 0;
            int FailCount = 0;
            int RowCount = 0;
            string ProjectName = string.Empty;
            int ProjectID = 0;
            int SetID = 0;
            bool SetDone = false;

            foreach (DataGridViewRow row in dgvMain.Rows)
            {
                FoundError = false;
                DatabaseName = "";
                ObjectName = "";
                ObjectText = "";
                ProjectName = "";

                try
                {
                    DatabaseName = Convert.ToString(row.Cells["DatabaseName"].Value);
                    ObjectName = Convert.ToString(row.Cells["DBObjectName"].Value);
                    ObjectText = Convert.ToString(row.Cells["ObjectText"].Value);
                    ProjectName = cbProject.Text;

                    if (!SetDone)
                    {
                        using (DataStuff sn = new DataStuff())
                        {
                            DataTable dt = sn.SaveProjectBackupSet(ProjectName);

                            if (dt != null)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    int.TryParse(dt.Rows[0]["ProjectID"].ToString(), out ProjectID);
                                    int.TryParse(dt.Rows[0]["ProjectSetID"].ToString(), out SetID);
                                }
                            }
                        }

                        SetDone = true;
                    }

                    if ((ObjectText.Trim() != "") && (ProjectID > 0) && (SetID > 0))
                    {
                        using (DataStuff sn = new DataStuff())
                        {
                            Success = sn.DoProjectBackup(SetID, ProjectID, ObjectName, DatabaseName, ObjectText);
                        }
                    }
                    else
                    {
                        Success = false;
                    }

                    RowCount++;

                    if (Success)
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
                        MessageBox.Show("There was an error backing up object " + ObjectName + ".", "Project Backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                catch
                {

                }

                Application.DoEvents();
            }

            if ((SuccessCount > 0) && (FailCount == 0))
            {
                MessageBox.Show(SuccessCount.ToString() + " objects successfully backed-up.", "Project Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(SuccessCount.ToString() + " objects successfully backed-up, " + FailCount.ToString() + " objects failed to back-up.", "Project Backup", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
