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
    
    public partial class tab_progressivi_formalita_aci
    {
        public tab_progressivi_formalita_aci()
        {
            this.join_tab_supervisione_profili = new HashSet<join_tab_supervisione_profili>();
        }
    
        public int id_progressivo_formalita_aci { get; set; }
        public int id_ente { get; set; }
        public Nullable<int> num_pratica_iscrizione_fermo { get; set; }
        public Nullable<int> id_stato { get; set; }
        public string cod_stato { get; set; }
        public System.DateTime data_stato { get; set; }
        public int id_struttura_stato { get; set; }
        public int id_risorsa_stato { get; set; }
    
        ///<summary><para>Relazione: FK_join_tab_supervisione_profili_tab_progressivi_formalita_aci</para> Chiave Origine: id_progressivo_formalita_aci</summary>
     public virtual ICollection<join_tab_supervisione_profili> join_tab_supervisione_profili { get; set; }
    }
}
