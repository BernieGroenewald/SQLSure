namespace SentryCheckOut
{
    partial class ObjectCheckIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectCheckIn));
            this.tResponsiblePerson = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tComment = new System.Windows.Forms.TextBox();
            this.tLastStatusDate = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbCheckIn = new System.Windows.Forms.ToolStripButton();
            this.tPartOfProject = new System.Windows.Forms.TextBox();
            this.tObjectStatus = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tResponsiblePerson
            // 
            this.tResponsiblePerson.Location = new System.Drawing.Point(134, 122);
            this.tResponsiblePerson.Name = "tResponsiblePerson";
            this.tResponsiblePerson.ReadOnly = true;
            this.tResponsiblePerson.Size = new System.Drawing.Size(180, 20);
            this.tResponsiblePerson.TabIndex = 90;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(54, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 89;
            this.label5.Text = "Part of Project";
            // 
            // tComment
            // 
            this.tComment.Location = new System.Drawing.Point(134, 174);
            this.tComment.Multiline = true;
            this.tComment.Name = "tComment";
            this.tComment.ReadOnly = true;
            this.tComment.Size = new System.Drawing.Size(354, 58);
            this.tComment.TabIndex = 86;
            // 
            // tLastStatusDate
            // 
            this.tLastStatusDate.Location = new System.Drawing.Point(135, 96);
            this.tLastStatusDate.Name = "tLastStatusDate";
            this.tLastStatusDate.ReadOnly = true;
            this.tLastStatusDate.Size = new System.Drawing.Size(180, 20);
            this.tLastStatusDate.TabIndex = 83;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCheckIn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(507, 54);
            this.toolStrip1.TabIndex = 87;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbCheckIn
            // 
            this.tsbCheckIn.Image = ((System.Drawing.Image)(resources.GetObject("tsbCheckIn.Image")));
            this.tsbCheckIn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbCheckIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCheckIn.Name = "tsbCheckIn";
            this.tsbCheckIn.Size = new System.Drawing.Size(57, 51);
            this.tsbCheckIn.Text = "Check In";
            this.tsbCheckIn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbCheckIn.Click += new System.EventHandler(this.tsbCheckIn_Click);
            // 
            // tPartOfProject
            // 
            this.tPartOfProject.Location = new System.Drawing.Point(134, 148);
            this.tPartOfProject.Name = "tPartOfProject";
            this.tPartOfProject.ReadOnly = true;
            this.tPartOfProject.Size = new System.Drawing.Size(179, 20);
            this.tPartOfProject.TabIndex = 92;
            // 
            // tObjectStatus
            // 
            this.tObjectStatus.Location = new System.Drawing.Point(135, 70);
            this.tObjectStatus.Name = "tObjectStatus";
            this.tObjectStatus.ReadOnly = true;
            this.tObjectStatus.Size = new System.Drawing.Size(179, 20);
            this.tObjectStatus.TabIndex = 91;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(72, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 85;
            this.label4.Text = "Comments";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 84;
            this.label3.Text = "Responsible Person";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 88;
            this.label2.Text = "Last Status Date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 82;
            this.label1.Text = "Current Status";
            // 
            // ObjectCheckIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 254);
            this.Controls.Add(this.tResponsiblePerson);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tComment);
            this.Controls.Add(this.tLastStatusDate);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tPartOfProject);
            this.Controls.Add(this.tObjectStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ObjectCheckIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Check In";
            this.Load += new System.EventHandler(this.ObjectCheckIn_Load);
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
        private System.Windows.Forms.ToolStripButton tsbCheckIn;
        private System.Windows.Forms.TextBox tPartOfProject;
        private System.Windows.Forms.TextBox tObjectStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}