namespace SentryControls
{
    partial class AddProject
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
            this.cbServerGroup = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tProjectDescription = new System.Windows.Forms.TextBox();
            this.tProjectName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cbServerGroup
            // 
            this.cbServerGroup.FormattingEnabled = true;
            this.cbServerGroup.Location = new System.Drawing.Point(93, 55);
            this.cbServerGroup.Name = "cbServerGroup";
            this.cbServerGroup.Size = new System.Drawing.Size(239, 21);
            this.cbServerGroup.TabIndex = 64;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 69;
            this.label5.Text = "Server Group";
            // 
            // cmdSave
            // 
            this.cmdSave.AutoSize = true;
            this.cmdSave.Location = new System.Drawing.Point(254, 184);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(78, 23);
            this.cmdSave.TabIndex = 68;
            this.cmdSave.Text = "Save Project";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 67;
            this.label2.Text = "Description";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 65;
            this.label1.Text = "Project Name";
            // 
            // tProjectDescription
            // 
            this.tProjectDescription.Location = new System.Drawing.Point(93, 82);
            this.tProjectDescription.Multiline = true;
            this.tProjectDescription.Name = "tProjectDescription";
            this.tProjectDescription.Size = new System.Drawing.Size(239, 96);
            this.tProjectDescription.TabIndex = 66;
            // 
            // tProjectName
            // 
            this.tProjectName.Location = new System.Drawing.Point(93, 29);
            this.tProjectName.Name = "tProjectName";
            this.tProjectName.Size = new System.Drawing.Size(239, 20);
            this.tProjectName.TabIndex = 63;
            // 
            // AddProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 228);
            this.Controls.Add(this.cbServerGroup);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tProjectDescription);
            this.Controls.Add(this.tProjectName);
            this.Name = "AddProject";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Project";
            this.Load += new System.EventHandler(this.AddProject_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbServerGroup;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tProjectDescription;
        private System.Windows.Forms.TextBox tProjectName;
    }
}