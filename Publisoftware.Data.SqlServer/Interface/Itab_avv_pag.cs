using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public interface Itab_avv_pag
    {
        int id_tab_avv_pag { get; set; }
        int id_ente { get; set; }
        int id_ente_gestito { get; set; }
        Nullable<int> id_contratto { get; set; }
        int id_entrata { get; set; }
        Nullable<decimal> id_contribuente_old { get; set; }
        decimal id_anag_contribuente { get; set; }
        int id_tipo_avvpag { get; set; }
        int id_stato_avv_pag { get; set; }
        string cod_stato_avv_pag { get; set; }
        System.DateTime dt_stato_avvpag { get; set; }
        Nullable<System.DateTime> dt_emissione { get; set; }
        string anno_riferimento { get; set; }
        Nullable<System.DateTime> periodo_riferimento_da { get; set; }
        Nullable<System.DateTime> periodo_riferimento_a { get; set; }
        Nullable<int> id_tab_contr_doc { get; set; }
        string numero_avv_pag { get; set; }
        string barcode { get; set; }
        string flag_spedizione_notifica { get; set; }
        Nullable<int> id_tipo_spedizione_notifica { get; set; }
        string tipo_spedizione_notifica { get; set; }
        string numero_raccomandata { get; set; }
        string flag_iter_recapito_notifica { get; set; }
        string flag_esito_sped_notifica { get; set; }
        Nullable<int> id_tipo_esito_notifica { get; set; }
        string tipo_esito_notifica { get; set; }
        Nullable<System.DateTime> data_avvenuta_notifica { get; set; }
        Nullable<int> id_notificatore { get; set; }
        Nullable<System.DateTime> dt_scadenza_not { get; set; }
        Nullable<System.DateTime> data_ricezione { get; set; }
        Nullable<System.DateTime> data_affissione_ap { get; set; }
        Nullable<System.DateTime> data_notifica_avvdep { get; set; }
        string esito_notifica_avvdep { get; set; }
        Nullable<decimal> imp_tot_avvpag { get; set; }
        Nullable<decimal> imp_imp_entr_avvpag { get; set; }
        Nullable<decimal> imp_esente_iva_avvpag { get; set; }
        Nullable<decimal> imp_iva_avvpag { get; set; }
        Nullable<decimal> imp_tot_spese_avvpag { get; set; }
        Nullable<decimal> imp_spese_notifica { get; set; }
        Nullable<decimal> imp_tot_pagato { get; set; }
        Nullable<decimal> importo_tot_da_pagare { get; set; }
        Nullable<decimal> imp_tot_avvpag_rid { get; set; }
        string flag_rateizzazione { get; set; }
        Nullable<System.DateTime> data_rateizzazione { get; set; }
        Nullable<decimal> imp_rateizzato { get; set; }
        Nullable<int> periodicita_rate { get; set; }
        Nullable<int> num_rate { get; set; }
        Nullable<System.DateTime> data_scadenza_1_rata { get; set; }
        string flag_rateizzazione_bis { get; set; }
        Nullable<System.DateTime> data_rateizzazione_bis { get; set; }
        Nullable<decimal> imp_rateizzato_bis { get; set; }
        Nullable<int> periodicita_rate_bis { get; set; }
        Nullable<int> num_rate_bis { get; set; }
        Nullable<System.DateTime> data_scadenza_1_rata_bis { get; set; }
        string flag_adesione { get; set; }
        Nullable<System.DateTime> data_adesione { get; set; }
        string flag_riemissione { get; set; }
        Nullable<int> num_avv_riemesso { get; set; }
        Nullable<int> id_risorsa { get; set; }
        Nullable<System.DateTime> dt_avv_pag { get; set; }
        string fonte_emissione { get; set; }
        Nullable<int> id_lista_emissione { get; set; }
        Nullable<int> id_lista_carico { get; set; }
        Nullable<int> id_lista_scarico { get; set; }
        string flag_carico { get; set; }
        string flag_scarico { get; set; }
        int id_stato { get; set; }
        string cod_stato { get; set; }
        System.DateTime data_stato { get; set; }
        int id_struttura_stato { get; set; }
        int id_risorsa_stato { get; set; }
        Nullable<decimal> importo_ridotto { get; set; }
        string identificativo_avv_pag { get; set; }
        string testo1 { get; set; }
        string testo2 { get; set; }
        string testo3 { get; set; }
        string testo4 { get; set; }
    }
}
