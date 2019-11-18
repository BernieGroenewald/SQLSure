using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using SentryDataStuff;

namespace SentryGeneral
{
    public partial class DataStore : Form
    {
        public string DataStoreConnectionString = string.Empty;

        public DataStore()
        {
            InitializeComponent();
        }

        private void DataStore_Load(object sender, EventArgs e)
        {
            General gs = new General();

            tServerName.Text = gs.DataStoreServerName;

            if (gs.DataStoreIntegratedSecurity == "Y")
            {
                cIntegratedSecurity.Checked = true;
            }
            else
            {
                cIntegratedSecurity.Checked = false;
            }

            tUserName.Text = gs.DataStoreUserName;
            tPassword.Text = gs.DataStorePassword;
            tDefaultDatabase.Text = gs.DataStoreDefaultDatabase;
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
        }

        private bool DoValidation()
        {
            if (tServerName.Text == "")
            {
                MessageBox.Show("Please enter a server name.", "Data Store Settings", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tServerName.Focus();
                return false;
            }

            if (!cIntegratedSecurity.Checked)
            {
                if (tUserName.Text == "")
                {
                    MessageBox.Show("Please enter a user name.", "Data Store Settings", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tUserName.Focus();
                    return false;
                }

                if (tPassword.Text == "")
                {
                    MessageBox.Show("Please enter a user password.", "Data Store Settings", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tPassword.Focus();
                    return false;
                }
            }

            if (tDefaultDatabase.Text == "")
            {
                MessageBox.Show("Please enter a default database name.", "Data Store Settings", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tDefaultDatabase.Focus();
                return false;
            }

            return true;
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (!DoValidation())
            {
                return;
            }

            General gs = new General();
            gs.DataStoreServerName = tServerName.Text;

            if (cIntegratedSecurity.Checked)
            {
                gs.DataStoreIntegratedSecurity = "Y";
            }
            else
            {
                gs.DataStoreIntegratedSecurity = "N";
            }

            gs.DataStoreUserName = tUserName.Text;
            gs.DataStorePassword = tPassword.Text;
            gs.DataStoreDefaultDatabase = tDefaultDatabase.Text;

            DataStoreConnectionString = gs.DataStoreConnectionString;

            MessageBox.Show("The data store configuration settings have been saved.", "Data Store Configuration", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void tServerName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
