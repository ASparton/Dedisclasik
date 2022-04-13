using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Configuration;

namespace WindowsFormsApp_GestionJoueurs
{
    public partial class Form1 : Form
    {
        OleDbConnection dbCon;
        public Form1()
        {
            InitializeComponent();
            InitConnexion();
            ChargeJoueurs();
        }

        public void InitConnexion()
        {
            // Connexion serveur local - attention l'url de connexion est dans app.config
            dbCon = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            dbCon.Open();
        }

        public void ChargeJoueurs()
        {
            #region Chargement des Joueurs
            // récupération de l'ensemble des Joueurs (id, nom, prénom)
            string sql = "Select ID_JOUEUR, NOM, SALAIRE from JOUEURS " +
                "where ID_EQUIPE = 1";
            OleDbCommand cmd = new OleDbCommand(sql, dbCon);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                // on récupère id, nom et salaire
                int id = reader.GetInt32(0); // Convert.ToInt32(reader.GetInt32(0));
                string nom = reader.GetString(1);
                int salaire = reader.GetInt32(2); // Convert.ToInt32(reader.GetString(2));

                // Création du joueur
                Joueurs j = new Joueurs(id, nom, salaire);
                // Ajout dans la ListBox
                ListBoxJoueurs.Items.Add(j);
            }
            reader.Close();
            #endregion
        }

        private void ListBoxJoueurs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListBoxJoueurs.SelectedIndex != -1)
            {
                // récupération du joueur sélectionné et mise àjour des deux TextBox
                Joueurs j = (Joueurs)ListBoxJoueurs.SelectedItem;
                textBoxNom.Text = j.getNom();
                textBoxSalaire.Text = j.getSalaire().ToString();
            }
        }

        private void Rafraichir()
        {
            textBoxNom.Clear();
            textBoxSalaire.Clear();
            ListBoxJoueurs.ClearSelected();
        }

        private void ButtonRafraichir_Click(object sender, EventArgs e)
        {
            Rafraichir();
        }

        private void ButtonAjouter_Click(object sender, EventArgs e)
        {
            if (textBoxNom.Text != "" && textBoxSalaire.Text != "")
            {
                // salaire numérique ?
                if (Int32.TryParse(textBoxSalaire.Text, out int s))
                {
                    // insertion dans la base, à partir des deux TextBox (ID_EQUIPE = 1) 
                    string insert = "insert into JOUEURS (NOM, SALAIRE, ID_EQUIPE) Values (?,?,1)";
                    OleDbCommand cmd = new OleDbCommand(insert, dbCon);
                    cmd.Parameters.Add("Nom", OleDbType.VarChar).Value = textBoxNom.Text;
                    cmd.Parameters.Add("Salaire", OleDbType.VarChar).Value = s;
                    cmd.ExecuteNonQuery();

                    // on récupère l'id du nouvel élement dans la base
                    string recup = "Select ID_JOUEUR from JOUEURS where NOM = '" +
                        textBoxNom.Text + "' and SALAIRE = '" + textBoxSalaire.Text + "'";
                    cmd = new OleDbCommand(recup, dbCon);
                    OleDbDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    int id = Convert.ToInt32(reader.GetInt32(0));
                    reader.Close();

                    // on crée l'objet joueur, on le rajoute dans la ListBox et on le "sélectionne"
                    Joueurs j = new Joueurs(id, textBoxNom.Text, s);
                    ListBoxJoueurs.Items.Add(j);
                    ListBoxJoueurs.SetSelected(ListBoxJoueurs.Items.IndexOf(j), true);
                }
                else PopupErreurOK("Le salaire doit être un nombre !", "Erreur");
            }
            else PopupErreurOK("Le nom et le salaire doivent être non vides !", "Erreur");
        }

        private void ButtonSupprimer_Click(object sender, EventArgs e)
        {
            if (ListBoxJoueurs.SelectedItem != null)
            {
                // récupération du joueur sélectionné
                Joueurs j = (Joueurs)ListBoxJoueurs.SelectedItem;

                // suppression du joueur de la base
                string delete = "delete from JOUEURS where ID_JOUEUR = " + j.getId().ToString();
                OleDbCommand cmd = new OleDbCommand(delete, dbCon);
                cmd.ExecuteNonQuery();

                // suppression de la ListBox et réinitialisation des Textbox
                ListBoxJoueurs.Items.Remove(ListBoxJoueurs.SelectedItem);
                Rafraichir();
            }
            else PopupErreurOK("Aucun joueur sélectionné dans la liste !", "Erreur");
        }

        private void ButtonModifier_Click(object sender, EventArgs e)
        {
            if (ListBoxJoueurs.SelectedItem != null)
            {
                if (textBoxNom.Text != "" && textBoxSalaire.Text != "")
                {
                    // salaire numérique ?
                    if (Int32.TryParse(textBoxSalaire.Text, out int s))
                    {
                        // le joueur sélectionné
                        Joueurs j = (Joueurs)ListBoxJoueurs.SelectedItem;

                        // modification du joueur dans la base
                        string update = "update JOUEURS " +
                            " set NOM = '" + textBoxNom.Text + "',  SALAIRE = '" + s +
                            "' WHERE ID_JOUEUR = " + j.getId();
                        OleDbCommand cmd = new OleDbCommand(update, dbCon);
                        cmd.ExecuteNonQuery();

                        // mise à jour du joueur et de la ListBox
                        ListBoxJoueurs.Items.Remove(ListBoxJoueurs.SelectedItem);
                        j.setNom(textBoxNom.Text);
                        j.setSalaire(s);
                        ListBoxJoueurs.Items.Add(j);
                        ListBoxJoueurs.SetSelected(ListBoxJoueurs.Items.IndexOf(j), true);
                    }
                    else PopupErreurOK("Le salaire doit être un nombre !", "Erreur");
                }
                else PopupErreurOK("Le nom et le salaire doivent être non vides !", "Erreur");
            }
            else PopupErreurOK("Aucun abonné sélectionné dans la liste !", "Erreur");
        }

        private void PopupErreurOK(string message, string caption)
        {
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBox.Show(message, caption, buttons);
        }

    }
}
