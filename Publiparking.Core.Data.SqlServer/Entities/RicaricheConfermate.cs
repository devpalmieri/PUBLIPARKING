using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Publiparking.Core.Data.SqlServer.Entities
{
    public partial class RicaricheConfermate
    {
        [Key]
        public int idRicaricaConfermata { get; set; }
        public int idRicaricaAbbonamento { get; set; }
        public int? idSMSOut { get; set; }
        [StringLength(1)]
        public string flag_on_off { get; set; }

        [ForeignKey(nameof(idRicaricaAbbonamento))]
        [InverseProperty(nameof(translog.RicaricheConfermate))]
        public virtual translog idRicaricaAbbonamentoNavigation { get; set; }
        [ForeignKey(nameof(idSMSOut))]
        [InverseProperty(nameof(SMSOut.RicaricheConfermate))]
        public virtual SMSOut idSMSOutNavigation { get; set; }
    }
}