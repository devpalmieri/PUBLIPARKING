﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Publiparking.Core.Data.SqlServer.Entities
{
    public partial class tab_registro_cookie
    {
        [Key]
        public int id_tab_registro_cookie { get; set; }
        [StringLength(50)]
        public string indirizzo_ip { get; set; }
        public string headers { get; set; }
        [StringLength(100)]
        public string session_id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? data_prima_visita { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? data_ultima_visita { get; set; }
        [StringLength(50)]
        public string consenso { get; set; }
        public bool? consenso_necessari { get; set; }
        public bool? consenso_preferenze { get; set; }
        public bool? consenso_statistiche { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? data_stato { get; set; }
        [StringLength(1)]
        public string flag_on_off { get; set; }
    }
}