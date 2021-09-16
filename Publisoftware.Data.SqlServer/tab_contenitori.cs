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
    
    public partial class tab_contenitori
    {
        public tab_contenitori()
        {
            this.join_contenitori_join_file = new HashSet<join_contenitori_join_file>();
        }
    
        public int id_tab_contenitori { get; set; }
        public string codice { get; set; }
        public int id_ente { get; set; }
        public int id_operatore { get; set; }
        public Nullable<System.DateTime> datacreazione { get; set; }
        public Nullable<System.DateTime> dataultimolog { get; set; }
        public string ultimolog { get; set; }
    
        ///<summary><para>Relazione: FK_tab_contenitori_anagrafica_risorse</para> Chiave Origine: id_operatore</summary>
     public virtual anagrafica_risorse anagrafica_risorse { get; set; }
        ///<summary><para>Relazione: FK_join_contenitori_join_file_tab_contenitori</para> Chiave Origine: id_tab_contenitori</summary>
     public virtual ICollection<join_contenitori_join_file> join_contenitori_join_file { get; set; }
        ///<summary><para>Relazione: FK_tab_contenitori_anagrafica_ente</para> Chiave Origine: id_ente</summary>
     public virtual anagrafica_ente anagrafica_ente { get; set; }
    }
}
