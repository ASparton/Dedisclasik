    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;
    using System.Windows.Forms;
    using System.Collections;

    namespace WindowsFormsApp2
    {
        public partial class Form1 : Form
        {
            MusiqueSQLEntities musique;

            public Form1()
            {
                InitializeComponent();
                musique = new MusiqueSQLEntities();
                ChargeLesCompositeurs();
            }

            private void ChargeLesCompositeurs()
            {
                // on récupère tous les compositeurs
                var musiciens = (from m in musique.Musicien
                                 where m.Oeuvre.Count > 0
                                 orderby m.Nom_Musicien
                                 select m).ToList();
                // on crée les objets locaux et on remplit la listbox
                foreach (Musicien m in musiciens)
                {
                    listBox1.Items.Add(m);
                }
            }

            private void textBox1_TextChanged(object sender, EventArgs e)
            {
                // on récupère tous les compositeurs satisfaisant le critère de recherche
                // ToUpper --> pour rester "case insensitive"
                var musiciens = (from m in musique.Musicien
                                 where m.Oeuvre.Count > 0 && m.Nom_Musicien.ToUpper().Contains(textBox1.Text.ToUpper())
                                 orderby m.Nom_Musicien
                                 select m).ToList();
                // on réinitialise les deux listBox
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                // on insère dans listBox1 les musiciens récupérés
                foreach (Musicien m in musiciens)
                {
                    listBox1.Items.Add(m);
                }
            }

            private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
            {
                listBox2.Items.Clear();
                Musicien m = (Musicien)listBox1.SelectedItem;
                foreach (Oeuvre o in m.Oeuvre)
                {
                    listBox2.Items.Add(o.Titre_Oeuvre);
                }
            }
        }
    }

/*
foreach (Diriger d in x.me.Diriger)
    foreach (Composition_Disque c in d.Enregistrement.Composition_Disque)
        listBox2.Items.Add(c.Disque.Album.Titre_Album);
*/
