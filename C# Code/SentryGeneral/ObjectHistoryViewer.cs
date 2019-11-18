using System;
using System.Windows.Forms;

namespace SentryGeneral
{
    public partial class ObjectHistoryViewer : Form
    {
        public string ObjectText = string.Empty;
        public string DatabaseName = string.Empty;
        public string ObjectName = string.Empty;
        public string AliasName = string.Empty;
        public string HistoryDate = string.Empty;

        public ObjectHistoryViewer()
        {
            InitializeComponent();
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ObjectHistoryViewer_Load(object sender, EventArgs e)
        {
            lObjectDescription.Text = AliasName + " - " + DatabaseName + " - " + HistoryDate;
            tObjectText.Text = ObjectText;
        }

        private void ObjectHistoryViewer_Resize(object sender, EventArgs e)
        {
            tObjectText.Width = this.Width - (2 * tObjectText.Left);
            tObjectText.Height = this.Height - (2 * tObjectText.Top);
            tObjectText.Refresh();
        }

        private void ObjectHistoryViewer_ResizeEnd(object sender, EventArgs e)
        {
            tObjectText.Width = this.Width - (2 * tObjectText.Left);
            tObjectText.Height = this.Height - (2 * tObjectText.Top);
            tObjectText.Refresh();
        }
    }
}
