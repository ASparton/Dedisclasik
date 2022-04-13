using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_MAJ_CHAMPIONNAT
{
    public partial class JOUEURS
    {
        public override string ToString()
        {
            return NOM + " (" + SALAIRE + ", " + EQUIPES.VILLE + ")";
        }
    }
}
