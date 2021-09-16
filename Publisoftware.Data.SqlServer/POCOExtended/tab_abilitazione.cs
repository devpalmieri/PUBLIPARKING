using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_abilitazione.Metadata))]
    public partial class tab_abilitazione:ISoftDeleted, IGestioneStato
    {
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

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Required(ErrorMessage="Nessuna Risorsa Selezionata")]
            [Range(1, Int32.MaxValue, ErrorMessage="Nessuna Risorsa Selezionata")]
            public int id_risorsa { get; set; }

            [Required(ErrorMessage = "Nessuna Struttura Selezionata")]
            [Range(1, Int32.MaxValue, ErrorMessage = "Nessuna Struttura Selezionata")]
            public int id_struttura_aziendale { get; set; }

            [Required(ErrorMessage = "Nessuna Struttura Selezionata")]
            [Range(1, Int32.MaxValue, ErrorMessage = "Nessun Ente Selezionato")]
            public int id_ente { get; set; }
        }
    }
}
