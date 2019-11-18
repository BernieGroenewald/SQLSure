using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SentryGeneral
{
    public partial class ScriptFileView : Form
    {
        public string FileText = string.Empty;

        public ScriptFileView()
        {
            InitializeComponent();
        }

        private void ScriptFileView_Load(object sender, EventArgs e)
        {
            tFile.Text = FileText;

            tFile.SelectionLength = 0;
        }

        private void cmdDone_Click(object sender, EventArgs e)
        {
            FileText = tFile.Text;
            this.Close();
        }

        private void ScriptFileView_Resize(object sender, EventArgs e)
        {
            tFile.Width = this.Width - (2 * tFile.Left);
            tFile.Height = this.Height - (2 * tFile.Top) - cmdDone.Height - 70;
            tFile.Refresh();

            cmdDone.Left = tFile.Left + tFile.Width - cmdDone.Width - 20;
            cmdDone.Top = tFile.Top + tFile.Height + 10;
        }

        private void ScriptFileView_ResizeEnd(object sender, EventArgs e)
        {
            tFile.Width = this.Width - (2 * tFile.Left);
            tFile.Height = this.Height - (2 * tFile.Top) - cmdDone.Height - 70;
            tFile.Refresh();

            cmdDone.Left = tFile.Left + tFile.Width - cmdDone.Width - 20;
            cmdDone.Top = tFile.Top + tFile.Height + 10;
        }
    }
}
