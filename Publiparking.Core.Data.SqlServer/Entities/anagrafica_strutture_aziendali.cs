﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Publiparking.Core.Data.SqlServer.Entities
{
    public partial class anagrafica_strutture_aziendali
    {
        public anagrafica_strutture_aziendali()
        {
            anagrafica_risorse = new HashSet<anagrafica_risorse>();
        }

        [Key]
        public int id_struttura_aziendale { get; set; }
        [Required]
        [StringLength(50)]
        public string codice_struttura_aziendale { get; set; }
        [Required]
        [StringLength(100)]
        public string descr_struttura { get; set; }
        [StringLength(10)]
        public string tipo_struttura { get; set; }
        public int? id_risorsa { get; set; }
        [StringLength(15)]
        public string telefono { get; set; }
        [StringLength(50)]
        public string email { get; set; }
        [StringLength(255)]
        public string indirizzo { get; set; }
        [StringLength(2)]
        public string provincia { get; set; }
        [StringLength(5)]
        public string cap { get; set; }
        public int? prog_lav { get; set; }
        [Required]
        [StringLength(1)]
        public string flag_on_off { get; set; }
        public int? id_ente_appartenenza { get; set; }

        [ForeignKey(nameof(id_ente_appartenenza))]
        [InverseProperty(nameof(anagrafica_ente.anagrafica_strutture_aziendali))]
        public virtual anagrafica_ente id_ente_appartenenzaNavigation { get; set; }
        [ForeignKey(nameof(id_risorsa))]
        [InverseProperty("anagrafica_strutture_aziendali")]
        public virtual anagrafica_risorse id_risorsaNavigation { get; set; }
        [InverseProperty("id_struttura_statoNavigation")]
        public virtual ICollection<anagrafica_risorse> anagrafica_risorse { get; set; }
    }
}