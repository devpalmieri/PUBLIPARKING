using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class join_dichiarazione_terzo_contribuente : ISoftDeleted
    {

        public static string VALIDO = "VAL-VAL";
        public static string SOSPESO = "SSP-";
        public static string SOSPESO_POSITIVO = "SSP-POS";
        public static string SOSPESO_NEGATIVO = "SSP-NEG";
        public bool IsSoftDeletable
        {
            get { return true; }
        }
    }
}
