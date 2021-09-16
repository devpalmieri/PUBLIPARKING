using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(join_avv_pag_mov_pag.Metadata))]
    public partial class join_avv_pag_mov_pag : ISoftDeleted, IGestioneStato
    {
        public const string ANN = "ANN-";
        public const string ANN_ANN = "ANN-ANN";
        public const string ANN_CON = "ANN-CON";
        public const string ACC_ACC = "ACC-ACC";
        public const string ACC_CON = "ACC-CON";
        public const string RET_ACC = "RET-ACC";
        public const string ACC = "ACC";
        public const string _ACC = "-ACC";
        public const string _CON = "-CON";

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public string importo_pagato_Euro
        {
            get
            {
                return importo_pagato.ToString("C");
            }
        }

        [DisplayName("Data Accredito")]
        public string data_oper_acc_String
        {
            get
            {
                if (data_oper_acc.HasValue)
                {
                    return data_oper_acc.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string AvvisoPagato
        {
            get
            {
                if (tab_avv_pag != null)
                {
                    return tab_avv_pag.anagrafica_tipo_avv_pag.TipoAvviso + " - " + tab_avv_pag.identificativo_avv_pag;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string AvvisoAccreditato
        {
            get
            {
                if (tab_avv_pag1 != null)
                {
                    return tab_avv_pag1.anagrafica_tipo_avv_pag.TipoAvviso + " - " + tab_avv_pag1.identificativo_avv_pag;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string AvvisoStornato
        {
            get
            {
                if (tab_avv_pag2!= null)
                {
                    return tab_avv_pag2.anagrafica_tipo_avv_pag.TipoAvviso + " - " + tab_avv_pag2.identificativo_avv_pag;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string NumeroCC
        {
            get
            {
                if (tab_mov_pag != null && tab_mov_pag.tab_cc_riscossione != null)
                {
                    return tab_mov_pag.tab_cc_riscossione.num_cc;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string SoggettoDebitore
        {
            get
            {
                if (tab_avv_pag.join_avv_pag_soggetto_debitore.Any(d => d.join_referente_contribuente != null && d.id_terzo_debitore != null && d.cod_stato.Equals(join_avv_pag_soggetto_debitore.ATT_ATT)))
                {
                    join_avv_pag_soggetto_debitore v_joinAvvPaggSoggDeb = tab_avv_pag.join_avv_pag_soggetto_debitore.Where(d => d.join_referente_contribuente != null && d.id_terzo_debitore != null && d.cod_stato.Equals(join_avv_pag_soggetto_debitore.ATT_ATT)).FirstOrDefault();
                    return "Soggetto debitore: " + v_joinAvvPaggSoggDeb.tab_terzo.terzoNominativoDisplay +
                           " terzo debitore di " + v_joinAvvPaggSoggDeb.join_referente_contribuente.tab_referente.referenteDisplay +
                           ", referente del contribuente " + tab_avv_pag.tab_contribuente.contribuenteDisplay +
                           " in qualità di " + v_joinAvvPaggSoggDeb.join_referente_contribuente.anagrafica_tipo_relazione.desc_tipo_relazione;
                }
                else if (tab_avv_pag.join_avv_pag_soggetto_debitore.Any(d => d.join_referente_contribuente != null && d.cod_stato.Equals(join_avv_pag_soggetto_debitore.ATT_ATT)))
                {
                    join_avv_pag_soggetto_debitore v_joinAvvPaggSoggDeb = tab_avv_pag.join_avv_pag_soggetto_debitore.Where(d => d.join_referente_contribuente != null && d.cod_stato.Equals(join_avv_pag_soggetto_debitore.ATT_ATT)).FirstOrDefault();
                    return "Soggetto debitore: " + v_joinAvvPaggSoggDeb.join_referente_contribuente.tab_referente.referenteDisplay +
                           " in qualità di " + v_joinAvvPaggSoggDeb.join_referente_contribuente.anagrafica_tipo_relazione.desc_tipo_relazione +
                           " del contribuente " + tab_avv_pag.tab_contribuente.contribuenteDisplay;
                }
                else if (tab_avv_pag.join_avv_pag_soggetto_debitore.Any(d => d.id_terzo_debitore != null && d.cod_stato.Equals(join_avv_pag_soggetto_debitore.ATT_ATT)))
                {
                    join_avv_pag_soggetto_debitore v_joinAvvPaggSoggDeb = tab_avv_pag.join_avv_pag_soggetto_debitore.Where(d => d.id_terzo_debitore != null && d.cod_stato.Equals(join_avv_pag_soggetto_debitore.ATT_ATT)).FirstOrDefault();
                    return "Soggetto debitore: " + v_joinAvvPaggSoggDeb.tab_terzo.terzoNominativoDisplay +
                           " terzo debitore del contribuente " + tab_avv_pag.tab_contribuente.contribuenteDisplay;
                }
                else
                {
                    //return String.Concat("Contribuente: ", tab_avv_pag.tab_contribuente.contribuenteDisplay);
                    return string.Empty;
                }
            }
            
        }
        public string CF_PIvaEnte_Dominio_PagoPA { get; set; }
        public int Id_Rata_PagoPA { get; set; }
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

        }
    }
}
