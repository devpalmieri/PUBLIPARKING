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
    
    public partial class RicaricheConfermate
    {
        public int idRicaricaConfermata { get; set; }
        public int idRicaricaAbbonamento { get; set; }
        public Nullable<int> idSMSOut { get; set; }
    
        public virtual translog translog { get; set; }
        public virtual SMSOut SMSOut { get; set; }
    }
}
