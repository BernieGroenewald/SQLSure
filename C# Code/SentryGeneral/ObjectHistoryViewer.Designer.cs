﻿namespace SentryGeneral
{
    partial class ObjectHistoryViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectHistoryViewer));
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.lObjectDescription = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tObjectText = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsbClose
            // 
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(40, 51);
            this.tsbClose.Text = "Close";
            this.tsbClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // lObjectDescription
            // 
            this.lObjectDescription.AutoSize = true;
            this.lObjectDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lObjectDescription.Location = new System.Drawing.Point(12, 72);
            this.lObjectDescription.Name = "lObjectDescription";
            this.lObjectDescription.Size = new System.Drawing.Size(91, 17);
            this.lObjectDescription.TabIndex = 10;
            this.lObjectDescription.Text = "ObjectDesc";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1038, 54);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tObjectText
            // 
            this.tObjectText.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tObjectText.Location = new System.Drawing.Point(12, 113);
            this.tObjectText.Multiline = true;
            this.tObjectText.Name = "tObjectText";
            this.tObjectText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tObjectText.Size = new System.Drawing.Size(1014, 512);
            this.tObjectText.TabIndex = 11;
            this.tObjectText.TabStop = false;
            // 
            // ObjectHistoryViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 638);
            this.Controls.Add(this.lObjectDescription);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tObjectText);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ObjectHistoryViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "History Viewer";
            this.Load += new System.EventHandler(this.ObjectHistoryViewer_Load);
            this.ResizeEnd += new System.EventHandler(this.ObjectHistoryViewer_ResizeEnd);
            this.Resize += new System.EventHandler(this.ObjectHistoryViewer_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.Label lObjectDescription;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TextBox tObjectText;
    }
}