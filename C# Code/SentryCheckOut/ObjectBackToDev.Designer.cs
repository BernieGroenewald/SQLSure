namespace SentryCheckOut
{
    partial class ObjectBackToDev
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectBackToDev));
            this.tLastStatusDate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tPreviousResponsiblePerson = new System.Windows.Forms.TextBox();
            this.tPreviousComment = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tCurrentStatus = new System.Windows.Forms.TextBox();
            this.tsbObjectHistory = new System.Windows.Forms.ToolStripButton();
            this.tResponsiblePerson = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tComment = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbCheckOut = new System.Windows.Forms.ToolStripButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tPartOfProject = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
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
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 13);
            this.label8.TabIndex = 70;
            this.label8.Text = "Responsible Person";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tLastStatusDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tPreviousResponsiblePerson);
            this.groupBox1.Controls.Add(this.tPreviousComment);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(24, 240);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(513, 168);
            this.groupBox1.TabIndex = 83;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Previous Status";
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
            // tCurrentStatus
            // 
            this.tCurrentStatus.Location = new System.Drawing.Point(135, 74);
            this.tCurrentStatus.Name = "tCurrentStatus";
            this.tCurrentStatus.ReadOnly = true;
            this.tCurrentStatus.Size = new System.Drawing.Size(180, 20);
            this.tCurrentStatus.TabIndex = 82;
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
            // tResponsiblePerson
            // 
            this.tResponsiblePerson.Location = new System.Drawing.Point(135, 100);
            this.tResponsiblePerson.Name = "tResponsiblePerson";
            this.tResponsiblePerson.ReadOnly = true;
            this.tResponsiblePerson.Size = new System.Drawing.Size(180, 20);
            this.tResponsiblePerson.TabIndex = 80;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(55, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 79;
            this.label5.Text = "Part of Project";
            // 
            // tComment
            // 
            this.tComment.Location = new System.Drawing.Point(135, 153);
            this.tComment.Multiline = true;
            this.tComment.Name = "tComment";
            this.tComment.Size = new System.Drawing.Size(355, 58);
            this.tComment.TabIndex = 73;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCheckOut,
            this.tsbObjectHistory});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(559, 54);
            this.toolStrip1.TabIndex = 78;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbCheckOut
            // 
            this.tsbCheckOut.Image = ((System.Drawing.Image)(resources.GetObject("tsbCheckOut.Image")));
            this.tsbCheckOut.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbCheckOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCheckOut.Name = "tsbCheckOut";
            this.tsbCheckOut.Size = new System.Drawing.Size(64, 51);
            this.tsbCheckOut.Text = "Take Back";
            this.tsbCheckOut.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbCheckOut.Click += new System.EventHandler(this.tsbCheckOut_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 76;
            this.label3.Text = "Responsible Person";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 75;
            this.label1.Text = "Current Status";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(73, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 77;
            this.label4.Text = "Comments";
            // 
            // tPartOfProject
            // 
            this.tPartOfProject.Location = new System.Drawing.Point(135, 126);
            this.tPartOfProject.Name = "tPartOfProject";
            this.tPartOfProject.ReadOnly = true;
            this.tPartOfProject.Size = new System.Drawing.Size(180, 20);
            this.tPartOfProject.TabIndex = 84;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(73, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 77;
            this.label6.Text = "Comments";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(28, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 13);
            this.label9.TabIndex = 76;
            this.label9.Text = "Responsible Person";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(55, 129);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 13);
            this.label10.TabIndex = 79;
            this.label10.Text = "Part of Project";
            // 
            // ObjectBackToDev
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 426);
            this.Controls.Add(this.tPartOfProject);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tCurrentStatus);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tResponsiblePerson);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tComment);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ObjectBackToDev";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Object Back To Development";
            this.Load += new System.EventHandler(this.ObjectBackToDev_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tLastStatusDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tPreviousResponsiblePerson;
        private System.Windows.Forms.TextBox tPreviousComment;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tCurrentStatus;
        private System.Windows.Forms.ToolStripButton tsbObjectHistory;
        private System.Windows.Forms.TextBox tResponsiblePerson;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tComment;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbCheckOut;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tPartOfProject;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}