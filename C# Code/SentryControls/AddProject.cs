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

namespace SentryControls
{
    public partial class AddProject : Form
    {
        int UserID = 0;
        public string NewProjectName = string.Empty;

        public AddProject()
        {
            InitializeComponent();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (tProjectName.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a project name.", "Add Project", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (cbServerGroup.Text.Trim() == "")
            {
                MessageBox.Show("Please select a server group for this project.", "Add Project", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            using (DataStuff sn = new DataStuff())
            {
                sn.SaveProject(tProjectName.Text, tProjectDescription.Text, UserID, cbServerGroup.Text, "Y");
                NewProjectName = tProjectName.Text;

                MessageBox.Show("Project saved.", "Add Project", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void AddProject_Load(object sender, EventArgs e)
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

            LoadServerGroups();
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
    }
}
