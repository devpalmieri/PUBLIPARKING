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
    
    public partial class Giri
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Giri()
        {
            this.GiriOperatore = new HashSet<GiriOperatore>();
            this.TariffeAbbonamenti = new HashSet<TariffeAbbonamenti>();
        }
    
        public int idGiro { get; set; }
        public string descrizione { get; set; }
        public System.DateTime dataUltimaModifica { get; set; }
    
        public virtual GiriFrequenzaFoto GiriFrequenzaFoto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GiriOperatore> GiriOperatore { get; set; }
        public virtual StalliGiro StalliGiro { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TariffeAbbonamenti> TariffeAbbonamenti { get; set; }
    }
}