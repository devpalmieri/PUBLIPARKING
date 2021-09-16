using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(join_file.Metadata))]
    public partial class join_file : ISoftDeleted, IGestioneStato
    {
        public const string ATT_ATT = "ATT-ATT";

        public static int TIPO_SPED_NOT_ID = 4;

        public static string TIPO_SPED_NOT = "NOT";
        public static string TIPO_ICP = "ICP";
        public static string TIPO_TOSAP = "TOS";
        public static string TIPO_COSAP = "COS";
        public static string TIPO_LETTURA = "LET";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_struttura = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

        }
    }
}
