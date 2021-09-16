using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_validazione_approvazione_liste_spedizione.Metadata))]
    public partial class tab_validazione_approvazione_liste_spedizione : ISoftDeleted, IGestioneStato
    {
        public const string ATT_ATT = "ATT-ATT";

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

            //[Required]
            //[Range(1, Int32.MaxValue, ErrorMessage = "Nessuna Struttura Selezionata")]
            //[DisplayName("Struttura Approvazione")]
            //public int? id_struttura_approvazione { get; set; }

            //[Required]
            //[Range(1, Int32.MaxValue, ErrorMessage = "Nessuna Risorsa Selezionata")]
            //[DisplayName("Risorsa Approvazione")]
            //public int? id_risorsa_approvazione { get; set; }

            //[Required(ErrorMessage = "Inserire una determina")]
            //[DisplayName("Determina")]
            //public string numero_determina { get; set; }

            //[Required(ErrorMessage = "Inserire una data di approvazione")]
            //[DisplayName("data approvazione determina")]
            //public DateTime data_approvazione_determina { get; set; }

        }
    }
}
