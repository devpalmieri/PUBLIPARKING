using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_carrello.Metadata))]
    public partial class tab_carrello : Itab_carrello, ISoftDeleted, IGestioneStato
    {
        public static string ANN = "ANN-";
        public static string DEF_PAG = "DEF-PAG";
        public const string ATT_RPT = "ATT-RPT";
        public const string ATT_PGT = "ATT-PAG";

        public const string FONTE_CARRELLO_WEB = "WEB";
        public const string FONTE_CARRELLO_PSP = "PSP";

        public const string OPERAZIONE_VER = "VER";
        public const string OPERAZIONE_RPT = "RPT";

        public const string TIPO_CARRELLO_S = "S";
        public const string TIPO_CARRELLO_M = "M";
        public const string TIPO_CARRELLO_C = "C";
        public static string ANNULLATO = "ANN-ANN";
        public const string TIPO_VERSAMENTO_PO = "PO";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_struttura_stato = p_idStruttura;
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
