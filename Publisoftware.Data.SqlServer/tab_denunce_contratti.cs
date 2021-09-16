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
    
    public partial class tab_denunce_contratti
    {
        public tab_denunce_contratti()
        {
            this.join_denunce_doc_input = new HashSet<join_denunce_doc_input>();
            this.join_denunce_motivazioni_richiesta_info = new HashSet<join_denunce_motivazioni_richiesta_info>();
            this.join_documenti_pratiche_presentati = new HashSet<join_documenti_pratiche_presentati>();
            this.join_denunce_oggetti = new HashSet<join_denunce_oggetti>();
            this.join_documenti_pratiche = new HashSet<join_documenti_pratiche>();
        }
    
        public int id_tab_denunce_contratti { get; set; }
        public int id_ente { get; set; }
        public int id_ente_gestito { get; set; }
        public int id_entrata { get; set; }
        public decimal id_contribuente { get; set; }
        public int id_tipo_doc_entrate { get; set; }
        public int anno { get; set; }
        public int prog_tipo_doc_entrata { get; set; }
        public string cod_doc { get; set; }
        public Nullable<int> id_causale { get; set; }
        public System.DateTime data_presentazione { get; set; }
        public Nullable<System.DateTime> data_decorrenza { get; set; }
        public Nullable<System.DateTime> data_scadenza { get; set; }
        public Nullable<int> id_risorsa_acquisizione { get; set; }
        public string annotazioni { get; set; }
        public int id_stato { get; set; }
        public string cod_stato { get; set; }
        public System.DateTime data_stato { get; set; }
        public int id_struttura_stato { get; set; }
        public Nullable<int> id_risorsa_stato { get; set; }
        public string identificativo_denuncia_contratto { get; set; }
        public string flag_soggetto_presentante { get; set; }
        public Nullable<int> id_referente_presentante { get; set; }
        public Nullable<int> id_utente_presentante { get; set; }
    
        ///<summary><para>Relazione: FK_tab_denunce_contratti_anagrafica_ente_gestito</para> Chiave Origine: id_ente_gestito</summary>
     public virtual anagrafica_ente_gestito anagrafica_ente_gestito { get; set; }
        ///<summary><para>Relazione: FK_tab_denunce_contratti_anagrafica_entrate</para> Chiave Origine: id_entrata</summary>
     public virtual anagrafica_entrate anagrafica_entrate { get; set; }
        ///<summary><para>Relazione: FK_tab_denunce_contratti_anagrafica_risorse</para> Chiave Origine: id_risorsa_acquisizione</summary>
     public virtual anagrafica_risorse anagrafica_risorse { get; set; }
        ///<summary><para>Relazione: FK_join_denunce_doc_input_tab_denunce_contratti</para> Chiave Origine: id_denuncia</summary>
     public virtual ICollection<join_denunce_doc_input> join_denunce_doc_input { get; set; }
        ///<summary><para>Relazione: FK_tab_denunce_contratti_anagrafica_causale</para> Chiave Origine: id_causale</summary>
     public virtual anagrafica_causale anagrafica_causale { get; set; }
        ///<summary><para>Relazione: FK_join_denunce_motivazioni_richiesta_info_tab_denunce_contratti</para> Chiave Origine: id_denuncia</summary>
     public virtual ICollection<join_denunce_motivazioni_richiesta_info> join_denunce_motivazioni_richiesta_info { get; set; }
        ///<summary><para>Relazione: FK_join_documenti_pratiche_presentati_tab_denunce_contratti</para> Chiave Origine: id_tab_denunce_contratti</summary>
     public virtual ICollection<join_documenti_pratiche_presentati> join_documenti_pratiche_presentati { get; set; }
        ///<summary><para>Relazione: FK_tab_denunce_contratti_tab_contribuente</para> Chiave Origine: id_contribuente</summary>
     public virtual tab_contribuente tab_contribuente { get; set; }
        ///<summary><para>Relazione: FK_tab_denunce_contratti_tab_tipo_doc_entrate</para> Chiave Origine: id_tipo_doc_entrate</summary>
     public virtual tab_tipo_doc_entrate tab_tipo_doc_entrate { get; set; }
        ///<summary><para>Relazione: FK_join_denunce_oggetti_tab_denunce_contratti</para> Chiave Origine: id_denunce_contratti</summary>
     public virtual ICollection<join_denunce_oggetti> join_denunce_oggetti { get; set; }
        ///<summary><para>Relazione: FK_join_documenti_pratiche_tab_denunce_contratti</para> Chiave Origine: id_denunce_contratti</summary>
     public virtual ICollection<join_documenti_pratiche> join_documenti_pratiche { get; set; }
        ///<summary><para>Relazione: FK_tab_denunce_contratti_tab_referente</para> Chiave Origine: id_referente_presentante</summary>
     public virtual tab_referente tab_referente { get; set; }
        ///<summary><para>Relazione: FK_tab_denunce_contratti_anagrafica_ente</para> Chiave Origine: id_ente</summary>
     public virtual anagrafica_ente anagrafica_ente { get; set; }
    }
}