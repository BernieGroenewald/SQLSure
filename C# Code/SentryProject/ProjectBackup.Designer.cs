namespace SentryProject
{
    partial class ProjectBackup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectBackup));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cActiveOnly = new System.Windows.Forms.CheckBox();
            this.cbProject = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbServers = new System.Windows.Forms.ComboBox();
            this.gbGrid = new System.Windows.Forms.GroupBox();
            this.cmdBackup = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cmdBackup);
            this.groupBox1.Controls.Add(this.cActiveOnly);
            this.groupBox1.Controls.Add(this.cbProject);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbServers);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(940, 84);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Project Detail";
            // 
            // cActiveOnly
            // 
            this.cActiveOnly.AutoSize = true;
            this.cActiveOnly.Checked = true;
            this.cActiveOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cActiveOnly.Location = new System.Drawing.Point(354, 49);
            this.cActiveOnly.Name = "cActiveOnly";
            this.cActiveOnly.Size = new System.Drawing.Size(121, 17);
            this.cActiveOnly.TabIndex = 59;
            this.cActiveOnly.Text = "Only Active Projects";
            this.cActiveOnly.UseVisualStyleBackColor = true;
            this.cActiveOnly.CheckedChanged += new System.EventHandler(this.cActiveOnly_CheckedChanged);
            // 
            // cbProject
            // 
            this.cbProject.FormattingEnabled = true;
            this.cbProject.Location = new System.Drawing.Point(107, 46);
            this.cbProject.Name = "cbProject";
            this.cbProject.Size = new System.Drawing.Size(240, 21);
            this.cbProject.TabIndex = 57;
            this.cbProject.SelectedIndexChanged += new System.EventHandler(this.cbProject_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(61, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 58;
            this.label5.Text = "Project";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 56;
            this.label2.Text = "Server";
            // 
            // cbServers
            // 
            this.cbServers.FormattingEnabled = true;
            this.cbServers.Location = new System.Drawing.Point(107, 19);
            this.cbServers.Name = "cbServers";
            this.cbServers.Size = new System.Drawing.Size(240, 21);
            this.cbServers.TabIndex = 55;
            this.cbServers.SelectedIndexChanged += new System.EventHandler(this.cbServers_SelectedIndexChanged);
            // 
            // gbGrid
            // 
            this.gbGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGrid.Location = new System.Drawing.Point(13, 103);
            this.gbGrid.Name = "gbGrid";
            this.gbGrid.Size = new System.Drawing.Size(939, 420);
            this.gbGrid.TabIndex = 1;
            this.gbGrid.TabStop = false;
            this.gbGrid.Text = "Project DB Objects";
            // 
            // cmdBackup
            // 
            this.cmdBackup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBackup.Image = ((System.Drawing.Image)(resources.GetObject("cmdBackup.Image")));
            this.cmdBackup.Location = new System.Drawing.Point(859, 16);
            this.cmdBackup.Name = "cmdBackup";
            this.cmdBackup.Size = new System.Drawing.Size(75, 62);
            this.cmdBackup.TabIndex = 60;
            this.cmdBackup.Text = "Backup";
            this.cmdBackup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdBackup.UseVisualStyleBackColor = true;
            this.cmdBackup.Click += new System.EventHandler(this.cmdBackup_Click);
            // 
            // ProjectBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 535);
            this.Controls.Add(this.gbGrid);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProjectBackup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Backup";
            this.Load += new System.EventHandler(this.ProjectBackup_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbServers;
        private System.Windows.Forms.ComboBox cbProject;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cActiveOnly;
        private System.Windows.Forms.GroupBox gbGrid;
        private System.Windows.Forms.Button cmdBackup;
    }
}