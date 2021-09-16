using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(join_sezioni_stampa_tipo_avv_pag.Metadata))]
    public partial class join_sezioni_stampa_tipo_avv_pag : ISoftDeleted, IGestioneStato
    {
        public const string RISORSA_INTERNA = "0";
        public const string RISORSA_ESTERNA = "1";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public bool IsGenerica { get; set; }

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
        }
    }
}
