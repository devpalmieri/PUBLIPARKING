﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Publiparking.Core.Data.SqlServer.Entities
{
    public partial class tab_tipo_ente
    {
        public tab_tipo_ente()
        {
            anagrafica_ente = new HashSet<anagrafica_ente>();
        }

        [Key]
        public int id_tipo_ente { get; set; }
        [Required]
        [StringLength(6)]
        public string cod_tipo_ente { get; set; }
        [Required]
        [StringLength(100)]
        public string desc_tipo_ente { get; set; }
        [StringLength(3)]
        public string prefisso_codice { get; set; }

        [InverseProperty("id_tipo_enteNavigation")]
        public virtual ICollection<anagrafica_ente> anagrafica_ente { get; set; }
    }
}