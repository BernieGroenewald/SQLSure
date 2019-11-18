namespace SentryGeneral
{
    partial class ViewObjectHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewObjectHistory));
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvHistory
            // 
            this.dgvHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistory.Location = new System.Drawing.Point(0, 0);
            this.dgvHistory.MultiSelect = false;
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.RowHeadersWidth = 20;
            this.dgvHistory.RowTemplate.Height = 30;
            this.dgvHistory.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistory.Size = new System.Drawing.Size(1415, 630);
            this.dgvHistory.TabIndex = 2;
            this.dgvHistory.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHistory_CellContentClick);
            // 
            // ViewObjectHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1415, 630);
            this.Controls.Add(this.dgvHistory);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ViewObjectHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Object History";
            this.Load += new System.EventHandler(this.ViewObjectHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvHistory;
    }
}