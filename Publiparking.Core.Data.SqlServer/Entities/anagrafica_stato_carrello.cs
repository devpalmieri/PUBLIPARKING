﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Publiparking.Core.Data.SqlServer.Entities
{
    public partial class anagrafica_stato_carrello
    {
        [Key]
        public int id_anagrafica_stato { get; set; }
        [Required]
        [StringLength(7)]
        public string cod_stato { get; set; }
        [Required]
        [StringLength(50)]
        public string desc_stato { get; set; }
        [Required]
        [StringLength(1)]
        public string flag_on_off { get; set; }
    }
}