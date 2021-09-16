using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_pec_configurazioni : ISoftDeleted, IGestioneStato
    {
        public const string FLAG_INVIO_PEC_USCITA = "O";
        public const string FLAG_INVIO_PEC_ENTRATA = "I";

        public const string TIPO_PEC = "PEC";
        public const string TIPO_EMAIL = "EMAIL";

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
    }
}
