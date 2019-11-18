namespace SentryGeneral
{
    partial class ViewObjectStatus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewObjectStatus));
            this.gbCheckoutStatus = new System.Windows.Forms.GroupBox();
            this.tCurrentStatus = new System.Windows.Forms.TextBox();
            this.tPartOfProject = new System.Windows.Forms.TextBox();
            this.tResponsiblePerson = new System.Windows.Forms.TextBox();
            this.tComment = new System.Windows.Forms.TextBox();
            this.tLastStatusDate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            this.gbCheckoutStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbCheckoutStatus
            // 
            this.gbCheckoutStatus.Controls.Add(this.tCurrentStatus);
            this.gbCheckoutStatus.Controls.Add(this.tPartOfProject);
            this.gbCheckoutStatus.Controls.Add(this.tResponsiblePerson);
            this.gbCheckoutStatus.Controls.Add(this.tComment);
            this.gbCheckoutStatus.Controls.Add(this.tLastStatusDate);
            this.gbCheckoutStatus.Controls.Add(this.label5);
            this.gbCheckoutStatus.Controls.Add(this.label4);
            this.gbCheckoutStatus.Controls.Add(this.label3);
            this.gbCheckoutStatus.Controls.Add(this.label2);
            this.gbCheckoutStatus.Controls.Add(this.label1);
            this.gbCheckoutStatus.Location = new System.Drawing.Point(12, 12);
            this.gbCheckoutStatus.Name = "gbCheckoutStatus";
            this.gbCheckoutStatus.Size = new System.Drawing.Size(713, 231);
            this.gbCheckoutStatus.TabIndex = 7;
            this.gbCheckoutStatus.TabStop = false;
            this.gbCheckoutStatus.Text = "Status";
            // 
            // tCurrentStatus
            // 
            this.tCurrentStatus.Location = new System.Drawing.Point(129, 42);
            this.tCurrentStatus.Name = "tCurrentStatus";
            this.tCurrentStatus.ReadOnly = true;
            this.tCurrentStatus.Size = new System.Drawing.Size(180, 20);
            this.tCurrentStatus.TabIndex = 1;
            // 
            // tPartOfProject
            // 
            this.tPartOfProject.Location = new System.Drawing.Point(129, 120);
            this.tPartOfProject.Name = "tPartOfProject";
            this.tPartOfProject.ReadOnly = true;
            this.tPartOfProject.Size = new System.Drawing.Size(180, 20);
            this.tPartOfProject.TabIndex = 4;
            // 
            // tResponsiblePerson
            // 
            this.tResponsiblePerson.Location = new System.Drawing.Point(129, 94);
            this.tResponsiblePerson.Name = "tResponsiblePerson";
            this.tResponsiblePerson.ReadOnly = true;
            this.tResponsiblePerson.Size = new System.Drawing.Size(180, 20);
            this.tResponsiblePerson.TabIndex = 3;
            // 
            // tComment
            // 
            this.tComment.Location = new System.Drawing.Point(129, 146);
            this.tComment.Multiline = true;
            this.tComment.Name = "tComment";
            this.tComment.ReadOnly = true;
            this.tComment.Size = new System.Drawing.Size(558, 58);
            this.tComment.TabIndex = 5;
            // 
            // tLastStatusDate
            // 
            this.tLastStatusDate.Location = new System.Drawing.Point(129, 68);
            this.tLastStatusDate.Name = "tLastStatusDate";
            this.tLastStatusDate.ReadOnly = true;
            this.tLastStatusDate.Size = new System.Drawing.Size(180, 20);
            this.tLastStatusDate.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(49, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Part of Project";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(67, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Comments";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Responsible Person";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Last Status Date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Current Status";
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(650, 249);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 8;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // ViewObjectStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 283);
            this.Controls.Add(this.gbCheckoutStatus);
            this.Controls.Add(this.cmdClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ViewObjectStatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Object Status";
            this.Load += new System.EventHandler(this.ViewObjectStatus_Load);
            this.gbCheckoutStatus.ResumeLayout(false);
            this.gbCheckoutStatus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbCheckoutStatus;
        private System.Windows.Forms.TextBox tCurrentStatus;
        private System.Windows.Forms.TextBox tPartOfProject;
        private System.Windows.Forms.TextBox tResponsiblePerson;
        private System.Windows.Forms.TextBox tComment;
        private System.Windows.Forms.TextBox tLastStatusDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdClose;
    }
}