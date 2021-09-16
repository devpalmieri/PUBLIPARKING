using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Publiparking.Core.Data.BD;
using Publiparking.Core.Data.BD.BD;
using Publiparking.Core.Data.SqlServer;
using Publiparking.Core.Data.SqlServer.Entities;
using Publiparking.Web.Base;
using Publiparking.Web.Classes;
using Publiparking.Web.Classes.Extensions;
using Publiparking.Web.Configuration;
using Publiparking.Web.Models;
using Publiparking.Web.Models.Account;

namespace Publiparking.Web.Controllers
{
    public class HomeOperatoreController : UnloggedBaseController
    {
        private readonly ILogger<HomeOperatoreController> _logger;
        private IConfiguration _configurationRoot;

        private readonly DbParkContext _context;
        private readonly DatabaseConfig _databaseConfigSettings;
        public HomeOperatoreController(ILogger<HomeOperatoreController> logger, IConfiguration configurationRoot)
        {
            _logger = logger;
            _configurationRoot = configurationRoot;
        }

        public async Task<IActionResult> Index()
        {
            CambiaEnteWebViewModel modelEnte = new CambiaEnteWebViewModel();
            modelEnte.Descrizione_Ente = string.Empty;
            anagrafica_ente enteDefault = new anagrafica_ente
            {
                descrizione_ente = "Seleziona...",
                id_ente = 0,
            };
            if (modelEnte.listEnte == null)
            {
                modelEnte.listEnte = await AnagraficaEnteBD.GetParkList(dbContextGenerale).ToListAsync(); //await dbContextGenerale.anagrafica_ente.ToListAsync();
            }
            return View("Index", modelEnte);
        }
        [HttpPost]
        public JsonResult SelectEnte(int IdEnte)
        {
            ViewBag.showLoginBar = false;
            ViewBag.IsCDS = false;
            if (IdEnte > 0)
            {
                CambiaEnteWebViewModel selezionaEnteModel = new CambiaEnteWebViewModel();
                var v_ente = dbContextGenerale.anagrafica_ente.Find(IdEnte);

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
        public IActionResult Privacy()
        {
            throw new Exception("Error in Privacy View");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Route("/HomeOperatore/HandleError/{code:int}")]
        public IActionResult HandleError(int code)
        {
            ViewData["ErrorMessage"] = $"Error occurred. The ErrorCode is: {code}";
            return View($"/Views/Shared/HandleError.cshtml");
        }
        [Route("HomeOperatore/Login")]
        public IActionResult Login()
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
            return View(v_accediModel);
        }

        [HttpPost("HomeOperatore/Login")]
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
            Operatori oparatore = OperatoriBD.IsAuthenticated(dbContextGeneraleReadOnly, loginModel.UserName, loginModel.Password);

            if (oparatore != null)
            {
                string cookie_id_ente = HelperCookies.GetCookie("id_ente", this.UnLogHttpContextAccessor);
                var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, oparatore.idOperatore.ToString()),
                        new Claim(ClaimTypes.Name, oparatore.username),
                        new Claim("Password",loginModel.Password),
                        //new Claim("CFPIva",loginModel.codFiscalePIVA),
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
                if (!string.IsNullOrEmpty(cookie_id_ente))
                    claims.Add(new Claim("IdEnte", cookie_id_ente));

                claims.Add(new Claim("Token", token));
                claims.Add(new Claim("authenticated", "1"));
                claims.Add(new Claim("IsLogged", "1"));
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity)).ConfigureAwait(false);

                HttpContext.Session.SetString("JWToken", token);

                //var claim = User.Claims;

                return LocalRedirect("/HomeUtenteLogged/Index");
            }
            return Unauthorized();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        public IActionResult AccessDenied() => View();
        #region NO Actions
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
            //foreach (tab_tipo_ente t in results)
            //{
            //    if (t.id_tipo_ente == tab_tipo_ente.CONSORZIO_ID)
            //        t.desc_tipo_ente = "Ente Gestore Servizi";

            //}
            return results;
        }
        public ActionResult Contact()
        {
            InfoMailViewModel modelMail = new InfoMailViewModel();


            //ViewBag.textButton = "Accedi Utente";
            ViewBag.textButton = "Area Riservata";
            ViewBag.Sede = "legale";
            return View("Contact", modelMail);
        }

        #endregion NO Actions

    }
}
