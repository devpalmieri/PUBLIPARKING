using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Publiparking.Core.Data.SqlServer.Entities
{
    public partial class SMSIn
    {
        public SMSIn()
        {
            SMSOut = new HashSet<SMSOut>();
        }

        [Key]
        public int idSMSIn { get; set; }
        [StringLength(30)]
        public string numeroMittente { get; set; }
        [StringLength(30)]
        public string numeroDestinatario { get; set; }
        [StringLength(160)]
        public string testo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? dataRicezione { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? dataElaborazione { get; set; }
        [StringLength(200)]
        public string notaElaborazione { get; set; }
        [StringLength(1)]
        public string flag_on_off { get; set; }

        [InverseProperty("idSMSInNavigation")]
        public virtual ICollection<SMSOut> SMSOut { get; set; }
    }
}