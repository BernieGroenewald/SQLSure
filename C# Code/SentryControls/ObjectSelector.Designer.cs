namespace SentryControls
{
    partial class ObjectSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectSelector));
            this.cObjectTriggers = new System.Windows.Forms.CheckBox();
            this.cObjectConstraints = new System.Windows.Forms.CheckBox();
            this.cObjectFunctions = new System.Windows.Forms.CheckBox();
            this.cObjectViews = new System.Windows.Forms.CheckBox();
            this.cObjectTables = new System.Windows.Forms.CheckBox();
            this.cObjectStoredProcedures = new System.Windows.Forms.CheckBox();
            this.cAllObjects = new System.Windows.Forms.CheckBox();
            this.lObjects = new System.Windows.Forms.Label();
            this.cmdOkObjects = new System.Windows.Forms.Button();
            this.cmdDropDownObjects = new System.Windows.Forms.Button();
            this.tSelected = new System.Windows.Forms.TextBox();
            this.pLine = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pLine)).BeginInit();
            this.SuspendLayout();
            // 
            // cObjectTriggers
            // 
            this.cObjectTriggers.AutoSize = true;
            this.cObjectTriggers.Location = new System.Drawing.Point(34, 230);
            this.cObjectTriggers.Name = "cObjectTriggers";
            this.cObjectTriggers.Size = new System.Drawing.Size(64, 17);
            this.cObjectTriggers.TabIndex = 67;
            this.cObjectTriggers.Text = "Triggers";
            this.cObjectTriggers.UseVisualStyleBackColor = true;
            this.cObjectTriggers.CheckedChanged += new System.EventHandler(this.cObjectTriggers_CheckedChanged);
            // 
            // cObjectConstraints
            // 
            this.cObjectConstraints.AutoSize = true;
            this.cObjectConstraints.Location = new System.Drawing.Point(34, 206);
            this.cObjectConstraints.Name = "cObjectConstraints";
            this.cObjectConstraints.Size = new System.Drawing.Size(78, 17);
            this.cObjectConstraints.TabIndex = 66;
            this.cObjectConstraints.Text = "Constraints";
            this.cObjectConstraints.UseVisualStyleBackColor = true;
            this.cObjectConstraints.CheckedChanged += new System.EventHandler(this.cObjectConstraints_CheckedChanged);
            // 
            // cObjectFunctions
            // 
            this.cObjectFunctions.AutoSize = true;
            this.cObjectFunctions.Location = new System.Drawing.Point(34, 182);
            this.cObjectFunctions.Name = "cObjectFunctions";
            this.cObjectFunctions.Size = new System.Drawing.Size(72, 17);
            this.cObjectFunctions.TabIndex = 65;
            this.cObjectFunctions.Text = "Functions";
            this.cObjectFunctions.UseVisualStyleBackColor = true;
            this.cObjectFunctions.CheckedChanged += new System.EventHandler(this.cObjectFunctions_CheckedChanged);
            // 
            // cObjectViews
            // 
            this.cObjectViews.AutoSize = true;
            this.cObjectViews.Location = new System.Drawing.Point(34, 135);
            this.cObjectViews.Name = "cObjectViews";
            this.cObjectViews.Size = new System.Drawing.Size(54, 17);
            this.cObjectViews.TabIndex = 64;
            this.cObjectViews.Text = "Views";
            this.cObjectViews.UseVisualStyleBackColor = true;
            this.cObjectViews.CheckedChanged += new System.EventHandler(this.cObjectViews_CheckedChanged);
            // 
            // cObjectTables
            // 
            this.cObjectTables.AutoSize = true;
            this.cObjectTables.Location = new System.Drawing.Point(34, 111);
            this.cObjectTables.Name = "cObjectTables";
            this.cObjectTables.Size = new System.Drawing.Size(58, 17);
            this.cObjectTables.TabIndex = 63;
            this.cObjectTables.Text = "Tables";
            this.cObjectTables.UseVisualStyleBackColor = true;
            this.cObjectTables.CheckedChanged += new System.EventHandler(this.cObjectTables_CheckedChanged);
            // 
            // cObjectStoredProcedures
            // 
            this.cObjectStoredProcedures.AutoSize = true;
            this.cObjectStoredProcedures.Location = new System.Drawing.Point(34, 158);
            this.cObjectStoredProcedures.Name = "cObjectStoredProcedures";
            this.cObjectStoredProcedures.Size = new System.Drawing.Size(114, 17);
            this.cObjectStoredProcedures.TabIndex = 62;
            this.cObjectStoredProcedures.Text = "Stored Procedures";
            this.cObjectStoredProcedures.UseVisualStyleBackColor = true;
            this.cObjectStoredProcedures.CheckedChanged += new System.EventHandler(this.cObjectStoredProcedures_CheckedChanged);
            // 
            // cAllObjects
            // 
            this.cAllObjects.AutoSize = true;
            this.cAllObjects.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cAllObjects.Location = new System.Drawing.Point(17, 88);
            this.cAllObjects.Name = "cAllObjects";
            this.cAllObjects.Size = new System.Drawing.Size(87, 17);
            this.cAllObjects.TabIndex = 61;
            this.cAllObjects.Text = "All Objects";
            this.cAllObjects.UseVisualStyleBackColor = true;
            this.cAllObjects.CheckedChanged += new System.EventHandler(this.cAllObjects_CheckedChanged);
            // 
            // lObjects
            // 
            this.lObjects.AutoSize = true;
            this.lObjects.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lObjects.Location = new System.Drawing.Point(14, 71);
            this.lObjects.Name = "lObjects";
            this.lObjects.Size = new System.Drawing.Size(92, 13);
            this.lObjects.TabIndex = 60;
            this.lObjects.Text = "Database Objects";
            // 
            // cmdOkObjects
            // 
            this.cmdOkObjects.Location = new System.Drawing.Point(180, 31);
            this.cmdOkObjects.Name = "cmdOkObjects";
            this.cmdOkObjects.Size = new System.Drawing.Size(75, 23);
            this.cmdOkObjects.TabIndex = 59;
            this.cmdOkObjects.Text = "Done";
            this.cmdOkObjects.UseVisualStyleBackColor = true;
            this.cmdOkObjects.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdDropDownObjects
            // 
            this.cmdDropDownObjects.Image = ((System.Drawing.Image)(resources.GetObject("cmdDropDownObjects.Image")));
            this.cmdDropDownObjects.Location = new System.Drawing.Point(225, 2);
            this.cmdDropDownObjects.Name = "cmdDropDownObjects";
            this.cmdDropDownObjects.Size = new System.Drawing.Size(30, 23);
            this.cmdDropDownObjects.TabIndex = 58;
            this.cmdDropDownObjects.UseVisualStyleBackColor = true;
            this.cmdDropDownObjects.Click += new System.EventHandler(this.cmdDropDownObjects_Click);
            // 
            // tSelected
            // 
            this.tSelected.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tSelected.Location = new System.Drawing.Point(3, 3);
            this.tSelected.Name = "tSelected";
            this.tSelected.ReadOnly = true;
            this.tSelected.Size = new System.Drawing.Size(225, 20);
            this.tSelected.TabIndex = 57;
            // 
            // pLine
            // 
            this.pLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pLine.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pLine.ErrorImage")));
            this.pLine.Image = ((System.Drawing.Image)(resources.GetObject("pLine.Image")));
            this.pLine.Location = new System.Drawing.Point(3, 57);
            this.pLine.Margin = new System.Windows.Forms.Padding(0);
            this.pLine.Name = "pLine";
            this.pLine.Size = new System.Drawing.Size(251, 14);
            this.pLine.TabIndex = 68;
            this.pLine.TabStop = false;
            // 
            // ObjectSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pLine);
            this.Controls.Add(this.cObjectTriggers);
            this.Controls.Add(this.cObjectConstraints);
            this.Controls.Add(this.cObjectFunctions);
            this.Controls.Add(this.cObjectViews);
            this.Controls.Add(this.cObjectTables);
            this.Controls.Add(this.cObjectStoredProcedures);
            this.Controls.Add(this.cAllObjects);
            this.Controls.Add(this.lObjects);
            this.Controls.Add(this.cmdOkObjects);
            this.Controls.Add(this.cmdDropDownObjects);
            this.Controls.Add(this.tSelected);
            this.Name = "ObjectSelector";
            this.Size = new System.Drawing.Size(259, 260);
            this.Load += new System.EventHandler(this.ObjectSelector_Load);
            this.Leave += new System.EventHandler(this.ObjectSelector_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.pLine)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cObjectTriggers;
        private System.Windows.Forms.CheckBox cObjectConstraints;
        private System.Windows.Forms.CheckBox cObjectFunctions;
        private System.Windows.Forms.CheckBox cObjectViews;
        private System.Windows.Forms.CheckBox cObjectTables;
        private System.Windows.Forms.CheckBox cObjectStoredProcedures;
        private System.Windows.Forms.CheckBox cAllObjects;
        private System.Windows.Forms.Label lObjects;
        private System.Windows.Forms.Button cmdOkObjects;
        private System.Windows.Forms.Button cmdDropDownObjects;
        private System.Windows.Forms.TextBox tSelected;
        private System.Windows.Forms.PictureBox pLine;
    }
}
