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
    
    public partial class tab_pagine
    {
        public tab_pagine()
        {
            this.tab_applicazioni = new HashSet<tab_applicazioni>();
        }
    
        public int id_tab_pagine { get; set; }
        public string descrizione { get; set; }
        public string controller { get; set; }
        public string actions { get; set; }
        public string tipo { get; set; }
        public bool schedulable { get; set; }
    
        ///<summary><para>Relazione: FK_tab_applicazioni_tab_pagine</para> Chiave Origine: id_tab_pagine</summary>
     public virtual ICollection<tab_applicazioni> tab_applicazioni { get; set; }
    }
}