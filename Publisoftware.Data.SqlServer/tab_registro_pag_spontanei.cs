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
    
    public partial class tab_registro_pag_spontanei
    {
        public int id_tab_pag_spontanei { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Email { get; set; }
        public string CFPagante { get; set; }
        public string PIvaPagante { get; set; }
        public string TipoPagante { get; set; }
        public string NumeroAvviso { get; set; }
        public string NumeroAvvisoPPA { get; set; }
        public Nullable<decimal> ImportoPagato { get; set; }
        public Nullable<System.DateTime> data_stato { get; set; }
    }
}
