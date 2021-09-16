//using Publisoftware.Utility.Crypto;
//using Publisoftware.Web.MVCPortal.Classes.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Publiparking.Web.Classes.Filters
{
    // ref. https://onallthingsweb.wordpress.com/2015/01/20/asp-net-mvc-seamless-routing-encryption/
    //      (il suo però no compila)
    public class EncryptionActionAttribute : ActionFilterAttribute
    {
        //public /*override*/ void OnActionExecutingOld(ActionExecutingContext filterContext)
        //{
        //    // TODO: Pietro: gestire qui eccezioni o lasciarle scorrere?
        //    // TODO: Usare RouteValueDictionaryHelper.GetQueryStringFromUrl (codice ripetuto li e qui)

        //    var url = filterContext.HttpContext.Request.Url.ToString();
        //    var index = url.IndexOf(RouteValueDictionaryHelper.EncodedQSParamNameEQ);
        //    var encQsIndex = index + RouteValueDictionaryHelper.SkipEncodedQSParamNameEQ;
        //    if (index > 0 && encQsIndex < url.Length)
        //    {
        //        // --------------------------------------------------------------------------------
        //        // Inutile: già UrlDecoded
        //        //string encodedQS = url.Substring(encQsIndex);
        //        //string encQSText = filterContext.HttpContext.Server.UrlDecode(encodedQS);
        //        // --------------------------------------------------------------------------------

        //        string encQSText = url.Substring(encQsIndex);
        //        var groups = RouteValueDictionaryHelper.GetDecriptedQS(encQSText).Split('&');
        //        var actionParams = filterContext.ActionDescriptor.GetParameters();

        //        //Parse all of the "KEY=VALUE" groups
        //        foreach (var group in groups)
        //        {
        //            string[] pair = group.Split('=');

        //            if (pair.Length < 2) // param without value
        //            {
        //                continue;
        //            }

        //            //Make sure the action has the parameter of the given name
        //            var actionParam = actionParams.FirstOrDefault(i => i.ParameterName == pair[0]);
        //            if (actionParam != null)
        //            {
        //                var nullType = Nullable.GetUnderlyingType(actionParam.ParameterType);

        //                //If a nullable type, make sure to use changetype for that type instead; 
        //                //nullable types are not supported
        //                if (nullType != null)
        //                {
        //                    filterContext.ActionParameters[pair[0]] = Convert.ChangeType(pair[1], nullType);
        //                }
        //                //Otherwise, assign and cast the value accordingly
        //                else
        //                {
        //                    filterContext.ActionParameters[pair[0]] =
        //                         Convert.ChangeType(pair[1], actionParam.ParameterType);
        //                }
        //            }
        //        }
        //    } // index > 0
        //} // OnActionExecutingOld

        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    // TODO: Pietro: gestire qui eccezioni o lasciarle scorrere?
        //    // TODO: Usare RouteValueDictionaryHelper.GetQueryStringFromUrl (codice ripetuto li e qui)

        //    var httpCtx = filterContext.HttpContext;
        //    var url = httpCtx.Request.Url.ToString();
        //    var qs = RouteValueDictionaryHelper.GetQueryStringFromUrl(url, httpCtx, true);
        //    if (qs == String.Empty) { return; }
        //    var groups = qs.Split('&');
        //    var actionParams = filterContext.ActionDescriptor.GetParameters();

        //    //Parse all of the "KEY=VALUE" groups
        //    foreach (var group in groups)
        //    {
        //        string[] pair = group.Split('=');

        //        if (pair.Length < 2) // param without value
        //        {
        //            continue;
        //        }

        //        //Make sure the action has the parameter of the given name
        //        var actionParam = actionParams.FirstOrDefault(i => i.ParameterName == pair[0]);
        //        if (actionParam != null)
        //        {
        //            var nullType = Nullable.GetUnderlyingType(actionParam.ParameterType);

        //            //If a nullable type, make sure to use changetype for that type instead; 
        //            //nullable types are not supported
        //            if (nullType != null)
        //            {
        //                filterContext.ActionParameters[pair[0]] = Convert.ChangeType(pair[1], nullType);
        //            }
        //            //Otherwise, assign and cast the value accordingly
        //            else
        //            {
        //                filterContext.ActionParameters[pair[0]] =
        //                     Convert.ChangeType(pair[1], actionParam.ParameterType);
        //            }
        //        }
        //    }
        //} // OnActionExecuting
    } // class
} //ns
