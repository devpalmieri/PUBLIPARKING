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
    
    public partial class anagrafica_stato_doc
    {
        public anagrafica_stato_doc()
        {
            this.tab_doc_output = new HashSet<tab_doc_output>();
            this.tab_doc_input = new HashSet<tab_doc_input>();
            this.join_tab_avv_pag_tab_doc_input = new HashSet<join_tab_avv_pag_tab_doc_input>();
            this.join_tab_avv_pag_tab_doc_input1 = new HashSet<join_tab_avv_pag_tab_doc_input>();
        }
    
        public int id_anagrafica_stato { get; set; }
        public string cod_stato { get; set; }
        public string desc_stato { get; set; }
        public Nullable<int> seq_stato { get; set; }
    
        ///<summary><para>Relazione: FK_tab_doc_output_anagrafica_stato_doc</para> Chiave Origine: id_stato</summary>
     public virtual ICollection<tab_doc_output> tab_doc_output { get; set; }
        ///<summary><para>Relazione: FK_tab_doc_input_anagrafica_stato_doc</para> Chiave Origine: id_stato</summary>
     public virtual ICollection<tab_doc_input> tab_doc_input { get; set; }
        ///<summary><para>Relazione: FK_join_tab_avv_pag_tab_doc_input_anagrafica_stato_doc</para> Chiave Origine: id_stato</summary>
     public virtual ICollection<join_tab_avv_pag_tab_doc_input> join_tab_avv_pag_tab_doc_input { get; set; }
        ///<summary><para>Relazione: FK_join_tab_avv_pag_tab_doc_input_anagrafica_stato_doc1</para> Chiave Origine: id_stato_lavorazione</summary>
     public virtual ICollection<join_tab_avv_pag_tab_doc_input> join_tab_avv_pag_tab_doc_input1 { get; set; }
    }
}