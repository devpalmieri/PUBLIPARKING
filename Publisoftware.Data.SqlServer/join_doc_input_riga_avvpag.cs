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
    
    public partial class join_doc_input_riga_avvpag
    {
        public join_doc_input_riga_avvpag()
        {
            this.join_documenti_pratiche = new HashSet<join_documenti_pratiche>();
        }
    
        public int id_join_avvpag_riga { get; set; }
        public int id_join_avv_pag_doc_input { get; set; }
        public int id_unita_contribuzione { get; set; }
        public int id_causale { get; set; }
        public Nullable<decimal> id_oggetto_contribuzione { get; set; }
        public Nullable<int> id_intervento { get; set; }
        public Nullable<int> id_fatt_consumi { get; set; }
        public Nullable<int> id_agevolazione { get; set; }
        public string annotazioni_acquisizione { get; set; }
        public Nullable<int> id_intervento_generato { get; set; }
        public string flag_esito { get; set; }
        public Nullable<int> id_causale_esito { get; set; }
        public Nullable<System.DateTime> data_esito { get; set; }
        public string annotazioni_esito { get; set; }
        public Nullable<int> id_stato { get; set; }
        public string cod_stato { get; set; }
        public Nullable<System.DateTime> data_stato { get; set; }
        public Nullable<int> id_struttura_stato { get; set; }
        public Nullable<int> id_risorsa_stato { get; set; }
    
        ///<summary><para>Relazione: FK_join_doc_input_riga_avvpag_anagrafica_agevolazione</para> Chiave Origine: id_agevolazione</summary>
     public virtual anagrafica_agevolazione anagrafica_agevolazione { get; set; }
        ///<summary><para>Relazione: FK_join_doc_input_riga_avvpag_anagrafica_causale</para> Chiave Origine: id_causale</summary>
     public virtual anagrafica_causale anagrafica_causale { get; set; }
        ///<summary><para>Relazione: FK_join_doc_input_riga_avvpag_anagrafica_risorse</para> Chiave Origine: id_risorsa_stato</summary>
     public virtual anagrafica_risorse anagrafica_risorse { get; set; }
        ///<summary><para>Relazione: FK_join_doc_input_riga_avvpag_anagrafica_strutture_aziendali</para> Chiave Origine: id_struttura_stato</summary>
     public virtual anagrafica_strutture_aziendali anagrafica_strutture_aziendali { get; set; }
        ///<summary><para>Relazione: FK_join_doc_input_riga_avvpag_tab_oggetti_contribuzione</para> Chiave Origine: id_oggetto_contribuzione</summary>
     public virtual tab_oggetti_contribuzione tab_oggetti_contribuzione { get; set; }
        ///<summary><para>Relazione: FK_join_documenti_pratiche_join_doc_input_riga_avvpag</para> Chiave Origine: id_join_avvpag_riga</summary>
     public virtual ICollection<join_documenti_pratiche> join_documenti_pratiche { get; set; }
        ///<summary><para>Relazione: FK_join_doc_input_riga_avvpag_join_tab_avv_pag_tab_doc_input</para> Chiave Origine: id_join_avv_pag_doc_input</summary>
     public virtual join_tab_avv_pag_tab_doc_input join_tab_avv_pag_tab_doc_input { get; set; }
        ///<summary><para>Relazione: FK_join_doc_input_riga_avvpag_tab_unita_contribuzione</para> Chiave Origine: id_unita_contribuzione</summary>
     public virtual tab_unita_contribuzione tab_unita_contribuzione { get; set; }
    }
}