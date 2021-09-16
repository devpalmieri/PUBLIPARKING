using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    //// Abilitare per la gestione dei controlli nelle View
    //[MetadataTypeAttribute(typeof(Tipo Entità))]
    public partial class join_tab_ispezioni_coattivo_tipo_ispezione : ISoftDeleted, IGestioneStato
    {
        public static string VAL = "VAL";
        public static string VAL_VAL = "VAL-VAL";
        public static string VAL_OLD = "VAL-OLD";
        public const string ANN_ANN = "ANN-ANN";

        public static string CLOSE = "CLOSE";
        public const string UNDO = "UNDO";
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

        public string data_fine_ispezione_String
        {
            get
            {
                if (data_fine_ispezione.HasValue)
                {
                    return data_fine_ispezione.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string esitoIspezione
        {
            get
            {
                if ((flag_fine_ispezione == "1" || flag_fine_ispezione == "2") && flag_esito_ispezione == "2")
                {
                    return "Negativo";
                }
                else if ((flag_fine_ispezione == "1" || flag_fine_ispezione == "2") && flag_esito_ispezione == "1")
                {
                    return "Positivo";
                }
                else if ((flag_fine_ispezione == null || flag_fine_ispezione == "0") && (flag_esito_ispezione == null || flag_esito_ispezione == "0"))
                {
                    return "Non effettuata";
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
