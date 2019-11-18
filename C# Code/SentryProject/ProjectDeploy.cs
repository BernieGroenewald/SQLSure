using System.Windows.Forms;

namespace SentryProject
{
    public partial class ProjectDeploy : Form
    {
        public ProjectDeploy()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectDeploy));
            this.project1 = new SentryProject.Project();
            this.SuspendLayout();
            // 
            // project1
            // 
            this.project1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.project1.IsApproveOnly = false;
            this.project1.IsProjectOnly = false;
            this.project1.Location = new System.Drawing.Point(12, 10);
            this.project1.Name = "project1";
            this.project1.Size = new System.Drawing.Size(1176, 628);
            this.project1.TabIndex = 0;
            // 
            // ProjectDeploy
            // 
            this.ClientSize = new System.Drawing.Size(1191, 658);
            this.Controls.Add(this.project1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProjectDeploy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }
    }
}
