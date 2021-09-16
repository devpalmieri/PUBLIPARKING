using Publiparking.Data;
using Publiparking.Data.BD;
using Publisoftware.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Publiparking.Web.NinoServer.Controllers
{
    public class telefonoController : Controller
    {
        // GET: telefono
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("codice_verifica_telefono")]
        public ActionResult codiceVerificaTelefono(string id, string iv)
        {
            //chiamata:http://localhost:57399/telefono/cambio_master            
            int v_codice = 0;
            bool v_bCambio = false;
            Random v_random = new Random();
            string v_codiceString = "";
            string v_numeroTelefonoString = "";
            //da copiare in altre funzioni           
            string v_sInputTotalParameters = Utility.getStringParameters(id, iv);
            string[] v_vectSplitParameters = v_sInputTotalParameters.Split(Utility.decifraturaSeparator);
            //if (parts.Length == 3) { }
            //v_sParameters.Split(g_cSeparator)[0];
            if (v_vectSplitParameters.Length == 2)
            {
                v_numeroTelefonoString = v_vectSplitParameters[0];
                v_codiceString = v_vectSplitParameters[1];
            }
            //fine decifratura


            try
            {
                if (!Utility.VerificaCorrettezzaNTelefono(ref v_numeroTelefonoString)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                if (String.IsNullOrEmpty(v_codiceString)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Int32.TryParse(v_codiceString, out v_codice)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(numero_telefono)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                dbEnte v_ctxGenerale = Utility.getGeneraleCtx();
                DbParkCtx v_ctx = Utility.getEnteCtx(49, v_ctxGenerale);
                string v_connectionString = Utility.getEnteStringConnection(49, v_ctxGenerale);
                if (string.IsNullOrEmpty(v_connectionString)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                
                SMSOut v_smsRisp = new SMSOut();
                v_smsRisp.numeroDestinatario = v_numeroTelefonoString;
                v_smsRisp.numeroMittente = v_smsRisp.numeroDestinatario;
                v_smsRisp.dataElaborazione = DateTime.Now;
                v_smsRisp.testo = "PubliParking - richiesta verifica telefono. Conferma con il seguente codice:" + v_codice;
                v_ctx.SMSOut.Add(v_smsRisp);
                v_ctx.SaveChanges();
                return Json(true);

            }
            catch (Exception ex)
            {
                return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ActionName("associa")]
        public ActionResult associa_telefono(string id, string iv, string numero_telefono)
        {
            try
            {
                int v_codiceComune = 0;
                string v_master = "";
                string v_codice_comune = "";
                string v_codice_conto = "";
                //da copiare in altre funzioni           
                string v_sInputTotalParameters = Utility.getStringParameters(id, iv);
                string[] v_vectSplitParameters = v_sInputTotalParameters.Split(Utility.decifraturaSeparator);
                //if (parts.Length == 3) { }
                //v_sParameters.Split(g_cSeparator)[0];
                if (v_vectSplitParameters.Length == 3)
                {
                    v_master = v_vectSplitParameters[0];
                    v_codice_comune = v_vectSplitParameters[1];
                    v_codice_conto = v_vectSplitParameters[2];
                }
                //fine decifratura
                if (!Utility.VerificaCorrettezzaNTelefono(ref numero_telefono)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Utility.VerificaCorrettezzaNAbbonamento(v_codice_conto)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Utility.VerificaCorrettezzaCodiceComune(v_codice_comune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Utility.VerificaCorrettezzaNTelefono(ref v_master)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(codice_comune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Int32.TryParse(v_codice_comune, out v_codiceComune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(numero_telefono)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(codice_conto)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(master)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                dbEnte v_ctxGenerale = Utility.getGeneraleCtx();
                DbParkCtx v_ctx = Utility.getEnteCtx(v_codiceComune, v_ctxGenerale);
                string v_connectionString = Utility.getEnteStringConnection(v_codiceComune, v_ctxGenerale);
                if (string.IsNullOrEmpty(v_connectionString)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }


                Abbonamenti v_abbonamento = AbbonamentiBD.getAbbonamentoByCodice(v_codice_conto, v_ctx);

                if (v_abbonamento != null)
                {
                    CellulariBD.creaCellulare(v_abbonamento.idAbbonamento, numero_telefono, false, v_ctx);
                    v_ctx.SaveChanges();
                    return Json(true);                   
                }
                else
                    return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ActionName("rimuovi")]
        public ActionResult rimozione_telefono(string id, string iv, string numero_telefono)
        {
            try
            {
                int v_codiceComune = 0;
                bool v_bRimozione = false;
                string v_master = "";
                string v_codice_comune = "";
                string v_codice_conto = "";
                //da copiare in altre funzioni           
                string v_sInputTotalParameters = Utility.getStringParameters(id, iv);
                string[] v_vectSplitParameters = v_sInputTotalParameters.Split(Utility.decifraturaSeparator);
                //if (parts.Length == 3) { }
                //v_sParameters.Split(g_cSeparator)[0];
                if (v_vectSplitParameters.Length == 3)
                {
                    v_master = v_vectSplitParameters[0];
                    v_codice_comune = v_vectSplitParameters[1];
                    v_codice_conto = v_vectSplitParameters[2];
                }
                //fine decifratura



                if (!Utility.VerificaCorrettezzaNTelefono(ref numero_telefono)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Utility.VerificaCorrettezzaNAbbonamento(v_codice_conto)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Utility.VerificaCorrettezzaCodiceComune(v_codice_comune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Utility.VerificaCorrettezzaNTelefono(ref v_master)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                //if (String.IsNullOrEmpty(codice_comune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Int32.TryParse(v_codice_comune, out v_codiceComune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(numero_telefono)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(codice_conto)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(master)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }


                dbEnte v_ctxGenerale = Utility.getGeneraleCtx();
                DbParkCtx v_ctx = Utility.getEnteCtx(v_codiceComune, v_ctxGenerale);
                string v_connectionString = Utility.getEnteStringConnection(v_codiceComune, v_ctxGenerale);
                if (string.IsNullOrEmpty(v_connectionString)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }


                Abbonamenti v_abbonamento = AbbonamentiBD.getAbbonamentoByCodice(v_codice_conto, v_ctx);

                if (v_abbonamento != null)
                {


                    Cellulari v_cellulare = CellulariBD.GetList(v_ctx).Where(c => c.numero == numero_telefono)
                                                                      .Where(c => c.idAbbonamento == v_abbonamento.idAbbonamento)
                                                                      .Where(c => !c.dataCessazione.HasValue).SingleOrDefault();
                    if (v_cellulare != null)
                    {
                        v_bRimozione = CellulariBD.disattivaCellulare(v_cellulare.idCellulare, v_ctx);
                        v_ctx.SaveChanges();
                    }
                    else
                        return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);

                    var v_return = new
                    {
                        successo = v_bRimozione
                    };
                    return Json(v_return);
                }
                else
                    return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
            }
        }


        //chiamata:http://localhost:57399/telefono/lista?codice_comune=49&codice_conto=921892848761&numero_telefono=%2B393921173448
        [HttpGet]
        public ActionResult lista(string id, string iv)
        {
            int v_codiceComune = 0;
            string v_sRisposta = "";
            try
            {

                string v_numero_telefono = "";
                string v_codice_comune = "";
                string v_codice_conto = "";

                //da copiare in altre funzioni           
                string v_sInputTotalParameters = Utility.getStringParameters(id, iv);
                string[] v_vectSplitParameters = v_sInputTotalParameters.Split(Utility.decifraturaSeparator);
                //if (parts.Length == 3) { }
                //v_sParameters.Split(g_cSeparator)[0];
                if (v_vectSplitParameters.Length == 3)
                {
                    v_numero_telefono = v_vectSplitParameters[0];
                    v_codice_comune = v_vectSplitParameters[1];
                    v_codice_conto = v_vectSplitParameters[2];
                }
                //fine decifratura

                IList<Cellulari> v_cellulariList = null;
                //var v_result="";

                if (!Utility.VerificaCorrettezzaNTelefono(ref v_numero_telefono)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Utility.VerificaCorrettezzaNAbbonamento(v_codice_conto)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Utility.VerificaCorrettezzaCodiceComune(v_codice_comune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                //if (String.IsNullOrEmpty(codice_comune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Int32.TryParse(v_codice_comune, out v_codiceComune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(numero_telefono)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(codice_conto)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                dbEnte v_ctxGenerale = Utility.getGeneraleCtx();
                DbParkCtx v_ctx = Utility.getEnteCtx(v_codiceComune, v_ctxGenerale);
                string v_connectionString = Utility.getEnteStringConnection(v_codiceComune, v_ctxGenerale);
                if (string.IsNullOrEmpty(v_connectionString)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }


                Abbonamenti v_abbonamento = AbbonamentiBD.getAbbonamentoByCodice(v_codice_conto, v_ctx);

                if (v_abbonamento != null)
                {
                    v_cellulariList = CellulariBD.GetList(v_ctx).Where(c=> c.idAbbonamento == v_abbonamento.idAbbonamento)
                                                                .Where(s => s.dataCessazione == null).ToList();
                    if (v_cellulariList != null)
                    {
                        //foreach (Cellulari v_cellulare in v_cellulariList)
                        //{
                        //    v_sRisposta = v_sRisposta + v_cellulare.numero + ",";
                        //}
                        //var v_result = v_cellulariList.Select(x => new
                        //{
                        //    x.numero
                        //});
                        //var v_result = new
                        //{

                        //    v_sRisposta
                        //};
                        //return Json(new { USCITA = v_result }, JsonRequestBehavior.AllowGet);
                        var v_result = v_cellulariList.Select(x => x.numero
                        ).ToArray();
                        return Json(v_result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        //return Json(new { USCITA = numero_telefono="" }, JsonRequestBehavior.AllowGet);
                        return Json(v_numero_telefono = "", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                    return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
            }
        }


        // POST: telefono/cambio_master
        // (ContentType: application/json)
        // Body:
        //{
        //  codice_comune:19
        //  codice_conto:19
        //  master_corrente:%2B393283095707
        //	nuovo_master: %2B393283095707,
        //}
        [HttpPost]
        [ActionName("cambio_master")]
        public ActionResult cambioMaster(string id, string iv, string nuovo_master)
        {
            //chiamata:http://localhost:57399/telefono/cambio_master
            int v_codiceComune = 0;
            bool v_bCambio = false;
            try
            {


                string v_master_corrente = "";
                string v_codice_comune = "";
                string v_codice_conto = "";
                //da copiare in altre funzioni           
                string v_sInputTotalParameters = Utility.getStringParameters(id, iv);
                string[] v_vectSplitParameters = v_sInputTotalParameters.Split(Utility.decifraturaSeparator);
                //if (parts.Length == 3) { }
                //v_sParameters.Split(g_cSeparator)[0];
                if (v_vectSplitParameters.Length == 3)
                {
                    v_master_corrente = v_vectSplitParameters[0];
                    v_codice_comune = v_vectSplitParameters[1];
                    v_codice_conto = v_vectSplitParameters[2];
                }
                //fine decifratura


                if (!Utility.VerificaCorrettezzaNTelefono(ref nuovo_master)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Utility.VerificaCorrettezzaNAbbonamento(v_codice_conto)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Utility.VerificaCorrettezzaCodiceComune(v_codice_comune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Utility.VerificaCorrettezzaNTelefono(ref v_master_corrente)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                //if (String.IsNullOrEmpty(codice_comune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Int32.TryParse(v_codice_comune, out v_codiceComune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(codice_conto)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(master_corrente)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(nuovo_master)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                dbEnte v_ctxGenerale = Utility.getGeneraleCtx();
                DbParkCtx v_ctx = Utility.getEnteCtx(v_codiceComune, v_ctxGenerale);
                string v_connectionString = Utility.getEnteStringConnection(v_codiceComune, v_ctxGenerale);
                if (string.IsNullOrEmpty(v_connectionString)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                Abbonamenti v_abbonamento = AbbonamentiBD.getAbbonamentoByCodice(v_codice_conto, v_ctx);

                if (v_abbonamento != null)
                {                    
                        Cellulari v_cellulare = CellulariBD.GetList(v_ctx).Where(c => c.numero == v_master_corrente)
                                                                      .Where(c => c.idAbbonamento == v_abbonamento.idAbbonamento)
                                                                      .Where(c => !c.dataCessazione.HasValue).SingleOrDefault();
                        if (v_cellulare != null)
                        {
                            if (v_cellulare.master)
                            {//se master torna badRequest
                                Cellulari v_cellulareNewSlave = null;
                                Cellulari v_cellulareNewMaster = null;
                                if (CellulariBD.disattivaCellulare(v_cellulare.idCellulare, v_ctx))
                                {
                                    v_cellulareNewSlave = new Cellulari();
                                    v_cellulareNewSlave.idAbbonamento = v_abbonamento.idAbbonamento;
                                    v_cellulareNewSlave.numero = v_master_corrente;
                                    v_cellulareNewSlave.dataAttivazione = DateTime.Now;
                                    v_cellulareNewSlave.master = false;
                                    v_ctx.Cellulari.Add(v_cellulareNewSlave);
                                    
                                    Cellulari v_cellulareOldSlave = CellulariBD.GetList(v_ctx).Where(c => c.numero == nuovo_master)
                                                                      .Where(c => c.idAbbonamento == v_abbonamento.idAbbonamento)
                                                                      .Where(c => !c.dataCessazione.HasValue).SingleOrDefault();

                                    if (v_cellulareOldSlave != null) { CellulariBD.disattivaCellulare(v_cellulareOldSlave.idCellulare,v_ctx); }
                                    v_cellulareNewMaster = new Cellulari();
                                    v_cellulareNewMaster.idAbbonamento = v_abbonamento.idAbbonamento;
                                    v_cellulareNewMaster.numero = nuovo_master;
                                    v_cellulareNewMaster.dataAttivazione = DateTime.Now;
                                    v_cellulareNewMaster.master = true;
                                    v_ctx.Cellulari.Add(v_cellulareNewMaster);
                                }
                                if (v_cellulareNewSlave != null && v_cellulareNewMaster != null)
                                {
                                    v_ctx.SaveChanges();
                                    v_bCambio = true;
                                }
                                else return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);

                            }
                            else return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
                        }
                        else return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);

                        var v_return = new
                        {
                            successo = v_bCambio
                        };                       
                        return Json(v_return);                   
                }
                else return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}