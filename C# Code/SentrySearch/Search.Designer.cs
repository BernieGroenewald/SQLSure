namespace SentrySearch
{
    partial class Search
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Search));
            this.Database = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ObjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvObjects = new System.Windows.Forms.DataGridView();
            this.Schema = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MatchesOn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ObjectDetail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ObjectId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.pDetail = new System.Windows.Forms.Panel();
            this.dgvDetail = new System.Windows.Forms.DataGridView();
            this.tDetail = new System.Windows.Forms.RichTextBox();
            this.pStatusBar = new System.Windows.Forms.Panel();
            this.lStatusRight = new System.Windows.Forms.Label();
            this.lStatusLeft = new System.Windows.Forms.Label();
            this.pSearch = new System.Windows.Forms.Panel();
            this.tSearchString = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.cmdSearch = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbEnvironment = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cExactMatch = new System.Windows.Forms.CheckBox();
            this.objectSelector1 = new SentryControls.ObjectSelector();
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjects)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.pDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.pStatusBar.SuspendLayout();
            this.pSearch.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Database
            // 
            this.Database.HeaderText = "Database";
            this.Database.Name = "Database";
            this.Database.ReadOnly = true;
            this.Database.Width = 78;
            // 
            // ObjectName
            // 
            this.ObjectName.HeaderText = "Object Name";
            this.ObjectName.Name = "ObjectName";
            this.ObjectName.ReadOnly = true;
            this.ObjectName.Width = 94;
            // 
            // dgvObjects
            // 
            this.dgvObjects.AllowUserToAddRows = false;
            this.dgvObjects.AllowUserToDeleteRows = false;
            this.dgvObjects.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvObjects.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvObjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvObjects.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ObjectName,
            this.Schema,
            this.Database,
            this.Type,
            this.MatchesOn,
            this.ObjectDetail,
            this.ObjectId});
            this.dgvObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvObjects.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgvObjects.Location = new System.Drawing.Point(0, 0);
            this.dgvObjects.MultiSelect = false;
            this.dgvObjects.Name = "dgvObjects";
            this.dgvObjects.ReadOnly = true;
            this.dgvObjects.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvObjects.RowHeadersVisible = false;
            this.dgvObjects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvObjects.Size = new System.Drawing.Size(1104, 379);
            this.dgvObjects.TabIndex = 15;
            this.dgvObjects.SelectionChanged += new System.EventHandler(this.dgvObjects_SelectionChanged);
            // 
            // Schema
            // 
            this.Schema.HeaderText = "Schema";
            this.Schema.Name = "Schema";
            this.Schema.ReadOnly = true;
            this.Schema.Width = 71;
            // 
            // Type
            // 
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Width = 56;
            // 
            // MatchesOn
            // 
            this.MatchesOn.HeaderText = "Matches On";
            this.MatchesOn.Name = "MatchesOn";
            this.MatchesOn.ReadOnly = true;
            this.MatchesOn.Width = 90;
            // 
            // ObjectDetail
            // 
            this.ObjectDetail.HeaderText = "Object Detail";
            this.ObjectDetail.Name = "ObjectDetail";
            this.ObjectDetail.ReadOnly = true;
            this.ObjectDetail.Width = 93;
            // 
            // ObjectId
            // 
            this.ObjectId.HeaderText = "ObjectId";
            this.ObjectId.Name = "ObjectId";
            this.ObjectId.ReadOnly = true;
            this.ObjectId.Visible = false;
            this.ObjectId.Width = 72;
            // 
            // scMain
            // 
            this.scMain.Location = new System.Drawing.Point(1, 38);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.scMain.Panel1.Controls.Add(this.dgvObjects);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.scMain.Panel2.Controls.Add(this.pDetail);
            this.scMain.Panel2.Controls.Add(this.pStatusBar);
            this.scMain.Size = new System.Drawing.Size(1104, 759);
            this.scMain.SplitterDistance = 379;
            this.scMain.TabIndex = 27;
            // 
            // pDetail
            // 
            this.pDetail.Controls.Add(this.dgvDetail);
            this.pDetail.Controls.Add(this.tDetail);
            this.pDetail.Dock = System.Windows.Forms.DockStyle.Left;
            this.pDetail.Location = new System.Drawing.Point(0, 30);
            this.pDetail.Name = "pDetail";
            this.pDetail.Size = new System.Drawing.Size(1023, 346);
            this.pDetail.TabIndex = 18;
            // 
            // dgvDetail
            // 
            this.dgvDetail.AllowUserToAddRows = false;
            this.dgvDetail.AllowUserToDeleteRows = false;
            this.dgvDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDetail.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetail.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgvDetail.Location = new System.Drawing.Point(0, 0);
            this.dgvDetail.MultiSelect = false;
            this.dgvDetail.Name = "dgvDetail";
            this.dgvDetail.ReadOnly = true;
            this.dgvDetail.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvDetail.RowHeadersVisible = false;
            this.dgvDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetail.Size = new System.Drawing.Size(1023, 346);
            this.dgvDetail.TabIndex = 16;
            // 
            // tDetail
            // 
            this.tDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tDetail.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.tDetail.Location = new System.Drawing.Point(0, 0);
            this.tDetail.Name = "tDetail";
            this.tDetail.Size = new System.Drawing.Size(1023, 346);
            this.tDetail.TabIndex = 0;
            this.tDetail.Text = "";
            // 
            // pStatusBar
            // 
            this.pStatusBar.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pStatusBar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pStatusBar.Controls.Add(this.lStatusRight);
            this.pStatusBar.Controls.Add(this.lStatusLeft);
            this.pStatusBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pStatusBar.Location = new System.Drawing.Point(0, 0);
            this.pStatusBar.Name = "pStatusBar";
            this.pStatusBar.Size = new System.Drawing.Size(1104, 30);
            this.pStatusBar.TabIndex = 17;
            // 
            // lStatusRight
            // 
            this.lStatusRight.AutoSize = true;
            this.lStatusRight.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lStatusRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.lStatusRight.Location = new System.Drawing.Point(1090, 0);
            this.lStatusRight.Name = "lStatusRight";
            this.lStatusRight.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.lStatusRight.Size = new System.Drawing.Size(10, 20);
            this.lStatusRight.TabIndex = 1;
            this.lStatusRight.Text = ".";
            // 
            // lStatusLeft
            // 
            this.lStatusLeft.AutoSize = true;
            this.lStatusLeft.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lStatusLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lStatusLeft.Location = new System.Drawing.Point(0, 0);
            this.lStatusLeft.Name = "lStatusLeft";
            this.lStatusLeft.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.lStatusLeft.Size = new System.Drawing.Size(10, 20);
            this.lStatusLeft.TabIndex = 0;
            this.lStatusLeft.Text = ".";
            // 
            // pSearch
            // 
            this.pSearch.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pSearch.Controls.Add(this.tSearchString);
            this.pSearch.Controls.Add(this.label1);
            this.pSearch.Location = new System.Drawing.Point(1, 2);
            this.pSearch.Name = "pSearch";
            this.pSearch.Size = new System.Drawing.Size(414, 30);
            this.pSearch.TabIndex = 0;
            // 
            // tSearchString
            // 
            this.tSearchString.Location = new System.Drawing.Point(94, 3);
            this.tSearchString.Name = "tSearchString";
            this.tSearchString.Size = new System.Drawing.Size(315, 20);
            this.tSearchString.TabIndex = 0;
            this.tSearchString.TextChanged += new System.EventHandler(this.tSearchString_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search string:   ";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "find.png");
            this.imageList1.Images.SetKeyName(1, "sign_stop.png");
            // 
            // cmdSearch
            // 
            this.cmdSearch.ImageIndex = 0;
            this.cmdSearch.ImageList = this.imageList1;
            this.cmdSearch.Location = new System.Drawing.Point(420, 2);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(77, 30);
            this.cmdSearch.TabIndex = 0;
            this.cmdSearch.Text = "Go";
            this.cmdSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cbEnvironment);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(594, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(303, 30);
            this.panel1.TabIndex = 28;
            // 
            // cbEnvironment
            // 
            this.cbEnvironment.FormattingEnabled = true;
            this.cbEnvironment.Location = new System.Drawing.Point(85, 3);
            this.cbEnvironment.Name = "cbEnvironment";
            this.cbEnvironment.Size = new System.Drawing.Size(208, 21);
            this.cbEnvironment.TabIndex = 0;
            this.cbEnvironment.SelectedIndexChanged += new System.EventHandler(this.cbEnvironment_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Environment: ";
            // 
            // cExactMatch
            // 
            this.cExactMatch.AutoSize = true;
            this.cExactMatch.Location = new System.Drawing.Point(503, 10);
            this.cExactMatch.Name = "cExactMatch";
            this.cExactMatch.Size = new System.Drawing.Size(85, 17);
            this.cExactMatch.TabIndex = 1;
            this.cExactMatch.Text = "Exact match";
            this.cExactMatch.UseVisualStyleBackColor = true;
            // 
            // objectSelector1
            // 
            this.objectSelector1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.objectSelector1.Location = new System.Drawing.Point(1164, 2);
            this.objectSelector1.Name = "objectSelector1";
            this.objectSelector1.Size = new System.Drawing.Size(259, 30);
            this.objectSelector1.TabIndex = 29;
            // 
            // Search
            // 
            this.AcceptButton = this.cmdSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1435, 829);
            this.Controls.Add(this.objectSelector1);
            this.Controls.Add(this.scMain);
            this.Controls.Add(this.pSearch);
            this.Controls.Add(this.cmdSearch);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cExactMatch);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Search";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search";
            this.Load += new System.EventHandler(this.Search_Load);
            this.ResizeEnd += new System.EventHandler(this.Search_ResizeEnd);
            this.Resize += new System.EventHandler(this.Search_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjects)).EndInit();
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.pDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.pStatusBar.ResumeLayout(false);
            this.pStatusBar.PerformLayout();
            this.pSearch.ResumeLayout(false);
            this.pSearch.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn Database;
        private System.Windows.Forms.DataGridViewTextBoxColumn ObjectName;
        private System.Windows.Forms.DataGridView dgvObjects;
        private System.Windows.Forms.DataGridViewTextBoxColumn Schema;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn MatchesOn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ObjectDetail;
        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.Panel pDetail;
        private System.Windows.Forms.DataGridView dgvDetail;
        private System.Windows.Forms.RichTextBox tDetail;
        private System.Windows.Forms.Panel pStatusBar;
        private System.Windows.Forms.Label lStatusRight;
        private System.Windows.Forms.Label lStatusLeft;
        private System.Windows.Forms.Panel pSearch;
        private System.Windows.Forms.TextBox tSearchString;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbEnvironment;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cExactMatch;
        private SentryControls.ObjectSelector objectSelector1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ObjectId;
    }
}