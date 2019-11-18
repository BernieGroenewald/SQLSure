using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SentryControls
{
    public partial class ReleaseProject : Form
    {
        private bool ProjectChanged = false;

        public ReleaseProject()
        {
            InitializeComponent();
        }

        private void ReleaseProject_Load(object sender, EventArgs e)
        {
            UCProject.ProjectSaved += IsProjectSaved;
            UCProject.Width = this.Width - 20;
            UCProject.Height = this.Height - 20;
        }

        private void ReleaseProject_Resize(object sender, EventArgs e)
        {
            UCProject.Width = this.Width - 20;
            UCProject.Height = this.Height - 20;
        }

        private void IsProjectSaved(object sender, EventArgs e)
        {
            ProjectChanged = UCProject.ProjectHasChanged;
        }

        private void ReleaseProject_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool SaveSuccess = false;

            if (ProjectChanged)
            {
                DialogResult result = MessageBox.Show("The project has changed. Do you want to save these changes?", "Project Maintenance", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SaveSuccess = UCProject.DoSaveProject();

                    if (!SaveSuccess)
                    {
                        e.Cancel = true;
                        return;
                    }
                }

                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }
    }
}
