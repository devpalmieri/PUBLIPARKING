using Publisoftware.Data.LinqExtended;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_doc_output : ISoftDeleted, IGestioneStato
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
            id_struttura_emittente = id_struttura_emittente == 0 ? p_idStruttura : id_struttura_emittente = id_struttura_emittente;
            id_risorsa_stato = p_idRisorsa;
        }

        public string NumDoc
        {
            get
            {
                return barcode;
            }
        }

        public string Stato
        {
            get
            {
                return anagrafica_stato_doc.desc_stato;
            }
        }

        public string data_emissione_doc_String
        {
            get
            {
                return data_emissione_doc.HasValue ? data_emissione_doc.Value.ToShortDateString() : string.Empty;
            }
        }
    }
}
