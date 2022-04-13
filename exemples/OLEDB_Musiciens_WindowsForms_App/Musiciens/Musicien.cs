using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musiciens
{
    class Musicien
    {
        int id;
        string nom;
        string prénom;

        public Musicien (int c, string n,string p)
        {
            id = c;
            nom = n;
            prénom = p;
        }
        public override string ToString()
        {
            return nom + " " + prénom;
        }
        public int getID()
        {
            return id;
        }
    }
}
