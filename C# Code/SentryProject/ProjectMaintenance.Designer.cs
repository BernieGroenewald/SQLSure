namespace SentryProject
{
    partial class ProjectMaintenance
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectMaintenance));
            this.project1 = new SentryProject.Project();
            this.SuspendLayout();
            // 
            // project1
            // 
            this.project1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.project1.IsApproveOnly = false;
            this.project1.IsProjectOnly = true;
            this.project1.Location = new System.Drawing.Point(12, 12);
            this.project1.Name = "project1";
            this.project1.Size = new System.Drawing.Size(1055, 546);
            this.project1.TabIndex = 0;
            // 
            // ProjectMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 570);
            this.Controls.Add(this.project1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProjectMaintenance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Maintenance";
            this.Load += new System.EventHandler(this.ProjectMaintenance_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Project project1;
    }
}