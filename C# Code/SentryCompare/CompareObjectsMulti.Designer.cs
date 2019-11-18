namespace SentryCompare
{
    partial class CompareObjectsMulti
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompareObjectsMulti));
            this.gbServerControls = new System.Windows.Forms.GroupBox();
            this.gbEnvironments = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // gbServerControls
            // 
            this.gbServerControls.Location = new System.Drawing.Point(12, 119);
            this.gbServerControls.Name = "gbServerControls";
            this.gbServerControls.Size = new System.Drawing.Size(541, 464);
            this.gbServerControls.TabIndex = 3;
            this.gbServerControls.TabStop = false;
            // 
            // gbEnvironments
            // 
            this.gbEnvironments.Location = new System.Drawing.Point(12, 12);
            this.gbEnvironments.Name = "gbEnvironments";
            this.gbEnvironments.Size = new System.Drawing.Size(654, 101);
            this.gbEnvironments.TabIndex = 2;
            this.gbEnvironments.TabStop = false;
            this.gbEnvironments.Text = "Environments";
            // 
            // CompareObjectsMulti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1334, 703);
            this.Controls.Add(this.gbServerControls);
            this.Controls.Add(this.gbEnvironments);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CompareObjectsMulti";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compare Objects";
            this.Load += new System.EventHandler(this.CompareObjectsMulti_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbServerControls;
        private System.Windows.Forms.GroupBox gbEnvironments;
    }
}