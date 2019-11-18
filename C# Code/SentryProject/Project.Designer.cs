namespace SentryProject
{
    partial class Project
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Project));
            this.lLegendDanger = new System.Windows.Forms.Label();
            this.lReleaseToServer = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbObjectStatus = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lReleaseFromServer = new System.Windows.Forms.Label();
            this.cbPartOfProject = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tComment = new System.Windows.Forms.TextBox();
            this.lLegendWarn = new System.Windows.Forms.Label();
            this.gbGrid = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAddItem = new System.Windows.Forms.ToolStripButton();
            this.tsbAddItemFile = new System.Windows.Forms.ToolStripButton();
            this.tsbDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbNewProject = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbMoveUp = new System.Windows.Forms.ToolStripButton();
            this.tsbMoveDown = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRelease = new System.Windows.Forms.ToolStripButton();
            this.lStatus = new System.Windows.Forms.Label();
            this.gbLegend = new System.Windows.Forms.GroupBox();
            this.pLegendDanger = new System.Windows.Forms.PictureBox();
            this.pLegendWarn = new System.Windows.Forms.PictureBox();
            this.pLegendAllOkay = new System.Windows.Forms.PictureBox();
            this.lLegendAllOkay = new System.Windows.Forms.Label();
            this.gbProject = new System.Windows.Forms.GroupBox();
            this.gbEnvironments = new System.Windows.Forms.GroupBox();
            this.gbProjectOnly = new System.Windows.Forms.GroupBox();
            this.rbActiveNo = new System.Windows.Forms.RadioButton();
            this.rbActiveYes = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbProject = new System.Windows.Forms.ComboBox();
            this.cbServers = new System.Windows.Forms.ComboBox();
            this.gbGrid.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.gbLegend.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pLegendDanger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pLegendWarn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pLegendAllOkay)).BeginInit();
            this.gbProject.SuspendLayout();
            this.gbProjectOnly.SuspendLayout();
            this.SuspendLayout();
            // 
            // lLegendDanger
            // 
            this.lLegendDanger.AutoSize = true;
            this.lLegendDanger.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lLegendDanger.Location = new System.Drawing.Point(43, 84);
            this.lLegendDanger.Name = "lLegendDanger";
            this.lLegendDanger.Size = new System.Drawing.Size(45, 16);
            this.lLegendDanger.TabIndex = 2;
            this.lLegendDanger.Text = "label6";
            // 
            // lReleaseToServer
            // 
            this.lReleaseToServer.AutoSize = true;
            this.lReleaseToServer.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lReleaseToServer.Location = new System.Drawing.Point(391, 46);
            this.lReleaseToServer.Name = "lReleaseToServer";
            this.lReleaseToServer.Size = new System.Drawing.Size(92, 13);
            this.lReleaseToServer.TabIndex = 48;
            this.lReleaseToServer.Text = "lReleaseToServer";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 51;
            this.label4.Text = "Comments";
            // 
            // cbObjectStatus
            // 
            this.cbObjectStatus.FormattingEnabled = true;
            this.cbObjectStatus.Location = new System.Drawing.Point(89, 43);
            this.cbObjectStatus.Name = "cbObjectStatus";
            this.cbObjectStatus.Size = new System.Drawing.Size(280, 21);
            this.cbObjectStatus.TabIndex = 6;
            this.cbObjectStatus.SelectedIndexChanged += new System.EventHandler(this.cbObjectStatus_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "Current Status";
            // 
            // lReleaseFromServer
            // 
            this.lReleaseFromServer.AutoSize = true;
            this.lReleaseFromServer.ForeColor = System.Drawing.Color.Red;
            this.lReleaseFromServer.Location = new System.Drawing.Point(391, 19);
            this.lReleaseFromServer.Name = "lReleaseFromServer";
            this.lReleaseFromServer.Size = new System.Drawing.Size(102, 13);
            this.lReleaseFromServer.TabIndex = 47;
            this.lReleaseFromServer.Text = "lReleaseFromServer";
            // 
            // cbPartOfProject
            // 
            this.cbPartOfProject.FormattingEnabled = true;
            this.cbPartOfProject.Location = new System.Drawing.Point(89, 16);
            this.cbPartOfProject.Name = "cbPartOfProject";
            this.cbPartOfProject.Size = new System.Drawing.Size(280, 21);
            this.cbPartOfProject.TabIndex = 5;
            this.cbPartOfProject.SelectedIndexChanged += new System.EventHandler(this.cbPartOfProject_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 46;
            this.label5.Text = "Project";
            // 
            // tComment
            // 
            this.tComment.Location = new System.Drawing.Point(89, 70);
            this.tComment.Multiline = true;
            this.tComment.Name = "tComment";
            this.tComment.Size = new System.Drawing.Size(515, 66);
            this.tComment.TabIndex = 7;
            // 
            // lLegendWarn
            // 
            this.lLegendWarn.AutoSize = true;
            this.lLegendWarn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lLegendWarn.Location = new System.Drawing.Point(43, 61);
            this.lLegendWarn.Name = "lLegendWarn";
            this.lLegendWarn.Size = new System.Drawing.Size(45, 16);
            this.lLegendWarn.TabIndex = 1;
            this.lLegendWarn.Text = "label3";
            // 
            // gbGrid
            // 
            this.gbGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGrid.Controls.Add(this.toolStrip1);
            this.gbGrid.Location = new System.Drawing.Point(3, 284);
            this.gbGrid.Name = "gbGrid";
            this.gbGrid.Size = new System.Drawing.Size(1158, 321);
            this.gbGrid.TabIndex = 62;
            this.gbGrid.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddItem,
            this.tsbAddItemFile,
            this.tsbDeleteItem,
            this.toolStripSeparator2,
            this.tsbNewProject,
            this.tsbSaveProject,
            this.toolStripSeparator1,
            this.tsbMoveUp,
            this.tsbMoveDown,
            this.toolStripSeparator3,
            this.tsbRelease});
            this.toolStrip1.Location = new System.Drawing.Point(3, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1152, 54);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAddItem
            // 
            this.tsbAddItem.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddItem.Image")));
            this.tsbAddItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbAddItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddItem.Name = "tsbAddItem";
            this.tsbAddItem.Size = new System.Drawing.Size(60, 51);
            this.tsbAddItem.Text = "Add Item";
            this.tsbAddItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbAddItem.Click += new System.EventHandler(this.tsbAddItem_Click);
            // 
            // tsbAddItemFile
            // 
            this.tsbAddItemFile.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddItemFile.Image")));
            this.tsbAddItemFile.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbAddItemFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddItemFile.Name = "tsbAddItemFile";
            this.tsbAddItemFile.Size = new System.Drawing.Size(54, 51);
            this.tsbAddItemFile.Text = "Add File";
            this.tsbAddItemFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbAddItemFile.ToolTipText = "Add Item from File";
            this.tsbAddItemFile.Click += new System.EventHandler(this.tsbAddItemFile_Click);
            // 
            // tsbDeleteItem
            // 
            this.tsbDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("tsbDeleteItem.Image")));
            this.tsbDeleteItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbDeleteItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeleteItem.Name = "tsbDeleteItem";
            this.tsbDeleteItem.Size = new System.Drawing.Size(71, 51);
            this.tsbDeleteItem.Text = "Delete Item";
            this.tsbDeleteItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbDeleteItem.Click += new System.EventHandler(this.tsbDeleteItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 54);
            // 
            // tsbNewProject
            // 
            this.tsbNewProject.Image = ((System.Drawing.Image)(resources.GetObject("tsbNewProject.Image")));
            this.tsbNewProject.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbNewProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNewProject.Name = "tsbNewProject";
            this.tsbNewProject.Size = new System.Drawing.Size(75, 51);
            this.tsbNewProject.Text = "New Project";
            this.tsbNewProject.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbNewProject.Click += new System.EventHandler(this.tsbNewProject_Click);
            // 
            // tsbSaveProject
            // 
            this.tsbSaveProject.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveProject.Image")));
            this.tsbSaveProject.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSaveProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveProject.Name = "tsbSaveProject";
            this.tsbSaveProject.Size = new System.Drawing.Size(75, 51);
            this.tsbSaveProject.Text = "Save Project";
            this.tsbSaveProject.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbSaveProject.Click += new System.EventHandler(this.tsbSaveProject_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 54);
            // 
            // tsbMoveUp
            // 
            this.tsbMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("tsbMoveUp.Image")));
            this.tsbMoveUp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMoveUp.Name = "tsbMoveUp";
            this.tsbMoveUp.Size = new System.Drawing.Size(52, 51);
            this.tsbMoveUp.Text = "Row Up";
            this.tsbMoveUp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbMoveUp.Click += new System.EventHandler(this.tsbMoveUp_Click);
            // 
            // tsbMoveDown
            // 
            this.tsbMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("tsbMoveDown.Image")));
            this.tsbMoveDown.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMoveDown.Name = "tsbMoveDown";
            this.tsbMoveDown.Size = new System.Drawing.Size(68, 51);
            this.tsbMoveDown.Text = "Row Down";
            this.tsbMoveDown.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbMoveDown.Click += new System.EventHandler(this.tsbMoveDown_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 54);
            // 
            // tsbRelease
            // 
            this.tsbRelease.Image = ((System.Drawing.Image)(resources.GetObject("tsbRelease.Image")));
            this.tsbRelease.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbRelease.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRelease.Name = "tsbRelease";
            this.tsbRelease.Size = new System.Drawing.Size(50, 51);
            this.tsbRelease.Text = "Release";
            this.tsbRelease.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbRelease.Click += new System.EventHandler(this.tsbRelease_Click);
            // 
            // lStatus
            // 
            this.lStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lStatus.AutoSize = true;
            this.lStatus.Location = new System.Drawing.Point(3, 617);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(37, 13);
            this.lStatus.TabIndex = 6;
            this.lStatus.Text = "Status";
            // 
            // gbLegend
            // 
            this.gbLegend.Controls.Add(this.pLegendDanger);
            this.gbLegend.Controls.Add(this.pLegendWarn);
            this.gbLegend.Controls.Add(this.pLegendAllOkay);
            this.gbLegend.Controls.Add(this.lLegendDanger);
            this.gbLegend.Controls.Add(this.lLegendWarn);
            this.gbLegend.Controls.Add(this.lLegendAllOkay);
            this.gbLegend.Dock = System.Windows.Forms.DockStyle.Right;
            this.gbLegend.Location = new System.Drawing.Point(856, 16);
            this.gbLegend.Name = "gbLegend";
            this.gbLegend.Size = new System.Drawing.Size(302, 131);
            this.gbLegend.TabIndex = 53;
            this.gbLegend.TabStop = false;
            this.gbLegend.Text = "Legend";
            // 
            // pLegendDanger
            // 
            this.pLegendDanger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pLegendDanger.Location = new System.Drawing.Point(15, 83);
            this.pLegendDanger.Name = "pLegendDanger";
            this.pLegendDanger.Size = new System.Drawing.Size(22, 17);
            this.pLegendDanger.TabIndex = 5;
            this.pLegendDanger.TabStop = false;
            // 
            // pLegendWarn
            // 
            this.pLegendWarn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pLegendWarn.Location = new System.Drawing.Point(15, 60);
            this.pLegendWarn.Name = "pLegendWarn";
            this.pLegendWarn.Size = new System.Drawing.Size(22, 17);
            this.pLegendWarn.TabIndex = 4;
            this.pLegendWarn.TabStop = false;
            // 
            // pLegendAllOkay
            // 
            this.pLegendAllOkay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pLegendAllOkay.Location = new System.Drawing.Point(15, 37);
            this.pLegendAllOkay.Name = "pLegendAllOkay";
            this.pLegendAllOkay.Size = new System.Drawing.Size(22, 17);
            this.pLegendAllOkay.TabIndex = 3;
            this.pLegendAllOkay.TabStop = false;
            // 
            // lLegendAllOkay
            // 
            this.lLegendAllOkay.AutoSize = true;
            this.lLegendAllOkay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lLegendAllOkay.Location = new System.Drawing.Point(43, 38);
            this.lLegendAllOkay.Name = "lLegendAllOkay";
            this.lLegendAllOkay.Size = new System.Drawing.Size(45, 16);
            this.lLegendAllOkay.TabIndex = 0;
            this.lLegendAllOkay.Text = "label2";
            // 
            // gbProject
            // 
            this.gbProject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbProject.Controls.Add(this.gbLegend);
            this.gbProject.Controls.Add(this.tComment);
            this.gbProject.Controls.Add(this.label4);
            this.gbProject.Controls.Add(this.cbObjectStatus);
            this.gbProject.Controls.Add(this.label1);
            this.gbProject.Controls.Add(this.lReleaseToServer);
            this.gbProject.Controls.Add(this.lReleaseFromServer);
            this.gbProject.Controls.Add(this.cbPartOfProject);
            this.gbProject.Controls.Add(this.label5);
            this.gbProject.Location = new System.Drawing.Point(3, 128);
            this.gbProject.Name = "gbProject";
            this.gbProject.Size = new System.Drawing.Size(1161, 150);
            this.gbProject.TabIndex = 61;
            this.gbProject.TabStop = false;
            this.gbProject.Text = "Select Project";
            // 
            // gbEnvironments
            // 
            this.gbEnvironments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbEnvironments.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbEnvironments.Location = new System.Drawing.Point(3, 3);
            this.gbEnvironments.Name = "gbEnvironments";
            this.gbEnvironments.Size = new System.Drawing.Size(1054, 108);
            this.gbEnvironments.TabIndex = 63;
            this.gbEnvironments.TabStop = false;
            this.gbEnvironments.Text = "Environments";
            // 
            // gbProjectOnly
            // 
            this.gbProjectOnly.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbProjectOnly.Controls.Add(this.rbActiveNo);
            this.gbProjectOnly.Controls.Add(this.rbActiveYes);
            this.gbProjectOnly.Controls.Add(this.label6);
            this.gbProjectOnly.Controls.Add(this.label3);
            this.gbProjectOnly.Controls.Add(this.label2);
            this.gbProjectOnly.Controls.Add(this.cbProject);
            this.gbProjectOnly.Controls.Add(this.cbServers);
            this.gbProjectOnly.Location = new System.Drawing.Point(1063, 7);
            this.gbProjectOnly.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.gbProjectOnly.Name = "gbProjectOnly";
            this.gbProjectOnly.Size = new System.Drawing.Size(1137, 115);
            this.gbProjectOnly.TabIndex = 65;
            this.gbProjectOnly.TabStop = false;
            this.gbProjectOnly.Text = "Project";
            // 
            // rbActiveNo
            // 
            this.rbActiveNo.AutoSize = true;
            this.rbActiveNo.Location = new System.Drawing.Point(182, 73);
            this.rbActiveNo.Name = "rbActiveNo";
            this.rbActiveNo.Size = new System.Drawing.Size(39, 17);
            this.rbActiveNo.TabIndex = 56;
            this.rbActiveNo.Text = "No";
            this.rbActiveNo.UseVisualStyleBackColor = true;
            // 
            // rbActiveYes
            // 
            this.rbActiveYes.AutoSize = true;
            this.rbActiveYes.Checked = true;
            this.rbActiveYes.Location = new System.Drawing.Point(133, 73);
            this.rbActiveYes.Name = "rbActiveYes";
            this.rbActiveYes.Size = new System.Drawing.Size(43, 17);
            this.rbActiveYes.TabIndex = 55;
            this.rbActiveYes.TabStop = true;
            this.rbActiveYes.Text = "Yes";
            this.rbActiveYes.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(54, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 54;
            this.label6.Text = "Project Active";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(87, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 53;
            this.label3.Text = "Project";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 52;
            this.label2.Text = "Base Server";
            // 
            // cbProject
            // 
            this.cbProject.FormattingEnabled = true;
            this.cbProject.Location = new System.Drawing.Point(133, 46);
            this.cbProject.Name = "cbProject";
            this.cbProject.Size = new System.Drawing.Size(317, 21);
            this.cbProject.TabIndex = 51;
            this.cbProject.SelectedIndexChanged += new System.EventHandler(this.cbProject_SelectedIndexChanged);
            // 
            // cbServers
            // 
            this.cbServers.FormattingEnabled = true;
            this.cbServers.Location = new System.Drawing.Point(133, 19);
            this.cbServers.Name = "cbServers";
            this.cbServers.Size = new System.Drawing.Size(317, 21);
            this.cbServers.TabIndex = 50;
            this.cbServers.SelectedIndexChanged += new System.EventHandler(this.cbServers_SelectedIndexChanged);
            // 
            // Project
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbProjectOnly);
            this.Controls.Add(this.lStatus);
            this.Controls.Add(this.gbEnvironments);
            this.Controls.Add(this.gbGrid);
            this.Controls.Add(this.gbProject);
            this.Name = "Project";
            this.Size = new System.Drawing.Size(1176, 639);
            this.Load += new System.EventHandler(this.Project_Load);
            this.Resize += new System.EventHandler(this.Project_Resize);
            this.gbGrid.ResumeLayout(false);
            this.gbGrid.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gbLegend.ResumeLayout(false);
            this.gbLegend.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pLegendDanger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pLegendWarn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pLegendAllOkay)).EndInit();
            this.gbProject.ResumeLayout(false);
            this.gbProject.PerformLayout();
            this.gbProjectOnly.ResumeLayout(false);
            this.gbProjectOnly.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lLegendDanger;
        private System.Windows.Forms.Label lReleaseToServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbObjectStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lReleaseFromServer;
        private System.Windows.Forms.ComboBox cbPartOfProject;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tComment;
        private System.Windows.Forms.Label lLegendWarn;
        private System.Windows.Forms.GroupBox gbGrid;
        private System.Windows.Forms.Label lStatus;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAddItem;
        private System.Windows.Forms.ToolStripButton tsbAddItemFile;
        private System.Windows.Forms.ToolStripButton tsbDeleteItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbNewProject;
        private System.Windows.Forms.ToolStripButton tsbSaveProject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbMoveUp;
        private System.Windows.Forms.ToolStripButton tsbMoveDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbRelease;
        private System.Windows.Forms.GroupBox gbLegend;
        private System.Windows.Forms.Label lLegendAllOkay;
        private System.Windows.Forms.GroupBox gbProject;
        private System.Windows.Forms.GroupBox gbEnvironments;
        private System.Windows.Forms.GroupBox gbProjectOnly;
        private System.Windows.Forms.RadioButton rbActiveNo;
        private System.Windows.Forms.RadioButton rbActiveYes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbProject;
        private System.Windows.Forms.ComboBox cbServers;
        private System.Windows.Forms.PictureBox pLegendAllOkay;
        private System.Windows.Forms.PictureBox pLegendDanger;
        private System.Windows.Forms.PictureBox pLegendWarn;
    }
}
