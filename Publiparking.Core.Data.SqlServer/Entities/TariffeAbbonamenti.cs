﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Publiparking.Core.Data.SqlServer.Entities
{
    public partial class TariffeAbbonamenti
    {
        public TariffeAbbonamenti()
        {
            FasceTariffeAbbonamenti = new HashSet<FasceTariffeAbbonamenti>();
        }

        [Key]
        public int idTariffaAbbonamento { get; set; }
        [Required]
        [StringLength(30)]
        public string descrizione { get; set; }
        public int idGiroValidita { get; set; }
        public bool validoFestivi { get; set; }
        public TimeSpan? ora_validita_inizio { get; set; }
        public TimeSpan? ora_validita_fine { get; set; }
        [Required]
        [StringLength(1)]
        public string flag_on_off { get; set; }

        [InverseProperty("idTariffaAbbonamentoNavigation")]
        public virtual ICollection<FasceTariffeAbbonamenti> FasceTariffeAbbonamenti { get; set; }
    }
}