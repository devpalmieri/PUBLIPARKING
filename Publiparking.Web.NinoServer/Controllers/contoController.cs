using Publiparking.Data;
using Publiparking.Data.BD;
using Publisoftware.Data;
using Publisoftware.Data.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stripe;

namespace Publiparking.Web.NinoServer.Controllers
{
    public class contoController : Controller
    {
        // GET: conto
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("richiedi")]        
        public ActionResult richiedi_conto(string id, string iv)
        {
            try
            {
                int v_codiceComune = 0;
                string v_numero_telefono = "";
                string v_codice_comune = "";
                Boolean v_bNuovoInserimento = false;
                //da copiare in altre funzioni           
                string v_sInputTotalParameters = Utility.getStringParameters(id, iv);
                string[] v_vectSplitParameters = v_sInputTotalParameters.Split(Utility.decifraturaSeparator);
                //if (parts.Length == 3) { }
                //v_sParameters.Split(g_cSeparator)[0];
                if (v_vectSplitParameters.Length == 2)
                {
                    v_numero_telefono = v_vectSplitParameters[0];
                    v_codice_comune = v_vectSplitParameters[1];

                    //v_numero_telefono = "+393925391295";
                    //v_codice_comune = "49";
                }
                //fine decifratura


                if (!Utility.VerificaCorrettezzaNTelefono(ref v_numero_telefono)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Utility.VerificaCorrettezzaCodiceComune(v_codice_comune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                //if (String.IsNullOrEmpty(codice_comune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Int32.TryParse(v_codice_comune, out v_codiceComune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(numero_telefono)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }


                //Generale v_ctxGenerale = Utility.getGeneraleCtx();
                //Ente v_ctx = Utility.getEnteCtx(v_codiceComune, v_ctxGenerale);


                dbEnte v_ctxGenerale = Utility.getGeneraleCtx();
                DbParkCtx v_ctx = Utility.getEnteCtx(v_codiceComune, v_ctxGenerale);



                string v_connectionString = Utility.getEnteStringConnection(v_codiceComune, v_ctxGenerale);
                //Publiservizi.ParkingPatrol.Data.Utilities.ConnectionDB.idEnte = v_codiceComune;

                if (string.IsNullOrEmpty(v_connectionString)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                Cellulari v_cellulare = CellulariBD.GetList(v_ctx).Where(c => c.numero == v_numero_telefono)
                                                                                   .Where(c => !c.dataCessazione.HasValue).OrderByDescending(c=> c.dataAttivazione).FirstOrDefault();
              

                if ((v_cellulare != null))
                {
                    Abbonamenti v_abbonamento = v_cellulare.Abbonamenti;
                    //AbbonamentoDTO v_abbonamentoSearch = AbbonamentiBD.loadById(v_listCellulariSearch[0].idAbbonamento);
                    if (v_abbonamento != null)
                    {
                        //v_richiestaDuplicata = true;
                        var v_return = new
                        {
                            codice_conto = v_abbonamento.codice.ToString()
                        };
                        return Json(v_return);
                    }
                    else { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                }
                else
                {

                    //modifica del 28/02/2018 l'abbonamento può essere solo uno per numero di telefono sia master che slave
                    //Abbonamenti v_abbonamentoNew = AbbonamentiBD.insertNew(ref v_ctx);

                    Abbonamenti v_abbonamentoNew = new Abbonamenti();
                    v_abbonamentoNew.codice = AbbonamentiBD.generaCodice(v_ctx);
                    v_abbonamentoNew.dataAbilitazione = DateTime.Now;
                    v_ctx.Abbonamenti.Add(v_abbonamentoNew);
                    v_ctx.SaveChanges();

                    v_cellulare = CellulariBD.creaCellulare(v_abbonamentoNew.idAbbonamento, v_numero_telefono, true, v_ctx);
                    v_ctx.SaveChanges();
                    if (v_cellulare != null)
                    {
                        var v_return = new
                        {
                            codice_conto = v_abbonamentoNew.codice.ToString()
                        };
                        return Json(v_return);
                    }
                    else
                    {
                        return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]        
        public ActionResult info(string id, string iv)
        {
            try
            {
                //decifratura
                //string id, string iv
                int v_codiceComune = 0; 
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


                if (!Utility.VerificaCorrettezzaNTelefono(ref v_numero_telefono)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Utility.VerificaCorrettezzaNAbbonamento(v_codice_conto)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Utility.VerificaCorrettezzaCodiceComune(v_codice_comune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                //if (String.IsNullOrEmpty(codice_comune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Int32.TryParse(v_codice_comune, out v_codiceComune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(numero_telefono)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(codice_conto)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                dbEnte v_ctxGenerale = Utility.getGeneraleCtx();
                DbParkCtx v_ctx = Utility.getEnteCtx(v_codiceComune, v_ctxGenerale);
                //Ente v_ctx = Utility.getEnteCtx(v_codiceComune, v_ctxGenerale);

                string v_connectionString = Utility.getEnteStringConnection(v_codiceComune, v_ctxGenerale);
                

                if (v_connectionString == "") { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                Cellulari v_cellulare = CellulariBD.getCellulareByNumero(v_numero_telefono, v_ctx);
                Abbonamenti v_abbonamento = AbbonamentiBD.GetById(v_cellulare.idAbbonamento, v_ctx);
                //decimal AbbonamentiBD.getCreditoResiduo(v_abbonamento, v_ctx);

                decimal v_saldo = AbbonamentiBD.getCreditoResiduo(v_abbonamento, v_ctx);

                var v_return = new
                {
                    credito = v_saldo,
                    master = v_cellulare.master
                };
                
                return Json(v_return, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// non funziona in quantyo bisognerebbe creare una tabella translogAPP che attualmente non c'è
        /// </summary>
        /// <param name="id"></param>
        /// <param name="iv"></param>
        /// <param name="importo_ricarica"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("ricarica")]     
        public ActionResult ricarica_conto(string id, string iv, string importo_ricarica, string token)
        {

            return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);

            //chiamata:http://localhost:57399/conto/ricarica

            //return Json(new { data = v_lstJoin }, JsonRequestBehavior.AllowGet);


            int v_codiceComune = 0;
            int v_dImportoRicarica = 0;
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


                if (!Utility.VerificaCorrettezzaNTelefono(ref v_numero_telefono)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Utility.VerificaCorrettezzaNAbbonamento(v_codice_conto)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Utility.VerificaCorrettezzaCodiceComune(v_codice_comune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                //if (String.IsNullOrEmpty(codice_comune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Int32.TryParse(v_codice_comune, out v_codiceComune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(numero_telefono)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(codice_conto)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(importo_ricarica)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Int32.TryParse(importo_ricarica, out v_dImportoRicarica)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(token)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                dbEnte v_ctxGenerale = Utility.getGeneraleCtx();
                DbParkCtx v_ctx = Utility.getEnteCtx(v_codiceComune, v_ctxGenerale);

                string v_connectionString = Utility.getEnteStringConnection(v_codiceComune, v_ctxGenerale);
                //Publiservizi.ParkingPatrol.Data.Utilities.ConnectionDB.idEnte = v_codiceComune;

                if (string.IsNullOrEmpty(v_connectionString)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //Effettua il pagamento con Stripe
                try
                {
                    if (v_ctxGenerale != null)
                    {
                        anagrafica_ente v_ente = AnagraficaEnteBD.GetById(v_codiceComune,v_ctxGenerale);
                        if (v_ente != null)
                        {
                            Cellulari v_cellulare = CellulariBD.getCellulareByNumero(v_numero_telefono,v_ctx);
                            if (v_cellulare != null)
                            {
                                //Abbonamenti v_abbonamento = AbbonamentiBD.getById(v_cellulare.idAbbonamento, ref v_ctx);
                                Abbonamenti v_abbonamento = AbbonamentiBD.GetById(v_cellulare.idAbbonamento,v_ctx);
                                if (v_abbonamento != null)
                                {
                                    var charge = new StripeChargeCreateOptions();
                                    var metadata = new Dictionary<string, string>()
                                    {//TODO: G definire i metadati da passare e far apparire sul pagamento su stripe
                                        { "Ricarica " + importo_ricarica, v_codice_conto }
                                    };
                                    charge.Metadata = metadata;
                                    charge.Amount = v_dImportoRicarica;
                                    charge.Currency = "eur";
                                    charge.Capture = true;
                                    charge.SourceTokenOrExistingSourceId = token;

                                    //Circumvent stripe.net date parsing error
                                    var currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
                                    System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
                                    System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.InvariantCulture;
                                    //var chargeService = new StripeChargeService(v_ente.chiave_segreta_stripe);
                                    //var stripeCharge = chargeService.Create(charge);
                                    System.Threading.Thread.CurrentThread.CurrentCulture = currentCulture;
                                    System.Threading.Thread.CurrentThread.CurrentUICulture = currentCulture;
                                    //Fine Circumvent



                                    //creare la nuova tabella transLogApp dove vengono inserite le ricariche
                                    //inserire il record di ricarica
                                    //TitoloTransLogAppDTO v_ticket = new TitoloTransLogAppDTO();

                                    //v_ticket.payDateTime = DateTime.Now;
                                    //v_ticket.extDateTime = DateTime.Now;
                                    //v_ticket.licenseNumber = v_codice_conto; //token;//v_suppInizioSosta.targa;
                                    //v_ticket.token_Input = token;
                                    //v_ticket.idPDM = 335;
                                    //v_ticket.amount = v_dImportoRicarica;// v_suppInizioSosta.identificativoSosta.Replace("PRK", "");
                                    //v_ticket.idPayType = 1;
                                    //if (TitoliTransLogAppBD.insert(v_ticket))
                                    //{

                                    //    Single v_saldo = AbbonamentiBD.creditoResiduo(v_abbonamento);

                                    //    var v_return = new
                                    //    {
                                    //        successo = true,
                                    //        credito = v_saldo
                                    //    };

                                    //    return Json(v_return);
                                    //}
                                    //else
                                    //    return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
                                    ////verificare il credito residuo
                                }
                                else return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
                            }
                            else return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
                        }
                        else return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
                    }
                    else return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
                }
                catch (StripeException ex)
                {
                    
                    return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        //public ActionResult lista_pagamenti(string numero_telefono, string codice_comune, string codice_conto, string offset, string limit)
        public ActionResult lista_pagamenti(string id, string iv, string offset, string limit)
        {
            int v_codiceComune = 0;
            int v_iOffsett = 0;
            int v_iLimit = 0;
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



                if (!Utility.VerificaCorrettezzaNTelefono(ref v_numero_telefono)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Utility.VerificaCorrettezzaNAbbonamento(v_codice_conto)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Utility.VerificaCorrettezzaCodiceComune(v_codice_comune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(codice_comune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Int32.TryParse(v_codice_comune, out v_codiceComune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(numero_telefono)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(codice_conto)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Int32.TryParse(offset, out v_iOffsett)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Int32.TryParse(limit, out v_iLimit)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                dbEnte v_ctxGenerale = Utility.getGeneraleCtx();
                DbParkCtx v_ctx = Utility.getEnteCtx(v_codiceComune, v_ctxGenerale);

                string v_connectionString = Utility.getEnteStringConnection(v_codiceComune, v_ctxGenerale);
                //Publiservizi.ParkingPatrol.Data.Utilities.ConnectionDB.idEnte = v_codiceComune;

                if (string.IsNullOrEmpty(v_connectionString)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                Cellulari v_cellulare = CellulariBD.getCellulareByNumero(v_numero_telefono,v_ctx);
                Abbonamenti v_abbonamento = AbbonamentiBD.GetById(v_cellulare.idAbbonamento,v_ctx);
                List<int> idsCellulari = CellulariBD.GetList(v_ctx).Where(a => a.idAbbonamento == v_abbonamento.idAbbonamento)
                                        .Where(c => !c.dataCessazione.HasValue).Select(c => c.idCellulare).ToList();

                if (v_abbonamento != null)
                {
                    IList<TitoliSMS> v_titoliSms = TitoliSMSBD.GetList(v_ctx).Where(l=> idsCellulari.Contains(l.idCellulare))
                                                                            .OrderByDescending(o => o.dataPagamento).Skip(v_iOffsett).Take(v_iLimit).ToList();
                    //v_cellulariList = CellulareBD.getList(v_abbonamento.idAbbonamento, ref v_ctx);
                    if (v_titoliSms != null)
                    {
                        var v_result = v_titoliSms.Select(x => new
                        {

                            ora_inizio = Utility.DateTimeToUnixTimestampLong(x.dataPagamento),
                            ora_fine = Utility.DateTimeToUnixTimestampLong(x.scadenza),
                            stallo_o_targa = x.codice,
                            costo = x.importo
                        });
                        //return Json(new { USCITA = v_result }, JsonRequestBehavior.AllowGet);
                        return Json(v_result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        //return Json(new { USCITA = numero_telefono = "" }, JsonRequestBehavior.AllowGet);
                        return Json(v_numero_telefono = "", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                    return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);

                //Single v_saldo = AbbonamentiBD.creditoResiduo(v_abbonamento);

                //var v_return = new
                //{
                //    credito = v_saldo,
                //    master = v_cellulare.master
                //};

                ////return Json(new { USCITA = v_return }, JsonRequestBehavior.AllowGet);
                //return Json(v_return, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}