namespace SentryControls
{
    partial class DBObjectMulti
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBObjectMulti));
            this.tComment = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tObjectType = new System.Windows.Forms.TextBox();
            this.cObjTriggers = new System.Windows.Forms.CheckBox();
            this.cObjViews = new System.Windows.Forms.CheckBox();
            this.cObjTables = new System.Windows.Forms.CheckBox();
            this.cObjFunctions = new System.Windows.Forms.CheckBox();
            this.cObjStoredProcedures = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tStatusDate = new System.Windows.Forms.TextBox();
            this.tUser = new System.Windows.Forms.TextBox();
            this.tStatus = new System.Windows.Forms.TextBox();
            this.tError = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbFilter = new System.Windows.Forms.ToolStripButton();
            this.tsbRemoveFilter = new System.Windows.Forms.ToolStripButton();
            this.tsbViewObject = new System.Windows.Forms.ToolStripButton();
            this.tsbHistory = new System.Windows.Forms.ToolStripButton();
            this.tsbCompare = new System.Windows.Forms.ToolStripButton();
            this.label3 = new System.Windows.Forms.Label();
            this.cbObjects = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbDatabaseName = new System.Windows.Forms.ComboBox();
            this.lServer = new System.Windows.Forms.Label();
            this.tDateModified = new System.Windows.Forms.TextBox();
            this.tDateCreated = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tComment
            // 
            this.tComment.Location = new System.Drawing.Point(80, 388);
            this.tComment.Multiline = true;
            this.tComment.Name = "tComment";
            this.tComment.ReadOnly = true;
            this.tComment.Size = new System.Drawing.Size(263, 75);
            this.tComment.TabIndex = 66;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(43, 221);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 65;
            this.label8.Text = "Type";
            // 
            // tObjectType
            // 
            this.tObjectType.Enabled = false;
            this.tObjectType.Location = new System.Drawing.Point(80, 218);
            this.tObjectType.Name = "tObjectType";
            this.tObjectType.ReadOnly = true;
            this.tObjectType.Size = new System.Drawing.Size(180, 20);
            this.tObjectType.TabIndex = 64;
            // 
            // cObjTriggers
            // 
            this.cObjTriggers.AutoSize = true;
            this.cObjTriggers.Location = new System.Drawing.Point(196, 144);
            this.cObjTriggers.Name = "cObjTriggers";
            this.cObjTriggers.Size = new System.Drawing.Size(64, 17);
            this.cObjTriggers.TabIndex = 51;
            this.cObjTriggers.Text = "Triggers";
            this.cObjTriggers.UseVisualStyleBackColor = true;
            this.cObjTriggers.CheckedChanged += new System.EventHandler(this.cObjTriggers_CheckedChanged);
            // 
            // cObjViews
            // 
            this.cObjViews.AutoSize = true;
            this.cObjViews.Location = new System.Drawing.Point(196, 167);
            this.cObjViews.Name = "cObjViews";
            this.cObjViews.Size = new System.Drawing.Size(54, 17);
            this.cObjViews.TabIndex = 52;
            this.cObjViews.Text = "Views";
            this.cObjViews.UseVisualStyleBackColor = true;
            this.cObjViews.CheckedChanged += new System.EventHandler(this.cObjViews_CheckedChanged);
            // 
            // cObjTables
            // 
            this.cObjTables.AutoSize = true;
            this.cObjTables.Location = new System.Drawing.Point(80, 167);
            this.cObjTables.Name = "cObjTables";
            this.cObjTables.Size = new System.Drawing.Size(58, 17);
            this.cObjTables.TabIndex = 48;
            this.cObjTables.Text = "Tables";
            this.cObjTables.UseVisualStyleBackColor = true;
            this.cObjTables.CheckedChanged += new System.EventHandler(this.cObjTables_CheckedChanged);
            // 
            // cObjFunctions
            // 
            this.cObjFunctions.AutoSize = true;
            this.cObjFunctions.Location = new System.Drawing.Point(80, 144);
            this.cObjFunctions.Name = "cObjFunctions";
            this.cObjFunctions.Size = new System.Drawing.Size(72, 17);
            this.cObjFunctions.TabIndex = 46;
            this.cObjFunctions.Text = "Functions";
            this.cObjFunctions.UseVisualStyleBackColor = true;
            this.cObjFunctions.CheckedChanged += new System.EventHandler(this.cObjFunctions_CheckedChanged);
            // 
            // cObjStoredProcedures
            // 
            this.cObjStoredProcedures.AutoSize = true;
            this.cObjStoredProcedures.Checked = true;
            this.cObjStoredProcedures.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cObjStoredProcedures.Location = new System.Drawing.Point(80, 120);
            this.cObjStoredProcedures.Name = "cObjStoredProcedures";
            this.cObjStoredProcedures.Size = new System.Drawing.Size(114, 17);
            this.cObjStoredProcedures.TabIndex = 44;
            this.cObjStoredProcedures.Text = "Stored Procedures";
            this.cObjStoredProcedures.UseVisualStyleBackColor = true;
            this.cObjStoredProcedures.CheckedChanged += new System.EventHandler(this.cObjStoredProcedures_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 365);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 63;
            this.label7.Text = "Status Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(45, 338);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 62;
            this.label5.Text = "User";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 311);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 61;
            this.label4.Text = "Status";
            // 
            // tStatusDate
            // 
            this.tStatusDate.Location = new System.Drawing.Point(80, 362);
            this.tStatusDate.Name = "tStatusDate";
            this.tStatusDate.ReadOnly = true;
            this.tStatusDate.Size = new System.Drawing.Size(147, 20);
            this.tStatusDate.TabIndex = 60;
            // 
            // tUser
            // 
            this.tUser.Location = new System.Drawing.Point(80, 335);
            this.tUser.Name = "tUser";
            this.tUser.ReadOnly = true;
            this.tUser.Size = new System.Drawing.Size(180, 20);
            this.tUser.TabIndex = 59;
            // 
            // tStatus
            // 
            this.tStatus.Location = new System.Drawing.Point(80, 308);
            this.tStatus.Name = "tStatus";
            this.tStatus.ReadOnly = true;
            this.tStatus.Size = new System.Drawing.Size(180, 20);
            this.tStatus.TabIndex = 58;
            // 
            // tError
            // 
            this.tError.Enabled = false;
            this.tError.Location = new System.Drawing.Point(7, 478);
            this.tError.Multiline = true;
            this.tError.Name = "tError";
            this.tError.ReadOnly = true;
            this.tError.Size = new System.Drawing.Size(336, 61);
            this.tError.TabIndex = 57;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 247);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "Date Created";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 391);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 67;
            this.label9.Text = "Comment";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbFilter,
            this.tsbRemoveFilter,
            this.tsbViewObject,
            this.tsbHistory,
            this.tsbCompare});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(350, 54);
            this.toolStrip1.TabIndex = 56;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbFilter
            // 
            this.tsbFilter.Image = ((System.Drawing.Image)(resources.GetObject("tsbFilter.Image")));
            this.tsbFilter.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFilter.Name = "tsbFilter";
            this.tsbFilter.Size = new System.Drawing.Size(37, 51);
            this.tsbFilter.Text = "Filter";
            this.tsbFilter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbFilter.Click += new System.EventHandler(this.tsbFilter_Click);
            // 
            // tsbRemoveFilter
            // 
            this.tsbRemoveFilter.Image = ((System.Drawing.Image)(resources.GetObject("tsbRemoveFilter.Image")));
            this.tsbRemoveFilter.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbRemoveFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRemoveFilter.Name = "tsbRemoveFilter";
            this.tsbRemoveFilter.Size = new System.Drawing.Size(54, 51);
            this.tsbRemoveFilter.Text = "Remove";
            this.tsbRemoveFilter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbRemoveFilter.ToolTipText = "Remove Filter";
            this.tsbRemoveFilter.Click += new System.EventHandler(this.tsbRemoveFilter_Click);
            // 
            // tsbViewObject
            // 
            this.tsbViewObject.Image = ((System.Drawing.Image)(resources.GetObject("tsbViewObject.Image")));
            this.tsbViewObject.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbViewObject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbViewObject.Name = "tsbViewObject";
            this.tsbViewObject.Size = new System.Drawing.Size(39, 51);
            this.tsbViewObject.Text = "View ";
            this.tsbViewObject.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbViewObject.ToolTipText = "View Object";
            this.tsbViewObject.Click += new System.EventHandler(this.tsbViewObject_Click);
            // 
            // tsbHistory
            // 
            this.tsbHistory.Image = ((System.Drawing.Image)(resources.GetObject("tsbHistory.Image")));
            this.tsbHistory.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHistory.Name = "tsbHistory";
            this.tsbHistory.Size = new System.Drawing.Size(49, 51);
            this.tsbHistory.Text = "History";
            this.tsbHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbHistory.Click += new System.EventHandler(this.tsbHistory_Click);
            // 
            // tsbCompare
            // 
            this.tsbCompare.Image = ((System.Drawing.Image)(resources.GetObject("tsbCompare.Image")));
            this.tsbCompare.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbCompare.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCompare.Name = "tsbCompare";
            this.tsbCompare.Size = new System.Drawing.Size(60, 51);
            this.tsbCompare.Text = "Compare";
            this.tsbCompare.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbCompare.Click += new System.EventHandler(this.tsbCompare_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 55;
            this.label3.Text = "Object";
            // 
            // cbObjects
            // 
            this.cbObjects.FormattingEnabled = true;
            this.cbObjects.Location = new System.Drawing.Point(80, 190);
            this.cbObjects.Name = "cbObjects";
            this.cbObjects.Size = new System.Drawing.Size(263, 21);
            this.cbObjects.TabIndex = 53;
            this.cbObjects.SelectedIndexChanged += new System.EventHandler(this.cbObjects_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 54;
            this.label6.Text = "Database";
            // 
            // cbDatabaseName
            // 
            this.cbDatabaseName.FormattingEnabled = true;
            this.cbDatabaseName.Location = new System.Drawing.Point(80, 92);
            this.cbDatabaseName.Name = "cbDatabaseName";
            this.cbDatabaseName.Size = new System.Drawing.Size(180, 21);
            this.cbDatabaseName.TabIndex = 42;
            this.cbDatabaseName.SelectedIndexChanged += new System.EventHandler(this.cbDatabaseName_SelectedIndexChanged);
            // 
            // lServer
            // 
            this.lServer.AutoSize = true;
            this.lServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lServer.Location = new System.Drawing.Point(1, 69);
            this.lServer.Name = "lServer";
            this.lServer.Size = new System.Drawing.Size(56, 17);
            this.lServer.TabIndex = 50;
            this.lServer.Text = "Server";
            // 
            // tDateModified
            // 
            this.tDateModified.Enabled = false;
            this.tDateModified.Location = new System.Drawing.Point(80, 270);
            this.tDateModified.Name = "tDateModified";
            this.tDateModified.ReadOnly = true;
            this.tDateModified.Size = new System.Drawing.Size(147, 20);
            this.tDateModified.TabIndex = 49;
            // 
            // tDateCreated
            // 
            this.tDateCreated.Enabled = false;
            this.tDateCreated.Location = new System.Drawing.Point(80, 244);
            this.tDateCreated.Name = "tDateCreated";
            this.tDateCreated.ReadOnly = true;
            this.tDateCreated.Size = new System.Drawing.Size(147, 20);
            this.tDateCreated.TabIndex = 47;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 273);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 45;
            this.label2.Text = "Date Modified";
            // 
            // DBObjectMulti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tComment);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tObjectType);
            this.Controls.Add(this.cObjTriggers);
            this.Controls.Add(this.cObjViews);
            this.Controls.Add(this.cObjTables);
            this.Controls.Add(this.cObjFunctions);
            this.Controls.Add(this.cObjStoredProcedures);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tStatusDate);
            this.Controls.Add(this.tUser);
            this.Controls.Add(this.tStatus);
            this.Controls.Add(this.tError);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbObjects);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbDatabaseName);
            this.Controls.Add(this.lServer);
            this.Controls.Add(this.tDateModified);
            this.Controls.Add(this.tDateCreated);
            this.Controls.Add(this.label2);
            this.Name = "DBObjectMulti";
            this.Size = new System.Drawing.Size(350, 556);
            this.Load += new System.EventHandler(this.DBObjectMulti_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tComment;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tObjectType;
        private System.Windows.Forms.CheckBox cObjTriggers;
        private System.Windows.Forms.CheckBox cObjViews;
        private System.Windows.Forms.CheckBox cObjTables;
        private System.Windows.Forms.CheckBox cObjFunctions;
        private System.Windows.Forms.CheckBox cObjStoredProcedures;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tStatusDate;
        private System.Windows.Forms.TextBox tUser;
        private System.Windows.Forms.TextBox tStatus;
        private System.Windows.Forms.TextBox tError;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbFilter;
        private System.Windows.Forms.ToolStripButton tsbRemoveFilter;
        private System.Windows.Forms.ToolStripButton tsbViewObject;
        private System.Windows.Forms.ToolStripButton tsbHistory;
        private System.Windows.Forms.ToolStripButton tsbCompare;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbObjects;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbDatabaseName;
        private System.Windows.Forms.Label lServer;
        private System.Windows.Forms.TextBox tDateModified;
        private System.Windows.Forms.TextBox tDateCreated;
        private System.Windows.Forms.Label label2;
    }
}
