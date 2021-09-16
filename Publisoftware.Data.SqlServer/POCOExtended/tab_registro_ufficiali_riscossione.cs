using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_registro_ufficiali_riscossione.Metadata))]
    public partial class tab_registro_ufficiali_riscossione : ISoftDeleted, IGestioneStato
    {
        public const int ATT_ATT_ID = 1;
        public const String ATT_ATT = "ATT-ATT";

        public const string FLAG_VERBALEQUIETANZA_VERBALE = "V";
        public const string FLAG_VERBALEQUIETANZA_QUIETANZA = "Q";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        /// <summary>
        /// Gestisce l'aggiornamento dei campi utente dello stato
        /// </summary>
        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }

        public sealed class Metadata
        {
            private Metadata()
            {

            }
        }
    }
}
