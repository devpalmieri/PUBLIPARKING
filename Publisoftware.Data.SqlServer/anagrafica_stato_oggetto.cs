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
    
    public partial class anagrafica_stato_oggetto
    {
        public anagrafica_stato_oggetto()
        {
            this.tab_oggetti_contribuzione = new HashSet<tab_oggetti_contribuzione>();
            this.join_denunce_oggetti = new HashSet<join_denunce_oggetti>();
        }
    
        public int id_anagrafica_stato { get; set; }
        public string cod_stato { get; set; }
        public string desc_stato { get; set; }
    
        ///<summary><para>Relazione: FK_tab_oggetti_contribuzione_anagrafica_stato_oggetto</para> Chiave Origine: id_stato_oggetto</summary>
     public virtual ICollection<tab_oggetti_contribuzione> tab_oggetti_contribuzione { get; set; }
        ///<summary><para>Relazione: FK_join_denunce_oggetti_anagrafica_stato_oggetto</para> Chiave Origine: id_stato_oggetto_data_denuncia</summary>
     public virtual ICollection<join_denunce_oggetti> join_denunce_oggetti { get; set; }
    }
}
