using System;
using System.Data;
using System.Windows.Forms;
using SentryDataStuff;

namespace SentryAdmin
{
    public partial class Administrator : Form
    {
        int UserID = 0;
        bool Loading = false;

        public Administrator()
        {
            InitializeComponent();
        }

        private void Administrator_Load(object sender, EventArgs e)
        {
            Loading = true;

            tNetworkName.Visible = false;
            cbNetworkName.Visible = true;

            tsbCancelUserEdit.Enabled = false;
            tsbSaveUser.Enabled = false;
            tsbDeleteUser.Enabled = false;

            cAdminBackupDev.CheckedChanged += Role_CheckedChanged;
            cAdminCheckOutDev.CheckedChanged += Role_CheckedChanged;
            cAdminCheckOutPreProd.CheckedChanged += Role_CheckedChanged;
            cAdminCheckOutProd.CheckedChanged += Role_CheckedChanged;
            cAdminCheckOutUAT.CheckedChanged += Role_CheckedChanged;
            cAdminProjectMaintenance.CheckedChanged += Role_CheckedChanged;
            cAdminRelPreProd.CheckedChanged += Role_CheckedChanged;
            cAdminRelProd.CheckedChanged += Role_CheckedChanged;
            cAdminRelUAT.CheckedChanged += Role_CheckedChanged;
            cAdminRestoreDev.CheckedChanged += Role_CheckedChanged;
            cAdminRestorePreProd.CheckedChanged += Role_CheckedChanged;
            cAdminRestoreProd.CheckedChanged += Role_CheckedChanged;
            cAdminRestoreUAT.CheckedChanged += Role_CheckedChanged;
            cAdminAdministration.CheckedChanged += Role_CheckedChanged;
            cAdminQuickRelease.CheckedChanged += Role_CheckedChanged;

            cDevBackupDev.CheckedChanged += Role_CheckedChanged;
            cDevCheckOutDev.CheckedChanged += Role_CheckedChanged;
            cDevCheckOutPreProd.CheckedChanged += Role_CheckedChanged;
            cDevCheckOutProd.CheckedChanged += Role_CheckedChanged;
            cDevCheckOutUAT.CheckedChanged += Role_CheckedChanged;
            cDevProjectMaintenance.CheckedChanged += Role_CheckedChanged;
            cDevRelPreProd.CheckedChanged += Role_CheckedChanged;
            cDevRelProd.CheckedChanged += Role_CheckedChanged;
            cDevRelUAT.CheckedChanged += Role_CheckedChanged;
            cDevRestoreDev.CheckedChanged += Role_CheckedChanged;
            cDevRestorePreProd.CheckedChanged += Role_CheckedChanged;
            cDevRestoreProd.CheckedChanged += Role_CheckedChanged;
            cDevRestoreUAT.CheckedChanged += Role_CheckedChanged;
            cDevAdministration.CheckedChanged += Role_CheckedChanged;
            cDevQuickRelease.CheckedChanged += Role_CheckedChanged;
            cDev1.CheckedChanged += Role_CheckedChanged;
            cDev2.CheckedChanged += Role_CheckedChanged;

            cPreProdBackupDev.CheckedChanged += Role_CheckedChanged;
            cPreProdCheckOutDev.CheckedChanged += Role_CheckedChanged;
            cPreProdCheckOutPreProd.CheckedChanged += Role_CheckedChanged;
            cPreProdCheckOutProd.CheckedChanged += Role_CheckedChanged;
            cPreProdCheckOutUAT.CheckedChanged += Role_CheckedChanged;
            cPreProdProjectMaintenance.CheckedChanged += Role_CheckedChanged;
            cPreProdRelPreProd.CheckedChanged += Role_CheckedChanged;
            cPreProdRelProd.CheckedChanged += Role_CheckedChanged;
            cPreProdRelUAT.CheckedChanged += Role_CheckedChanged;
            cPreProdRestoreDev.CheckedChanged += Role_CheckedChanged;
            cPreProdRestorePreProd.CheckedChanged += Role_CheckedChanged;
            cPreProdRestoreProd.CheckedChanged += Role_CheckedChanged;
            cPreProdRestoreUAT.CheckedChanged += Role_CheckedChanged;
            cPreProdAdministration.CheckedChanged += Role_CheckedChanged;
            cPreProdQuickRelease.CheckedChanged += Role_CheckedChanged;
            cPreProd1.CheckedChanged += Role_CheckedChanged;
            cPreProd2.CheckedChanged += Role_CheckedChanged;

            cProdBackupDev.CheckedChanged += Role_CheckedChanged;
            cProdCheckOutDev.CheckedChanged += Role_CheckedChanged;
            cProdCheckOutPreProd.CheckedChanged += Role_CheckedChanged;
            cProdCheckOutProd.CheckedChanged += Role_CheckedChanged;
            cProdCheckOutUAT.CheckedChanged += Role_CheckedChanged;
            cProdProjectMaintenance.CheckedChanged += Role_CheckedChanged;
            cProdRelPreProd.CheckedChanged += Role_CheckedChanged;
            cProdRelProd.CheckedChanged += Role_CheckedChanged;
            cProdRelUAT.CheckedChanged += Role_CheckedChanged;
            cProdRestoreDev.CheckedChanged += Role_CheckedChanged;
            cProdRestorePreProd.CheckedChanged += Role_CheckedChanged;
            cProdRestoreProd.CheckedChanged += Role_CheckedChanged;
            cProdRestoreUAT.CheckedChanged += Role_CheckedChanged;
            cProdAdministration.CheckedChanged += Role_CheckedChanged;
            cProdQuickRelease.CheckedChanged += Role_CheckedChanged;
            cProd1.CheckedChanged += Role_CheckedChanged;
            cProd2.CheckedChanged += Role_CheckedChanged;

            cUATBackupDev.CheckedChanged += Role_CheckedChanged;
            cUATCheckOutDev.CheckedChanged += Role_CheckedChanged;
            cUATCheckOutPreProd.CheckedChanged += Role_CheckedChanged;
            cUATCheckOutProd.CheckedChanged += Role_CheckedChanged;
            cUATCheckOutUAT.CheckedChanged += Role_CheckedChanged;
            cUATProjectMaintenance.CheckedChanged += Role_CheckedChanged;
            cUATRelPreProd.CheckedChanged += Role_CheckedChanged;
            cUATRelProd.CheckedChanged += Role_CheckedChanged;
            cUATRelUAT.CheckedChanged += Role_CheckedChanged;
            cUATRestoreDev.CheckedChanged += Role_CheckedChanged;
            cUATRestorePreProd.CheckedChanged += Role_CheckedChanged;
            cUATRestoreProd.CheckedChanged += Role_CheckedChanged;
            cUATRestoreUAT.CheckedChanged += Role_CheckedChanged;
            cUATAdministration.CheckedChanged += Role_CheckedChanged;
            cUATQuickRelease.CheckedChanged += Role_CheckedChanged;
            cUAT1.CheckedChanged += Role_CheckedChanged;
            cUAT2.CheckedChanged += Role_CheckedChanged;

            cDevCheckOutOverride.CheckedChanged += Role_CheckedChanged;
            cUATCheckOutOverride.CheckedChanged += Role_CheckedChanged;
            cPreProdCheckOutOverride.CheckedChanged += Role_CheckedChanged;
            cProdCheckOutOverride.CheckedChanged += Role_CheckedChanged;
            cAdminCheckOutOverride.CheckedChanged += Role_CheckedChanged;
            cAdmin1.CheckedChanged += Role_CheckedChanged;
            cAdmin2.CheckedChanged += Role_CheckedChanged;

            cApproverFinal1.CheckedChanged += Role_CheckedChanged;
            cApproverFinal10.CheckedChanged += Role_CheckedChanged;
            cApproverFinal11.CheckedChanged += Role_CheckedChanged;
            cApproverFinal12.CheckedChanged += Role_CheckedChanged;
            cApproverFinal13.CheckedChanged += Role_CheckedChanged;
            cApproverFinal14.CheckedChanged += Role_CheckedChanged;
            cApproverFinal15.CheckedChanged += Role_CheckedChanged;
            cApproverFinal16.CheckedChanged += Role_CheckedChanged;
            cApproverFinal17.CheckedChanged += Role_CheckedChanged;
            cApproverFinal18.CheckedChanged += Role_CheckedChanged;
            cApproverFinal2.CheckedChanged += Role_CheckedChanged;
            cApproverFinal3.CheckedChanged += Role_CheckedChanged;
            cApproverFinal4.CheckedChanged += Role_CheckedChanged;
            cApproverFinal5.CheckedChanged += Role_CheckedChanged;
            cApproverFinal6.CheckedChanged += Role_CheckedChanged;
            cApproverFinal7.CheckedChanged += Role_CheckedChanged;
            cApproverFinal8.CheckedChanged += Role_CheckedChanged;
            cApproverFinal9.CheckedChanged += Role_CheckedChanged;

            cApproverTesting1.CheckedChanged += Role_CheckedChanged;
            cApproverTesting10.CheckedChanged += Role_CheckedChanged;
            cApproverTesting11.CheckedChanged += Role_CheckedChanged;
            cApproverTesting12.CheckedChanged += Role_CheckedChanged;
            cApproverTesting13.CheckedChanged += Role_CheckedChanged;
            cApproverTesting14.CheckedChanged += Role_CheckedChanged;
            cApproverTesting15.CheckedChanged += Role_CheckedChanged;
            cApproverTesting16.CheckedChanged += Role_CheckedChanged;
            cApproverTesting17.CheckedChanged += Role_CheckedChanged;
            cApproverTesting18.CheckedChanged += Role_CheckedChanged;
            cApproverTesting2.CheckedChanged += Role_CheckedChanged;
            cApproverTesting3.CheckedChanged += Role_CheckedChanged;
            cApproverTesting4.CheckedChanged += Role_CheckedChanged;
            cApproverTesting5.CheckedChanged += Role_CheckedChanged;
            cApproverTesting6.CheckedChanged += Role_CheckedChanged;
            cApproverTesting7.CheckedChanged += Role_CheckedChanged;
            cApproverTesting8.CheckedChanged += Role_CheckedChanged;
            cApproverTesting9.CheckedChanged += Role_CheckedChanged;



            LoadUsers();

            LoadRoles();

            GetRoleValues();

            GetStrictSettings();

            Loading = false;
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
                        cStrictProjectObjectAdd.Checked = true;
                    }
                    else
                    {
                        cStrictProjectObjectAdd.Checked = false;
                    }

                    SettingValue = sn.GetSystemSetting("Key6");

                    if (SettingValue.Trim() == "true")
                    {
                        cStrictObjectBaseCreate.Checked = true;
                    }
                    else
                    {
                        cStrictObjectBaseCreate.Checked = false;
                    }

                    SettingValue = sn.GetSystemSetting("Key7");

                    if (SettingValue.Trim() == "true")
                    {
                        cStrictForceCheckOutDev.Checked = true;
                    }
                    else
                    {
                        cStrictForceCheckOutDev.Checked = false;
                    }

                    SettingValue = sn.GetSystemSetting("Key8");

                    if (SettingValue.Trim() == "true")
                    {
                        cForceTesterApproval.Checked = true;
                    }
                    else
                    {
                        cForceTesterApproval.Checked = false;
                    }

                    SettingValue = sn.GetSystemSetting("Key9");

                    if (SettingValue.Trim() == "true")
                    {
                        cForceApproverApproval.Checked = true;
                    }
                    else
                    {
                        cForceApproverApproval.Checked = false;
                    }

                    SettingValue = sn.GetSystemSetting("Key10");

                    tEmailPickupFolder.Text = SettingValue;

                    SettingValue = sn.GetSystemSetting("Key11");

                    if (SettingValue.Trim() == "true")
                    {
                        cUpdateChangeLog.Checked = true;
                    }
                    else
                    {
                        cUpdateChangeLog.Checked = false;
                    }
                }
            }

            catch
            {

            }
        }
        private void GetRoleValues()
        {
            string RoleName = string.Empty;
            string FunctionName = string.Empty;
            string CurBox = string.Empty;

            foreach (Control Ctl in tpRoles.Controls)
            {
                if (Ctl is CheckBox)
                {
                    ((CheckBox)Ctl).Checked = false;
                }
            }

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetRoleValues();

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            RoleName = row["RoleName"].ToString();
                            FunctionName = row["FunctionName"].ToString();

                            foreach (Control Ctl in tpRoles.Controls)
                            {
                                if (Ctl is CheckBox)
                                {
                                    if (((CheckBox)Ctl).Tag.ToString() == FunctionName)
                                    {
                                        CurBox = ((CheckBox)Ctl).Name;

                                        if (RoleName == "Developer")
                                        {
                                            if (CurBox.Contains("cDev"))
                                            {
                                                ((CheckBox)Ctl).Checked = true;
                                            }
                                        }

                                        if (RoleName == "Releaser UAT")
                                        {
                                            if (CurBox.Contains("cUAT"))
                                            {
                                                ((CheckBox)Ctl).Checked = true;
                                            }
                                        }

                                        if (RoleName == "Releaser Pre-Production")
                                        {
                                            if (CurBox.Contains("cPreProd"))
                                            {
                                                ((CheckBox)Ctl).Checked = true;
                                            }
                                        }

                                        if (RoleName == "Releaser Production")
                                        {
                                            if (CurBox.Contains("cProd"))
                                            {
                                                ((CheckBox)Ctl).Checked = true;
                                            }
                                        }

                                        if (RoleName == "Administrator")
                                        {
                                            if (CurBox.Contains("cAdmin"))
                                            {
                                                ((CheckBox)Ctl).Checked = true;
                                            }
                                        }

                                        if (RoleName == "Final Approver")
                                        {
                                            if (CurBox.Contains("cApproverFinal"))
                                            {
                                                ((CheckBox)Ctl).Checked = true;
                                            }
                                        }

                                        if (RoleName == "Testing Approver")
                                        {
                                            if (CurBox.Contains("cApproverTesting"))
                                            {
                                                ((CheckBox)Ctl).Checked = true;
                                            }
                                        }
                                    }
                                }
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

        private void Role_CheckedChanged(object sender, EventArgs e)
        {
            string RoleName = string.Empty;
            string RoleFunction = string.Empty;
            string CurBox = string.Empty;
            string AddRemoveAction = string.Empty;

            if (Loading)
            {
                return;
            }

            try
            {

                RoleFunction = ((CheckBox)sender).Tag.ToString();
                CurBox = ((CheckBox)sender).Name.ToString();

                if (((CheckBox)sender).Checked)
                {
                    AddRemoveAction = "Add";
                }
                else
                {
                    AddRemoveAction = "Remove";
                }

                if (CurBox.Contains("cDev"))
                {
                    RoleName = "Developer";
                }

                if (CurBox.Contains("cUAT"))
                {
                    RoleName = "Releaser UAT";
                }

                if (CurBox.Contains("cPreProd"))
                {
                    RoleName = "Releaser Pre-Production";
                }

                if (CurBox.Contains("cProd"))
                {
                    RoleName = "Releaser Production";
                }

                if (CurBox.Contains("cAdmin"))
                {
                    RoleName = "Administrator";
                }

                if (CurBox.Contains("cApproverTesting"))
                {
                    RoleName = "Testing Approver";
                }

                if (CurBox.Contains("cApproverFinal"))
                {
                    RoleName = "Final Approver";
                }

                using (DataStuff sn = new DataStuff())
                {
                    if (!sn.SaveRoleSetting(RoleName, RoleFunction, AddRemoveAction))
                    {
                        MessageBox.Show("There was an error updating the system role.", "Role Definition", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            catch
            {
                
            }
        }

        private void tsbNewUser_Click(object sender, EventArgs e)
        {
            UserID = 0;

            cbNetworkName.Visible = false;
            tNetworkName.Visible = true;

            tsbCancelUserEdit.Enabled = true;
            tsbDeleteUser.Enabled = false;
            tsbSaveUser.Enabled = true;

            tName.Text = "";
            tEmail.Text = "";
            cUserActive.Checked = true;

            tNetworkName.Focus();
        }

        private void LoadUsers()
        {
            try
            {
                cbNetworkName.Items.Clear();

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetNetworkSystemUsers();

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            cbNetworkName.Items.Add(row["QTNumber"].ToString());
                        }
                    }
                }
            }

            catch
            {
            }
        }

        private void LoadRoles()
        {
            try
            {
                cbRole.Items.Clear();

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetRoles();

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            cbRole.Items.Add(row["RoleName"].ToString());
                        }
                    }
                }
            }

            catch
            {
            }
        }

        private bool IsValid()
        {
            if (tNetworkName.Visible)
            {
                if (tNetworkName.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter the user's network name.", "User Maintenance", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tNetworkName.Focus();
                    return false;
                }
            }
            else
            {
                if (cbNetworkName.Text.Trim() == "")
                {
                    MessageBox.Show("Please select a network name.", "User Maintenance", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cbNetworkName.Focus();
                    return false;
                }
            }

            if (tName.Text.Trim() == "")
            {
                MessageBox.Show("Please enter the user's name.", "User Maintenance", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tName.Focus();
                return false;
            }

            if (tEmail.Text.Trim() == "")
            {
                MessageBox.Show("Please enter the user's email address.", "User Maintenance", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tName.Focus();
                return false;
            }

            if (cbRole.Text.Trim() == "")
            {
                MessageBox.Show("Please select a role for this user.", "User Maintenance", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cbRole.Focus();
                return false;
            }

            return true;
        }

        private void cbNetworkName_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserID = 0;

            if (cbNetworkName.Text.Trim() != "")
            {
                try
                {
                    using (DataStuff sn = new DataStuff())
                    {
                        DataTable dt = sn.GetNetworkSystemUserByID(cbNetworkName.Text);

                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                tName.Text = row["UserName"].ToString();
                                tEmail.Text = row["EmailAddress"].ToString();
                                UserID = Convert.ToInt32(row["UserID"].ToString());

                                cbRole.Text = row["RoleName"].ToString();

                                if (row["Active"].ToString() == "Y")
                                {
                                    cUserActive.Checked = true;
                                }
                                else
                                {
                                    cUserActive.Checked = false;
                                }
                            }

                            if (cbNetworkName.Text.Trim() != "")
                            {
                                tsbCancelUserEdit.Enabled = false;
                                tsbDeleteUser.Enabled = true;
                                tsbNewUser.Enabled = true;
                                tsbSaveUser.Enabled = true;
                            }
                            else
                            {
                                tsbCancelUserEdit.Enabled = false;
                                tsbDeleteUser.Enabled = false;
                                tsbNewUser.Enabled = true;
                                tsbSaveUser.Enabled = true;
                            }
                        }
                    }
                }

                catch
                {
                    throw;
                }
            }
        }

        private void tsbCancelUserEdit_Click(object sender, EventArgs e)
        {
            tsbCancelUserEdit.Enabled = false;
            tsbDeleteUser.Enabled = false;
            tsbSaveUser.Enabled = false;
            tsbNewUser.Enabled = true;

            tNetworkName.Visible = false;
            cbNetworkName.Visible = true;

            tName.Text = "";
            tEmail.Text = "";

            cbNetworkName.Text = "";
            cbNetworkName.Focus();
        }

        private bool SaveUser()
        {
            try
            {

            }

            catch
            {

            }

            return true;
        }

        private void tsbSaveUser_Click(object sender, EventArgs e)
        {
            string NetworkUserName = string.Empty;

            if (!IsValid())
            {
                return;
            }

            try
            {
                if (tNetworkName.Visible)
                {
                    UserID = 0;
                    NetworkUserName = tNetworkName.Text;
                }
                else
                {
                    NetworkUserName = cbNetworkName.Text;
                }

                using (DataStuff sn = new DataStuff())
                {
                    if (sn.SaveNetworkSystemUser(UserID, NetworkUserName, tName.Text, tEmail.Text, cUserActive.Checked ? "Y" : "N"))
                    {
                        if (sn.SaveUserRole(cbRole.Text, NetworkUserName))
                        {
                            MessageBox.Show("User successfully saved.", "User Maintenance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        tNetworkName.Visible = false;
                        cbNetworkName.Visible = true;
                        tsbCancelUserEdit.Enabled = false;
                        tsbSaveUser.Enabled = true;
                        tsbDeleteUser.Enabled = true;

                        LoadUsers();

                        //cbNetworkName.SelectedIndex = 1;
                        cbNetworkName.Focus();
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("There was an error saving the user.", "User Maintenance", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private bool UserReferenced()
        {
            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    if (sn.NetworkSystemUserReferenced(UserID) > 0)
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
                return true;
            }
        }
        private void tsbDeleteUser_Click(object sender, EventArgs e)
        {
            if (UserID != 0)
            {
                if (UserReferenced())
                {
                    MessageBox.Show("The user cannot be deleted, " + tName.Text + " is still referenced in the system.", "User Maintenance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                try
                {
                    using (DataStuff sn = new DataStuff())
                    {
                        if (sn.DeleteNetworkSystemUser(UserID))
                        {
                            MessageBox.Show("User successfully deleted.", "User Maintenance", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            tNetworkName.Visible = false;
                            cbNetworkName.Visible = true;
                            tsbCancelUserEdit.Enabled = false;
                            tsbSaveUser.Enabled = true;
                            tsbDeleteUser.Enabled = true;

                            LoadUsers();

                            //cbNetworkName.SelectedIndex = 1;
                        }
                    }
                }

                catch
                {
                    MessageBox.Show("There was an error deleting the user.", "User Maintenance", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void cStrictProjectObjectAdd_CheckedChanged(object sender, EventArgs e)
        {
            string SettingValue = string.Empty;

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    if (cStrictProjectObjectAdd.Checked == true)
                    {
                        SettingValue = "true";
                    }
                    else
                    {
                        SettingValue = "false";
                    }

                    sn.SaveSystemSetting("Key5", SettingValue);
                    
                }
            }

            catch
            {

            }
        }

        private void cStrictObjectBaseCreate_CheckedChanged(object sender, EventArgs e)
        {
            string SettingValue = string.Empty;

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    if (cStrictObjectBaseCreate.Checked == true)
                    {
                        SettingValue = "true";
                    }
                    else
                    {
                        SettingValue = "false";
                    }

                    sn.SaveSystemSetting("Key6", SettingValue);
                }
            }

            catch
            {

            }
        }

        private void SaveSMTPLocation()
        {
            string SettingValue = string.Empty;

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    SettingValue = tEmailPickupFolder.Text;
                    
                    sn.SaveSystemSetting("Key10", SettingValue);
                }
            }

            catch
            {

            }
        }

        private void cStrictForceCheckOutDev_CheckedChanged(object sender, EventArgs e)
        {
            string SettingValue = string.Empty;

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    if (cStrictForceCheckOutDev.Checked == true)
                    {
                        SettingValue = "true";
                    }
                    else
                    {
                        SettingValue = "false";
                    }

                    sn.SaveSystemSetting("Key7", SettingValue);
                }
            }

            catch
            {

            }
        }

        private void cForceTesterApproval_CheckedChanged(object sender, EventArgs e)
        {
            string SettingValue = string.Empty;

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    if (cForceTesterApproval.Checked == true)
                    {
                        SettingValue = "true";
                    }
                    else
                    {
                        SettingValue = "false";
                    }

                    sn.SaveSystemSetting("Key8", SettingValue);
                }
            }

            catch
            {

            }
        }

        private void cForceApproverApproval_CheckedChanged(object sender, EventArgs e)
        {
            string SettingValue = string.Empty;

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    if (cForceApproverApproval.Checked == true)
                    {
                        SettingValue = "true";
                    }
                    else
                    {
                        SettingValue = "false";
                    }

                    sn.SaveSystemSetting("Key9", SettingValue);
                }
            }

            catch
            {

            }
        }

        private void cAdminCheckOutDev_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Administrator_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSMTPLocation();
        }

        private void cUpdateChangeLog_CheckedChanged(object sender, EventArgs e)
        {
            string SettingValue = string.Empty;

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    if (cUpdateChangeLog.Checked == true)
                    {
                        SettingValue = "true";
                    }
                    else
                    {
                        SettingValue = "false";
                    }

                    sn.SaveSystemSetting("Key11", SettingValue);
                }
            }

            catch
            {

            }
        }
    }
}
