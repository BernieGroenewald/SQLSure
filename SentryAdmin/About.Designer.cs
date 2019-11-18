namespace SentryAdmin
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmdRegister = new System.Windows.Forms.Button();
            this.lCompanyName = new System.Windows.Forms.Label();
            this.lSupportDate = new System.Windows.Forms.Label();
            this.lUsers = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(13, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(222, 98);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // cmdRegister
            // 
            this.cmdRegister.Image = ((System.Drawing.Image)(resources.GetObject("cmdRegister.Image")));
            this.cmdRegister.Location = new System.Drawing.Point(360, 176);
            this.cmdRegister.Name = "cmdRegister";
            this.cmdRegister.Size = new System.Drawing.Size(96, 43);
            this.cmdRegister.TabIndex = 1;
            this.cmdRegister.Text = "Register";
            this.cmdRegister.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdRegister.UseVisualStyleBackColor = true;
            this.cmdRegister.Click += new System.EventHandler(this.cmdRegister_Click);
            // 
            // lCompanyName
            // 
            this.lCompanyName.AutoSize = true;
            this.lCompanyName.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lCompanyName.Location = new System.Drawing.Point(32, 132);
            this.lCompanyName.Name = "lCompanyName";
            this.lCompanyName.Size = new System.Drawing.Size(91, 18);
            this.lCompanyName.TabIndex = 2;
            this.lCompanyName.Text = "Registered to";
            // 
            // lSupportDate
            // 
            this.lSupportDate.AutoSize = true;
            this.lSupportDate.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lSupportDate.Location = new System.Drawing.Point(32, 160);
            this.lSupportDate.Name = "lSupportDate";
            this.lSupportDate.Size = new System.Drawing.Size(57, 18);
            this.lSupportDate.TabIndex = 3;
            this.lSupportDate.Text = "Support";
            // 
            // lUsers
            // 
            this.lUsers.AutoSize = true;
            this.lUsers.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lUsers.Location = new System.Drawing.Point(32, 188);
            this.lUsers.Name = "lUsers";
            this.lUsers.Size = new System.Drawing.Size(42, 18);
            this.lUsers.TabIndex = 4;
            this.lUsers.Text = "Users";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 220);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(443, 89);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Support";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please contact us: support@sqlsure.com";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Visit our website: www.sqlsure.com";
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 321);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lUsers);
            this.Controls.Add(this.lSupportDate);
            this.Controls.Add(this.lCompanyName);
            this.Controls.Add(this.cmdRegister);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.Load += new System.EventHandler(this.About_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button cmdRegister;
        private System.Windows.Forms.Label lCompanyName;
        private System.Windows.Forms.Label lSupportDate;
        private System.Windows.Forms.Label lUsers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}