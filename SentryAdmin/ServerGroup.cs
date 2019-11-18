using System;
using System.Data;
using System.Windows.Forms;
using SentryDataStuff;

namespace SentryAdmin
{
    public partial class ServerGroup : Form
    {
        public ServerGroup()
        {
            InitializeComponent();
        }

        private void ServerGroup_Load(object sender, EventArgs e)
        {
            LoadServerGroups();
            LoadServerRoles();

            gbDev.Enabled = false;
            gbUAT.Enabled = false;
            gbPreProd.Enabled = false;
            gbProd.Enabled = false;

            tsbSave.Enabled = false;
            tServerGroup.Visible = false;
            cbServerGroup.Visible = true;
            cbServerGroup.Focus();
        }

        private void LoadServerRoles()
        {
            try
            {
                cbServerRoleDev.Items.Clear();
                cbServerRoleUAT.Items.Clear();
                cbServerRolePreProd.Items.Clear();
                cbServerRoleProd.Items.Clear();

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetServerRoles();

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            cbServerRoleDev.Items.Add(row["ServerRoleDesc"].ToString());
                            cbServerRoleUAT.Items.Add(row["ServerRoleDesc"].ToString());
                            cbServerRolePreProd.Items.Add(row["ServerRoleDesc"].ToString());
                            cbServerRoleProd.Items.Add(row["ServerRoleDesc"].ToString());
                        }
                    }
                }
            }

            catch
            {
                throw;
            }
        }

        private void LoadServerGroups()
        {
            try
            {
                cbServerGroup.Items.Clear();

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetServerGroups();

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            cbServerGroup.Items.Add(row["ServerGroupDesc"].ToString());
                        }
                    }
                }
            }

            catch
            {
                throw;
            }
        }

        private void cbServerGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbServerGroup.Text != "")
            {
                gbDev.Enabled = true;
                gbUAT.Enabled = true;
                gbPreProd.Enabled = true;
                gbProd.Enabled = true;

                tServerAliasDev.Text = "";
                tServerNameDev.Text = "";
                cbServerRoleDev.Text = "Development";
                tReleaseOrderDev.Text = "1";

                tServerAliasUAT.Text = "";
                tServerNameUAT.Text = "";
                cbServerRoleUAT.Text = "User Acceptance Testing";
                tReleaseOrderUAT.Text = "2";

                tServerAliasPreProd.Text = "";
                tServerNamePreProd.Text = "";
                cbServerRolePreProd.Text = "Pre-Production";
                tReleaseOrderPreProd.Text = "3";

                tServerAliasProd.Text = "";
                tServerNameProd.Text = "";
                cbServerRoleProd.Text = "Production";
                tReleaseOrderProd.Text = "4";

                try
                {
                    using (DataStuff sn = new DataStuff())
                    {
                        DataTable dt = sn.GetServersInGroupDetail(cbServerGroup.Text);

                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row["ServerRoleDesc"].ToString() == "Development")
                                {
                                    tServerAliasDev.Text = row["ServerAliasDesc"].ToString();
                                    tServerNameDev.Text = row["ServerName"].ToString();
                                    cbServerRoleDev.Text = row["ServerRoleDesc"].ToString();
                                    tReleaseOrderDev.Text = row["ReleaseOrder"].ToString();
                                }

                                if (row["ServerRoleDesc"].ToString() == "User Acceptance Testing")
                                {
                                    tServerAliasUAT.Text = row["ServerAliasDesc"].ToString();
                                    tServerNameUAT.Text = row["ServerName"].ToString();
                                    cbServerRoleUAT.Text = row["ServerRoleDesc"].ToString();
                                    tReleaseOrderUAT.Text = row["ReleaseOrder"].ToString();
                                }

                                if (row["ServerRoleDesc"].ToString() == "Pre-Production")
                                {
                                    tServerAliasPreProd.Text = row["ServerAliasDesc"].ToString();
                                    tServerNamePreProd.Text = row["ServerName"].ToString();
                                    cbServerRolePreProd.Text = row["ServerRoleDesc"].ToString();
                                    tReleaseOrderPreProd.Text = row["ReleaseOrder"].ToString();
                                }

                                if (row["ServerRoleDesc"].ToString() == "Production")
                                {
                                    tServerAliasProd.Text = row["ServerAliasDesc"].ToString();
                                    tServerNameProd.Text = row["ServerName"].ToString();
                                    cbServerRoleProd.Text = row["ServerRoleDesc"].ToString();
                                    tReleaseOrderProd.Text = row["ReleaseOrder"].ToString();
                                }
                            }

                            tsbSave.Enabled = true;
                        }
                    }
                }

                catch
                {
                    throw;
                }
            }
        }

        private bool DoValidation()
        {
            if (cbServerGroup.Visible)
            {
                if (cbServerGroup.Text == "")
                {
                    MessageBox.Show("Please select a server group name.", "Server Settings", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cbServerGroup.Focus();
                    return false;
                }
            }

            if (tServerGroup.Visible)
            {
                if (tServerGroup.Text == "")
                {
                    MessageBox.Show("Please enter a server group name.", "Server Settings", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tServerGroup.Focus();
                    return false;
                }
            }

            if (tServerAliasDev.Text == "")
            {
                MessageBox.Show("Please enter a development server alias name.", "Server Settings", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tServerAliasDev.Focus();
                return false;
            }

            if (tServerNameDev.Text == "")
            {
                MessageBox.Show("Please enter a server name.", "Server Settings", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tServerNameDev.Focus();
                return false;
            }

            if (cbServerRoleDev.Text == "")
            {
                MessageBox.Show("Please select a server role.", "Server Settings", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cbServerRoleDev.Focus();
                return false;
            }
            
            if (tReleaseOrderDev.Text == "")
            {
                MessageBox.Show("Please enter a release order for this server.", "Server Settings", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tReleaseOrderDev.Focus();
                return false;
            }

            if (tServerAliasUAT.Text != "")
            {
                if (tServerNameUAT.Text == "")
                {
                    MessageBox.Show("Please enter a server name.", "Server Settings", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tServerNameUAT.Focus();
                    return false;
                }

                if (cbServerRoleUAT.Text == "")
                {
                    MessageBox.Show("Please select a server role.", "Server Settings", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cbServerRoleUAT.Focus();
                    return false;
                }

                if (tReleaseOrderUAT.Text == "")
                {
                    MessageBox.Show("Please enter a release order for this server.", "Server Settings", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tReleaseOrderUAT.Focus();
                    return false;
                }
            }

            if (tServerAliasPreProd.Text != "")
            {
                if (tServerNamePreProd.Text == "")
                {
                    MessageBox.Show("Please enter a server name.", "Server Settings", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tServerNamePreProd.Focus();
                    return false;
                }

                if (cbServerRolePreProd.Text == "")
                {
                    MessageBox.Show("Please select a server role.", "Server Settings", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cbServerRolePreProd.Focus();
                    return false;
                }

                if (tReleaseOrderPreProd.Text == "")
                {
                    MessageBox.Show("Please enter a release order for this server.", "Server Settings", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tReleaseOrderPreProd.Focus();
                    return false;
                }
            }

            if (tServerAliasProd.Text != "")
            {
                if (tServerNameProd.Text == "")
                {
                    MessageBox.Show("Please enter a server name.", "Server Settings", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tServerNameProd.Focus();
                    return false;
                }

                if (cbServerRoleProd.Text == "")
                {
                    MessageBox.Show("Please select a server role.", "Server Settings", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cbServerRoleProd.Focus();
                    return false;
                }

                if (tReleaseOrderProd.Text == "")
                {
                    MessageBox.Show("Please enter a release order for this server.", "Server Settings", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tReleaseOrderProd.Focus();
                    return false;
                }
            }

            return true;
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            tsbSave.Enabled = true;
            gbDev.Enabled = true;
            gbUAT.Enabled = true;
            gbPreProd.Enabled = true;
            gbProd.Enabled = true;

            tServerGroup.Visible = true;
            cbServerGroup.Visible = false;

            tServerAliasDev.Text = "";
            tServerNameDev.Text = "";
            cbServerRoleDev.Text = "Development";
            tReleaseOrderDev.Text = "1";

            tServerAliasUAT.Text = "";
            tServerNameUAT.Text = "";
            cbServerRoleUAT.Text = "User Acceptance Testing";
            tReleaseOrderUAT.Text = "2";

            tServerAliasPreProd.Text = "";
            tServerNamePreProd.Text = "";
            cbServerRolePreProd.Text = "Pre-Production";
            tReleaseOrderPreProd.Text = "3";

            tServerAliasProd.Text = "";
            tServerNameProd.Text = "";
            cbServerRoleProd.Text = "Production";
            tReleaseOrderProd.Text = "4";

            tServerGroup.Focus();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            string ServerGroup = string.Empty;
            bool DevSaved = false;
            bool UATSaved = false;
            bool ProdSaved = false;
            bool PreProdSaved = false;

            if (!DoValidation())
            {
                return;
            }

            if (cbServerGroup.Visible)
            {
                ServerGroup = cbServerGroup.Text;
            }
            else
            {
                ServerGroup = tServerGroup.Text;
            }

            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    if (tServerAliasDev.Text.Trim() != "")
                    {
                        sn.SaveServerGroup(ServerGroup, tServerAliasDev.Text, tServerNameDev.Text, cbServerRoleDev.Text, tReleaseOrderDev.Text);
                        DevSaved = true;
                    }

                    if (tServerAliasUAT.Text.Trim() != "")
                    {
                        sn.SaveServerGroup(ServerGroup, tServerAliasUAT.Text, tServerNameUAT.Text, cbServerRoleUAT.Text, tReleaseOrderUAT.Text);
                        UATSaved = true;
                    }

                    if (tServerAliasPreProd.Text.Trim() != "")
                    {
                        sn.SaveServerGroup(ServerGroup, tServerAliasPreProd.Text, tServerNamePreProd.Text, cbServerRolePreProd.Text, tReleaseOrderPreProd.Text);
                        PreProdSaved = true;
                    }

                    if (tServerAliasProd.Text.Trim() != "")
                    {
                        sn.SaveServerGroup(ServerGroup, tServerAliasProd.Text, tServerNameProd.Text, cbServerRoleProd.Text, tReleaseOrderProd.Text);
                        ProdSaved = true;
                    }

                    if ((DevSaved) || (UATSaved) || (ProdSaved) || (PreProdSaved))
                    {
                        MessageBox.Show("Server group saved.", "Server Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tServerGroup.Visible = false;
                        cbServerGroup.Visible = true;
                        LoadServerGroups();
                    }
                    else
                    {
                        MessageBox.Show("There was an error saving the server group.", "Server Settings", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }

            catch
            {

            }
        }
    }
}
