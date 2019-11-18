namespace SentryAdmin
{
    partial class ServerSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerSettings));
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.cIntegratedSecurity = new System.Windows.Forms.CheckBox();
            this.tsbNew = new System.Windows.Forms.ToolStripButton();
            this.tPassword = new System.Windows.Forms.TextBox();
            this.tUserName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbServerName = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbTestConnection = new System.Windows.Forms.ToolStripButton();
            this.label3 = new System.Windows.Forms.Label();
            this.tServerName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tServerRole = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
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
            // cIntegratedSecurity
            // 
            this.cIntegratedSecurity.AutoSize = true;
            this.cIntegratedSecurity.Location = new System.Drawing.Point(104, 154);
            this.cIntegratedSecurity.Name = "cIntegratedSecurity";
            this.cIntegratedSecurity.Size = new System.Drawing.Size(115, 17);
            this.cIntegratedSecurity.TabIndex = 3;
            this.cIntegratedSecurity.Text = "Integrated Security";
            this.cIntegratedSecurity.UseVisualStyleBackColor = true;
            this.cIntegratedSecurity.CheckedChanged += new System.EventHandler(this.cIntegratedSecurity_CheckedChanged);
            // 
            // tsbNew
            // 
            this.tsbNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbNew.Image")));
            this.tsbNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNew.Name = "tsbNew";
            this.tsbNew.Size = new System.Drawing.Size(36, 51);
            this.tsbNew.Text = "New";
            this.tsbNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbNew.ToolTipText = "Add New Server";
            this.tsbNew.Click += new System.EventHandler(this.tsbNew_Click);
            // 
            // tPassword
            // 
            this.tPassword.Location = new System.Drawing.Point(104, 203);
            this.tPassword.Name = "tPassword";
            this.tPassword.PasswordChar = '*';
            this.tPassword.Size = new System.Drawing.Size(154, 20);
            this.tPassword.TabIndex = 5;
            this.tPassword.TextChanged += new System.EventHandler(this.tPassword_TextChanged);
            // 
            // tUserName
            // 
            this.tUserName.Location = new System.Drawing.Point(104, 177);
            this.tUserName.Name = "tUserName";
            this.tUserName.Size = new System.Drawing.Size(154, 20);
            this.tUserName.TabIndex = 4;
            this.tUserName.TextChanged += new System.EventHandler(this.tUserName_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 55;
            this.label6.Text = "Server Alias";
            // 
            // cbServerName
            // 
            this.cbServerName.FormattingEnabled = true;
            this.cbServerName.Location = new System.Drawing.Point(104, 74);
            this.cbServerName.Name = "cbServerName";
            this.cbServerName.Size = new System.Drawing.Size(278, 21);
            this.cbServerName.TabIndex = 0;
            this.cbServerName.SelectedIndexChanged += new System.EventHandler(this.cbServerName_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(35, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 56;
            this.label7.Text = "Server Role";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNew,
            this.tsbSave,
            this.toolStripSeparator1,
            this.tsbTestConnection});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(409, 54);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 51;
            this.label3.Text = "User Name";
            // 
            // tServerName
            // 
            this.tServerName.Enabled = false;
            this.tServerName.Location = new System.Drawing.Point(104, 101);
            this.tServerName.Name = "tServerName";
            this.tServerName.Size = new System.Drawing.Size(278, 20);
            this.tServerName.TabIndex = 1;
            this.tServerName.TextChanged += new System.EventHandler(this.tServerName_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 52;
            this.label4.Text = "Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Server Name";
            // 
            // tServerRole
            // 
            this.tServerRole.Enabled = false;
            this.tServerRole.Location = new System.Drawing.Point(104, 127);
            this.tServerRole.Name = "tServerRole";
            this.tServerRole.Size = new System.Drawing.Size(278, 20);
            this.tServerRole.TabIndex = 2;
            // 
            // ServerSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 252);
            this.Controls.Add(this.tServerRole);
            this.Controls.Add(this.cIntegratedSecurity);
            this.Controls.Add(this.tPassword);
            this.Controls.Add(this.tUserName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbServerName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tServerName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ServerSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Server Settings";
            this.Load += new System.EventHandler(this.ServerSettings_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.CheckBox cIntegratedSecurity;
        private System.Windows.Forms.ToolStripButton tsbNew;
        private System.Windows.Forms.TextBox tPassword;
        private System.Windows.Forms.TextBox tUserName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbServerName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbTestConnection;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tServerName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tServerRole;
    }
}