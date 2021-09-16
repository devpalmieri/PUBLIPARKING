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
    public partial class tab_doc_input : ISoftDeleted, IGestioneStato
    {
        public const string ESITO_ACC = "Accolta";
        public const string ESITO_RES = "Respinta";

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

        public string NumDoc
        {
            get
            {
                if (string.IsNullOrEmpty(identificativo_doc_input))
                {
                    return cod_doc + "/" + anno + "/" + prog_tipo_doc_entrata;
                }
                else
                {
                    return identificativo_doc_input.Trim();
                }
            }
        }

        public string TipoPratica
        {
            get
            {
                return tab_tipo_doc_entrate.descr_doc;
            }
        }

        public string DescrizioneIstanza
        {
            get
            {
                return TipoPratica + " " + NumDoc;
            }
        }

        public string data_presentazione_String
        {
            get
            {
                return data_presentazione.HasValue ? data_presentazione.Value.ToShortDateString() : string.Empty;
            }
        }

        public string Stato
        {
            get
            {
                return anagrafica_stato_doc.desc_stato;
            }
        }

    }
}
