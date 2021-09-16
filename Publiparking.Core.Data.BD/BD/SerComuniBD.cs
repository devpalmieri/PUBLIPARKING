using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publiparking.Core.Data.BD.Base;
using Publiparking.Core.Data.SqlServer.Entities;
using Publiparking.Core.Data.SqlServer;

namespace Publiparking.Core.Data.BD
{
    public class SerComuniBD : EntityBD<ser_comuni>
    {
        public SerComuniBD()
        {

        }
        public static IQueryable<ser_comuni> GetListAttivi(DbParkContext p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetList(p_dbContext).Where(a => a.f_comune_sto == 0);
        }
    }
}
