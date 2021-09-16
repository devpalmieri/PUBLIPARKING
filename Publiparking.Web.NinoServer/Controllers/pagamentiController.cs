using Publiparking.Data;
using Publiparking.Data.BD;
using Publisoftware.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Publiparking.Web.NinoServer.Controllers
{
    public class pagamentiController : Controller
    {
        // GET: pagamenti
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("stallo")]
        public ActionResult pagamentoStallo(string id, string iv, string numero_stallo, string id_tariffa, string ora_inizio, string ora_fine)
        {
            //chiamata:http://localhost:57399/pagamenti/stallo
            int v_iCodiceComune = 0;
            int v_iNumeroStallo = 0;
            int v_iIdTariffa = 0;
            double v_dOraInizio = 0;
            double v_dOraFine = 0;
            DateTime v_dtOraInizio;
            DateTime v_dtOraFine;
            bool v_bSuccesso = false;
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

                //if (String.IsNullOrEmpty(numero_telefono)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(codice_comune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!int.TryParse(v_codice_comune, out v_iCodiceComune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(codice_conto)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(numero_stallo)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!int.TryParse(numero_stallo, out v_iNumeroStallo)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(id_tariffa)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!int.TryParse(id_tariffa, out v_iIdTariffa)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(ora_inizio)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Double.TryParse(ora_inizio, out v_dOraInizio)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(ora_fine)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Double.TryParse(ora_fine, out v_dOraFine)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                v_dtOraInizio = Utility.UnixTimeStampToDateTime(v_dOraInizio);
                v_dtOraFine = Utility.UnixTimeStampToDateTime(v_dOraFine);

                if (v_dtOraFine < v_dtOraInizio) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                //connessione
                dbEnte v_ctxGenerale = Utility.getGeneraleCtx();
                DbParkCtx v_ctx = Utility.getEnteCtx(v_iCodiceComune, v_ctxGenerale);
                string v_connectionString = Utility.getEnteStringConnection(v_iCodiceComune, v_ctxGenerale);
                if (string.IsNullOrEmpty(v_connectionString)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }


                Cellulari v_cellulare = CellulariBD.getCellulareByNumero(v_numero_telefono, v_ctx);
                if (v_cellulare != null)
                {
                    //StalloDTO v_stallo = StalloBD.loadByNumero(numero_stallo);
                    Stalli v_stallo = StalliBD.getStalloByNumero(numero_stallo, v_ctx);
                    if (v_stallo != null)
                    {
                        TimeSpan v_tsDiffResult = v_dtOraFine.Subtract(v_dtOraInizio);
                        TitoliSMS v_titoloSMS = TitoliSMSBD.calcolaTitolo(v_stallo, (int)v_tsDiffResult.TotalMinutes, v_dtOraInizio, v_ctx);

                        if (v_titoloSMS.importo > 0)
                        {
                            Abbonamenti v_abbonamento = AbbonamentiBD.getAbbonamentoByCodice(v_codice_conto, v_ctx);
                            if (v_abbonamento != null)
                            {
                                if (AbbonamentiBD.getCreditoResiduo(v_abbonamento, v_ctx) >= v_titoloSMS.importo)
                                {
                                    v_titoloSMS.idCellulare = v_cellulare.idCellulare;
                                    v_titoloSMS.scadenza = v_dtOraFine;
                                    v_ctx.TitoliSMS.Add(v_titoloSMS);
                                    v_ctx.SaveChanges();

                                    OperazioniLocal v_operazione = new OperazioniLocal();
                                    v_operazione.codiceTitolo = v_titoloSMS.codice;
                                    v_operazione.data = v_titoloSMS.dataPagamento;
                                    v_operazione.idOperatore = 2;
                                    v_operazione.idStallo = v_titoloSMS.idStallo;
                                    v_operazione.stato = "O";//Occupato regolare
                                    v_ctx.OperazioniLocal.Add(v_operazione);
                                    v_ctx.SaveChanges();
                                    v_bSuccesso = true;
                                }
                            }
                        }
                    }
                }
                if (v_bSuccesso)
                {
                    var v_return = new
                    {
                        successo = v_bSuccesso
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

        [HttpPost]
        [ActionName("targa")]
        public ActionResult pagamentoTarga(string id, string iv, string numero_targa, string id_tariffa, string ora_inizio, string ora_fine)
        {
            //chiamata:http://localhost:57399/pagamenti/stallo
            int v_iCodiceComune = 0;
            //int v_iNumeroStallo = 0;
            int v_iIdTariffa = 0;
            double v_dOraInizio = 0;
            double v_dOraFine = 0;
            DateTime v_dtOraInizio;
            DateTime v_dtOraFine;
            bool v_bSuccesso = false;
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
                //if (String.IsNullOrEmpty(numero_telefono)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(codice_comune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!int.TryParse(v_codice_comune, out v_iCodiceComune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(codice_conto)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(numero_targa)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(id_tariffa)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!int.TryParse(id_tariffa, out v_iIdTariffa)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(ora_inizio)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Double.TryParse(ora_inizio, out v_dOraInizio)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(ora_fine)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Double.TryParse(ora_fine, out v_dOraFine)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                v_dtOraInizio = Utility.UnixTimeStampToDateTime(v_dOraInizio);
                v_dtOraFine = Utility.UnixTimeStampToDateTime(v_dOraFine);

                if (v_dtOraFine < v_dtOraInizio) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                //connessione
                dbEnte v_ctxGenerale = Utility.getGeneraleCtx();
                DbParkCtx v_ctx = Utility.getEnteCtx(v_iCodiceComune, v_ctxGenerale);
                string v_connectionString = Utility.getEnteStringConnection(v_iCodiceComune, v_ctxGenerale);
                if (string.IsNullOrEmpty(v_connectionString)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                Cellulari v_cellulare = CellulariBD.getCellulareByNumero(v_numero_telefono, v_ctx);
                if (v_cellulare != null)
                {

                    TimeSpan v_tsDiffResult = v_dtOraFine.Subtract(v_dtOraInizio);
                    TitoliSMSTarga v_titoloTargaSMS = TitoliSMSTargaBD.calcolaTitolo((int)v_tsDiffResult.TotalMinutes, v_iIdTariffa, v_dtOraInizio, v_ctx);
                    if (v_titoloTargaSMS.importo > 0)
                    {
                        Abbonamenti v_abbonamento = AbbonamentiBD.getAbbonamentoByCodice(v_codice_conto, v_ctx);
                        if (v_abbonamento != null)
                        {
                            if (AbbonamentiBD.getCreditoResiduo(v_abbonamento, v_ctx) >= v_titoloTargaSMS.importo)
                            {
                                v_titoloTargaSMS.idCellulare = v_cellulare.idCellulare;
                                v_titoloTargaSMS.codice_titolo = numero_targa;
                                v_titoloTargaSMS.scadenza = v_dtOraFine;
                                v_ctx.TitoliSMSTarga.Add(v_titoloTargaSMS);
                                v_ctx.SaveChanges();
                                v_bSuccesso = true;
                            }
                        }
                    }                    
                }
                if (v_bSuccesso)
                {
                    var v_return = new
                    {
                        successo = v_bSuccesso
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


        [HttpGet]
        public ActionResult calcola_importo_stallo(string id, string iv, string numero_stallo, string id_tariffa, string ora_inizio, string ora_fine)
        {
            //chiamata:http://localhost:57399/pagamenti/calcola_importo_stallo?numero_telefono=%2B393921173448&codice_comune=49&codice_conto=921892848761&numero_stallo=100&id_tariffa=1&ora_inizio=1510824968&ora_fine=1510837020

            int v_iCodiceComune = 0;
            int v_iNumeroStallo = 0;
            int v_iIdTariffa = 0;
            double v_dOraInizio = 0;
            double v_dOraFine = 0;
            DateTime v_dtOraInizio;
            DateTime v_dtOraFine;
            bool v_bSuccesso = false;
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

                //if (String.IsNullOrEmpty(numero_telefono)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(codice_comune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!int.TryParse(v_codice_comune, out v_iCodiceComune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(codice_conto)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(numero_stallo)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!int.TryParse(numero_stallo, out v_iNumeroStallo)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(id_tariffa)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!int.TryParse(id_tariffa, out v_iIdTariffa)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(ora_inizio)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Double.TryParse(ora_inizio, out v_dOraInizio)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(ora_fine)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Double.TryParse(ora_fine, out v_dOraFine)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                v_dtOraInizio = Utility.UnixTimeStampToDateTime(v_dOraInizio);
                v_dtOraFine = Utility.UnixTimeStampToDateTime(v_dOraFine);

                if (v_dtOraFine < v_dtOraInizio) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                //connessione
                dbEnte v_ctxGenerale = Utility.getGeneraleCtx();
                DbParkCtx v_ctx = Utility.getEnteCtx(v_iCodiceComune, v_ctxGenerale);
                string v_connectionString = Utility.getEnteStringConnection(v_iCodiceComune, v_ctxGenerale);
                if (string.IsNullOrEmpty(v_connectionString)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }


                Cellulari v_cellulare = CellulariBD.getCellulareByNumero(v_numero_telefono, v_ctx);
                if (v_cellulare != null)
                {

                    Stalli v_stallo = StalliBD.getStalloByNumero(numero_stallo, v_ctx);
                    if (v_stallo != null)
                    {
                        TimeSpan v_tsDiffResult = v_dtOraFine.Subtract(v_dtOraInizio);
                        TitoliSMS v_titoloSMS = TitoliSMSBD.calcolaTitolo(v_stallo, (int)v_tsDiffResult.TotalMinutes, v_dtOraInizio, v_ctx);

                        var v_return = new
                        {
                            importo = v_titoloSMS.importo,
                        };
                        return Json(v_return, JsonRequestBehavior.AllowGet);

                    }
                    else return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
                }
                else return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]        
        public ActionResult calcola_importo_targa(string id, string iv, string numero_targa, string id_tariffa, string ora_inizio, string ora_fine)
        {
            //chiamata:http://localhost:57399/pagamenti/calcola_importo_targa?numero_telefono=%2B393921173448&codice_comune=49&codice_conto=921892848761&numero_targa=AA555AA&id_tariffa=1&ora_inizio=1510824968&ora_fine=1510837020
            int v_iCodiceComune = 0;
            //int v_iNumeroStallo = 0;
            int v_iIdTariffa = 0;
            double v_dOraInizio = 0;
            double v_dOraFine = 0;
            DateTime v_dtOraInizio;
            DateTime v_dtOraFine;
            bool v_bSuccesso = false;
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

                //if (String.IsNullOrEmpty(numero_telefono)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(codice_comune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!int.TryParse(v_codice_comune, out v_iCodiceComune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                //if (String.IsNullOrEmpty(codice_conto)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(numero_targa)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(id_tariffa)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!int.TryParse(id_tariffa, out v_iIdTariffa)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(ora_inizio)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Double.TryParse(ora_inizio, out v_dOraInizio)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(ora_fine)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Double.TryParse(ora_fine, out v_dOraFine)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                v_dtOraInizio = Utility.UnixTimeStampToDateTime(v_dOraInizio);
                v_dtOraFine = Utility.UnixTimeStampToDateTime(v_dOraFine);

                if (v_dtOraFine < v_dtOraInizio) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                //connessione
                dbEnte v_ctxGenerale = Utility.getGeneraleCtx();
                DbParkCtx v_ctx = Utility.getEnteCtx(v_iCodiceComune, v_ctxGenerale);
                string v_connectionString = Utility.getEnteStringConnection(v_iCodiceComune, v_ctxGenerale);
                if (string.IsNullOrEmpty(v_connectionString)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                Cellulari v_cellulare = CellulariBD.getCellulareByNumero(v_numero_telefono, v_ctx);
                if (v_cellulare != null)
                {
                    TimeSpan v_tsDiffResult = v_dtOraFine.Subtract(v_dtOraInizio);
                    TitoliSMSTarga v_titoloSMS = TitoliSMSTargaBD.calcolaTitolo((int)v_tsDiffResult.TotalMinutes, v_iIdTariffa, v_dtOraInizio, v_ctx);

                    var v_return = new
                    {
                        importo = v_titoloSMS.importo
                    };
                    //return Json(new { USCITA = v_return }, JsonRequestBehavior.AllowGet);
                    return Json(v_return, JsonRequestBehavior.AllowGet);
                }
                else return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ActionName("prolunga_stallo")]        
        public ActionResult prolungamentoStallo(string id, string iv, string numero_stallo, string id_tariffa, string ora_inizio, string ora_fine)
        {
            //chiamata:http://localhost:57399/pagamenti/stallo
            int v_iCodiceComune = 0;
            int v_iNumeroStallo = 0;
            int v_iIdTariffa = 0;
            double v_dOraInizio = 0;
            double v_dOraFine = 0;
            DateTime v_dtOraInizio;
            DateTime v_dtOraFine;
            bool v_bSuccesso = false;
            try
            {
                string v_numero_telefono = "";
                string v_codice_comune = "";
                string v_codice_conto = "";
                //da copiare in altre funzioni           
                string v_sInputTotalParameters = Utility.getStringParameters(id, iv);
                string[] v_vectSplitParameters = v_sInputTotalParameters.Split(Utility.decifraturaSeparator);
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
                if (!int.TryParse(v_codice_comune, out v_iCodiceComune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(numero_stallo)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!int.TryParse(numero_stallo, out v_iNumeroStallo)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(id_tariffa)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!int.TryParse(id_tariffa, out v_iIdTariffa)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(ora_inizio)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Double.TryParse(ora_inizio, out v_dOraInizio)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(ora_fine)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Double.TryParse(ora_fine, out v_dOraFine)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                v_dtOraInizio = Utility.UnixTimeStampToDateTime(v_dOraInizio);
                v_dtOraFine = Utility.UnixTimeStampToDateTime(v_dOraFine);

                if (v_dtOraFine < v_dtOraInizio) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                //connessione
                dbEnte v_ctxGenerale = Utility.getGeneraleCtx();
                DbParkCtx v_ctx = Utility.getEnteCtx(v_iCodiceComune, v_ctxGenerale);
                string v_connectionString = Utility.getEnteStringConnection(v_iCodiceComune, v_ctxGenerale);
                if (string.IsNullOrEmpty(v_connectionString)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                Cellulari v_cellulare = CellulariBD.getCellulareByNumero(v_numero_telefono, v_ctx);
                if (v_cellulare != null)
                {
                    Stalli v_stallo = StalliBD.getStalloByNumero(numero_stallo, v_ctx);
                    if (v_stallo != null)
                    {
                        TitoliSMS v_TitoloSmsDaProlungare = TitoliSMSBD.GetList(v_ctx).Where(t => t.idCellulare == v_cellulare.idCellulare)
                                                                                      .Where(t => t.idStallo == v_stallo.idStallo)
                                                                                      .Where(t => t.dataPagamento == v_dtOraInizio).SingleOrDefault();
                        if (v_TitoloSmsDaProlungare != null)
                        {
                            //controllo che il titolo non sia già scaduto perchè una persona può provare a fare il prolungamento dopo aver ricevuto una multa
                            if (DateTime.Now > v_TitoloSmsDaProlungare.scadenza) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                            //controllo che sia un prolungamento
                            if (v_TitoloSmsDaProlungare.scadenza > v_dtOraFine) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                            TimeSpan v_tsDiffResult = v_dtOraFine.Subtract(v_dtOraInizio);
                            TitoliSMS v_titoloSMSCalcoloImporto = TitoliSMSBD.calcolaTitolo(v_stallo, (int)v_tsDiffResult.TotalMinutes, v_dtOraInizio, v_ctx);
                            if (v_titoloSMSCalcoloImporto.importo > 0)
                            {
                                Abbonamenti v_abbonamento = AbbonamentiBD.getAbbonamentoByCodice(v_codice_conto, v_ctx);
                                if (v_abbonamento != null)
                                {
                                    if (AbbonamentiBD.getCreditoResiduo(v_abbonamento, v_ctx) >= (v_titoloSMSCalcoloImporto.importo - v_TitoloSmsDaProlungare.importo))
                                    {
                                        v_TitoloSmsDaProlungare.scadenza = v_dtOraFine;
                                        v_TitoloSmsDaProlungare.importo = v_titoloSMSCalcoloImporto.importo;                                       

                                        OperazioniLocal v_operazione = new OperazioniLocal();
                                        v_operazione.codiceTitolo = v_TitoloSmsDaProlungare.codice;
                                        v_operazione.data = v_TitoloSmsDaProlungare.dataPagamento;
                                        v_operazione.idOperatore = 2;
                                        v_operazione.idStallo = v_TitoloSmsDaProlungare.idStallo;
                                        v_operazione.stato = "O";
                                        v_ctx.OperazioniLocal.Add(v_operazione);
                                        v_ctx.SaveChanges();
                                        v_bSuccesso = true;
                                    }
                                }
                            }
                        }
                    }
                }
                if (v_bSuccesso)
                {
                    var v_return = new
                    {
                        successo = v_bSuccesso
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

        // POST: pagamenti/prolungamentoTarga
        // (ContentType: application/json)
        // Body:
        //{
        //  numero_telefono:%2B393283095707
        //  codice_comune:19
        //  codice_conto:641400503293
        //	numero_targa:1323
        //  id_tariffa:1
        //  ora_inizio:1506441060
        //  ora_fine:1506441060
        //}
        [HttpPost]
        [ActionName("prolunga_targa")]
        public ActionResult prolungamentoTarga(string id, string iv, string numero_targa, string id_tariffa, string ora_inizio, string ora_fine)
        {
            //chiamata:http://localhost:57399/pagamenti/stallo
            int v_iCodiceComune = 0;
            //int v_iNumeroStallo = 0;
            int v_iIdTariffa = 0;
            double v_dOraInizio = 0;
            double v_dOraFine = 0;
            DateTime v_dtOraInizio;
            DateTime v_dtOraFine;
            bool v_bSuccesso = false;
            try
            {

                string v_numero_telefono = "";
                string v_codice_comune = "";
                string v_codice_conto = "";
                //da copiare in altre funzioni           
                string v_sInputTotalParameters = Utility.getStringParameters(id, iv);
                string[] v_vectSplitParameters = v_sInputTotalParameters.Split(Utility.decifraturaSeparator);
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
                if (!int.TryParse(v_codice_comune, out v_iCodiceComune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(numero_targa)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(id_tariffa)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!int.TryParse(id_tariffa, out v_iIdTariffa)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(ora_inizio)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Double.TryParse(ora_inizio, out v_dOraInizio)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(ora_fine)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Double.TryParse(ora_fine, out v_dOraFine)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                v_dtOraInizio = Utility.UnixTimeStampToDateTime(v_dOraInizio);
                v_dtOraFine = Utility.UnixTimeStampToDateTime(v_dOraFine);
                if (v_dtOraFine < v_dtOraInizio) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                //connessione
                dbEnte v_ctxGenerale = Utility.getGeneraleCtx();
                DbParkCtx v_ctx = Utility.getEnteCtx(v_iCodiceComune, v_ctxGenerale);
                string v_connectionString = Utility.getEnteStringConnection(v_iCodiceComune, v_ctxGenerale);
                if (string.IsNullOrEmpty(v_connectionString)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                Cellulari v_cellulare = CellulariBD.getCellulareByNumero(v_numero_telefono, v_ctx);
                if (v_cellulare != null)
                {

                    TitoliSMSTarga v_TitoloSmsTargaDaProlungare = TitoliSMSTargaBD.GetList(v_ctx).Where(t => t.idCellulare == v_cellulare.idCellulare)
                                                                                      .Where(t => t.codice_titolo == numero_targa)
                                                                                      .Where(t => t.dataPagamento == v_dtOraInizio).SingleOrDefault();

                    if (v_TitoloSmsTargaDaProlungare != null)
                    {
                        //controllo che il titolo non sia già scaduto perchè una persona può provare a fare il prolungamento dopo aver ricevuto una multa
                        //if (DateTime.Now>v_TitoloSmsDaProlungare.scadenza ) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                        //controllo che sia un prolungamento
                        if (v_TitoloSmsTargaDaProlungare.scadenza > v_dtOraFine) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                        TimeSpan v_tsDiffResult = v_dtOraFine.Subtract(v_dtOraInizio);
                        TitoliSMSTarga v_titoloSMSCalcoloImporto = TitoliSMSTargaBD.calcolaTitolo((int)v_tsDiffResult.TotalMinutes, v_iIdTariffa, v_dtOraInizio, v_ctx);
                        if (v_titoloSMSCalcoloImporto.importo > 0)
                        {
                            Abbonamenti v_abbonamento = AbbonamentiBD.getAbbonamentoByCodice(v_codice_conto, v_ctx);
                            if (v_abbonamento != null)
                            {
                                if (AbbonamentiBD.getCreditoResiduo(v_abbonamento, v_ctx) >= (v_titoloSMSCalcoloImporto.importo - v_TitoloSmsTargaDaProlungare.importo))
                                {
                                    v_TitoloSmsTargaDaProlungare.importo = v_titoloSMSCalcoloImporto.importo;
                                    v_TitoloSmsTargaDaProlungare.scadenza = v_dtOraFine;                                    
                                    v_ctx.SaveChanges();
                                    v_bSuccesso = true;
                                }
                            }
                        }
                    }
                }
                if (v_bSuccesso)
                {
                    var v_return = new
                    {
                        successo = v_bSuccesso
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

        [HttpPost]
        [ActionName("interrompi_stallo")]
        //public ActionResult pagamentoStallo(string numero_telefono, string codice_comune, string codice_conto, string numero_stallo, string id_tariffa, string ora_inizio, string ora_fine)
        public ActionResult interruzioneStallo(string id, string iv, string numero_stallo, string id_tariffa, string ora_inizio, string ora_fine)
        {
            //chiamata:http://localhost:57399/pagamenti/stallo
            int v_iCodiceComune = 0;
            int v_iNumeroStallo = 0;
            int v_iIdTariffa = 0;
            double v_dOraInizio = 0;
            double v_dOraFine = 0;
            DateTime v_dtOraInizio;
            DateTime v_dtOraFine;
            bool v_bSuccesso = false;
            try
            {
                string v_numero_telefono = "";
                string v_codice_comune = "";
                string v_codice_conto = "";
                //da copiare in altre funzioni           
                string v_sInputTotalParameters = Utility.getStringParameters(id, iv);
                string[] v_vectSplitParameters = v_sInputTotalParameters.Split(Utility.decifraturaSeparator);
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
                if (!int.TryParse(v_codice_comune, out v_iCodiceComune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(numero_stallo)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!int.TryParse(numero_stallo, out v_iNumeroStallo)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(id_tariffa)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!int.TryParse(id_tariffa, out v_iIdTariffa)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(ora_inizio)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Double.TryParse(ora_inizio, out v_dOraInizio)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(ora_fine)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Double.TryParse(ora_fine, out v_dOraFine)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                v_dtOraInizio = Utility.UnixTimeStampToDateTime(v_dOraInizio);
                v_dtOraFine = Utility.UnixTimeStampToDateTime(v_dOraFine);
                /*
                TimeSpan v_tsDiffResultControl = v_dtOraFine.Subtract(v_dtOraInizio);
                if (v_tsDiffResultControl.TotalSeconds<=0) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                */
                if (v_dtOraFine < v_dtOraInizio) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                //connessione
                dbEnte v_ctxGenerale = Utility.getGeneraleCtx();
                DbParkCtx v_ctx = Utility.getEnteCtx(v_iCodiceComune, v_ctxGenerale);
                string v_connectionString = Utility.getEnteStringConnection(v_iCodiceComune, v_ctxGenerale);
                if (string.IsNullOrEmpty(v_connectionString)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                Cellulari v_cellulare = CellulariBD.getCellulareByNumero(v_numero_telefono, v_ctx);
                if (v_cellulare != null)
                {
                    Stalli v_stallo = StalliBD.getStalloByNumero(numero_stallo, v_ctx);
                    if (v_stallo != null)
                    {
                        TitoliSMS v_TitoloSmsDaProlungare = TitoliSMSBD.GetList(v_ctx).Where(t => t.idCellulare == v_cellulare.idCellulare)
                                                                                      .Where(t => t.idStallo == v_stallo.idStallo)
                                                                                      .Where(t => t.dataPagamento == v_dtOraInizio).SingleOrDefault();
                        if (v_TitoloSmsDaProlungare != null)
                        {
                            //controllo che il titolo non sia già scaduto perchè una persona può provare a fare il prolungamento dopo aver ricevuto una multa
                            if (DateTime.Now > v_TitoloSmsDaProlungare.scadenza) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                            //controllo che sia un interruzione
                            if (v_TitoloSmsDaProlungare.scadenza < v_dtOraFine) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                            //old fino al 27/11/2018 TimeSpan v_tsDiffResult = v_dtOraFine.Subtract(v_dtOraInizio);
                            //27/11/2018
                            //come da richiesta di alfredo nella mail del 26/11/2018 verifico che se l'interruzione è avvenuta prima del minuto dall'ora d'inizio allora
                            //aggiungo un minuto all'ora d'inizio così si paga almeno il primo minuto. Prima l'interruzione non era permessa se non era passato un minuto dalla data inizio
                            TimeSpan v_tsDiffResult = v_dtOraFine.Subtract(v_dtOraInizio);
                            if ((int)v_tsDiffResult.TotalMinutes < 1) v_dtOraFine = v_dtOraInizio.AddMinutes(1);
                            v_tsDiffResult = v_dtOraFine.Subtract(v_dtOraInizio);
                            //end 27/11/2018
                            TitoliSMS v_titoloSMSCalcoloImporto = TitoliSMSBD.calcolaTitolo(v_stallo, (int)v_tsDiffResult.TotalMinutes, v_dtOraInizio, v_ctx);
                            if (v_titoloSMSCalcoloImporto.importo > 0)
                            {
                                Abbonamenti v_abbonamento = AbbonamentiBD.getAbbonamentoByCodice(v_codice_conto, v_ctx);
                                if (v_abbonamento != null)
                                {
                                    if (AbbonamentiBD.getCreditoResiduo(v_abbonamento, v_ctx) >= (v_titoloSMSCalcoloImporto.importo - v_TitoloSmsDaProlungare.importo))
                                    {


                                        v_TitoloSmsDaProlungare.importo = v_titoloSMSCalcoloImporto.importo;
                                        v_TitoloSmsDaProlungare.scadenza = v_dtOraFine;

                                        OperazioniLocal v_operazione = new OperazioniLocal();
                                        v_operazione.codiceTitolo = v_TitoloSmsDaProlungare.codice;
                                        v_operazione.data = v_TitoloSmsDaProlungare.dataPagamento;
                                        v_operazione.idOperatore = 2;
                                        v_operazione.idStallo = v_TitoloSmsDaProlungare.idStallo;
                                        v_operazione.stato = "O";
                                        v_ctx.SaveChanges();
                                        v_bSuccesso = true;

                                    }
                                }
                            }
                        }
                    }
                }
                if (v_bSuccesso)
                {
                    var v_return = new
                    {
                        successo = v_bSuccesso
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

        // POST: pagamenti/prolungamentoTarga
        // (ContentType: application/json)
        // Body:
        //{
        //  numero_telefono:%2B393283095707
        //  codice_comune:19
        //  codice_conto:641400503293
        //	numero_targa:1323
        //  id_tariffa:1
        //  ora_inizio:1506441060
        //  ora_fine:1506441060
        //}
        [HttpPost]
        [ActionName("interrompi_targa")]
        //public ActionResult pagamentoTarga(string numero_telefono, string codice_comune, string codice_conto, string numero_targa, string id_tariffa, string ora_inizio, string ora_fine)
        public ActionResult interruzioneTarga(string id, string iv, string numero_targa, string id_tariffa, string ora_inizio, string ora_fine)
        {
            //chiamata:http://localhost:57399/pagamenti/stallo
            int v_iCodiceComune = 0;
            //int v_iNumeroStallo = 0;
            int v_iIdTariffa = 0;
            double v_dOraInizio = 0;
            double v_dOraFine = 0;
            DateTime v_dtOraInizio;
            DateTime v_dtOraFine;
            bool v_bSuccesso = false;
            try
            {

                string v_numero_telefono = "";
                string v_codice_comune = "";
                string v_codice_conto = "";
                //da copiare in altre funzioni           
                string v_sInputTotalParameters = Utility.getStringParameters(id, iv);
                string[] v_vectSplitParameters = v_sInputTotalParameters.Split(Utility.decifraturaSeparator);
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
                if (!int.TryParse(v_codice_comune, out v_iCodiceComune)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(numero_targa)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(id_tariffa)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!int.TryParse(id_tariffa, out v_iIdTariffa)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(ora_inizio)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Double.TryParse(ora_inizio, out v_dOraInizio)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (String.IsNullOrEmpty(ora_fine)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                if (!Double.TryParse(ora_fine, out v_dOraFine)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                v_dtOraInizio = Utility.UnixTimeStampToDateTime(v_dOraInizio);
                v_dtOraFine = Utility.UnixTimeStampToDateTime(v_dOraFine);

                if (v_dtOraFine < v_dtOraInizio) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                //connessione
                dbEnte v_ctxGenerale = Utility.getGeneraleCtx();
                DbParkCtx v_ctx = Utility.getEnteCtx(v_iCodiceComune, v_ctxGenerale);
                string v_connectionString = Utility.getEnteStringConnection(v_iCodiceComune, v_ctxGenerale);
                if (string.IsNullOrEmpty(v_connectionString)) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }

                Cellulari v_cellulare = CellulariBD.getCellulareByNumero(v_numero_telefono, v_ctx);
                if (v_cellulare != null)
                {
                    TitoliSMSTarga v_TitoloSmsTargaDaProlungare = TitoliSMSTargaBD.GetList(v_ctx).Where(t => t.idCellulare == v_cellulare.idCellulare)
                                                                                      .Where(t => t.codice_titolo == numero_targa)
                                                                                      .Where(t => t.dataPagamento == v_dtOraInizio).SingleOrDefault();
                    if (v_TitoloSmsTargaDaProlungare != null)
                    {
                        //controllo che il titolo non sia già scaduto perchè una persona può provare a fare il prolungamento dopo aver ricevuto una multa
                        if (DateTime.Now > v_TitoloSmsTargaDaProlungare.scadenza) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }
                        //controllo che sia un interruzione
                        if (v_TitoloSmsTargaDaProlungare.scadenza < v_dtOraFine) { return Json(new { ERRORE = Utility.returnBadRequest(Response) }, JsonRequestBehavior.AllowGet); }


                        //old fino al 27/11/2018 TimeSpan v_tsDiffResult = v_dtOraFine.Subtract(v_dtOraInizio);
                        //27/11/2018
                        //come da richiesta di alfredo nella mail del 26/11/2018 verifico che se l'interruzione è avvenuta prima del minuto dall'ora d'inizio allora
                        //aggiungo un minuto all'ora d'inizio così si paga almeno il primo minuto. Prima l'interruzione non era permessa se non era passato un minuto dalla data inizio
                        TimeSpan v_tsDiffResult = v_dtOraFine.Subtract(v_dtOraInizio);
                        if ((int)v_tsDiffResult.TotalMinutes < 1) v_dtOraFine = v_dtOraInizio.AddMinutes(1);
                        v_tsDiffResult = v_dtOraFine.Subtract(v_dtOraInizio);
                        //end 27/11/2018



                        TitoliSMSTarga v_titoloSMSCalcoloImporto = TitoliSMSTargaBD.calcolaTitolo((int)v_tsDiffResult.TotalMinutes, v_iIdTariffa, v_dtOraInizio,v_ctx);
                        if (v_titoloSMSCalcoloImporto.importo > 0)
                        {
                            Abbonamenti v_abbonamento = AbbonamentiBD.getAbbonamentoByCodice(v_codice_conto,v_ctx);
                            if (v_abbonamento != null)
                            {
                                if (AbbonamentiBD.getCreditoResiduo(v_abbonamento,v_ctx) >= (v_titoloSMSCalcoloImporto.importo - v_TitoloSmsTargaDaProlungare.importo))
                                {
                                    v_TitoloSmsTargaDaProlungare.importo = v_titoloSMSCalcoloImporto.importo;
                                    v_TitoloSmsTargaDaProlungare.scadenza = v_dtOraFine;
                                    v_ctx.SaveChanges();
                                    v_bSuccesso = true;
                                }
                            }
                        }
                    }
                }
                if (v_bSuccesso)
                {
                    var v_return = new
                    {
                        successo = v_bSuccesso
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