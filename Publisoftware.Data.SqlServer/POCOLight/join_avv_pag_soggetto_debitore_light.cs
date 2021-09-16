using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class join_avv_pag_soggetto_debitore_light : BaseEntity<join_avv_pag_soggetto_debitore_light>
    {
        public int id_join_avv_pag_soggetto_debitore { get; set; }

        public int id_tab_avv_pag { get; set; }
        public string NumeroAvviso { get; set; }
        public string dt_emissione_String { get; set; }
        public DateTime? dt_emissione { get; set; }
        public decimal imp_tot_avvpag_rid { get; set; }
        public string Importo { get; set; }
        public decimal importoSpeseNotificaDecimal { get; set; }
        public decimal importoSpeseCoattiveDecimal { get; set; }
        public string ImportoSpeseNotifica { get; set; }
        public string ImportoSpeseCoattive { get; set; }
        public string Rate { get; set; }
        public string Targa { get; set; }
        public string SpeditoNotificato { get; set; }
        public decimal imp_tot_pagato { get; set; }
        public string imp_tot_pagato_Euro { get; set; }
        public decimal importo_tot_da_pagare { get; set; }
        public decimal importo_sanzioni_eliminate_eredi { get; set; }
        public string importo_sanzioni_eliminate_eredi_Euro { get; set; }
        public string ImportoDaPagare { get; set; }
        public string stato { get; set; }
        public string cod_stato { get; set; }
        public string codStatoReale { get; set; }
        public string Adesione { get; set; }
        public string TipoBene { get; set; }
        public int id_tab_supervisione_finale { get; set; }
        public string IntimazioneCorrelata { get; set; }
        public string impRidottoPerAdesione { get; set; }
        public string imponibile { get; set; }
        public string iva { get; set; }
        public int id_avvpag_preavviso_collegato { get; set; }
        public bool IsIstanzaVisibile { get; set; }
        public bool ExistsAtti { get; set; }
        public bool ExistsAttiIntimSoll { get; set; }
        public bool ExistsAttiCoattivi { get; set; }
        public bool ExistsOrdineOrigine { get; set; }
        public bool IsFatturazioneAcqua { get; set; }
        public int IdAvvPagPreColl { get; set; }
        public string soggettoDebitore { get; set; }
        public string soggettoDebitoreTerzo { get; set; }
        public string Contribuente { get; set; }
        public decimal interessi_eliminati_definizione_agevolata { get; set; }
        public string interessi_eliminati_definizione_agevolata_Euro { get; set; }
        public decimal sanzioni_eliminate_definizione_agevolata { get; set; }
        public string sanzioni_eliminate_definizione_agevolata_Euro { get; set; }
        public decimal importo_definizione_agevolata_eredi_decimal { get; set; }
        public string importo_definizione_agevolata_eredi_Euro { get; set; }
        public decimal? ImportoAttiSuccessivi { get; set; }
        public string ImportoAttiSuccessivi_Euro { get; set; }
        public int id_tipo_avvpag { get; set; }
        public string flag_spedizione_notifica { get; set; }
        public string IsVisibleBene { get; set; }
        public string IsVisibleAtti { get; set; }
        public string IsVisibleAcqua { get; set; }
        public bool ExistsAttiSuccessivi { get; set; }
        public bool ExistsIspezioni { get; set; }
        public string SpeditoRicezione { get; set; }
        public string DescrizioneTipoAvviso { get; set; }
    }
}
