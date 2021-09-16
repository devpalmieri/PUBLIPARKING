using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Publiparking.Core.Data.BD;
using Publiparking.Core.Data.SqlServer;
using Publiparking.Core.Data.SqlServer.Entities;
using Publiparking.Web.Base;
using Publiparking.Web.Classes;
using Publiparking.Web.Classes.Consts;
using Publiparking.Web.Classes.Enumerator;
using Publiparking.Web.Classes.Extensions;
using Publiparking.Web.Classes.Filters;
using Publiparking.Web.Configuration;
using Publiparking.Web.Models;
using Publiparking.Web.Models.Account;
using Publiparking.Web.Models.oAuth2;
using Publiparking.Web.Token;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Publiparking.Web.Controllers
{
    [Tracking]
    public class HomeUtenteController : UnloggedBaseController
    {

        #region Not Used Implementation
        //private readonly ILogger<HomeUtenteController> _logger;
        //private readonly DbParkContext _context;
        //private readonly DatabaseConfig _databaseConfigSettings;

        //private readonly UserManager<User> _userManager;
        //private readonly IMapper _mapper;
        //private readonly RoleManager<Role> _roleManager;
        //private readonly JwtSettings _jwtSettings;
        //private IConfiguration _configurationRoot;
        //private readonly string COOKIE_ACCOUNT = "COOKIE_ACCOUNT";
        //public HomeUtenteController(IMapper mapper,
        //   UserManager<User> userManager,
        //   RoleManager<Role> roleManager,
        //   IConfiguration configurationRoot,
        //   IOptionsSnapshot<JwtSettings> jwtSettings,
        //   ILogger<HomeUtenteController> logger)
        //{
        //    _mapper = mapper;
        //    _userManager = userManager;
        //    _roleManager = roleManager;
        //    _jwtSettings = jwtSettings.Value;
        //    _configurationRoot = configurationRoot;
        //    _logger = logger;
        //}
        #endregion Not Used Implementation

        #region Private Members
        private readonly ILogger<HomeUtenteController> _logger;
        private IConfiguration _configurationRoot;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _env;
        private readonly CookieManager.ICookieManager _cookieManager;
        private readonly CookieManager.ICookie _cookie;
        #endregion Private Members

        #region Costructor
        public HomeUtenteController(
            ILogger<HomeUtenteController> logger,
            IConfiguration configurationRoot,
            IHttpContextAccessor httpContextAccessor,
            IHostingEnvironment env, CookieManager.ICookieManager cookieManager, CookieManager.ICookie cookie)
        {
            _logger = logger;
            _configurationRoot = configurationRoot;
            this._httpContextAccessor = httpContextAccessor;
            _env = env;
            this._cookieManager = cookieManager;
            this._cookie = cookie;
        }
        #endregion Costructor

        #region Actions
        public async Task<IActionResult> Index()
        {
            ViewBag.PathInfoPrivacyPDF = string.Empty;
            ViewBag.RemoveCookieConsenso = string.Empty;

            #region Esempio gestione Cookies con CookieManager
            //ParkCookie cooObj = new ParkCookie()
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Indentifier = "valueasgrsdgdf66514sdfgsd51d65s31g5dsg1rs5dg",
            //    Date = DateTime.Now
            //};
            //_cookieManager.Set("Key1", cooObj, 100000);

            //-----------------------------------------------------------------
            //ParkCookie myCook = _cookieManager.GetOrSet<ParkCookie>("Key2", () =>
            //{
            //    return new ParkCookie()
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Indentifier = "valueasgrsdgdf66514sdfgsd51d65s31g5dsg1rs5dg",
            //        Date = DateTime.Now
            //    };

            //}, new CookieOptions() { HttpOnly = true, Expires = DateTime.Now.AddDays(1) });
            #endregion Esempio gestione Cookies con CookieManager
            await Task.Delay(ParkConsts.DELAY_TIME).ConfigureAwait(false);
            return View("Index");
        }

        [HttpPost]
        public JsonResult SelectEnte(int IdEnte)
        {
            ViewBag.showLoginBar = false;
            ViewBag.IsCDS = false;
            if (IdEnte > 0)
            {
                CambiaEnteWebViewModel selezionaEnteModel = new CambiaEnteWebViewModel();
                anagrafica_ente v_ente = AnagraficaEnteBD.GetById(dbContextGeneraleReadOnly, IdEnte);
                string valueAccountCookie = UnLogHttpContextAccessor.HttpContext.Request.Cookies["id_ente"];// Request.Cookies.Get(SPID_COOKIE) ?? new HttpCookie(SPID_COOKIE);
                HelperCookies.SetCookie("id_ente", IdEnte.ToString(), UnLogHttpContextAccessor, 20);

                HttpContext.Session.SetComplexData("CurrentEnte", v_ente);
                HttpContext.Session.SetString("IsLogged", "1");
                selezionaEnteModel.selEnteId = IdEnte;
                string v_result = v_ente.url_ente + ";" + v_ente.descrizione_ente;
                return Json(v_result);
            }
            else
                return null;
        }
        public async Task<IActionResult> Privacy()
        {
            //ESEMPIO Message
            //string a = "0";
            //if (a == "0")
            //{
            //    ShowMessageBox("wwwwwwwwwwwwwwwwwwwwwwwwwwwwww", MessageTypeEnum.success, false);
            //    return View();
            //}
            await Task.Delay(ParkConsts.DELAY_TIME).ConfigureAwait(false);
            return View();
        }
        public async Task<IActionResult> Appointments()
        {
            await Task.Delay(ParkConsts.DELAY_TIME).ConfigureAwait(false);
            return View();
        }
        public async Task<IActionResult> Esperienza()
        {
            await Task.Delay(ParkConsts.DELAY_TIME).ConfigureAwait(false);
            return View();
        }
        public async Task<IActionResult> About()
        {
            string _PATH_PUBLIPARKING_PREFIX = "Publiparking/";
            string rootPath = string.Empty;
            string pdfPath = string.Empty;
            string url_cod_etico = string.Empty;
            string url_cod_antimafia = string.Empty;
            tab_path_portale param = await TabPathPortaleBD.GetByModeAsync(dbContextReadOnly, this.Ambiente);
            rootPath = param.path_download;
            if (!String.IsNullOrEmpty(rootPath))
                pdfPath = rootPath + _PATH_PUBLIPARKING_PREFIX;// + "CodiceEtico_PUBLIPARKING.pdf";

            if (string.IsNullOrEmpty(pdfPath))
            {
                ViewBag.messageType = MessageTypeEnum.error;
                ViewBag.message = ParkConsts.ERR_PDF_PATH_EMPTY;
                return View();
            }
            url_cod_antimafia = pdfPath + "CodiceAntimafia_PUBLIPARKING.pdf";
            url_cod_etico = pdfPath + "CodiceEtico_PUBLIPARKING.pdf";
            ViewBag.PathPDFCodAntimafica = url_cod_antimafia;
            ViewBag.PathPDFCodEtico = url_cod_etico;
            return View();
        }
        public async Task<IActionResult> ModPagamento()
        {
            await Task.Delay(ParkConsts.DELAY_TIME).ConfigureAwait(false);
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        [Route("HomeUtente/Login")]
        public async Task<IActionResult> Login()
        {
            LoginViewModel v_accediModel = await GetLoginModel();

            return View(v_accediModel);
        }

        [HttpPost("HomeUtente/Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            #region Comment
            //var user = _userManager.Users.SingleOrDefault(u => u.UserName == userLoginResource.Email);
            //if (user is null)
            //{
            //    return NotFound("User not found");
            //}

            //var userSigninResult = await _userManager.CheckPasswordAsync(user, userLoginResource.Password);

            //if (userSigninResult)
            //{
            //    var roles = await _userManager.GetRolesAsync(user);
            //    string token = GenerateJwt(user, roles);
            #endregion Comment
            int v_idente = loginModel.selEnteId;
            tab_utenti user = TabUtentiBD.IsAuthenticated(dbContextGeneraleReadOnly, loginModel.UserName, loginModel.Password);
            Sessione.SetIsLogged(false);
            if (user == null)
            {
                //ViewBag.ModalMessage = ParkConsts.ERR_LOGIN_FAILLURE;
                ShowMessageBox(ParkConsts.ERR_LOGIN_FAILLURE, MessageTypeEnum.error);
                ViewBag.IsFormVisible = true;
                ViewBag.HasEnte = false;
                if (loginModel.selEnteId > 0)
                    ViewBag.HasEnte = true;
                loginModel = await GetLoginModel();
                loginModel.selEnteId = v_idente;
                return View(loginModel);
            }
            if (user != null)
            {
                //string cookie_id_ente = HelperCookies.GetCookie("id_ente", this.UnLogHttpContextAccessor);
                var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.id_utente.ToString()),
                        new Claim(ClaimTypes.Name, user.nome_utente),
                        new Claim("IdUser",user.id_utente.ToString()),
                        new Claim("Password",loginModel.Password),
                        new Claim("CFPIva",loginModel.codFiscalePIVA),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationRoot["JwtSecurityToken:Key"]));
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _configurationRoot["JwtSecurityToken:Issuer"],
                    audience: _configurationRoot["JwtSecurityToken:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: signingCredentials
                    );
                string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                if (loginModel.selEnteId > 0)
                    claims.Add(new Claim("IdEnte", loginModel.selEnteId.ToString())); ;

                claims.Add(new Claim("Token", token));
                claims.Add(new Claim("authenticated", "1"));
                claims.Add(new Claim("IsLogged", "1"));
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity)).ConfigureAwait(false);

                HttpContext.Session.SetString("JWToken", token);
                Sessione.SetIsLogged(true);
                return LocalRedirect("~/HomeUtenteLogged/Index");
            }
            return Unauthorized();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            //return Redirect("/");
            Sessione.ClearSession();
            return LocalRedirect("~/HomeUtente/Index");
        }

        public IActionResult AccessDenied() => View();

        #region Error

        /// <summary>
        /// Disabilitato.
        ///Gli errori vengono gestiti automaticamente dopo l'autenticazione
        /// </summary>
        /// <returns></returns>
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
        [Route("/HomeUtente/HandleErrorNoLOgged/{code:int}")]
        public IActionResult HandleError(int code)
        {
            ViewData["ErrorMessage"] = $"Error occurred. The ErrorCode is: {code}";
            return View($"/Views/Shared/HandleErrorNoLOgged.cshtml");
        }
        #endregion Error

        #region COOKIE & PRIVACY
        public ActionResult AccettaPrivacy()
        {
            string pdfPath = @"/Documents/Privacy/doc_privacy.pdf";

            if (string.IsNullOrEmpty(pdfPath))
            {
                ShowMessageBox(ParkConsts.ERR_PDF_PATH_EMPTY, MessageTypeEnum.error, false);
                return View("Index"); ;
            }
            ViewBag.PathInfoPrivacyPDF = pdfPath;
            return View("Index"); ;

        }
        public ActionResult PubliparkingPrivacy()
        {
            string pdfPath = @"/Documents/Privacy/Privacy_Policy_Publiparking_1_0.pdf";

            if (string.IsNullOrEmpty(pdfPath))
            {
                ShowMessageBox(ParkConsts.ERR_PDF_PATH_EMPTY, MessageTypeEnum.error, false);
                return View("Index"); ;
            }
            ViewBag.PathInfoPrivacyPDF = pdfPath;
            return View("Index"); ;

        }
        [HttpGet]
        public ActionResult ViewPrivacy(int? id = 0, string cod_ente = "", bool isNecessari = false, bool isPreferenze = false, bool isStatistiche = false)
        {
            Sessione.SetPrivacy(true);
            string consenso = ParkConsts.CONSENSO_ALL;
            if ((isNecessari) || (isPreferenze) || (isStatistiche))
                consenso = ParkConsts.CONSENSO_PARTIAL;
            tab_registro_cookie v_cookie = GetDataRequest(this.HttpContext, consenso, isNecessari, isPreferenze, isStatistiche);
            if (v_cookie != null)
                Sessione.SetCookieAccepted(true);
            //return View("Index");
            return LocalRedirect("~/HomeUtente/Index");
        }
        [HttpGet]
        public ActionResult ClosePrivacy(int? id = 0, string cod_ente = "")
        {
            Sessione.SetPrivacy(false);
            string consenso = ParkConsts.CONSENSO_DENIED;
            tab_registro_cookie v_cookie = GetDataRequest(this.HttpContext, consenso, false, false, false);
            if (v_cookie != null)
                Sessione.SetCookieAccepted(true);
            return View("Index");
        }
        public ActionResult RemoveConsenso()
        {
            if (this.HttpContext.Request.Cookies["firstVisit"] != null)
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(-1);
                this.HttpContext.Response.Cookies.Append("firstVisit", "true");
            }
            if (this.HttpContext.Request.Cookies["visited"] != null)
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(-1);
                this.HttpContext.Response.Cookies.Append("visited", "true");
            }

            ViewBag.RemoveCookieConsenso = "remove";
            return View("Index");
        }
        [HttpPost]
        public ActionResult RemoveCookieConsenso()
        {
            string ip_address = string.Empty;
            if (this.HttpContext.Connection.RemoteIpAddress != null)
            {
                System.Net.IPAddress ip = this.HttpContext.Connection.RemoteIpAddress;
                ip_address = ip.ToString();
            }
            //if (string.IsNullOrEmpty(ip_address))
            //    ip_address = Request.ServerVariables["REMOTE_ADDR"];

            tab_registro_cookie v_cookie = TabRegistroCookieBD.GetCookieByIPAddress(ip_address, dbContextGenerale);
            if (v_cookie != null)
            {
                //dbContextGenerale.tab_registro_cookie.Remove(v_cookie);
                v_cookie.consenso = ParkConsts.CONSENSO_DENIED;
                v_cookie.consenso_necessari = false;
                v_cookie.consenso_preferenze = false;
                v_cookie.consenso_statistiche = false;
                dbContextGenerale.Entry(v_cookie).State = EntityState.Modified;
                dbContextGenerale.SaveChanges();
                Sessione.SetPrivacy(false);
                Sessione.DeleteCookieAccepted();
            }
            ViewBag.ControllerName = "HomeUtente";
            ViewBag.ActionName = "Index";
            return LocalRedirect("~/HomeUtente/Index");
        }
        #endregion COOKIE & PRIVACY

        #region Spid
        /// <summary>
        /// Carica la finestra per 
        /// l'autenticazione Spid
        /// </summary>
        /// <param name="isLogOut"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AccediSpid(bool isLogOut = false)
        {
            return View("AccediSpid");
        }
        /// <summary>
        /// Mostra la lista
        /// degli IDP
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewIDPList()
        {
            string controller = "HomeUtente";
            ViewBag.CallController = controller;
            return PartialView("_IDPList");
        }
        /// <summary>
        /// Esegue il Redirect
        /// alla pagina di login Spid
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IDPList()
        {
            return RedirectToAction("SpidLogin");
        }
        /// <summary>
        /// Viene caricata la pagina
        /// per il login dello SPID
        /// </summary>
        /// <param name="isLogOut"></param>
        /// <returns></returns>
        public ActionResult SpidLogin()
        {
            ViewBag.Logged = false;
            if (Sessione.GetUtenteSpid() != null)
            {
                ViewBag.Logged = true;
            }
            ViewBag.ControllerName = "HomeUtente";
            ViewBag.ActionName = "Index";

            return View();
        }
        #endregion Spid
        #endregion Actions

        #region Private Methods
        private tab_registro_cookie GetDataRequest(HttpContext httpContext, string p_consenso = "",
          bool p_consenso_necessari = false, bool p_consenso_preferenze = false, bool p_consenso_statistiche = false)
        {
            string keyFirstVisit = "firstVisit";
            string keyVisited = "visited";
            if (httpContext.Request.Cookies["visited"] == null && httpContext.Request.Cookies["firstVisit"] == null)
            {
                //PRIMA VISITA AL PORTALE
                // Il cookie viene rimosso quando si chiude il browser
                httpContext.Response.Cookies.Append(keyFirstVisit, "true");

                // Il cookie non viene mai rimosso
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddYears(100);
                httpContext.Response.Cookies.Append(keyVisited, "true", option);
            }
            // Verifica lo stato del cookie
            if (httpContext.Request.Cookies[keyFirstVisit] != null)
            {
                //E' la prima visita al sito. Non viene visualizzato il banner
                // Response.Write("Questa è la prima visita al sito.<br />");
            }
            else
            {
                Sessione.SetPrivacy(true);
                // Response.Write("Hai già visitato il sito.<br />");
            }
            string ip_address = string.Empty;
            if (httpContext.Connection.RemoteIpAddress != null)
            {
                System.Net.IPAddress ip = httpContext.Connection.RemoteIpAddress;
                ip_address = ip.ToString();
            }

            tab_registro_cookie v_cookie = TabRegistroCookieBD.GetCookieByIPAddress(ip_address, dbContextGenerale);
            string sessionId = httpContext.Session.Id;
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            string headerString = "{";
            foreach (var header in httpContext.Request.Headers)
            {
                headerString += header.Key + " : " + header.Value + ", ";
                requestHeaders.Add(header.Key, header.Value);
            }
            headerString = headerString.TrimEnd(',', ' ') + "}";
            if (v_cookie == null)
            {
                v_cookie = new tab_registro_cookie();
                //Prima visita utente
                v_cookie.indirizzo_ip = ip_address;
                v_cookie.session_id = sessionId;
                v_cookie.headers = headerString;
                v_cookie.data_prima_visita = DateTime.Now;
                v_cookie.consenso = p_consenso;
                v_cookie.consenso_necessari = p_consenso_necessari;
                v_cookie.consenso_preferenze = p_consenso_preferenze;
                v_cookie.consenso_statistiche = p_consenso_statistiche;
                int id_rec_cookie = 0;
                TabRegistroCookieBD.SaveCookieUser(v_cookie, dbContextGenerale, out id_rec_cookie);
            }
            else
            {
                //Non è la prima visita
                Sessione.SetPrivacy(true);
                if (p_consenso.ToUpper() == ParkConsts.CONSENSO_ALL)
                    v_cookie.consenso = p_consenso;

                if (p_consenso.ToUpper() == ParkConsts.CONSENSO_PARTIAL)
                {
                    v_cookie.consenso = p_consenso;
                    v_cookie.consenso_necessari = p_consenso_necessari;
                    v_cookie.consenso_preferenze = p_consenso_preferenze;
                    v_cookie.consenso_statistiche = p_consenso_statistiche;
                }
                v_cookie.data_ultima_visita = DateTime.Now;
                dbContextGenerale.Entry(v_cookie).State = EntityState.Modified;
            }
            return v_cookie;
        }
        #endregion Private Methods

        #region NO Actions
        /// <summary>
        /// Inizializza il model
        /// per l'Autenticazione
        /// </summary>
        /// <returns></returns>
        [NonAction]
        private async Task<LoginViewModel> GetLoginModel()
        {
            LoginViewModel v_accediModel = new LoginViewModel();
            ViewBag.IsFormVisible = true;
            ViewBag.HasEnte = true;

            ViewBag.Url_Ente = string.Empty;
            v_accediModel.Descrizione_Ente = string.Empty;
            //PREPARO GLI ITEM PER LE DROPDOWN
            //TIPO ENTE ED ENTE
            v_accediModel.listTipoEnte = GetTipologiaEnte();
            tab_tipo_ente tipoEnteDefault = new tab_tipo_ente
            {
                desc_tipo_ente = "Seleziona...",
                id_tipo_ente = -1
            };
            anagrafica_ente enteDefault = new anagrafica_ente
            {
                descrizione_ente = "Seleziona...",
                id_ente = 0,
            };
            if (Sessione.GetCurrentEnte() != null)
            {
                anagrafica_ente v_ente = Sessione.GetCurrentEnte();
                ViewBag.Url_Ente = v_ente.url_ente;
                if (v_accediModel.listEnte == null)
                {
                    v_accediModel.listEnte = AnagraficaEnteBD.GetParkList(dbContextReadOnly)
                        .Where(x => x.id_tipo_ente == v_ente.id_tipo_ente).ToList();

                }
                v_accediModel.listEnte.Add(enteDefault);
                v_accediModel.Descrizione_Ente = v_ente.descrizione_ente;
                v_accediModel.selEnteId = v_ente.id_ente;
                v_accediModel.Url_Ente = v_ente.url_ente;
                v_accediModel.selected_id_tipo_ente = v_ente.id_tipo_ente.Value;
            }
            else
            {
                v_accediModel.Url_Ente = string.Empty;
                v_accediModel.selected_id_tipo_ente = tipoEnteDefault.id_tipo_ente;
                v_accediModel.listTipoEnte.Add(tipoEnteDefault);
                if (v_accediModel.listEnte == null)
                {
                    v_accediModel.listEnte = new List<anagrafica_ente>();
                    v_accediModel.selEnteId = enteDefault.id_ente;
                    v_accediModel.listEnte.Add(enteDefault);
                }
            }
            return v_accediModel;
        }

        /// <summary>
        /// Carica l'elenco della
        /// tipologia di enti
        /// </summary>
        /// <returns></returns>
        [NonAction]
        private List<tab_tipo_ente> GetTipologiaEnte()
        {
            List<tab_tipo_ente> results = new List<tab_tipo_ente>();
            List<int> IdsTipoEnte = new List<int>()
            {
                tab_tipo_ente.COMUNE_ENTE_ID, tab_tipo_ente.REGIONE_ID
            };
            results = TabTipoEnteBD.GetList(dbContextGeneraleReadOnly)
                .Where(x => IdsTipoEnte.Contains(x.id_tipo_ente))
                .ToList();

            return results;
        }
        public ActionResult Contact()
        {
            InfoMailViewModel modelMail = new InfoMailViewModel();
            ViewBag.textButton = "Area Riservata";
            ViewBag.Sede = "legale";
            return View("Contact", modelMail);
        }

        #endregion NO Actions
    }
}
