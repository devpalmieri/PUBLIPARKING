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
    
    public partial class CausaliVerbali
    {
        public int idVerbale { get; set; }
        public int idCausale { get; set; }
        public int idCausaleVerbale { get; set; }
    
        public virtual Causali Causali { get; set; }
        public virtual Verbali Verbali { get; set; }
    }
}
