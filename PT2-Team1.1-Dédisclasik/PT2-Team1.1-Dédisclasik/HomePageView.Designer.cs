namespace PT2_Team1._1_Dédisclasik
{
    partial class HomePageView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomePageView));
            this.RegisterViewButton = new System.Windows.Forms.Button();
            this.LoginViewButton = new System.Windows.Forms.Button();
            this.WelcomeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // RegisterViewButton
            // 
            this.RegisterViewButton.Font = new System.Drawing.Font("Times New Roman", 14.25F);
            this.RegisterViewButton.Location = new System.Drawing.Point(300, 232);
            this.RegisterViewButton.Name = "RegisterViewButton";
            this.RegisterViewButton.Size = new System.Drawing.Size(157, 54);
            this.RegisterViewButton.TabIndex = 11;
            this.RegisterViewButton.Text = "S\'enregistrer";
            this.RegisterViewButton.UseVisualStyleBackColor = true;
            this.RegisterViewButton.Click += new System.EventHandler(this.RegisterViewButton_Click);
            // 
            // LoginViewButton
            // 
            this.LoginViewButton.Font = new System.Drawing.Font("Times New Roman", 14.25F);
            this.LoginViewButton.Location = new System.Drawing.Point(300, 325);
            this.LoginViewButton.Name = "LoginViewButton";
            this.LoginViewButton.Size = new System.Drawing.Size(157, 54);
            this.LoginViewButton.TabIndex = 12;
            this.LoginViewButton.Text = "Se connecter";
            this.LoginViewButton.UseVisualStyleBackColor = true;
            this.LoginViewButton.Click += new System.EventHandler(this.LoginViewButton_Click);
            // 
            // WelcomeLabel
            // 
            this.WelcomeLabel.BackColor = System.Drawing.Color.Transparent;
            this.WelcomeLabel.Font = new System.Drawing.Font("Poor Richard", 42.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WelcomeLabel.ForeColor = System.Drawing.Color.MistyRose;
            this.WelcomeLabel.Location = new System.Drawing.Point(22, 73);
            this.WelcomeLabel.MaximumSize = new System.Drawing.Size(100000, 180);
            this.WelcomeLabel.Name = "WelcomeLabel";
            this.WelcomeLabel.Size = new System.Drawing.Size(776, 156);
            this.WelcomeLabel.TabIndex = 13;
            this.WelcomeLabel.Text = "Bienvenue sur l\'application de votre boutique Dedisclasik";
            this.WelcomeLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // HomePageView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AntiqueWhite;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.WelcomeLabel);
            this.Controls.Add(this.LoginViewButton);
            this.Controls.Add(this.RegisterViewButton);
            this.DoubleBuffered = true;
            this.Name = "HomePageView";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button RegisterViewButton;
        private System.Windows.Forms.Button LoginViewButton;
        private System.Windows.Forms.Label WelcomeLabel;
    }
}