namespace SentryCheckOut
{
    partial class ObjectCheckOut
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectCheckOut));
            this.label6 = new System.Windows.Forms.Label();
            this.cbCheckOutFrom = new System.Windows.Forms.ComboBox();
            this.cmdNewProject = new System.Windows.Forms.Button();
            this.tResponsiblePerson = new System.Windows.Forms.TextBox();
            this.cbPartOfProject = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tComment = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbCheckOut = new System.Windows.Forms.ToolStripButton();
            this.tsbObjectHistory = new System.Windows.Forms.ToolStripButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tCurrentStatus = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tLastStatusDate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tPreviousResponsiblePerson = new System.Windows.Forms.TextBox();
            this.tPreviousComment = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 64;
            this.label6.Text = "Check Out From";
            // 
            // cbCheckOutFrom
            // 
            this.cbCheckOutFrom.FormattingEnabled = true;
            this.cbCheckOutFrom.Location = new System.Drawing.Point(123, 102);
            this.cbCheckOutFrom.Name = "cbCheckOutFrom";
            this.cbCheckOutFrom.Size = new System.Drawing.Size(180, 21);
            this.cbCheckOutFrom.TabIndex = 0;
            this.cbCheckOutFrom.SelectedIndexChanged += new System.EventHandler(this.cbCheckOutFrom_SelectedIndexChanged);
            // 
            // cmdNewProject
            // 
            this.cmdNewProject.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cmdNewProject.Image = ((System.Drawing.Image)(resources.GetObject("cmdNewProject.Image")));
            this.cmdNewProject.Location = new System.Drawing.Point(403, 129);
            this.cmdNewProject.Name = "cmdNewProject";
            this.cmdNewProject.Size = new System.Drawing.Size(75, 47);
            this.cmdNewProject.TabIndex = 3;
            this.cmdNewProject.Text = "New Project";
            this.cmdNewProject.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdNewProject.UseVisualStyleBackColor = true;
            this.cmdNewProject.Click += new System.EventHandler(this.cmdNewProject_Click);
            // 
            // tResponsiblePerson
            // 
            this.tResponsiblePerson.Location = new System.Drawing.Point(123, 129);
            this.tResponsiblePerson.Name = "tResponsiblePerson";
            this.tResponsiblePerson.ReadOnly = true;
            this.tResponsiblePerson.Size = new System.Drawing.Size(180, 20);
            this.tResponsiblePerson.TabIndex = 61;
            // 
            // cbPartOfProject
            // 
            this.cbPartOfProject.FormattingEnabled = true;
            this.cbPartOfProject.Location = new System.Drawing.Point(124, 155);
            this.cbPartOfProject.Name = "cbPartOfProject";
            this.cbPartOfProject.Size = new System.Drawing.Size(273, 21);
            this.cbPartOfProject.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 60;
            this.label5.Text = "Part of Project";
            // 
            // tComment
            // 
            this.tComment.Location = new System.Drawing.Point(123, 182);
            this.tComment.Multiline = true;
            this.tComment.Name = "tComment";
            this.tComment.Size = new System.Drawing.Size(355, 58);
            this.tComment.TabIndex = 2;
            this.tComment.TextChanged += new System.EventHandler(this.tComment_TextChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCheckOut,
            this.tsbObjectHistory});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(540, 54);
            this.toolStrip1.TabIndex = 58;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbCheckOut
            // 
            this.tsbCheckOut.Image = ((System.Drawing.Image)(resources.GetObject("tsbCheckOut.Image")));
            this.tsbCheckOut.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbCheckOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCheckOut.Name = "tsbCheckOut";
            this.tsbCheckOut.Size = new System.Drawing.Size(67, 51);
            this.tsbCheckOut.Text = "Check Out";
            this.tsbCheckOut.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbCheckOut.Click += new System.EventHandler(this.tsbCheckOut_Click);
            // 
            // tsbObjectHistory
            // 
            this.tsbObjectHistory.Image = ((System.Drawing.Image)(resources.GetObject("tsbObjectHistory.Image")));
            this.tsbObjectHistory.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbObjectHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbObjectHistory.Name = "tsbObjectHistory";
            this.tsbObjectHistory.Size = new System.Drawing.Size(49, 51);
            this.tsbObjectHistory.Text = "History";
            this.tsbObjectHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbObjectHistory.Click += new System.EventHandler(this.tsbObjectHistory_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(61, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 56;
            this.label4.Text = "Comments";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 54;
            this.label3.Text = "Responsible Person";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "Current Status";
            // 
            // tCurrentStatus
            // 
            this.tCurrentStatus.Location = new System.Drawing.Point(123, 78);
            this.tCurrentStatus.Name = "tCurrentStatus";
            this.tCurrentStatus.ReadOnly = true;
            this.tCurrentStatus.Size = new System.Drawing.Size(180, 20);
            this.tCurrentStatus.TabIndex = 65;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tLastStatusDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tPreviousResponsiblePerson);
            this.groupBox1.Controls.Add(this.tPreviousComment);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(12, 267);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(513, 168);
            this.groupBox1.TabIndex = 70;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Previous Status";
            // 
            // tLastStatusDate
            // 
            this.tLastStatusDate.Location = new System.Drawing.Point(137, 58);
            this.tLastStatusDate.Name = "tLastStatusDate";
            this.tLastStatusDate.ReadOnly = true;
            this.tLastStatusDate.Size = new System.Drawing.Size(180, 20);
            this.tLastStatusDate.TabIndex = 74;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 75;
            this.label2.Text = "Last Status Date";
            // 
            // tPreviousResponsiblePerson
            // 
            this.tPreviousResponsiblePerson.Location = new System.Drawing.Point(137, 32);
            this.tPreviousResponsiblePerson.Name = "tPreviousResponsiblePerson";
            this.tPreviousResponsiblePerson.ReadOnly = true;
            this.tPreviousResponsiblePerson.Size = new System.Drawing.Size(180, 20);
            this.tPreviousResponsiblePerson.TabIndex = 73;
            // 
            // tPreviousComment
            // 
            this.tPreviousComment.Location = new System.Drawing.Point(137, 84);
            this.tPreviousComment.Multiline = true;
            this.tPreviousComment.Name = "tPreviousComment";
            this.tPreviousComment.ReadOnly = true;
            this.tPreviousComment.Size = new System.Drawing.Size(354, 58);
            this.tPreviousComment.TabIndex = 72;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(75, 87);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 71;
            this.label7.Text = "Comments";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 13);
            this.label8.TabIndex = 70;
            this.label8.Text = "Responsible Person";
            // 
            // ObjectCheckOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 457);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tCurrentStatus);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbCheckOutFrom);
            this.Controls.Add(this.cmdNewProject);
            this.Controls.Add(this.tResponsiblePerson);
            this.Controls.Add(this.cbPartOfProject);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tComment);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ObjectCheckOut";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Check Out";
            this.Load += new System.EventHandler(this.ObjectCheckOut_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbCheckOutFrom;
        private System.Windows.Forms.Button cmdNewProject;
        private System.Windows.Forms.TextBox tResponsiblePerson;
        private System.Windows.Forms.ComboBox cbPartOfProject;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tComment;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbCheckOut;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tCurrentStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tLastStatusDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tPreviousResponsiblePerson;
        private System.Windows.Forms.TextBox tPreviousComment;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStripButton tsbObjectHistory;
    }
}