using System.Web;
using System.Web.Mvc;

namespace Publiparking.Web.NinoServer
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
