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

namespace Musiciens
{
    public partial class Form1 : Form
    {
        OleDbConnection dbCon;
        public Form1()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            // Connexion serveur local - attention l'url de connexion est dans app.config
            dbCon = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            dbCon.Open();
            #region Accès à la base
            string sql = "Select Distinct Musicien.Code_Musicien, Nom_Musicien, Prénom_Musicien FROM Musicien "
                + "INNER JOIN Composer ON Musicien.Code_Musicien = Composer.Code_Musicien "
                + "ORDER BY Nom_Musicien ";
            OleDbCommand cmd = new OleDbCommand(sql, dbCon);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader.GetInt32(0));
                string nom = reader.GetString(1);
                string prénom;
                if (!reader.IsDBNull(2))
                    prénom = reader.GetString(2);
                else
                    prénom = "Inconnu";

                Musicien m = new Musicien(id, nom, prénom);
                listBox1.Items.Add(m);
            }
            reader.Close();
            #endregion
        }


        private void chargerOeuvres()
        {
            bool vide = true;
            listBox2.Items.Clear();
            Musicien Me = (Musicien)listBox1.SelectedItem;
            int id = Me.getID();
            string sql = "SELECT Titre_Oeuvre FROM Oeuvre" +
                " INNER JOIN Composer ON Oeuvre.Code_Oeuvre=Composer.Code_Oeuvre " +
                "WHERE Code_Musicien = " + id.ToString(); 
            sql += "ORDER BY Titre_Oeuvre";
            OleDbCommand cmd = new OleDbCommand(sql, dbCon);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listBox2.Items.Add(reader.GetString(0));
                vide = false;
            }
            if (vide) // Ne devrait jamais se produire...
            {
                listBox2.Items.Add("N'a rien composé");
            }
            reader.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            chargerOeuvres();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();

            string sql = "SELECT Code_Musicien, Nom_Musicien, Prénom_Musicien "
                + "FROM Musicien "
                + "WHERE Nom_Musicien LIKE '" + textBox1.Text + "%' "
                + "OR Prénom_Musicien LIKE '" + textBox1.Text + "%' "
                + "ORDER BY Nom_Musicien "; // On cherche les musiciens dont le nom ou le prénom commence par le texte rentré
            OleDbCommand cmd = new OleDbCommand(sql, dbCon);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader.GetInt32(0));
                string nom = reader.GetString(1);
                string prénom;
                if (!reader.IsDBNull(2))
                    prénom = reader.GetString(2);
                else
                    prénom = "Inconnu";

                Musicien m = new Musicien(id, nom, prénom);
                listBox1.Items.Add(m);
            }
        }
    }
}
