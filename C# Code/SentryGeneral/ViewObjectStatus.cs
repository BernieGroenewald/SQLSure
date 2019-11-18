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
    public partial class ViewObjectStatus : Form
    {
        public string AliasName = string.Empty;
        public string ObjectName = string.Empty;

        public ViewObjectStatus()
        {
            InitializeComponent();
        }

        private void ViewObjectStatus_Load(object sender, EventArgs e)
        {
            try
            {
                gbCheckoutStatus.Text = "Status for " + ObjectName + " on " + AliasName;

                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetObjectStatusDetail(AliasName, ObjectName);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            tCurrentStatus.Text = row["ObjectStatusDesc"].ToString();
                            tLastStatusDate.Text = row["HistoryDate"].ToString();
                            tResponsiblePerson.Text = row["UserName"].ToString();
                            tComment.Text = row["Comment"].ToString();
                            tPartOfProject.Text = row["ProjectName"].ToString();
                        }
                    }
                }
            }

            catch
            {
                throw;
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
