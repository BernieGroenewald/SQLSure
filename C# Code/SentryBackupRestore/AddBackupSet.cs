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

namespace SentryBackupRestore
{
    public partial class AddBackupSet : Form
    {
        int UserID = 0;
        public string NewBackupSetName = string.Empty;

        public AddBackupSet()
        {
            InitializeComponent();
        }
        
        private void AddBackupSet_Load(object sender, EventArgs e)
        {
            using (DataStuff sn = new DataStuff())
            {
                DataTable dt = sn.SingleUser(Environment.UserName, "AddProject");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        UserID = Convert.ToInt32(row["UserID"].ToString());
                    }
                }
            }

            GetServers(UserID);
        }

        private void GetServers(int UserID)
        {
            try
            {
                cbServerAlias.Items.Clear();

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetServers(UserID);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            cbServerAlias.Items.Add(row["ServerAliasDesc"].ToString());
                        }
                    }
                }
            }

            catch
            {
                throw;
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (tBackupSetName.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a backup set name.", "Add Backup Set", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (cbServerAlias.Text.Trim() == "")
            {
                MessageBox.Show("Please select a server alias for this backup set.", "Add Backup Set", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            using (DataStuff sn = new DataStuff())
            {
                sn.SaveBackupSet(tBackupSetName.Text, tBackupSetDescription.Text, UserID, cbServerAlias.Text, "Y");
                NewBackupSetName = tBackupSetName.Text;

                MessageBox.Show("Backup set saved.", "Add Backup Set", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
    }
}
