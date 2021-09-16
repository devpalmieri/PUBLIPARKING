using Publiparking.Data;
using Publiparking.Data.BD;
using Publiparking.Service.Base;
using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Service.SMSParking
{
    public partial class SMSParkingManager : ServiceBaseWithLogging
    {
        List<DBInfos> v_Entilist = new List<DBInfos>();
        //Non fa propagare le eccezioni => Alternativa System.Threading
        System.Timers.Timer m_timer;

        static void Main()
        {
            if (System.Environment.UserInteractive)
            {
                //utilizzato per il debug
                SMSParkingManager v_service = new SMSParkingManager();
                v_service.TestStartupAndStop();
            }
            else
            {
                System.ServiceProcess.ServiceBase.Run(new SMSParkingManager());
            }

        }
        public SMSParkingManager()
        {
            this.ServiceName = "SMSParking";
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
        }

        public override void OnStartTask(string[] args)
        {
            m_timer = new System.Timers.Timer();
            m_timer.Interval = Int32.Parse(ConfigurationManager.AppSettings["minIntervalloEsecuzione"]) * 60000; // 60 seconds
            m_timer.Elapsed += (sender, e) =>
            {
                try
                {
                    OnTimer(sender, e);
                }
                catch (Exception ex)
                {
                    this.logger.LogException("Errore nell'esecuzione del servizio: ", ex as Exception, EnLogSeverity.Fatal);
                    throw;
                }
            };
            m_timer.AutoReset = false;
            m_timer.Start();
            this.logger.LogMessage(String.Format("Timer istanziato [Intervallo:{0} ms]", m_timer.Interval), EnLogSeverity.Debug);
        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            logger.LogMessage(String.Format("Avvio del servizio SMSParking in corso ..."), EnLogSeverity.Info);
            //Carica la lista dei database solo la prima volta. Per ricaricare la lista, fermare e riavviare
            if (v_Entilist.Count() == 0)
            {
                string v_dbServer = ConfigurationManager.AppSettings["dbServer"].ToString();
                string v_dbName = ConfigurationManager.AppSettings["dbName"].ToString();
                string v_dbUserName = ConfigurationManager.AppSettings["dbUserName"].ToString();
                string v_dbPassWord = ConfigurationManager.AppSettings["dbPassWord"].ToString();
                AdoHelper v_helper = new AdoHelper();
                try
                {
                    v_Entilist = v_helper.getDbSettorialiParkInfo(v_dbServer, v_dbName, v_dbUserName, v_dbPassWord);
                }
                catch (Exception exception)
                {
                    logger.LogException("Errore nella lettura della lista degli enti", exception, EnLogSeverity.Error);
                }
            }

            logger.LogMessage(String.Format("numero enti da elaborare {0}", v_Entilist.Count()), EnLogSeverity.Info);
            DbParkCtx ctx = null;
            foreach (DBInfos dbInfo in v_Entilist)
            {
                try
                {
                    logger.LogMessage(String.Format("DBNAME ---> {0}", dbInfo.DbName), EnLogSeverity.Info);
                    ctx = dbInfo.GetParkCtx(false);
                    if (ctx == null)
                    {
                        logger.LogMessage("impossibile connettersi al DB", EnLogSeverity.Error);
                    }
                    else
                    {
                        int v_minutiScadenzaMessaggioOut = Int32.Parse(ConfigurationManager.AppSettings["minutiScadenzaMessaggioOut"].ToString());
                        int v_idModem = Int32.Parse(ConfigurationManager.AppSettings["idModem"].ToString());
                        string v_inviosms = ConfigurationManager.AppSettings["invioSMSAbbonamentoinscadenza"].ToString();
                        if (ConfigurazioneBD.GetList(ctx).FirstOrDefault() == null)
                        {
                            logger.LogMessage(String.Format("Ente non configurato"), EnLogSeverity.Info);
                            continue;
                        }
                        string v_mittenteConfermeRicariche = ConfigurazioneBD.GetList(ctx).FirstOrDefault().numeroMittente;
                        leggiSMSIn(v_minutiScadenzaMessaggioOut, v_idModem, dbInfo, ctx);
                        confermaRicariche(v_mittenteConfermeRicariche, dbInfo, ctx);
                        if (v_inviosms == "true" && DateTime.Now.Hour == 8)
                        {
                            controllaAbbonamentiInscadenza(v_mittenteConfermeRicariche, dbInfo, ctx);
                        }
                        ctx.SaveChanges();

                    }
                }
                catch (Exception exc)
                {
                    logger.LogException("Errore nell'invio sms", exc, EnLogSeverity.Error);
                    ctx.Dispose();
                }
                finally
                {
                    ctx.Dispose();
                }
            }

            logger.LogMessage(String.Format("Fine del servizio SMSParking in corso ..."), EnLogSeverity.Info);
            m_timer.Start();
        }

        #region PRIVATE METHOD

        internal void TestStartupAndStop()
        {

            logger.LogMessage(String.Format("Avvio del servizio SMSParking in corso ..."), EnLogSeverity.Info);
            //Carica la lista dei database solo la prima volta. Per ricaricare la lista, fermare e riavviare
            if (v_Entilist.Count() == 0)
            {
                string v_dbServer = ConfigurationManager.AppSettings["dbServer"].ToString();
                string v_dbName = ConfigurationManager.AppSettings["dbName"].ToString();
                string v_dbUserName = ConfigurationManager.AppSettings["dbUserName"].ToString();
                string v_dbPassWord = ConfigurationManager.AppSettings["dbPassWord"].ToString();
                AdoHelper v_helper = new AdoHelper();
                try
                {
                    v_Entilist = v_helper.getDbSettorialiParkInfo(v_dbServer, v_dbName, v_dbUserName, v_dbPassWord);
                }
                catch (Exception exception)
                {
                    logger.LogException("Errore nella lettura della lista degli enti", exception, EnLogSeverity.Error);
                }
            }

            logger.LogMessage(String.Format("numero enti da elaborare {0}", v_Entilist.Count()), EnLogSeverity.Info);
            DbParkCtx ctx = null;
            foreach (DBInfos dbInfo in v_Entilist.Where(s => s.DbName == "db_park_termoli"))
            {
                try
                {
                    logger.LogMessage(String.Format("DBNAME ---> {0}", dbInfo.DbName), EnLogSeverity.Info);
                    ctx = dbInfo.GetParkCtx();
                    if (ctx == null)
                    {
                        logger.LogMessage("impossibile connettersi al DB", EnLogSeverity.Error);
                    }
                    else
                    {
                        int v_minutiScadenzaMessaggioOut = Int32.Parse(ConfigurationManager.AppSettings["minutiScadenzaMessaggioOut"].ToString());
                        int v_idModem = Int32.Parse(ConfigurationManager.AppSettings["idModem"].ToString());
                        string v_inviosms = ConfigurationManager.AppSettings["invioSMSAbbonamentoinscadenza"].ToString();
                        if (ConfigurazioneBD.GetList(ctx).FirstOrDefault() == null)
                        {
                            logger.LogMessage(String.Format("Ente non configurato"), EnLogSeverity.Info);
                            continue;
                        }
                        string v_mittenteConfermeRicariche = ConfigurazioneBD.GetList(ctx).FirstOrDefault().numeroMittente;
                        leggiSMSIn(v_minutiScadenzaMessaggioOut, v_idModem, dbInfo, ctx);
                        confermaRicariche(v_mittenteConfermeRicariche, dbInfo, ctx);
                        //gli sms vengono inviati alle 8 di mattina
                        //if (v_inviosms == "true" && DateTime.Now.Hour == 9)
                        {
                            controllaAbbonamentiInscadenza(v_mittenteConfermeRicariche, dbInfo, ctx);
                        }
                        ctx.SaveChanges();

                    }
                }
                catch (Exception exc)
                {
                    logger.LogException("Errore nell'invio sms", exc, EnLogSeverity.Error);
                    ctx.Dispose();
                }
                finally
                {
                    ctx.Dispose();
                }
            }

            logger.LogMessage(String.Format("Fine del servizio SMS in corso ..."), EnLogSeverity.Info);

        }

        private void leggiSMSIn(int p_minutiScadenzaMessaggioOut, int p_idModem, DBInfos dBInfo, DbParkCtx v_contextExt)
        {
            logger.LogMessage(String.Format("leggiSMSIn"), EnLogSeverity.Debug);
            IList<int> v_listaSMSInIds;
            try
            {

                v_listaSMSInIds = SMSInBD.GetListSMSNonElaborati(p_minutiScadenzaMessaggioOut, v_contextExt).Select(s => s.idSMSIn).ToList();

                if (v_listaSMSInIds == null || v_listaSMSInIds.Count == 0)
                {
                    logger.LogMessage(String.Format("non vi sono SMS da elaborare"), EnLogSeverity.Debug);
                }
                else
                {
                    logger.LogMessage(String.Format("ci sono {0} SMS da elaborare", v_listaSMSInIds.Count), EnLogSeverity.Info);
                }

                DbParkCtx v_context = null;
                foreach (int v_smsInId in v_listaSMSInIds)
                {
                    using (v_context = dBInfo.GetParkCtx())
                    {
                        try
                        {
                            SMSIn v_smsIn = SMSInBD.GetById(v_smsInId, v_context);
                            SMSInBD.elaboraMessaggio(v_smsIn, p_idModem, v_context);
                            v_context.SaveChanges();

                        }
                        catch (Exception ex)
                        {
                            logger.LogException("Errore in elaboraMessaggio", ex, EnLogSeverity.Error);
                        }
                        finally
                        {
                            v_context.Dispose();
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel metodo leggiSMSIn", ex, EnLogSeverity.Error);
            }

        }
        private void confermaRicariche(string p_mittenteConfermeRicariche, DBInfos dBInfo, DbParkCtx v_contextExt)
        {
            logger.LogMessage(String.Format("confermaRicariche"), EnLogSeverity.Debug);
            IList<Int32> v_listaRicaricheInIds;
            try
            {
                //TODO: da implementare la lettura delle ricariche non confermate da translog
                DateTime v_datariferimento = DateTime.Now.AddDays(-1); //taglio di 5 giorni
                v_listaRicaricheInIds = TranslogBD.getListRicaricheNonConfermate(v_contextExt)
                    .Where(t => t.tlPayDateTime.HasValue && t.tlPayDateTime.Value > v_datariferimento)
                    .Select(r => r.tlRecordID).ToList();

                if (v_listaRicaricheInIds == null || v_listaRicaricheInIds.Count == 0)
                {
                    logger.LogMessage(String.Format("non vi sono ricariche non confermate da elaborare"), EnLogSeverity.Debug);
                }
                else
                {
                    logger.LogMessage(String.Format("ci sono {0} ricariche non confermate da elaborare", v_listaRicaricheInIds.Count), EnLogSeverity.Info);
                }


                DbParkCtx v_context = null;
                foreach (Int64 v_ricaricaId in v_listaRicaricheInIds)
                {
                    using (v_context = dBInfo.GetParkCtx())
                    {
                        try
                        {
                            translog v_ricarica = TranslogBD.GetById(v_ricaricaId, v_context);
                            TranslogBD.confermaRicaricaAbbonamento(v_ricarica, p_mittenteConfermeRicariche, v_context);
                            v_context.SaveChanges();

                        }
                        catch (Exception ex)
                        {
                            logger.LogException("Errore in confermaRicariche", ex, EnLogSeverity.Error);
                        }
                        finally
                        {
                            v_context.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                logger.LogException("Errore nel metodo confermaRicariche", ex, EnLogSeverity.Error);
            }



        }

        private void controllaAbbonamentiInscadenza(string p_numeroMittente, DBInfos dBInfo, DbParkCtx v_contextExt)
        {
            logger.LogMessage(String.Format("controllaAbbonamentiInscadenza"), EnLogSeverity.Debug);
            IList<Int32> v_listaidsAbbonamentiInScadenzaIds;
            IList<Int32> v_listaidsPassInScadenzaIds;
            try
            {
                Configurazione v_config = ConfigurazioneBD.GetList(v_contextExt).FirstOrDefault();
                if (!v_config.msg_scadenza_numgiorni.HasValue)
                {
                    logger.LogMessage(String.Format("invio SMS abbonamenti in scadenza non configurato..."), EnLogSeverity.Debug);                   
                }
                else
                {
                    int p_GiorniTaglio = v_config.msg_scadenza_numgiorni.Value;
                    //TODO: da implementare la lettura delle ricariche non confermate da translog
                    DateTime v_datariferimento = DateTime.Now.AddDays(-p_GiorniTaglio).Date;
                    DateTime v_oggi = DateTime.Now.Date;
                    DateTime v_domani = DateTime.Now.AddDays(1).Date; //utilizzo la mezzanotte del giorno successivo come limite superiore

                    List<int> v_lstidAbbonamentiRinnovati = AbbonamentiRinnoviBD.GetList(v_contextExt).Where(a => a.dataInizio > v_oggi).Select(a => a.idAbbonamento).ToList();

                    v_listaidsAbbonamentiInScadenzaIds = AbbonamentiRinnoviBD.GetList(v_contextExt).Where(a => a.dataFine >= v_datariferimento && a.dataFine <= v_domani)
                                                                                                   .Where(a => !v_lstidAbbonamentiRinnovati.Contains(a.idAbbonamento))
                                                                                                   .Select(r => r.idAbbonamento).ToList();

                    if (v_listaidsAbbonamentiInScadenzaIds == null || v_listaidsAbbonamentiInScadenzaIds.Count == 0)
                    {
                        logger.LogMessage(String.Format("non vi sono rinnovi in scadenza"), EnLogSeverity.Debug);
                    }
                    else
                    {
                        logger.LogMessage(String.Format("ci sono {0} rinnovi in scadenza", v_listaidsAbbonamentiInScadenzaIds.Count), EnLogSeverity.Info);
                    }


                    DbParkCtx v_context = null;
                    foreach (Int64 v_abbonamentoId in v_listaidsAbbonamentiInScadenzaIds)
                    {
                        using (v_context = dBInfo.GetParkCtx(false))
                        {
                            try
                            {
                                AbbonamentiPeriodici v_abbonamento = AbbonamentiPeriodiciBD.GetById(v_abbonamentoId, v_context);
                                if (v_abbonamento == null)
                                {
                                    continue;
                                }
                                string v_numeroCellulare = v_abbonamento.telefono;
                                if (!string.IsNullOrEmpty(v_numeroCellulare))
                                {
                                    string v_numero = v_numeroCellulare.Replace("+", "");

                                    if (v_numero.StartsWith("0") || v_numero.Length < 10)
                                    {
                                        logger.LogMessage(String.Format("numero cellulare non coerente {0}", v_numero), EnLogSeverity.Debug);
                                        continue;
                                    }



                                    DateTime v_datarif = DateTime.Now.Date;
                                    IQueryable<SMSOut> v_smsOutGiaPresente = SMSOutBD.GetList(v_context).Where(s => s.numeroDestinatario.StartsWith(v_numero)).Where(d => d.dataInvio > v_datarif);

                                    if (!v_smsOutGiaPresente.Any())
                                    {
                                        string testoConfig = string.IsNullOrEmpty(v_config.msg_scadenza_rinnovo) ? "Abbonamento in scadenza" : v_config.msg_scadenza_rinnovo;
                                        SMSOut v_SMSOut = new SMSOut();
                                        string v_testo = v_abbonamento.codice + Environment.NewLine + testoConfig;
                                        v_SMSOut.numeroDestinatario = v_numero;
                                        v_SMSOut.numeroMittente = p_numeroMittente;
                                        v_SMSOut.dataElaborazione = DateTime.Now;
                                        v_SMSOut.testo = v_testo;
                                        v_SMSOut.idSMSIn = 1;
                                        v_context.SMSOut.Add(v_SMSOut);
                                    }
                                }
                                v_context.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                logger.LogException("Errore in controllaAbbonamentiInscadenza", ex, EnLogSeverity.Error);
                            }
                            finally
                            {
                                v_context.Dispose();
                            }
                        }
                    }
                }
                //Gestione pass
                if (!v_config.msg_scadenza_pass_numgiorni.HasValue)
                {
                    logger.LogMessage(String.Format("invio SMS pass in scadenza non configurato..."), EnLogSeverity.Debug);                    
                }
                else
                {
                    int p_GiorniTaglioaPass = v_config.msg_scadenza_pass_numgiorni.Value;

                    DateTime v_datariferimentoPass = DateTime.Now.AddDays(-p_GiorniTaglioaPass).Date;
                    DateTime v_domani = DateTime.Now.AddDays(1).Date;

                    //I pass non hanno alcun record in AbbonamentiRinnovi a differenza degli abbonamenti
                    v_listaidsPassInScadenzaIds = AbbonamentiPeriodiciBD.GetList(v_contextExt).Where(a => a.validoAl.HasValue && a.validoAl.Value >= v_datariferimentoPass && a.validoAl.Value <= v_domani)
                                                                                              //.Where(a => !a.AbbonamentiRinnovi.Any())
                                                                                              .Select(r => r.idAbbonamentoPeriodico).ToList();

                    if (v_listaidsPassInScadenzaIds == null || v_listaidsPassInScadenzaIds.Count == 0)
                    {
                        logger.LogMessage(String.Format("non vi sono Pass in scadenza"), EnLogSeverity.Debug);
                    }
                    else
                    {
                        logger.LogMessage(String.Format("ci sono {0} Pass in scadenza", v_listaidsPassInScadenzaIds.Count), EnLogSeverity.Info);
                    }

                    DbParkCtx v_context = null;
                    foreach (Int64 v_passId in v_listaidsPassInScadenzaIds)
                    {
                        using (v_context = dBInfo.GetParkCtx())
                        {
                            try
                            {
                                AbbonamentiPeriodici v_abbonamento = AbbonamentiPeriodiciBD.GetById(v_passId, v_context);
                                if (v_abbonamento == null)
                                {
                                    continue;
                                }
                                string v_numeroCellulare = v_abbonamento.telefono;
                                if (!string.IsNullOrEmpty(v_numeroCellulare))
                                {
                                    string v_numero = v_numeroCellulare.Replace("+", "");

                                    if (v_numero.StartsWith("0") || v_numero.Length < 10)
                                    {
                                        logger.LogMessage(String.Format("numero cellulare non coerente {0}", v_numero), EnLogSeverity.Debug);
                                        continue;
                                    }

                                    DateTime v_datarif = DateTime.Now.Date;
                                    IQueryable<SMSOut> v_smsOutGiaPresente = SMSOutBD.GetList(v_context).Where(s => s.numeroDestinatario.StartsWith(v_numero)).Where(d => d.dataInvio > v_datarif);
                                    //nel caso già presente includiamo sia i pass che gli abbonamenti
                                    if (!v_smsOutGiaPresente.Any())
                                    {
                                        string testoConfig = string.IsNullOrEmpty(v_config.msg_scadenza_pass) ? "Pass in scadenza" : v_config.msg_scadenza_pass;
                                        SMSOut v_SMSOut = new SMSOut();
                                        string v_testo = v_abbonamento.codice + Environment.NewLine + testoConfig;
                                        v_SMSOut.numeroDestinatario = v_numero;
                                        v_SMSOut.numeroMittente = p_numeroMittente;
                                        v_SMSOut.dataElaborazione = DateTime.Now;
                                        v_SMSOut.testo = v_testo;
                                        v_SMSOut.idSMSIn = 1;
                                        v_context.SMSOut.Add(v_SMSOut);
                                    }
                                }
                                v_context.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                logger.LogException("Errore in controllaAbbonamentiInscadenza", ex, EnLogSeverity.Error);
                            }
                            finally
                            {
                                v_context.Dispose();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel metodo controllaAbbonamentiInscadenza", ex, EnLogSeverity.Error);
            }
        }


        private void controllaAbbonamentiScaduti(string p_numeroMittente, DBInfos dBInfo, DbParkCtx v_contextExt)
        {
            logger.LogMessage(String.Format("controllaAbbonamentiInscadenza"), EnLogSeverity.Debug);
            IList<Int32> v_listaidsAbbonamentiInScadenzaIds;
            IList<Int32> v_listaidsPassInScadenzaIds;
            try
            {
                Configurazione v_config = ConfigurazioneBD.GetList(v_contextExt).FirstOrDefault();
                if (!v_config.msg_scadenza_numgiorni.HasValue)
                {
                    logger.LogMessage(String.Format("invio SMS abbonamenti in scadenza non configurato..."), EnLogSeverity.Debug);
                }
                else
                {
                    int p_GiorniTaglio = v_config.msg_scadenza_numgiorni.Value;
                    //TODO: da implementare la lettura delle ricariche non confermate da translog
                    //DateTime v_datariferimento = DateTime.Now.AddDays(p_GiorniTaglio).Date;
                    //DateTime v_oggi = DateTime.Now.Date;
                    //DateTime v_domani = v_datariferimento.AddDays(1).Date; //utilizzo la mezzanotte del giorno successivo come limite superiore



                    //List<int> v_lstidAbbonamentiRinnovati = AbbonamentiRinnoviBD.GetList(v_contextExt).Where(a => a.dataInizio > v_oggi).Select(a => a.idAbbonamento).ToList();

                    //v_listaidsAbbonamentiInScadenzaIds = AbbonamentiRinnoviBD.GetList(v_contextExt).Where(a => a.dataFine >= v_datariferimento && a.dataFine <= v_domani)
                    //                                                                               .Where(a => !v_lstidAbbonamentiRinnovati.Contains(a.idAbbonamento))
                    //                                                                               .Select(r => r.idAbbonamento).ToList();

                    DateTime v_oggi = DateTime.Now.Date;
                    DateTime v_datariferimento = v_oggi.AddDays(-p_GiorniTaglio);
                                       
                    List<int> v_lstidAbbonamentiRinnovati = AbbonamentiRinnoviBD.GetList(v_contextExt).Where(a => a.dataInizio > v_oggi).Select(a => a.idAbbonamento).ToList();

                    v_listaidsAbbonamentiInScadenzaIds = AbbonamentiRinnoviBD.GetList(v_contextExt).Where(a => a.dataFine >= v_datariferimento && a.dataFine <= v_oggi)
                                                                                                   .Where(a => !v_lstidAbbonamentiRinnovati.Contains(a.idAbbonamento))
                                                                                                   .Select(r => r.idAbbonamento).ToList();


                    if (v_listaidsAbbonamentiInScadenzaIds == null || v_listaidsAbbonamentiInScadenzaIds.Count == 0)
                    {
                        logger.LogMessage(String.Format("non vi sono rinnovi in scadenza"), EnLogSeverity.Debug);
                    }
                    else
                    {
                        logger.LogMessage(String.Format("ci sono {0} rinnovi in scadenza", v_listaidsAbbonamentiInScadenzaIds.Count), EnLogSeverity.Info);
                    }


                    DbParkCtx v_context = null;
                    foreach (Int64 v_abbonamentoId in v_listaidsAbbonamentiInScadenzaIds)
                    {
                        using (v_context = dBInfo.GetParkCtx(false))
                        {
                            try
                            {
                                AbbonamentiPeriodici v_abbonamento = AbbonamentiPeriodiciBD.GetById(v_abbonamentoId, v_context);
                                if (v_abbonamento == null)
                                {
                                    continue;
                                }
                                string v_numeroCellulare = v_abbonamento.telefono;
                                if (!string.IsNullOrEmpty(v_numeroCellulare))
                                {
                                    string v_numero = v_numeroCellulare.Replace("+", "");

                                    if (v_numero.StartsWith("0") || v_numero.Length < 10)
                                    {
                                        logger.LogMessage(String.Format("numero cellulare non coerente {0}", v_numero), EnLogSeverity.Debug);
                                        continue;
                                    }



                                    DateTime v_datarif = DateTime.Now.Date;
                                    IQueryable<SMSOut> v_smsOutGiaPresente = SMSOutBD.GetList(v_context).Where(s => s.numeroDestinatario.StartsWith(v_numero)).Where(d => d.dataInvio > v_datarif);

                                    if (!v_smsOutGiaPresente.Any())
                                    {
                                        string testoConfig = string.IsNullOrEmpty(v_config.msg_scadenza_rinnovo) ? "Abbonamento in scadenza" : v_config.msg_scadenza_rinnovo;
                                        SMSOut v_SMSOut = new SMSOut();
                                        string v_testo = v_abbonamento.codice + Environment.NewLine + testoConfig;
                                        v_SMSOut.numeroDestinatario = v_numero;
                                        v_SMSOut.numeroMittente = p_numeroMittente;
                                        v_SMSOut.dataElaborazione = DateTime.Now;
                                        v_SMSOut.testo = v_testo;
                                        v_SMSOut.idSMSIn = 1;
                                        v_context.SMSOut.Add(v_SMSOut);
                                    }
                                }
                                v_context.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                logger.LogException("Errore in controllaAbbonamentiInscadenza", ex, EnLogSeverity.Error);
                            }
                            finally
                            {
                                v_context.Dispose();
                            }
                        }
                    }
                }
                //Gestione pass
                if (!v_config.msg_scadenza_pass_numgiorni.HasValue)
                {
                    logger.LogMessage(String.Format("invio SMS pass in scadenza non configurato..."), EnLogSeverity.Debug);
                }
                else
                {
                    int p_GiorniTaglioaPass = v_config.msg_scadenza_pass_numgiorni.Value;

                    //DateTime v_datariferimentoPass = DateTime.Now.AddDays(-p_GiorniTaglioaPass).Date;
                    //DateTime v_domani = DateTime.Now.AddDays(1).Date;

                    ////I pass non hanno alcun record in AbbonamentiRinnovi a differenza degli abbonamenti
                    //v_listaidsPassInScadenzaIds = AbbonamentiPeriodiciBD.GetList(v_contextExt).Where(a => a.validoAl.HasValue && a.validoAl.Value >= v_datariferimentoPass && a.validoAl.Value <= v_domani)
                    //                                                                          //.Where(a => !a.AbbonamentiRinnovi.Any())
                    //                                                                          .Select(r => r.idAbbonamentoPeriodico).ToList();

                    DateTime v_oggi = DateTime.Now.Date;
                    DateTime v_datariferimento = v_oggi.AddDays(-p_GiorniTaglioaPass);

                    v_listaidsPassInScadenzaIds = AbbonamentiPeriodiciBD.GetList(v_contextExt).Where(a => a.validoAl.HasValue && a.validoAl.Value >= v_datariferimento && a.validoAl.Value <= v_oggi)
                                                                                              .Select(r => r.idAbbonamentoPeriodico).ToList();

                    if (v_listaidsPassInScadenzaIds == null || v_listaidsPassInScadenzaIds.Count == 0)
                    {
                        logger.LogMessage(String.Format("non vi sono Pass in scadenza"), EnLogSeverity.Debug);
                    }
                    else
                    {
                        logger.LogMessage(String.Format("ci sono {0} Pass in scadenza", v_listaidsPassInScadenzaIds.Count), EnLogSeverity.Info);
                    }

                    DbParkCtx v_context = null;
                    foreach (Int64 v_passId in v_listaidsPassInScadenzaIds)
                    {
                        using (v_context = dBInfo.GetParkCtx())
                        {
                            try
                            {
                                AbbonamentiPeriodici v_abbonamento = AbbonamentiPeriodiciBD.GetById(v_passId, v_context);
                                if (v_abbonamento == null)
                                {
                                    continue;
                                }
                                string v_numeroCellulare = v_abbonamento.telefono;
                                if (!string.IsNullOrEmpty(v_numeroCellulare))
                                {
                                    string v_numero = v_numeroCellulare.Replace("+", "");

                                    if (v_numero.StartsWith("0") || v_numero.Length < 10)
                                    {
                                        logger.LogMessage(String.Format("numero cellulare non coerente {0}", v_numero), EnLogSeverity.Debug);
                                        continue;
                                    }

                                    DateTime v_datarif = DateTime.Now.Date;
                                    IQueryable<SMSOut> v_smsOutGiaPresente = SMSOutBD.GetList(v_context).Where(s => s.numeroDestinatario.StartsWith(v_numero)).Where(d => d.dataInvio > v_datarif);
                                    //nel caso già presente includiamo sia i pass che gli abbonamenti
                                    if (!v_smsOutGiaPresente.Any())
                                    {
                                        string testoConfig = string.IsNullOrEmpty(v_config.msg_scadenza_pass) ? "Pass in scadenza" : v_config.msg_scadenza_pass;
                                        SMSOut v_SMSOut = new SMSOut();
                                        string v_testo = v_abbonamento.codice + Environment.NewLine + testoConfig;
                                        v_SMSOut.numeroDestinatario = v_numero;
                                        v_SMSOut.numeroMittente = p_numeroMittente;
                                        v_SMSOut.dataElaborazione = DateTime.Now;
                                        v_SMSOut.testo = v_testo;
                                        v_SMSOut.idSMSIn = 1;
                                        v_context.SMSOut.Add(v_SMSOut);
                                    }
                                }
                                v_context.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                logger.LogException("Errore in controllaAbbonamentiInscadenza", ex, EnLogSeverity.Error);
                            }
                            finally
                            {
                                v_context.Dispose();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel metodo controllaAbbonamentiInscadenza", ex, EnLogSeverity.Error);
            }
        }



        #endregion
    }
}
