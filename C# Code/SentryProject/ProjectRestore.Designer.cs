namespace SentryProject
{
    partial class ProjectRestore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectRestore));
            this.gbGrid = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdRestore = new System.Windows.Forms.Button();
            this.cActiveOnly = new System.Windows.Forms.CheckBox();
            this.cbProject = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbServers = new System.Windows.Forms.ComboBox();
            this.lStatus = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbGrid
            // 
            this.gbGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGrid.Location = new System.Drawing.Point(13, 103);
            this.gbGrid.Name = "gbGrid";
            this.gbGrid.Size = new System.Drawing.Size(939, 420);
            this.gbGrid.TabIndex = 3;
            this.gbGrid.TabStop = false;
            this.gbGrid.Text = "Project DB Objects";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cmdRestore);
            this.groupBox1.Controls.Add(this.cActiveOnly);
            this.groupBox1.Controls.Add(this.cbProject);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbServers);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(940, 84);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Project Detail";
            // 
            // cmdRestore
            // 
            this.cmdRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRestore.Image = ((System.Drawing.Image)(resources.GetObject("cmdRestore.Image")));
            this.cmdRestore.Location = new System.Drawing.Point(859, 16);
            this.cmdRestore.Name = "cmdRestore";
            this.cmdRestore.Size = new System.Drawing.Size(75, 62);
            this.cmdRestore.TabIndex = 60;
            this.cmdRestore.Text = "Restore";
            this.cmdRestore.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdRestore.UseVisualStyleBackColor = true;
            this.cmdRestore.Click += new System.EventHandler(this.cmdRestore_Click);
            // 
            // cActiveOnly
            // 
            this.cActiveOnly.AutoSize = true;
            this.cActiveOnly.Checked = true;
            this.cActiveOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cActiveOnly.Location = new System.Drawing.Point(523, 23);
            this.cActiveOnly.Name = "cActiveOnly";
            this.cActiveOnly.Size = new System.Drawing.Size(121, 17);
            this.cActiveOnly.TabIndex = 59;
            this.cActiveOnly.Text = "Only Active Projects";
            this.cActiveOnly.UseVisualStyleBackColor = true;
            // 
            // cbProject
            // 
            this.cbProject.FormattingEnabled = true;
            this.cbProject.Location = new System.Drawing.Point(105, 19);
            this.cbProject.Name = "cbProject";
            this.cbProject.Size = new System.Drawing.Size(412, 21);
            this.cbProject.TabIndex = 57;
            this.cbProject.SelectedIndexChanged += new System.EventHandler(this.cbProject_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(59, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 58;
            this.label5.Text = "Project";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 56;
            this.label2.Text = "Restore to Server";
            // 
            // cbServers
            // 
            this.cbServers.FormattingEnabled = true;
            this.cbServers.Location = new System.Drawing.Point(105, 46);
            this.cbServers.Name = "cbServers";
            this.cbServers.Size = new System.Drawing.Size(240, 21);
            this.cbServers.TabIndex = 55;
            this.cbServers.SelectedIndexChanged += new System.EventHandler(this.cbServers_SelectedIndexChanged);
            // 
            // lStatus
            // 
            this.lStatus.AutoSize = true;
            this.lStatus.Location = new System.Drawing.Point(12, 530);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(37, 13);
            this.lStatus.TabIndex = 4;
            this.lStatus.Text = "Status";
            // 
            // ProjectRestore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1207, 686);
            this.Controls.Add(this.lStatus);
            this.Controls.Add(this.gbGrid);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProjectRestore";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Restore";
            this.Load += new System.EventHandler(this.ProjectRestore_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbGrid;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmdRestore;
        private System.Windows.Forms.CheckBox cActiveOnly;
        private System.Windows.Forms.ComboBox cbProject;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbServers;
        private System.Windows.Forms.Label lStatus;
    }
}