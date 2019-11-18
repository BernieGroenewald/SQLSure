namespace SentryBackupRestore
{
    partial class AddBackupSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddBackupSet));
            this.cbServerAlias = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tBackupSetDescription = new System.Windows.Forms.TextBox();
            this.tBackupSetName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cbServerAlias
            // 
            this.cbServerAlias.FormattingEnabled = true;
            this.cbServerAlias.Location = new System.Drawing.Point(121, 53);
            this.cbServerAlias.Name = "cbServerAlias";
            this.cbServerAlias.Size = new System.Drawing.Size(239, 21);
            this.cbServerAlias.TabIndex = 64;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(52, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 69;
            this.label5.Text = "Server Alias";
            // 
            // cmdSave
            // 
            this.cmdSave.AutoSize = true;
            this.cmdSave.Location = new System.Drawing.Point(282, 182);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(78, 23);
            this.cmdSave.TabIndex = 66;
            this.cmdSave.Text = "Save Set";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 68;
            this.label2.Text = "Description";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 67;
            this.label1.Text = "Backup Set Name";
            // 
            // tBackupSetDescription
            // 
            this.tBackupSetDescription.Location = new System.Drawing.Point(121, 80);
            this.tBackupSetDescription.Multiline = true;
            this.tBackupSetDescription.Name = "tBackupSetDescription";
            this.tBackupSetDescription.Size = new System.Drawing.Size(239, 96);
            this.tBackupSetDescription.TabIndex = 65;
            // 
            // tBackupSetName
            // 
            this.tBackupSetName.Location = new System.Drawing.Point(121, 27);
            this.tBackupSetName.Name = "tBackupSetName";
            this.tBackupSetName.Size = new System.Drawing.Size(239, 20);
            this.tBackupSetName.TabIndex = 63;
            // 
            // AddBackupSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 221);
            this.Controls.Add(this.cbServerAlias);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tBackupSetDescription);
            this.Controls.Add(this.tBackupSetName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddBackupSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Backup Set";
            this.Load += new System.EventHandler(this.AddBackupSet_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbServerAlias;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tBackupSetDescription;
        private System.Windows.Forms.TextBox tBackupSetName;
    }
}