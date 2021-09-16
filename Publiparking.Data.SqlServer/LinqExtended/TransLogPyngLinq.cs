using Publiparking.Data.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.LinqExtended
{
    public static class TransLogPyngLinq
    {
        public static IQueryable<TransLogDTO> ToTranslogDTO(this IQueryable<translog_pyng> iniziale)
        {
            return iniziale.ToList().Select(d => new TransLogDTO
            {
                idTicket = d.tlRecordID,
                idPDM = d.tlPDM.HasValue ? d.tlPDM.Value : -1,
                idPayType = d.tlPayType.HasValue ? d.tlPayType.Value : -1,
                payDateTime = d.tlPayDateTime.HasValue ? d.tlPayDateTime.Value : DateTime.Now.AddDays(-10),
                expDateTime = d.tlExpDateTime.HasValue ? d.tlExpDateTime.Value : DateTime.Now.AddDays(-10),
                amount = d.tlAmount.HasValue ? d.tlAmount.Value : 0,
                ticketNumber = d.tlTicketNo.HasValue ? d.tlTicketNo.Value : -1,
                licenseNumber = d.tlLicenseNo,
                parkingSpaceNumber = !string.IsNullOrEmpty(d.tlParkingSpaceNo) ? Int32.Parse(d.tlParkingSpaceNo) : 0,
                idServiceType = d.tlUserType.HasValue ? d.tlUserType.Value : -1,
                note = "",
                specialId = d.tlSpecialId.HasValue ? d.tlSpecialId.Value : -1,
                parkingSpaceNo = d.tlParkingSpaceNo,
                serviceCarSender = d.tlServiceCarSender,
                tlPDM_TicketNo_union = string.Format("{0},{1}", (d.tlPDM.HasValue ? d.tlPDM.Value : 0), (d.tlTicketNo.HasValue ? d.tlTicketNo.Value : 0)),
                recordType = 0
            }).AsQueryable();
        }
    }
}
