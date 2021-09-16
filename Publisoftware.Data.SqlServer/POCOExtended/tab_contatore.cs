using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_contatore.Metadata))]
    public partial class tab_contatore : ISoftDeleted, IGestioneStato
    {
        public const string ATT_ATT = "ATT-ATT";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public string data_istallazione_presa_incarico_String
        {
            get
            {
                if (data_istallazione_presa_incarico.HasValue)
                {
                    return data_istallazione_presa_incarico.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_cessazione_String
        {
            get
            {
                if (data_cessazione.HasValue)
                {
                    return data_cessazione.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string LetturaIniziale
        {
            get
            {
                return string.Format("{0:0}", lettura_iniziale) + " mc";
            }
        }

        public string LetturaFinale
        {
            get
            {
                if (lettura_finale.HasValue)
                {
                    return string.Format("{0:0}", lettura_finale) + " mc";
                }
                else
                {
                    return string.Empty;
                }
            }
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

            [DisplayName("ID")]
            public int id_contatore { get; set; }
            public int id_tipo_contatore { get; set; }
            public string flag_num_lancetta { get; set; }
            public string matricola { get; set; }
            public int numero_cifre_lancette { get; set; }
            public string numero_sigillo { get; set; }
            public System.DateTime data_istallazione_presa_incarico { get; set; }
            public decimal lettura_iniziale { get; set; }
            public System.DateTime data_cessazione { get; set; }
            public decimal lettura_finale { get; set; }
            public string flag_singolo_generale { get; set; }
            public string flag_istallazione { get; set; }
            public string flag_autoclave { get; set; }
            public string flag_ubicazione { get; set; }
            public string descr_ubicazione { get; set; }
            public System.DateTime data_penultima_lettura { get; set; }
            public decimal penultima_lettura { get; set; }
            public System.DateTime data_ultima_lettura { get; set; }
            public decimal ultima_lettura { get; set; }
            public decimal num_giorni_ultimo_addebito { get; set; }
            public decimal qta_ultimo_addebito { get; set; }
            public decimal prodie_ultimo_addebito { get; set; }
            public decimal num_giorni_totali { get; set; }
            public decimal qta_totale { get; set; }
            public decimal prodie_totale { get; set; }
            public int id_stato { get; set; }
            public string cod_stato { get; set; }
            public System.DateTime data_stato { get; set; }
            public int id_struttura_stato { get; set; }
            public int id_risorsa_stato { get; set; }
        }
    }
}
