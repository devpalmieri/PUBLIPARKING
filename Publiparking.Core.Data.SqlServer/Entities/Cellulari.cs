using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Publiparking.Core.Data.SqlServer.Entities
{
    public partial class Cellulari
    {
        public Cellulari()
        {
            TitoliSMS = new HashSet<TitoliSMS>();
            TitoliSMSTarga = new HashSet<TitoliSMSTarga>();
        }

        [Key]
        public int idCellulare { get; set; }
        [Required]
        [StringLength(20)]
        public string numero { get; set; }
        public int idAbbonamento { get; set; }
        public bool master { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime dataAttivazione { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? dataCessazione { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? dataCodiceVerifica { get; set; }
        [StringLength(50)]
        public string codiceVerifica { get; set; }
        [StringLength(1)]
        public string flag_on_off { get; set; }

        [InverseProperty("idCellulareNavigation")]
        public virtual ICollection<TitoliSMS> TitoliSMS { get; set; }
        [InverseProperty("idCellulareNavigation")]
        public virtual ICollection<TitoliSMSTarga> TitoliSMSTarga { get; set; }
    }
}