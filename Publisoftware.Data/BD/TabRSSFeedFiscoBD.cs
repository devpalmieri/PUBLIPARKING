using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD.BD
{
    public class TabRSSFeedFiscoBD : EntityBD<tab_rssfeed_fisco>
    {
        #region Costructor
        public TabRSSFeedFiscoBD() { }
        #endregion Costructor

        #region Public Methods
        
        public static List<tab_rssfeed_fisco> GetFeeds(dbEnte p_ctx)
        {
            List<tab_rssfeed_fisco> results = GetList(p_ctx).ToList();
            return results;
        }
        #endregion Public Methods
    }
}
