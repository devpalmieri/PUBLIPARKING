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
    
    public partial class tab_vincoli_beni_pignorati
    {
        public int id_tab_vincoli_beni { get; set; }
        public int id_join_avv_pag_doc_input { get; set; }
        public string descr_vincolo { get; set; }
        public Nullable<System.DateTime> data_accensione_vincolo { get; set; }
        public string nominativo_rag_sociale_beneficiario { get; set; }
        public string cod_fiscale_piva_beneficiario { get; set; }
        public string note { get; set; }
        public Nullable<System.DateTime> data_scadenza_vincolo { get; set; }
        public Nullable<decimal> importo_vincolato { get; set; }
    
        ///<summary><para>Relazione: FK_tab_vincoli_beni_pignorati_join_tab_avv_pag_tab_doc_input</para> Chiave Origine: id_join_avv_pag_doc_input</summary>
     public virtual join_tab_avv_pag_tab_doc_input join_tab_avv_pag_tab_doc_input { get; set; }
    }
}
