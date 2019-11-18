namespace SentryControls
{
    partial class ReleaseProject
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReleaseProject));
            this.UCProject = new SentryControls.Project();
            this.SuspendLayout();
            // 
            // UCProject
            // 
            this.UCProject.IsProjectOnly = false;
            this.UCProject.Location = new System.Drawing.Point(13, 13);
            this.UCProject.Name = "UCProject";
            this.UCProject.Size = new System.Drawing.Size(1463, 765);
            this.UCProject.TabIndex = 0;
            // 
            // ReleaseProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 703);
            this.Controls.Add(this.UCProject);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReleaseProject";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Release Project";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReleaseProject_FormClosing);
            this.Load += new System.EventHandler(this.ReleaseProject_Load);
            this.Resize += new System.EventHandler(this.ReleaseProject_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private Project UCProject;
    }
}