using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Publiparking.Core.Data.BD;
using Publiparking.Core.Data.SqlServer.Entities;
using Publiparking.Web.Base;
using Publiparking.Web.Classes;
using Publiparking.Web.Classes.Consts;
using Publiparking.Web.Classes.Enumerator;
using Publiparking.Web.Configuration;
using Publiparking.Web.Configuration.ValueProvider;
using Publiparking.Web.Models;
using Publiparking.Web.Models.Account;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
namespace Publiparking.Web.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Authorize(Policy = "OnlyTest")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class HomeUtenteLoggedController : LoggedBaseController
    {
        #region Private Members
        private readonly ILogger<HomeUtenteLoggedController> _logger;
        private CryptoParamsProtector _protector;
        #endregion Private Members

        #region Costructor
        public HomeUtenteLoggedController(ILogger<HomeUtenteLoggedController> logger,
            CryptoParamsProtector protector)
        {
            _protector = protector;
            _logger = logger;
        }
        #endregion Costructor

        #region Action
        [Authorize()]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize()]
        public IActionResult About()
        {
            return View();
        }
        [Authorize()]
        public IActionResult Esperienza()
        {
            return View();
        }
        [Authorize()]
        public IActionResult Privacy()
        {
            //ESEMPIO ALERT
            //string a = "a";
            //if (a == "a")
            //{
            //    ViewBag.Alert = MessageServices.ShowAlert(Alerts.Success, "Operazione convlusa con successo!");
            //}
            return View();
        }
        [Authorize("Authenticated")]
        public IActionResult ModPagamento()
        {
            return View("ModPagamento");
        }
        [Authorize()]
        public ActionResult Contact()
        {
            InfoMailViewModel modelMail = new InfoMailViewModel();
            ViewBag.Sede = "legale";
            return View("Contact", modelMail);
        }
        public IActionResult Profile([FromCrypto] int secretUserId = 0)
        {
            UserProfileViewModel user_model = new UserProfileViewModel();
            var paramDictionary = new Dictionary<string, string>();
            if (Sessione.GetUserAuthenticated() != null)
            {
                tab_utenti user = Sessione.GetUserAuthenticated();
                user_model = new UserProfileViewModel
                {
                    Email = user.email,
                    PhoneNumber = user.telefono_mobile,
                    UserId = user.id_utente,
                    UserName = user.nome_utente,
                    Password = user.password_utente,
                    Attivo = user.flag_utente_attivo == true ? "ATTIVO" : "NON ATTIVO"
                };
                paramDictionary.Add("secretUserId", user.id_utente.ToString());
                ViewBag.encryptedUser = _protector.EncryptParamDictionary(paramDictionary);
                return View(user_model);
            }
            ViewBag.Alert = MessageServices.ShowAlert(Alerts.Warning, "Dati del profilo non trovati!");
            return View(user_model);
        }
        /// <summary>
        /// Recupera i dati anagrafici
        /// dell'utente.
        /// Migliorabile ma serve per testare la
        /// protezione dei parametri
        /// </summary>
        /// <param name="secretUserId"></param>
        /// <param name="profile"></param>
        /// <returns></returns>
        [Authorize("Authenticated")]
        public IActionResult ProfileAnag([FromCrypto] int secretUserId, UserProfileViewModel profile)
        {
            UserProfileViewModel user_model = new UserProfileViewModel();
            var paramDictionary = new Dictionary<string, string>();
            tab_utenti user = TabUtentiBD.GetById(dbContext, secretUserId);
            ViewBag.CFPiva = "Codice Fiscale";
            string cfPiva = string.Empty;
            string ragSociale = string.Empty;

            if (user != null)
            {
                if (!string.IsNullOrEmpty(user.codice_fiscale))
                {
                    if (user.codice_fiscale.Length == 16)
                        cfPiva = user.codice_fiscale;
                }
                else
                {
                    if (!string.IsNullOrEmpty(user.p_iva))
                        cfPiva = user.p_iva;
                    ViewBag.CFPiva = "Partita Iva";
                }
                if (cfPiva.Length == 11)
                    ragSociale = user.ragione_sociale;
                user_model = new UserProfileViewModel
                {
                    Email = user.email,
                    PhoneNumber = user.telefono_mobile,
                    UserId = user.id_utente,
                    UserName = user.nome_utente,
                    Password = user.password_utente,
                    Attivo = user.flag_utente_attivo == true ? "ATTIVO" : "NON ATTIVO",
                    CFPIva = cfPiva,
                    RagSociale = !string.IsNullOrEmpty(ragSociale) ? ragSociale : "",
                    Mobile = !string.IsNullOrEmpty(user.telefono_mobile) ? user.telefono_mobile : "",
                    Cognome = !string.IsNullOrEmpty(user.cognome) ? user.cognome : "",
                    Nome = !string.IsNullOrEmpty(user.nome) ? user.nome : "",
                };
                paramDictionary.Add("secretUserId", user.id_utente.ToString());
                ViewBag.encryptedUser = _protector.EncryptParamDictionary(paramDictionary);
                return View(user_model);
            }
            ViewBag.Alert = MessageServices.ShowAlert(Alerts.Warning, "Dati anagrafici del profilo non trovati!");
            return View(user_model);
        }
        /// <summary>
        /// Recupera i dati anagrafici
        /// dell'utente.
        /// Migliorabile ma serve per testare una seconda
        /// modalità di protezione dei parametri
        /// </summary>
        /// <param name="secretUserId"></param>
        /// <param name="profile"></param>
        /// <returns></returns>
        [Authorize("Authenticated")]
        [CryptoValueProvider]
        public IActionResult ProfileAnagCrypto(int secretUserId, UserProfileViewModel profile)
        {
            UserProfileViewModel user_model = new UserProfileViewModel();
            var paramDictionary = new Dictionary<string, string>();
            tab_utenti user = TabUtentiBD.GetById(dbContext, secretUserId);
            ViewBag.CFPiva = "Codice Fiscale";
            string cfPiva = string.Empty;
            string ragSociale = string.Empty;

            if (user != null)
            {
                if (!string.IsNullOrEmpty(user.codice_fiscale))
                {
                    if (user.codice_fiscale.Length == 16)
                        cfPiva = user.codice_fiscale;
                }
                else
                {
                    if (!string.IsNullOrEmpty(user.p_iva))
                        cfPiva = user.p_iva;
                    ViewBag.CFPiva = "Partita Iva";
                }
                if (cfPiva.Length == 11)
                    ragSociale = user.ragione_sociale;
                user_model = new UserProfileViewModel
                {
                    Email = user.email,
                    PhoneNumber = user.telefono_mobile,
                    UserId = user.id_utente,
                    UserName = user.nome_utente,
                    Password = user.password_utente,
                    Attivo = user.flag_utente_attivo == true ? "ATTIVO" : "NON ATTIVO",
                    CFPIva = cfPiva,
                    RagSociale = !string.IsNullOrEmpty(ragSociale) ? ragSociale : "",
                    Mobile = !string.IsNullOrEmpty(user.telefono_mobile) ? user.telefono_mobile : "",
                    Cognome = !string.IsNullOrEmpty(user.cognome) ? user.cognome : "",
                    Nome = !string.IsNullOrEmpty(user.nome) ? user.nome : "",
                };
                paramDictionary.Add("secretUserId", user.id_utente.ToString());
                ViewBag.encryptedUser = _protector.EncryptParamDictionary(paramDictionary);
                return View("ProfileAnag", user_model);
            }
            ViewBag.Alert = MessageServices.ShowAlert(Alerts.Warning, "Dati anagrafici del profilo non trovati!");
            return View(user_model);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion Action
    }
}
