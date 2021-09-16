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
    
    public partial class anagrafica_tipo_servizi
    {
        public anagrafica_tipo_servizi()
        {
            this.anagrafica_servizi = new HashSet<anagrafica_servizi>();
            this.anagrafica_cicli = new HashSet<anagrafica_cicli>();
            this.tab_pianificazione_cicli = new HashSet<tab_pianificazione_cicli>();
            this.tab_pianificazione_cicli_input_emissione = new HashSet<tab_pianificazione_cicli_input_emissione>();
            this.tab_tipo_servizio_entrata_cc = new HashSet<tab_tipo_servizio_entrata_cc>();
            this.anagrafica_controlli_qualita_emissione_avvpag = new HashSet<anagrafica_controlli_qualita_emissione_avvpag>();
            this.join_conto_economico_tipo_voce_contribuzione = new HashSet<join_conto_economico_tipo_voce_contribuzione>();
            this.join_tipo_avvpag_voci_contrib_new = new HashSet<join_tipo_avvpag_voci_contrib_new>();
            this.tab_ente_servizi = new HashSet<tab_ente_servizi>();
            this.tab_iter_supervisione_atti_coa = new HashSet<tab_iter_supervisione_atti_coa>();
            this.anagrafica_causale = new HashSet<anagrafica_causale>();
            this.anagrafica_tipo_avv_pag = new HashSet<anagrafica_tipo_avv_pag>();
            this.anagrafica_tipo_avv_pag1 = new HashSet<anagrafica_tipo_avv_pag>();
            this.join_tipo_avvpag_voci_contrib_trasmesse = new HashSet<join_tipo_avvpag_voci_contrib_trasmesse>();
            this.tab_calcolo_tipo_voci_contribuzione = new HashSet<tab_calcolo_tipo_voci_contribuzione>();
            this.tab_rateizzazioni = new HashSet<tab_rateizzazioni>();
            this.tab_cc_riscossione = new HashSet<tab_cc_riscossione>();
            this.join_ente_strutture_tipoavvpag = new HashSet<join_ente_strutture_tipoavvpag>();
            this.tab_modalita_rate_avvpag = new HashSet<tab_modalita_rate_avvpag>();
            this.tab_rateizzazioni1 = new HashSet<tab_rateizzazioni>();
            this.tab_pec_configurazioni = new HashSet<tab_pec_configurazioni>();
            this.join_causali_motivazioni_causali_esito = new HashSet<join_causali_motivazioni_causali_esito>();
            this.anagrafica_documenti = new HashSet<anagrafica_documenti>();
            this.tab_esclusione_rateizzazione = new HashSet<tab_esclusione_rateizzazione>();
        }
    
        public int id_tipo_servizio { get; set; }
        public string descr_tiposervizio { get; set; }
    
        ///<summary><para>Relazione: FK_anagrafica_servizi_anagrafica_tipo_servizi</para> Chiave Origine: id_tipo_servizio</summary>
     public virtual ICollection<anagrafica_servizi> anagrafica_servizi { get; set; }
        ///<summary><para>Relazione: FK_anagrafica_cicli_anagrafica_tipo_servizi</para> Chiave Origine: id_tipo_servizio</summary>
     public virtual ICollection<anagrafica_cicli> anagrafica_cicli { get; set; }
        ///<summary><para>Relazione: FK_tab_pianificazione_cicli_anagrafica_tipo_servizi</para> Chiave Origine: id_tipo_servizio</summary>
     public virtual ICollection<tab_pianificazione_cicli> tab_pianificazione_cicli { get; set; }
        ///<summary><para>Relazione: FK_tab_pianificazione_cicli_input_emissione_anagrafica_tipo_servizi</para> Chiave Origine: id_tipo_servizio</summary>
     public virtual ICollection<tab_pianificazione_cicli_input_emissione> tab_pianificazione_cicli_input_emissione { get; set; }
        ///<summary><para>Relazione: FK_tab_tiposervizio_entrata_cc_anagrafica_tipo_servizi</para> Chiave Origine: id_tipo_servizio</summary>
     public virtual ICollection<tab_tipo_servizio_entrata_cc> tab_tipo_servizio_entrata_cc { get; set; }
        ///<summary><para>Relazione: FK_anagrafica_controlli_qualita_emissione_avvpag_anagrafica_tipo_servizi</para> Chiave Origine: id_tipo_servizio</summary>
     public virtual ICollection<anagrafica_controlli_qualita_emissione_avvpag> anagrafica_controlli_qualita_emissione_avvpag { get; set; }
        ///<summary><para>Relazione: FK_join_conto_economico_tipo_voce_contribuzione_anagrafica_tipo_servizi</para> Chiave Origine: id_tipo_servizio</summary>
     public virtual ICollection<join_conto_economico_tipo_voce_contribuzione> join_conto_economico_tipo_voce_contribuzione { get; set; }
        ///<summary><para>Relazione: FK_join_tipo_avvpag_voci_contrib_new_anagrafica_tipo_servizi</para> Chiave Origine: id_tipo_servizio</summary>
     public virtual ICollection<join_tipo_avvpag_voci_contrib_new> join_tipo_avvpag_voci_contrib_new { get; set; }
        ///<summary><para>Relazione: FK_tab_ente_servizi_anagrafica_tipo_servizi</para> Chiave Origine: id_tipo_servizio</summary>
     public virtual ICollection<tab_ente_servizi> tab_ente_servizi { get; set; }
        ///<summary><para>Relazione: FK_tab_iter_supervisione_atti_coa_anagrafica_tipo_servizi</para> Chiave Origine: id_tipo_servizio_avvpag</summary>
     public virtual ICollection<tab_iter_supervisione_atti_coa> tab_iter_supervisione_atti_coa { get; set; }
        ///<summary><para>Relazione: FK_anagrafica_causale_anagrafica_servizi1</para> Chiave Origine: id_tipo_servizio</summary>
     public virtual ICollection<anagrafica_causale> anagrafica_causale { get; set; }
        ///<summary><para>Relazione: FK_anagrafica_tipo_avv_pag_anagrafica_tipo_servizi</para> Chiave Origine: id_servizio</summary>
     public virtual ICollection<anagrafica_tipo_avv_pag> anagrafica_tipo_avv_pag { get; set; }
        ///<summary><para>Relazione: FK_anagrafica_tipo_avv_pag_anagrafica_tipo_servizi1</para> Chiave Origine: id_tipo_servizio_avvpag_successivi</summary>
     public virtual ICollection<anagrafica_tipo_avv_pag> anagrafica_tipo_avv_pag1 { get; set; }
        ///<summary><para>Relazione: FK_join_tipo_avvpag_voci_contrib_trasmesse_anagrafica_tipo_servizi</para> Chiave Origine: id_tipo_servizio</summary>
     public virtual ICollection<join_tipo_avvpag_voci_contrib_trasmesse> join_tipo_avvpag_voci_contrib_trasmesse { get; set; }
        ///<summary><para>Relazione: FK_tab_calcolo_tipo_voci_contribuzione_anagrafica_tipo_servizi</para> Chiave Origine: id_tipo_servizio</summary>
     public virtual ICollection<tab_calcolo_tipo_voci_contribuzione> tab_calcolo_tipo_voci_contribuzione { get; set; }
        ///<summary><para>Relazione: FK_tab_rateizzazioni_anagrafica_tipo_servizi</para> Chiave Origine: id_tipo_servizio</summary>
     public virtual ICollection<tab_rateizzazioni> tab_rateizzazioni { get; set; }
        ///<summary><para>Relazione: FK_tab_cc_riscossione_anagrafica_tipo_servizi</para> Chiave Origine: id_tipo_servizio</summary>
     public virtual ICollection<tab_cc_riscossione> tab_cc_riscossione { get; set; }
        ///<summary><para>Relazione: FK_join_ente_strutture_tipoavvpag_anagrafica_tipo_servizi</para> Chiave Origine: id_tipo_servizio</summary>
     public virtual ICollection<join_ente_strutture_tipoavvpag> join_ente_strutture_tipoavvpag { get; set; }
        ///<summary><para>Relazione: FK_tab_modalita_rate_avvpag_anagrafica_tipo_servizi</para> Chiave Origine: id_tipo_servizio</summary>
     public virtual ICollection<tab_modalita_rate_avvpag> tab_modalita_rate_avvpag { get; set; }
        ///<summary><para>Relazione: FK_tab_rateizzazioni_anagrafica_tipo_servizi1</para> Chiave Origine: id_tipo_servizio_atto_rateizzato</summary>
     public virtual ICollection<tab_rateizzazioni> tab_rateizzazioni1 { get; set; }
        ///<summary><para>Relazione: FK_tab_pec_configurazioni_anagrafica_tipo_servizi</para> Chiave Origine: id_tipo_servizio</summary>
     public virtual ICollection<tab_pec_configurazioni> tab_pec_configurazioni { get; set; }
        ///<summary><para>Relazione: FK_join_causali_motivazioni_causali_esito_anagrafica_tipo_servizi</para> Chiave Origine: id_tipo_servizio_avvpag</summary>
     public virtual ICollection<join_causali_motivazioni_causali_esito> join_causali_motivazioni_causali_esito { get; set; }
        ///<summary><para>Relazione: FK_anagrafica_documenti_anagrafica_tipo_servizi</para> Chiave Origine: id_tipo_servizio</summary>
     public virtual ICollection<anagrafica_documenti> anagrafica_documenti { get; set; }
        ///<summary><para>Relazione: FK_tab_esclusione_rateizzazione_anagrafica_tipo_servizi</para> Chiave Origine: id_tipo_servizio</summary>
     public virtual ICollection<tab_esclusione_rateizzazione> tab_esclusione_rateizzazione { get; set; }
    }
}
