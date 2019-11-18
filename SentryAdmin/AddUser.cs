using System;
using System.Data;
using System.Windows.Forms;
using SentryDataStuff;

namespace SentryAdmin
{
    public partial class AddUser : Form
    {
        int UserID = 0;

        public AddUser()
        {
            InitializeComponent();
        }

        private void AddUser_Load(object sender, EventArgs e)
        {
            LoadRoles();
            tNetworkName.Text = Environment.UserName;
            tName.Focus();
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

        private void SaveUser()
        {
            string NetworkUserName = string.Empty;

            if (!IsValid())
            {
                return;
            }

            try
            {
                UserID = 0;
                NetworkUserName = tNetworkName.Text;

                using (DataStuff sn = new DataStuff())
                {
                    if (sn.SaveNetworkSystemUser(UserID, NetworkUserName, tName.Text, tEmail.Text, "Y"))
                    {
                        if (sn.SaveUserRole(cbRole.Text, NetworkUserName))
                        {
                            MessageBox.Show("User successfully saved.", "User Maintenance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        tNetworkName.Focus();
                    }
                }
            }

            catch
            {
                MessageBox.Show("There was an error saving the user.", "User Maintenance", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        private bool IsValid()
        {
            if (tNetworkName.Text.Trim() == "")
            {
                MessageBox.Show("Please enter the user's network name.", "User Maintenance", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tNetworkName.Focus();
                return false;
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

        private void cmdSave_Click(object sender, EventArgs e)
        {
            SaveUser();
        }
    }
}
