using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Publiparking.Core.Data.BD;
using Publiparking.Core.Data.SqlServer.Entities;
using Publiparking.Web.Classes;
using Publiparking.Web.Classes.Helper;
using Publiparking.Web.Models.Menu;
using Publisoftware.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Publiparking.Web.Base
{
    public class LoggedBaseController : BaseController
    {
        public IHttpContextAccessor httpContextAccessor { get; set; }
        private readonly IMemoryCache _memoryCache;
        public IMemoryCache memoryCache { get; set; }
        public bool ExpiredSession { get; set; }
        public bool IsError { get; set; }
        public string Message { get; set; }

        public LoggedBaseController()
        {
            IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
            this.httpContextAccessor = _httpContextAccessor;
            this.memoryCache = _memoryCache;
            try
            {
                if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    var claimEnte = ((ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity).FindFirst("IdEnte");
                    var claimUser = ((ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity).FindFirst("IdUser");
                    var claimToken = ((ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity).FindFirst("Token");
                    if (claimToken !=null)
                    {
                        _httpContextAccessor.HttpContext.Session.SetString("JWToken", claimToken.Value );
                    }
                    if (claimEnte != null)
                    {
                        int idEnte = int.Parse(claimEnte.Value);
                        anagrafica_ente currEnte = dbContextGenerale.anagrafica_ente.Find(idEnte);
                        Sessione.SetCurrentEnte(currEnte);
                        _dbServer = currEnte.indirizzo_ip_db;
                        _dbName = currEnte.nome_db;

                        _dbUserName = currEnte.user_name_db;
                        _dbPassWord = CryptMD5.Decrypt(currEnte.password_db);
                        //_idStruttura = Sessione.getCurrentStruttura().id_struttura_aziendale;
                        //_idRisorsa = Sessione.getCurrentOperatore().id_risorsa;
                        // _idContribuentiDefaultList = Sessione.getIdContribuentiAbilitatiList();
                        //Aggiunge l'utente in Sessione
                        if (claimUser != null)
                        {
                            int idUser = int.Parse(claimUser.Value);
                            tab_utenti user = TabUtentiBD.GetById(dbContextGeneraleReadOnly, idUser);
                            if (user != null)
                                Sessione.SetUserAuthenticated(user);
                        }
                        //Carica le voci di menu abilitate
                        List<MenuNodeFirstLevel> menu = MenuHelper.GetMenu(dbContextGeneraleReadOnly, Sessione.GetCurrentEnte().id_ente);
                    }
                    else
                    {
                        this.Message = "Sessione di lavoro scaduta.";
                        this.ExpiredSession = true;
                        this.IsError = true;

                    }
                }
                else
                {
                    this.Message = "Utente non autenticato.";
                    this.ExpiredSession = true;
                    this.IsError = true;

                }
            }
            catch (Exception ex)
            {

                this.Message = string.Format("Errore in fase di creazione del contesto del settoriale. {0}", ex.Message);
                this.IsError = true;
            }
        }

        #region Uso della Sessione
        //public LoggedBaseController()
        //{
        //    IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
        //    this.httpContextAccessor = _httpContextAccessor;
        //    try
        //    {
        //        if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        //        {
        //            var claim = ((ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity).FindFirst(ClaimTypes.Name);
        //            var claim2 = ((ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity).FindFirst("IdEnte");
        //            string a = claim2.Value;

        //        }
        //        if (_httpContextAccessor.HttpContext.Session.GetString("IsLogged") != null)
        //        {
        //            if (Sessione.GetIsLogged())
        //            {
        //                anagrafica_ente currEnte = Sessione.GetCurrentEnte();
        //                _dbServer = currEnte.indirizzo_ip_db;
        //                _dbName = currEnte.nome_db;

        //                _dbUserName = currEnte.user_name_db;
        //                _dbPassWord = CryptMD5.Decrypt(currEnte.password_db);
        //                //_idStruttura = Sessione.getCurrentStruttura().id_struttura_aziendale;
        //                //_idRisorsa = Sessione.getCurrentOperatore().id_risorsa;
        //                // _idContribuentiDefaultList = Sessione.getIdContribuentiAbilitatiList();

        //                // Se autenticato provvede alla creazione
        //                //del menu orizontale
        //                //MenuOrizontale();
        //                //Carica le pagine  con Help
        //                //LoadHelp();
        //            }
        //            else
        //            {
        //                this.Message = "Sessione di lavoro scaduta.";
        //                this.ExpiredSession = true;
        //                this.IsError = true;

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        this.Message = string.Format("Errore in fase di creazione del contesto del settoriale. {0}", ex.Message);
        //        this.IsError = true;
        //    }
        //}
        #endregion Uso della Sessione

    }

}
