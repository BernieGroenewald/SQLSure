namespace SentryBackupRestore
{
    partial class BackupRestore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackupRestore));
            this.tCreateDate = new System.Windows.Forms.TextBox();
            this.cRestoreObjTriggers = new System.Windows.Forms.CheckBox();
            this.gbRestoreDatabase = new System.Windows.Forms.GroupBox();
            this.gbRestoreObjectTypes = new System.Windows.Forms.GroupBox();
            this.cRestoreObjViews = new System.Windows.Forms.CheckBox();
            this.cRestoreObjTables = new System.Windows.Forms.CheckBox();
            this.cRestoreObjFunctions = new System.Windows.Forms.CheckBox();
            this.cRestoreObjStoredProcedures = new System.Windows.Forms.CheckBox();
            this.gbRestoreVarious = new System.Windows.Forms.GroupBox();
            this.cmdRestoreNameSearch = new System.Windows.Forms.Button();
            this.cbRestoreUserName = new System.Windows.Forms.ComboBox();
            this.tRestoreObjectName = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cbObjectStatusRestore = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.gbRestoreGrid = new System.Windows.Forms.GroupBox();
            this.tpRestore = new System.Windows.Forms.TabPage();
            this.gbRestoreFilter = new System.Windows.Forms.GroupBox();
            this.gbRestoreSet = new System.Windows.Forms.GroupBox();
            this.lRestoreSetCount = new System.Windows.Forms.Label();
            this.lRestoreStatus = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tBackupServerName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tRestoreDateCreated = new System.Windows.Forms.TextBox();
            this.tRestoreComment = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cbRestoreSetName = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbRestoreToServer = new System.Windows.Forms.ComboBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsbRestore = new System.Windows.Forms.ToolStripButton();
            this.toolStripProgressBar2 = new System.Windows.Forms.ToolStripProgressBar();
            this.gbDatabases = new System.Windows.Forms.GroupBox();
            this.rbActiveNo = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.rbActiveYes = new System.Windows.Forms.RadioButton();
            this.tsbSaveSet = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbNewSet = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSelectAll = new System.Windows.Forms.ToolStripButton();
            this.tsbDeSelectAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbClear = new System.Windows.Forms.ToolStripButton();
            this.tsbFetchObjects = new System.Windows.Forms.ToolStripButton();
            this.tsbBackUp = new System.Windows.Forms.ToolStripButton();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.cbObjectStatus = new System.Windows.Forms.ComboBox();
            this.gbGrid = new System.Windows.Forms.GroupBox();
            this.gbCriteria = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lStatus = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.rbSpecificDate = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.rbSixMonths = new System.Windows.Forms.RadioButton();
            this.rbThreeMonths = new System.Windows.Forms.RadioButton();
            this.rbOneMonth = new System.Windows.Forms.RadioButton();
            this.tpBackup = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.gbObjectTypes = new System.Windows.Forms.GroupBox();
            this.cObjTriggers = new System.Windows.Forms.CheckBox();
            this.cObjViews = new System.Windows.Forms.CheckBox();
            this.cObjTables = new System.Windows.Forms.CheckBox();
            this.cObjFunctions = new System.Windows.Forms.CheckBox();
            this.cObjStoredProcedures = new System.Windows.Forms.CheckBox();
            this.gbSetDetails = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tComment = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbBackupSetName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbServers = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.gbRestoreObjectTypes.SuspendLayout();
            this.gbRestoreVarious.SuspendLayout();
            this.tpRestore.SuspendLayout();
            this.gbRestoreFilter.SuspendLayout();
            this.gbRestoreSet.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.gbCriteria.SuspendLayout();
            this.tpBackup.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.gbObjectTypes.SuspendLayout();
            this.gbSetDetails.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tCreateDate
            // 
            this.tCreateDate.Enabled = false;
            this.tCreateDate.Location = new System.Drawing.Point(167, 123);
            this.tCreateDate.Name = "tCreateDate";
            this.tCreateDate.Size = new System.Drawing.Size(240, 20);
            this.tCreateDate.TabIndex = 62;
            // 
            // cRestoreObjTriggers
            // 
            this.cRestoreObjTriggers.AutoSize = true;
            this.cRestoreObjTriggers.Location = new System.Drawing.Point(29, 113);
            this.cRestoreObjTriggers.Name = "cRestoreObjTriggers";
            this.cRestoreObjTriggers.Size = new System.Drawing.Size(64, 17);
            this.cRestoreObjTriggers.TabIndex = 4;
            this.cRestoreObjTriggers.Tag = "Trigger";
            this.cRestoreObjTriggers.Text = "Triggers";
            this.cRestoreObjTriggers.UseVisualStyleBackColor = true;
            this.cRestoreObjTriggers.CheckedChanged += new System.EventHandler(this.cRestoreObjTriggers_CheckedChanged);
            // 
            // gbRestoreDatabase
            // 
            this.gbRestoreDatabase.Location = new System.Drawing.Point(185, 19);
            this.gbRestoreDatabase.Name = "gbRestoreDatabase";
            this.gbRestoreDatabase.Size = new System.Drawing.Size(793, 182);
            this.gbRestoreDatabase.TabIndex = 4;
            this.gbRestoreDatabase.TabStop = false;
            this.gbRestoreDatabase.Text = "Databases";
            // 
            // gbRestoreObjectTypes
            // 
            this.gbRestoreObjectTypes.Controls.Add(this.cRestoreObjTriggers);
            this.gbRestoreObjectTypes.Controls.Add(this.cRestoreObjViews);
            this.gbRestoreObjectTypes.Controls.Add(this.cRestoreObjTables);
            this.gbRestoreObjectTypes.Controls.Add(this.cRestoreObjFunctions);
            this.gbRestoreObjectTypes.Controls.Add(this.cRestoreObjStoredProcedures);
            this.gbRestoreObjectTypes.Location = new System.Drawing.Point(6, 19);
            this.gbRestoreObjectTypes.Name = "gbRestoreObjectTypes";
            this.gbRestoreObjectTypes.Size = new System.Drawing.Size(173, 182);
            this.gbRestoreObjectTypes.TabIndex = 3;
            this.gbRestoreObjectTypes.TabStop = false;
            this.gbRestoreObjectTypes.Text = "Database Object Types";
            // 
            // cRestoreObjViews
            // 
            this.cRestoreObjViews.AutoSize = true;
            this.cRestoreObjViews.Location = new System.Drawing.Point(29, 90);
            this.cRestoreObjViews.Name = "cRestoreObjViews";
            this.cRestoreObjViews.Size = new System.Drawing.Size(54, 17);
            this.cRestoreObjViews.TabIndex = 3;
            this.cRestoreObjViews.Tag = "View";
            this.cRestoreObjViews.Text = "Views";
            this.cRestoreObjViews.UseVisualStyleBackColor = true;
            this.cRestoreObjViews.CheckedChanged += new System.EventHandler(this.cRestoreObjViews_CheckedChanged);
            // 
            // cRestoreObjTables
            // 
            this.cRestoreObjTables.AutoSize = true;
            this.cRestoreObjTables.Location = new System.Drawing.Point(29, 67);
            this.cRestoreObjTables.Name = "cRestoreObjTables";
            this.cRestoreObjTables.Size = new System.Drawing.Size(58, 17);
            this.cRestoreObjTables.TabIndex = 2;
            this.cRestoreObjTables.Tag = "User Table";
            this.cRestoreObjTables.Text = "Tables";
            this.cRestoreObjTables.UseVisualStyleBackColor = true;
            this.cRestoreObjTables.CheckedChanged += new System.EventHandler(this.cRestoreObjTables_CheckedChanged);
            // 
            // cRestoreObjFunctions
            // 
            this.cRestoreObjFunctions.AutoSize = true;
            this.cRestoreObjFunctions.Location = new System.Drawing.Point(29, 44);
            this.cRestoreObjFunctions.Name = "cRestoreObjFunctions";
            this.cRestoreObjFunctions.Size = new System.Drawing.Size(72, 17);
            this.cRestoreObjFunctions.TabIndex = 1;
            this.cRestoreObjFunctions.Tag = "Scalar Function";
            this.cRestoreObjFunctions.Text = "Functions";
            this.cRestoreObjFunctions.UseVisualStyleBackColor = true;
            this.cRestoreObjFunctions.CheckedChanged += new System.EventHandler(this.cRestoreObjFunctions_CheckedChanged);
            // 
            // cRestoreObjStoredProcedures
            // 
            this.cRestoreObjStoredProcedures.AutoSize = true;
            this.cRestoreObjStoredProcedures.Checked = true;
            this.cRestoreObjStoredProcedures.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cRestoreObjStoredProcedures.Location = new System.Drawing.Point(29, 21);
            this.cRestoreObjStoredProcedures.Name = "cRestoreObjStoredProcedures";
            this.cRestoreObjStoredProcedures.Size = new System.Drawing.Size(114, 17);
            this.cRestoreObjStoredProcedures.TabIndex = 0;
            this.cRestoreObjStoredProcedures.Tag = "Stored Procedure";
            this.cRestoreObjStoredProcedures.Text = "Stored Procedures";
            this.cRestoreObjStoredProcedures.UseVisualStyleBackColor = true;
            this.cRestoreObjStoredProcedures.CheckedChanged += new System.EventHandler(this.cRestoreObjStoredProcedures_CheckedChanged);
            // 
            // gbRestoreVarious
            // 
            this.gbRestoreVarious.Controls.Add(this.cmdRestoreNameSearch);
            this.gbRestoreVarious.Controls.Add(this.cbRestoreUserName);
            this.gbRestoreVarious.Controls.Add(this.tRestoreObjectName);
            this.gbRestoreVarious.Controls.Add(this.label14);
            this.gbRestoreVarious.Controls.Add(this.label13);
            this.gbRestoreVarious.Controls.Add(this.cbObjectStatusRestore);
            this.gbRestoreVarious.Controls.Add(this.label12);
            this.gbRestoreVarious.Location = new System.Drawing.Point(985, 20);
            this.gbRestoreVarious.Name = "gbRestoreVarious";
            this.gbRestoreVarious.Size = new System.Drawing.Size(461, 181);
            this.gbRestoreVarious.TabIndex = 5;
            this.gbRestoreVarious.TabStop = false;
            this.gbRestoreVarious.Text = "Various Filters";
            // 
            // cmdRestoreNameSearch
            // 
            this.cmdRestoreNameSearch.Image = ((System.Drawing.Image)(resources.GetObject("cmdRestoreNameSearch.Image")));
            this.cmdRestoreNameSearch.Location = new System.Drawing.Point(414, 11);
            this.cmdRestoreNameSearch.Name = "cmdRestoreNameSearch";
            this.cmdRestoreNameSearch.Size = new System.Drawing.Size(38, 36);
            this.cmdRestoreNameSearch.TabIndex = 62;
            this.cmdRestoreNameSearch.UseVisualStyleBackColor = true;
            this.cmdRestoreNameSearch.Click += new System.EventHandler(this.cmdRestoreNameSearch_Click);
            // 
            // cbRestoreUserName
            // 
            this.cbRestoreUserName.FormattingEnabled = true;
            this.cbRestoreUserName.Location = new System.Drawing.Point(140, 46);
            this.cbRestoreUserName.Name = "cbRestoreUserName";
            this.cbRestoreUserName.Size = new System.Drawing.Size(268, 21);
            this.cbRestoreUserName.TabIndex = 1;
            this.cbRestoreUserName.SelectedIndexChanged += new System.EventHandler(this.cbRestoreUserName_SelectedIndexChanged);
            // 
            // tRestoreObjectName
            // 
            this.tRestoreObjectName.Location = new System.Drawing.Point(140, 20);
            this.tRestoreObjectName.Name = "tRestoreObjectName";
            this.tRestoreObjectName.Size = new System.Drawing.Size(268, 20);
            this.tRestoreObjectName.TabIndex = 0;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(63, 76);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 13);
            this.label14.TabIndex = 60;
            this.label14.Text = "Object Status";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(74, 49);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 13);
            this.label13.TabIndex = 59;
            this.label13.Text = "User Name";
            // 
            // cbObjectStatusRestore
            // 
            this.cbObjectStatusRestore.FormattingEnabled = true;
            this.cbObjectStatusRestore.Location = new System.Drawing.Point(140, 73);
            this.cbObjectStatusRestore.Name = "cbObjectStatusRestore";
            this.cbObjectStatusRestore.Size = new System.Drawing.Size(268, 21);
            this.cbObjectStatusRestore.TabIndex = 2;
            this.cbObjectStatusRestore.SelectedIndexChanged += new System.EventHandler(this.cbObjectStatusRestore_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(46, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(88, 13);
            this.label12.TabIndex = 57;
            this.label12.Text = "Object Name like";
            // 
            // gbRestoreGrid
            // 
            this.gbRestoreGrid.Location = new System.Drawing.Point(12, 387);
            this.gbRestoreGrid.Name = "gbRestoreGrid";
            this.gbRestoreGrid.Size = new System.Drawing.Size(1446, 317);
            this.gbRestoreGrid.TabIndex = 5;
            this.gbRestoreGrid.TabStop = false;
            this.gbRestoreGrid.Text = "Objects";
            // 
            // tpRestore
            // 
            this.tpRestore.Controls.Add(this.gbRestoreGrid);
            this.tpRestore.Controls.Add(this.gbRestoreFilter);
            this.tpRestore.Controls.Add(this.gbRestoreSet);
            this.tpRestore.Controls.Add(this.toolStrip2);
            this.tpRestore.Location = new System.Drawing.Point(4, 22);
            this.tpRestore.Name = "tpRestore";
            this.tpRestore.Padding = new System.Windows.Forms.Padding(3);
            this.tpRestore.Size = new System.Drawing.Size(1464, 710);
            this.tpRestore.TabIndex = 1;
            this.tpRestore.Text = "Restore";
            this.tpRestore.UseVisualStyleBackColor = true;
            // 
            // gbRestoreFilter
            // 
            this.gbRestoreFilter.Controls.Add(this.gbRestoreVarious);
            this.gbRestoreFilter.Controls.Add(this.gbRestoreDatabase);
            this.gbRestoreFilter.Controls.Add(this.gbRestoreObjectTypes);
            this.gbRestoreFilter.Location = new System.Drawing.Point(6, 171);
            this.gbRestoreFilter.Name = "gbRestoreFilter";
            this.gbRestoreFilter.Size = new System.Drawing.Size(1452, 210);
            this.gbRestoreFilter.TabIndex = 2;
            this.gbRestoreFilter.TabStop = false;
            this.gbRestoreFilter.Text = "Restore Set Filters";
            // 
            // gbRestoreSet
            // 
            this.gbRestoreSet.Controls.Add(this.lRestoreSetCount);
            this.gbRestoreSet.Controls.Add(this.lRestoreStatus);
            this.gbRestoreSet.Controls.Add(this.label8);
            this.gbRestoreSet.Controls.Add(this.tBackupServerName);
            this.gbRestoreSet.Controls.Add(this.label7);
            this.gbRestoreSet.Controls.Add(this.tRestoreDateCreated);
            this.gbRestoreSet.Controls.Add(this.tRestoreComment);
            this.gbRestoreSet.Controls.Add(this.label9);
            this.gbRestoreSet.Controls.Add(this.label10);
            this.gbRestoreSet.Controls.Add(this.cbRestoreSetName);
            this.gbRestoreSet.Controls.Add(this.label11);
            this.gbRestoreSet.Controls.Add(this.cbRestoreToServer);
            this.gbRestoreSet.Location = new System.Drawing.Point(6, 60);
            this.gbRestoreSet.Name = "gbRestoreSet";
            this.gbRestoreSet.Size = new System.Drawing.Size(1452, 105);
            this.gbRestoreSet.TabIndex = 1;
            this.gbRestoreSet.TabStop = false;
            this.gbRestoreSet.Text = "Set Details";
            // 
            // lRestoreSetCount
            // 
            this.lRestoreSetCount.AutoSize = true;
            this.lRestoreSetCount.Location = new System.Drawing.Point(890, 64);
            this.lRestoreSetCount.Name = "lRestoreSetCount";
            this.lRestoreSetCount.Size = new System.Drawing.Size(0, 13);
            this.lRestoreSetCount.TabIndex = 67;
            // 
            // lRestoreStatus
            // 
            this.lRestoreStatus.AutoSize = true;
            this.lRestoreStatus.Location = new System.Drawing.Point(890, 31);
            this.lRestoreStatus.Name = "lRestoreStatus";
            this.lRestoreStatus.Size = new System.Drawing.Size(0, 13);
            this.lRestoreStatus.TabIndex = 66;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(101, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 65;
            this.label8.Text = "Backup Server";
            // 
            // tBackupServerName
            // 
            this.tBackupServerName.Enabled = false;
            this.tBackupServerName.Location = new System.Drawing.Point(185, 42);
            this.tBackupServerName.Name = "tBackupServerName";
            this.tBackupServerName.Size = new System.Drawing.Size(240, 20);
            this.tBackupServerName.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(537, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 63;
            this.label7.Text = "Create Date";
            // 
            // tRestoreDateCreated
            // 
            this.tRestoreDateCreated.Enabled = false;
            this.tRestoreDateCreated.Location = new System.Drawing.Point(607, 64);
            this.tRestoreDateCreated.Name = "tRestoreDateCreated";
            this.tRestoreDateCreated.Size = new System.Drawing.Size(240, 20);
            this.tRestoreDateCreated.TabIndex = 62;
            // 
            // tRestoreComment
            // 
            this.tRestoreComment.Enabled = false;
            this.tRestoreComment.Location = new System.Drawing.Point(607, 14);
            this.tRestoreComment.Multiline = true;
            this.tRestoreComment.Name = "tRestoreComment";
            this.tRestoreComment.Size = new System.Drawing.Size(240, 44);
            this.tRestoreComment.TabIndex = 57;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(482, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(119, 13);
            this.label9.TabIndex = 58;
            this.label9.Text = "Backup Set Description";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(85, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(94, 13);
            this.label10.TabIndex = 56;
            this.label10.Text = "Backup Set Name";
            // 
            // cbRestoreSetName
            // 
            this.cbRestoreSetName.FormattingEnabled = true;
            this.cbRestoreSetName.Location = new System.Drawing.Point(185, 14);
            this.cbRestoreSetName.Name = "cbRestoreSetName";
            this.cbRestoreSetName.Size = new System.Drawing.Size(240, 21);
            this.cbRestoreSetName.TabIndex = 0;
            this.cbRestoreSetName.SelectedIndexChanged += new System.EventHandler(this.cbRestoreSetName_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(89, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 13);
            this.label11.TabIndex = 54;
            this.label11.Text = "Restore to Server";
            // 
            // cbRestoreToServer
            // 
            this.cbRestoreToServer.FormattingEnabled = true;
            this.cbRestoreToServer.Location = new System.Drawing.Point(185, 68);
            this.cbRestoreToServer.Name = "cbRestoreToServer";
            this.cbRestoreToServer.Size = new System.Drawing.Size(240, 21);
            this.cbRestoreToServer.TabIndex = 2;
            this.cbRestoreToServer.SelectedIndexChanged += new System.EventHandler(this.cbRestoreToServer_SelectedIndexChanged);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRestore,
            this.toolStripProgressBar2});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1458, 54);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsbRestore
            // 
            this.tsbRestore.Image = ((System.Drawing.Image)(resources.GetObject("tsbRestore.Image")));
            this.tsbRestore.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbRestore.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRestore.Name = "tsbRestore";
            this.tsbRestore.Size = new System.Drawing.Size(50, 51);
            this.tsbRestore.Text = "Restore";
            this.tsbRestore.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbRestore.Click += new System.EventHandler(this.tsbRestore_Click);
            // 
            // toolStripProgressBar2
            // 
            this.toolStripProgressBar2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar2.AutoSize = false;
            this.toolStripProgressBar2.Name = "toolStripProgressBar2";
            this.toolStripProgressBar2.Size = new System.Drawing.Size(500, 25);
            // 
            // gbDatabases
            // 
            this.gbDatabases.Location = new System.Drawing.Point(474, 73);
            this.gbDatabases.Name = "gbDatabases";
            this.gbDatabases.Size = new System.Drawing.Size(793, 182);
            this.gbDatabases.TabIndex = 1;
            this.gbDatabases.TabStop = false;
            this.gbDatabases.Text = "Databases";
            // 
            // rbActiveNo
            // 
            this.rbActiveNo.AutoSize = true;
            this.rbActiveNo.Location = new System.Drawing.Point(216, 149);
            this.rbActiveNo.Name = "rbActiveNo";
            this.rbActiveNo.Size = new System.Drawing.Size(39, 17);
            this.rbActiveNo.TabIndex = 61;
            this.rbActiveNo.Text = "No";
            this.rbActiveNo.UseVisualStyleBackColor = true;
            this.rbActiveNo.CheckedChanged += new System.EventHandler(this.rbActiveNo_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(97, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 63;
            this.label3.Text = "Create Date";
            // 
            // rbActiveYes
            // 
            this.rbActiveYes.AutoSize = true;
            this.rbActiveYes.Checked = true;
            this.rbActiveYes.Location = new System.Drawing.Point(167, 149);
            this.rbActiveYes.Name = "rbActiveYes";
            this.rbActiveYes.Size = new System.Drawing.Size(43, 17);
            this.rbActiveYes.TabIndex = 60;
            this.rbActiveYes.TabStop = true;
            this.rbActiveYes.Text = "Yes";
            this.rbActiveYes.UseVisualStyleBackColor = true;
            this.rbActiveYes.CheckedChanged += new System.EventHandler(this.rbActiveYes_CheckedChanged);
            // 
            // tsbSaveSet
            // 
            this.tsbSaveSet.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveSet.Image")));
            this.tsbSaveSet.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSaveSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveSet.Name = "tsbSaveSet";
            this.tsbSaveSet.Size = new System.Drawing.Size(54, 51);
            this.tsbSaveSet.Text = "Save Set";
            this.tsbSaveSet.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbSaveSet.Click += new System.EventHandler(this.tsbSaveSet_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNewSet,
            this.tsbSaveSet,
            this.toolStripSeparator1,
            this.tsbSelectAll,
            this.tsbDeSelectAll,
            this.toolStripSeparator2,
            this.tsbClear,
            this.tsbFetchObjects,
            this.tsbBackUp,
            this.toolStripProgressBar1});
            this.toolStrip1.Location = new System.Drawing.Point(3, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1446, 54);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbNewSet
            // 
            this.tsbNewSet.Image = ((System.Drawing.Image)(resources.GetObject("tsbNewSet.Image")));
            this.tsbNewSet.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbNewSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNewSet.Name = "tsbNewSet";
            this.tsbNewSet.Size = new System.Drawing.Size(54, 51);
            this.tsbNewSet.Text = "New Set";
            this.tsbNewSet.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbNewSet.Click += new System.EventHandler(this.tsbNewSet_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 54);
            // 
            // tsbSelectAll
            // 
            this.tsbSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("tsbSelectAll.Image")));
            this.tsbSelectAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSelectAll.Name = "tsbSelectAll";
            this.tsbSelectAll.Size = new System.Drawing.Size(59, 51);
            this.tsbSelectAll.Text = "Select All";
            this.tsbSelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbSelectAll.ToolTipText = "Select All Databases";
            this.tsbSelectAll.Click += new System.EventHandler(this.tsbSelectAll_Click);
            // 
            // tsbDeSelectAll
            // 
            this.tsbDeSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("tsbDeSelectAll.Image")));
            this.tsbDeSelectAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbDeSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeSelectAll.Name = "tsbDeSelectAll";
            this.tsbDeSelectAll.Size = new System.Drawing.Size(78, 51);
            this.tsbDeSelectAll.Text = "De-Select All";
            this.tsbDeSelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbDeSelectAll.ToolTipText = "De-Select All Databases";
            this.tsbDeSelectAll.Click += new System.EventHandler(this.tsbDeSelectAll_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 54);
            // 
            // tsbClear
            // 
            this.tsbClear.Image = ((System.Drawing.Image)(resources.GetObject("tsbClear.Image")));
            this.tsbClear.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClear.Name = "tsbClear";
            this.tsbClear.Size = new System.Drawing.Size(63, 51);
            this.tsbClear.Text = "Clear Grid";
            this.tsbClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbClear.Click += new System.EventHandler(this.tsbClear_Click);
            // 
            // tsbFetchObjects
            // 
            this.tsbFetchObjects.Image = ((System.Drawing.Image)(resources.GetObject("tsbFetchObjects.Image")));
            this.tsbFetchObjects.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbFetchObjects.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFetchObjects.Name = "tsbFetchObjects";
            this.tsbFetchObjects.Size = new System.Drawing.Size(72, 51);
            this.tsbFetchObjects.Text = "Get Objects";
            this.tsbFetchObjects.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbFetchObjects.Click += new System.EventHandler(this.tsbFetchObjects_Click);
            // 
            // tsbBackUp
            // 
            this.tsbBackUp.Image = ((System.Drawing.Image)(resources.GetObject("tsbBackUp.Image")));
            this.tsbBackUp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbBackUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBackUp.Name = "tsbBackUp";
            this.tsbBackUp.Size = new System.Drawing.Size(50, 51);
            this.tsbBackUp.Text = "Backup";
            this.tsbBackUp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbBackUp.Click += new System.EventHandler(this.tsbBackUp_Click);
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar1.AutoSize = false;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(500, 25);
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // cbObjectStatus
            // 
            this.cbObjectStatus.FormattingEnabled = true;
            this.cbObjectStatus.Location = new System.Drawing.Point(142, 57);
            this.cbObjectStatus.Name = "cbObjectStatus";
            this.cbObjectStatus.Size = new System.Drawing.Size(280, 21);
            this.cbObjectStatus.TabIndex = 56;
            this.cbObjectStatus.SelectedIndexChanged += new System.EventHandler(this.cbObjectStatus_SelectedIndexChanged);
            // 
            // gbGrid
            // 
            this.gbGrid.Location = new System.Drawing.Point(7, 358);
            this.gbGrid.Name = "gbGrid";
            this.gbGrid.Size = new System.Drawing.Size(1439, 334);
            this.gbGrid.TabIndex = 4;
            this.gbGrid.TabStop = false;
            this.gbGrid.Text = "Objects";
            // 
            // gbCriteria
            // 
            this.gbCriteria.Controls.Add(this.cbObjectStatus);
            this.gbCriteria.Controls.Add(this.label5);
            this.gbCriteria.Controls.Add(this.lStatus);
            this.gbCriteria.Controls.Add(this.dtpDate);
            this.gbCriteria.Controls.Add(this.rbSpecificDate);
            this.gbCriteria.Controls.Add(this.rbAll);
            this.gbCriteria.Controls.Add(this.rbSixMonths);
            this.gbCriteria.Controls.Add(this.rbThreeMonths);
            this.gbCriteria.Controls.Add(this.rbOneMonth);
            this.gbCriteria.Location = new System.Drawing.Point(7, 262);
            this.gbCriteria.Name = "gbCriteria";
            this.gbCriteria.Size = new System.Drawing.Size(1439, 90);
            this.gbCriteria.TabIndex = 3;
            this.gbCriteria.TabStop = false;
            this.gbCriteria.Text = "Set Criteria (Objects modified since)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(49, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 55;
            this.label5.Text = "...and with status";
            // 
            // lStatus
            // 
            this.lStatus.AutoSize = true;
            this.lStatus.Location = new System.Drawing.Point(829, 21);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(0, 13);
            this.lStatus.TabIndex = 6;
            // 
            // dtpDate
            // 
            this.dtpDate.Enabled = false;
            this.dtpDate.Location = new System.Drawing.Point(649, 19);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(159, 20);
            this.dtpDate.TabIndex = 5;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // rbSpecificDate
            // 
            this.rbSpecificDate.AutoSize = true;
            this.rbSpecificDate.Location = new System.Drawing.Point(530, 19);
            this.rbSpecificDate.Name = "rbSpecificDate";
            this.rbSpecificDate.Size = new System.Drawing.Size(89, 17);
            this.rbSpecificDate.TabIndex = 4;
            this.rbSpecificDate.Text = "Specific Date";
            this.rbSpecificDate.UseVisualStyleBackColor = true;
            this.rbSpecificDate.CheckedChanged += new System.EventHandler(this.rbSpecificDate_CheckedChanged);
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Checked = true;
            this.rbAll.Location = new System.Drawing.Point(44, 19);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(36, 17);
            this.rbAll.TabIndex = 3;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "All";
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // rbSixMonths
            // 
            this.rbSixMonths.AutoSize = true;
            this.rbSixMonths.Location = new System.Drawing.Point(399, 19);
            this.rbSixMonths.Name = "rbSixMonths";
            this.rbSixMonths.Size = new System.Drawing.Size(69, 17);
            this.rbSixMonths.TabIndex = 2;
            this.rbSixMonths.Text = "6 Months";
            this.rbSixMonths.UseVisualStyleBackColor = true;
            this.rbSixMonths.CheckedChanged += new System.EventHandler(this.rbSixMonths_CheckedChanged);
            // 
            // rbThreeMonths
            // 
            this.rbThreeMonths.AutoSize = true;
            this.rbThreeMonths.Location = new System.Drawing.Point(268, 19);
            this.rbThreeMonths.Name = "rbThreeMonths";
            this.rbThreeMonths.Size = new System.Drawing.Size(69, 17);
            this.rbThreeMonths.TabIndex = 1;
            this.rbThreeMonths.Text = "3 Months";
            this.rbThreeMonths.UseVisualStyleBackColor = true;
            this.rbThreeMonths.CheckedChanged += new System.EventHandler(this.rbThreeMonths_CheckedChanged);
            // 
            // rbOneMonth
            // 
            this.rbOneMonth.AutoSize = true;
            this.rbOneMonth.Location = new System.Drawing.Point(142, 19);
            this.rbOneMonth.Name = "rbOneMonth";
            this.rbOneMonth.Size = new System.Drawing.Size(64, 17);
            this.rbOneMonth.TabIndex = 0;
            this.rbOneMonth.Text = "1 Month";
            this.rbOneMonth.UseVisualStyleBackColor = true;
            this.rbOneMonth.CheckedChanged += new System.EventHandler(this.rbOneMonth_CheckedChanged);
            // 
            // tpBackup
            // 
            this.tpBackup.Controls.Add(this.groupBox4);
            this.tpBackup.Location = new System.Drawing.Point(4, 22);
            this.tpBackup.Name = "tpBackup";
            this.tpBackup.Padding = new System.Windows.Forms.Padding(3);
            this.tpBackup.Size = new System.Drawing.Size(1464, 710);
            this.tpBackup.TabIndex = 0;
            this.tpBackup.Text = "Backup";
            this.tpBackup.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.gbGrid);
            this.groupBox4.Controls.Add(this.gbCriteria);
            this.groupBox4.Controls.Add(this.toolStrip1);
            this.groupBox4.Controls.Add(this.gbObjectTypes);
            this.groupBox4.Controls.Add(this.gbSetDetails);
            this.groupBox4.Controls.Add(this.gbDatabases);
            this.groupBox4.Location = new System.Drawing.Point(6, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1452, 698);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            // 
            // gbObjectTypes
            // 
            this.gbObjectTypes.Controls.Add(this.cObjTriggers);
            this.gbObjectTypes.Controls.Add(this.cObjViews);
            this.gbObjectTypes.Controls.Add(this.cObjTables);
            this.gbObjectTypes.Controls.Add(this.cObjFunctions);
            this.gbObjectTypes.Controls.Add(this.cObjStoredProcedures);
            this.gbObjectTypes.Location = new System.Drawing.Point(1273, 73);
            this.gbObjectTypes.Name = "gbObjectTypes";
            this.gbObjectTypes.Size = new System.Drawing.Size(173, 182);
            this.gbObjectTypes.TabIndex = 2;
            this.gbObjectTypes.TabStop = false;
            this.gbObjectTypes.Text = "Object Types";
            // 
            // cObjTriggers
            // 
            this.cObjTriggers.AutoSize = true;
            this.cObjTriggers.Location = new System.Drawing.Point(29, 113);
            this.cObjTriggers.Name = "cObjTriggers";
            this.cObjTriggers.Size = new System.Drawing.Size(64, 17);
            this.cObjTriggers.TabIndex = 9;
            this.cObjTriggers.Text = "Triggers";
            this.cObjTriggers.UseVisualStyleBackColor = true;
            this.cObjTriggers.CheckedChanged += new System.EventHandler(this.cObjTriggers_CheckedChanged);
            // 
            // cObjViews
            // 
            this.cObjViews.AutoSize = true;
            this.cObjViews.Location = new System.Drawing.Point(29, 90);
            this.cObjViews.Name = "cObjViews";
            this.cObjViews.Size = new System.Drawing.Size(54, 17);
            this.cObjViews.TabIndex = 10;
            this.cObjViews.Text = "Views";
            this.cObjViews.UseVisualStyleBackColor = true;
            this.cObjViews.CheckedChanged += new System.EventHandler(this.cObjViews_CheckedChanged);
            // 
            // cObjTables
            // 
            this.cObjTables.AutoSize = true;
            this.cObjTables.Location = new System.Drawing.Point(29, 67);
            this.cObjTables.Name = "cObjTables";
            this.cObjTables.Size = new System.Drawing.Size(134, 17);
            this.cObjTables.TabIndex = 8;
            this.cObjTables.Text = "Tables (Structure Only)";
            this.cObjTables.UseVisualStyleBackColor = true;
            this.cObjTables.CheckedChanged += new System.EventHandler(this.cObjTables_CheckedChanged);
            // 
            // cObjFunctions
            // 
            this.cObjFunctions.AutoSize = true;
            this.cObjFunctions.Location = new System.Drawing.Point(29, 44);
            this.cObjFunctions.Name = "cObjFunctions";
            this.cObjFunctions.Size = new System.Drawing.Size(72, 17);
            this.cObjFunctions.TabIndex = 7;
            this.cObjFunctions.Text = "Functions";
            this.cObjFunctions.UseVisualStyleBackColor = true;
            this.cObjFunctions.CheckedChanged += new System.EventHandler(this.cObjFunctions_CheckedChanged);
            // 
            // cObjStoredProcedures
            // 
            this.cObjStoredProcedures.AutoSize = true;
            this.cObjStoredProcedures.Checked = true;
            this.cObjStoredProcedures.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cObjStoredProcedures.Location = new System.Drawing.Point(29, 21);
            this.cObjStoredProcedures.Name = "cObjStoredProcedures";
            this.cObjStoredProcedures.Size = new System.Drawing.Size(114, 17);
            this.cObjStoredProcedures.TabIndex = 6;
            this.cObjStoredProcedures.Text = "Stored Procedures";
            this.cObjStoredProcedures.UseVisualStyleBackColor = true;
            this.cObjStoredProcedures.CheckedChanged += new System.EventHandler(this.cObjStoredProcedures_CheckedChanged);
            // 
            // gbSetDetails
            // 
            this.gbSetDetails.Controls.Add(this.label3);
            this.gbSetDetails.Controls.Add(this.tCreateDate);
            this.gbSetDetails.Controls.Add(this.rbActiveNo);
            this.gbSetDetails.Controls.Add(this.rbActiveYes);
            this.gbSetDetails.Controls.Add(this.label6);
            this.gbSetDetails.Controls.Add(this.tComment);
            this.gbSetDetails.Controls.Add(this.label4);
            this.gbSetDetails.Controls.Add(this.label1);
            this.gbSetDetails.Controls.Add(this.cbBackupSetName);
            this.gbSetDetails.Controls.Add(this.label2);
            this.gbSetDetails.Controls.Add(this.cbServers);
            this.gbSetDetails.Location = new System.Drawing.Point(6, 73);
            this.gbSetDetails.Name = "gbSetDetails";
            this.gbSetDetails.Size = new System.Drawing.Size(462, 182);
            this.gbSetDetails.TabIndex = 0;
            this.gbSetDetails.TabStop = false;
            this.gbSetDetails.Text = "Set Details";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(105, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 59;
            this.label6.Text = "Set Active";
            // 
            // tComment
            // 
            this.tComment.Location = new System.Drawing.Point(167, 73);
            this.tComment.Multiline = true;
            this.tComment.Name = "tComment";
            this.tComment.Size = new System.Drawing.Size(240, 44);
            this.tComment.TabIndex = 57;
            this.tComment.TextChanged += new System.EventHandler(this.tComment_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 13);
            this.label4.TabIndex = 58;
            this.label4.Text = "Backup Set Description";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "Backup Set Name";
            // 
            // cbBackupSetName
            // 
            this.cbBackupSetName.FormattingEnabled = true;
            this.cbBackupSetName.Location = new System.Drawing.Point(167, 19);
            this.cbBackupSetName.Name = "cbBackupSetName";
            this.cbBackupSetName.Size = new System.Drawing.Size(240, 21);
            this.cbBackupSetName.TabIndex = 55;
            this.cbBackupSetName.SelectedIndexChanged += new System.EventHandler(this.cbBackupSetName_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(123, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 54;
            this.label2.Text = "Server";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // cbServers
            // 
            this.cbServers.FormattingEnabled = true;
            this.cbServers.Location = new System.Drawing.Point(167, 46);
            this.cbServers.Name = "cbServers";
            this.cbServers.Size = new System.Drawing.Size(240, 21);
            this.cbServers.TabIndex = 53;
            this.cbServers.SelectedIndexChanged += new System.EventHandler(this.cbServers_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpBackup);
            this.tabControl1.Controls.Add(this.tpRestore);
            this.tabControl1.Location = new System.Drawing.Point(2, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1472, 736);
            this.tabControl1.TabIndex = 2;
            // 
            // BackupRestore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1480, 743);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BackupRestore";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Backup and Restore";
            this.Load += new System.EventHandler(this.BackupRestore_Load);
            this.gbRestoreObjectTypes.ResumeLayout(false);
            this.gbRestoreObjectTypes.PerformLayout();
            this.gbRestoreVarious.ResumeLayout(false);
            this.gbRestoreVarious.PerformLayout();
            this.tpRestore.ResumeLayout(false);
            this.tpRestore.PerformLayout();
            this.gbRestoreFilter.ResumeLayout(false);
            this.gbRestoreSet.ResumeLayout(false);
            this.gbRestoreSet.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gbCriteria.ResumeLayout(false);
            this.gbCriteria.PerformLayout();
            this.tpBackup.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.gbObjectTypes.ResumeLayout(false);
            this.gbObjectTypes.PerformLayout();
            this.gbSetDetails.ResumeLayout(false);
            this.gbSetDetails.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tCreateDate;
        private System.Windows.Forms.CheckBox cRestoreObjTriggers;
        private System.Windows.Forms.GroupBox gbRestoreDatabase;
        private System.Windows.Forms.GroupBox gbRestoreObjectTypes;
        private System.Windows.Forms.CheckBox cRestoreObjViews;
        private System.Windows.Forms.CheckBox cRestoreObjTables;
        private System.Windows.Forms.CheckBox cRestoreObjFunctions;
        private System.Windows.Forms.CheckBox cRestoreObjStoredProcedures;
        private System.Windows.Forms.GroupBox gbRestoreVarious;
        private System.Windows.Forms.Button cmdRestoreNameSearch;
        private System.Windows.Forms.ComboBox cbRestoreUserName;
        private System.Windows.Forms.TextBox tRestoreObjectName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbObjectStatusRestore;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox gbRestoreGrid;
        private System.Windows.Forms.TabPage tpRestore;
        private System.Windows.Forms.GroupBox gbRestoreFilter;
        private System.Windows.Forms.GroupBox gbRestoreSet;
        private System.Windows.Forms.Label lRestoreSetCount;
        private System.Windows.Forms.Label lRestoreStatus;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tBackupServerName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tRestoreDateCreated;
        private System.Windows.Forms.TextBox tRestoreComment;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbRestoreSetName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbRestoreToServer;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsbRestore;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar2;
        private System.Windows.Forms.GroupBox gbDatabases;
        private System.Windows.Forms.RadioButton rbActiveNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbActiveYes;
        private System.Windows.Forms.ToolStripButton tsbSaveSet;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbNewSet;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbSelectAll;
        private System.Windows.Forms.ToolStripButton tsbDeSelectAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbClear;
        private System.Windows.Forms.ToolStripButton tsbFetchObjects;
        private System.Windows.Forms.ToolStripButton tsbBackUp;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ComboBox cbObjectStatus;
        private System.Windows.Forms.GroupBox gbGrid;
        private System.Windows.Forms.GroupBox gbCriteria;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lStatus;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.RadioButton rbSpecificDate;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.RadioButton rbSixMonths;
        private System.Windows.Forms.RadioButton rbThreeMonths;
        private System.Windows.Forms.RadioButton rbOneMonth;
        private System.Windows.Forms.TabPage tpBackup;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox gbObjectTypes;
        private System.Windows.Forms.CheckBox cObjTriggers;
        private System.Windows.Forms.CheckBox cObjViews;
        private System.Windows.Forms.CheckBox cObjTables;
        private System.Windows.Forms.CheckBox cObjFunctions;
        private System.Windows.Forms.CheckBox cObjStoredProcedures;
        private System.Windows.Forms.GroupBox gbSetDetails;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tComment;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbBackupSetName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbServers;
        private System.Windows.Forms.TabControl tabControl1;
    }
}