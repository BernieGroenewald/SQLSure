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
    public partial class FilterObject : Form
    {
        public string FilterString = string.Empty;
        public string PoPFilter = string.Empty;

        public FilterObject()
        {
            InitializeComponent();
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            FilterString = tFilter.Text;
            PoPFilter = cbPartOfProject.Text;
            this.Close();
        }

        private void FilterObject_Load(object sender, EventArgs e)
        {
            tFilter.Text = FilterString;
            LoadProjects();
        }

        private void LoadProjects()
        {
            try
            {
                cbPartOfProject.Items.Clear();

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetProjects();

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            cbPartOfProject.Items.Add(row["ProjectName"].ToString());
                        }
                    }
                }
            }

            catch
            {
                throw;
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
