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
    
    public partial class anagrafica_tipo_documento
    {
        public anagrafica_tipo_documento()
        {
            this.anagrafica_documenti = new HashSet<anagrafica_documenti>();
        }
    
        public int id_tipo_documento { get; set; }
        public string codice { get; set; }
        public string sigla { get; set; }
        public string descrizione { get; set; }
    
        ///<summary><para>Relazione: FK_anagrafica_documenti_anagrafica_tipo_documento</para> Chiave Origine: id_tipo_documento</summary>
     public virtual ICollection<anagrafica_documenti> anagrafica_documenti { get; set; }
    }
}
