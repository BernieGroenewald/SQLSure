namespace SentryProject
{
    partial class ProjectApproval
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectApproval));
            this.project2 = new SentryProject.Project();
            this.gbApproval = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdFinalApprove = new System.Windows.Forms.Button();
            this.cmdTestingApprove = new System.Windows.Forms.Button();
            this.tFinalApproveDate = new System.Windows.Forms.TextBox();
            this.tFinalApprovedBy = new System.Windows.Forms.TextBox();
            this.tTestingApproveDate = new System.Windows.Forms.TextBox();
            this.tTestingApprovedBy = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tRequestDate = new System.Windows.Forms.TextBox();
            this.cmdRequestApproval = new System.Windows.Forms.Button();
            this.tRequestBy = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbApproval.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // project2
            // 
            this.project2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.project2.IsApproveOnly = true;
            this.project2.IsProjectOnly = true;
            this.project2.Location = new System.Drawing.Point(12, 22);
            this.project2.Name = "project2";
            this.project2.Size = new System.Drawing.Size(1176, 621);
            this.project2.TabIndex = 0;
            // 
            // gbApproval
            // 
            this.gbApproval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbApproval.Controls.Add(this.groupBox2);
            this.gbApproval.Controls.Add(this.groupBox1);
            this.gbApproval.Location = new System.Drawing.Point(12, 649);
            this.gbApproval.Name = "gbApproval";
            this.gbApproval.Size = new System.Drawing.Size(1175, 153);
            this.gbApproval.TabIndex = 1;
            this.gbApproval.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmdFinalApprove);
            this.groupBox2.Controls.Add(this.cmdTestingApprove);
            this.groupBox2.Controls.Add(this.tFinalApproveDate);
            this.groupBox2.Controls.Add(this.tFinalApprovedBy);
            this.groupBox2.Controls.Add(this.tTestingApproveDate);
            this.groupBox2.Controls.Add(this.tTestingApprovedBy);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(346, 20);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(823, 124);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Approval";
            // 
            // cmdFinalApprove
            // 
            this.cmdFinalApprove.Image = ((System.Drawing.Image)(resources.GetObject("cmdFinalApprove.Image")));
            this.cmdFinalApprove.Location = new System.Drawing.Point(724, 75);
            this.cmdFinalApprove.Name = "cmdFinalApprove";
            this.cmdFinalApprove.Size = new System.Drawing.Size(94, 40);
            this.cmdFinalApprove.TabIndex = 11;
            this.cmdFinalApprove.Text = "Approve";
            this.cmdFinalApprove.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.cmdFinalApprove.UseVisualStyleBackColor = true;
            this.cmdFinalApprove.Click += new System.EventHandler(this.cmdFinalApprove_Click);
            // 
            // cmdTestingApprove
            // 
            this.cmdTestingApprove.Image = ((System.Drawing.Image)(resources.GetObject("cmdTestingApprove.Image")));
            this.cmdTestingApprove.Location = new System.Drawing.Point(255, 77);
            this.cmdTestingApprove.Name = "cmdTestingApprove";
            this.cmdTestingApprove.Size = new System.Drawing.Size(94, 40);
            this.cmdTestingApprove.TabIndex = 10;
            this.cmdTestingApprove.Text = "Approve";
            this.cmdTestingApprove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdTestingApprove.UseVisualStyleBackColor = true;
            this.cmdTestingApprove.Click += new System.EventHandler(this.cmdTestingApprove_Click);
            // 
            // tFinalApproveDate
            // 
            this.tFinalApproveDate.Location = new System.Drawing.Point(639, 43);
            this.tFinalApproveDate.Name = "tFinalApproveDate";
            this.tFinalApproveDate.ReadOnly = true;
            this.tFinalApproveDate.Size = new System.Drawing.Size(139, 20);
            this.tFinalApproveDate.TabIndex = 9;
            // 
            // tFinalApprovedBy
            // 
            this.tFinalApprovedBy.Location = new System.Drawing.Point(639, 17);
            this.tFinalApprovedBy.Name = "tFinalApprovedBy";
            this.tFinalApprovedBy.ReadOnly = true;
            this.tFinalApprovedBy.Size = new System.Drawing.Size(179, 20);
            this.tFinalApprovedBy.TabIndex = 8;
            this.tFinalApprovedBy.Text = "Not Approved";
            // 
            // tTestingApproveDate
            // 
            this.tTestingApproveDate.Location = new System.Drawing.Point(170, 45);
            this.tTestingApproveDate.Name = "tTestingApproveDate";
            this.tTestingApproveDate.ReadOnly = true;
            this.tTestingApproveDate.Size = new System.Drawing.Size(139, 20);
            this.tTestingApproveDate.TabIndex = 7;
            // 
            // tTestingApprovedBy
            // 
            this.tTestingApprovedBy.Location = new System.Drawing.Point(170, 19);
            this.tTestingApprovedBy.Name = "tTestingApprovedBy";
            this.tTestingApprovedBy.ReadOnly = true;
            this.tTestingApprovedBy.Size = new System.Drawing.Size(179, 20);
            this.tTestingApprovedBy.TabIndex = 6;
            this.tTestingApprovedBy.Text = "Not Approved";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(603, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Date";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(494, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Final Approver Approved By";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(134, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(58, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Testing Approved By";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tRequestDate);
            this.groupBox1.Controls.Add(this.cmdRequestApproval);
            this.groupBox1.Controls.Add(this.tRequestBy);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(7, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(332, 134);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Approval Request";
            // 
            // tRequestDate
            // 
            this.tRequestDate.Location = new System.Drawing.Point(141, 45);
            this.tRequestDate.Name = "tRequestDate";
            this.tRequestDate.ReadOnly = true;
            this.tRequestDate.Size = new System.Drawing.Size(139, 20);
            this.tRequestDate.TabIndex = 3;
            // 
            // cmdRequestApproval
            // 
            this.cmdRequestApproval.Image = ((System.Drawing.Image)(resources.GetObject("cmdRequestApproval.Image")));
            this.cmdRequestApproval.Location = new System.Drawing.Point(178, 87);
            this.cmdRequestApproval.Name = "cmdRequestApproval";
            this.cmdRequestApproval.Size = new System.Drawing.Size(148, 41);
            this.cmdRequestApproval.TabIndex = 0;
            this.cmdRequestApproval.Text = "Request Approval";
            this.cmdRequestApproval.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdRequestApproval.UseVisualStyleBackColor = true;
            this.cmdRequestApproval.Click += new System.EventHandler(this.cmdRequestApproval_Click);
            // 
            // tRequestBy
            // 
            this.tRequestBy.Location = new System.Drawing.Point(141, 19);
            this.tRequestBy.Name = "tRequestBy";
            this.tRequestBy.ReadOnly = true;
            this.tRequestBy.Size = new System.Drawing.Size(179, 20);
            this.tRequestBy.TabIndex = 2;
            this.tRequestBy.Text = "Not Requested";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Request Date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Requested By";
            // 
            // ProjectApproval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 813);
            this.Controls.Add(this.gbApproval);
            this.Controls.Add(this.project2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProjectApproval";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Approval";
            this.Load += new System.EventHandler(this.ProjectApproval_Load);
            this.gbApproval.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

      
        private Project project2;
        private System.Windows.Forms.GroupBox gbApproval;
        private System.Windows.Forms.Button cmdRequestApproval;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmdFinalApprove;
        private System.Windows.Forms.Button cmdTestingApprove;
        private System.Windows.Forms.TextBox tFinalApproveDate;
        private System.Windows.Forms.TextBox tFinalApprovedBy;
        private System.Windows.Forms.TextBox tTestingApproveDate;
        private System.Windows.Forms.TextBox tTestingApprovedBy;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tRequestDate;
        private System.Windows.Forms.TextBox tRequestBy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}