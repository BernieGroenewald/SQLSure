namespace SentryCheckOut
{
    partial class ObjectUndoCheckOut
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectUndoCheckOut));
            this.tResponsiblePerson = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tComment = new System.Windows.Forms.TextBox();
            this.tLastStatusDate = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbUndoCheckOut = new System.Windows.Forms.ToolStripButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tObjectStatus = new System.Windows.Forms.TextBox();
            this.tPartOfProject = new System.Windows.Forms.TextBox();
            this.cRestorePrevious = new System.Windows.Forms.CheckBox();
            this.tPreviousVersion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmdViewPrevious = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tResponsiblePerson
            // 
            this.tResponsiblePerson.Location = new System.Drawing.Point(122, 119);
            this.tResponsiblePerson.Name = "tResponsiblePerson";
            this.tResponsiblePerson.ReadOnly = true;
            this.tResponsiblePerson.Size = new System.Drawing.Size(180, 20);
            this.tResponsiblePerson.TabIndex = 75;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(42, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 74;
            this.label5.Text = "Part of Project";
            // 
            // tComment
            // 
            this.tComment.Location = new System.Drawing.Point(122, 171);
            this.tComment.Multiline = true;
            this.tComment.Name = "tComment";
            this.tComment.ReadOnly = true;
            this.tComment.Size = new System.Drawing.Size(354, 58);
            this.tComment.TabIndex = 71;
            // 
            // tLastStatusDate
            // 
            this.tLastStatusDate.Location = new System.Drawing.Point(123, 93);
            this.tLastStatusDate.Name = "tLastStatusDate";
            this.tLastStatusDate.ReadOnly = true;
            this.tLastStatusDate.Size = new System.Drawing.Size(180, 20);
            this.tLastStatusDate.TabIndex = 67;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbUndoCheckOut});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(489, 54);
            this.toolStrip1.TabIndex = 72;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbUndoCheckOut
            // 
            this.tsbUndoCheckOut.Image = ((System.Drawing.Image)(resources.GetObject("tsbUndoCheckOut.Image")));
            this.tsbUndoCheckOut.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbUndoCheckOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUndoCheckOut.Name = "tsbUndoCheckOut";
            this.tsbUndoCheckOut.Size = new System.Drawing.Size(101, 51);
            this.tsbUndoCheckOut.Text = "Undo Check-Out";
            this.tsbUndoCheckOut.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbUndoCheckOut.Click += new System.EventHandler(this.tsbUndoCheckOut_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 70;
            this.label4.Text = "Comments";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 68;
            this.label3.Text = "Responsible Person";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 73;
            this.label2.Text = "Last Status Date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 66;
            this.label1.Text = "Current Status";
            // 
            // tObjectStatus
            // 
            this.tObjectStatus.Location = new System.Drawing.Point(123, 67);
            this.tObjectStatus.Name = "tObjectStatus";
            this.tObjectStatus.ReadOnly = true;
            this.tObjectStatus.Size = new System.Drawing.Size(179, 20);
            this.tObjectStatus.TabIndex = 76;
            // 
            // tPartOfProject
            // 
            this.tPartOfProject.Location = new System.Drawing.Point(122, 145);
            this.tPartOfProject.Name = "tPartOfProject";
            this.tPartOfProject.ReadOnly = true;
            this.tPartOfProject.Size = new System.Drawing.Size(179, 20);
            this.tPartOfProject.TabIndex = 77;
            // 
            // cRestorePrevious
            // 
            this.cRestorePrevious.AutoSize = true;
            this.cRestorePrevious.Checked = true;
            this.cRestorePrevious.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cRestorePrevious.Location = new System.Drawing.Point(122, 235);
            this.cRestorePrevious.Name = "cRestorePrevious";
            this.cRestorePrevious.Size = new System.Drawing.Size(145, 17);
            this.cRestorePrevious.TabIndex = 78;
            this.cRestorePrevious.Text = "Restore Previous Version";
            this.cRestorePrevious.UseVisualStyleBackColor = true;
            // 
            // tPreviousVersion
            // 
            this.tPreviousVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tPreviousVersion.Location = new System.Drawing.Point(12, 302);
            this.tPreviousVersion.Multiline = true;
            this.tPreviousVersion.Name = "tPreviousVersion";
            this.tPreviousVersion.ReadOnly = true;
            this.tPreviousVersion.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tPreviousVersion.Size = new System.Drawing.Size(461, 364);
            this.tPreviousVersion.TabIndex = 79;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 277);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 15);
            this.label6.TabIndex = 80;
            this.label6.Text = "Previous Version";
            // 
            // cmdViewPrevious
            // 
            this.cmdViewPrevious.Image = ((System.Drawing.Image)(resources.GetObject("cmdViewPrevious.Image")));
            this.cmdViewPrevious.Location = new System.Drawing.Point(371, 117);
            this.cmdViewPrevious.Name = "cmdViewPrevious";
            this.cmdViewPrevious.Size = new System.Drawing.Size(105, 48);
            this.cmdViewPrevious.TabIndex = 81;
            this.cmdViewPrevious.Text = "View Previous Version";
            this.cmdViewPrevious.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdViewPrevious.UseVisualStyleBackColor = true;
            this.cmdViewPrevious.Click += new System.EventHandler(this.cmdViewPrevious_Click);
            // 
            // ObjectUndoCheckOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 263);
            this.Controls.Add(this.cmdViewPrevious);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tPreviousVersion);
            this.Controls.Add(this.cRestorePrevious);
            this.Controls.Add(this.tPartOfProject);
            this.Controls.Add(this.tObjectStatus);
            this.Controls.Add(this.tResponsiblePerson);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tComment);
            this.Controls.Add(this.tLastStatusDate);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ObjectUndoCheckOut";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Undo Check Out";
            this.Load += new System.EventHandler(this.ObjectUndoCheckOut_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tResponsiblePerson;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tComment;
        private System.Windows.Forms.TextBox tLastStatusDate;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbUndoCheckOut;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tObjectStatus;
        private System.Windows.Forms.TextBox tPartOfProject;
        private System.Windows.Forms.CheckBox cRestorePrevious;
        private System.Windows.Forms.TextBox tPreviousVersion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button cmdViewPrevious;
    }
}