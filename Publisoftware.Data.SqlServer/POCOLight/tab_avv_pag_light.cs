using Publisoftware.Utility.SecureString;
using Publisoftware.Utility.Stringa;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_avv_pag_light : BaseEntity<tab_avv_pag_light>
    {
        public string NumeroIstanza { get; set; }
        public string TipoIstanza { get; set; }
        public string DataPresentazione { get; set; }

        public int id_tab_avv_pag { get; set; }

        public string id_tab_avv_pag_sec
        {
            get
            {
                if (id_tab_avv_pag > 0)
                {
                    return EncryptionHelper.Encrypt(id_tab_avv_pag.ToString());
                }
                else
                {
                    return "";
                }
            }
        }
        public int id_tipo_avvpag { get; set; }
        public string Identificativo { get; set; }
        public string NumeroAvviso { get; set; }
        public string NumeroAvvisoRettificato { get; set; }
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

        public decimal id_anag_contribuente { get; set; }
        public string FonteContribuente { get; set; }
        public string RagSocialeContribuente { get; set; }
        public string SpeditoNotificato { get; set; }
        public string SpeditoRicezione { get; set; }
        public decimal importo_tot_da_pagare { get; set; }
        public decimal importo_sanzioni_eliminate_eredi { get; set; }
        public string importo_sanzioni_eliminate_eredi_Euro { get; set; }
        public decimal ImportoDaPagareDefAgevDecimal { get; set; }
        public string ImportoDaPagareDefAgev { get; set; }
        public string imp_tot_pagato_Euro { get; set; }
        public decimal imp_tot_pagato { get; set; }
        public string ImportoDaPagare { get; set; }
        public decimal importoInteressiDecimal { get; set; }
        public decimal importoSanzioniDecimal { get; set; }
        public string ImportoInteressi { get; set; }
        public string ImportoSanzioni { get; set; }
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
        public bool ExistsOrdineOrigine { get; set; }
        public bool ExistsProceduraConcorsuale { get; set; }
        public bool IsFatturazioneAcqua { get; set; }
        public int IdAvvPagPreColl { get; set; }
        public string soggettoDebitore { get; set; }
        public string soggettoDebitoreTerzo { get; set; }
        public string Contribuente { get; set; }
        public bool IsIstanzaPresentabile { get; set; }
        public bool IsAttoSuccessivo { get; set; }
        public string AttoSuccessivo { get; set; }
        public bool IsAlreadyPratica { get; set; }
        public string dataMassimaPresentazioneIstanza { get; set; }
        public bool IsAvvisoPagabile { get; set; }
        public string dataMassimaPagamentoAvviso { get; set; }
        public string data_ricezione_String { get; set; }
        public DateTime? data_ricezione { get; set; }
        public string avvisoBonario { get; set; }
        public bool IsProvvedimentoPresentabile { get; set; }
        public decimal? importo_sgravio { get; set; }
        public string importo_sgravio_Euro { get; set; }
        public string color { get; set; }
        public bool IsAvvisoSgravabile { get; set; }
        public bool IsAvvisoStatoAnnRetDanDar { get; set; }
        public bool IsAvvisoStatoAnnDan { get; set; }
        public bool IsAvvisoStatoAnnDanDar { get; set; }
        public int IdTipoAvviso { get; set; }
        public string Entrata { get; set; }
        public string TipoAvviso { get; set; }
        public bool IsAttoComposto { get; set; }
        public decimal interessi_eliminati_definizione_agevolata { get; set; }
        public string interessi_eliminati_definizione_agevolata_Euro { get; set; }
        public decimal sanzioni_eliminate_definizione_agevolata { get; set; }
        public string sanzioni_eliminate_definizione_agevolata_Euro { get; set; }
        public decimal importo_definizione_agevolata_eredi_decimal { get; set; }
        public string importo_definizione_agevolata_eredi_Euro { get; set; }
        public decimal importo_ridotto_decimal { get; set; }
        public string importo_ridotto_Euro { get; set; }

        public string Descr_Rate { get; set; }
        private string _has_rate_pagate = "0";

        [DefaultValue("0")]
        public string HasRatePagate
        {
            get
            {
                return _has_rate_pagate;
            }
            set
            {
                _has_rate_pagate = value;
            }
        }
        public string CodiceTipoAvviso { get; set; }
        public string DescrizioneTipoAvviso { get; set; }

        public bool FlagImportoAttiSuccessivi { get; set; }

        public decimal? ImportoAttiSuccessivi { get; set; }
        public string ImportoAttiSuccessivi_Euro { get; set; }
        public string IsRateizzato { get; set; }
        public string flag_spedizione_notifica { get; set; }
        public string IsVisibleBene { get; set; }
        public string IsVisibleAtti { get; set; }
        public string IsVisibleAcqua { get; set; }

        public bool InPagamento { get; set; }

        public bool HasImportoRidotto { get; set; }

        public decimal ImportoRidotto { get; set; }

        public string ImportoRidotto_Euro
        {
            get
            {
                return ImportoRidotto.ToString("C");
            }
        }
        //---------------------------------------------------
    }
    public class tab_avv_pag_light_exp : BaseEntity<tab_avv_pag_light_exp>
    {

        public string DataPresentazione { get; set; }

        public int IDAvviso { get; set; }
        public int IDTipoAvviso { get; set; }
        public string Identificativo { get; set; }
        public string NumeroAvviso { get; set; }
        public DateTime? Data_Emissione { get; set; }
        public decimal? Importo { get; set; }
        public string SpeditoNotificato { get; set; }
        public string stato { get; set; }
        public string Contribuente { get; set; }
        public DateTime? Data_Ricezione_Notifica { get; set; }
        public DateTime? Data_Avvenuta_Notifica { get; set; }
    }
}
