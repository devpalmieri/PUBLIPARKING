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
    
    public partial class Stalli
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Stalli()
        {
            this.OperazioniLocal = new HashSet<OperazioniLocal>();
            this.Penali = new HashSet<Penali>();
            this.StalliGiro = new HashSet<StalliGiro>();
            this.TitoliSMS = new HashSet<TitoliSMS>();
            this.TitoliSMSTarga = new HashSet<TitoliSMSTarga>();
            this.Verbali = new HashSet<Verbali>();
        }
    
        public int idStallo { get; set; }
        public string numero { get; set; }
        public string ubicazione { get; set; }
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public bool fotoRichiesta { get; set; }
        public Nullable<int> idToponimo { get; set; }
        public Nullable<int> idTariffa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OperazioniLocal> OperazioniLocal { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Penali> Penali { get; set; }
        public virtual tab_toponimi tab_toponimi { get; set; }
        public virtual Tariffe Tariffe { get; set; }
        public virtual StalliFrequenzaFoto StalliFrequenzaFoto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StalliGiro> StalliGiro { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TitoliSMS> TitoliSMS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TitoliSMSTarga> TitoliSMSTarga { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Verbali> Verbali { get; set; }
    }
}