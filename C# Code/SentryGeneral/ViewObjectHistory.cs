using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SentryDataStuff;

namespace SentryGeneral
{
    public partial class ViewObjectHistory : Form
    {
        public string ObjectDescription = string.Empty;
        public string ObjectText = string.Empty;
        public string DatabaseName = string.Empty;
        public string ObjectName = string.Empty;
        public string AliasName = string.Empty;

        public string ConnectionString = string.Empty;

        public ViewObjectHistory()
        {
            InitializeComponent();
        }

        private void ViewObjectHistory_Load(object sender, EventArgs e)
        {
            LoadObjectHistory();

            dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
            dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvHistory.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvHistory.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvHistory.ColumnHeadersDefaultCellStyle.Font = new Font(dgvHistory.Font, FontStyle.Bold);
            dgvHistory.RowTemplate.Height = 30;
            dgvHistory.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvHistory.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgvHistory.GridColor = Color.Black;
        }

        private void LoadObjectHistory()
        {
            try
            {
                using (DataStuff sn = new DataStuff())
                {
                    DataTable dt = sn.GetObjectHistory(DatabaseName, ObjectName);

                    if (dt.Rows.Count > 0)
                    {
                        dgvHistory.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("No history was found for object " + ObjectName + ".", "Object History", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }

            catch
            {
                throw;
            }
        }

        private void dgvHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                ObjectHistoryViewer hv = new ObjectHistoryViewer();

                hv.ObjectText = Convert.ToString(dgvHistory[9, e.RowIndex].Value);
                hv.DatabaseName = Convert.ToString(dgvHistory[5, e.RowIndex].Value);
                hv.ObjectName = Convert.ToString(dgvHistory[4, e.RowIndex].Value);
                hv.AliasName = Convert.ToString(dgvHistory[7, e.RowIndex].Value);
                hv.HistoryDate = Convert.ToString(dgvHistory[2, e.RowIndex].Value);

                hv.ShowDialog();
            }
        }
    }
}
