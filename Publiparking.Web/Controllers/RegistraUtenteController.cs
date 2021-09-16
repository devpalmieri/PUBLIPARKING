using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Publiparking.Core.Data.BD;
using Publiparking.Core.Data.BD.BD;
using Publiparking.Core.Data.BD.Helper;
using Publiparking.Core.Data.SqlServer.Entities;
using Publiparking.Web.Base;
using Publiparking.Web.Classes;
using Publiparking.Web.Classes.Enumerator;
using Publiparking.Web.Classes.Helper;
using Publiparking.Web.Configuration;
using Publiparking.Web.Models;
using Publiparking.Web.Models.SpidModels;
using Publisoftware.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Web.Controllers
{
    public class RegistraUtenteController : UnloggedBaseController
    {
        private readonly ILogger<HomeUtenteController> _logger;
        private IConfiguration _configurationRoot;
        public RegistraUtenteController(
           ILogger<HomeUtenteController> logger,
           IConfiguration configurationRoot)
        {
            _logger = logger;
            _configurationRoot = configurationRoot;
        }
        public IActionResult RegistraUtente()
        {
            SpidUser spidUser = Sessione.GetUtenteSpid();

            List<tab_domande_segrete> v_domandeSegreteList = new List<tab_domande_segrete>();
            v_domandeSegreteList = TabDomandeSegreteBD.GetList(dbContextReadOnly).ToList();
            RegistraUtenteViewModel v_registraUtenteModel = new RegistraUtenteViewModel()
            {
                listDomandaSegreta = v_domandeSegreteList,
                selDomandaSegretaId = v_domandeSegreteList.FirstOrDefault().id_domanda_segreta,
                codFiscalePIVA = spidUser != null ? spidUser.FiscalNumber : string.Empty
            };

            string spidMessage = Sessione.GetLoginSpidMessage();

            if (!string.IsNullOrEmpty(spidMessage))
            {
                ViewBag.Alert = MessageServices.ShowAlert(Alerts.Warning, spidMessage);
            }
            ViewBag.Credenziali = true;
            ViewBag.Anag = false;
            return View(v_registraUtenteModel);
        }
        [HttpPost]
        public ActionResult Registra(RegistraUtenteViewModel registraUtenteModel)
        {
            List<tab_domande_segrete> v_domandeSegreteList = TabDomandeSegreteBD.GetList(dbContext).ToList();
            registraUtenteModel.listDomandaSegreta = v_domandeSegreteList;

            if (TabUtentiBD.AnyUserName(dbContextReadOnly, registraUtenteModel.UserName.ToUpper()))
            {
                ViewBag.Alert = MessageServices.ShowAlert(Alerts.Danger, "Errore. Username già utilizzata.");
                ViewBag.messageTitle = "Registrazione utente";
                ViewBag.isVerificaUtente = false;
                ModelState.AddModelError("UserName", "Username già utilizzata.");
                return View("RegistraUtente", registraUtenteModel);
            }
            if (TabUtentiBD.AnyEmail(dbContext, registraUtenteModel.email))
            {
                ViewBag.Alert = MessageServices.ShowAlert(Alerts.Danger, "Errore. Email già utilizzata.");
                ViewBag.messageTitle = "isVerificaUtente utente";
                ViewBag.isVerificaUtente = false;
                ModelState.AddModelError("email", "Email già utilizzata.");
                return View("RegistraUtente", registraUtenteModel);
            }
            var securityPassword = _configurationRoot.GetSection("SecurityPassword").Get<SecurityPassword>();
            string secPass = securityPassword.Key;

            if (!PasswordHelper.CheckSicurezzaPassword(registraUtenteModel.Password, secPass))
            {
                ViewBag.Alert = MessageServices.ShowAlert(Alerts.Danger, "Errore. Formato password non valido.");
                ViewBag.messageTitle = "Registrazione utente";
                ViewBag.isVerificaUtente = false;
                ModelState.AddModelError("Password", "Formato password non valido.");
                return View("RegistraUtente", registraUtenteModel);
            }

            if (TabUtentiBD.AnyCodiceFiscaleUserName(dbContext, registraUtenteModel.codFiscalePIVA))
            {
                ViewBag.Alert = MessageServices.ShowAlert(Alerts.Danger, "Errore. Codice fiscale già presente.");
                ViewBag.messageTitle = "Registrazione utente";
                ViewBag.isVerificaUtente = false;
                ModelState.AddModelError("codFiscalePIVA", "Codice fiscale già presente.");
                return View("RegistraUtente", registraUtenteModel);
            }
            string v_codiceFiscale = CFHelper.getCodiceFiscale(registraUtenteModel.data_nas,
                                                               registraUtenteModel.nome,
                                                               registraUtenteModel.cognome,
                                                               registraUtenteModel.cod_comune_nas,
                                                               registraUtenteModel.stato_nas,
                                                               registraUtenteModel.id_sesso,
                                                               dbContext);

            if (v_codiceFiscale != CFHelper.transformCodiceFiscale(registraUtenteModel.codFiscalePIVA))
            {
                ViewBag.messageTitle = "Registrazione utente";
                ViewBag.isVerificaUtente = false;
                string msg = string.Empty;
                if (string.IsNullOrEmpty(v_codiceFiscale))
                {
                    msg = "Incongruenza tra il codice fiscale ed i dati inseriti.";
                    ModelState.AddModelError("codFiscalePIVA", "Incongruenza tra il codice fiscale ed i dati inseriti.");
                }
                else
                {
                    msg = "Incongruenza tra il codice fiscale ed i dati inseriti. Il codice fiscale, in base ai dati inseriti, è " + v_codiceFiscale + ".";
                    ModelState.AddModelError("codFiscalePIVA", "Incongruenza tra il codice fiscale ed i dati inseriti. Il codice fiscale, in base ai dati inseriti, è " + v_codiceFiscale + ".");
                }
                ViewBag.Alert = MessageServices.ShowAlert(Alerts.Danger, msg);
                return View("RegistraUtente", registraUtenteModel);
            }
            tab_utenti v_utente = new tab_utenti();

            if (TabUtentiBD.WhereByCodiceFiscaleUserNameNull(dbContext, registraUtenteModel.codFiscalePIVA) != null)
            {
                v_utente = TabUtentiBD.WhereByCodiceFiscaleUserNameNull(dbContext, registraUtenteModel.codFiscalePIVA);
                ViewBag.Alert = MessageServices.ShowAlert(Alerts.Danger, "L'utente è stato registrato precedentemente allo sportello. ");
            }
            v_utente.nome_utente = registraUtenteModel.UserName.ToUpper();
            v_utente.password_utente = CryptMD5.getMD5(registraUtenteModel.Password);
            v_utente.flag_password_sistema = false;

            v_utente.codice_fiscale = registraUtenteModel.codFiscalePIVA.ToUpper();
            v_utente.tipo_utente = tab_utenti.TIPOUTENTE_FISICO;
            v_utente.nome = registraUtenteModel.nome;
            v_utente.cognome = registraUtenteModel.cognome;
            //v_utente.ragione_sociale = registraUtenteModel.ragioneSociale;
            v_utente.email = registraUtenteModel.email;
            //v_utente.nazionalita = registraUtenteModel.nazionalita;
            //v_utente.pref_mobile = null;
            v_utente.telefono_mobile = registraUtenteModel.TelMobile;
            //v_utente.pref_fisso = null;
            v_utente.telefono_fisso = registraUtenteModel.TelFisso;
            v_utente.data_stato = DateTime.Now;
            //v_utente.id_struttura_stato = _idStruttura;
            //v_utente.id_risorsa_stato = _idRisorsa;
            //TODO: Da rimuovere
            v_utente.flag_on_off = "1";
            v_utente.cod_stato = tab_utenti.ACQ_ACQ;
            v_utente.flag_utente_attivo = false;
            v_utente.id_domanda_segreta = registraUtenteModel.selDomandaSegretaId;
            v_utente.risposta_domanda_segreta = registraUtenteModel.rispostaSegreta;

            if (v_utente.id_utente == 0/*IdUtente == -1*/)
            {
                dbContext.tab_utenti.Add(v_utente);
            }
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
            tab_utenti currentUser = TabUtentiBD.GetUtenteByUsernameEmail(registraUtenteModel.UserName, registraUtenteModel.email, dbContext);
            string message = string.Empty;
            var mailConfig = _configurationRoot.GetSection("MailConfig").Get<MailConfig>();
            if (Convert.ToBoolean(mailConfig.IsMailAbilitata != null ? mailConfig.IsMailAbilitata : "true"))
            {
                try
                {
                    Core.Utility.IMailSender wHandler = Core.Utility.MailHelper.Instance.GetSender(mailConfig.ServerPosta != null ? mailConfig.ServerPosta : "smtp.publiservizi.net",
                                                                             mailConfig.Mail != null ? mailConfig.Mail : "demo@publiservizi.net",
                                                                             mailConfig.Password != null ? mailConfig.Password : "Demo2016");

                    string idUtenteE = CryptMD5.Encrypt(v_utente.id_utente.ToString());
                    //string host = PSHostHelper.GetServerURIAuthorityAndAppPath(Request);
                    string host = string.Concat(
                                    HttpContext.Request.Scheme,
                                    "://",
                                    HttpContext.Request.Host.ToUriComponent());
                    string route = "/RegistraUtente/ConfermaUtente";
                    string linkConferma = $"<a href=\"{host}/{route}?idUtenteE={idUtenteE}\">{host}/{route}?idUtenteE={idUtenteE}</a>";
                    string corpo = "Per completare la registrazione, cliccare sul seguente link oppure copiare ed incollare tale link nel browser: " + linkConferma;

                    wHandler.sendMail(mailConfig.Mail != null ? mailConfig.Mail : "demo@publiservizi.net",
                                          v_utente.email,
                                          string.Empty,
                                          mailConfig.MailBCC != null ? mailConfig.MailBCC : "fpalmieri@publisoftware.it",
                                          "Conferma Indirizzo Email",
                                          "Conferma Indirizzo Email",
                                          corpo);
                    message = ViewBag.message + "I dati sono stati acquisiti. Prima di completare la registrazione, è necessario verificare l'indirizzo email cliccando il link di conferma inviato all'indirizzo " + v_utente.email + ".";
                    ViewBag.Alert = MessageServices.ShowAlert(Alerts.Success, message);


                }
                catch (Exception e)
                {
                    message = ViewBag.message + "I dati sono stati acquisiti, ma c'è stato un errore nell'invio dell'email per il completamento della registrazione. Contattare il supporto all'indirizzo supporto@publiservizi.net."; //TODO: cambiare con l'email giusta
                    ViewBag.Alert = MessageServices.ShowAlert(Alerts.Success, message);
                }
            }
            ViewBag.isVerificaUtente = true;
            return View("RegistraUtente", registraUtenteModel);

        }
        public ActionResult ConfermaUtente(string idUtenteE)
        {
            ViewBag.showLoginBar = false;

            int v_idUtente = Convert.ToInt32(CryptMD5.Decrypt(idUtenteE));

            tab_utenti v_utente = TabUtentiBD.GetById(dbContext, v_idUtente);

            v_utente.cod_stato = tab_utenti.ATT_ATT;
            v_utente.flag_utente_attivo = true;

            dbContext.SaveChanges();
            ViewBag.Alert = MessageServices.ShowAlert(Alerts.Success, "Indirizzo email verificato.");

            return View();
        }
    }
}
