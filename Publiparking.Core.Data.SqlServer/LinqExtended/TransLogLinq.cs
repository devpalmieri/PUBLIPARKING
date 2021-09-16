using Publiparking.Core.Data.SqlServer.dto;
using Publiparking.Core.Data.SqlServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.SqlServer.LinqExtended
{
    public static class TransLogLinq
    {
        public static IQueryable<TransLogDTO> ToDTO(this IQueryable<translog> iniziale)
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
                serviceCarSender = d.tlServiceCarSender.HasValue ? d.tlServiceCarSender.Value.ToString() : string.Empty,
                tlPDM_TicketNo_union = d.tlPDM_TicketNo_union,
                recordType = d.tlRecordType.HasValue ? d.tlRecordType.Value : -1
            }).AsQueryable();
        }
    }

}
