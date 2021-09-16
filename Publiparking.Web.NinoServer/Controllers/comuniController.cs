using Publiparking.Data;
using Publiparking.Data.BD;
using Publiparking.Data.dto;
using Publiparking.Service.Base;
using Publisoftware.Data;
using Publisoftware.Data.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Publiparking.Web.NinoServer.Controllers
{
    public class comuniController : Controller
    {
        // GET: comuni
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult provarisposta(int i, string s)
        {
            // Response.TrySkipIisCustomErrors();
            //Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var pippo = new { a = i, b = s };
            return Json(pippo, JsonRequestBehavior.AllowGet);
        }

        //------------------------------------------------CON CIFRATURA TUTTO INVARIATO
        //chiamata:http://localhost:57399/comuni/lista     
        [HttpGet]
        public ActionResult lista()
        {

            try
            {
                dbEnte v_generalecontext = Utility.getGeneraleCtx();
                //List<anagrafica_ente> v_listEnti = AnagraficaEnteBD.GetList(v_generalecontext).ToList();
                IList<anagrafica_ente> v_listEnti = AnagraficaEnteBD.GetParkList(v_generalecontext).ToList();

                var v_result = v_listEnti.Select(x => new
                {
                    nome_comune = x.descrizione_ente,
                    codice_comune = x.id_ente,
                    tipo_parcheggio = Utility.getModalitaPagamento(x.id_ente, v_generalecontext),
                    // pubkey_conto_stripe = Utility.getValue(x.chiave_pubblica_stripe)
                });
                //return Json(new { USCITA = v_result }, JsonRequestBehavior.AllowGet);
                return Json(v_result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
            }
        }

        //chiamata:http://localhost:57399/comuni/lista_tariffe?codice_comune=19
        [HttpGet]
        public ActionResult lista_tariffe(string codice_comune)
        {
            int v_codiceComune = 0;

            try
            {


                if (!Utility.VerificaCorrettezzaCodiceComune(codice_comune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(codice_comune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Int32.TryParse(codice_comune, out v_codiceComune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                dbEnte v_generalecontext = Utility.getGeneraleCtx();
                DbParkCtx v_parkcontext = Utility.getEnteCtx(v_codiceComune, v_generalecontext);

                if (v_parkcontext == null) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                IList<Tariffe> v_listTariffe = TariffeBD.GetList(v_parkcontext).ToList();
                var v_result = v_listTariffe.Select(x => new
                {
                    id_tariffa = x.idTariffa,
                    descrizione_tariffa = x.descrizione.ToString(),
                    feriale_ora_inizio = String.Format("{0:00}", x.ferialeOraInizioPrimaFascia.Hours) + ":" + String.Format("{0:00}", x.ferialeOraInizioPrimaFascia.Minutes),
                    feriale_ora_fine = String.Format("{0:00}", x.ferialeOraFinePrimaFascia.Hours) + ":" + String.Format("{0:00}", x.ferialeOraFinePrimaFascia.Minutes),
                    festivo_ora_inizio = String.Format("{0:00}", x.festivoOraInizioPrimaFascia.Hours) + ":" + String.Format("{0:00}", x.festivoOraInizioPrimaFascia.Minutes),
                    festivo_ora_fine = String.Format("{0:00}", x.festivoOraFinePrimaFascia.Hours) + ":" + String.Format("{0:00}", x.festivoOraFinePrimaFascia.Minutes),
                    importo_festivo_fascia_iniziale = Decimal.Round(x.festivoFasciaInizialeEuroOra, 0).ToString(),
                    importo_festivo_fascia_successiva = Decimal.Round(x.festivoFasciaSuccessivaEuroOra, 0).ToString(),
                    importo_feriale_fascia_iniziale = Decimal.Round(x.ferialeFasciaInizialeEuroOra, 0).ToString(),
                    importo_feriale_fascia_successiva = Decimal.Round(x.ferialeFasciaSuccessivaEuroOra, 0).ToString()
                });

                return Json(v_result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}