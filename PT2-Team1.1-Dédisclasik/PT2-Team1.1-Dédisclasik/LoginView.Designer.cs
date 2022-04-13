namespace PT2_Team1._1_Dédisclasik
{
    partial class LoginView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginView));
            this.LoginLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.loginTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.ConnectionButton = new System.Windows.Forms.Button();
            this.ApplicationNameLabel = new System.Windows.Forms.Label();
            this.HidePasswordButton = new System.Windows.Forms.Button();
            this.ShowPasswordButton = new System.Windows.Forms.Button();
            this.ChangePassword = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LoginLabel
            // 
            this.LoginLabel.AutoSize = true;
            this.LoginLabel.BackColor = System.Drawing.Color.Transparent;
            this.LoginLabel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LoginLabel.Font = new System.Drawing.Font("Poor Richard", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginLabel.ForeColor = System.Drawing.Color.MistyRose;
            this.LoginLabel.Location = new System.Drawing.Point(119, 108);
            this.LoginLabel.Name = "LoginLabel";
            this.LoginLabel.Size = new System.Drawing.Size(81, 33);
            this.LoginLabel.TabIndex = 0;
            this.LoginLabel.Text = "Login:";
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.BackColor = System.Drawing.Color.Transparent;
            this.PasswordLabel.Font = new System.Drawing.Font("Poor Richard", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordLabel.ForeColor = System.Drawing.Color.MistyRose;
            this.PasswordLabel.Location = new System.Drawing.Point(119, 248);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(118, 33);
            this.PasswordLabel.TabIndex = 1;
            this.PasswordLabel.Text = "Password:";
            // 
            // loginTextBox
            // 
            this.loginTextBox.Font = new System.Drawing.Font("Times New Roman", 12.75F);
            this.loginTextBox.Location = new System.Drawing.Point(169, 156);
            this.loginTextBox.Name = "loginTextBox";
            this.loginTextBox.Size = new System.Drawing.Size(239, 27);
            this.loginTextBox.TabIndex = 2;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold);
            this.passwordTextBox.Location = new System.Drawing.Point(167, 296);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(241, 30);
            this.passwordTextBox.TabIndex = 3;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // ConnectionButton
            // 
            this.ConnectionButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ConnectionButton.Font = new System.Drawing.Font("Times New Roman", 18F);
            this.ConnectionButton.Location = new System.Drawing.Point(508, 211);
            this.ConnectionButton.Name = "ConnectionButton";
            this.ConnectionButton.Size = new System.Drawing.Size(207, 49);
            this.ConnectionButton.TabIndex = 4;
            this.ConnectionButton.Text = "Se connecter";
            this.ConnectionButton.UseVisualStyleBackColor = true;
            this.ConnectionButton.Click += new System.EventHandler(this.ConnectionButton_Click);
            // 
            // ApplicationNameLabel
            // 
            this.ApplicationNameLabel.AutoSize = true;
            this.ApplicationNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.ApplicationNameLabel.Font = new System.Drawing.Font("Poor Richard", 41.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApplicationNameLabel.ForeColor = System.Drawing.Color.MistyRose;
            this.ApplicationNameLabel.Location = new System.Drawing.Point(269, 22);
            this.ApplicationNameLabel.Name = "ApplicationNameLabel";
            this.ApplicationNameLabel.Size = new System.Drawing.Size(266, 62);
            this.ApplicationNameLabel.TabIndex = 5;
            this.ApplicationNameLabel.Text = "Dedisclasik";
            // 
            // HidePasswordButton
            // 
            this.HidePasswordButton.Image = ((System.Drawing.Image)(resources.GetObject("HidePasswordButton.Image")));
            this.HidePasswordButton.Location = new System.Drawing.Point(372, 296);
            this.HidePasswordButton.Name = "HidePasswordButton";
            this.HidePasswordButton.Size = new System.Drawing.Size(36, 30);
            this.HidePasswordButton.TabIndex = 6;
            this.HidePasswordButton.TabStop = false;
            this.HidePasswordButton.UseVisualStyleBackColor = true;
            this.HidePasswordButton.Click += new System.EventHandler(this.ShowPasswordButton_Click);
            // 
            // ShowPasswordButton
            // 
            this.ShowPasswordButton.Image = ((System.Drawing.Image)(resources.GetObject("ShowPasswordButton.Image")));
            this.ShowPasswordButton.Location = new System.Drawing.Point(372, 295);
            this.ShowPasswordButton.Name = "ShowPasswordButton";
            this.ShowPasswordButton.Size = new System.Drawing.Size(36, 31);
            this.ShowPasswordButton.TabIndex = 7;
            this.ShowPasswordButton.TabStop = false;
            this.ShowPasswordButton.UseVisualStyleBackColor = true;
            this.ShowPasswordButton.Click += new System.EventHandler(this.HidePasswordButton_Click);
            // 
            // ChangePassword
            // 
            this.ChangePassword.Font = new System.Drawing.Font("Times New Roman", 15F);
            this.ChangePassword.Location = new System.Drawing.Point(531, 391);
            this.ChangePassword.Name = "ChangePassword";
            this.ChangePassword.Size = new System.Drawing.Size(257, 47);
            this.ChangePassword.TabIndex = 8;
            this.ChangePassword.Text = "Changer de mot de passe";
            this.ChangePassword.UseVisualStyleBackColor = true;
            this.ChangePassword.Click += new System.EventHandler(this.ChangePassword_Click);
            // 
            // LoginView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AntiqueWhite;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ChangePassword);
            this.Controls.Add(this.HidePasswordButton);
            this.Controls.Add(this.ShowPasswordButton);
            this.Controls.Add(this.ApplicationNameLabel);
            this.Controls.Add(this.ConnectionButton);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.loginTextBox);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.LoginLabel);
            this.DoubleBuffered = true;
            this.Name = "LoginView";
            this.Text = "login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LoginLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.TextBox loginTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Button ConnectionButton;
        private System.Windows.Forms.Label ApplicationNameLabel;
        private System.Windows.Forms.Button HidePasswordButton;
        private System.Windows.Forms.Button ShowPasswordButton;
        private System.Windows.Forms.Button ChangePassword;
    }
}