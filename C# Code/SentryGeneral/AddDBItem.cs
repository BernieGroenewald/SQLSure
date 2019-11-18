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

namespace SentryGeneral
{
    public partial class AddDBItem : Form
    {
        public string ConnectionString = string.Empty;
        public string DatabaseName = string.Empty;
        public string ObjectName = string.Empty;

        public AddDBItem()
        {
            InitializeComponent();
        }

        private void cbDatabaseName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatabaseName = cbDatabaseName.Text;
            LoadDatabaseObjects();
        }

        private void cbObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObjectName = cbObjects.Text;
        }

        private void AddDBItem_Load(object sender, EventArgs e)
        {
            LoadDatabaseNames();
        }

        private void LoadDatabaseNames()
        {
            try
            {
                cbDatabaseName.Items.Clear();

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetSystemDatabases(ConnectionString);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            cbDatabaseName.Items.Add(row["Name"].ToString());
                        }
                    }
                }
            }

            catch
            {
                throw;
            }
        }

        private void LoadDatabaseObjects()
        {
            try
            {
                cbObjects.Items.Clear();

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetDatabaseObjects(ConnectionString, cbDatabaseName.Text, "10000");

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            cbObjects.Items.Add(row["Name"].ToString());
                        }
                    }
                }
            }

            catch (Exception ex)
            {
            }
        }

        private void cmdAddItem_Click(object sender, EventArgs e)
        {
            if (cbDatabaseName.Text.Trim() == "")
            {
                MessageBox.Show("Please select a database name.", "Add Object", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (cbObjects.Text.Trim() == "")
            {
                MessageBox.Show("Please select a database object.", "Add Object", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            this.Close();
        }
    }
}
