namespace SentryAdmin
{
    partial class Register
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Register));
            this.tReg1 = new System.Windows.Forms.TextBox();
            this.tReg2 = new System.Windows.Forms.TextBox();
            this.tReg3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmdRegister = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tCompanyName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tReg1
            // 
            this.tReg1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tReg1.Location = new System.Drawing.Point(143, 52);
            this.tReg1.MaxLength = 4;
            this.tReg1.Name = "tReg1";
            this.tReg1.Size = new System.Drawing.Size(43, 22);
            this.tReg1.TabIndex = 0;
            this.tReg1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tReg1.TextChanged += new System.EventHandler(this.tReg1_TextChanged);
            // 
            // tReg2
            // 
            this.tReg2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tReg2.Location = new System.Drawing.Point(205, 52);
            this.tReg2.MaxLength = 4;
            this.tReg2.Name = "tReg2";
            this.tReg2.Size = new System.Drawing.Size(43, 22);
            this.tReg2.TabIndex = 1;
            this.tReg2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tReg2.TextChanged += new System.EventHandler(this.tReg2_TextChanged);
            // 
            // tReg3
            // 
            this.tReg3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tReg3.Location = new System.Drawing.Point(270, 52);
            this.tReg3.MaxLength = 4;
            this.tReg3.Name = "tReg3";
            this.tReg3.Size = new System.Drawing.Size(43, 22);
            this.tReg3.TabIndex = 2;
            this.tReg3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tReg3.TextChanged += new System.EventHandler(this.tReg3_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Registration Code";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(189, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(254, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "-";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(88, 79);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // cmdRegister
            // 
            this.cmdRegister.Image = ((System.Drawing.Image)(resources.GetObject("cmdRegister.Image")));
            this.cmdRegister.Location = new System.Drawing.Point(221, 146);
            this.cmdRegister.Name = "cmdRegister";
            this.cmdRegister.Size = new System.Drawing.Size(92, 37);
            this.cmdRegister.TabIndex = 7;
            this.cmdRegister.Text = "Register";
            this.cmdRegister.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdRegister.UseVisualStyleBackColor = true;
            this.cmdRegister.Click += new System.EventHandler(this.cmdRegister_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Organization Name";
            // 
            // tCompanyName
            // 
            this.tCompanyName.Location = new System.Drawing.Point(143, 80);
            this.tCompanyName.Name = "tCompanyName";
            this.tCompanyName.Size = new System.Drawing.Size(170, 20);
            this.tCompanyName.TabIndex = 9;
            this.tCompanyName.TextChanged += new System.EventHandler(this.tCompanyName_TextChanged);
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 195);
            this.Controls.Add(this.tCompanyName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdRegister);
            this.Controls.Add(this.tReg3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tReg2);
            this.Controls.Add(this.tReg1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Register";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Register";
            this.Load += new System.EventHandler(this.Register_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tReg1;
        private System.Windows.Forms.TextBox tReg2;
        private System.Windows.Forms.TextBox tReg3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button cmdRegister;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tCompanyName;
    }
}