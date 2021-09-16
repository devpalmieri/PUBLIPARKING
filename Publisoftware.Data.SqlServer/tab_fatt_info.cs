//------------------------------------------------------------------------------
// <auto-generated>
//     Codice generato da un modello.
//
//     Le modifiche manuali a questo file potrebbero causare un comportamento imprevisto dell'applicazione.
//     Se il codice viene rigenerato, le modifiche manuali al file verranno sovrascritte.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Publisoftware.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tab_fatt_info
    {
        public int id_tab_fatt_info { get; set; }
        public Nullable<int> id_lista { get; set; }
        public Nullable<int> id_avviso { get; set; }
        public Nullable<int> id_unita_contribuzione { get; set; }
        public Nullable<int> id_fatt_cont { get; set; }
        public Nullable<int> id_fatt_consumi { get; set; }
        public Nullable<decimal> id_contribuente { get; set; }
        public string cod_fiscale { get; set; }
        public string p_iva { get; set; }
        public string codice_univoco { get; set; }
        public string flag_contribuente_referente { get; set; }
        public Nullable<decimal> id_oggetto { get; set; }
        public Nullable<decimal> id_oggetto_contribuzione { get; set; }
        public Nullable<int> num_componenti { get; set; }
        public string macrocategoria { get; set; }
        public string descrizione_categoria { get; set; }
        public string flag_impianto_depurazione_ente { get; set; }
        public Nullable<decimal> deposito_cauzionale { get; set; }
        public string tipo_fatturazione { get; set; }
        public Nullable<System.DateTime> periodo_fatturazione_da { get; set; }
        public Nullable<System.DateTime> periodo_fatturazione_a { get; set; }
        public Nullable<decimal> somma_consumo_periodo { get; set; }
        public Nullable<decimal> somma_consumo_conguaglio { get; set; }
        public Nullable<decimal> somma_importo_quota_fissa { get; set; }
        public Nullable<decimal> somma_importo_acquedotto { get; set; }
        public Nullable<decimal> somma_importo_fognatura { get; set; }
        public Nullable<decimal> somma_importo_depurazione { get; set; }
        public Nullable<decimal> somma_importo_iva { get; set; }
        public Nullable<decimal> somma_importo_manutenzione { get; set; }
        public Nullable<decimal> somma_oneri_perequazione { get; set; }
        public Nullable<decimal> somma_acconti_precedenti { get; set; }
        public Nullable<decimal> componente_tariffaria_negativa { get; set; }
        public Nullable<decimal> addebiti_accrediti_diversi { get; set; }
        public Nullable<decimal> totale_lordo { get; set; }
        public string flag_pagamenti_precedenti_regolari_non_regolari { get; set; }
        public string flag_grafico_on_off { get; set; }
        public string flag_fatturazione_elettronica { get; set; }
        public string flag_split_payment { get; set; }
        public string flag_invio_mail { get; set; }
        public string flag_invio_mail_cartaceo { get; set; }
        public string flag_tipo_stampa_invio { get; set; }
        public Nullable<int> id_contatore { get; set; }
        public string matricola_contatore { get; set; }
        public Nullable<int> id_lettura_fatturazione_1 { get; set; }
        public Nullable<decimal> qta_lettura_fatturazione_1 { get; set; }
        public Nullable<System.DateTime> data_lettura_fatturazione_1 { get; set; }
        public Nullable<int> id_lettura_fatturazione_2 { get; set; }
        public Nullable<decimal> qta_lettura_fatturazione_2 { get; set; }
        public Nullable<System.DateTime> data_lettura_fatturazione_2 { get; set; }
        public Nullable<int> id_lettura_1 { get; set; }
        public Nullable<decimal> qta_lettura_1 { get; set; }
        public Nullable<System.DateTime> data_lettura_1 { get; set; }
        public string tipo_lettura_1 { get; set; }
        public Nullable<int> id_lettura_2 { get; set; }
        public Nullable<decimal> qta_lettura_2 { get; set; }
        public Nullable<System.DateTime> data_lettura_2 { get; set; }
        public string tipo_lettura_2 { get; set; }
        public Nullable<int> id_lettura_3 { get; set; }
        public Nullable<decimal> qta_lettura_3 { get; set; }
        public Nullable<System.DateTime> data_lettura_3 { get; set; }
        public string tipo_lettura_3 { get; set; }
        public Nullable<int> id_lettura_4 { get; set; }
        public Nullable<decimal> qta_lettura_4 { get; set; }
        public Nullable<System.DateTime> data_lettura_4 { get; set; }
        public string tipo_lettura_4 { get; set; }
        public Nullable<int> id_lettura_5 { get; set; }
        public Nullable<decimal> qta_lettura_5 { get; set; }
        public Nullable<System.DateTime> data_lettura_5 { get; set; }
        public string tipo_lettura_5 { get; set; }
        public Nullable<int> id_lettura_6 { get; set; }
        public Nullable<decimal> qta_lettura_6 { get; set; }
        public Nullable<System.DateTime> data_lettura_6 { get; set; }
        public string tipo_lettura_6 { get; set; }
        public Nullable<int> num_giorni_1_2 { get; set; }
        public Nullable<int> num_giorni_2_3 { get; set; }
        public Nullable<int> num_giorni_3_4 { get; set; }
        public Nullable<int> num_giorni_4_5 { get; set; }
        public Nullable<int> num_giorni_5_6 { get; set; }
        public Nullable<decimal> prodie_1_2 { get; set; }
        public Nullable<decimal> prodie_2_3 { get; set; }
        public Nullable<decimal> prodie_3_4 { get; set; }
        public Nullable<decimal> prodie_4_5 { get; set; }
        public Nullable<decimal> prodie_5_6 { get; set; }
        public Nullable<int> id_bonus { get; set; }
        public string flag_bonus { get; set; }
        public Nullable<decimal> importo_bonus { get; set; }
        public Nullable<System.DateTime> data_preavviso_scadenza_bonus { get; set; }
        public Nullable<System.DateTime> data_inizio_bonus { get; set; }
        public Nullable<System.DateTime> data_fine_bonus { get; set; }
        public string email { get; set; }
        public string pec { get; set; }
        public string numero_fattura_elettronica { get; set; }
        public string annotazioni_avviso { get; set; }
        public Nullable<int> id_stato { get; set; }
        public string cod_stato { get; set; }
        public System.DateTime data_stato { get; set; }
        public int id_struttura_stato { get; set; }
        public int id_risorsa_stato { get; set; }
    
        ///<summary><para>Relazione: FK_tab_fatt_info_tab_agevolazioni</para> Chiave Origine: id_bonus</summary>
     public virtual tab_agevolazioni tab_agevolazioni { get; set; }
        ///<summary><para>Relazione: FK_tab_fatt_info_tab_avv_pag</para> Chiave Origine: id_avviso</summary>
     public virtual tab_avv_pag tab_avv_pag { get; set; }
        ///<summary><para>Relazione: FK_tab_fatt_info_tab_contatore</para> Chiave Origine: id_contatore</summary>
     public virtual tab_contatore tab_contatore { get; set; }
        ///<summary><para>Relazione: FK_tab_fatt_info_tab_contribuente</para> Chiave Origine: id_contribuente</summary>
     public virtual tab_contribuente tab_contribuente { get; set; }
        ///<summary><para>Relazione: FK_tab_fatt_info_tab_fatt_consumi</para> Chiave Origine: id_fatt_consumi</summary>
     public virtual tab_fatt_consumi tab_fatt_consumi { get; set; }
        ///<summary><para>Relazione: FK_tab_fatt_info_tab_fatt_cont</para> Chiave Origine: id_fatt_cont</summary>
     public virtual tab_fatt_cont tab_fatt_cont { get; set; }
        ///<summary><para>Relazione: FK_tab_fatt_info_tab_letture</para> Chiave Origine: id_lettura_1</summary>
     public virtual tab_letture tab_letture { get; set; }
        ///<summary><para>Relazione: FK_tab_fatt_info_tab_letture1</para> Chiave Origine: id_lettura_2</summary>
     public virtual tab_letture tab_letture1 { get; set; }
        ///<summary><para>Relazione: FK_tab_fatt_info_tab_letture2</para> Chiave Origine: id_lettura_3</summary>
     public virtual tab_letture tab_letture2 { get; set; }
        ///<summary><para>Relazione: FK_tab_fatt_info_tab_letture3</para> Chiave Origine: id_lettura_4</summary>
     public virtual tab_letture tab_letture3 { get; set; }
        ///<summary><para>Relazione: FK_tab_fatt_info_tab_letture4</para> Chiave Origine: id_lettura_5</summary>
     public virtual tab_letture tab_letture4 { get; set; }
        ///<summary><para>Relazione: FK_tab_fatt_info_tab_letture5</para> Chiave Origine: id_lettura_6</summary>
     public virtual tab_letture tab_letture5 { get; set; }
        ///<summary><para>Relazione: FK_tab_fatt_info_tab_letture6</para> Chiave Origine: id_lettura_fatturazione_1</summary>
     public virtual tab_letture tab_letture6 { get; set; }
        ///<summary><para>Relazione: FK_tab_fatt_info_tab_letture7</para> Chiave Origine: id_lettura_fatturazione_2</summary>
     public virtual tab_letture tab_letture7 { get; set; }
        ///<summary><para>Relazione: FK_tab_fatt_info_tab_liste</para> Chiave Origine: id_lista</summary>
     public virtual tab_liste tab_liste { get; set; }
        ///<summary><para>Relazione: FK_tab_fatt_info_tab_oggetti</para> Chiave Origine: id_oggetto</summary>
     public virtual tab_oggetti tab_oggetti { get; set; }
        ///<summary><para>Relazione: FK_tab_fatt_info_tab_oggetti_contribuzione</para> Chiave Origine: id_oggetto_contribuzione</summary>
     public virtual tab_oggetti_contribuzione tab_oggetti_contribuzione { get; set; }
        ///<summary><para>Relazione: FK_tab_fatt_info_tab_unita_contribuzione</para> Chiave Origine: id_unita_contribuzione</summary>
     public virtual tab_unita_contribuzione tab_unita_contribuzione { get; set; }
    }
}