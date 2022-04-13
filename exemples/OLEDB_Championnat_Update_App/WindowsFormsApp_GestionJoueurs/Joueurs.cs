using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp_GestionJoueurs
{
    class Joueurs
    {
        int id;
        string nom;
        int salaire;

        public Joueurs(int c, string n, int s)
        {
            id = c;
            nom = n;
            salaire = s;
        }

        public override string ToString()
        {
            return "(" + salaire.ToString() + ") " + nom;

        }

        public int getId()
        {
            return id;
        }

        public string getNom()
        {
            return nom;
        }

        public void setNom(string n)
        {
            nom = n;
        }

        public int getSalaire()
        {
            return salaire;
        }

        public void setSalaire(int s)
        {
            salaire = s;
        }


    }
}
