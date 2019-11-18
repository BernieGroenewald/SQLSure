using System.Windows.Forms;

namespace SentryProject
{
    public partial class ProjectMaintenance : Form
    {
        public string ConnectionString = string.Empty;

        public ProjectMaintenance()
        {
            InitializeComponent();
        }

        private void ProjectMaintenance_Load(object sender, System.EventArgs e)
        {
            Project p = new Project();
            p.ConnReleaseFromServer = ConnectionString;
            p.IsProjectOnly = true;
            p.Parent = this;
            p.Left = 10;
            p.Top = 10;
        }
    }
}
