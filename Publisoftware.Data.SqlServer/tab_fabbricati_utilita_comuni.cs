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
    
    public partial class tab_fabbricati_utilita_comuni
    {
        public int id_tab_fabbricati_utilita_comuni { get; set; }
        public int id_tab_fabbricati { get; set; }
        public string sezione_urbana { get; set; }
        public string foglio { get; set; }
        public string numero { get; set; }
        public Nullable<int> denominatore { get; set; }
        public string subalterno { get; set; }
    
        ///<summary><para>Relazione: fk_tab_fabbricati_utilita_comuni_tab_fabbricati</para> Chiave Origine: id_tab_fabbricati</summary>
     public virtual tab_fabbricati tab_fabbricati { get; set; }
    }
}
