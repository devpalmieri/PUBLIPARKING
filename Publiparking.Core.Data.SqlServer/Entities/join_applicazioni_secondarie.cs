﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Publiparking.Core.Data.SqlServer.Entities
{
    public partial class join_applicazioni_secondarie
    {
        [Key]
        public int id_join_applicazioni_secondarie { get; set; }
        public int id_tab_applicazioni_principale { get; set; }
        public int id_tab_applicazioni_secondarie { get; set; }
        [Required]
        [StringLength(1)]
        public string flag_on_off { get; set; }
    }
}