//------------------------------------------------------------------------------
// <auto-generated>
//     Codice generato da un modello.
//
//     Le modifiche manuali a questo file potrebbero causare un comportamento imprevisto dell'applicazione.
//     Se il codice viene rigenerato, le modifiche manuali al file verranno sovrascritte.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Publiparking.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class GiriOperatore
    {
        public int idOperatore { get; set; }
        public int idGiro { get; set; }
        public int giorno { get; set; }
    
        public virtual Giri Giri { get; set; }
        public virtual Operatori Operatori { get; set; }
    }
}
