using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_avv_pag_fatt_emissione_light : BaseEntity<tab_avv_pag_fatt_emissione_light>
    {
        public string NumeroIstanza { get; set; }
        public string TipoIstanza { get; set; }
        public string DataPresentazione { get; set; }

        public int id_tab_avv_pag { get; set; }
        public int id_tipo_avvpag { get; set; }
        public string Identificativo { get; set; }
        public string NumeroAvviso { get; set; }
        public string dt_emissione_String { get; set; }
        public DateTime? dt_emissione { get; set; }
        public decimal imp_tot_avvpag_rid { get; set; }
        public decimal imp_tot_avvpag_Euro { get; set; }
        public string Importo { get; set; }
        public decimal importoSpeseNotificaDecimal { get; set; }
        public decimal importoSpeseCoattiveDecimal { get; set; }
        public string ImportoSpeseNotifica { get; set; }
        public string ImportoSpeseCoattive { get; set; }
        public string Rate { get; set; }
        public string Targa { get; set; }
        public string SpeditoNotificato { get; set; }
        public decimal importo_tot_da_pagare { get; set; }
        public decimal importo_sanzioni_eliminate_eredi { get; set; }
        public string importo_sanzioni_eliminate_eredi_Euro { get; set; }
        public string imp_tot_pagato_Euro { get; set; }
        public decimal imp_tot_pagato { get; set; }
        public string ImportoDaPagare { get; set; }
        public string stato { get; set; }
        public string cod_stato { get; set; }
        public string codStatoReale { get; set; }
        public string Adesione { get; set; }
        public string TipoBene { get; set; }
        public int id_tab_supervisione_finale { get; set; }
        public string Atti { get; set; }
        public string IntimazioneCorrelata { get; set; }
        public string impRidottoPerAdesione { get; set; }
        public string imponibile { get; set; }
        public string iva { get; set; }
        public int id_avvpag_preavviso_collegato { get; set; }
        public bool IsIstanzaVisibile { get; set; }
        public bool ExistsAtti { get; set; }
        public bool ExistsAttiIntimSoll { get; set; }
        public bool ExistsAttiCoattivi { get; set; }
        public bool ExistsAttiSuccessivi { get; set; }
        public bool ExistsIspezioni { get; set; }
        public bool IsFatturazioneAcqua { get; set; }
        public bool ExistsOrdineOrigine { get; set; }
        public bool ExistsProceduraConcorsuale { get; set; }
        public int IdAvvPagPreColl { get; set; }
        public string soggettoDebitore { get; set; }
        public string soggettoDebitoreTerzo { get; set; }
        public string Contribuente { get; set; }
        public bool IsIstanzaPresentabile { get; set; }
        public string dataMassimaPresentazioneIstanza { get; set; }
        public bool IsAvvisoPagabile { get; set; }
        public string IsVisibleBene { get; set; }
        public string IsVisibleAtti { get; set; }
        public string IsVisibleAcqua { get; set; }
        public string nome { get; set; }
        public string cognome { get; set; }
        public string codice_fiscale { get; set; }
        public string p_iva { get; set; }
        public string ragione_sociale { get; set; }
        public string color { get; set; }
        public string identificativo_avv_pag { get; set; }
        public string flag_spedizione_notifica { get; set; }
        public bool IsAvvisoSgravabile { get; set; }
        public bool IsAvvisoStatoAnnRetDanDar { get; set; }
        public bool IsAvvisoStatoAnnDan { get; set; }
        public bool IsAvvisoStatoAnnDanDar { get; set; }
        public bool IsProvvedimentoPresentabile { get; set; }
        public string dataMassimaPagamentoAvviso { get; set; }
        public string data_ricezione_String { get; set; }
        public DateTime? data_ricezione { get; set; }
        public string avvisoBonario { get; set; }
        public decimal? importo_sgravio { get; set; }
        public string importo_sgravio_Euro { get; set; }
        public string DescrizioneTipoAvviso { get; set; }
        public string SpeditoRicezione { get; set; }
        public decimal? ImportoAttiSuccessivi { get; set; }
        public string ImportoAttiSuccessivi_Euro { get; set; }
        public decimal interessi_eliminati_definizione_agevolata { get; set; }
        public string interessi_eliminati_definizione_agevolata_Euro { get; set; }
        public decimal sanzioni_eliminate_definizione_agevolata { get; set; }
        public string sanzioni_eliminate_definizione_agevolata_Euro { get; set; }
        public decimal importo_definizione_agevolata_eredi_decimal { get; set; }
        public string importo_definizione_agevolata_eredi_Euro { get; set; }
    }
}
