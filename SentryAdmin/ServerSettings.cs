using System;
using System.Data;
using System.Windows.Forms;
using SentryDataStuff;

namespace SentryAdmin
{
    public partial class ServerSettings : Form
    {
        public int ServerID = 0;
        public string ConnectionString = string.Empty;
        string UserName = string.Empty;
        int UserID = 0;

        public ServerSettings()
        {
            InitializeComponent();
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            ServerID = 0;
            cbServerName.Text = "";
            tServerName.Text = "";
            tUserName.Text = "";
            tPassword.Text = "";
        }

        private bool DoValidation()
        {
            SetToolbar();

            if (cbServerName.Text == "")
            {
                MessageBox.Show("Please select a server alias.", "Server Settings", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (!cIntegratedSecurity.Checked)
            {
                if (tUserName.Text == "")
                {
                    MessageBox.Show("Please enter a user name.", "Server Settings", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                if (tPassword.Text == "")
                {
                    MessageBox.Show("Please enter a user password.", "Server Settings", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }

            return true;
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            string IntegratedSecurity = "";

            if (!DoValidation())
            {
                return;
            }

            try
            {
                if (cIntegratedSecurity.Checked)
                {
                    IntegratedSecurity = "Y";
                }
                else
                {
                    IntegratedSecurity = "N";
                }

                using (DataStuff sn = new DataStuff())
                {
                    sn.SaveServer(cbServerName.Text, tUserName.Text, tPassword.Text, IntegratedSecurity, UserID);

                    MessageBox.Show("Server saved.", "Server Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("There was an error saving the server settings - " + ex.Message, "Server Settings", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void tsbTestConnection_Click(object sender, EventArgs e)
        {
            string ConnectionString = string.Empty;

            if (cIntegratedSecurity.Checked)
            {
                ConnectionString = "Data Source=" + tServerName.Text + ";Integrated Security=True";
            }
            else
            {
                ConnectionString = "Data Source=" + tServerName.Text + ";User ID=" + tUserName.Text + ";Password=" + tPassword.Text;
            }

            try
            {
                using (DBStuff dbs = new DBStuff())
                {
                    dbs.CommandTimeOut = 10;

                    if (dbs.TestConnection(ConnectionString))
                    {
                        MessageBox.Show("Connection successful", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Connection failed", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            catch
            {

            }
        }

        private void cbServerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetServerDetail();

            if (tServerName.Text == "")
            {
                GetServerAliasDetail();
            }

            SetToolbar();
        }

        private void SetToolbar()
        {
            tsbSave.Enabled = false;
            tsbTestConnection.Enabled = false;

            if (cbServerName.Text == "")
            {
                return;    
            }

            if (tServerName.Text == "")
            {
                return;
            }

            if (!cIntegratedSecurity.Checked)
            {
                if (tUserName.Text == "")
                {
                    return;
                }

                if (tPassword.Text == "")
                {
                    return;
                }
            }

            tsbSave.Enabled = true;
            tsbTestConnection.Enabled = true;
        }
        private void cbServerRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetToolbar();
        }

        private void ServerSettings_Load(object sender, EventArgs e)
        {
            using (DataStuff sn = new DataStuff())
            {
                DataTable dt = sn.SingleUser(Environment.UserName, "ServerSettings");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        UserName = row["UserName"].ToString();
                        UserID = Convert.ToInt32(row["UserID"].ToString());
                    }
                }
            }

            this.Text = "Server Settings - " + UserName;

            LoadServerNames();

            tsbSave.Enabled = false;
            tsbTestConnection.Enabled = false;
        }

        private void LoadServerNames()
        {
            try
            {
                cbServerName.Items.Clear();

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetServerAliases();

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            cbServerName.Items.Add(row["ServerAliasDesc"].ToString());
                        }
                    }
                }
            }

            catch
            {
                throw;
            }
        }
        
        private void GetServerID()
        {
            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    ServerID = sn.GetServerID(cbServerName.Text, UserID);
                }
            }

            catch
            {
                ServerID = 0;
            }
        }

        private void GetServerDetail()
        {
            try
            {
                tServerName.Text = "";
                ServerID = 0;
                tUserName.Text = "";
                tPassword.Text = "";
                cIntegratedSecurity.Checked = false;
                tServerRole.Text = "";

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetServerDetail(cbServerName.Text, UserID);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            ServerID = Convert.ToInt32(row["ServerID"].ToString());
                            tServerName.Text = row["ServerName"].ToString();
                            tUserName.Text = row["UserName"].ToString();

                            DataStuff p = new DataStuff();
                            tPassword.Text = p.Ontsyfer((byte[])row["Password"]);
                            
                            if (row["IntegratedSecurity"].ToString() == "Y")
                            {
                                cIntegratedSecurity.Checked = true;
                            }
                            else
                            {
                                cIntegratedSecurity.Checked = false;
                            }

                            tServerRole.Text = row["ServerRoleDesc"].ToString();
                        }
                    }
                }
            }

            catch (Exception ex)
            {

            }
        }

        private void GetServerAliasDetail()
        {
            try
            {
                tServerName.Text = "";
                tServerRole.Text = "";

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetServerAliasDetail(cbServerName.Text);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            tServerName.Text = row["ServerName"].ToString();
                            tServerRole.Text = row["ServerRoleDesc"].ToString();
                        }
                    }
                }
            }

            catch
            {

            }
        }

        private void cIntegratedSecurity_CheckedChanged(object sender, EventArgs e)
        {
            if (cIntegratedSecurity.Checked)
            {
                tUserName.Enabled = false;
                tPassword.Enabled = false;
            }
            else
            {
                tUserName.Enabled = true;
                tPassword.Enabled = true;
            }

            SetToolbar();
        }

        private void tServerName_TextChanged(object sender, EventArgs e)
        {
            SetToolbar();
        }

        private void tUserName_TextChanged(object sender, EventArgs e)
        {
            SetToolbar();
        }

        private void tPassword_TextChanged(object sender, EventArgs e)
        {
            SetToolbar();
        }
    }
}
