using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PT2_Team1._1_Dédisclasik
{
    public partial class LoginView : Form
    {
        public KeyValuePair<int, bool> LoginResult { get; private set; }    // Le code de l'abonné connecté dans la base

        /// <summary>
        /// Créer la fenêtre de connexion.
        /// </summary>
        public LoginView()
        {
            InitializeComponent();
            LoginResult = new KeyValuePair<int, bool>();
        }

        /// <summary>
        /// Lie le login et le mot de passe donné à un abonné dans la base pour l'emmener sur sa fenêtre de gestion d'emprunts.
        /// </summary>
        /// Guillaume Froidcourt
        private void ConnectionButton_Click(object sender, EventArgs e)
        {
            LoginResult = Login.GetUserLogin(loginTextBox.Text, passwordTextBox.Text);
            if (LoginResult.Key == -1)
            {
                MessageBox.Show("Connexion impossible : Mauvais login ou mot de passe", "Connexion impossible", MessageBoxButtons.OK);
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        /// <summary>
        /// Dévoile le mot de passe dans la extBox.
        /// </summary>
        /// Guillaume Froidcourt
        private void ShowPasswordButton_Click(object sender, EventArgs e)
        {
            if (passwordTextBox.UseSystemPasswordChar)
            {
                passwordTextBox.UseSystemPasswordChar = false;
                ShowPasswordButton.BringToFront();
            }

        }

        /// <summary>
        /// Cache le mot de passe dans la TextBox.
        /// </summary>
        /// Guillaume Froidcourt
        private void HidePasswordButton_Click(object sender, EventArgs e)
        {
            if (!passwordTextBox.UseSystemPasswordChar)
            {
                passwordTextBox.UseSystemPasswordChar = true;
                HidePasswordButton.BringToFront();
            }

        }

        /// <summary>
        /// Ouvre une nouvelle page permettant à l'utilisateur de changer son mot de passe.
        /// </summary>
        private void ChangePassword_Click(object sender, EventArgs e)
        {
            ChangePassword change = new ChangePassword();
            change.ShowDialog();
        }
    }
}
