//using Publisoftware.Data;
//using Publisoftware.Data.BD;
//using Publisoftware.Data.LinqExtended;
//using Publisoftware.Web.MVCPortal.Base.Controllers;
//using Publisoftware.Web.MVCPortal.Classes.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Publiparking.Web.Classes.Filters
{
    //    public class LoggedFilterAttribute : FilterAttribute, IActionFilter
    //    {
    //        #region "METODI PRIVATI"
    //        /// <summary>
    //        /// Verifica che esista almeno un privilegio abilitato per la terna (risorsa, struttura, ente) in sessione...
    //        /// Accesso consentito se ha una abilitazione diretta sulla applicazione corrente OPPURE se l'applicazione corrente è flaggata di "sistema"
    //        /// </summary>
    //        /// <param name="filterContext"></param>
    //        /// <returns></returns>
    //        private bool checkPriviledge(ActionExecutingContext filterContext)
    //        {
    //            bool ret = false;

    //            LoggedBaseController loggedController = (LoggedBaseController)filterContext.Controller;

    //            if (loggedController.currApplicazione == null) return false;
    //            if (Sessione.getCurrentOperatore() == null || Sessione.getCurrentStruttura() == null || Sessione.getCurrentEnte() == null) return false;

    //            if (Sessione.getCurrentApplicazioniAbilitate() == null || Sessione.getCurrentApplicazioniAbilitate().Count() == 0)
    //            {
    //                //Elenco delle applicazioni abilitate
    //                IQueryable<tab_applicazioni> v_ApplicazioniAbilitate = TabApplicazioniBD.GetEnableApplicationList(
    //                                                                    Sessione.getCurrentOperatore().id_risorsa,
    //                                                                    Sessione.getCurrentStruttura().id_struttura_aziendale,
    //                                                                    Sessione.getCurrentEnte().id_ente,
    //                                                                    Sessione.getCurrentModalitaOp(),
    //                                                                    Sessione.getCurrentLivelloAut(),
    //                                                                    loggedController.dbContextReadOnly);

    //                Sessione.createCurrentApplicazioniAbilitate(v_ApplicazioniAbilitate.ToList());
    //            }

    //            ret = Sessione.getCurrentApplicazioniAbilitate().Any(s => s.FullCode.Equals(loggedController.currApplicazione.FullCode));

    //            return ret;
    //        }

    //        private void buildToolBarButtons(ActionExecutingContext filterContext)
    //        {
    //            LoggedBaseController loggedController = (LoggedBaseController)filterContext.Controller;

    //            if (filterContext.IsChildAction && filterContext.ParentActionViewContext.Controller is LoggedBaseController)
    //            {
    //                loggedController.toolbarButtons = ((LoggedBaseController)filterContext.ParentActionViewContext.Controller).toolbarButtons;
    //                return;
    //            }

    //            var app = loggedController.currApplicazione;
    //            List<int> v_idAbilitate = Sessione.getCurrentApplicazioniAbilitate().Select(a => a.id_tab_applicazioni).ToList();

    //#if EFFETTUARE_QUERY_TAB_APPLICAZIONI
    //            ICollection<join_applicazioni_link> joinApplicazioniLink = app.join_applicazioni_link;
    //#else
    //            ICollection<join_applicazioni_link> joinApplicazioniLink;
    //            if (!app.join_applicazioni_linkDicByIdTabApplicazioni.TryGetValue(app.id_tab_applicazioni, out joinApplicazioniLink))
    //            {
    //                loggedController.toolbarButtons = new List<ToolBarButton>();
    //            }
    //            else
    //#endif
    //            {
    //                loggedController.toolbarButtons = joinApplicazioniLink//app.join_applicazioni_link
    //                                                    .Where(d => v_idAbilitate.Contains(d.id_tab_applicazioni_link))
    //                                                    .OrderBy(d => d.ordine)
    //                                                    .Select(d => new ToolBarButton { Label = d.label, FullCode = d.tab_applicazioni1.FullCode, ToolTip = d.tab_applicazioni1.tooltip, SelezioneBloccante = d.selezione_richiesta/*, ButtonVisible=!d.tab_applicazioni1.tab_abilitazione.FirstOrDefault().flag_abilitato*/ })
    //                                                    .ToList();

    //                if (Sessione.getCurrentHistory() != null)
    //                {
    //                    foreach (Sessione.HistoryPoint v_point in Sessione.getCurrentHistory())
    //                    {
    //                        if (!v_point.FullCodeDestinazione.Contains(app.FullCode))
    //                        {
    //                            if (v_point.FullCodeOrigine == null || 
    //                                v_point.FullCodeOrigine.Count() == 0 ||
    //                                v_point.FullCodeOrigine.Any(o => o.Contains(app.FullCode)))
    //                            {
    //                                var toolBarButton = new ToolBarButton
    //                                {
    //                                    Label = v_point.Label,
    //                                    FullCode = v_point.FullCodeDestinazione,
    //                                    ToolTip = "",
    //                                    SelezioneBloccante = false
    //                                };
    //                                toolBarButton.SetPostParams(v_point.PostParams);
    //                                loggedController.toolbarButtons.Add(toolBarButton);
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        }

    //        private void builReportsToolBarButtons(ActionExecutingContext filterContext)
    //        {
    //            LoggedBaseController loggedController = (LoggedBaseController)filterContext.Controller;

    //            if (filterContext.IsChildAction && filterContext.ParentActionViewContext.Controller is LoggedBaseController)
    //            {
    //                loggedController.toolbarButtons_Reports = ((LoggedBaseController)filterContext.ParentActionViewContext.Controller).toolbarButtons_Reports;
    //                return;
    //            }

    //            var app = loggedController.currApplicazione;

    //#if EFFETTUARE_QUERY_TAB_APPLICAZIONI
    //            loggedController.toolbarButtons_Reports = app.join_report_link.OrderBy(j => j.ordine).Select(j => new ToolBarButton { Label = j.label, FullCode = "./System/" + j.tab_report.controller + "/" + j.tab_report.action }).ToList();
    //#else
    //            loggedController.toolbarButtons_Reports = app.toolbarButtons_Reports;
    //#endif
    //        }

    //        private void addDBparams(ActionExecutingContext filterContext)
    //        {
    //            LoggedBaseController loggedController = (LoggedBaseController)filterContext.Controller;

    //            var app = loggedController.currApplicazione;

    //            //TODO: Davide esclusione parametri GET
    //            Dictionary<string, string> DBparams = tab_applicazioni.GetUrlParameters(app.parametri_url);
    //            foreach (string key in DBparams.Keys)
    //            {
    //                if (key.Substring(0, 1) == "%")
    //                {
    //                    filterContext.ActionParameters.Remove(key);
    //                    filterContext.ActionParameters.Remove(key.Substring(1, (key.Length - 1)));
    //                    filterContext.ActionParameters.Add(key.Substring(1, (key.Length - 1)), Int32.Parse(DBparams[key]));
    //                }
    //                else if (key.Substring(0, 1) == "$")
    //                {
    //                    filterContext.ActionParameters.Remove(key);
    //                    filterContext.ActionParameters.Remove(key.Substring(1, (key.Length - 1)));
    //                    filterContext.ActionParameters.Add(key.Substring(1, (key.Length - 1)), Boolean.Parse(DBparams[key]));
    //                }
    //                else
    //                {
    //                    filterContext.ActionParameters[key] = DBparams[key];
    //                }
    //            }
    //        }

    //        #region "RECUPERO APPLICAZIONE PER RICHIESTA CORRENTE"
    //        private string getAppCodeFromURI(Uri url, ActionExecutingContext filterContext)
    //        {
    //            string ret = String.Empty;

    //            if (url != null)
    //            {
    //                string absolutePath = url.AbsolutePath;
    //                string applicationPath = filterContext.HttpContext.Request.ApplicationPath;
    //                var regex = new Regex(Regex.Escape(applicationPath));
    //                var urlSegmentToParse = applicationPath != "/" ? regex.Replace(absolutePath, "", 1) : absolutePath;

    //                //App corrente a partire da URL richiesta
    //                string[] uriParts = urlSegmentToParse.Split('/');

    //                if (uriParts.Length > 1)
    //                    ret = uriParts[1];
    //            }

    //            return ret;
    //        }

    //#if EFFETTUARE_QUERY_TAB_APPLICAZIONI

    //        /// <summary>
    //        /// Recupera il fullcode dell applicazione a partire dalla richiesta consultando prima l URL diretto, poi il refererURL
    //        /// </summary>
    //        /// <param name="filterContext"></param>
    //        /// <returns></returns>
    //        private tab_applicazioni recoverCurrentAppFromRequest(ActionExecutingContext filterContext)
    //        {
    //            LoggedBaseController loggedController = (LoggedBaseController)filterContext.Controller;

    //            tab_applicazioni app = TabApplicazioniBD.GetByFullCode(getAppCodeFromURI(filterContext.HttpContext.Request.Url, filterContext), loggedController.dbContext);

    //            //Tenta recupero da referrer nell'header della richiesta HTTP (non è detto che ci sia)
    //            if (app == null)
    //            {
    //                app = TabApplicazioniBD.GetByFullCode(getAppCodeFromURI(filterContext.HttpContext.Request.UrlReferrer, filterContext), loggedController.dbContext);
    //            }

    //            return app;
    //        }
    //#else
    //        private RouteConfig.TabApplicazioneRouteCfg recoverCurrentAppFromRequestUsingRouting(ActionExecutingContext filterContext)
    //        {
    //            LoggedBaseController loggedController = (LoggedBaseController)filterContext.Controller;

    //            //tab_applicazioni app = TabApplicazioniBD.GetByFullCode(getAppCodeFromURI(filterContext.HttpContext.Request.Url, filterContext), loggedController.dbContext);
    //            var app = RouteConfig.TabApplicazioneOrNull(getAppCodeFromURI(filterContext.HttpContext.Request.Url, filterContext));

    //            //Tenta recupero da referrer nell'header della richiesta HTTP (non è detto che ci sia)
    //            if (app == null)
    //            {
    //                //app = TabApplicazioniBD.GetByFullCode(getAppCodeFromURI(filterContext.HttpContext.Request.UrlReferrer, filterContext), loggedController.dbContext);
    //                app = RouteConfig.TabApplicazioneOrNull(getAppCodeFromURI(filterContext.HttpContext.Request.UrlReferrer, filterContext));
    //            }

    //            return app;
    //        }
    //#endif

    //        #endregion

    //        #region "GESTIONE REDIRECT"
    //        /// <summary>
    //        /// Prepara il risultato all'interno del contesto per eseguire il redirect a seconda della richiesta (Ajax e non)
    //        /// </summary>
    //        /// <param name="parameters">Dictionary con i parametri da accodare nel redirect</param>
    //        private void PrepareResponseRedirect(ActionExecutingContext filterContext, string controller, string action, int unused = (int)System.Net.HttpStatusCode.BadRequest, RouteValueDictionary parameters = null)
    //        {

    //            if (filterContext.HttpContext.Request.IsAjaxRequest())
    //            {
    //                string redURL = new Uri(filterContext.HttpContext.Request.Url, VirtualPathUtility.ToAbsolute(String.Format("~/{0}/{1}", controller, action))).AbsoluteUri;
    //                var url = new Uri(filterContext.HttpContext.Request.Url, VirtualPathUtility.ToAbsolute(String.Format("~/{0}/{1}", controller, action)));

    //                if (parameters != null && parameters.Keys.Count > 0)
    //                {
    //                    redURL = redURL + "?";
    //                    foreach (string key in parameters.Keys)
    //                    {
    //                        redURL = redURL + String.Format("{0}={1}&", key, parameters[key]);
    //                    }
    //                    redURL = redURL.TrimEnd(new char[] { '&' });
    //                }

    //                JsonResult result = new JsonResult();
    //                filterContext.HttpContext.Response.Clear();
    //                // -----------------------------------------------------------------------------------------------------
    //                // Attenzione: se modifichi lo StatusCode devi modificare "accordingly" anche il codice in Title.cshtml
    //                filterContext.HttpContext.Response.StatusCode = 419; //Authentication Timeout [ma non è standard HTTP!]
    //                // -----------------------------------------------------------------------------------------------------
    //                result.Data = new { redUrl = redURL };
    //                filterContext.Result = result;
    //            }
    //            else
    //            {
    //                RouteValueDictionary rDict = new RouteValueDictionary();
    //                rDict.Add("Controller", controller);
    //                rDict.Add("Action", action);

    //                if (parameters != null)
    //                {
    //                    foreach (string key in parameters.Keys)
    //                    {
    //                        if (!rDict.ContainsKey(key))
    //                        {
    //                            rDict.Add(key, parameters[key]);
    //                        }
    //                    }
    //                }

    //                filterContext.Result = new RedirectToRouteResult(rDict);
    //            }

    //        }

    //        /// <summary>
    //        /// Prepara il risultato all'interno del contesto per eseguire il redirect a seconda della richiesta (Ajax e non)
    //        /// </summary>
    //        /// <param name="parameters">Dictionary con i parametri da accodare nel redirect</param>
    //        /// <remarks>Prima di utilizzarla CONTROLLARE CHE LA RICHIESTA NON È AJAX</remarks>
    //        private void PrepareResponseRedirectByFullcode(ActionExecutingContext filterContext, string fullCode, string action, RouteValueDictionary parameters = null)
    //        {
    //            RouteValueDictionary rDic;
    //            if (parameters != null && parameters.Keys.Count > 0)
    //            {
    //                rDic = new RouteValueDictionary(parameters);
    //            }
    //            else
    //            {
    //                rDic = new RouteValueDictionary();
    //            }
    //            rDic["action"] = action;

    //            if (filterContext.HttpContext.Request.IsAjaxRequest())
    //            {
    //                // TODO: microottimizzazione: fare a meno di UrlHelper!
    //                var urlHelper = new UrlHelper(filterContext.RequestContext);
    //                var redURL = urlHelper.RouteUrl(fullCode, rDic);

    //                JsonResult result = new JsonResult();
    //                filterContext.HttpContext.Response.Clear();
    //                // -----------------------------------------------------------------------------------------------------
    //                // Attenzione: se modifichi lo StatusCode devi modificare "accordingly" anche il codice in Title.cshtml
    //                filterContext.HttpContext.Response.StatusCode = 419; //Authentication Timeout [ma non è standard HTTP!]
    //                // -----------------------------------------------------------------------------------------------------
    //                result.Data = new { redUrl = redURL };
    //                filterContext.Result = result;
    //            }
    //            else
    //            {
    //                filterContext.Result = new RedirectToRouteResult(fullCode, rDic);
    //            }

    //        }

    //        private void PrepareResponseRedirectToExpSession(ActionExecutingContext filterContext)
    //        {
    //            // TODO: usare PrepareResponseRedirectByFullcode
    //            PrepareResponseRedirect(filterContext, "AlertPages", "SessionExpired", 419);
    //        }

    //        private void PrepareResponseRedirectToAccDenied(ActionExecutingContext filterContext, string appFullCode = "")
    //        {
    //            // TODO: usare PrepareResponseRedirectByFullcode
    //            PrepareResponseRedirect(filterContext, "AlertPages", "AccessDenied", (int)System.Net.HttpStatusCode.Forbidden, new RouteValueDictionary { { "AppCode", appFullCode } });
    //        }
    //        #endregion

    //        #region "GESTIONE VIEW BAG"
    //        private void SetViewBagOperatore(ActionExecutingContext filterContext)
    //        {
    //            //OPERATORE SELEZIONATO => operatore logged in (NON CHILD)
    //            if (Sessione.getCurrentOperatore() != null)
    //            {
    //                filterContext.Controller.ViewBag.StrutturaLabel = Sessione.getTranslationFor("StrutturaLabel");
    //                filterContext.Controller.ViewBag.StrutturaDescr = (Sessione.getCurrentStruttura() != null) ? Sessione.getCurrentStruttura().descr_struttura : "";

    //                filterContext.Controller.ViewBag.OperatoreLabel = Sessione.getTranslationFor("OperatoreLabel");
    //                filterContext.Controller.ViewBag.OperatoreName = (Sessione.getCurrentOperatore() != null) ? Sessione.getCurrentOperatore().username : "";

    //                ModalitaOperativaEnum modalitaOp = Sessione.getCurrentModalitaOp();
    //                filterContext.Controller.ViewBag.ModalitaOp = modalitaOp;
    //#if DEBUG
    //                if (modalitaOp == ModalitaOperativaEnum.ALL)
    //                {
    //                    //throw new ApplicationException("Modalità operativa non può essere \"ALL\"");
    //                }
    //#endif
    //                string modOp = String.Format("{0}", (char)modalitaOp);
    //                string modalitaOperativaDescrizione = ModalitaOperativa.ModOpDictionaryDescrizione[modOp];
    //                filterContext.Controller.ViewBag.ModOperativaLabel = Sessione.getTranslationFor("ModOperativaLabel");
    //                filterContext.Controller.ViewBag.ModalitaOperativa = modalitaOperativaDescrizione;

    //                //Show Hide barra operatore a seconda della risorsa (generica e non) (NON CHILD)
    //                if (Sessione.getCurrentOperatore().IsGenerica)
    //                {
    //                    filterContext.Controller.ViewBag.ShowOperatoreBar = false;
    //                }
    //                else
    //                {
    //                    filterContext.Controller.ViewBag.ShowOperatoreBar = true;
    //                }
    //            }
    //        }

    //        private void SetViewBagUtente(ActionExecutingContext filterContext)
    //        {
    //            //Ripristino dati utente selezionato (NON CHILD)
    //            tab_utenti currUtente = Sessione.getCurrentUtente();
    //            if (currUtente != null)
    //            {
    //                filterContext.Controller.ViewBag.UtenteLabel =string.Format("Benvenuto {0}", Sessione.getTranslationFor("UtenteLabel"));
    //                filterContext.Controller.ViewBag.UtenteName = currUtente.nome_utente;
    //            }
    //        }

    //        private void SetViewBagContribuente(ActionExecutingContext filterContext)
    //        {
    //            //Ripristino dati contribuente selezionato (NON CHILD)
    //            tab_contribuente currContrib = Sessione.getCurrentContribuente();
    //            if (currContrib != null)
    //            {
    //                filterContext.Controller.ViewBag.ContribuenteLabel = Sessione.getTranslationFor("ContribuenteLabel");
    //                filterContext.Controller.ViewBag.ContribuenteName = currContrib.isPersonaFisica ? String.Format("{0} {1} CF: {2}", currContrib.cognome, currContrib.nome, currContrib.cod_fiscale) : String.Format("{0} P.Iva: {1}", currContrib.rag_sociale, currContrib.p_iva);
    //                filterContext.Controller.ViewBag.SelContribuente = currContrib;
    //                filterContext.Controller.ViewBag.CodiceContribuente = currContrib.id_anag_contribuente;
    //            }
    //        }

    //        private void SetViewBagTerzo(ActionExecutingContext filterContext)
    //        {
    //            //Ripristino dati contribuente selezionato (NON CHILD)
    //            tab_terzo currTerzo = Sessione.getCurrentTerzo();
    //            if (currTerzo != null)
    //            {
    //                filterContext.Controller.ViewBag.TerzoLabel = Sessione.getTranslationFor("TerzoLabel");
    //                filterContext.Controller.ViewBag.TerzoName = currTerzo.isPersonaFisica ? String.Format("{0} {1} CF: {2}", currTerzo.cognome, currTerzo.nome, currTerzo.cod_fiscale) : String.Format("{0} P.Iva: {1}", currTerzo.rag_sociale, currTerzo.p_iva);
    //                filterContext.Controller.ViewBag.SelTerzo = currTerzo;
    //                filterContext.Controller.ViewBag.CodiceTerzo = currTerzo.id_terzo;
    //            }
    //        }

    //        private void SetViewBagEnte(ActionExecutingContext filterContext)
    //        {
    //            //Riprisinto dati ente (NON CHILD)
    //            filterContext.Controller.ViewBag.EnteLabel = Sessione.getTranslationFor("EnteLabel");
    //            filterContext.Controller.ViewBag.EnteDescrizione = (Sessione.getCurrentEnte() != null) ? Sessione.getCurrentEnte().descrizione_ente : "";
    //        }

    //        private void SetViewBagNonChild(ActionExecutingContext filterContext)
    //        {
    //            LoggedBaseController loggedController = (LoggedBaseController)filterContext.Controller;

    //            //Ripristino variabili viewbag per la nuova richiesta (NON CHILD)
    //            filterContext.Controller.ViewBag.ContattaciLabel = Sessione.getTranslationFor("ContattaciLabel");
    //            filterContext.Controller.ViewBag.logo_ente_img = loggedController.buildImgUrl("logo_ente.png");
    //            filterContext.Controller.ViewBag.isLogged = true;


    //            //DOC 15/07/2015 - Menu sempre nascosto: utente lo apre quando vuole cambiare pagina
    //            //filterContext.Controller.ViewBag.isSxSidebarVisible = Sessione.getSxsidebarVisibility();
    //            filterContext.Controller.ViewBag.isSxSidebarVisible = false;

    //            SetViewBagOperatore(filterContext);
    //            SetViewBagUtente(filterContext);
    //            SetViewBagContribuente(filterContext);
    //            SetViewBagTerzo(filterContext);
    //            SetViewBagEnte(filterContext);
    //        }

    //        private void SetViewBagIndietro(ActionExecutingContext filterContext)
    //        {
    //            //Gestione bottone indietro => app code di ritorno
    //            string appPath = filterContext.RequestContext.HttpContext.Request.ApplicationPath;
    //            if (filterContext.RequestContext.HttpContext.Request.UrlReferrer != null)
    //            {

    //                string computedFullCode = getAppCodeFromURI(filterContext.RequestContext.HttpContext.Request.UrlReferrer, filterContext);
    //                LoggedBaseController loggedController = (LoggedBaseController)filterContext.Controller;

    //#if EFFETTUARE_QUERY_TAB_APPLICAZIONI
    //                tab_applicazioni app = TabApplicazioniBD.GetByFullCode(computedFullCode, loggedController.dbContext);
    //#else
    //                // 23/02/2017: sta controllando solo se esiste il full code, query inutile.
    //                var app = RouteConfig.FirstOrDefault(computedFullCode);
    //#if DEBUG
    //                if (app != null)
    //                {
    //                    var ctrl = RouteConfig.DefaultControllerOrNull(app);
    //                    var acti = RouteConfig.DefaultActionOrNull(app);
    //                    var bothAC = RouteConfig.DefaultControllerAndActionOrNull(app);
    //                }
    //#endif
    //#endif
    //                //tab_applicazioni app = TabApplicazioniBD.GetByFullCode(computedFullCode, loggedController.dbContext);

    //                if (app != null)
    //                {
    //                    filterContext.Controller.ViewBag.Indietro = computedFullCode;
    //                }
    //                else
    //                {
    //                    if (Sessione.getCurrentOperatore().IsGenerica)
    //                    {
    //                        filterContext.Controller.ViewBag.Indietro = RouteConfig.HOME_UTENTE;
    //                    }
    //                    else
    //                    {
    //                        filterContext.Controller.ViewBag.Indietro = RouteConfig.HOME_OPERATORE;
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                if (Sessione.getCurrentOperatore()?.IsGenerica == false)
    //                {
    //                    filterContext.Controller.ViewBag.Indietro = RouteConfig.HOME_OPERATORE;
    //                }
    //                else
    //                {
    //                    filterContext.Controller.ViewBag.Indietro = RouteConfig.HOME_UTENTE;
    //                }
    //            }
    //        }
    //        /// <summary>
    //        /// Verifica e gestisce la visualizzazione
    //        /// del pulsante relativo all'Help
    //        /// </summary>
    //        /// <param name="filterContext"></param>
    //        private void SetViewBagHelp(ActionExecutingContext filterContext)
    //        {
    //            //Gestione bottone indietro => app code di ritorno
    //            string appPath = filterContext.RequestContext.HttpContext.Request.ApplicationPath;
    //            if (filterContext.RequestContext.HttpContext.Request.UrlReferrer != null)
    //            {

    //                string computedFullCode = getAppCodeFromURI(filterContext.RequestContext.HttpContext.Request.UrlReferrer, filterContext);
    //                LoggedBaseController loggedController = (LoggedBaseController)filterContext.Controller;

    //                string currController = loggedController.ControllerName;
    //                string action = filterContext.ActionDescriptor.ActionName;

    //                List<tab_help_pagine> listHelp = Sessione.getPagineHelp();
    //                if (listHelp != null)
    //                {
    //                    tab_help_pagine help = listHelp.Where(x => x.Controller == currController && x.Action == action).FirstOrDefault();
    //                    if (help != null)
    //                    {
    //                        filterContext.Controller.ViewBag.IsVisibleHelp = true;
    //                        filterContext.Controller.ViewBag.HelpTitle = help.Title;
    //                        filterContext.Controller.ViewBag.HelpContent = help.TextContent;
    //                        filterContext.Controller.ViewBag.HelpType =GenericConsts.MODAL_TYPE_INFO;
    //                    }
    //                }
    //            }

    //        }

    //        private bool CheckSezioniUtente(ActionExecutingContext filterContext, out string appName)
    //        {
    //            //Gestione bottone indietro => app code di ritorno
    //            appName = "";
    //            string appPath = filterContext.RequestContext.HttpContext.Request.ApplicationPath;
    //            if (filterContext.RequestContext.HttpContext.Request.UrlReferrer != null)
    //            {

    //                string computedFullCode = getAppCodeFromURI(filterContext.RequestContext.HttpContext.Request.UrlReferrer, filterContext);
    //                LoggedBaseController loggedController = (LoggedBaseController)filterContext.Controller;

    //                string currController = loggedController.ControllerName;
    //                string action = filterContext.ActionDescriptor.ActionName;

    //                if (currController.ToLower()=="sezioniutenteweb")
    //                {
    //                    if (action.ToLower() == "contact")
    //                    {
    //                        appName = "Contatti";
    //                        return true;
    //                    }
    //                    else if (action.ToLower() == "About")
    //                    {
    //                        appName = "Chisiamo";
    //                        return true;
    //                    }
    //                    else
    //                        return false;

    //                }


    //            }
    //            return false;

    //        }


    //        #endregion "GESTIONE VIEW BAG"

    //        /// <summary>
    //        /// Log della richiesta
    //        /// </summary>
    //        /// <param name="filterContext"></param>
    //        private void loggaRichiesta(ActionExecutingContext filterContext)
    //        {
    //            //LOG 
    //            if (!filterContext.IsChildAction && !filterContext.HttpContext.Request.IsAjaxRequest())
    //            //if (app.FullCode == uriParts[0] && filterContext.ActionDescriptor != null && filterContext.ActionDescriptor.ControllerDescriptor != null
    //            //     && filterContext.ActionDescriptor.ControllerDescriptor.ControllerName == app.tab_pagine.controller)
    //            {
    //                LoggedBaseController loggedController = (LoggedBaseController)filterContext.Controller;

    //                if (loggedController.currApplicazione == null) return;

    //                if (Sessione.getCurrentIdLogOperatore() != null)
    //                {
    //                    LogHelper.LogOperazioniOperatore(loggedController.currApplicazione.id_tab_applicazioni, loggedController.dbContextGenerale);
    //                    loggedController.dbContextGenerale.SaveChanges();
    //                }
    //                else if (Sessione.getCurrentIdLogUtente() != null)
    //                {
    //                    LogHelper.LogOperazioniUtente(loggedController.currApplicazione.id_tab_applicazioni, loggedController.dbContextGenerale);
    //                    loggedController.dbContextGenerale.SaveChanges();
    //                }
    //            }
    //        }
    //        #endregion

    ////        public void OnActionExecuted(ActionExecutedContext filterContext)
    ////        {
    ////            if (filterContext.Controller is LoggedBaseController)
    ////            {
    ////                if (Sessione.isLogged())
    ////                {
    ////#if DEBUG
    ////                    // Uso in cshtml:
    ////                    // C# 
    ////                    //      @{ string flagDebug = (ViewData["DEBUG"] as bool?) == true ? "1" : "0"; }
    ////                    // JS:
    ////                    //      (....).FLAG_DEBUG = "@flagDebug"; // Che sarà "1" o "0"
    ////                    filterContext.Controller.ViewData["DEBUG"] = true;
    ////#endif
    ////                    LoggedBaseController loggedController = (LoggedBaseController)filterContext.Controller;
    ////                    filterContext.Controller.ViewBag.toolbarButtons = loggedController.toolbarButtons;
    ////                    filterContext.Controller.ViewBag.toolbarButtons_Reports = loggedController.toolbarButtons_Reports;
    ////                }
    ////            }
    ////        }


    ////        public void OnActionExecuting(ActionExecutingContext filterContext)
    ////        {
    ////            filterContext.Controller.ViewBag.logo_ente_img = String.Empty;

    ////            if (filterContext.Controller is LoggedBaseController)
    ////            {
    ////                LoggedBaseController loggedController = (LoggedBaseController)filterContext.Controller;

    ////                #region "Verifica Sessione"
    ////                HttpSessionStateBase session = filterContext.HttpContext.Session;
    ////                //Se la sessione è scaduta o l'utente non è loggato per la FormsAuth => redirect pagina sessionexpired
    ////                if (session.IsNewSession || System.Web.HttpContext.Current.User == null || !System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
    ////                {
    ////                    //if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
    ////                    //    System.Web.Security.FormsAuthentication.SignOut();
    ////                    //Redirect pagina sessione scaduta
    ////                    PrepareResponseRedirectToExpSession(filterContext);
    ////                    return;
    ////                }
    ////                #endregion

    ////                //Caricamento traduzioni una tantum (SE non ho caricato il menu è la prima richiesta da loggato)
    ////                if (Sessione.getMenuAppAbilitate() == null) { loggedController.loadSessionTranslations(); }

    ////                //Setta variabili ViewBag per azioni principali (NON child e NON ajax)
    ////                if (!filterContext.IsChildAction && !filterContext.HttpContext.Request.IsAjaxRequest()) { 
    ////                    SetViewBagNonChild(filterContext); }

    ////                //Setta viewbag.indietro in funzione del refererURL
    ////                SetViewBagIndietro(filterContext);

    ////                //-------------------------Button Help----------------------------------
    ////                //SetViewBagHelp(filterContext);

    ////                // ---------------------------------------------------------------------.

    ////                // -----------------------------------------------------------
    ////                // Pietro: navigazione (History) manuale
    ////                // if (!filterContext.HttpContext.Request.IsAjaxRequest())
    ////                // {
    ////                //     bool shouldForget = Sessione.ShouldForgetNavigationPrevActionRecords();
    ////                //     if (shouldForget)
    ////                //     {
    ////                //         Sessione.ClearNavigationPrevActionRecords();
    ////                //     }
    ////                //     Sessione.SetForgetNavigationPrevActionRecords(); // !!
    ////                // }
    ////                // -----------------------------------------------------------

    ////                #region "Prova traduzione proprietà entity"
    ////                //Dictionary<string, string> enteTrans = new Dictionary<string, string>();
    ////                //if (Sessione.getCurrentEnte() != null)
    ////                //{
    ////                //    enteTrans = loggedController.langDataDictionaryFor("anagrafica_ente", Sessione.getCurrentEnte().id_ente.ToString());
    ////                //}
    ////                //filterContext.Controller.ViewBag.EnteDescrizione = (Sessione.getCurrentEnte() != null) ? Sessione.getCurrentEnte().translateObjProperty(x => x.descrizione_ente, enteTrans) : "";
    ////                #endregion

    ////                //Recupera applicazione legata alla richiesta corrente
    ////#if EFFETTUARE_QUERY_TAB_APPLICAZIONI
    ////                tab_applicazioni app = recoverCurrentAppFromRequest(filterContext);
    ////#else
    ////                var app = recoverCurrentAppFromRequestUsingRouting(filterContext);

    ////                var v_len = filterContext.HttpContext.Request.Url.ToString().Substring(filterContext.HttpContext.Request.Url.ToString().IndexOf(app.FullCode) + app.FullCode.Length).Length == 0;
    ////#endif

    ////                //Se non riesco a recuperare una app dalla richiesta => nego l'accesso
    ////                if (app == null)
    ////                {
    ////                    //TODO: Davide LOG ERRORI - Errore funzionale
    ////                    PrepareResponseRedirectToAccDenied(filterContext);
    ////                    return;
    ////                }

    ////                string appName = string.Empty;
    ////                bool checkApp = CheckSezioniUtente(filterContext, out appName);
    ////                //Setta applicazione corrente
    ////                loggedController.currApplicazione = app;
    ////                if (checkApp)
    ////                    filterContext.Controller.ViewBag.TitoloApplicazione = appName;
    ////                else
    ////                    filterContext.Controller.ViewBag.TitoloApplicazione = app.label_menu;
    ////                filterContext.Controller.ViewBag.FullCode = app.FullCode;

    ////                //LOG
    ////                loggaRichiesta(filterContext);

    ////                //Verifica privilegi su app corrente o referer
    ////                bool hasPriviledges = checkPriviledge(filterContext);

    ////                if (!hasPriviledges)
    ////                {
    ////                    PrepareResponseRedirectToAccDenied(filterContext, app.FullCode);
    ////                    return;
    ////                }

    ////                //Svuota il contribuente in sessione quando richiesto
    ////                if (v_len && 
    ////                    app != null && 
    ////                    !app.contribuente_required && 
    ////                    app.flag_visualizazione && 
    ////                    Sessione.getCurrentContribuente() != null &&
    ////                    Sessione.getCurrentModalitaOp() == ModalitaOperativaEnum.BackOffice)
    ////                {
    ////                    Sessione.deleteCurrentContribuente();
    ////                    Sessione.deleteCurrentIstanza();
    ////                    filterContext.Controller.ViewBag.ContribuenteName = null;
    ////                    filterContext.Controller.ViewBag.CodiceContribuente = null;
    ////                }

    ////                //Svuota il terzo in sessione quando richiesto
    ////                if (v_len && 
    ////                    app != null && 
    ////                    !app.terzo_required && 
    ////                    app.flag_visualizazione &&
    ////                    Sessione.getCurrentTerzo() != null &&
    ////                    Sessione.getCurrentModalitaOp() == ModalitaOperativaEnum.BackOffice)
    ////                {
    ////                    Sessione.deleteCurrentTerzo();
    ////                    filterContext.Controller.ViewBag.TerzoName = null;
    ////                    filterContext.Controller.ViewBag.CodiceTerzo = null;
    ////                }

    ////                //Il dottore ha voluto nel caso in cui non ci fosse in sessione il contribuente, ma fosse richiesto, il redirect a "Ricerca Contribuente"
    ////                if (v_len && 
    ////                    app != null && 
    ////                    app.contribuente_required && 
    ////                    !app.terzo_required && 
    ////                    app.flag_visualizazione && 
    ////                    Sessione.getCurrentModalitaOp() == ModalitaOperativaEnum.BackOffice && 
    ////                    Sessione.getCurrentContribuente() == null)
    ////                {
    ////                    Sessione.addHistory(app.FullCode, app.label_menu, RouteConfig.DETTAGLIO_CONTRIBUENTE);
    ////                    PrepareResponseRedirectByFullcode(filterContext, RouteConfig.RICERCA_CONTRIBUENTE, "RicercaContribuente");
    ////                }

    ////                //Il dottore ha voluto nel caso in cui non ci fosse in sessione il terzo, ma fosse richiesto, il redirect a "Ricerca Terzo"
    ////                if (v_len && 
    ////                    app != null && 
    ////                    app.terzo_required && 
    ////                    !app.contribuente_required &&
    ////                    app.flag_visualizazione && 
    ////                    Sessione.getCurrentModalitaOp() == ModalitaOperativaEnum.BackOffice && 
    ////                    Sessione.getCurrentTerzo() == null)
    ////                {
    ////                    Sessione.addHistory(app.FullCode, app.label_menu, RouteConfig.DETTAGLIO_TERZO);
    ////                    PrepareResponseRedirectByFullcode(filterContext, RouteConfig.RICERCA_DEL_TERZO_DEBITORE, "RicercaTerzi");
    ////                }

    ////                //tab_applicazioni1 -> tab_applicazioni_link
    ////                //Carica bottoniera
    ////                buildToolBarButtons(filterContext);

    ////                //Carica bottoniera reports
    ////                builReportsToolBarButtons(filterContext);

    ////                //Gestione Parametri provenienti da DB
    ////                addDBparams(filterContext);
    ////            }

    ////        }
    //    }
}