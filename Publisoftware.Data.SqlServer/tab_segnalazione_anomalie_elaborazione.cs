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
    
    public partial class tab_segnalazione_anomalie_elaborazione
    {
        public int id_segnalazione_anomalia { get; set; }
        public System.DateTime data_segnalazione { get; set; }
        public int id_esecuzione_applicazioni { get; set; }
        public string descrizione_segnalazione { get; set; }
    }
}