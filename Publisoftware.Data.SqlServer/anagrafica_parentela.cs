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
    
    public partial class anagrafica_parentela
    {
        public anagrafica_parentela()
        {
            this.join_referente_contribuente = new HashSet<join_referente_contribuente>();
        }
    
        public int id_parentela { get; set; }
        public string sigla_parentela { get; set; }
        public string descrizione_parentela { get; set; }
    
        ///<summary><para>Relazione: FK_join_referente_contribuente_anagrafica_parentela</para> Chiave Origine: id_anagrafica_parentela</summary>
     public virtual ICollection<join_referente_contribuente> join_referente_contribuente { get; set; }
    }
}