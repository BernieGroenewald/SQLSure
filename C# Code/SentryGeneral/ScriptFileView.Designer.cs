namespace SentryGeneral
{
    partial class ScriptFileView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptFileView));
            this.cmdDone = new System.Windows.Forms.Button();
            this.tFile = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cmdDone
            // 
            this.cmdDone.Location = new System.Drawing.Point(892, 734);
            this.cmdDone.Name = "cmdDone";
            this.cmdDone.Size = new System.Drawing.Size(75, 23);
            this.cmdDone.TabIndex = 5;
            this.cmdDone.Text = "Done";
            this.cmdDone.UseVisualStyleBackColor = true;
            this.cmdDone.Click += new System.EventHandler(this.cmdDone_Click);
            // 
            // tFile
            // 
            this.tFile.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tFile.Location = new System.Drawing.Point(12, 12);
            this.tFile.Multiline = true;
            this.tFile.Name = "tFile";
            this.tFile.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tFile.Size = new System.Drawing.Size(955, 716);
            this.tFile.TabIndex = 4;
            this.tFile.TabStop = false;
            // 
            // ScriptFileView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 764);
            this.Controls.Add(this.cmdDone);
            this.Controls.Add(this.tFile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ScriptFileView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Script File";
            this.Load += new System.EventHandler(this.ScriptFileView_Load);
            this.ResizeEnd += new System.EventHandler(this.ScriptFileView_ResizeEnd);
            this.Resize += new System.EventHandler(this.ScriptFileView_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdDone;
        private System.Windows.Forms.TextBox tFile;
    }
}