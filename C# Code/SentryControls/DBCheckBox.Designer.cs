namespace SentryControls
{
    partial class DBCheckBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBCheckBox));
            this.lTemplate = new System.Windows.Forms.Label();
            this.cmdOkDatabases = new System.Windows.Forms.Button();
            this.pMain = new System.Windows.Forms.Panel();
            this.cmdDropDownDatabases = new System.Windows.Forms.Button();
            this.tSelected = new System.Windows.Forms.TextBox();
            this.pLine = new System.Windows.Forms.PictureBox();
            this.pMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pLine)).BeginInit();
            this.SuspendLayout();
            // 
            // lTemplate
            // 
            this.lTemplate.AutoSize = true;
            this.lTemplate.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lTemplate.Location = new System.Drawing.Point(15, 21);
            this.lTemplate.Name = "lTemplate";
            this.lTemplate.Size = new System.Drawing.Size(0, 13);
            this.lTemplate.TabIndex = 0;
            this.lTemplate.Visible = false;
            // 
            // cmdOkDatabases
            // 
            this.cmdOkDatabases.Location = new System.Drawing.Point(180, 31);
            this.cmdOkDatabases.Name = "cmdOkDatabases";
            this.cmdOkDatabases.Size = new System.Drawing.Size(75, 23);
            this.cmdOkDatabases.TabIndex = 16;
            this.cmdOkDatabases.Text = "Done";
            this.cmdOkDatabases.UseVisualStyleBackColor = true;
            this.cmdOkDatabases.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // pMain
            // 
            this.pMain.AutoScroll = true;
            this.pMain.Controls.Add(this.lTemplate);
            this.pMain.Location = new System.Drawing.Point(3, 70);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(252, 281);
            this.pMain.TabIndex = 15;
            // 
            // cmdDropDownDatabases
            // 
            this.cmdDropDownDatabases.Image = ((System.Drawing.Image)(resources.GetObject("cmdDropDownDatabases.Image")));
            this.cmdDropDownDatabases.Location = new System.Drawing.Point(225, 2);
            this.cmdDropDownDatabases.Name = "cmdDropDownDatabases";
            this.cmdDropDownDatabases.Size = new System.Drawing.Size(30, 23);
            this.cmdDropDownDatabases.TabIndex = 14;
            this.cmdDropDownDatabases.UseVisualStyleBackColor = true;
            this.cmdDropDownDatabases.Click += new System.EventHandler(this.cmdDropDown_Click);
            // 
            // tSelected
            // 
            this.tSelected.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tSelected.Location = new System.Drawing.Point(3, 3);
            this.tSelected.Name = "tSelected";
            this.tSelected.ReadOnly = true;
            this.tSelected.Size = new System.Drawing.Size(225, 20);
            this.tSelected.TabIndex = 13;
            this.tSelected.TextChanged += new System.EventHandler(this.tSelected_TextChanged);
            // 
            // pLine
            // 
            this.pLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pLine.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pLine.ErrorImage")));
            this.pLine.Image = ((System.Drawing.Image)(resources.GetObject("pLine.Image")));
            this.pLine.Location = new System.Drawing.Point(4, 57);
            this.pLine.Margin = new System.Windows.Forms.Padding(0);
            this.pLine.Name = "pLine";
            this.pLine.Size = new System.Drawing.Size(251, 14);
            this.pLine.TabIndex = 17;
            this.pLine.TabStop = false;
            // 
            // DBCheckBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pLine);
            this.Controls.Add(this.cmdOkDatabases);
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.cmdDropDownDatabases);
            this.Controls.Add(this.tSelected);
            this.Name = "DBCheckBox";
            this.Size = new System.Drawing.Size(260, 351);
            this.Load += new System.EventHandler(this.CheckCombo_Load);
            this.Leave += new System.EventHandler(this.CheckCombo_Leave);
            this.pMain.ResumeLayout(false);
            this.pMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pLine)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lTemplate;
        private System.Windows.Forms.Button cmdOkDatabases;
        private System.Windows.Forms.Panel pMain;
        private System.Windows.Forms.Button cmdDropDownDatabases;
        private System.Windows.Forms.TextBox tSelected;
        private System.Windows.Forms.PictureBox pLine;
    }
}
