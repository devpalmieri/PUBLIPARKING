using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Publiparking.Core.Data.BD;
using Publiparking.Core.Data.SqlServer;
using Publiparking.Core.Data.SqlServer.Entities;
using Publiparking.Web.Base;
using Publiparking.Web.Classes.Consts;
using Publiparking.Web.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publiparking.Web.Classes.Filters
{
    public class TrackingAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log("OnActionExecuted", filterContext.RouteData);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log("OnActionExecuting", filterContext.RouteData);
            //Recupero di UnloggedBaseController
            //UnloggedBaseController unloggedController = (UnloggedBaseController)filterContext.Controller;

            //Recupero del servizio CookieManager
            CookieManager.ICookieManager _cookieManager = (CookieManager.ICookieManager)filterContext.HttpContext.RequestServices.GetService(typeof(CookieManager.ICookieManager));
            DbParkContext ctx = StartDatabaseSetting.GetGeneraleCtx();
            if ((filterContext.HttpContext != null) && (ctx != null))
            {
                tab_registro_cookie v_cookie = CheckRequest(filterContext.HttpContext, ctx);
            }
        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Log("OnResultExecuted", filterContext.RouteData);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Log("OnResultExecuting ", filterContext.RouteData);
        }

        private void Log(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = String.Format("{0}- controller:{1} action:{2}", methodName,
                                                                        controllerName,
                                                                        actionName);
            //Debug.WriteLine(message);
        }

        #region Private Methods
        private tab_registro_cookie CheckRequest(HttpContext httpContext, DbParkContext p_context)
        {
            Sessione.DeleteCookieAccepted();
            string keyFirstVisit = "firstVisit";
            string keyVisited = "visited";
            if (httpContext.Request.Cookies[keyVisited] == null && httpContext.Request.Cookies[keyFirstVisit] == null)
            {
                //PRIMA VISITA AL PORTALE
                // Il cookie viene rimosso quando si chiude il browser
                httpContext.Response.Cookies.Append(keyFirstVisit, "true");
                Sessione.SetPrivacy(false);

                // Il cookie non viene mai rimosso
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddYears(100);
                httpContext.Response.Cookies.Append(keyVisited, "true", option);
            }
            #region Previous Version
            //if (httpContext.Request.Cookies["visited"] == null && httpContext.Request.Cookies["firstVisit"] == null)
            //{
            //    //PRIMA VISITA AL PORTALE
            //    // Il cookie viene rimosso quando si chiude il browser
            //    httpContext.Response.Cookies.Append(keyFirstVisit, "true");

            //    // Il cookie non viene mai rimosso
            //    CookieOptions option = new CookieOptions();
            //    option.Expires = DateTime.Now.AddYears(100);
            //    httpContext.Response.Cookies.Append(keyVisited, "true", option);
            //}
            //// Verifica lo stato del cookie
            //if (httpContext.Request.Cookies[keyFirstVisit] != null)
            //{
            //    Sessione.SetPrivacy(false);
            //    //E' la prima visita al sito. Non viene visualizzato il banner
            //    // Response.Write("Questa è la prima visita al sito.<br />");
            //}
            //else
            //{
            //    Sessione.SetPrivacy(true);
            //    // Response.Write("Hai già visitato il sito.<br />");
            //}
            #endregion Previous Version
            string ip_address = string.Empty;
            if (httpContext.Connection.RemoteIpAddress != null)
            {
                System.Net.IPAddress ip = httpContext.Connection.RemoteIpAddress;
                ip_address = ip.ToString();
            }

            tab_registro_cookie v_cookie = TabRegistroCookieBD.GetCookieByIPAddress(ip_address, p_context);

            if (v_cookie == null)
            {
                #region Not Implementated
                //v_cookie = new tab_registro_cookie();
                ////Prima visita utente
                //v_cookie.indirizzo_ip = ip_address;
                //v_cookie.session_id = request.HttpContext.Session.SessionID != null ? request.HttpContext.Session.SessionID.TrimStart().Trim().TrimEnd() : "";
                //v_cookie.headers = request.HttpContext.Request.Headers != null ? request.HttpContext.Request.Headers.ToString() : "";
                //v_cookie.data_prima_visita = DateTime.Now;
                //v_cookie.consenso = p_consenso;
                //v_cookie.consenso_necessari = p_consenso_necessari;
                //v_cookie.consenso_preferenze = p_consenso_preferenze;
                //v_cookie.consenso_statistiche = p_consenso_statistiche;
                //int id_rec_cookie = 0;
                //TabRegistroCookieBD.SaveCookieUser(v_cookie, dbContextGenerale, out id_rec_cookie);
                #endregion Not Implementated
            }
            else
            {
                bool v_accepet = true;
                if (!string.IsNullOrEmpty(v_cookie.consenso))
                {
                    if ((v_cookie.consenso.ToUpper() == ParkConsts.CONSENSO_ALL) ||
                        (v_cookie.consenso.ToUpper() == ParkConsts.CONSENSO_PARTIAL))
                    {
                        Sessione.SetCookieAccepted(true);
                    }
                    else
                        v_accepet = false;
                }
                else
                    v_accepet = false;
                //Non è la prima visita
                Sessione.SetPrivacy(v_accepet);
                v_cookie.data_ultima_visita = DateTime.Now;
                p_context.Entry(v_cookie).State = EntityState.Modified;
                p_context.SaveChanges();
            }
            return v_cookie;
        }

        #endregion Private Methods
    }
}
