using Publiparking.Data.dto;
using Publiparking.Data.dto.type;
using Publiparking.Data.LinqExtended;
using Publisoftware.Data;
using Publisoftware.Data.BD;
using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Publiparking.Data.BD
{
    public class StalliBD : EntityBD<Stalli>
    {
        private static ILogger m_logger = LoggerFactory.getInstance().getLogger<NLogger>("StalliBD");
        public StalliBD()
        {

        }

        public static Stalli getStalloByNumero(string p_numero, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("getStalloByNumero: {0}",p_numero), EnLogSeverity.Debug);
            return GetList(p_context).Where(a => a.numero.Equals(p_numero)).SingleOrDefault();

        }
        public static StatoStalloExt getStatoCorrente(Int32 pIdStallo, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("getStatoCorrente ---> id: {0}", pIdStallo.ToString()), EnLogSeverity.Debug);
            OperazioneDTO v_operazioni = OperazioneBD.loadLast(pIdStallo, p_context);
            return getStato(v_operazioni, p_context);
        }
        public static StatoStalloExt getUltimoStatoAbusivo(Int32 pIdStallo, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("getUltimoStatoAbusivo ---> id: {0}", pIdStallo.ToString()), EnLogSeverity.Debug);

            OperazioneDTO v_operazione = OperazioneBD.loadLastStatoBeforePreavviso(pIdStallo, p_context);
            return getStato(v_operazione, p_context);
        }

        public static bool isScadutoOltreTolleranza(Int32 pIDStallo, Int32 p_minutiTolleranzaVerbale, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("isScadutoOltreTolleranza ---> id: {0}", pIDStallo.ToString()), EnLogSeverity.Debug);

            bool risp = false;


            StatoStalloExt v_statoCorrente = getStatoCorrente(pIDStallo, p_context);

            if (v_statoCorrente.Stato == OperazioneDTO.statoPagamentoScaduto)
            {
                OperazioneDTO vOperazione = OperazioneBD.loadLast(pIDStallo, p_context);
                TitoloDTO vTitolo = TitoloBD.loadByCodice(vOperazione.codiceTitolo, p_context);

                if (vTitolo != null)
                {
                    if (vTitolo.scadenza.AddMinutes(p_minutiTolleranzaVerbale) < DateTime.Now)
                        risp = true;
                }
            }

            return risp;
        }
        
        public static bool isVerbalizzabileSosta(Int32 pIdStallo, string pTarga, Int32 p_minutiTolleranzaVerbale, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("isVerbalizzabileSosta ---> id: {0}", pIdStallo.ToString()), EnLogSeverity.Debug);
            bool risp = false;
            OperazioneDTO v_ultimoPreavviso = OperazioneBD.loadLastPreavviso(pIdStallo, p_context);

            if (v_ultimoPreavviso != null && v_ultimoPreavviso.stato == OperazioneDTO.statoPreavviso)
            {
                TimeSpan v_adesso = DateTime.Now.TimeOfDay;
                TimeSpan v_oraOperazione = v_ultimoPreavviso.data.TimeOfDay;
                TimeSpan v_tempoTrascorso = v_adesso.Subtract(v_oraOperazione);

                if (v_tempoTrascorso.TotalMinutes > p_minutiTolleranzaVerbale)
                {
                    if (pTarga.Trim().Length == 0)
                        risp = true;
                    else if (v_ultimoPreavviso.targa != null && v_ultimoPreavviso.targa.ToUpper().Trim() == pTarga.ToUpper().Trim())
                        risp = true;
                }
            }

            return risp;
        }

        public static bool setStatoToLibero(Int32 pIdOperatore, Int32 pIdStallo, Int32 pIdGiro, double pX, double pY, string pFileFoto, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("setStatoToLibero ---> id: {0}", pIdStallo.ToString()), EnLogSeverity.Debug);

            bool risp = false;
            OperazioneDTO vStatoNuovo = new OperazioneDTO();
            OperazioneParamRequired vRequiredParam = OperatoriBD.nextOperazioneParamRequired(pIdOperatore, pIdStallo, p_context);
            Operatori vOperatore = OperatoriBD.GetById(pIdOperatore, p_context);

            if ((!vRequiredParam.foto) | pFileFoto.Length > 0)
            {
                if ((!vRequiredParam.GPS) | ((pX * pY) != 0))
                {
                    vStatoNuovo.idOperatore = vOperatore.idOperatore;
                    vStatoNuovo.idStallo = pIdStallo;
                    vStatoNuovo.stato = OperazioneDTO.statoLibero;
                    vStatoNuovo.fileFoto = pFileFoto;
                    vStatoNuovo.X = pX;
                    vStatoNuovo.Y = pY;

                    if (OperazioniLocalBD.CreaOperazioneDaDTO(vStatoNuovo, p_context))
                    {
                        updateFotoRichiesta(pIdGiro, pIdStallo, p_context);
                        OperatoriBD.decreaseNoGps(pIdOperatore, p_context);
                        p_context.SaveChanges();
                        risp = true;
                    }
                }
            }

            return risp;
        }

        public static bool setStatoToOccupatoRegolare(Int32 pIdOperatore, Int32 pIdStallo, Int32 pIdGiro, double pX, double pY, string pTarga, string pFileFoto, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("setStatoToOccupatoRegolare ---> id: {0}", pIdStallo.ToString()), EnLogSeverity.Debug);

            bool risp = false;
            OperazioneDTO vStatoNuovo = new OperazioneDTO();
            OperazioneParamRequired vRequiredParam = OperatoriBD.nextOperazioneParamRequired(pIdOperatore, pIdStallo, p_context);


            if ((!vRequiredParam.foto) | pFileFoto.Length > 0)
            {
                if ((!vRequiredParam.GPS) | ((pX * pY) != 0))
                {
                    vStatoNuovo.idOperatore = pIdOperatore;
                    vStatoNuovo.idStallo = pIdStallo;
                    vStatoNuovo.stato = OperazioneDTO.statoRegolareConNumero;
                    vStatoNuovo.codiceTitolo = TitoloBD.getUltimoPagatoByIdStallo(pIdStallo, p_context).codice;
                    vStatoNuovo.fileFoto = pFileFoto;
                    vStatoNuovo.X = pX;
                    vStatoNuovo.Y = pY;
                    vStatoNuovo.targa = pTarga;

                    if (OperazioniLocalBD.CreaOperazioneDaDTO(vStatoNuovo, p_context))
                    {
                        updateFotoRichiesta(pIdGiro, pIdStallo, p_context);
                        OperatoriBD.decreaseNoGps(pIdOperatore, p_context);
                        p_context.SaveChanges();
                        risp = true;
                    }
                }
            }

            return risp;
        }

        public static bool setStatoToOccupatoConTitolo(Int32 pIdOperatore, Int32 pIdStallo, Int32 pIdGiro, double pX, double pY, string pCodiceTitolo, DbParkCtx p_context, string pTarga = "")
        {
            m_logger.LogMessage(String.Format("setStatoToOccupatoConTitolo ---> id: {0}", pIdStallo.ToString()), EnLogSeverity.Debug);
            bool risp = false;
            OperazioneDTO vStatoNuovo = new OperazioneDTO();
            OperazioneParamRequired vRequiredParam = OperatoriBD.nextOperazioneParamRequired(pIdOperatore, pIdStallo, p_context);

            if ((!vRequiredParam.GPS) | ((pX * pY) != 0))
            {
                if (pCodiceTitolo.Length > 12 && !string.IsNullOrEmpty(pTarga) && pTarga.Length > 5)
                {
                    AbbonamentiPeriodici v_abbonamento = AbbonamentiPeriodiciBD.getAbbonamentoByCodice(pCodiceTitolo, p_context);
                    if (v_abbonamento != null && v_abbonamento.targa.ToUpper().Contains(pTarga.ToUpper()))
                    {
                        vStatoNuovo.idOperatore = pIdOperatore;
                        vStatoNuovo.idStallo = pIdStallo;
                        vStatoNuovo.stato = OperazioneDTO.statoRegolareConBiglietto;
                        vStatoNuovo.codiceTitolo = pCodiceTitolo;
                        vStatoNuovo.X = pX;
                        vStatoNuovo.Y = pY;
                        vStatoNuovo.targa = pTarga;
                        if (OperazioniLocalBD.CreaOperazioneDaDTO(vStatoNuovo, p_context))
                        {
                            updateFotoRichiesta(pIdGiro, pIdStallo, p_context);
                            OperatoriBD.decreaseNoGps(pIdOperatore, p_context);
                            p_context.SaveChanges();
                            risp = true;
                        }

                    }
                }
                //TODO: Gestione Abbonamento
                /*
                                If pCodiceTitolo.Length = 12 AndAlso pTarga.Length > 5 Then
                                    Dim v_abbonamento As AbbonamentoPeriodicoDTO = AbbonamentoPeriodicoBD.loadByCodice(pCodiceTitolo)

                                    If v_abbonamento IsNot Nothing AndAlso v_abbonamento.targa.ToUpper.Contains(pTarga.ToUpper) Then
                                        vStatoNuovo.idOperatore = pIdOperatore
                                        vStatoNuovo.idStallo = pIdStallo
                                        vStatoNuovo.stato = OperazioneDTO.statoRegolareConBiglietto
                                        vStatoNuovo.codiceTitolo = pCodiceTitolo
                                        vStatoNuovo.X = pX
                                        vStatoNuovo.Y = pY
                                        vStatoNuovo.targa = pTarga

                                        If OperazioneBD.insert(vStatoNuovo) > 0 Then
                                            risp = True
                                        End If
                                    End If
                                Else
                */

                else
                {
                    vStatoNuovo.idOperatore = pIdOperatore;
                    vStatoNuovo.idStallo = pIdStallo;
                    vStatoNuovo.stato = OperazioneDTO.statoRegolareConBiglietto;
                    vStatoNuovo.codiceTitolo = pCodiceTitolo;
                    vStatoNuovo.X = pX;
                    vStatoNuovo.Y = pY;

                    if (!String.IsNullOrEmpty(pTarga))
                    {
                        vStatoNuovo.targa = pTarga;
                    }

                    if (OperazioniLocalBD.CreaOperazioneDaDTO(vStatoNuovo, p_context))
                    {
                        updateFotoRichiesta(pIdGiro, pIdStallo, p_context);
                        OperatoriBD.decreaseNoGps(pIdOperatore, p_context);
                        p_context.SaveChanges();
                        risp = true;
                    }
                }

                StalliTargheBD.creaStalloTarga(pIdOperatore, pIdStallo, pTarga, p_context);
                //chiusura del codice da inserire x abbonamento (Voluto anche con targa vuota per sganciare lo stallo dalla targa)
                //If risp = True Then
                //    StalloTargaBD.insert(pIdStallo, pTarga, pIdOperatore, Now)
                //End If

            }
            return risp;
        }

        public static bool setStatoToOccupatoGratuito(Int32 pIdOperatore, Int32 pIdStallo, Int32 pIdGiro, double pX, double pY, string pFileFoto, string pCodicePermesso, string pComune, string pTarga, DbParkCtx p_context, dbEnte p_contextEnte)
        {
            m_logger.LogMessage(String.Format("setStatoToOccupatoGratuito ---> id: {0}", pIdStallo.ToString()), EnLogSeverity.Debug);
            bool risp = false;
            OperazioneDTO vStatoNuovo = new OperazioneDTO();
            OperazioneParamRequired vRequiredParam = OperatoriBD.nextOperazioneParamRequired(pIdOperatore, pIdStallo, p_context);

            if ((!vRequiredParam.foto) | pFileFoto.Length > 0)
            {
                if ((!vRequiredParam.GPS) | ((pX * pY) != 0))
                {
                    ser_comuni v_comune = SerComuniBD.GetListAttivi(p_contextEnte).Where(c => c.des_comune.Equals(pComune)).FirstOrDefault();
                    Int32 v_idComune = v_comune != null ? v_comune.cod_comune : -1;
                    Int32 v_idPermesso = 0;
                    Permessi v_permesso = PermessiBD.getPermessoByCodiceAndCodComune(pCodicePermesso, v_idComune, p_context);

                    if (v_permesso == null)
                    {
                        v_permesso = new Permessi();
                        v_permesso.codice = pCodicePermesso;
                        v_permesso.idComune = v_idComune;
                        p_context.Permessi.Add(v_permesso);
                        p_context.SaveChanges();
                    }
                    else
                        v_idPermesso = v_permesso.idPermesso;

                    vStatoNuovo.idOperatore = pIdOperatore;
                    vStatoNuovo.idStallo = pIdStallo;
                    vStatoNuovo.stato = OperazioneDTO.statoGratuito;
                    vStatoNuovo.fileFoto = pFileFoto;
                    vStatoNuovo.X = pX;
                    vStatoNuovo.Y = pY;
                    vStatoNuovo.codiceTitolo = v_idPermesso.ToString() + ";" + pTarga;
                    vStatoNuovo.targa = pTarga;

                    if (OperazioniLocalBD.CreaOperazioneDaDTO(vStatoNuovo, p_context))
                    {
                        updateFotoRichiesta(pIdGiro, pIdStallo, p_context);
                        OperatoriBD.decreaseNoGps(pIdOperatore, p_context);
                        p_context.SaveChanges();
                        risp = true;
                    }
                }
            }

            return risp;
        }


        public static Int32 setStatoToOccupatoAbusivo(Int32 pIdOperatore, Int32 pIdStallo, Int32 pIdGiro,
            double pX, double pY, List<string> pFileFoto, string pTipoVeicolo, string pMarca,
            string pModello, string pTarga, bool pTargaEstera, string pUbicazione,
            bool pAssenzaTrasgressore, string pCodiceBollettino,
            string p_serie, List<Int32> pCodiciViolati, string pNote, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("setStatoToOccupatoAbusivo ---> id: {0}", pIdStallo.ToString()), EnLogSeverity.Debug);

            // TODO: Non riemettere il verbale se il veicolo non è cambiato (oldStato = occupatoAbusivo)
            Int32 risp = -1;
            OperazioneParamRequired vRequiredParam = OperatoriBD.nextOperazioneParamRequired(pIdOperatore, pIdStallo, p_context);
            if ((!vRequiredParam.foto) | pFileFoto.Count > 0)
            {
                if ((!vRequiredParam.GPS) | ((pX * pY) != 0))
                {

                    Verbali vVerbale = new Verbali();

                    vVerbale.idOperatore = pIdOperatore;
                    vVerbale.data = DateTime.Now;
                    vVerbale.idStallo = pIdStallo;
                    vVerbale.via = pUbicazione;
                    vVerbale.tipoVeicolo = pTipoVeicolo;
                    vVerbale.marca = pMarca;
                    vVerbale.modello = pModello;
                    vVerbale.targa = pTarga;
                    vVerbale.targaEstera = pTargaEstera;
                    vVerbale.codiceBollettino = pCodiceBollettino;
                    vVerbale.serie = p_serie;
                    vVerbale.note = pNote;
                    vVerbale.assenzatrasgressore = pAssenzaTrasgressore;
                    //vVerbale.codiciViolati = pCodiciViolati;
                    CausaliVerbali cv;
                    decimal v_totale = 0;
                    foreach (int codice in pCodiciViolati)
                    {
                        cv = new CausaliVerbali();
                        cv.Verbali = vVerbale;
                        cv.idCausale = codice;
                        p_context.CausaliVerbali.Add(cv);
                        Causali v_causale = CausaliBD.GetById(codice, p_context);
                        v_totale = v_totale + v_causale.importo;
                    }
                    vVerbale.totale = v_totale;

                    //vVerbale.foto = pFileFoto;
                    FotoVerbali fv;
                    foreach (string v_foto in pFileFoto)
                    {
                        fv = new FotoVerbali();
                        fv.Verbali = vVerbale;
                        fv.fileFoto = v_foto;
                        p_context.FotoVerbali.Add(fv);
                    }
                    p_context.Verbali.Add(vVerbale);

                    OperazioneDTO vStatoNuovo = new OperazioneDTO();

                    vStatoNuovo.idOperatore = pIdOperatore;
                    vStatoNuovo.idStallo = pIdStallo;
                    vStatoNuovo.stato = OperazioneDTO.statoAbusivo;
                    vStatoNuovo.fileFoto = pFileFoto[0];
                    vStatoNuovo.X = pX;
                    vStatoNuovo.Y = pY;
                    vStatoNuovo.idVerbale = vVerbale.idVerbale;
                    vStatoNuovo.targa = pTarga;

                    if (OperazioniLocalBD.CreaOperazioneDaDTO(vStatoNuovo, p_context))
                    {
                        updateFotoRichiesta(pIdGiro, pIdStallo, p_context);
                        OperatoriBD.decreaseNoGps(pIdOperatore, p_context);
                        p_context.SaveChanges();
                        risp = vVerbale.idVerbale;
                    }
                   

                }
            }

            return risp;
        }

        public static Int32 setStatoToOccupatoAbusivoPenale(Int32 pIdOperatore, Int32 pIdStallo, Int32 pIdGiro, double pX, double pY, List<string> pFileFoto, string pTipoVeicolo, string pMarca, string pModello, string pTarga, bool pTargaEstera, string pUbicazione, bool pAssenzaTrasgressore, string pNote, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("setStatoToOccupatoAbusivoPenale ---> id: {0}", pIdStallo.ToString()), EnLogSeverity.Debug);

            // TODO: Non riemettere la penale se il veicolo non è cambiato (oldStato = occupatoAbusivo)
            Int32 risp = -1;
            OperazioneParamRequired vRequiredParam = OperatoriBD.nextOperazioneParamRequired(pIdOperatore, pIdStallo, p_context);

            if ((!vRequiredParam.foto) | pFileFoto.Count > 0)
            {
                if ((!vRequiredParam.GPS) | ((pX * pY) != 0))
                {

                    Penali v_penale = new Penali();
                    v_penale.codice = PenaliBD.generaCodice(p_context);
                    v_penale.idOperatore = pIdOperatore;
                    v_penale.data = DateTime.Now;
                    v_penale.idStallo = pIdStallo;
                    v_penale.via = pUbicazione;
                    v_penale.tipoVeicolo = pTipoVeicolo;
                    v_penale.marca = pMarca;
                    v_penale.modello = pModello;
                    v_penale.targa = pTarga;
                    v_penale.targaEstera = pTargaEstera;
                    v_penale.note = pNote;
                    v_penale.assenzatrasgressore = pAssenzaTrasgressore;
                    FotoPenali fp;
                    foreach (string v_foto in pFileFoto)
                    {
                        fp = new FotoPenali();
                        fp.Penali = v_penale;
                        fp.fileFoto = v_foto;
                        p_context.FotoPenali.Add(fp);
                    }
                    Stalli v_stallo = StalliBD.GetById(pIdStallo, p_context);
                    //v_penale.totale = (v_stallo.Tariffe.Penale.HasValue) ? (v_stallo.Tariffe.Penale.Value) / 100 : 0;

                     decimal v_importoPenale = (v_stallo.Tariffe.Penale.HasValue) ? (v_stallo.Tariffe.Penale.Value) / 100 : 0;

                    if(v_importoPenale == 0)
                    {
                        //int v_minuti = DateDiff(DateInterval.Minute, DateTime Now, Now.Date.AddDays(1))
                        DateTime v_oggi = DateTime.Now;
                        DateTime v_domani = DateTime.Now.Date.AddDays(1);
                        TimeSpan ts = v_domani - v_oggi;
                        int v_minuti = (int)ts.TotalMinutes;

                        TitoliOperatori v_titolo = TitoliOperatoriBD.calcolaByTariffa(v_stallo.Tariffe, v_minuti, DateTime.Now, p_context);
                        v_importoPenale = Math.Ceiling(v_titolo.importo / 100);
                    }

                    v_penale.totale = v_importoPenale;

                    p_context.Penali.Add(v_penale);

                    OperazioneDTO vStatoNuovo = new OperazioneDTO();

                    vStatoNuovo.idOperatore = pIdOperatore;
                    vStatoNuovo.idStallo = pIdStallo;
                    vStatoNuovo.stato = OperazioneDTO.statoAbusivo;
                    vStatoNuovo.fileFoto = pFileFoto[0];
                    vStatoNuovo.X = pX;
                    vStatoNuovo.Y = pY;
                    vStatoNuovo.idPenale = v_penale.idPenale;
                    vStatoNuovo.targa = pTarga;

                    if (OperazioniLocalBD.CreaOperazioneDaDTO(vStatoNuovo, p_context))
                    {
                        updateFotoRichiesta(pIdGiro, pIdStallo, p_context);
                        OperatoriBD.decreaseNoGps(pIdOperatore, p_context);
                        p_context.SaveChanges();
                        risp = v_penale.idPenale;
                    }


                }
            }

            return risp;
        }

        public static bool setStatoToPreavviso(Int32 pIdOperatore, Int32 pIdStallo, Int32 pIdGiro, double pX, double pY, string pFileFoto, string pTarga, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("setStatoToPreavviso ---> id: {0}", pIdStallo.ToString()), EnLogSeverity.Debug);

            bool risp = false;
            OperazioneDTO vStatoNuovo = new OperazioneDTO();
            OperazioneParamRequired vRequiredParam = OperatoriBD.nextOperazioneParamRequired(pIdOperatore, pIdStallo, p_context);
            if ((!vRequiredParam.foto) | pFileFoto.Length > 0)
            {
                if ((!vRequiredParam.GPS) | ((pX * pY) != 0))
                {
                    vStatoNuovo.idOperatore = pIdOperatore;
                    vStatoNuovo.idStallo = pIdStallo;
                    vStatoNuovo.stato = OperazioneDTO.statoPreavviso;
                    vStatoNuovo.fileFoto = pFileFoto;
                    vStatoNuovo.X = pX;
                    vStatoNuovo.Y = pY;
                    vStatoNuovo.targa = pTarga;

                    if (OperazioniLocalBD.CreaOperazioneDaDTO(vStatoNuovo, p_context))
                    {
                        updateFotoRichiesta(pIdGiro, pIdStallo, p_context);
                        OperatoriBD.decreaseNoGps(pIdOperatore, p_context);
                        p_context.SaveChanges();
                        risp = true;
                    }
                }
            }

            return risp;
        }

        public static bool setStatoToVerbalizzato(Int32 pIdOperatore, Int32 pIdStallo, Int32 pIdGiro, double pX, double pY, string pCodiceBollettino, string p_serie, string pFileFoto, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("setStatoToVerbalizzato ---> id: {0}", pIdStallo.ToString()), EnLogSeverity.Debug);

            bool risp = false;
            OperazioneDTO vStatoNuovo = new OperazioneDTO();
            OperazioneParamRequired vRequiredParam = OperatoriBD.nextOperazioneParamRequired(pIdOperatore, pIdStallo, p_context);
            // TODO: Richiesta da Alfredo e da rimuovere
            // Dim vOperatore As OperatoreDTO = OperatoreBD.loadById(pIdOperatore)

            if ((!vRequiredParam.foto) | pFileFoto.Length > 0)
            {
                if ((!vRequiredParam.GPS) | ((pX * pY) != 0))
                {
                    vStatoNuovo.idOperatore = pIdOperatore;
                    vStatoNuovo.idStallo = pIdStallo;
                    vStatoNuovo.stato = OperazioneDTO.statoVerbalizzato;
                    vStatoNuovo.fileFoto = pFileFoto;

                    Verbali vVerbale = VerbaliBD.loadByCodiceBollettino(pCodiceBollettino, p_serie, p_serie, p_context);
                    if (vVerbale != null)
                        vStatoNuovo.idVerbale = vVerbale.idVerbale;

                    Penali v_penale = PenaliBD.loadByCodice(pCodiceBollettino, p_context);
                    if (v_penale != null)
                        vStatoNuovo.idPenale = v_penale.idPenale;

                    vStatoNuovo.X = pX;
                    vStatoNuovo.Y = pY;

                    if (OperazioniLocalBD.CreaOperazioneDaDTO(vStatoNuovo, p_context))
                    {
                        updateFotoRichiesta(pIdGiro, pIdStallo, p_context);
                        OperatoriBD.decreaseNoGps(pIdOperatore, p_context);
                        p_context.SaveChanges();
                        risp = true;
                    }
                }
            }

            return risp;
        }

        public static bool setStatoToGiaPreavvisato(Int32 pIdOperatore, Int32 pIdStallo, Int32 pIdGiro, double pX, double pY, string pFileFoto, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("setStatoToGiaPreavvisato ---> id: {0}", pIdStallo.ToString()), EnLogSeverity.Debug);
            bool risp = false;
            OperazioneDTO vStatoNuovo = new OperazioneDTO();
            OperazioneParamRequired vRequiredParam = OperatoriBD.nextOperazioneParamRequired(pIdOperatore, pIdStallo, p_context);
            // TODO: Richiesta da Alfredo e da rimuovere
            // Dim vOperatore As OperatoreDTO = OperatoreBD.loadById(pIdOperatore)

            if ((!vRequiredParam.foto) | pFileFoto.Length > 0)
            {
                if ((!vRequiredParam.GPS) | ((pX * pY) != 0))
                {
                    vStatoNuovo.idOperatore = pIdOperatore;
                    vStatoNuovo.idStallo = pIdStallo;
                    vStatoNuovo.stato = OperazioneDTO.statoGiaPreavvisato;
                    vStatoNuovo.fileFoto = pFileFoto;
                    vStatoNuovo.X = pX;
                    vStatoNuovo.Y = pY;

                    if (OperazioniLocalBD.CreaOperazioneDaDTO(vStatoNuovo, p_context))
                    {
                        updateFotoRichiesta(pIdGiro, pIdStallo, p_context);
                        OperatoriBD.decreaseNoGps(pIdOperatore, p_context);
                        p_context.SaveChanges();
                        risp = true;
                    }
                }
            }
            return risp;
        }

        internal static void updateFotoRichiesta(Int32 pIdGiro, Int32 pIdStallo, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("updateFotoRichiesta ---> id: {0}", pIdStallo.ToString()), EnLogSeverity.Debug);

            bool vNewVal = false;
            Giri vGiro;
            Stalli vStallo;
            Configurazione v_configurazione = ConfigurazioneBD.GetList(p_context).FirstOrDefault();
            Int32 vFrequenzaFoto = v_configurazione.frequenzaFoto;

            Int32 vOraCorrente = DateTime.Now.Hour;

            vStallo = StalliBD.GetById(pIdStallo, p_context);
            if (!(vStallo == null))
            {
                if (vStallo.StalliFrequenzaFoto != null && vStallo.StalliFrequenzaFoto.oraInizio <= vOraCorrente &&
                   vOraCorrente < vStallo.StalliFrequenzaFoto.oraFine)
                {
                    vFrequenzaFoto = vStallo.StalliFrequenzaFoto.frequenzaFoto;
                }

            }

            vGiro = GiriBD.GetById(pIdGiro, p_context);
            if (!(vGiro == null))
            {
                if (vGiro.GiriFrequenzaFoto != null && vGiro.GiriFrequenzaFoto.oraInizio <= vOraCorrente &&
                   vOraCorrente < vGiro.GiriFrequenzaFoto.oraFine)
                {
                    vFrequenzaFoto = vGiro.GiriFrequenzaFoto.frequenzaFoto;
                }
            }

            Random rnd = new Random();
            int value = Convert.ToInt32(100 * rnd.NextDouble());

            if (value > (100 - vFrequenzaFoto))
                vNewVal = true;

            vStallo.fotoRichiesta = vNewVal;

        }

        private static StatoStalloExt getStato(OperazioneDTO p_operazione,DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("getStato"), EnLogSeverity.Debug);

            // HACK: Gestire i nuovi stati
            StatoStalloExt risp = null;

            if (!(p_operazione == null))
            {
                risp = new StatoStalloExt();
                risp.Targa =string.IsNullOrEmpty(p_operazione.targa) ? "" : p_operazione.targa;
                risp.Data = p_operazione.data;

                switch (p_operazione.stato)
                {
                    case OperazioneDTO.statoLibero:
                        {
                            risp.Stato = p_operazione.stato;
                            break;
                        }

                    case OperazioneDTO.statoRegolareConNumero:
                        {
                            TitoloDTO vTitolo;

                            if (p_operazione.codiceTitolo == null || p_operazione.codiceTitolo.Length == 0)
                                vTitolo = TitoloBD.loadByIdStallo(p_operazione.idStallo, p_context);
                            else
                                vTitolo = TitoloBD.loadByCodice(p_operazione.codiceTitolo, p_context);

                            if (vTitolo != null)
                            {
                                if (vTitolo.scadenza >= DateTime.Now)
                                {
                                    risp.Stato = p_operazione.stato;
                                    risp.Note = "Scad.: " + vTitolo.scadenza.ToShortTimeString() + " " + vTitolo.scadenza.ToShortDateString();
                                }
                                else if (vTitolo.scadenza < DateTime.Now & vTitolo.scadenza > DateTime.Now.Date)
                                {
                                    risp.Stato = OperazioneDTO.statoPagamentoScaduto;

                                    TimeSpan v_adesso = DateTime.Now.TimeOfDay;
                                    TimeSpan v_oraScadenza = vTitolo.scadenza.TimeOfDay;
                                    TimeSpan v_tempoScaduto = v_adesso.Subtract(v_oraScadenza);

                                    risp.Note = "scaduto da: " + Math.Truncate(v_tempoScaduto.TotalMinutes).ToString() + " min.";
                                }
                                else
                                    risp.Stato = OperazioneDTO.statoLibero;
                            }

                            break;
                        }

                    case OperazioneDTO.statoRegolareConBiglietto:
                        {
                            TitoloDTO vTitolo = TitoloBD.loadByCodice(p_operazione.codiceTitolo, p_context);
                            if (vTitolo != null)
                            {
                                if (vTitolo.scadenza >= DateTime.Now)
                                {
                                    risp.Stato = p_operazione.stato;
                                    risp.Note = "Scad.: " + vTitolo.scadenza.ToShortTimeString() + " " + vTitolo.scadenza.ToShortDateString();
                                }
                                else if (vTitolo.scadenza < DateTime.Now & vTitolo.scadenza > DateTime.Now.Date)
                                {
                                    risp.Stato = OperazioneDTO.statoPagamentoScaduto;

                                    TimeSpan v_adesso = DateTime.Now.TimeOfDay;
                                    TimeSpan v_oraScadenza = vTitolo.scadenza.TimeOfDay;
                                    TimeSpan v_tempoScaduto = v_adesso.Subtract(v_oraScadenza);

                                    risp.Note = "scaduto da: " + Math.Truncate(v_tempoScaduto.TotalMinutes).ToString() + " min.";
                                }
                                else
                                    risp.Stato = OperazioneDTO.statoLibero;
                            }

                            break;
                        }

                    case OperazioneDTO.statoPreavviso:
                        {
                            OperazioneDTO v_operazionePrec = OperazioneBD.loadLastBeforeOperazione(p_operazione,p_context);

                            if (v_operazionePrec.stato == OperazioneDTO.statoRegolareConNumero)
                            {
                                TitoloDTO vTitolo;

                                if (v_operazionePrec.codiceTitolo == null || v_operazionePrec.codiceTitolo.Length == 0)
                                    vTitolo = TitoloBD.loadByIdStallo(v_operazionePrec.idStallo, p_context);
                                else
                                    vTitolo = TitoloBD.loadByCodice(v_operazionePrec.codiceTitolo, p_context);

                                if (vTitolo != null)
                                {
                                    if (vTitolo.scadenza >= DateTime.Now)
                                    {
                                        risp.Stato = v_operazionePrec.stato;
                                        risp.Note = "Scad.: " + vTitolo.scadenza.ToShortTimeString() + " " + vTitolo.scadenza.ToShortDateString();
                                    }
                                    else if (vTitolo.scadenza < DateTime.Now & vTitolo.scadenza > DateTime.Now.Date & vTitolo.scadenza >= p_operazione.data)
                                    {
                                        risp.Stato = OperazioneDTO.statoPagamentoScaduto;

                                        TimeSpan v_adesso = DateTime.Now.TimeOfDay;
                                        TimeSpan v_oraScadenza = vTitolo.scadenza.TimeOfDay;
                                        TimeSpan v_tempoScaduto = v_adesso.Subtract(v_oraScadenza);

                                        risp.Note = "scaduto da: " + Math.Truncate(v_tempoScaduto.TotalMinutes).ToString() + " min.";
                                    }
                                }
                            }

                            if (risp.Stato == "")
                            {
                                TimeSpan v_adesso = DateTime.Now.TimeOfDay;
                                TimeSpan v_oraOperazione = p_operazione.data.TimeOfDay;
                                TimeSpan v_tempoTrascorso = v_adesso.Subtract(v_oraOperazione);

                                risp.Stato = p_operazione.stato;
                                risp.Note = "Emesso da: " + Math.Truncate(v_tempoTrascorso.TotalMinutes).ToString() + " min.";
                            }

                            break;
                        }

                    case OperazioneDTO.statoGiaPreavvisato:
                        {
                            p_operazione = OperazioneBD.loadLastPreavviso(p_operazione.idStallo, p_context);

                            OperazioneDTO v_operazionePrec = OperazioneBD.loadLastBeforeOperazione(p_operazione, p_context);

                            if (v_operazionePrec.stato == OperazioneDTO.statoRegolareConNumero)
                            {
                                TitoloDTO vTitolo;

                                if (v_operazionePrec.codiceTitolo == null || v_operazionePrec.codiceTitolo.Length == 0)
                                    vTitolo = TitoloBD.loadByIdStallo(v_operazionePrec.idStallo, p_context);
                                else
                                    vTitolo = TitoloBD.loadByCodice(v_operazionePrec.codiceTitolo, p_context);

                                if (vTitolo != null)
                                {
                                    if (vTitolo.scadenza >= DateTime.Now)
                                    {
                                        risp.Stato = v_operazionePrec.stato;
                                        risp.Note = "Scad.: " + vTitolo.scadenza.ToShortTimeString() + " " + vTitolo.scadenza.ToShortDateString();
                                    }
                                }
                            }

                            if (risp.Stato == "")
                            {
                                risp = new StatoStalloExt();
                                risp.Targa = p_operazione.targa;
                                risp.Data = p_operazione.data;

                                TimeSpan v_adesso = DateTime.Now.TimeOfDay;
                                TimeSpan v_oraOperazione = p_operazione.data.TimeOfDay;
                                TimeSpan v_tempoTrascorso = v_adesso.Subtract(v_oraOperazione);

                                risp.Stato = p_operazione.stato;
                                risp.Note = "Emesso da: " + Math.Truncate(v_tempoTrascorso.TotalMinutes).ToString() + " min.";
                            }

                            break;
                        }

                    case OperazioneDTO.statoAbusivo:
                    case OperazioneDTO.statoVerbalizzato:
                        {
                            risp.Stato = p_operazione.stato;

                            if (p_operazione.idVerbale > 0)
                            {
                                Verbali vVerbale = VerbaliBD.GetById(p_operazione.idVerbale, p_context);
                                if (vVerbale != null)
                                {
                                    risp.Note = "Verbalizzato";
                                    risp.Targa = vVerbale.targa;
                                }
                            }

                            if (p_operazione.idPenale > 0)
                            {
                                Penali vPenale = PenaliBD.GetById(p_operazione.idPenale, p_context);
                                if (vPenale != null)
                                {
                                    Stalli v_stallo = StalliBD.GetById(p_operazione.idStallo, p_context);
                                    Tariffe v_tariffa = TariffeBD.GetById(v_stallo.idTariffa.Value, p_context);
                                    Int32 v_fasciaAttuale = 0;
                                    Int32 v_fasciaPenale = 0;

                                    if (DateTime.Now.TimeOfDay <= new TimeSpan(14, 0, 0))
                                        v_fasciaAttuale = 1;
                                    else
                                        v_fasciaAttuale = 2;

                                    if (vPenale.data.TimeOfDay <= new TimeSpan(14, 0, 0))
                                        v_fasciaPenale = 1;
                                    else
                                        v_fasciaPenale = 2;

                                    if (v_fasciaAttuale > v_fasciaPenale)
                                    {
                                        // Ripenalizzabile
                                        OperazioneDTO v_ultimaConTitolo = OperazioneBD.loadLastWithTitolo(v_stallo.idStallo,p_context);

                                        if (v_ultimaConTitolo != null)
                                        {
                                            TitoloDTO v_titolo = TitoloBD.loadByCodice(v_ultimaConTitolo.codiceTitolo, p_context);

                                            if (v_titolo != null)
                                            {
                                                risp.Stato = OperazioneDTO.statoPagamentoScaduto;

                                                TimeSpan v_adesso = DateTime.Now.TimeOfDay;
                                                TimeSpan v_oraScadenza = v_titolo.scadenza.TimeOfDay;
                                                TimeSpan v_tempoScaduto = v_adesso.Subtract(v_oraScadenza);

                                                risp.Note = "Pen. scad.: " + Math.Truncate(v_tempoScaduto.TotalMinutes).ToString() + " min.";
                                                risp.Targa = vPenale.targa;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // Già penalizzato
                                        risp.Note = "Penalizzato";
                                        risp.Targa = vPenale.targa;
                                    }
                                }
                            }

                            break;
                        }

                    default:
                        {
                            risp.Stato = p_operazione.stato;
                            break;
                        }
                }
            }
            else
            {
                risp = new StatoStalloExt();
                risp.Targa = "";
                risp.Data = DateTime.Now;
                risp.Stato = OperazioneDTO.statoLibero;
            }

            m_logger.LogMessage(String.Format("getStato --> stato: {0}, targa: {1} ",risp.Stato,risp.Targa), EnLogSeverity.Debug);

            return risp;
        }


        public static IQueryable<Stalli> loadStalliByIdGiro(int pIdGiro, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("loadStalliByIdGiro"), EnLogSeverity.Debug);

            return StalliGiroBD.GetList(p_context).Where(sg => sg.idGiro == pIdGiro).OrderBy(sg => sg.sequenza).Select(sg => sg.Stalli);
        }
        
    }
}
