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
    
    public partial class tab_consuntivo_soglie_tipo
    {
        public tab_consuntivo_soglie_tipo()
        {
            this.tab_consuntivo_soglie = new HashSet<tab_consuntivo_soglie>();
        }
    
        public int id_tipo_soglia { get; set; }
        public string descrizione_tipo_soglia { get; set; }
    
        ///<summary><para>Relazione: FK_tab_consuntivo_soglie_tab_consuntivo_soglie_tipo</para> Chiave Origine: id_tipo_soglia</summary>
     public virtual ICollection<tab_consuntivo_soglie> tab_consuntivo_soglie { get; set; }
    }
}
