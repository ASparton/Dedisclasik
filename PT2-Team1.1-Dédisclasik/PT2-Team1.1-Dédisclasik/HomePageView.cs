using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace PT2_Team1._1_Dédisclasik
{
    public partial class HomePageView : Form
    {
        // Le résultat de la connexion s'il y a eu (code de l'utilisateur en ID, booléen à true si administrateur)
        public KeyValuePair<int, bool> LoginResult { get; private set; }

        /// <summary>
        /// Créer la page d'accueil du logiciel ammenant vers le menu d'enregistrement et de connexion.
        /// </summary>
        public HomePageView()
        {
            InitializeComponent();
            LoginResult = new KeyValuePair<int, bool>(-1, false);
        }

        /// <summary>
        /// Ouvre la fenêtre de connexion.
        /// </summary>
        /// Guillaume Froidcourt
        private void LoginViewButton_Click(object sender, EventArgs e)
        {
            Hide(); // Cache la fenêtre actuelle avant d'ouvrir la fenêtre de connexion

            LoginView loginView = new LoginView();
            loginView.ShowDialog();
            if (loginView.DialogResult == DialogResult.OK)
            {
                LoginResult = loginView.LoginResult;
                Close();
            }
        }

        /// <summary>
        /// Ouvre la fenêtre d'enregistrement.
        /// </summary>
        /// Guillaume Froidcourt
        private void RegisterViewButton_Click(object sender, EventArgs e)
        {
            new RegisterView().ShowDialog();
        }
    }
}