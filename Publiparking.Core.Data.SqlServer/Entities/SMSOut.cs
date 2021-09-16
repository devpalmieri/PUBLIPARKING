using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Publiparking.Core.Data.SqlServer.Entities
{
    public partial class SMSOut
    {
        public SMSOut()
        {
            RicaricheConfermate = new HashSet<RicaricheConfermate>();
        }

        [Key]
        public int idSMSOut { get; set; }
        [StringLength(30)]
        public string numeroMittente { get; set; }
        [StringLength(30)]
        public string numeroDestinatario { get; set; }
        [StringLength(160)]
        public string testo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? dataInvio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? dataElaborazione { get; set; }
        public int? idSMSIn { get; set; }
        [StringLength(1)]
        public string flag_on_off { get; set; }

        [ForeignKey(nameof(idSMSIn))]
        [InverseProperty(nameof(SMSIn.SMSOut))]
        public virtual SMSIn idSMSInNavigation { get; set; }
        [InverseProperty("idSMSOutNavigation")]
        public virtual ICollection<RicaricheConfermate> RicaricheConfermate { get; set; }
    }
}