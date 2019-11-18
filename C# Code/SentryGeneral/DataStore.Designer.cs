namespace SentryGeneral
{
    partial class DataStore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataStore));
            this.cIntegratedSecurity = new System.Windows.Forms.CheckBox();
            this.tPassword = new System.Windows.Forms.TextBox();
            this.tUserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tServerName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbTestConnection = new System.Windows.Forms.ToolStripButton();
            this.tDefaultDatabase = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cIntegratedSecurity
            // 
            this.cIntegratedSecurity.AutoSize = true;
            this.cIntegratedSecurity.Location = new System.Drawing.Point(124, 105);
            this.cIntegratedSecurity.Name = "cIntegratedSecurity";
            this.cIntegratedSecurity.Size = new System.Drawing.Size(115, 17);
            this.cIntegratedSecurity.TabIndex = 54;
            this.cIntegratedSecurity.Text = "Integrated Security";
            this.cIntegratedSecurity.UseVisualStyleBackColor = true;
            this.cIntegratedSecurity.CheckedChanged += new System.EventHandler(this.cIntegratedSecurity_CheckedChanged);
            // 
            // tPassword
            // 
            this.tPassword.Location = new System.Drawing.Point(124, 154);
            this.tPassword.Name = "tPassword";
            this.tPassword.PasswordChar = '*';
            this.tPassword.Size = new System.Drawing.Size(154, 20);
            this.tPassword.TabIndex = 56;
            // 
            // tUserName
            // 
            this.tUserName.Location = new System.Drawing.Point(124, 128);
            this.tUserName.Name = "tUserName";
            this.tUserName.Size = new System.Drawing.Size(154, 20);
            this.tUserName.TabIndex = 55;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(58, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 58;
            this.label3.Text = "User Name";
            // 
            // tServerName
            // 
            this.tServerName.Location = new System.Drawing.Point(124, 79);
            this.tServerName.Name = "tServerName";
            this.tServerName.Size = new System.Drawing.Size(278, 20);
            this.tServerName.TabIndex = 53;
            this.tServerName.TextChanged += new System.EventHandler(this.tServerName_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(65, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 59;
            this.label4.Text = "Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 57;
            this.label2.Text = "Server Name";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSave,
            this.toolStripSeparator1,
            this.tsbTestConnection});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(484, 54);
            this.toolStrip1.TabIndex = 60;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbSave
            // 
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(36, 51);
            this.tsbSave.Text = "Save";
            this.tsbSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbSave.ToolTipText = "Save Server";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 54);
            // 
            // tsbTestConnection
            // 
            this.tsbTestConnection.Image = ((System.Drawing.Image)(resources.GetObject("tsbTestConnection.Image")));
            this.tsbTestConnection.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbTestConnection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTestConnection.Name = "tsbTestConnection";
            this.tsbTestConnection.Size = new System.Drawing.Size(36, 51);
            this.tsbTestConnection.Text = "Test";
            this.tsbTestConnection.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbTestConnection.ToolTipText = "Test Server Connection";
            this.tsbTestConnection.Click += new System.EventHandler(this.tsbTestConnection_Click);
            // 
            // tDefaultDatabase
            // 
            this.tDefaultDatabase.Location = new System.Drawing.Point(124, 180);
            this.tDefaultDatabase.Name = "tDefaultDatabase";
            this.tDefaultDatabase.Size = new System.Drawing.Size(154, 20);
            this.tDefaultDatabase.TabIndex = 61;
            this.tDefaultDatabase.Text = "SQLSure";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 183);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 62;
            this.label1.Text = "Default Database";
            // 
            // DataStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 243);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tDefaultDatabase);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.cIntegratedSecurity);
            this.Controls.Add(this.tPassword);
            this.Controls.Add(this.tUserName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tServerName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DataStore";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data Store Configuration";
            this.Load += new System.EventHandler(this.DataStore_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cIntegratedSecurity;
        private System.Windows.Forms.TextBox tPassword;
        private System.Windows.Forms.TextBox tUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tServerName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbTestConnection;
        private System.Windows.Forms.TextBox tDefaultDatabase;
        private System.Windows.Forms.Label label1;
    }
}