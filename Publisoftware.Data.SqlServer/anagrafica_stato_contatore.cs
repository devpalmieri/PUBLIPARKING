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
    
    public partial class anagrafica_stato_contatore
    {
        public anagrafica_stato_contatore()
        {
            this.tab_contatore = new HashSet<tab_contatore>();
        }
    
        public int id_anagrafica_stato { get; set; }
        public string cod_stato { get; set; }
        public string desc_stato { get; set; }
    
        ///<summary><para>Relazione: FK_tab_contatore_anagrafica_stato_contatore</para> Chiave Origine: id_stato</summary>
     public virtual ICollection<tab_contatore> tab_contatore { get; set; }
    }
}
