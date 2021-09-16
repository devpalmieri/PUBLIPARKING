using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_tipo_ispezione : ISoftDeleted
    {
        //public const int VEICOLI = 1;

        //public const int REDDITO_DA_LOCAZIONE = 3;
        //public const int REDDITO_LAVORO_DIPENDENTE = 4;
        //public const int REDDITO_PENSIONE = 5;

        public const string ACI = "ACI";
        public const int ACI_ID = 1;
        public const string SIATEL = "SIA";
        public const string DISPONIBILITA_FINANZIARIA = "DIS";
        public const int DISPONIBILITA_FINANZIARIA_ID = 2;
        public const string LOCAZIONI = "LOC";
        public const string IMMOBILI = "IMM";
        public const string MOBILI = "MOB";
        public const string STRAGIUDIZIALE = "STR";

        public bool IsSoftDeletable
        {
            get { return true; }
        }
    }
}
