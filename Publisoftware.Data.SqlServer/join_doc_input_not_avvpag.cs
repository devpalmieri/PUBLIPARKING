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
    
    public partial class join_doc_input_not_avvpag
    {
        public join_doc_input_not_avvpag()
        {
            this.join_documenti_pratiche = new HashSet<join_documenti_pratiche>();
        }
    
        public int id_join_doc_input_not_avvpag { get; set; }
        public int id_tab_sped_not { get; set; }
        public int id_causale_istanza { get; set; }
        public string note_causale { get; set; }
        public int id_stato { get; set; }
        public string cod_stato { get; set; }
        public System.DateTime data_stato { get; set; }
        public int id_struttura_stato { get; set; }
        public int id_risorsa_stato { get; set; }
        public int id_join_avv_pag_doc_input { get; set; }
    
        ///<summary><para>Relazione: FK_join_doc_input_not_avvpag_anagrafica_risorse</para> Chiave Origine: id_risorsa_stato</summary>
     public virtual anagrafica_risorse anagrafica_risorse { get; set; }
        ///<summary><para>Relazione: FK_join_doc_input_not_avvpag_anagrafica_strutture_aziendali</para> Chiave Origine: id_struttura_stato</summary>
     public virtual anagrafica_strutture_aziendali anagrafica_strutture_aziendali { get; set; }
        ///<summary><para>Relazione: FK_join_doc_input_not_avvpag_anagrafica_causale</para> Chiave Origine: id_causale_istanza</summary>
     public virtual anagrafica_causale anagrafica_causale { get; set; }
        ///<summary><para>Relazione: FK_join_doc_input_not_avvpag_tab_sped_not</para> Chiave Origine: id_tab_sped_not</summary>
     public virtual tab_sped_not tab_sped_not { get; set; }
        ///<summary><para>Relazione: FK_join_documenti_pratiche_join_doc_input_not_avvpag</para> Chiave Origine: id_join_doc_input_not_avvpag</summary>
     public virtual ICollection<join_documenti_pratiche> join_documenti_pratiche { get; set; }
        ///<summary><para>Relazione: FK_join_doc_input_not_avvpag_join_tab_avv_pag_tab_doc_input</para> Chiave Origine: id_join_avv_pag_doc_input</summary>
     public virtual join_tab_avv_pag_tab_doc_input join_tab_avv_pag_tab_doc_input { get; set; }
    }
}