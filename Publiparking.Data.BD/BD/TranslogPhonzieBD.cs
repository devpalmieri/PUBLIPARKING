using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.BD
{
    public class TranslogPhonzieBD : EntityBD<translog_phonzie>
    {
        public TranslogPhonzieBD()
        {

        }

        public static IQueryable<translog_phonzie> GetListByServiceCarSenderLicenseNoSpecialID(int p_ServiceCarSender,string p_LicenseNo, int p_SpecialId, DbParkCtx p_context)
        {
            return GetList(p_context).Where(t => t.tlServiceCarSender == p_ServiceCarSender)
                                     .Where(t => t.tlLicenseNo == p_LicenseNo)
                                     .Where(t=> t.tlSpecialId == p_SpecialId);
        }

        public static IQueryable<translog_phonzie> GetListByServiceCarSenderLicenseNo(int p_ServiceCarSender, string p_LicenseNo, DbParkCtx p_context)
        {
            return GetList(p_context).Where(t => t.tlServiceCarSender == p_ServiceCarSender)
                                     .Where(t => t.tlLicenseNo == p_LicenseNo);
        }

        public static int maxIdentificativoSostaPhonzie(DbParkCtx p_dbContext)
        {
            return GetList(p_dbContext).Max(t=> t.tlSpecialId) ?? -1;
        }
    }
}
