using Publiparking.Core.Data.BD.Base;
using Publiparking.Core.Data.SqlServer;
using Publiparking.Core.Data.SqlServer.Entities;
using Publisoftware.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.BD
{
    public class AbbonamentiPeriodiciBD : EntityBD<AbbonamentiPeriodici>
    {
        public AbbonamentiPeriodiciBD()
        {

        }

        public async static Task<IQueryable<AbbonamentiPeriodici>> GetListAsync(DbParkContext p_dbContext)
        {
            //IQueryable<AbbonamentiPeriodici> query =GetList(p_dbContext).AsAsyncQueryable<AbbonamentiPeriodici>();
            IQueryable<AbbonamentiPeriodici> query = p_dbContext.AbbonamentiPeriodici.AsQueryable();
            return query;
        }
        public static AbbonamentiPeriodici getAbbonamentoByCodice(string p_codice, DbParkContext p_dbContext)
        {
            return GetList(p_dbContext).Where(a => a.codice.Equals(p_codice)).SingleOrDefault();

        }
    }
}
