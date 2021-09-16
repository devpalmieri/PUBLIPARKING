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
    
    public partial class tab_avv_pag_fatt_emissione
    {
        public tab_avv_pag_fatt_emissione()
        {
            this.join_controlli_avvpag_fatt_emissione = new HashSet<join_controlli_avvpag_fatt_emissione>();
            this.tab_avvisi_anomali_liste_carico = new HashSet<tab_avvisi_anomali_liste_carico>();
            this.tab_rata_avv_pag_fatt_emissione = new HashSet<tab_rata_avv_pag_fatt_emissione>();
            this.tab_sped_not_fatt_emissione = new HashSet<tab_sped_not_fatt_emissione>();
            this.tab_sped_not = new HashSet<tab_sped_not>();
            this.join_avv_pag_fatt_emissione_soggetto_debitore = new HashSet<join_avv_pag_fatt_emissione_soggetto_debitore>();
            this.join_tab_avv_pag_tab_doc_input = new HashSet<join_tab_avv_pag_tab_doc_input>();
            this.tab_unita_contribuzione_fatt_emissione = new HashSet<tab_unita_contribuzione_fatt_emissione>();
        }
    
        public int id_tab_avv_pag { get; set; }
        public int id_ente { get; set; }
        public int id_ente_gestito { get; set; }
        public Nullable<int> id_contratto { get; set; }
        public int id_entrata { get; set; }
        public Nullable<decimal> id_contribuente_old { get; set; }
        public decimal id_anag_contribuente { get; set; }
        public int id_tipo_avvpag { get; set; }
        public int id_stato_avv_pag { get; set; }
        public string cod_stato_avv_pag { get; set; }
        public System.DateTime dt_stato_avvpag { get; set; }
        public Nullable<System.DateTime> dt_emissione { get; set; }
        public string anno_riferimento { get; set; }
        public Nullable<System.DateTime> periodo_riferimento_da { get; set; }
        public Nullable<System.DateTime> periodo_riferimento_a { get; set; }
        public Nullable<int> id_tab_contr_doc { get; set; }
        public string numero_avv_pag { get; set; }
        public string barcode { get; set; }
        public string flag_spedizione_notifica { get; set; }
        public Nullable<int> id_tipo_spedizione_notifica { get; set; }
        public string tipo_spedizione_notifica { get; set; }
        public string numero_raccomandata { get; set; }
        public string flag_iter_recapito_notifica { get; set; }
        public string flag_esito_sped_notifica { get; set; }
        public Nullable<int> id_tipo_esito_notifica { get; set; }
        public string tipo_esito_notifica { get; set; }
        public Nullable<System.DateTime> data_avvenuta_notifica { get; set; }
        public Nullable<int> id_notificatore { get; set; }
        public Nullable<System.DateTime> dt_scadenza_not { get; set; }
        public Nullable<System.DateTime> data_ricezione { get; set; }
        public Nullable<System.DateTime> data_affissione_ap { get; set; }
        public Nullable<System.DateTime> data_notifica_avvdep { get; set; }
        public string esito_notifica_avvdep { get; set; }
        public Nullable<decimal> imp_tot_avvpag { get; set; }
        public Nullable<decimal> imp_imp_entr_avvpag { get; set; }
        public Nullable<decimal> imp_esente_iva_avvpag { get; set; }
        public Nullable<decimal> imp_iva_avvpag { get; set; }
        public Nullable<decimal> imp_tot_spese_avvpag { get; set; }
        public Nullable<decimal> imp_spese_notifica { get; set; }
        public Nullable<decimal> imp_tot_pagato { get; set; }
        public Nullable<decimal> importo_tot_da_pagare { get; set; }
        public Nullable<decimal> imp_tot_avvpag_rid { get; set; }
        public string flag_rateizzazione { get; set; }
        public Nullable<System.DateTime> data_rateizzazione { get; set; }
        public Nullable<decimal> imp_rateizzato { get; set; }
        public Nullable<int> periodicita_rate { get; set; }
        public Nullable<int> num_rate { get; set; }
        public Nullable<System.DateTime> data_scadenza_1_rata { get; set; }
        public string flag_rateizzazione_bis { get; set; }
        public Nullable<System.DateTime> data_rateizzazione_bis { get; set; }
        public Nullable<decimal> imp_rateizzato_bis { get; set; }
        public Nullable<int> periodicita_rate_bis { get; set; }
        public Nullable<int> num_rate_bis { get; set; }
        public Nullable<System.DateTime> data_scadenza_1_rata_bis { get; set; }
        public string flag_adesione { get; set; }
        public Nullable<System.DateTime> data_adesione { get; set; }
        public string flag_riemissione { get; set; }
        public Nullable<int> num_avv_riemesso { get; set; }
        public Nullable<int> id_risorsa { get; set; }
        public Nullable<System.DateTime> dt_avv_pag { get; set; }
        public Nullable<int> id_lista_emissione { get; set; }
        public Nullable<int> id_lista_carico { get; set; }
        public Nullable<int> id_lista_scarico { get; set; }
        public string flag_carico { get; set; }
        public string flag_scarico { get; set; }
        public int id_stato { get; set; }
        public string cod_stato { get; set; }
        public System.DateTime data_stato { get; set; }
        public int id_struttura_stato { get; set; }
        public int id_risorsa_stato { get; set; }
        public Nullable<decimal> importo_ridotto { get; set; }
        public Nullable<int> id_tab_supervisione_finale { get; set; }
        public string fonte_emissione { get; set; }
        public string testo1 { get; set; }
        public string testo2 { get; set; }
        public string testo3 { get; set; }
        public string testo4 { get; set; }
        public string identificativo_avv_pag { get; set; }
        public string identificativo_avv_pag_ente { get; set; }
        public Nullable<decimal> importo_sanzioni_eliminate_eredi { get; set; }
        public Nullable<decimal> importo_spese_coattive_sospese_su_preavvisi { get; set; }
        public Nullable<decimal> importo_pagato_con_atti_successivi { get; set; }
        public string dati_avviso_bonario { get; set; }
        public Nullable<bool> flag_notifica_eredi_generici { get; set; }
        public Nullable<System.DateTime> data_avviso_bonario { get; set; }
        public Nullable<int> num_riga_flusso_trasmesso { get; set; }
        public Nullable<System.DateTime> data_notifica_avviso_bonario { get; set; }
        public int gg_sospensione_trasmessi { get; set; }
        public int gg_sospensione_generati { get; set; }
        public string num_provvedimento_sgravio { get; set; }
        public Nullable<System.DateTime> data_provvedimento_sgravio { get; set; }
        public Nullable<System.DateTime> data_comunicazione_sgravio { get; set; }
        public Nullable<decimal> importo_sgravio { get; set; }
        public Nullable<decimal> importo_rimborso_effettuato { get; set; }
        public Nullable<decimal> importo_rimborso_da_effettuare { get; set; }
        public Nullable<System.DateTime> data_rimborso { get; set; }
        public string flag_tipo_atto_successivo { get; set; }
        public Nullable<decimal> sanzioni_eliminate_definizione_agevolata { get; set; }
        public Nullable<decimal> interessi_eliminati_definizione_agevolata { get; set; }
        public Nullable<decimal> importo_iscrizione_ruolo { get; set; }
        public Nullable<decimal> imp_maggiorazione_onere_riscossione_61_90 { get; set; }
        public Nullable<decimal> imp_maggiorazione_onere_riscossione_121 { get; set; }
        public Nullable<decimal> imp_tot_interesse_mora_giornaliero { get; set; }
        public string flag_ricalcolo { get; set; }
    
        ///<summary><para>Relazione: FK_tab_avv_pag_fatt_emissione_anagrafica_entrate</para> Chiave Origine: id_entrata</summary>
     public virtual anagrafica_entrate anagrafica_entrate { get; set; }
        ///<summary><para>Relazione: FK_tab_avv_pag_fatt_emissione_anagrafica_risorse</para> Chiave Origine: id_risorsa_stato</summary>
     public virtual anagrafica_risorse anagrafica_risorse { get; set; }
        ///<summary><para>Relazione: FK_tab_avv_pag_fatt_emissione_anagrafica_risorse1</para> Chiave Origine: id_risorsa</summary>
     public virtual anagrafica_risorse anagrafica_risorse1 { get; set; }
        ///<summary><para>Relazione: FK_tab_avv_pag_fatt_emissione_anagrafica_stato_avv_pag</para> Chiave Origine: id_stato_avv_pag</summary>
     public virtual anagrafica_stato_avv_pag anagrafica_stato_avv_pag { get; set; }
        ///<summary><para>Relazione: FK_tab_avv_pag_fatt_emissione_anagrafica_stato_avv_pag1</para> Chiave Origine: id_stato</summary>
     public virtual anagrafica_stato_avv_pag anagrafica_stato_avv_pag1 { get; set; }
        ///<summary><para>Relazione: FK_tab_avv_pag_fatt_emissione_anagrafica_strutture_aziendali</para> Chiave Origine: id_struttura_stato</summary>
     public virtual anagrafica_strutture_aziendali anagrafica_strutture_aziendali { get; set; }
        ///<summary><para>Relazione: FK_join_controlli_avvpag_fatt_emissione_tab_avv_pag_fatt_emissione</para> Chiave Origine: id_avvpag_fatt_emissione</summary>
     public virtual ICollection<join_controlli_avvpag_fatt_emissione> join_controlli_avvpag_fatt_emissione { get; set; }
        ///<summary><para>Relazione: FK_tab_avv_pag_fatt_emissione_anagrafica_ente_gestito</para> Chiave Origine: id_ente_gestito</summary>
     public virtual anagrafica_ente_gestito anagrafica_ente_gestito { get; set; }
        ///<summary><para>Relazione: FK_tab_avv_pag_fatt_emissione_tab_avv_pag</para> Chiave Origine: num_avv_riemesso</summary>
     public virtual tab_avv_pag tab_avv_pag { get; set; }
        ///<summary><para>Relazione: FK_tab_avv_pag_fatt_emissione_TAB_SUPERVISIONE_FINALE_V2</para> Chiave Origine: id_tab_supervisione_finale</summary>
     public virtual TAB_SUPERVISIONE_FINALE_V2 TAB_SUPERVISIONE_FINALE_V2 { get; set; }
        ///<summary><para>Relazione: FK_tab_avv_pag_fatt_emissione_anagrafica_tipo_avv_pag</para> Chiave Origine: id_tipo_avvpag</summary>
     public virtual anagrafica_tipo_avv_pag anagrafica_tipo_avv_pag { get; set; }
        ///<summary><para>Relazione: FK_tab_avvisi_anomali_liste_carico_tab_avv_pag_fatt_emissione</para> Chiave Origine: id_avv_pag_fatt_emissione</summary>
     public virtual ICollection<tab_avvisi_anomali_liste_carico> tab_avvisi_anomali_liste_carico { get; set; }
        ///<summary><para>Relazione: FK_tab_rata_avv_pag_fatt_emissione_tab_avv_pag_fatt_emissione</para> Chiave Origine: id_tab_avv_pag</summary>
     public virtual ICollection<tab_rata_avv_pag_fatt_emissione> tab_rata_avv_pag_fatt_emissione { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_tab_avv_pag_fatt_emissione</para> Chiave Origine: id_avv_pag_fatt_emissione</summary>
     public virtual ICollection<tab_sped_not_fatt_emissione> tab_sped_not_fatt_emissione { get; set; }
        ///<summary><para>Relazione: FK_tab_avv_pag_fatt_emissione_tab_liste</para> Chiave Origine: id_lista_emissione</summary>
     public virtual tab_liste tab_liste { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_tab_avv_pag_fatt_emissione</para> Chiave Origine: id_avv_pag_fatt_emissione</summary>
     public virtual ICollection<tab_sped_not> tab_sped_not { get; set; }
        ///<summary><para>Relazione: FK_join_avv_pag_fatt_emissione_soggetto_debitore_tab_avv_pag_fatt_emissione</para> Chiave Origine: id_tab_avv_pag</summary>
     public virtual ICollection<join_avv_pag_fatt_emissione_soggetto_debitore> join_avv_pag_fatt_emissione_soggetto_debitore { get; set; }
        ///<summary><para>Relazione: FK_tab_avv_pag_fatt_emissione_tab_contribuente</para> Chiave Origine: id_anag_contribuente</summary>
     public virtual tab_contribuente tab_contribuente { get; set; }
        ///<summary><para>Relazione: FK_join_tab_avv_pag_tab_doc_input_tab_avv_pag_fatt_emissione</para> Chiave Origine: id_tab_avv_pag_fatt_emissione</summary>
     public virtual ICollection<join_tab_avv_pag_tab_doc_input> join_tab_avv_pag_tab_doc_input { get; set; }
        ///<summary><para>Relazione: FK_tab_unita_contribuzione_fatt_emissione_tab_avv_pag_fatt_emissione</para> Chiave Origine: id_avv_pag_generato</summary>
     public virtual ICollection<tab_unita_contribuzione_fatt_emissione> tab_unita_contribuzione_fatt_emissione { get; set; }
        ///<summary><para>Relazione: FK_tab_avv_pag_fatt_emissione_anagrafica_ente</para> Chiave Origine: id_ente</summary>
     public virtual anagrafica_ente anagrafica_ente { get; set; }
    }
}
