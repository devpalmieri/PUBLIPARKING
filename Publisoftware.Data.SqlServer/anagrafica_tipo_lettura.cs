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
    
    public partial class anagrafica_tipo_lettura
    {
        public anagrafica_tipo_lettura()
        {
            this.tab_letture = new HashSet<tab_letture>();
        }
    
        public int id_tipo_lettura { get; set; }
        public string cod_tipo_lettura { get; set; }
        public string descrizione_tipo_lettura { get; set; }
        public string flag_iof { get; set; }
        public string descrizione_tipo { get; set; }
    
        ///<summary><para>Relazione: FK_tab_letture_anagrafica_tipo_lettura</para> Chiave Origine: id_tipo_lettura</summary>
     public virtual ICollection<tab_letture> tab_letture { get; set; }
    }
}