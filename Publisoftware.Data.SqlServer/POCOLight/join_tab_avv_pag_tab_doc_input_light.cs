using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class join_tab_avv_pag_tab_doc_input_light : BaseEntity<join_tab_avv_pag_tab_doc_input_light>
    {
        public int id_join_avv_pag_doc_input { get; set; }
        public int? id_join_avv_pag_doc_input_collegata { get; set; }
        public string StatoJoin { get; set; }
        public string StatoJoin1 { get; set; }
        public int IdStatoJoin { get; set; }
        public int IdStatoJoin1 { get; set; }
        public string CodStatoJoin { get; set; }
        public string CodStatoJoin1 { get; set; }
        public string risorsaLavorazioneJoin { get; set; }
        public string Causale { get; set; }
        public int IdCausale { get; set; }

        public string TrattamentoCausale { get; set; }
        public string SiglaCausale { get; set; }

        public int id_tab_doc_input { get; set; }
        public string NumeroIstanza { get; set; }
        public string TipoIstanza { get; set; }
        public string DataPresentazione { get; set; }
        public string identificativo_doc_input { get; set; }
        public string contribuente_nominativo { get; set; }
        public string statoIstanza { get; set; }
        public string risorsaAcquisizione { get; set; }
        public string risorsaLavorazione { get; set; }
        public string risorsaControllo { get; set; }
        public string statoDoc { get; set; }
        public string terzo_nominativo { get; set; }
        public int idStatoIstanza { get; set; }
        public string tipoRateizzazione { get; set; }

        public int id_tab_avv_pag { get; set; }
        public string TipoAvviso { get; set; }
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
        public string ImportoDaPagare { get; set; }
        public int idStato { get; set; }
        public string stato { get; set; }
        public string cod_stato { get; set; }
        public string codStatoReale { get; set; }
        public string Adesione { get; set; }
        public string TipoBene { get; set; }
        public int id_tab_supervisione_finale { get; set; }
        public string IntimazioneCorrelata { get; set; }
        public string impRidottoPerAdesione { get; set; }
        public int id_avvpag_preavviso_collegato { get; set; }
        public bool IsIstanzaVisibile { get; set; }
        public bool ExistsAtti { get; set; }
        public bool ExistsAttiIntimSoll { get; set; }
        public bool ExistsAttiCoattivi { get; set; }
        public string soggettoDebitore { get; set; }
        public string soggettoDebitoreTerzo { get; set; }
        public string Contribuente { get; set; }
        public string FonteEmissione { get; set; }

        public int id_tab_avv_pag1 { get; set; }
        public string TipoAvviso1 { get; set; }
        public string NumeroAvviso1 { get; set; }
        public string dt_emissione_String1 { get; set; }
        public DateTime? dt_emissione1 { get; set; }
        public decimal imp_tot_avvpag_rid1 { get; set; }
        public string Importo1 { get; set; }
        public decimal importoSpeseNotificaDecimal1 { get; set; }
        public decimal importoSpeseCoattiveDecimal1 { get; set; }
        public string ImportoSpeseNotifica1 { get; set; }
        public string ImportoSpeseCoattive1 { get; set; }
        public string Rate1 { get; set; }
        public string SpeditoNotificato1 { get; set; }
        public decimal imp_tot_pagato1 { get; set; }
        public string imp_tot_pagato_Euro1 { get; set; }
        public decimal importo_tot_da_pagare1 { get; set; }
        public string ImportoDaPagare1 { get; set; }
        public int idStato1 { get; set; }
        public string stato1 { get; set; }
        public string cod_stato1 { get; set; }
        public string codStatoReale1 { get; set; }
        public string Adesione1 { get; set; }
        public string TipoBene1 { get; set; }
        public int id_tab_supervisione_finale1 { get; set; }
        public string IntimazioneCorrelata1 { get; set; }
        public string impRidottoPerAdesione1 { get; set; }
        public int id_avvpag_preavviso_collegato1 { get; set; }
        public bool IsIstanzaVisibile1 { get; set; }
        public bool ExistsAtti1 { get; set; }
        public bool ExistsAttiIntimSoll1 { get; set; }
        public bool ExistsAttiCoattivi1 { get; set; }
        public string soggettoDebitore1 { get; set; }
        public string soggettoDebitoreTerzo1 { get; set; }
        public string Contribuente1 { get; set; }
        public string FonteEmissione1 { get; set; }
        public bool isSgravato1 { get; set; }
        public bool isMotivazioniAvvisoLavorate1 { get; set; }
        public bool isConsegnabileSportello { get; set; }

        public int id_tab_avv_pag_fatt_emissione { get; set; }
    }
}
