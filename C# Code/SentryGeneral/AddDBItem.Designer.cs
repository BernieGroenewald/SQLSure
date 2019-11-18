namespace SentryGeneral
{
    partial class AddDBItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddDBItem));
            this.cmdAddItem = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cbObjects = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbDatabaseName = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmdAddItem
            // 
            this.cmdAddItem.AutoSize = true;
            this.cmdAddItem.Location = new System.Drawing.Point(228, 79);
            this.cmdAddItem.Name = "cmdAddItem";
            this.cmdAddItem.Size = new System.Drawing.Size(75, 23);
            this.cmdAddItem.TabIndex = 42;
            this.cmdAddItem.Text = "Add Item";
            this.cmdAddItem.UseVisualStyleBackColor = true;
            this.cmdAddItem.Click += new System.EventHandler(this.cmdAddItem_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "Object";
            // 
            // cbObjects
            // 
            this.cbObjects.FormattingEnabled = true;
            this.cbObjects.Location = new System.Drawing.Point(77, 52);
            this.cbObjects.Name = "cbObjects";
            this.cbObjects.Size = new System.Drawing.Size(226, 21);
            this.cbObjects.TabIndex = 40;
            this.cbObjects.SelectedIndexChanged += new System.EventHandler(this.cbObjects_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 39;
            this.label6.Text = "Database";
            // 
            // cbDatabaseName
            // 
            this.cbDatabaseName.FormattingEnabled = true;
            this.cbDatabaseName.Location = new System.Drawing.Point(77, 25);
            this.cbDatabaseName.Name = "cbDatabaseName";
            this.cbDatabaseName.Size = new System.Drawing.Size(226, 21);
            this.cbDatabaseName.TabIndex = 38;
            this.cbDatabaseName.SelectedIndexChanged += new System.EventHandler(this.cbDatabaseName_SelectedIndexChanged);
            // 
            // AddDBItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 125);
            this.Controls.Add(this.cmdAddItem);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbObjects);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbDatabaseName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddDBItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Database Item";
            this.Load += new System.EventHandler(this.AddDBItem_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdAddItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbObjects;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbDatabaseName;
    }
}