using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Configuration;

namespace PT2_Team1._1_Dédisclasik
{
    public partial class RegisterView : Form
    {
        private readonly OleDbConnection dbConnection;

        /// <summary>
        /// Créer l'interface permettant d'enregistrer un nouvel abonné dans la base.
        /// </summary>
        public RegisterView()
        {
            InitializeComponent();
            dbConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            AddCountriesToComboBox();
            FirstNameTextBox.Select();
        }

        /// <summary>
        /// Test les champs et enregistre un nouvel abonné dans la base.
        /// </summary>
        /// Guillaume Froidcourt
        private void RegisterButton_Click(object sender, EventArgs e)
        {
            bool firstNameCondition = FirstNameTextBox.Text.Length > 0 && FirstNameTextBox.Text.Length < 32 && FirstNameTextBox.Text.All(char.IsLetter);
            bool lastNameCondition = LastNameTextBox.Text.Length > 0 && LastNameTextBox.Text.Length < 32 && LastNameTextBox.Text.All(char.IsLetter);
            bool loginCondition = LoginTextBox.Text.Length > 0 && LoginTextBox.Text.Length < 32 && LoginTextBox.Text.All(char.IsLetterOrDigit);
            bool passwordCondition = PasswordTextBox.Text.Length > 5 && PasswordTextBox.Text.Length < 32 && PasswordTextBox.Text.Any(char.IsUpper)
                && PasswordTextBox.Text.Any(char.IsLower) && !PasswordTextBox.Text.Contains(" ");
            bool samePassword = CheckPassword.Text.Equals(PasswordTextBox.Text);

            // Message d'erreur si l'une des conditions n'est pas remplie
            if (!firstNameCondition)
            {
                MessageBox.Show("Vous devez renseignez votre prénom.", "Erreur formulaire",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!lastNameCondition)
            {
                MessageBox.Show("Vous devez renseignez votre nom.", "Erreur formulaire",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!loginCondition)
            {
                MessageBox.Show("Vous devez renseignez un nom d'utilisateur.", "Erreur formulaire",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!passwordCondition)
            {
                MessageBox.Show("Votre mot de passe doit faire au minimum 6 caractères, doit contenir au minimum une majuscule" +
                    " et ne doit pas avoir d'espace.", "Erreur formulaire", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!samePassword)
            {
                MessageBox.Show("Vos mots de passe ne correspondent pas.", "Vous n'avez pas les mêmes mots de passe.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int countryCode = -1;
                if (CountryComboBox.SelectedIndex != 0)
                {
                    countryCode = Register.GetCountryCode(CountryComboBox.SelectedItem.ToString());
                }
                Register.RegisterSubscriber(countryCode, LastNameTextBox.Text,
                    FirstNameTextBox.Text, LoginTextBox.Text, PasswordTextBox.Text);
            }
        }

        /// <summary>
        /// Révèle le mot de passe.
        /// </summary>
        /// Guillaume Froidcourt
        private void ShowPasswordButton_Click(object sender, EventArgs e)
        {
            if (PasswordTextBox.UseSystemPasswordChar)
            {
                PasswordTextBox.UseSystemPasswordChar = false;
                ShowPasswordButton.BringToFront();
            }
        }

        /// <summary>
        /// Cache le mot de passe.
        /// </summary>
        /// Guillaume Froidcourt
        private void HidePasswordButton_Click(object sender, EventArgs e)
        {
            if (!PasswordTextBox.UseSystemPasswordChar)
            {
                PasswordTextBox.UseSystemPasswordChar = true;
                HidePasswordButton.BringToFront();
            }
        }

        /// <summary>
        /// Méthode permettant d'ajouter les pays à la comboBox correspondante.
        /// </summary>
        /// Guillaume Froidcourt
        private void AddCountriesToComboBox()
        {
            CountryComboBox.Items.Add("");

            try
            {
                int j = 0;
                string count = "Select COUNT(CODE_PAYS) from PAYS";
                OleDbCommand counter = new OleDbCommand(count, dbConnection);
                counter.Connection.Open();
                OleDbDataReader read = counter.ExecuteReader();
                while (read.Read())
                {
                    j = read.GetInt32(0);
                }
                counter.Connection.Close();
                string sql = "SELECT NOM_PAYS from PAYS";
                OleDbCommand cmd = new OleDbCommand(sql, dbConnection);
                cmd.Connection.Open();
                OleDbDataReader reader = cmd.ExecuteReader();
                for (int i = 0; i < j; i++)
                {
                    while (reader.Read())
                    {
                        CountryComboBox.Items.Add(reader.GetString(0).Trim());
                    }
                }
                reader.Close();
                cmd.Connection.Close();
            }
            catch (OleDbException ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Affiche le mot de passe.
        /// </summary>
        /// Guillaume Froidcourt
        private void HideCheckPasswordButton_Click(object sender, EventArgs e)
        {
            if (CheckPassword.UseSystemPasswordChar)
            {
                CheckPassword.UseSystemPasswordChar = false;
                ShowCheckPasswordButton.BringToFront();
            }
        }

        /// <summary>
        /// Cache le mot de passe.
        /// </summary>
        /// Guillaume Froidcourt
        private void ShowCheckPasswordButton_Click(object sender, EventArgs e)
        {
            if (!CheckPassword.UseSystemPasswordChar)
            {
                CheckPassword.UseSystemPasswordChar = true;
                HideCheckPasswordButton.BringToFront();
            }
        }
    }
}