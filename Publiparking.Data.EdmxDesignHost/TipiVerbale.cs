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
    
    public partial class TipiVerbale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipiVerbale()
        {
            this.Causali = new HashSet<Causali>();
        }
    
        public int idTipoVerbale { get; set; }
        public string descrizione { get; set; }
        public Nullable<bool> isDefault { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Causali> Causali { get; set; }
    }
}