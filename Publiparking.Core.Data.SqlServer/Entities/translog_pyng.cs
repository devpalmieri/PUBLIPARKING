using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Publiparking.Core.Data.SqlServer.Entities
{
    public partial class translog_pyng
    {
        [Key]
        public int tlRecordID { get; set; }
        public int? tlTracer { get; set; }
        public int? tlPDM { get; set; }
        public int? tlPDM_Sender { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? tlDatePC { get; set; }
        public int? tlRecordType { get; set; }
        public int? tlPayType { get; set; }
        public int? tlUserType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? tlPayDateTime { get; set; }
        public int? tlAmount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? tlExpDateTime { get; set; }
        public int? tlTicketNo { get; set; }
        [StringLength(20)]
        public string tlID { get; set; }
        [StringLength(3)]
        public string tlCurrency { get; set; }
        public int? tlOk { get; set; }
        public long? tlSityControlID { get; set; }
        public int? tlSityControlLine { get; set; }
        [StringLength(12)]
        public string tlLicenseNo { get; set; }
        public long? tlPDM_Id { get; set; }
        public int? tlCreditCardFee { get; set; }
        public bool? tlIsOnline { get; set; }
        public int? tlServiceCar { get; set; }
        public int? tlMoneyCar { get; set; }
        public string tlServiceCarSender { get; set; }
        public int? tlMoneyCarSender { get; set; }
        [StringLength(20)]
        public string tlPSAM { get; set; }
        public int? tlSpecialId { get; set; }
        [StringLength(50)]
        public string tlParkingSpaceNo { get; set; }
        [StringLength(4)]
        public string tlValidThru { get; set; }
        [StringLength(1)]
        public string flag_on_off { get; set; }
    }
}