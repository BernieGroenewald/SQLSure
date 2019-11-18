using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using SentryGeneral;
using SentryDataStuff;


namespace SentryProject
{
    public partial class Project : UserControl
    {
        string DBName = string.Empty;
        string ObjectName = string.Empty;
        string UserName = string.Empty;
        int UserID = 0;
        public string ReleaseFromServer = string.Empty;
        public string ReleaseToServer1 = string.Empty;
        public string ReleaseToServer2 = string.Empty;
        public string ReleaseToServer3 = string.Empty;
        public string ReleaseToServer4 = string.Empty;
        public string ConnReleaseFromServer = string.Empty;
        public string ConnReleaseToServer1 = string.Empty;
        public string ConnReleaseToServer2 = string.Empty;
        public string ConnReleaseToServer3 = string.Empty;
        public string ConnReleaseToServer4 = string.Empty;
        public bool ProjectHasChanged = false;

        public string UATServer = string.Empty;
        public string ProdServer = string.Empty;

        public event EventHandler ProjectSaved;
        public event ProjectLoadedHandler ProjectLoaded ;
        public delegate void ProjectLoadedHandler(ProjectEventArgs e);

        private bool ProjectOnly;
        private bool ApproveOnly;
        string InitialPath = string.Empty;
        public bool IsNewObject = false;
        int FileTextCol = 0;
        int CheckBoxCol = 0;
        int PrevCheckCount = 0;
        int ReleaseFromServerCol = 0;
        int ReleaseToServer1Col = 0;
        int ReleaseToServer2Col = 0;
        int ReleaseToServer3Col = 0;
        int ReleaseToServer4Col = 0;
        int DBNameCol = 0;
        int DBObjNameCol = 0;
        bool StrictObjectAdd = false;
        General gs = new General();
        bool IsProductionRelease = false;
        bool RequireTesterApproval = false;
        bool RequireApproverApproval = false;
        bool UpdateChangeLog = false;
        Color AllOkay = Color.Lime;
        Color Danger = Color.Yellow;
        Color Warn = Color.DeepSkyBlue;

        DataGridView dgvMain;

        AccessRights ar = new AccessRights();

        bool CanReleaseUAT = false;
        bool CanReleasePreProd = false;
        bool CanReleaseProd = false;

        public Project()
        {
            InitializeComponent();
        }

        private void cbServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReleaseFromServer = "";
            AddServer(cbServers.Text);
            cbProject.Enabled = true;
            cbProject.Text = "";

            if (dgvMain != null)
            {
                if (dgvMain.Rows.Count > 0)
                {
                    dgvMain.Rows.Clear();
                }
            }

            LoadProjects();

            if (ApproveOnly)
            {
                UATServer = cbServers.Text;

                ReleaseFromServer = UATServer;

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetCheckOutFromServersProduction(ReleaseFromServer);

                    if (dt.Rows.Count > 0)
                    {
                        ProdServer = dt.Rows[0]["ServerAliasDesc"].ToString();
                    }
                }

                ReleaseToServer1 = ProdServer;
            }
        }

        private void GetStrictSettings()
        {
            string SettingValue = string.Empty;

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    SettingValue = sn.GetSystemSetting("Key5");

                    if (SettingValue.Trim() == "true")
                    {
                        StrictObjectAdd = true;
                    }
                    else
                    {
                        StrictObjectAdd = false;
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

                    SettingValue = sn.GetSystemSetting("Key11");

                    if (SettingValue.Trim() == "true")
                    {
                        UpdateChangeLog = true;
                    }
                    else
                    {
                        UpdateChangeLog = false;
                    }
                }
            }

            catch
            {

            }
        }
        
        private void cbProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReCreateGrid();
        }

        private void cbPartOfProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsApproved())
            {
                return;
            }

            ReCreateGrid();
        }

        private void cbObjectStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tsbAddItem_Click(object sender, EventArgs e)
        {
            string ReturnDBName = string.Empty;
            string ReturnObject = string.Empty;

            if (StrictObjectAdd)
            {
                MessageBox.Show("Please add an object to this project by checking it out in the development environment and selecting this project.", "Add Database Object", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            AddDBItem db = new AddDBItem();
            db.ConnectionString = ConnReleaseFromServer;
            db.ShowDialog();

            ReturnDBName = db.DatabaseName;
            ReturnObject = db.ObjectName;

            if (ReturnDBName.Trim() != "" && ReturnObject.Trim() != "")
            {
                LoadGridRow(ReturnDBName, ReturnObject);

                SetSequence();
            }
        }

        private void tsbAddItemFile_Click(object sender, EventArgs e)
        {
            string FileName = string.Empty;

            InitialPath = gs.ProjectScriptFilePath;

            OpenFileDialog fd = new OpenFileDialog();

            if (InitialPath.Trim() != "")
            {
                fd.InitialDirectory = InitialPath;
            }
            else
            {
                fd.Filter = "SQL files (*.sql)|*.sql|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            }

            fd.FilterIndex = 1;
            fd.RestoreDirectory = true;

            if (fd.ShowDialog() == DialogResult.OK)
            {
                FileName = fd.FileName;

                string text = System.IO.File.ReadAllText(FileName);

                if (text.Trim() != "")
                {
                    LoadGridRowFile(fd.SafeFileName, text);

                    SetSequence();
                }

                InitialPath = Path.GetDirectoryName(FileName);

                gs.ProjectScriptFilePath = InitialPath;

                ProjectHasChanged = true;

                if (ProjectSaved != null)
                {
                    ProjectSaved(this, null);
                }

                return;
            }
            else
            {
                return;
            }
        }

        private void tsbDeleteItem_Click(object sender, EventArgs e)
        {
            if (this.dgvMain.SelectedRows.Count > 0)
            {
                dgvMain.Rows.RemoveAt(this.dgvMain.SelectedRows[0].Index);

                SetSequence();
            }
        }

        private void tsbNewProject_Click(object sender, EventArgs e)
        {
            AddProject ap = new AddProject();
            ap.ShowDialog();

            if (ap.NewProjectName.Trim() != "")
            {
                LoadProjects();
                cbPartOfProject.Text = ap.NewProjectName;
                ProjectHasChanged = true;

                if (ProjectSaved != null)
                {
                    ProjectSaved(this, null);
                }
            }
        }

        private void tsbSaveProject_Click(object sender, EventArgs e)
        {
            bool Success = SaveProject(true);
        }

        private void tsbMoveUp_Click(object sender, EventArgs e)
        {
            moveUp();
        }

        private void tsbMoveDown_Click(object sender, EventArgs e)
        {
            moveDown();
        }

        private bool IsApproved()
        {
            bool TesterApproved = false;
            bool FinalApproved = false;

            if (IsProductionRelease)
            {
                if ((RequireTesterApproval) || (RequireApproverApproval))
                {
                    //See if this project has been approved...

                    try
                    {
                        using (DataStuff sn = new DataStuff())
                        {
                            DataTable dt = sn.ProjectGetApprovalStatusRelease(ReleaseFromServer, cbPartOfProject.Text);

                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    if (row["FunctionName"].ToString() == "Approve Testing")
                                    {
                                        TesterApproved = true;
                                    }

                                    if (row["FunctionName"].ToString() == "Approve Final")
                                    {
                                        FinalApproved = true;
                                    }
                                }
                            }
                        }

                        if ((RequireTesterApproval) && (!TesterApproved))
                        {
                            MessageBox.Show("This project requires tester approval before release.", "Release Project", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return false;
                        }

                        if ((RequireApproverApproval) && (!FinalApproved))
                        {
                            MessageBox.Show("This project requires final approver approval before release.", "Release Project", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return false;
                        }
                    }

                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        private void tsbRelease_Click(object sender, EventArgs e)
        {
            string ObjectType = string.Empty;
            string ReleaseTo = string.Empty;
            string ReleaseDBName = string.Empty;
            string ReleaseObjectName = string.Empty;
            string ReleaseObjectText = string.Empty;
            string RetVal = string.Empty;

            ReleaseTo = lReleaseToServer.Text.Substring(11);

            if (!IsApproved())
            {
                return;
            }

            DialogResult result = MessageBox.Show("Release the objects from " + ReleaseFromServer + " to " + ReleaseTo + "?", "Release Objects", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SaveProjectStatus();

                foreach (DataGridViewRow row in dgvMain.Rows)
                {
                    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)row.Cells["ObjectSelected"];
                    checkCell.TrueValue = true;

                    if (Convert.ToBoolean(checkCell.Value) == Convert.ToBoolean(checkCell.TrueValue))
                    {
                        ObjectType = "";
                        ObjectType = Convert.ToString(row.Cells["DatabaseName"].Value) == "" ? "File" : "Object";

                        if (ObjectType == "Object")
                        {
                            ReleaseDBName = Convert.ToString(row.Cells["DatabaseName"].Value);
                            ReleaseObjectName = Convert.ToString(row.Cells["DBObjectName"].Value);
                            ReleaseObjectText = GetObjectText(ConnReleaseFromServer, ReleaseDBName, ReleaseObjectName);

                            if (ReleaseObjectText.Trim() != "")
                            {
                                if (!DeployObject(ConnReleaseFromServer, ReleaseDBName, ReleaseObjectName, ReleaseObjectText))
                                {
                                    return;
                                }
                            }
                        }

                        if (ObjectType == "File")
                        {
                            ReleaseObjectName = Convert.ToString(row.Cells["FileObjectDesc"].Value);
                            ReleaseObjectText = Convert.ToString(row.Cells["FileObject"].Value);

                            if (ReleaseObjectText.Trim() != "")
                            {
                                if (!DeployFile(ReleaseObjectName, ReleaseObjectText))
                                {
                                    return;
                                }
                            }
                        }
                    }
                }

                if (IsProductionRelease)
                {
                    if (UpdateChangeLog)
                    {
                        try
                        {
                            using (DataStuff sn = new DataStuff())
                            {
                                sn.UpdateChangeLog(ReleaseFromServer, cbPartOfProject.Text, UserID);
                            }
                        }

                        catch
                        {
                            MessageBox.Show("There was an error updating the change log." + Environment.NewLine + "Please update this log manually.", "Release Object", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    //Set the project active to false

                    try
                    {
                        rbActiveNo.Checked = true;
                        SaveProject(false);
                    }

                    catch
                    {

                    }
                }

                MessageBox.Show("Object(s) successfully released.", "Release Object", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Project_Load(object sender, EventArgs e)
        {
            using (DataStuff sn = new DataStuff())
            {
                //sn.ConnectionString = ConnReleaseFromServer;

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

            ar.UserID = UserID;
            CanReleaseUAT = ar.ReleaseUAT;
            CanReleasePreProd = ar.ReleasePreProduction;
            CanReleaseProd = ar.ReleaseProduction;

            GetServers(UserID);

            DisableToolbar();

            cbPartOfProject.Enabled = false;
            cbProject.Enabled = false;

            lReleaseFromServer.Text = "";
            lReleaseToServer.Text = "";
            LoadObjectStatus();

            gbEnvironments.Width = this.Width - 10;
            gbEnvironments.Left = 3;
            gbProjectOnly.Left = 3;
            gbProjectOnly.Top = 3;
            gbProjectOnly.Width = gbEnvironments.Width;
            gbProject.Width = gbEnvironments.Width;
            gbGrid.Width = gbEnvironments.Width;

            if (!ProjectOnly)
            {
                pLegendAllOkay.BackColor = AllOkay;
                lLegendAllOkay.Text = "Release version is the same";

                pLegendDanger.BackColor = Danger;
                lLegendDanger.Text = "Release to server has a newer version";

                pLegendWarn.BackColor = Warn;
                lLegendWarn.Text = "Release version is newer";

                gbEnvironments.Visible = true;
                gbProjectOnly.Visible = false;
                gbProject.Visible = true;
                gbLegend.Visible = true;
                lStatus.Visible = true;
                lStatus.Top = this.Height - 20;
            }
            else
            {
                gbEnvironments.Visible = false;
                gbProjectOnly.Visible = true;
                gbProject.Visible = false;
                gbLegend.Visible = false;

                gbGrid.Top = gbProjectOnly.Top + gbProjectOnly.Height + 10;
                gbGrid.Height = gbGrid.Height + 100;

                tsbRelease.Visible = false;
                lStatus.Visible = false;
            }

            if (ApproveOnly)
            {
                pLegendAllOkay.BackColor = AllOkay;
                lLegendAllOkay.Text = "Release version is the same";

                pLegendDanger.BackColor = Danger;
                lLegendDanger.Text = "Release to server has a newer version";

                pLegendWarn.BackColor = Warn;
                lLegendWarn.Text = "Release version is newer";

                gbEnvironments.Visible = false;
                gbProjectOnly.Visible = true;
                gbProject.Visible = false;
                gbLegend.Visible = true;

                tsbAddItem.Enabled = false;
                tsbAddItemFile.Enabled = false;
                tsbDeleteItem.Enabled = false;
                tsbMoveDown.Enabled = false;
                tsbMoveUp.Enabled = false;
                tsbNewProject.Enabled = false;
                tsbRelease.Enabled = false;
                tsbSaveProject.Enabled = false;

                lStatus.Visible = false;
            }

            GetStrictSettings();
        }

        public bool IsProjectOnly
        {
            get { return ProjectOnly; }
            set
            {
                ProjectOnly = value;
                Project_Load(this, null);
            }
        }

        public bool IsApproveOnly
        {
            get { return ApproveOnly; }
            set
            {
                ApproveOnly = value;
                //Project_Load(this, null);
            }
        }

        private void GetServers(int UserID)
        {
            int LastX = 20;
            int LastY = 20;
            int LastXSG = 20;
            int MaxY = 0;


            string ServerGroup = string.Empty;
            GroupBox CurGroupBox = null;

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
                            if (row["ServerGroupDesc"].ToString() == ServerGroup)
                            {
                                CheckBox c = new CheckBox();
                                c.Parent = CurGroupBox;
                                c.Name = "c" + row["ServerAliasDesc"].ToString();
                                c.Text = row["ServerAliasDesc"].ToString();
                                c.Left = LastX;
                                c.Top = LastY;
                                LastY = c.Top + c.Height;
                                c.CheckedChanged += CheckBox_Clicked;
                                c.Enabled = false;
                                c.Tag = row["ServerRoleDesc"].ToString();

                                CurGroupBox.Height = LastY;

                                if (MaxY < CurGroupBox.Height)
                                {
                                    MaxY = CurGroupBox.Height + 10;
                                }

                                CurGroupBox.Height = MaxY;

                                switch (c.Tag.ToString())
                                {
                                    case "Development":
                                        c.Enabled = true;
                                        break;

                                    case "User Acceptance Testing":
                                        if (CanReleaseUAT)
                                        {
                                            c.Enabled = true;
                                        }

                                        break;

                                    case "Pre-Production":
                                        if (CanReleasePreProd)
                                        {
                                            c.Enabled = true;
                                        }

                                        break;

                                    case "Production":
                                        if (CanReleaseProd)
                                        {
                                            c.Enabled = true;
                                        }

                                        break;

                                    default:
                                        break;
                                }
                            }
                            else
                            {
                                GroupBox gbServerGroup = new GroupBox();

                                ServerGroup = row["ServerGroupDesc"].ToString();
                                gbServerGroup.Parent = gbEnvironments;
                                gbServerGroup.Left = LastXSG;
                                gbServerGroup.Top = 18;
                                LastY = 20;
                                gbServerGroup.Name = "gbServerGroup" + ServerGroup;
                                LastXSG = gbServerGroup.Left + gbServerGroup.Width + 20;
                                gbServerGroup.Text = ServerGroup;

                                CurGroupBox = gbServerGroup;

                                CheckBox c = new CheckBox();
                                c.Parent = CurGroupBox;
                                c.Name = "c" + row["ServerAliasDesc"].ToString();
                                c.Text = row["ServerAliasDesc"].ToString();
                                c.Left = LastX;
                                c.Top = LastY;
                                LastY = c.Top + c.Height;
                                c.CheckedChanged += CheckBox_Clicked;
                                c.Enabled = false;
                                c.Tag = row["ServerRoleDesc"].ToString();

                                gbServerGroup.Height = LastY + 10;

                                if (MaxY < CurGroupBox.Height)
                                {
                                    MaxY = CurGroupBox.Height + 10;
                                }

                                CurGroupBox.Height = MaxY;

                                switch (c.Tag.ToString())
                                {
                                    case "Development":
                                        c.Enabled = true;
                                        break;

                                    case "User Acceptance Testing":
                                        if (CanReleaseUAT)
                                        {
                                            c.Enabled = true;
                                        }

                                        break;

                                    case "Pre-Production":
                                        if (CanReleasePreProd)
                                        {
                                            c.Enabled = true;
                                        }

                                        break;

                                    case "Production":
                                        if (CanReleaseProd)
                                        {
                                            c.Enabled = true;
                                        }

                                        break;

                                    default:
                                        break;
                                }
                            }

                            cbServers.Items.Add(row["ServerAliasDesc"].ToString());
                        }
                    }

                    gbEnvironments.Height = CurGroupBox.Top + MaxY + 10;

                    LastY = gbEnvironments.Top + gbEnvironments.Height + 5;
                    gbProject.Top = LastY;
                    LastY = gbProject.Top + gbProject.Height + 5;
                    gbGrid.Top = LastY;
                }
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        private void EnableAllEnvironments()
        {
            foreach (Control ctl in gbEnvironments.Controls)
            {
                if (ctl is GroupBox)
                {
                    ((GroupBox)ctl).Enabled = true;
                }
            }
        }

        private void DisableEnvironments()
        {
            foreach (Control ctl in gbEnvironments.Controls)
            {
                if (ctl is GroupBox)
                {
                    ((GroupBox)ctl).Enabled = false;
                }
            }

            foreach (Control OuterCtl in gbEnvironments.Controls)
            {
                if (OuterCtl is GroupBox)
                {
                    foreach (Control InnerCtl in OuterCtl.Controls)
                    {
                        if (InnerCtl is CheckBox)
                        {
                            if (((CheckBox)InnerCtl).Checked)
                            {
                                ((CheckBox)InnerCtl).Parent.Enabled = true;
                            }
                        }
                    }
                }
            }
        }

        private string GetServerGroup()
        {
            string GroupName = string.Empty;

            foreach (Control OuterCtl in gbEnvironments.Controls)
            {
                if (OuterCtl is GroupBox)
                {
                    foreach (Control InnerCtl in OuterCtl.Controls)
                    {
                        if (InnerCtl is CheckBox)
                        {
                            if (((CheckBox)InnerCtl).Checked)
                            {
                                GroupName = ((CheckBox)InnerCtl).Parent.Text;
                            }
                        }
                    }
                }
            }

            return GroupName;
        }

        private int GetServerGroupIDFromName(string GroupName)
        {
            int GroupID = 0;

            using (DataStuff sn = new DataStuff())
            {
                DataTable dt = sn.GetServerGroupID(GroupName);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        GroupID = Convert.ToInt32(row["ServerGroupID"].ToString());

                    }
                }
            }

            return GroupID;
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
                tsbRelease.Enabled = true;
            }
            else
            {
                tsbRelease.Enabled = false;
            }
        }

        private void LoadProjects()
        {
            string GroupName = string.Empty;
            string IncludeInactive = string.Empty;

            try
            {
                cbPartOfProject.Items.Clear();
                cbProject.Items.Clear();

                if (!ProjectOnly)
                {
                    IncludeInactive = "N";
                    GroupName = GetServerGroup();
                }
                else
                {
                    IncludeInactive = "Y";
                    GroupName = GetServerGroupFromAlias(cbServers.Text);
                }

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetProjectsByServerGroup(GroupName, IncludeInactive);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            cbPartOfProject.Items.Add(row["ProjectName"].ToString());
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

        private int AddServer(string ServerAlias)
        {
            if (ReleaseFromServer.Trim() == "")
            {
                ReleaseFromServer = ServerAlias;
                ConnReleaseFromServer = GetServerConnectionString(ServerAlias);
                lReleaseFromServer.Text = "Release from: " + ServerAlias;
                return 1;
            }

            if (ReleaseToServer1.Trim() == "")
            {
                ReleaseToServer1 = ServerAlias;
                ConnReleaseToServer1 = GetServerConnectionString(ServerAlias);
                lReleaseToServer.Text = "Release to: " + ServerAlias;
                return 2;
            }

            if (ReleaseToServer2.Trim() == "")
            {
                ReleaseToServer2 = ServerAlias;
                ConnReleaseToServer2 = GetServerConnectionString(ServerAlias);
                lReleaseToServer.Text = "Release to: " + ReleaseToServer1 + ", " + ServerAlias;
                return 3;
            }

            if (ReleaseToServer3.Trim() == "")
            {
                ReleaseToServer3 = ServerAlias;
                ConnReleaseToServer3 = GetServerConnectionString(ServerAlias);
                lReleaseToServer.Text = "Release to: " + ReleaseToServer1 + ", " + ReleaseToServer2 + ", " + ServerAlias;
                return 4;
            }

            if (ReleaseToServer4.Trim() == "")
            {
                ReleaseToServer4 = ServerAlias;
                ConnReleaseToServer4 = GetServerConnectionString(ServerAlias);
                lReleaseToServer.Text = "Release to: " + ReleaseToServer1 + ", " + ReleaseToServer2 + ", " + ", " + ReleaseToServer3 + ServerAlias;
                return 5;
            }

            return 0;
        }

        private int RemoveServer(string ServerAlias)
        {
            if (ReleaseFromServer.Trim() == ServerAlias)
            {
                ReleaseFromServer = "";
                ConnReleaseFromServer = "";
                MoveServerUp();
            }

            if (ReleaseToServer1.Trim() == ServerAlias)
            {
                ReleaseToServer1 = "";
                ConnReleaseToServer1 = "";
                MoveServerUp();
            }

            if (ReleaseToServer2.Trim() == ServerAlias)
            {
                ReleaseToServer2 = "";
                ConnReleaseToServer2 = "";
                MoveServerUp();
            }

            if (ReleaseToServer3.Trim() == ServerAlias)
            {
                ReleaseToServer3 = "";
                ConnReleaseToServer3 = "";
                MoveServerUp();
            }

            if (ReleaseToServer4.Trim() == ServerAlias)
            {
                ReleaseToServer4 = "";
                ConnReleaseToServer4 = "";
            }

            lReleaseFromServer.Text = "";
            lReleaseToServer.Text = "";

            if (ReleaseFromServer.Trim() != "")
            {
                lReleaseFromServer.Text = "Release from: " + ReleaseFromServer;
            }

            if (ReleaseToServer1.Trim() != "")
            {
                lReleaseToServer.Text = "Release to: " + ReleaseToServer1;
            }

            if (ReleaseToServer2.Trim() != "")
            {
                lReleaseToServer.Text = "Release to: " + ReleaseToServer1 + ", " + ReleaseToServer2;
            }

            if (ReleaseToServer3.Trim() != "")
            {
                lReleaseToServer.Text = "Release to: " + ReleaseToServer1 + ", " + ReleaseToServer2 + ", " + ReleaseToServer3;
            }

            if (ReleaseToServer4.Trim() != "")
            {
                lReleaseToServer.Text = "Release to: " + ReleaseToServer1 + ", " + ReleaseToServer2 + ", " + ReleaseToServer3 + ", " + ReleaseToServer4;
            }

            return ServerCount();
        }

        private int ServerCount()
        {
            int Count = 0;

            if (ReleaseFromServer.Trim() != "")
            {
                Count = 1;
            }

            if (ReleaseToServer1.Trim() != "")
            {
                Count = 2;
            }

            if (ReleaseToServer2.Trim() != "")
            {
                Count = 3;
            }

            if (ReleaseToServer3.Trim() != "")
            {
                Count = 4;
            }

            if (ReleaseToServer4.Trim() != "")
            {
                Count = 5;
            }

            return Count;
        }

        private void MoveServerUp()
        {
            if (ReleaseFromServer.Trim() == "")
            {
                ReleaseFromServer = ReleaseToServer1;
                ConnReleaseFromServer = ConnReleaseToServer1;
                ReleaseToServer1 = "";
                ConnReleaseToServer1 = "";

                if (ReleaseToServer2.Trim() != "")
                {
                    MoveServerUp();
                }
            }

            if (ReleaseToServer1.Trim() == "")
            {
                ReleaseToServer1 = ReleaseToServer2;
                ConnReleaseToServer1 = ConnReleaseToServer2;
                ReleaseToServer2 = "";
                ConnReleaseToServer2 = "";

                if (ReleaseToServer3.Trim() != "")
                {
                    MoveServerUp();
                }
            }

            if (ReleaseToServer2.Trim() == "")
            {
                ReleaseToServer2 = ReleaseToServer3;
                ConnReleaseToServer2 = ConnReleaseToServer3;
                ReleaseToServer3 = "";
                ConnReleaseToServer3 = "";

                if (ReleaseToServer4.Trim() != "")
                {
                    MoveServerUp();
                }
            }

            if (ReleaseToServer3.Trim() == "")
            {
                ReleaseToServer3 = ReleaseToServer4;
                ConnReleaseToServer3 = ConnReleaseToServer4;
                ReleaseToServer4 = "";
                ConnReleaseToServer4 = "";
            }
        }

        private void CheckBox_Clicked(object sender, EventArgs e)
        {
            string ServerAlias = string.Empty;
            int CheckCount = 0;
            string ServerRole = string.Empty;

            IsProductionRelease = false;

            if (((CheckBox)sender).Checked)
            {
                ServerAlias = ((CheckBox)sender).Text;

                CheckCount = AddServer(ServerAlias);

                DisableEnvironments();
            }

            if (!((CheckBox)sender).Checked)
            {
                ServerAlias = ((CheckBox)sender).Text;
                CheckCount = RemoveServer(ServerAlias);
            }

            if (CheckCount == 0)
            {
                DBName = "";
                ObjectName = "";
                DisableToolbar();
                EnableAllEnvironments();
            }

            if (PrevCheckCount > 0)
            {
                if (PrevCheckCount != CheckCount)
                {
                    cbPartOfProject.Enabled = false;
                    cbPartOfProject.Text = "";
                    gbGrid.Controls.Remove(dgvMain);
                }
            }

            if (CheckCount > 1)
            {
                LoadProjects();
                cbPartOfProject.Enabled = true;
                cbPartOfProject.Focus();

                if (ReleaseToServer1.Trim() != "")
                {
                    ServerRole = GetServerRole(ReleaseToServer1, UserID);
                }
                else
                {
                    if (ReleaseToServer2.Trim() != "")
                    {
                        ServerRole = GetServerRole(ReleaseToServer2, UserID);
                    }
                    else
                    {
                        if (ReleaseToServer3.Trim() != "")
                        {
                            ServerRole = GetServerRole(ReleaseToServer3, UserID);
                        }
                        else
                        {
                            if (ReleaseToServer4.Trim() != "")
                            {
                                ServerRole = GetServerRole(ReleaseToServer4, UserID);
                            }
                        }
                    }
                }

                if (ServerRole != "")
                {
                    switch (ServerRole)
                    {
                        case "Production":
                            cbObjectStatus.Text = "Released to Production";
                            IsProductionRelease = true;
                            break;

                        case "User Acceptance Testing":
                            cbObjectStatus.Text = "Released to UAT";
                            break;

                        case "Pre-Production":
                            cbObjectStatus.Text = "Released to Pre-Prod";
                            break;

                        default:
                            break;
                    }
                }
            }
            else
            {
                cbPartOfProject.Enabled = false;
                cbPartOfProject.Text = "";
                dgvMain = null;
            }

            PrevCheckCount = CheckCount;

        }

        private void DisableToolbar()
        {
            tsbAddItem.Enabled = false;
            tsbAddItemFile.Enabled = false;
            tsbDeleteItem.Enabled = false;
            tsbSaveProject.Enabled = false;
            tsbMoveDown.Enabled = false;
            tsbMoveUp.Enabled = false;
            tsbRelease.Enabled = false;
        }

        private void CreateGrid()
        {
            //dgvMain = null;

            if ((cbPartOfProject.Text == "") && (cbProject.Text == ""))
            {
                //gbGrid.Width = this.Width - 40;
                //gbGrid.Height = this.Height - gbGrid.Top - 70;
                //gbEnvironments.Width = gbGrid.Width;
                //gbProject.Width = gbGrid.Width;
                //gbProjectOnly.Width = gbGrid.Width;

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
            dgvMain.Columns.Add("ProjectID", "ProjectID");
            dgvMain.Columns[ColNo].Visible = false;

            ColNo++;
            dgvMain.Columns.Add("ProjectName", "ProjectName");
            dgvMain.Columns[ColNo].Visible = false;

            ColNo++;
            dgvMain.Columns.Add("DatabaseName", "Database Name");
            DBNameCol = ColNo;

            ColNo++;
            dgvMain.Columns.Add("DBObjectName", "Object Name");
            DBObjNameCol = ColNo;

            if (ReleaseFromServer.Trim() != "")
            {
                ColNo++;
                dgvMain.Columns.Add("ReleaseFromDate", ReleaseFromServer);
                ReleaseFromServerCol = ColNo;
            }

            if (ReleaseToServer1.Trim() != "")
            {
                ColNo++;
                dgvMain.Columns.Add("ReleaseToDate1", ReleaseToServer1);
                ReleaseToServer1Col = ColNo;
            }

            if (ReleaseToServer2.Trim() != "")
            {
                ColNo++;
                dgvMain.Columns.Add("ReleaseToDate2", ReleaseToServer2);
                ReleaseToServer2Col = ColNo;
            }

            if (ReleaseToServer3.Trim() != "")
            {
                ColNo++;
                dgvMain.Columns.Add("ReleaseToDate3", ReleaseToServer3);
                ReleaseToServer3Col = ColNo;
            }

            if (ReleaseToServer4.Trim() != "")
            {
                ColNo++;
                dgvMain.Columns.Add("ReleaseToDate4", ReleaseToServer4);
                ReleaseToServer4Col = ColNo;
            }

            ColNo++;
            dgvMain.Columns.Add("FileObjectID", "FileObjectID");
            dgvMain.Columns[ColNo].Visible = false;

            ColNo++;
            dgvMain.Columns.Add("ProjectObjectID", "ProjectObjectID");
            dgvMain.Columns[ColNo].Visible = false;

            ColNo++;
            dgvMain.Columns.Add("ObjectSequence", "Sequence");

            ColNo++;
            DataGridViewCheckBoxColumn dgCheckCell = new DataGridViewCheckBoxColumn();
            dgCheckCell.Name = "ObjectSelected";
            dgvMain.Columns.Add(dgCheckCell);
            dgvMain.Columns[ColNo].HeaderText = "Selected";
            CheckBoxCol = ColNo;

            ColNo++;
            dgvMain.Columns.Add("FileObjectDesc", "File");

            ColNo++;
            dgvMain.Columns.Add("FileObject", "File Text");
            FileTextCol = ColNo;

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
            dgvMain.KeyDown += new KeyEventHandler(dgvMain_KeyDown);
            dgvMain.CellMouseEnter += new DataGridViewCellEventHandler(dgvMain_CellMouseEnter);

            dgvMain.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom);

            //lStatus.Top = dgvMain.Top + dgvMain.Height + 40;
            lStatus.Left = 10;
            lStatus.Text = "";
        }


        private void LoadGrid()
        {
            int RowCounter = 1;
            string ConnStr = string.Empty;
            string DateModFrom = string.Empty;
            string DateModTo1 = string.Empty;
            string DateModTo2 = string.Empty;
            string DateModTo3 = string.Empty;
            string DateModTo4 = string.Empty;
            string ObjName = string.Empty;
            string DBName = string.Empty;
            int ServerCount = 0;
            string ProjectActive = string.Empty;

            if ((cbPartOfProject.Text == "") && (cbProject.Text == ""))
            {
                return;
            }

            dgvMain.Rows.Clear();

            using (DataStuff sn = new DataStuff())
            {
                string ProjectName = string.Empty;

                if (ProjectOnly)
                {
                    ProjectName = cbProject.Text;

                    if (ProjectName.Trim() == "")
                    {
                        ProjectActive = "Y";
                    }
                    else
                    {
                        ProjectActive = sn.IsProjectActive(ProjectName);
                    }

                    if (ProjectActive.Trim() == "Y")
                    {
                        rbActiveYes.Checked = true;
                    }
                    else
                    {
                        rbActiveNo.Checked = true;
                    }
                }
                else
                {
                    ProjectName = cbPartOfProject.Text;
                }

                DataTable dt = sn.GetProjectObjects(ProjectName);

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["ObjectStatusDesc"].ToString().Trim() != "")
                    {
                        cbObjectStatus.Text = dt.Rows[0]["ObjectStatusDesc"].ToString();
                        tComment.Text = dt.Rows[0]["Comment"].ToString();
                    }
                    else
                    {
                        cbObjectStatus.Text = "";
                        tComment.Text = dt.Rows[0]["Comment"].ToString();
                    }

                    if (tComment.Text.Trim() == "")
                    {
                        tComment.Text = dt.Rows[0]["ProjectDesc"].ToString();
                    }

                    tComment.Text = gs.RemoveSpecialCharacters(tComment.Text);

                    foreach (DataRow row in dt.Rows)
                    {
                        if (ReleaseToServer4.Trim() != "")
                        {
                            ServerCount = 5;
                            ConnStr = GetServerConnectionString(ReleaseToServer4);
                            ObjName = row["DBObjectName"].ToString();
                            DBName = row["DBName"].ToString();

                            DateModTo4 = LoadObjectDetail(ConnStr, DBName, "", ObjName);
                            ConnStr = "";

                            ConnStr = GetServerConnectionString(ReleaseToServer3);
                            DateModTo3 = LoadObjectDetail(ConnStr, DBName, "", ObjName);
                            ConnStr = "";

                            ConnStr = GetServerConnectionString(ReleaseToServer2);
                            DateModTo2 = LoadObjectDetail(ConnStr, DBName, "", ObjName);
                            ConnStr = "";

                            ConnStr = GetServerConnectionString(ReleaseToServer1);
                            DateModTo1 = LoadObjectDetail(ConnStr, DBName, "", ObjName);
                            ConnStr = "";

                            ConnStr = GetServerConnectionString(ReleaseFromServer);
                            DateModFrom = LoadObjectDetail(ConnStr, DBName, "", ObjName);

                            dgvMain.Rows.Add(row["ProjectID"].ToString(),
                                             row["ProjectName"].ToString(),
                                             row["DBName"].ToString(),
                                             row["DBObjectName"].ToString(),
                                             DateModFrom,
                                             DateModTo1,
                                             DateModTo2,
                                             DateModTo3,
                                             DateModTo4,
                                             row["FileObjectID"].ToString(),
                                             row["ProjectObjectID"].ToString(),
                                             row["ObjectSequence"].ToString() == "0" ? RowCounter.ToString() : row["ObjectSequence"].ToString(),
                                             row["ObjectSelected"].ToString() == "Y" ? true : false,
                                             row["ObjectDescription"].ToString(),
                                             row["FileObject"].ToString());
                        }
                        else
                        {
                            if (ReleaseToServer3.Trim() != "")
                            {
                                ServerCount = 4;
                                ObjName = row["DBObjectName"].ToString();
                                DBName = row["DBName"].ToString();

                                ConnStr = GetServerConnectionString(ReleaseToServer3);
                                DateModTo3 = LoadObjectDetail(ConnStr, DBName, "", ObjName);
                                ConnStr = "";

                                ConnStr = GetServerConnectionString(ReleaseToServer2);
                                DateModTo2 = LoadObjectDetail(ConnStr, DBName, "", ObjName);
                                ConnStr = "";

                                ConnStr = GetServerConnectionString(ReleaseToServer1);
                                DateModTo1 = LoadObjectDetail(ConnStr, DBName, "", ObjName);
                                ConnStr = "";

                                ConnStr = GetServerConnectionString(ReleaseFromServer);
                                DateModFrom = LoadObjectDetail(ConnStr, DBName, "", ObjName);

                                dgvMain.Rows.Add(row["ProjectID"].ToString(),
                                                 row["ProjectName"].ToString(),
                                                 row["DBName"].ToString(),
                                                 row["DBObjectName"].ToString(),
                                                 DateModFrom,
                                                 DateModTo1,
                                                 DateModTo2,
                                                 DateModTo3,
                                                 row["FileObjectID"].ToString(),
                                                 row["ProjectObjectID"].ToString(),
                                                 row["ObjectSequence"].ToString() == "0" ? RowCounter.ToString() : row["ObjectSequence"].ToString(),
                                                 row["ObjectSelected"].ToString() == "Y" ? true : false,
                                                 row["ObjectDescription"].ToString(),
                                                 row["FileObject"].ToString());
                            }
                            else
                            {
                                if (ReleaseToServer2.Trim() != "")
                                {
                                    ServerCount = 3;

                                    ObjName = row["DBObjectName"].ToString();
                                    DBName = row["DBName"].ToString();

                                    ConnStr = GetServerConnectionString(ReleaseToServer2);
                                    DateModTo2 = LoadObjectDetail(ConnStr, DBName, "", ObjName);
                                    ConnStr = "";

                                    ConnStr = GetServerConnectionString(ReleaseToServer1);
                                    DateModTo1 = LoadObjectDetail(ConnStr, DBName, "", ObjName);
                                    ConnStr = "";

                                    ConnStr = GetServerConnectionString(ReleaseFromServer);
                                    DateModFrom = LoadObjectDetail(ConnStr, DBName, "", ObjName);

                                    dgvMain.Rows.Add(row["ProjectID"].ToString(),
                                                     row["ProjectName"].ToString(),
                                                     row["DBName"].ToString(),
                                                     row["DBObjectName"].ToString(),
                                                     DateModFrom,
                                                     DateModTo1,
                                                     DateModTo2,
                                                     row["FileObjectID"].ToString(),
                                                     row["ProjectObjectID"].ToString(),
                                                     row["ObjectSequence"].ToString() == "0" ? RowCounter.ToString() : row["ObjectSequence"].ToString(),
                                                     row["ObjectSelected"].ToString() == "Y" ? true : false,
                                                     row["ObjectDescription"].ToString(),
                                                     row["FileObject"].ToString());
                                }
                                else
                                {
                                    if (ReleaseToServer1.Trim() != "")
                                    {
                                        ServerCount = 2;

                                        ObjName = row["DBObjectName"].ToString();
                                        DBName = row["DBName"].ToString();

                                        ConnStr = GetServerConnectionString(ReleaseToServer1);
                                        DateModTo1 = LoadObjectDetail(ConnStr, DBName, "", ObjName);
                                        ConnStr = "";

                                        ConnStr = GetServerConnectionString(ReleaseFromServer);
                                        DateModFrom = LoadObjectDetail(ConnStr, DBName, "", ObjName);

                                        dgvMain.Rows.Add(row["ProjectID"].ToString(),
                                                         row["ProjectName"].ToString(),
                                                         row["DBName"].ToString(),
                                                         row["DBObjectName"].ToString(),
                                                         DateModFrom,
                                                         DateModTo1,
                                                         row["FileObjectID"].ToString(),
                                                         row["ProjectObjectID"].ToString(),
                                                         row["ObjectSequence"].ToString() == "0" ? RowCounter.ToString() : row["ObjectSequence"].ToString(),
                                                         row["ObjectSelected"].ToString() == "Y" ? true : false,
                                                         row["ObjectDescription"].ToString(),
                                                         row["FileObject"].ToString());
                                    }
                                    else
                                    {
                                        ServerCount = 1;

                                        ObjName = row["DBObjectName"].ToString();
                                        DBName = row["DBName"].ToString();

                                        ConnStr = GetServerConnectionString(ReleaseFromServer);
                                        DateModFrom = LoadObjectDetail(ConnStr, DBName, "", ObjName);

                                        dgvMain.Rows.Add(row["ProjectID"].ToString(),
                                                         row["ProjectName"].ToString(),
                                                         row["DBName"].ToString(),
                                                         row["DBObjectName"].ToString(),
                                                         DateModFrom,
                                                         row["FileObjectID"].ToString(),
                                                         row["ProjectObjectID"].ToString(),
                                                         row["ObjectSequence"].ToString() == "0" ? RowCounter.ToString() : row["ObjectSequence"].ToString(),
                                                         row["ObjectSelected"].ToString() == "Y" ? true : false,
                                                         row["ObjectDescription"].ToString(),
                                                         row["FileObject"].ToString());
                                    }
                                }
                            }
                        }

                        if (row["ObjectSelected"].ToString() == "Y")
                        {
                            tsbRelease.Enabled = true;
                        }

                        RowCounter++;
                    }

                    tsbDeleteItem.Enabled = true;

                    if ((!ProjectOnly) || (ApproveOnly))
                    {
                        SetDateModifiedColour(ServerCount);
                    }
                }

                if (dt.Rows.Count > 1)
                {
                    tsbMoveDown.Enabled = true;
                    tsbMoveUp.Enabled = true;
                }
            }

            if (!ApproveOnly)
            {
                tsbAddItem.Enabled = true;
                tsbAddItemFile.Enabled = true;
                tsbSaveProject.Enabled = true;
            }
            else
            {
                DisableToolbar();
            }
        }

        private void LoadGridRow(string DBName, string ObjName)
        {
            string ConnStr = string.Empty;
            string DateModFrom = string.Empty;
            string DateModTo1 = string.Empty;
            string DateModTo2 = string.Empty;
            string DateModTo3 = string.Empty;
            string DateModTo4 = string.Empty;

            int ServerCount = 0;

            if (ReleaseToServer4.Trim() != "")
            {
                ServerCount = 5;
                ConnStr = GetServerConnectionString(ReleaseToServer4);

                DateModTo4 = LoadObjectDetail(ConnStr, DBName, "", ObjName);
                ConnStr = "";

                ConnStr = GetServerConnectionString(ReleaseToServer3);
                DateModTo3 = LoadObjectDetail(ConnStr, DBName, "", ObjName);
                ConnStr = "";

                ConnStr = GetServerConnectionString(ReleaseToServer2);
                DateModTo2 = LoadObjectDetail(ConnStr, DBName, "", ObjName);
                ConnStr = "";

                ConnStr = GetServerConnectionString(ReleaseToServer1);
                DateModTo1 = LoadObjectDetail(ConnStr, DBName, "", ObjName);
                ConnStr = "";

                ConnStr = GetServerConnectionString(ReleaseFromServer);
                DateModFrom = LoadObjectDetail(ConnStr, DBName, "", ObjName);

                dgvMain.Rows.Add("", "", DBName, ObjName, DateModFrom, DateModTo1, DateModTo2, DateModTo3, DateModTo4, "", "", "99", true, "", "");
            }
            else
            {
                if (ReleaseToServer3.Trim() != "")
                {
                    ServerCount = 4;

                    ConnStr = GetServerConnectionString(ReleaseToServer3);
                    DateModTo3 = LoadObjectDetail(ConnStr, DBName, "", ObjName);
                    ConnStr = "";

                    ConnStr = GetServerConnectionString(ReleaseToServer2);
                    DateModTo2 = LoadObjectDetail(ConnStr, DBName, "", ObjName);
                    ConnStr = "";

                    ConnStr = GetServerConnectionString(ReleaseToServer1);
                    DateModTo1 = LoadObjectDetail(ConnStr, DBName, "", ObjName);
                    ConnStr = "";

                    ConnStr = GetServerConnectionString(ReleaseFromServer);
                    DateModFrom = LoadObjectDetail(ConnStr, DBName, "", ObjName);

                    dgvMain.Rows.Add("", "", DBName, ObjName, DateModFrom, DateModTo1, DateModTo2, DateModTo3, "", "", "99", true, "", "");
                }
                else
                {
                    if (ReleaseToServer2.Trim() != "")
                    {
                        ServerCount = 3;

                        ConnStr = GetServerConnectionString(ReleaseToServer2);
                        DateModTo2 = LoadObjectDetail(ConnStr, DBName, "", ObjName);
                        ConnStr = "";

                        ConnStr = GetServerConnectionString(ReleaseToServer1);
                        DateModTo1 = LoadObjectDetail(ConnStr, DBName, "", ObjName);
                        ConnStr = "";

                        ConnStr = GetServerConnectionString(ReleaseFromServer);
                        DateModFrom = LoadObjectDetail(ConnStr, DBName, "", ObjName);

                        dgvMain.Rows.Add("", "", DBName, ObjName, DateModFrom, DateModTo1, DateModTo2, "", "", "99", true, "", "");
                    }
                    else
                    {
                        if (ReleaseToServer1.Trim() != "")
                        {
                            ServerCount = 2;

                            ConnStr = GetServerConnectionString(ReleaseToServer1);
                            DateModTo1 = LoadObjectDetail(ConnStr, DBName, "", ObjName);
                            ConnStr = "";

                            ConnStr = GetServerConnectionString(ReleaseFromServer);
                            DateModFrom = LoadObjectDetail(ConnStr, DBName, "", ObjName);

                            dgvMain.Rows.Add("", "", DBName, ObjName, DateModFrom, DateModTo1, "", "", "99", true, "", "");
                        }
                        else
                        {
                            ServerCount = 1;

                            ConnStr = GetServerConnectionString(ReleaseFromServer);
                            DateModFrom = LoadObjectDetail(ConnStr, DBName, "", ObjName);

                            dgvMain.Rows.Add("", "", DBName, ObjName, DateModFrom, "", "", "99", true, "", "");
                        }
                    }
                }
            }

            tsbRelease.Enabled = true;
            tsbDeleteItem.Enabled = true;
            SetDateModifiedColour(ServerCount);

            if (dgvMain.RowCount > 1)
            {
                tsbMoveDown.Enabled = true;
                tsbMoveUp.Enabled = true;
            }

            if (!ApproveOnly)
            {
                tsbAddItem.Enabled = true;
                tsbAddItemFile.Enabled = true;
                tsbSaveProject.Enabled = true;
                ProjectHasChanged = true;
            }
            else
            {
                DisableToolbar();
            }

            if (ProjectSaved != null)
            {
                ProjectSaved(this, null);
            }
        }

        private void LoadGridRowFile(string FileName, string FileText)
        {
            if (ReleaseToServer4.Trim() != "")
            {
                dgvMain.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "99", true, FileName, FileText);
            }
            else
            {
                if (ReleaseToServer3.Trim() != "")
                {
                    dgvMain.Rows.Add("", "", "", "", "", "", "", "", "", "", "99", true, FileName, FileText);
                }
                else
                {
                    if (ReleaseToServer2.Trim() != "")
                    {
                        dgvMain.Rows.Add("", "", "", "", "", "", "", "", "", "99", true, FileName, FileText);
                    }
                    else
                    {
                        if (ReleaseToServer1.Trim() != "")
                        {
                            dgvMain.Rows.Add("", "", "", "", "", "", "", "", "99", true, FileName, FileText);
                        }
                        else
                        {
                            dgvMain.Rows.Add("", "", "", "", "", "", "", "99", true, FileName, FileText);
                        }
                    }
                }
            }

            tsbRelease.Enabled = true;
            tsbDeleteItem.Enabled = true;

            if (dgvMain.RowCount > 1)
            {
                tsbMoveDown.Enabled = true;
                tsbMoveUp.Enabled = true;
            }

            if (!ApproveOnly)
            {
                tsbAddItem.Enabled = true;
                tsbAddItemFile.Enabled = true;
                tsbSaveProject.Enabled = true;
            }
            else
            {
                DisableToolbar();
            }
        }

        private void SetDateModifiedColour(int Cols)
        {
            DateTime BaseDate = DateTime.Now;
            DateTime RelTo1 = DateTime.Now;
            DateTime RelTo2 = DateTime.Now;
            bool DateError = false;

            if (Cols == 2)
            {
                foreach (DataGridViewRow row in dgvMain.Rows)
                {
                    DateError = false;

                    try
                    {
                        BaseDate = Convert.ToDateTime(row.Cells["ReleaseFromDate"].Value);
                        RelTo1 = Convert.ToDateTime(row.Cells["ReleaseToDate1"].Value);
                    }

                    catch
                    {
                        DateError = true;
                    }

                    if (!DateError)
                    {
                        if (Math.Abs((BaseDate - RelTo1).Minutes) < 5)
                        {
                            row.Cells["ReleaseFromDate"].Style.BackColor = AllOkay;
                            row.Cells["ReleaseToDate1"].Style.BackColor = AllOkay;
                        }
                        else
                        {
                            if (BaseDate < RelTo1)
                            {
                                row.Cells["ReleaseFromDate"].Style.BackColor = Danger;
                                row.Cells["ReleaseToDate1"].Style.BackColor = Danger;
                            }

                            if (BaseDate > RelTo1)
                            {
                                row.Cells["ReleaseFromDate"].Style.BackColor = Warn;
                                row.Cells["ReleaseToDate1"].Style.BackColor = Warn;
                            }
                        }
                    }
                }
            }

            if (Cols == 3)
            {
                foreach (DataGridViewRow row in dgvMain.Rows)
                {
                    DateError = false;

                    try
                    {
                        BaseDate = Convert.ToDateTime(row.Cells["ReleaseFromDate"].Value);
                        RelTo1 = Convert.ToDateTime(row.Cells["ReleaseToDate1"].Value);
                        RelTo2 = Convert.ToDateTime(row.Cells["ReleaseToDate2"].Value);
                    }

                    catch
                    {
                        DateError = true;
                    }

                    if (!DateError)
                    {
                        if ((Math.Abs((BaseDate - RelTo1).Minutes) < 5) && (Math.Abs((BaseDate - RelTo2).Minutes) < 5))
                        {
                            row.Cells["ReleaseFromDate"].Style.BackColor = AllOkay;
                            row.Cells["ReleaseToDate1"].Style.BackColor = AllOkay;
                            row.Cells["ReleaseToDate2"].Style.BackColor = AllOkay;
                        }
                        else
                        {
                            if ((BaseDate < RelTo1) || (BaseDate < RelTo2))
                            {
                                row.Cells["ReleaseFromDate"].Style.BackColor = Danger;
                                row.Cells["ReleaseToDate1"].Style.BackColor = Danger;
                                row.Cells["ReleaseToDate2"].Style.BackColor = Danger;
                            }

                            if ((BaseDate > RelTo1) || (BaseDate > RelTo2))
                            {
                                row.Cells["ReleaseFromDate"].Style.BackColor = Warn;
                                row.Cells["ReleaseToDate1"].Style.BackColor = Warn;
                                row.Cells["ReleaseToDate2"].Style.BackColor = Warn;
                            }
                        }
                    }
                }
            }
        }

        private void ReCreateGrid()
        {
            string ServerRole = string.Empty;

            CreateGrid();
            LoadGrid();

            if (ReleaseToServer1.Trim() != "")
            {
                ServerRole = GetServerRole(ReleaseToServer1, UserID);
            }
            else
            {
                if (ReleaseToServer2.Trim() != "")
                {
                    ServerRole = GetServerRole(ReleaseToServer2, UserID);
                }
                else
                {
                    if (ReleaseToServer3.Trim() != "")
                    {
                        ServerRole = GetServerRole(ReleaseToServer3, UserID);
                    }
                    else
                    {
                        if (ReleaseToServer4.Trim() != "")
                        {
                            ServerRole = GetServerRole(ReleaseToServer4, UserID);
                        }
                    }
                }
            }

            if (ServerRole != "")
            {
                switch (ServerRole)
                {
                    case "Production":
                        cbObjectStatus.Text = "Released to Production";
                        break;

                    case "User Acceptance Testing":
                        cbObjectStatus.Text = "Released to UAT";
                        break;

                    case "Pre-Production":
                        cbObjectStatus.Text = "Released to Pre-Prod";
                        break;

                    default:
                        break;
                }
            }

            ProjectLoaded?.Invoke(new ProjectEventArgs(cbServers.Text, cbProject.Text));
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

        private void dgvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Up))
            {
                moveUp();
            }
            if (e.KeyCode.Equals(Keys.Down))
            {
                moveDown();
            }

            e.Handled = true;
        }

        private void moveUp()
        {
            if (dgvMain.RowCount > 0)
            {
                if (dgvMain.SelectedRows.Count > 0)
                {
                    int rowCount = dgvMain.Rows.Count;
                    int index = dgvMain.SelectedCells[0].OwningRow.Index;

                    if (index == 0)
                    {
                        return;
                    }
                    DataGridViewRowCollection rows = dgvMain.Rows;

                    // remove the previous row and add it behind the selected row.

                    DataGridViewRow prevRow = rows[index - 1];
                    rows.Remove(prevRow);
                    prevRow.Frozen = false;
                    rows.Insert(index, prevRow);
                    dgvMain.ClearSelection();
                    dgvMain.Rows[index - 1].Selected = true;
                }

                SetSequence();
            }
        }

        private void moveDown()
        {
            if (dgvMain.RowCount > 0)
            {
                if (dgvMain.SelectedRows.Count > 0)
                {
                    int rowCount = dgvMain.Rows.Count;
                    int index = dgvMain.SelectedCells[0].OwningRow.Index;

                    if (index == (rowCount - 1))
                    {
                        return;
                    }
                    DataGridViewRowCollection rows = dgvMain.Rows;

                    // remove the next row and add it in front of the selected row.

                    DataGridViewRow nextRow = rows[index + 1];
                    rows.Remove(nextRow);
                    nextRow.Frozen = false;
                    rows.Insert(index, nextRow);
                    dgvMain.ClearSelection();
                    dgvMain.Rows[index + 1].Selected = true;
                }

                SetSequence();
            }
        }

        private void dgvMain_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == CheckBoxCol && e.RowIndex != -1)
            {
                IsItemSelected();
            }
        }

        private void dgvMain_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvMain.IsCurrentCellDirty)
            {
                dgvMain.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void SetSequence()
        {
            int Sequence = 1;

            foreach (DataGridViewRow row in dgvMain.Rows)
            {
                row.Cells["ObjectSequence"].Value = Sequence;
                Sequence++;
            }

            ProjectHasChanged = true;

            if (ProjectSaved != null)
            {
                ProjectSaved(this, null);
            }
        }

        public bool DoSaveProject()
        {
            return SaveProject(true);
        }

        private bool SaveProject(bool ShowMessage)
        {
            string ProjectName = string.Empty;
            string FileObject = string.Empty;
            string FileObjectDesc = string.Empty;
            string DBName = string.Empty;
            string DBObjectName = string.Empty;
            int ObjectSequence = 0;
            string ObjectSelected = string.Empty;
            string DoDelete = string.Empty;
            int RowCounter = 0;
            string TmpVal = string.Empty;
            string ProjectActive = string.Empty;

            if (!ProjectOnly)
            {
                SaveProjectStatus();
                ProjectName = cbPartOfProject.Text;
            }
            else
            {
                ProjectName = cbProject.Text;
            }

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    foreach (DataGridViewRow row in dgvMain.Rows)
                    {
                        if (RowCounter == 0)
                        {
                            DoDelete = "Y";
                        }
                        else
                        {
                            DoDelete = "N";
                        }

                        FileObject = (string)row.Cells["FileObject"].Value;
                        FileObjectDesc = (string)row.Cells["FileObjectDesc"].Value;
                        DBName = (string)row.Cells["DatabaseName"].Value;
                        DBObjectName = (string)row.Cells["DBObjectName"].Value;
                        //TmpVal = row.Cells["ObjectSequence"].Value;

                        ObjectSequence = int.Parse(row.Cells["ObjectSequence"].Value.ToString());

                        DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)row.Cells["ObjectSelected"];
                        checkCell.TrueValue = true;

                        if (Convert.ToBoolean(checkCell.Value) == Convert.ToBoolean(checkCell.TrueValue))
                        {
                            ObjectSelected = "Y";
                        }
                        else
                        {
                            ObjectSelected = "N";
                        }

                        if (rbActiveYes.Checked)
                        {
                            ProjectActive = "Y";
                        }
                        else
                        {
                            ProjectActive = "N";
                        }

                        sn.SaveProjectObject(ProjectName, FileObject, FileObjectDesc, DBName, DBObjectName, ObjectSequence, ObjectSelected, UserID, DoDelete, ProjectActive);

                        RowCounter++;
                    }
                }

                if (ShowMessage)
                {
                    MessageBox.Show("Detail records saved.", "Save Project Detail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                ProjectHasChanged = false;

                if (ProjectSaved != null)
                {
                    ProjectSaved(this, null);
                }

                return true;
            }

            catch (Exception ex)
            {
                if (ShowMessage)
                {
                    MessageBox.Show("Error saving the detail - " + ex.Message, "Save Project Detail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return false;
            }
        }

        private void SaveProjectStatus()
        {
            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    sn.SaveProjectStatus(cbPartOfProject.Text, cbObjectStatus.Text, gs.RemoveSpecialCharacters(tComment.Text), UserID);
                }
            }

            catch
            {
                return;
            }
        }

        private void dgvMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string ReturnText = string.Empty;
            string DBName = string.Empty;
            string ObjName = string.Empty;

            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }

            if ((ProjectOnly) && (!ApproveOnly))
            {
                if (e.ColumnIndex == ReleaseFromServerCol && e.RowIndex != -1)
                {
                    try
                    {
                        ObjectViewer ov = new ObjectViewer();
                        
                        ov.ConnectionString = GetServerConnectionString(ReleaseFromServer);
                        ov.ObjectDescription = dgvMain[DBObjNameCol, e.RowIndex].Value.ToString();
                        ov.ObjectText = "";
                        ov.DatabaseName = dgvMain[DBNameCol, e.RowIndex].Value.ToString();
                        ov.ObjectName = dgvMain[DBObjNameCol, e.RowIndex].Value.ToString();
                        ov.AliasName = ReleaseFromServer;

                        ov.ShowDialog();

                        return;
                    }

                    catch
                    {
                        return;
                    }
                }

                if (e.ColumnIndex == FileTextCol && e.RowIndex != -1)
                {
                    try
                    {
                        ScriptFileView sf = new ScriptFileView();

                        sf.FileText = Convert.ToString(dgvMain[e.ColumnIndex, e.RowIndex].Value);

                        sf.ShowDialog();

                        ReturnText = sf.FileText;

                        dgvMain[e.ColumnIndex, e.RowIndex].Value = ReturnText;

                        return;
                    }

                    catch
                    {
                        return;
                    }
                }

                return;
            }

            if (e.ColumnIndex == CheckBoxCol && e.RowIndex != -1)
            {
                ProjectHasChanged = true;

                if (ProjectSaved != null)
                {
                    ProjectSaved(this, null);
                }

                return;
            }

            if (e.ColumnIndex == FileTextCol && e.RowIndex != -1)
            {
                ScriptFileView sf = new ScriptFileView();

                sf.FileText = Convert.ToString(dgvMain[e.ColumnIndex, e.RowIndex].Value);

                sf.ShowDialog();

                ReturnText = sf.FileText;

                dgvMain[e.ColumnIndex, e.RowIndex].Value = ReturnText;

                return;
            }

            if (e.ColumnIndex == ReleaseFromServerCol && e.RowIndex != -1)
            {
                try
                {
                    DBName = (string)dgvMain[DBNameCol, e.RowIndex].Value;
                    ObjName = (string)dgvMain[DBObjNameCol, e.RowIndex].Value;

                    if ((DBName.Trim() != "") && (ObjName.Trim() != ""))
                    {
                        CompareFiles(ReleaseFromServer, DBName, ObjName);
                    }

                    return;
                }

                catch
                {
                    return;
                }
            }

            if (e.ColumnIndex == ReleaseToServer1Col && e.RowIndex != -1)
            {
                try
                {
                    DBName = (string)dgvMain[DBNameCol, e.RowIndex].Value;
                    ObjName = (string)dgvMain[DBObjNameCol, e.RowIndex].Value;

                    if ((DBName.Trim() != "") && (ObjName.Trim() != ""))
                    {
                        CompareFiles(ReleaseToServer1, DBName, ObjName);
                    }

                    return;
                }

                catch
                {
                    return;
                }
            }

            if (e.ColumnIndex == ReleaseToServer2Col && e.RowIndex != -1)
            {
                try
                {
                    DBName = (string)dgvMain[DBNameCol, e.RowIndex].Value;
                    ObjName = (string)dgvMain[DBObjNameCol, e.RowIndex].Value;

                    if ((DBName.Trim() != "") && (ObjName.Trim() != ""))
                    {
                        CompareFiles(ReleaseToServer2, DBName, ObjName);
                    }

                    return;
                }

                catch
                {
                    return;
                }
            }

            if (e.ColumnIndex == ReleaseToServer3Col && e.RowIndex != -1)
            {
                try
                {
                    DBName = (string)dgvMain[DBNameCol, e.RowIndex].Value;
                    ObjName = (string)dgvMain[DBObjNameCol, e.RowIndex].Value;

                    if ((DBName.Trim() != "") && (ObjName.Trim() != ""))
                    {
                        CompareFiles(ReleaseToServer3, DBName, ObjName);
                    }

                    return;
                }

                catch
                {
                    return;
                }
            }

            if (e.ColumnIndex == ReleaseToServer4Col && e.RowIndex != -1)
            {
                try
                {
                    DBName = (string)dgvMain[DBNameCol, e.RowIndex].Value;
                    ObjName = (string)dgvMain[DBObjNameCol, e.RowIndex].Value;

                    if ((DBName.Trim() != "") && (ObjName.Trim() != ""))
                    {
                        CompareFiles(ReleaseToServer4, DBName, ObjName);
                    }

                    return;
                }

                catch
                {
                    return;
                }
            }
        }

        private void dgvMain_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            //Skip the Column and Row headers

            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }

            var dataGridView = (sender as DataGridView);

            if (ProjectOnly)
            {
                try
                {
                    if ((e.ColumnIndex == FileTextCol))
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

            try
            {
                if ((e.ColumnIndex == ReleaseFromServerCol) || (e.ColumnIndex == ReleaseToServer1Col) || (e.ColumnIndex == ReleaseToServer2Col) || (e.ColumnIndex == ReleaseToServer3Col) || (e.ColumnIndex == ReleaseToServer4Col))
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

            try
            {
                if ((e.ColumnIndex == FileTextCol))
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

        private void SetStatus(string StatusText)
        {
            lStatus.Text = StatusText;
            Application.DoEvents();
        }

        private bool DeployObject(string ConnectionString, string DatabaseName, string ObjectName, string ObjectText)
        {
            string RetVal = string.Empty;
            string RetValSMO = string.Empty;
            string ServerRole = string.Empty;

            if (cbObjectStatus.Text == "")
            {
                MessageBox.Show("Please select the object's status.", "Change Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (tComment.Text == "")
            {
                MessageBox.Show("Please enter a comment for this change.", "Change Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                if (!BackupObject(ConnectionString, DatabaseName, ObjectName, ObjectText))
                {
                    return false;
                }

                if (ReleaseToServer1.Trim() != "")
                {
                    if (!BackupReleaseToServerObject(ReleaseToServer1, DatabaseName, ObjectName))
                    {
                        return false;
                    }

                    if (ReleaseToServer2.Trim() != "")
                    {
                        if (!BackupReleaseToServerObject(ReleaseToServer2, DatabaseName, ObjectName))
                        {
                            return false;
                        }

                        if (ReleaseToServer3.Trim() != "")
                        {
                            if (!BackupReleaseToServerObject(ReleaseToServer3, DatabaseName, ObjectName))
                            {
                                return false;
                            }

                            if (ReleaseToServer4.Trim() != "")
                            {
                                if (!BackupReleaseToServerObject(ReleaseToServer4, DatabaseName, ObjectName))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }

            catch
            {
                MessageBox.Show("An error occurred while creating the backup record.", "Backup Object", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("An error occurred while creating the backup record.");

                return false;
            }

            try
            {
                SetStatus("Adding object comment...");


                ObjectText = AddProcComment(ObjectText);
                ObjectText = gs.ChangeCreateToAlter(ObjectText);

                ServerRole = GetServerRole(ReleaseFromServer, UserID);

                //Only create a backup of the release-from server if it is not production. Developers do not have permissions to change objects in production...

                if (ServerRole.Trim() != "Production")
                {
                    using (DataStuff sn = new DataStuff())
                    {
                        ConnectionString = ConnectionString + ";Database=" + DatabaseName;

                        try
                        {
                            RetVal = sn.CreateObjectS(ConnectionString, ObjectText);

                            if (RetVal != "")
                            {
                                RetValSMO = sn.CreateObjectSMO(ConnectionString, ObjectText);

                                if (RetValSMO != "")
                                {
                                    MessageBox.Show("Failed - " + RetVal, "Modify Procedure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return false;
                                }
                            }
                        }

                        catch
                        {

                        }
                    }
                }
            }

            catch
            {
                return false;
            }

            try
            {
                if (ReleaseToServer1.Trim() != "")
                {
                    using (DataStuff sn = new DataStuff())
                    {
                        string ConnectionStr = string.Empty;

                        ConnectionStr = GetServerConnectionString(ReleaseToServer1, UserID);

                        ConnectionStr = ConnectionStr + ";Database=" + DatabaseName;

                        if (IsNewObject)
                        {
                            ObjectText = gs.ChangeAlterToCreate(ObjectText);
                        }

                        SetStatus("Deploying object " + DatabaseName + " - " + ObjectName + " to " + ReleaseToServer1 + "...");

                        RetVal = sn.CreateObjectS(ConnectionStr, ObjectText);

                        if (RetVal != "")
                        {
                            RetValSMO = sn.CreateObjectSMO(ConnectionStr, ObjectText);

                            if (RetValSMO != "")
                            {
                                MessageBox.Show("Failed - " + RetVal, "Modify Procedure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                SetStatus("Error deploying object " + DatabaseName + " - " + ObjectName + " to " + ReleaseToServer1 + "...");
                                return false;
                            }
                        }
                    }

                    if (ReleaseToServer2.Trim() != "")
                    {
                        using (DataStuff sn = new DataStuff())
                        {
                            string ConnectionStr = string.Empty;

                            ConnectionStr = GetServerConnectionString(ReleaseToServer2, UserID);

                            ConnectionStr = ConnectionStr + ";Database=" + DatabaseName;

                            if (IsNewObject)
                            {
                                ObjectText = gs.ChangeAlterToCreate(ObjectText);
                            }

                            SetStatus("Deploying object " + DatabaseName + " - " + ObjectName + " to " + ReleaseToServer2 + "...");

                            RetVal = sn.CreateObjectS(ConnectionStr, ObjectText);

                            if (RetVal != "")
                            {
                                RetValSMO = sn.CreateObjectSMO(ConnectionStr, ObjectText);

                                if (RetValSMO != "")
                                {
                                    MessageBox.Show("Failed - " + RetVal, "Modify Procedure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    SetStatus("Error deploying object " + DatabaseName + " - " + ObjectName + " to " + ReleaseToServer2 + "...");
                                    return false;
                                }
                            }
                        }

                        if (ReleaseToServer3.Trim() != "")
                        {
                            using (DataStuff sn = new DataStuff())
                            {
                                string ConnectionStr = string.Empty;

                                ConnectionStr = GetServerConnectionString(ReleaseToServer3, UserID);

                                ConnectionStr = ConnectionStr + ";Database=" + DatabaseName;

                                if (IsNewObject)
                                {
                                    ObjectText = gs.ChangeAlterToCreate(ObjectText);
                                }

                                SetStatus("Deploying object " + DatabaseName + " - " + ObjectName + " to " + ReleaseToServer3 + "...");

                                RetVal = sn.CreateObjectS(ConnectionStr, ObjectText);

                                if (RetVal != "")
                                {
                                    RetValSMO = sn.CreateObjectSMO(ConnectionStr, ObjectText);

                                    if (RetValSMO != "")
                                    {
                                        MessageBox.Show("Failed - " + RetVal, "Modify Procedure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        SetStatus("Error deploying object " + DatabaseName + " - " + ObjectName + " to " + ReleaseToServer3 + "...");
                                        return false;
                                    }
                                }
                            }

                            if (ReleaseToServer4.Trim() != "")
                            {
                                using (DataStuff sn = new DataStuff())
                                {
                                    string ConnectionStr = string.Empty;

                                    ConnectionStr = GetServerConnectionString(ReleaseToServer4, UserID);

                                    ConnectionStr = ConnectionStr + ";Database=" + DatabaseName;

                                    if (IsNewObject)
                                    {
                                        ObjectText = gs.ChangeAlterToCreate(ObjectText);
                                    }

                                    SetStatus("Deploying object " + DatabaseName + " - " + ObjectName + " to " + ReleaseToServer4 + "...");

                                    RetVal = sn.CreateObjectS(ConnectionStr, ObjectText);

                                    if (RetVal != "")
                                    {
                                        RetValSMO = sn.CreateObjectSMO(ConnectionStr, ObjectText);

                                        if (RetValSMO != "")
                                        {
                                            MessageBox.Show("Failed - " + RetVal, "Modify Procedure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            SetStatus("Error deploying object " + DatabaseName + " - " + ObjectName + " to " + ReleaseToServer4 + "...");
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Failed - " + ex.Message, "Modify Procedure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Failed - " + ex.Message);

                return false;
            }

            SetStatus("Object " + DatabaseName + " - " + ObjectName + " successfully deployed.");

            return true;
        }

        private bool DeployFile(string ObjectName, string ObjectText)
        {
            string RetVal = string.Empty;
            string ServerRole = string.Empty;

            if (cbObjectStatus.Text == "")
            {
                MessageBox.Show("Please select the object's status.", "Change Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (tComment.Text == "")
            {
                MessageBox.Show("Please enter a comment for this change.", "Change Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                if (ReleaseToServer1.Trim() != "")
                {
                    using (DataStuff sn = new DataStuff())
                    {
                        string ConnectionStr = string.Empty;

                        ConnectionStr = GetServerConnectionString(ReleaseToServer1, UserID);

                        SetStatus("Deploying file object " + ObjectName + " to " + ReleaseToServer1 + "...");

                        RetVal = sn.CreateObjectSMO(ConnectionStr, ObjectText);

                        if (RetVal != "")
                        {
                            MessageBox.Show("Failed - " + RetVal, "Execute Script", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            SetStatus("Error deploying file object " + ObjectName + " to " + ReleaseToServer1 + ".");

                            return false;
                        }
                    }

                    if (ReleaseToServer2.Trim() != "")
                    {
                        using (DataStuff sn = new DataStuff())
                        {
                            string ConnectionStr = string.Empty;

                            ConnectionStr = GetServerConnectionString(ReleaseToServer2, UserID);

                            SetStatus("Deploying file object " + ObjectName + " to " + ReleaseToServer2 + "...");

                            RetVal = sn.CreateObjectSMO(ConnectionStr, ObjectText);

                            if (RetVal != "")
                            {
                                MessageBox.Show("Failed - " + RetVal, "Execute Script", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                SetStatus("Error deploying file object " + ObjectName + " to " + ReleaseToServer2 + ".");

                                return false;
                            }
                        }

                        if (ReleaseToServer3.Trim() != "")
                        {
                            using (DataStuff sn = new DataStuff())
                            {
                                string ConnectionStr = string.Empty;

                                ConnectionStr = GetServerConnectionString(ReleaseToServer3, UserID);

                                SetStatus("Deploying file object " + ObjectName + " to " + ReleaseToServer3 + "...");

                                RetVal = sn.CreateObjectSMO(ConnectionStr, ObjectText);

                                if (RetVal != "")
                                {
                                    MessageBox.Show("Failed - " + RetVal, "Execute Script", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    SetStatus("Error deploying file object " + ObjectName + " to " + ReleaseToServer3 + ".");

                                    return false;
                                }
                            }

                            if (ReleaseToServer4.Trim() != "")
                            {
                                using (DataStuff sn = new DataStuff())
                                {
                                    string ConnectionStr = string.Empty;

                                    ConnectionStr = GetServerConnectionString(ReleaseToServer4, UserID);

                                    SetStatus("Deploying file object " + ObjectName + " to " + ReleaseToServer4 + "...");

                                    RetVal = sn.CreateObjectSMO(ConnectionStr, ObjectText);

                                    if (RetVal != "")
                                    {
                                        MessageBox.Show("Failed - " + RetVal, "Execute Script", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        SetStatus("Error deploying file object " + ObjectName + " to " + ReleaseToServer4 + ".");

                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Failed - " + ex.Message, "Execute Script", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Failed - " + ex.Message);

                return false;
            }

            SetStatus("File object " + ObjectName + " successfully deployed.");

            return true;
        }

        private string GetServerRole(string ServerNameIn, int UserID)
        {
            try
            {
                string ServerRole = string.Empty;
                string UserName = string.Empty;
                string Password = string.Empty;
                string IntegratedSecurity = string.Empty;
                string ConnectionString = string.Empty;

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetServerDetail(ServerNameIn, UserID);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            ServerRole = row["ServerRoleDesc"].ToString();
                        }
                    }
                }

                return ServerRole;
            }

            catch
            {
                return "";
            }
        }

        public string AddProcComment(string ObjectText)
        {
            string ProcComment = "--SVC: " + DateTime.Now.ToString() + " - " + UserName + " - " + cbObjectStatus.Text + " - " + gs.RemoveSpecialCharacters(tComment.Text) + Environment.NewLine;

            ObjectText = ProcComment + ObjectText;

            return ObjectText;
        }
        
        private void LoadObjectStatus()
        {
            try
            {
                cbObjectStatus.Items.Clear();

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetObjectStatus(Environment.UserName);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            cbObjectStatus.Items.Add(row["ObjectStatusDesc"].ToString());
                        }
                    }
                }
            }

            catch
            {
                throw;
            }
        }

        private bool BackupObject(string ConnectionString, string DatabaseName, string ObjectName, string ObjectText)
        {
            try
            {
                string BackupComment;
                bool Result = false;

                SetStatus("Creating backup of " + ReleaseFromServer + " - " + DatabaseName + ": " + ObjectName + "...");

                BackupComment = UserName + " - " + cbObjectStatus.Text + " - " + gs.RemoveSpecialCharacters(tComment.Text);

                using (DataStuff sn = new DataStuff())
                {
                    Result = sn.BackupObject(ReleaseFromServer, cbObjectStatus.Text, UserName, ObjectName, DatabaseName, ObjectText, BackupComment, cbPartOfProject.Text, "", "", "N", "N");

                    if (!Result)
                    {
                        MessageBox.Show("There was an error creating a backup for this object. Please ensure that your servers have been set up correctly.", "Change Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            catch
            {
                MessageBox.Show("An error occurred while creating the backup record on the deploy from server.", "Backup Object", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Error creating backup of " + ReleaseFromServer + " - " + DatabaseName + ": " + ObjectName + ".");

                return false;
            }

            return true;
        }

        private bool BackupReleaseToServerObject(string ReleaseToServer, string DatabaseName, string ObjectName)
        {
            string ObjectText = string.Empty;
            string ObjectDescription = string.Empty;
            string ConnectionStr = string.Empty;
            bool Result = false;

            IsNewObject = false;
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

                BackupComment = UserName + " - " + cbObjectStatus.Text + " - Backup copy before release - " + gs.RemoveSpecialCharacters(tComment.Text);

                using (DataStuff sn = new DataStuff())
                {
                    Result = sn.BackupObject(ReleaseToServer, cbObjectStatus.Text, UserName, ObjectName, DatabaseName, ObjectText, BackupComment, cbPartOfProject.Text, "", "", "N", "N");

                    if (!Result)
                    {
                        MessageBox.Show("There was an error creating a backup for this object. Please ensure that your servers have been set up correctly.", "Change Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    SetStatus("Creating backup of " + ReleaseToServer + " - " + DatabaseName + ": " + ObjectName + "...");
                }
            }

            catch
            {
                MessageBox.Show("An error occurred while creating the backup record.", "Backup Object", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Error creating backup of " + ReleaseToServer + " - " + DatabaseName + ": " + ObjectName + ".");
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

        private void CompareFiles(string Alias1, string DBName, string ObjName)
        {
            string File1 = string.Empty;
            string File2 = string.Empty;
            string File1Content = string.Empty;
            string File2Content = string.Empty;
            bool FoundContent = false;
            string WinMergePath = string.Empty;
            string Server2ConnString = string.Empty;
            string Server1ConnString = string.Empty;
            string Alias2 = string.Empty;

            string path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)).FullName;

            File1 = path.Trim() + @"\" + "Compare1" + DateTime.Now.ToLongTimeString().Replace(":", "").Replace(" ", "") + ".txt";
            File2 = path.Trim() + @"\" + "Compare2" + DateTime.Now.ToLongTimeString().Replace(":", "").Replace(" ", "") + ".txt";

            if (ReleaseFromServer.Trim() == Alias1.Trim())
            {
                Alias2 = ReleaseToServer1;

                Server1ConnString = GetServerConnectionString(Alias1, UserID);
                File1Content = GetObjectText(Server1ConnString, DBName, ObjName);

                Server2ConnString = GetServerConnectionString(Alias2, UserID);
                File2Content = GetObjectText(Server2ConnString, DBName, ObjName);
            }

            if (ReleaseToServer1.Trim() == Alias1.Trim())
            {
                if (ReleaseToServer2.Trim() != "")
                {
                    Alias2 = ReleaseToServer2;
                }
                else
                {
                    Alias2 = ReleaseFromServer;
                }

                Server1ConnString = GetServerConnectionString(Alias1, UserID);
                File1Content = GetObjectText(Server1ConnString, DBName, ObjName);

                Server2ConnString = GetServerConnectionString(Alias2, UserID);
                File2Content = GetObjectText(Server2ConnString, DBName, ObjName);
            }

            if (ReleaseToServer2.Trim() == Alias1.Trim())
            {
                if (ReleaseToServer3.Trim() != "")
                {
                    Alias2 = ReleaseToServer3;
                }
                else
                {
                    Alias2 = ReleaseFromServer;
                }

                Server1ConnString = GetServerConnectionString(Alias1, UserID);
                File1Content = GetObjectText(Server1ConnString, DBName, ObjName);

                Server2ConnString = GetServerConnectionString(Alias2, UserID);
                File2Content = GetObjectText(Server2ConnString, DBName, ObjName);
            }

            if (ReleaseToServer3.Trim() == Alias1.Trim())
            {
                if (ReleaseToServer4.Trim() != "")
                {
                    Alias2 = ReleaseToServer4;
                }
                else
                {
                    Alias2 = ReleaseFromServer;
                }

                Server1ConnString = GetServerConnectionString(Alias1, UserID);
                File1Content = GetObjectText(Server1ConnString, DBName, ObjName);

                Server2ConnString = GetServerConnectionString(Alias2, UserID);
                File2Content = GetObjectText(Server2ConnString, DBName, ObjName);
            }

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

                    startInfo.Arguments = "/e /dl " + "\"" + Alias1 + "\" /dr " + "\"" + Alias2 + "\" " + @File1 + " " + @File2;
                    Process.Start(startInfo);
                }

                catch
                {
                    MessageBox.Show("Oops, something went wrong. Please check your WinMerge installation.", "WinMerge", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Project_Resize(object sender, EventArgs e)
        {
            //pTop.Width = this.Width - 140;
            //gbGrid.Width = this.Width - 40;
            //gbGrid.Height = this.Height - gbGrid.Top - 70;
            //gbEnvironments.Width = gbGrid.Width;
            //pTop.Width = this.Width - 50;
            //gbProject.Width = pTop.Width - 50;
            //gbProjectOnly.Width =  pTop.Width - 150;

            //ReCreateGrid();
        }
    }

    public class ProjectEventArgs
    {
        public ProjectEventArgs(string Server, string Project)
        {
            ProjectName = Project;
            ServerName = Server;
        }
        public String ServerName
        {
            get;
            private set;
        }
        public String ProjectName
        {
            get;
            private set;
        }
    }
}
