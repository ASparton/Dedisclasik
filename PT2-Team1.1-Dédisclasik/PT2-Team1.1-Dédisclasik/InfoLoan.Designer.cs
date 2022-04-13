
namespace PT2_Team1._1_Dédisclasik
{
    partial class InfoLoan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoLoan));
            this.LastNameLabel = new System.Windows.Forms.Label();
            this.FirstNameLabel = new System.Windows.Forms.Label();
            this.LoginLabel = new System.Windows.Forms.Label();
            this.LoanDateLabel = new System.Windows.Forms.Label();
            this.ExpectedReturnDateLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LastNameLabel
            // 
            this.LastNameLabel.AutoSize = true;
            this.LastNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.LastNameLabel.Font = new System.Drawing.Font("Poor Richard", 18.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LastNameLabel.ForeColor = System.Drawing.Color.MistyRose;
            this.LastNameLabel.Location = new System.Drawing.Point(131, 71);
            this.LastNameLabel.Name = "LastNameLabel";
            this.LastNameLabel.Size = new System.Drawing.Size(69, 28);
            this.LastNameLabel.TabIndex = 0;
            this.LastNameLabel.Text = "label1";
            // 
            // FirstNameLabel
            // 
            this.FirstNameLabel.AutoSize = true;
            this.FirstNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.FirstNameLabel.Font = new System.Drawing.Font("Poor Richard", 18.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FirstNameLabel.ForeColor = System.Drawing.Color.MistyRose;
            this.FirstNameLabel.Location = new System.Drawing.Point(131, 28);
            this.FirstNameLabel.Name = "FirstNameLabel";
            this.FirstNameLabel.Size = new System.Drawing.Size(74, 28);
            this.FirstNameLabel.TabIndex = 1;
            this.FirstNameLabel.Text = "label2";
            // 
            // LoginLabel
            // 
            this.LoginLabel.AutoSize = true;
            this.LoginLabel.BackColor = System.Drawing.Color.Transparent;
            this.LoginLabel.Font = new System.Drawing.Font("Poor Richard", 18.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginLabel.ForeColor = System.Drawing.Color.MistyRose;
            this.LoginLabel.Location = new System.Drawing.Point(90, 113);
            this.LoginLabel.Name = "LoginLabel";
            this.LoginLabel.Size = new System.Drawing.Size(121, 28);
            this.LoginLabel.TabIndex = 2;
            this.LoginLabel.Text = "LoginLabel";
            // 
            // LoanDateLabel
            // 
            this.LoanDateLabel.AutoSize = true;
            this.LoanDateLabel.BackColor = System.Drawing.Color.Transparent;
            this.LoanDateLabel.Font = new System.Drawing.Font("Poor Richard", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoanDateLabel.ForeColor = System.Drawing.Color.MistyRose;
            this.LoanDateLabel.Location = new System.Drawing.Point(133, 170);
            this.LoanDateLabel.Name = "LoanDateLabel";
            this.LoanDateLabel.Size = new System.Drawing.Size(51, 20);
            this.LoanDateLabel.TabIndex = 3;
            this.LoanDateLabel.Text = "label4";
            // 
            // ExpectedReturnDateLabel
            // 
            this.ExpectedReturnDateLabel.AutoSize = true;
            this.ExpectedReturnDateLabel.BackColor = System.Drawing.Color.Transparent;
            this.ExpectedReturnDateLabel.Font = new System.Drawing.Font("Poor Richard", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExpectedReturnDateLabel.ForeColor = System.Drawing.Color.MistyRose;
            this.ExpectedReturnDateLabel.Location = new System.Drawing.Point(134, 217);
            this.ExpectedReturnDateLabel.Name = "ExpectedReturnDateLabel";
            this.ExpectedReturnDateLabel.Size = new System.Drawing.Size(51, 20);
            this.ExpectedReturnDateLabel.TabIndex = 4;
            this.ExpectedReturnDateLabel.Text = "label5";
            // 
            // InfoLoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(354, 324);
            this.Controls.Add(this.ExpectedReturnDateLabel);
            this.Controls.Add(this.LoanDateLabel);
            this.Controls.Add(this.LoginLabel);
            this.Controls.Add(this.FirstNameLabel);
            this.Controls.Add(this.LastNameLabel);
            this.DoubleBuffered = true;
            this.Name = "InfoLoan";
            this.Text = "InfoLoan";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LastNameLabel;
        private System.Windows.Forms.Label FirstNameLabel;
        private System.Windows.Forms.Label LoginLabel;
        private System.Windows.Forms.Label LoanDateLabel;
        private System.Windows.Forms.Label ExpectedReturnDateLabel;
    }
}