using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Data.OleDb;
using System.Configuration;

namespace PT2_Team1._1_Dédisclasik
{
    partial class ChangePassword : Form
    {
        #region Attributs et constructeur
        private MD5 saltForHash;
        OleDbConnection dbConnection;
        bool samePassword, newPasswordValid;
        public ChangePassword()
        {
            InitializeComponent();
            saltForHash = MD5.Create();
            dbConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            samePassword = newPasswordValid = false;
        }
        #endregion
        #region Boutons pour chacher/afficher les mots de passe
        /// <summary>
        /// Afficher le mot de passe
        /// </summary>
        /// Guillaume Froidcourt
        private void HideAncienButton_Click(object sender, EventArgs e)
        {
            if (AncienPasswordTextBox.UseSystemPasswordChar)
            {
                AncienPasswordTextBox.UseSystemPasswordChar = false;
                ShowAncienButton.BringToFront();
            }
        }

        /// <summary>
        /// Cacher le mot de passes
        /// </summary>
        /// Guillaume Froidcourt
        private void ShowAncienButton_Click(object sender, EventArgs e)
        {
            if (!AncienPasswordTextBox.UseSystemPasswordChar)
            {
                AncienPasswordTextBox.UseSystemPasswordChar = true;
                HideAncienButton.BringToFront();
            }

        }

        /// <summary>
        /// Afficher le mot de passe
        /// </summary>
        /// Guillaume Froidcourt
        private void HideNouveauButton_Click(object sender, EventArgs e)
        {
            if (NouveauPasswordTextBox.UseSystemPasswordChar)
            {
                NouveauPasswordTextBox.UseSystemPasswordChar = false;
                ShowNouveauButton.BringToFront();
            }
        }

        /// <summary>
        /// Cacher le mot de passes
        /// </summary>
        /// Guillaume Froidcourt
        private void ShowNouveauButton_Click(object sender, EventArgs e)
        {

            if (!NouveauPasswordTextBox.UseSystemPasswordChar)
            {
                NouveauPasswordTextBox.UseSystemPasswordChar = true;
                HideNouveauButton.BringToFront();
            }
        }

        /// <summary>
        /// Afficher le mot de passe
        /// </summary>
        /// Guillaume Froidcourt
        private void HideConfirmationButton_Click(object sender, EventArgs e)
        {
            if (NouveauConfirmPasswordTextBox.UseSystemPasswordChar)
            {
                NouveauConfirmPasswordTextBox.UseSystemPasswordChar = false;
                ShowConfirmerButton.BringToFront();
            }
        }

        /// <summary>
        /// Cacher le mot de passes
        /// </summary>
        /// Guillaume Froidcourt
        private void ShowConfirmerButton_Click(object sender, EventArgs e)
        {
            if (!NouveauConfirmPasswordTextBox.UseSystemPasswordChar)
            {
                NouveauConfirmPasswordTextBox.UseSystemPasswordChar = true;
                HideConfirmationButton.BringToFront();
            }
        }
        #endregion
        #region Méthodes relatives au changement du mot de passe
        /// <summary>
        /// Récupère l'ID d'un abonné à partir de son login.
        /// </summary>
        /// <returns> L'ID de l'abonné</returns>
        /// Guillaume Froidcourt et Grégory Simon
        private int GetCodeFromLogin()
        {
            int ID = -1;
            string sql = "SELECT CODE_ABONNÉ FROM ABONNÉS WHERE LOGIN_ABONNÉ = '" + LogintextBox.Text + "'";
            dbConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, dbConnection);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ID = reader.GetInt32(0);
            }
            reader.Close();
            dbConnection.Close();
            return ID;
        }

        /// <summary>
        /// Méthode permettant de vérifier si l'ancien mot de passe est correct
        /// </summary>
        /// <returns> Vrai si et seulement si les deux mots de passe correspondent.</returns>
        private bool CheckOldPassword()
        {
            int ID = GetCodeFromLogin();
            string oldPass = "";
            string sql = "SELECT PASSWORD_ABONNÉ FROM ABONNÉS WHERE CODE_ABONNÉ = " + ID;
            dbConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, dbConnection);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                oldPass = reader.GetString(0);
            }
            reader.Close();
            dbConnection.Close();
            var salting = saltForHash.ComputeHash(new UTF8Encoding().GetBytes(AncienPasswordTextBox.Text));
            string hash = BitConverter.ToString(salting).Replace("-", string.Empty);
            if (hash.Equals(oldPass))
            {
                return true;
            }

            return false;
        }



        /// <summary>
        /// Méthode permettant de changez le mot de passe d'un abonné.
        /// </summary>
        /// Guillaume Froidcourt et Grégory Simon
        private void ChangePasswordButton_Click(object sender, EventArgs e)
        {
            newPasswordValid = NouveauPasswordTextBox.Text.Length > 5 && NouveauPasswordTextBox.Text.Length < 32 && NouveauPasswordTextBox.Text.Any(char.IsUpper)
                  && NouveauPasswordTextBox.Text.Any(char.IsLower) && !NouveauPasswordTextBox.Text.Contains(" ");
            samePassword = NouveauPasswordTextBox.Text.Equals(NouveauConfirmPasswordTextBox.Text);
            int ID = GetCodeFromLogin();
            if (CheckOldPassword() && newPasswordValid && samePassword && !(AncienPasswordTextBox.Text.Equals(NouveauPasswordTextBox.Text) && samePassword))
            {
                DialogResult result = MessageBox.Show("Voulez-vous vraiment modifier votre mot de passe ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // Hashage du mot de l'ancien mot de passe pour voir s'il existe dans la base
                    var salting = saltForHash.ComputeHash(new UTF8Encoding().GetBytes(AncienPasswordTextBox.Text));
                    string hash = BitConverter.ToString(salting).Replace("-", string.Empty);

                    // Hasage du nouveau mot de passe afin de l'entrer dans la base
                    salting = saltForHash.ComputeHash(new UTF8Encoding().GetBytes(NouveauPasswordTextBox.Text));
                    var newHash = BitConverter.ToString(salting).Replace("-", string.Empty);
                    // Mise à jour du mot de passe avec le nouveau
                    string sql = "UPDATE ABONNÉS SET PASSWORD_ABONNÉ = '" + newHash + "' WHERE PASSWORD_ABONNÉ = '" + hash + "' AND CODE_ABONNÉ = " + ID;
                    dbConnection.Open();
                    OleDbCommand cmd = new OleDbCommand(sql, dbConnection);
                    cmd.ExecuteNonQuery();
                    dbConnection.Close();
                    MessageBox.Show("Mot de passe modifié.", "Modification effectuée", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            else if (LogintextBox.Text.Length == 0)
            {
                MessageBox.Show("Vous devez renseigner votre login.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Si le l'ancien mot de passe ne correspont pas
            else if (!CheckOldPassword())
            {
                MessageBox.Show("Ancien mot de passe invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Si le nouveau mot de passe n'est pas valide
            else if (!newPasswordValid)
            {
                MessageBox.Show("Votre nouveau mot de passe doit faire au minimum 6 caractères, doit contenir au minimum une majuscule" +
                     " et ne doit pas avoir d'espace.", "Erreur formulaire", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Si les deux nouveaux mots de passe ne correspondent pas
            else if (!samePassword)
            {
                MessageBox.Show("Vos mots de passe ne correspondent pas.", "Vous n'avez pas les mêmes mots de passe", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Si le nouveau mot de passe et l'ancien sont les mêmes
            else if (AncienPasswordTextBox.Text.Equals(NouveauPasswordTextBox.Text) && samePassword)
            {
                MessageBox.Show("Attention, votre nouveau mot de passe est le même que le précédent.", "Mêmes mots de passe", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}
