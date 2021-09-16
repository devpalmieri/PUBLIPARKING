using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Publiparking.Core.Data.SqlServer.Entities
{
    public partial class Stalli
    {
        public Stalli()
        {
            TitoliSMS = new HashSet<TitoliSMS>();
            TitoliSMSTarga = new HashSet<TitoliSMSTarga>();
        }

        [Key]
        public int idStallo { get; set; }
        [Required]
        [StringLength(10)]
        public string numero { get; set; }
        [Required]
        [StringLength(200)]
        public string ubicazione { get; set; }
        [Column(TypeName = "decimal(8, 6)")]
        public decimal X { get; set; }
        [Column(TypeName = "decimal(8, 6)")]
        public decimal Y { get; set; }
        public bool fotoRichiesta { get; set; }
        public int? idToponimo { get; set; }
        public int? idTariffa { get; set; }
        [StringLength(1)]
        public string flag_on_off { get; set; }

        [ForeignKey(nameof(idTariffa))]
        [InverseProperty(nameof(Tariffe.Stalli))]
        public virtual Tariffe idTariffaNavigation { get; set; }
        [InverseProperty("idStalloNavigation")]
        public virtual ICollection<TitoliSMS> TitoliSMS { get; set; }
        [InverseProperty("idStalloNavigation")]
        public virtual ICollection<TitoliSMSTarga> TitoliSMSTarga { get; set; }
    }
}