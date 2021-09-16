using Italia.Spid.Authentication.IdP;
using Microsoft.AspNetCore.Http;
using Publiparking.Core.Data.SqlServer.Entities;
using Publiparking.Web.Classes.Helper;
using Publiparking.Web.Models.Menu;
using Publiparking.Web.Models.SpidModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publiparking.Web.Classes
{
    public class Sessione
    {
        IHttpContextAccessor _httpContextAccessor = null;

        public static string CurrentEnte = "CurrentEnte";
        public static string IsLogged = "IsLogged";

        public static string Menu = "Menu";
        public static string MenuNodes = "MenuNodes";
        public static string UserAuthenticated = "UserAuthenticated";

        public static string Cookie = "Cookie";
        public static string CookieAccept = "CookieAccept";
        public static string IsCookieAccepted = "IsCookieAccepted";

        public static string UtenteSpid = "UtenteSpid";
        public static string LoginSpidMessage = "LoginSpidMessage";
        public static string CurrentLogUtenteSpid = "CurrentLogUtenteSpid";
        public static string ListIDPSpid = "ListIDPSpid";
        public static string SpidAuthnRequest = "SpidAuthnRequest";
        public Sessione()
        {
            _httpContextAccessor = new HttpContextAccessor();
        }

        #region Ente
        public static void SetCurrentEnte(anagrafica_ente anagraficaEnte)
        {
            SessioneHelper.storeValue(CurrentEnte, anagraficaEnte);
        }

        public static anagrafica_ente GetCurrentEnte()
        {
            if (SessioneHelper.getValue<anagrafica_ente>(CurrentEnte) != null)
                return (anagrafica_ente)SessioneHelper.getValue<anagrafica_ente>(CurrentEnte);
            else
                return null;
        }
        public static void DeleteCurrentEnte()
        {
            SessioneHelper.deleteValue(CurrentEnte);
        }
        #endregion Ente

        #region SPID
        public static void SetUtenteSpid(SpidUser currUsr)
        {
            SessioneHelper.storeValue(UtenteSpid, currUsr);
        }
        public static SpidUser GetUtenteSpid()
        {
            try
            {
                //if (_httpContextAccessor.HttpContext.Session != null)
                //{
                if (SessioneHelper.getValue<SpidUser>(UtenteSpid) != null)
                    return (SpidUser)SessioneHelper.getValue<SpidUser>(UtenteSpid);
                else
                    return null;
                //}
                //else
                //    return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static void DeleteUtenteSpid()
        {
            SessioneHelper.deleteValue(UtenteSpid);
        }
        public static void SetLoginSpidMessage(string msg)
        {
            SessioneHelper.storeValue(LoginSpidMessage, msg);
        }
        public static void SetListIDP(List<IdentityProviderConfigData> listIdp)
        {
            SessioneHelper.storeValue(ListIDPSpid, listIdp);
        }
        public static List<IdentityProviderConfigData> GetListIDPd()
        {
            if (SessioneHelper.getValue<List<IdentityProviderConfigData>>(ListIDPSpid) != null)
                return (List<IdentityProviderConfigData>)SessioneHelper.getValue<List<IdentityProviderConfigData>>(ListIDPSpid);
            else
                return null;
        }
        public static void DeleteListIDP()
        {
            SessioneHelper.deleteValue(ListIDPSpid);
        }

        public static void SetSpidRequest(AuthnRequestType request)
        {
            SessioneHelper.storeValue(SpidAuthnRequest, request);
        }
        public static AuthnRequestType GetSpidRequest()
        {
            if (SessioneHelper.getValue<AuthnRequestType>(SpidAuthnRequest) != null)
                return (AuthnRequestType)SessioneHelper.getValue<AuthnRequestType>(SpidAuthnRequest);
            else
                return null;
        }
        public static void DeleteSpidRequest()
        {
            SessioneHelper.deleteValue(SpidAuthnRequest);
        }
        #endregion SPID


        #region IsLogged
        public static void SetUserAuthenticated(tab_utenti user)
        {
            SessioneHelper.storeValue(UserAuthenticated, user);
        }

        public static tab_utenti GetUserAuthenticated()
        {
            if (SessioneHelper.getValue<tab_utenti>(UserAuthenticated) != null)
                return (tab_utenti)SessioneHelper.getValue<tab_utenti>(UserAuthenticated);
            else
                return null;
        }
        public static void DeleteUserAuthenticated()
        {
            SessioneHelper.deleteValue(UserAuthenticated);
        }
        public static void SetIsLogged(bool isLogged)
        {
            SessioneHelper.storeValue(IsLogged, isLogged);
        }

        public static bool GetIsLogged()
        {
            if (SessioneHelper.getValue<bool>(IsLogged) == true)
                return true;
            else
                return false;
        }
        public static void DeleteIsLogged()
        {
            SessioneHelper.deleteValue(IsLogged);
        }
        public static string GetLoginSpidMessage()
        {
            if (SessioneHelper.getValue<string>(LoginSpidMessage) != null)
                return (string)SessioneHelper.getValue<string>(LoginSpidMessage);
            else
                return null;
        }
        public static void DeleteLoginSpidMessage()
        {
            SessioneHelper.deleteValue(LoginSpidMessage);
        }

        #endregion Ente

        #region Menu
        public static void SetCurrentMenu(List<tab_menu_primo_livello> appsEnabled)
        {
            SessioneHelper.storeValue(Menu, appsEnabled);
        }

        public static List<tab_menu_primo_livello> GetCurrentMenu()
        {
            if (SessioneHelper.getValue<List<tab_menu_primo_livello>>(Menu) != null)
                return (List<tab_menu_primo_livello>)SessioneHelper.getValue<List<tab_menu_primo_livello>>(Menu);
            else
                return null;
        }
        public static void DeleteCurrentMenu()
        {
            SessioneHelper.deleteValue(Menu);
        }

        public static void SetCurrentNodesMenu(List<MenuNodeFirstLevel> nodes)
        {
            SessioneHelper.storeValue(MenuNodes, nodes);
        }

        public static List<MenuNodeFirstLevel> GetCurrentNodesMenu()
        {
            if (SessioneHelper.getValue<List<MenuNodeFirstLevel>>(MenuNodes) != null)
                return (List<MenuNodeFirstLevel>)SessioneHelper.getValue<List<MenuNodeFirstLevel>>(MenuNodes);
            else
                return null;
        }
        public static void DeleteCurrentNodesMenu()
        {
            SessioneHelper.deleteValue(MenuNodes);
        }

        #endregion Menu

        #region Cookies & Privacy
        public static void SetPrivacy(bool accept)
        {
            SessioneHelper.storeValue(Cookie, accept);
        }

        public static bool GetPrivacy()
        {
            if (SessioneHelper.getValue<bool>(Cookie) == true)
                return true;
            else
                return false;
        }
        public static void DeletePrivacy()
        {
            SessioneHelper.deleteValue(Cookie);
        }

        public static bool GetCookieAccepted()
        {
            if (SessioneHelper.getValue<bool>(IsCookieAccepted) == true)
                return true;
            else
                return false;
        }
        public static void SetCookieAccepted(bool accept)
        {
            SessioneHelper.storeValue(IsCookieAccepted, accept);
        }
        public static void DeleteCookieAccepted()
        {
            SessioneHelper.deleteValue(IsCookieAccepted);
        }
        #endregion Cookies & Privacy
        /// <summary>
        /// Pulisce la Sessione
        /// da richiamare in fase di logout
        /// </summary>
        public static void ClearSession()
        {
            DeleteCurrentEnte();
            DeleteUtenteSpid();
            DeleteListIDP();
            DeleteSpidRequest();
            DeleteUserAuthenticated();
            DeleteIsLogged();
            DeleteLoginSpidMessage();
            DeleteCurrentNodesMenu();
            DeleteCurrentMenu();
            DeletePrivacy();
            DeleteCookieAccepted();
        }
    }

}
