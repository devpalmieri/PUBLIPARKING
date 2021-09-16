using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Publiparking.Data.BD
{
    public class SMSInBD : EntityBD<SMSIn>
    {

        private static ILogger m_logger = LoggerFactory.getInstance().getLogger<NLogger>("SMSInBD");

        public enum tipo
        {
            Sconosciuto = 0,
            Attivazione = 10,
            Titolo = 20,
            AggiungiCellulare = 30,
            RimuoviCellulare = 40,
            CambiaMaster = 50,
            SostaATempo = 60,
            Saldo = 70
        }

        public static tipo getTipo(SMSIn p_SMS)
        {
            tipo risp = tipo.Sconosciuto;
            string v_testo = p_SMS.testo;

            // Attivazione
            if ((v_testo.Length == 18 && v_testo.Substring(0, 6).ToUpper() == "ATTIVA") || (v_testo.Length == 19 && v_testo.Substring(0, 7).ToUpper() == "ATTIVA "))
                risp = tipo.Attivazione;
            else if ((v_testo.Trim().ToUpper() == "ATTIVA"))
                risp = tipo.Attivazione;
            else if ((v_testo.Length == 18 && v_testo.Substring(0, 8).ToUpper() == "AGGIUNGI") || (v_testo.Length == 19 && v_testo.Substring(0, 9).ToUpper() == "AGGIUNGI "))
                risp = tipo.AggiungiCellulare;
            else if ((v_testo.Length == 17 && v_testo.Substring(0, 7).ToUpper() == "RIMUOVI") || (v_testo.Length == 18 && v_testo.Substring(0, 8).ToUpper() == "RIMUOVI "))
                risp = tipo.RimuoviCellulare;
            else if ((v_testo.Length == 20 && v_testo.Substring(0, 10).ToUpper() == "PRINCIPALE") || (v_testo.Length == 21 && v_testo.Substring(0, 11).ToUpper() == "PRINCIPALE "))
                risp = tipo.CambiaMaster;
            else if (v_testo.ToUpper() == "SALDO" || v_testo.ToUpper() == "SAL")
                risp = tipo.Saldo;
            else if (v_testo.Length <= 8)
            {
                Int32 v_separatore = 0;

                if (v_testo.Contains(" ") | v_testo.Contains("a") || v_testo.Contains("A"))
                    risp = tipo.SostaATempo;
            }

            return risp;
        }

        public SMSInBD()
        {

        }


        public static bool SaveSMSInFromWebService(SMSIn p_SMSIn, DbParkCtx p_context)
        {
            bool retval = false;
            p_context.SMSIn.Add(p_SMSIn);
            int res = p_context.SaveChanges();
            if (res > 0)
            {
                retval = true;
            }

            return retval;
        }

        public static IQueryable<SMSIn> GetList(DbParkCtx p_context)
        {
            Configurazione v_conf = ConfigurazioneBD.GetList(p_context).FirstOrDefault();
            if (!string.IsNullOrEmpty(v_conf.numeroMittente))
            {
                return GetListInternal(p_context).Where(s => s.numeroDestinatario.Contains(v_conf.numeroMittente));
            }
            else
            {
                return GetListInternal(p_context);
            }

            
        }

        public static IQueryable<SMSIn> GetListSMSNonElaborati(int p_minuti, DbParkCtx p_context)
        {

            if (p_minuti > 0)
            {
                DateTime v_datariferimento = DateTime.Now.AddMinutes(-p_minuti);
                return GetList(p_context).Where(s => !s.dataElaborazione.HasValue)
                                         .Where(s => s.dataRicezione.HasValue && s.dataRicezione.Value > v_datariferimento);
            }
            else
            {
                return GetList(p_context).Where(s => !s.dataElaborazione.HasValue);
            }

        }
        public static int elaboraMessaggio(SMSIn p_SMSIn, int p_idModem, DbParkCtx p_context)
        {
            Int32 risp = -1;
            tipo v_tipo = SMSInBD.getTipo(p_SMSIn);
            string v_testo = p_SMSIn.testo;
            m_logger.LogMessage(String.Format("elaboraMessaggio ---> id {0} --- testo {1}", p_SMSIn.idSMSIn, v_testo), EnLogSeverity.Info);
            switch (v_tipo)
            {
                case tipo.Sconosciuto:
                    {
                        // Formato sconosciuto
                        p_SMSIn.notaElaborazione = "Formato non valido";
                        p_SMSIn.dataElaborazione = DateTime.Now;
                        risp = 1;
                        break;
                    }

                case tipo.Attivazione:
                    {
                        risp = attivaAbbonamento(p_SMSIn, p_context);
                        break;
                    }

                case tipo.AggiungiCellulare:
                    {
                        risp = aggiungiCellulare(p_SMSIn, p_context);
                        break;
                    }

                case tipo.RimuoviCellulare:
                    {
                        risp = rimuoviCellulare(p_SMSIn, p_context);
                        break;
                    }

                case tipo.CambiaMaster:
                    {
                        risp = cambiaMaster(p_SMSIn, p_context);
                        break;
                    }

                case tipo.SostaATempo:
                    {
                        risp = pagaConTempo(p_SMSIn, p_idModem, p_context);
                        break;
                    }

                case tipo.Saldo:
                    {
                        risp = getSaldo(p_SMSIn, p_context);
                        break;
                    }
                default:
                    {
                        // Formato sconosciuto
                        p_SMSIn.notaElaborazione = "Formato non valido";
                        p_SMSIn.dataElaborazione = DateTime.Now;
                        risp = 1;
                        break;
                    }

            }

            return risp;
        }

        private static Int32 attivaAbbonamento(SMSIn p_SMSIn, DbParkCtx p_context)
        {
            // Attiva un abbonamento e collega come master il cellulare che ha inviato l'SMS
            Int32 risp = -1;
            string v_testo = p_SMSIn.testo;
            
            //10-11-2020 inserita la creazione dell'abbonamento
            Abbonamenti v_abbonamento;
            if (v_testo.Trim().ToUpper() == "ATTIVA")
            {
                //crea abbonamento
                v_abbonamento = new Abbonamenti();
                v_abbonamento.codice = AbbonamentiBD.generaCodice(p_context);
                //v_abbonamento.dataAbilitazione = DateTime.Now;
                v_abbonamento.dataStampa = DateTime.Now;
                p_context.Abbonamenti.Add(v_abbonamento);
                p_context.SaveChanges();
            }
            else
            {
                //10-11-2020 prima veniva preso il primo abbonamento libero
                string v_codiceAbbonamento = v_testo.Trim().Substring(6);
                v_abbonamento = AbbonamentiBD.getAbbonamentoByCodice(v_codiceAbbonamento, p_context);                    
            }                     

            if (v_abbonamento != null && v_abbonamento.dataStampa.HasValue)
            {
                string v_codiceAbbonamento = v_abbonamento.codice;

                // Verifica se l'abbonamento è già attivo
                if (!v_abbonamento.dataAbilitazione.HasValue)
                {
                    Cellulari v_cellulare = CellulariBD.getCellulareByNumero(p_SMSIn.numeroMittente, p_context);

                    // Verifica se il cellulare è già utilizzato su un'altro abbonamento
                    if (v_cellulare == null)
                    {
                        v_cellulare = CellulariBD.creaCellulare(v_abbonamento.idAbbonamento, p_SMSIn.numeroMittente, true, p_context);

                        if (v_cellulare != null)
                        {
                            if (AbbonamentiBD.attivaAbbonamento(v_abbonamento.idAbbonamento, p_context))
                            {
                                SMSOut v_smsRisp = new SMSOut();
                                v_smsRisp.idSMSIn = p_SMSIn.idSMSIn;
                                v_smsRisp.numeroDestinatario = p_SMSIn.numeroMittente;
                                v_smsRisp.numeroMittente = p_SMSIn.numeroDestinatario;
                                v_smsRisp.dataElaborazione = DateTime.Now;
                                //v_smsRisp.testo = v_codiceAbbonamento + Environment.NewLine + "Abbonamento attivato.";
                                string v_testorisp = string.Format("Servizio SMS {0} Attivo; ricarica il credito da parcometro-tasto giallo-servizi-servizio SMS-inserisci codice 12 cifre.",v_codiceAbbonamento);
                                v_smsRisp.testo = v_testorisp;
                                p_SMSIn.notaElaborazione = "Abbonamento attivo";
                                p_SMSIn.dataElaborazione = DateTime.Now;
                                p_context.SMSOut.Add(v_smsRisp);

                                SMSOut v_smsRisp2 = new SMSOut();
                                v_smsRisp2.idSMSIn = p_SMSIn.idSMSIn;
                                v_smsRisp2.numeroDestinatario = p_SMSIn.numeroMittente;
                                v_smsRisp2.numeroMittente = p_SMSIn.numeroDestinatario;
                                v_smsRisp2.dataElaborazione = DateTime.Now;
                                string v_testorisp2 = string.Format("Servizio SMS {0} Attivo; per attivare una sosta inviare SMS con \"stallo\", uno spazio, \"minuti di sosta\"; per conoscere il saldo inviare SMS \"SALDO\" .", v_codiceAbbonamento);
                                v_smsRisp.testo = v_testorisp2;                                
                                p_context.SMSOut.Add(v_smsRisp2);
                                risp = 1;
                            }
                        }

                    }
                    else
                    {
                        // Segnala che il cellulare è già collegato ad un'altro abbonamento
                        SMSOut v_smsRisp = new SMSOut();

                        v_smsRisp.idSMSIn = p_SMSIn.idSMSIn;
                        v_smsRisp.numeroDestinatario = p_SMSIn.numeroMittente;
                        v_smsRisp.numeroMittente = p_SMSIn.numeroDestinatario;
                        v_smsRisp.dataElaborazione = DateTime.Now;
                        v_smsRisp.testo = v_cellulare.Abbonamenti.codice + Environment.NewLine + "Cellulare gia' collegato ad altro abbonamento.";

                        p_SMSIn.notaElaborazione = "Cellulare gia' collegato ad altro abbonamento";
                        p_SMSIn.dataElaborazione = DateTime.Now;
                        p_context.SMSOut.Add(v_smsRisp);
                        risp = 1;
                    }
                }
                else
                {
                    // Segnala che l'abbonamento è già attivo
                    SMSOut v_smsRisp = new SMSOut();

                    v_smsRisp.idSMSIn = p_SMSIn.idSMSIn;
                    v_smsRisp.numeroDestinatario = p_SMSIn.numeroMittente;
                    v_smsRisp.numeroMittente = p_SMSIn.numeroDestinatario;
                    v_smsRisp.dataElaborazione = DateTime.Now;
                    v_smsRisp.testo =  "Abbonamento gia' attivo.";
                    p_context.SMSOut.Add(v_smsRisp);
                    risp = 1;
                    p_SMSIn.notaElaborazione = "Abbonamento già attivo";
                    p_SMSIn.dataElaborazione = DateTime.Now;
                    risp = 1;
                }
            }
            else
            {
                p_SMSIn.notaElaborazione = "Abbonamento inesistente";
                p_SMSIn.dataElaborazione = DateTime.Now;
                risp = 1;
            }

            return risp;
        }
        private static Int32 aggiungiCellulare(SMSIn p_SMSIn, DbParkCtx p_context)
        {
            // Collega un nuovo cellulare all'abbonamento del master che ha inviato l'SMS
            Int32 risp = -1;
            Cellulari v_master = CellulariBD.getCellulareByNumero(p_SMSIn.numeroMittente, p_context);

            if (v_master != null && v_master.master && v_master.dataCessazione == null)
            {
                Abbonamenti v_abbonamento = AbbonamentiBD.GetById(v_master.idAbbonamento, p_context);            

                if (v_abbonamento != null)
                {
                    string v_testo = p_SMSIn.testo;
                    string v_nuovoNumero = "+39" + v_testo.Substring(v_testo.Length - 10);   //"+39" + Right(p_SMSIn.testo, 10);
                    Cellulari v_cellulare = CellulariBD.getCellulareByNumero(v_nuovoNumero, p_context);

                    // Verifica se il cellulare è già utilizzato su un'altro abbonamento
                    if (v_cellulare == null)
                    {
                        string v_regExpCellulare = @"(\+)(3)(9)(\d)(\d)(\d)(\d)(\d)(\d)(\d)(\d)(\d)(\d)";
                        Regex v_regExp = new Regex(v_regExpCellulare, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        Match v_match = v_regExp.Match(v_nuovoNumero);

                        if (v_match.Success)
                        {
                            v_cellulare = CellulariBD.creaCellulare(v_abbonamento.idAbbonamento, v_nuovoNumero, false, p_context);
                            if (v_cellulare != null)
                            {
                                SMSOut v_smsRisp = new SMSOut();

                                v_smsRisp.idSMSIn = p_SMSIn.idSMSIn;
                                v_smsRisp.numeroDestinatario = v_nuovoNumero;
                                v_smsRisp.numeroMittente = p_SMSIn.numeroDestinatario;
                                v_smsRisp.dataElaborazione = DateTime.Now;
                                v_smsRisp.testo = v_abbonamento.codice + Environment.NewLine + "Nuovo numero aggiunto.";

                                p_SMSIn.notaElaborazione = "Nuovo numero aggiunto";
                                p_SMSIn.dataElaborazione = DateTime.Now;
                                p_context.SMSOut.Add(v_smsRisp);
                                risp = 1;
                            }
                        
                        }
                        else
                        {
                            // Segnala che il numero di cellulare non è valido
                            SMSOut v_smsRisp = new SMSOut();

                            v_smsRisp.idSMSIn = p_SMSIn.idSMSIn;
                            v_smsRisp.numeroDestinatario = p_SMSIn.numeroMittente;
                            v_smsRisp.numeroMittente = p_SMSIn.numeroDestinatario;
                            v_smsRisp.dataElaborazione = DateTime.Now;
                            v_smsRisp.testo = "Numero Cellulare non valido.";
                            p_context.SMSOut.Add(v_smsRisp);
                            p_SMSIn.notaElaborazione = "Numero Cellulare non valido";
                            p_SMSIn.dataElaborazione = DateTime.Now;
                            risp = 1;
                        }
                    }
                    else
                    {
                        // Segnala che il cellulare è già collegato ad un'altro abbonamento
                        SMSOut v_smsRisp = new SMSOut();

                        v_smsRisp.idSMSIn = p_SMSIn.idSMSIn;
                        v_smsRisp.numeroDestinatario = p_SMSIn.numeroMittente;
                        v_smsRisp.numeroMittente = p_SMSIn.numeroDestinatario;
                        v_smsRisp.dataElaborazione = DateTime.Now;
                        v_smsRisp.testo = v_abbonamento.codice + Environment.NewLine + "Cellulare gia' collegato ad un abbonamento.";
                        p_context.SMSOut.Add(v_smsRisp);
                        p_SMSIn.notaElaborazione = "Cellulare gia' collegato ad un abbonamento";
                        p_SMSIn.dataElaborazione = DateTime.Now;
                        risp = 1;
                    }
                }
                else
                {
                    p_SMSIn.notaElaborazione = "Abbonamento inesistente";
                    p_SMSIn.dataElaborazione = DateTime.Now;
                    risp = 1;
                }
            }
            else
            {
                p_SMSIn.notaElaborazione = "Cellulare non attivo o non master per l'abbonamento";
                p_SMSIn.dataElaborazione = DateTime.Now;
                risp = 1;
            }

            return risp;
        }

        private static Int32 rimuoviCellulare(SMSIn p_SMSIn, DbParkCtx p_context)
        {
            Int32 risp = -1;
            // Rimuove un cellulare da un abbonamento
            Cellulari v_master = CellulariBD.getCellulareByNumero(p_SMSIn.numeroMittente, p_context);

            if (v_master != null && v_master.master == true && v_master.dataCessazione == null)
            {
                Abbonamenti v_abbonamento = AbbonamentiBD.GetById(v_master.idAbbonamento, p_context);

                if (v_abbonamento != null)
                {
                    string v_testo = p_SMSIn.testo;
                    string v_numeroDaRimuovere = "+39" + v_testo.Substring(v_testo.Length - 10); ;//"+39" + Right(p_SMSIn.testo, 10);
                    Cellulari v_cellulare = CellulariBD.getCellulareByNumero(v_numeroDaRimuovere, p_context);

                    // Verifica se il cellulare esiste ed è attivo
                    if (v_cellulare != null && v_cellulare.dataCessazione == null && v_cellulare.idAbbonamento == v_abbonamento.idAbbonamento)
                    {
                        if (v_cellulare.master)
                        {
                            // Impossibile rimuovere il cellulare master
                            SMSOut v_smsRisp = new SMSOut();

                            v_smsRisp.idSMSIn = p_SMSIn.idSMSIn;
                            v_smsRisp.numeroDestinatario = p_SMSIn.numeroMittente;
                            v_smsRisp.numeroMittente = p_SMSIn.numeroDestinatario;
                            v_smsRisp.dataElaborazione = DateTime.Now;
                            v_smsRisp.testo = v_abbonamento.codice + Environment.NewLine + "Non e' possibile rimuovere il cellulare principale dall'abbonamento.";
                            p_context.SMSOut.Add(v_smsRisp);
                            p_SMSIn.notaElaborazione = "Non e' possibile rimuovere il cellulare principale dall'abbonamento";
                            p_SMSIn.dataElaborazione = DateTime.Now;
                            risp = 1;
                        }
                        else if (CellulariBD.disattivaCellulare(v_cellulare.idCellulare, p_context))
                        {
                            SMSOut v_smsRisp = new SMSOut();

                            v_smsRisp.idSMSIn = p_SMSIn.idSMSIn;
                            v_smsRisp.numeroDestinatario = v_numeroDaRimuovere;
                            v_smsRisp.numeroMittente = p_SMSIn.numeroDestinatario;
                            v_smsRisp.dataElaborazione = DateTime.Now;
                            v_smsRisp.testo = v_abbonamento.codice + Environment.NewLine + "Il suo cellulare è stato rimosso dall'abbonamento N. " + v_abbonamento.codice.ToString();
                            p_context.SMSOut.Add(v_smsRisp);
                            p_SMSIn.notaElaborazione = "Cellulare rimosso dall'abbonamento N. " + v_abbonamento.codice.ToString();
                            p_SMSIn.dataElaborazione = DateTime.Now;

                            risp = 1;
                        }
                    }
                    else
                    {
                        // Segnala che il numero di cellulare non è valido
                        SMSOut v_smsRisp = new SMSOut();

                        v_smsRisp.idSMSIn = p_SMSIn.idSMSIn;
                        v_smsRisp.numeroDestinatario = p_SMSIn.numeroMittente;
                        v_smsRisp.numeroMittente = p_SMSIn.numeroDestinatario;
                        v_smsRisp.dataElaborazione = DateTime.Now;
                        v_smsRisp.testo = v_abbonamento.codice + Environment.NewLine + "Cellulare non attivo sull'abbonamento.";
                        p_context.SMSOut.Add(v_smsRisp);
                        p_SMSIn.notaElaborazione = "Cellulare non attivo sull'abbonamento";
                        p_SMSIn.dataElaborazione = DateTime.Now;
                        risp = 1;
                    }
                }
                else
                {
                    p_SMSIn.notaElaborazione = "Abbonamento inesistente";
                    p_SMSIn.dataElaborazione = DateTime.Now;
                    risp = 1;
                }
            }
            else
            {
                p_SMSIn.notaElaborazione = "Cellulare non attivo o non master per l'abbonamento";
                p_SMSIn.dataElaborazione = DateTime.Now;
                risp = 1;
            }

            return risp;
        }
        private static Int32 cambiaMaster(SMSIn p_SMSIn, DbParkCtx p_context)
        {
            // Cambia il cellulare Master
            Int32 Risp = -1;
            Cellulari v_master = CellulariBD.getCellulareByNumero(p_SMSIn.numeroMittente, p_context);

            if (v_master != null && v_master.master && v_master.dataCessazione == null)
            {
                Abbonamenti v_abbonamento = AbbonamentiBD.GetById(v_master.idAbbonamento, p_context);

                if (v_abbonamento != null)
                {

                    string v_testo = p_SMSIn.testo;
                    string v_nuovoMaster = "+39" + v_testo.Substring(v_testo.Length - 10);
                    Cellulari v_cellulare = CellulariBD.getCellulareByNumero(v_nuovoMaster, p_context);

                    string v_regExpCellulare = @"(\+)(3)(9)(\d)(\d)(\d)(\d)(\d)(\d)(\d)(\d)(\d)(\d)";
                    Regex v_regExp = new Regex(v_regExpCellulare, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    Match v_match = v_regExp.Match(v_nuovoMaster);

                    if (v_match.Success)
                    {
                        if (v_cellulare != null && v_cellulare.dataCessazione == null && v_cellulare.idAbbonamento != v_abbonamento.idAbbonamento)
                        {
                            // Segnala che il cellulare è già collegato ad un'altro abbonamento
                            SMSOut v_smsRisp = new SMSOut();

                            v_smsRisp.idSMSIn = p_SMSIn.idSMSIn;
                            v_smsRisp.numeroDestinatario = p_SMSIn.numeroMittente;
                            v_smsRisp.numeroMittente = p_SMSIn.numeroDestinatario;
                            v_smsRisp.dataElaborazione = DateTime.Now;
                            v_smsRisp.testo = v_abbonamento.codice + Environment.NewLine + "Cellulare collegato ad altro abbonamento.";
                            p_context.SMSOut.Add(v_smsRisp);
                            p_SMSIn.notaElaborazione = "Cellulare collegato ad altro abbonamento";
                            p_SMSIn.dataElaborazione = DateTime.Now;
                            Risp = 1;
                        }
                        else
                        {
                            v_cellulare = CellulariBD.creaCellulare(v_abbonamento.idAbbonamento, v_nuovoMaster, true, p_context);
                            if (v_cellulare != null)
                            {
                                SMSOut v_smsRisp = new SMSOut();

                                v_smsRisp.idSMSIn = p_SMSIn.idSMSIn;
                                v_smsRisp.numeroDestinatario = v_nuovoMaster;
                                v_smsRisp.numeroMittente = p_SMSIn.numeroDestinatario;
                                v_smsRisp.dataElaborazione = DateTime.Now;
                                v_smsRisp.testo = v_abbonamento.codice + Environment.NewLine + "Da ora, questo e' il numero principale per l'abbonamento N. " + v_abbonamento.codice;
                                p_context.SMSOut.Add(v_smsRisp);
                                p_SMSIn.notaElaborazione = "Cambio Master effettuato";
                                p_SMSIn.dataElaborazione = DateTime.Now;
                                Risp = 1;
                            }

                        }
                    }
                    else
                    {
                        // Segnala che il numero di cellulare non è valido
                        SMSOut v_smsRisp = new SMSOut();

                        v_smsRisp.idSMSIn = p_SMSIn.idSMSIn;
                        v_smsRisp.numeroDestinatario = p_SMSIn.numeroMittente;
                        v_smsRisp.numeroMittente = p_SMSIn.numeroDestinatario;
                        v_smsRisp.dataElaborazione = DateTime.Now;
                        v_smsRisp.testo = v_abbonamento.codice + Environment.NewLine + "Numero Cellulare non valido.";
                        p_context.SMSOut.Add(v_smsRisp);
                        p_SMSIn.notaElaborazione = "Numero Cellulare non valido";
                        p_SMSIn.dataElaborazione = DateTime.Now;
                    }
                }
                else
                {
                    p_SMSIn.notaElaborazione = "Abbonamento inesistente";
                    p_SMSIn.dataElaborazione = DateTime.Now;
                }
            }
            else
            {
                p_SMSIn.notaElaborazione = "Cellulare non attivo o non master per l'abbonamento";
                p_SMSIn.dataElaborazione = DateTime.Now;
            }

            return Risp;
        }
       
        private static Int32 pagaConTempo(SMSIn p_SMSIn, int p_idModem, DbParkCtx p_context)
        {
            Int32 risp = -1;
            Cellulari v_cellulare = CellulariBD.getCellulareByNumero(p_SMSIn.numeroMittente, p_context);
            SMSOut v_SMSOutErrore = new SMSOut();
            string v_testoSMSIn = p_SMSIn.testo;
            Int32 v_numeroStallo = SMSInBD.getNumeroStallo(v_testoSMSIn);
            Int32 v_minutiPagati = SMSInBD.getMinutiRinnovo(v_testoSMSIn);


            if (v_numeroStallo == 0)
            {
                // Verifica che sia inserito il numero dello stallo
                p_SMSIn.notaElaborazione = "Non è stato indicato correttamente lo stallo";
                p_SMSIn.dataElaborazione = DateTime.Now;
            }
            else if (v_minutiPagati > 600)
            {
                // Verifica che i minuti inseriti siano validi
                p_SMSIn.notaElaborazione = "Il numero dei minuti inseriti non è valido";
                p_SMSIn.dataElaborazione = DateTime.Now;
            }
            else if (v_cellulare == null)
            {
                // Verifica che i minuti inseriti siano validi
                p_SMSIn.notaElaborazione = "Il cellulare che ha richiesto il pagamento, non è abilitato";
                p_SMSIn.dataElaborazione = DateTime.Now;
            }
            else
            {
                Stalli v_stallo = StalliBD.getStalloByNumero(v_numeroStallo.ToString(), p_context);

                if (v_stallo != null)
                {
                    DateTime v_dataRicezione = p_SMSIn.dataRicezione.HasValue ? p_SMSIn.dataRicezione.Value : DateTime.Now;
                    TitoliSMS v_titoloSMS = TitoliSMSBD.calcolaTitolo(v_stallo, v_minutiPagati, v_dataRicezione, p_context);

                    if (v_titoloSMS != null && v_titoloSMS.importo > 0)
                    {
                        Abbonamenti v_abbonamento = AbbonamentiBD.GetById(v_cellulare.idAbbonamento, p_context);

                        if (v_abbonamento != null)
                        {
                            if (AbbonamentiBD.getCreditoResiduo(v_abbonamento, p_context) >= v_titoloSMS.importo)
                            {
                                v_titoloSMS.idCellulare = v_cellulare.idCellulare;
                                v_titoloSMS.codice = p_idModem.ToString();                               

                                OperazioniLocal v_operazione = new OperazioniLocal();
                                v_operazione.codiceTitolo = v_titoloSMS.codice;
                                v_operazione.data = v_titoloSMS.dataPagamento;
                                v_operazione.idOperatore = 2;
                                v_operazione.idStallo = v_titoloSMS.idStallo;
                                v_operazione.X = 0;
                                v_operazione.Y = 0;
                                v_operazione.idVerbale = 0;
                                v_operazione.idPenale = 0;
                                v_operazione.fileFoto = string.Empty;
                                v_operazione.targa = string.Empty;
                                v_operazione.stato = OperazioniLocalBD.STATOREGOLARECONNUMERO;

                                p_context.OperazioniLocal.Add(v_operazione);

                                // Manda l'SMS con le informazioni dl pagamento
                                SMSOut v_SMSOut = new SMSOut();
                                v_SMSOut.dataElaborazione = DateTime.Now;
                                v_SMSOut.numeroDestinatario = p_SMSIn.numeroMittente;
                                v_SMSOut.numeroMittente = p_SMSIn.numeroDestinatario;
                                v_SMSOut.idSMSIn = p_SMSIn.idSMSIn;
                                string v_testo = "";

                                v_testo = v_testo + "Scadenza Parcheggio: " + v_titoloSMS.scadenza.ToShortTimeString() + " \r\n";
                                v_testo = v_testo + "Importo pagato: " + (v_titoloSMS.importo / 100).ToString("0.00") + " Euro" +  "\r\n";
                                v_testo = v_testo + "Per lo stallo numero: " + v_stallo.numero +  "\r\n";

                                v_SMSOut.testo = v_abbonamento.codice + Environment.NewLine + v_testo;
                                p_context.TitoliSMS.Add(v_titoloSMS);
                                p_context.SMSOut.Add(v_SMSOut);
                                p_context.SaveChanges();

                                v_titoloSMS.codice = p_idModem.ToString() + "." + v_titoloSMS.idTitoloSMS.ToString();
                                v_operazione.codiceTitolo = v_titoloSMS.codice;
                                p_context.SaveChanges();
                            }
                            else
                            {
                                // Segnala che il numero di cellulare non è valido
                                SMSOut v_smsRisp = new SMSOut();
                                v_smsRisp.idSMSIn = p_SMSIn.idSMSIn;
                                v_smsRisp.numeroDestinatario = p_SMSIn.numeroMittente;
                                v_smsRisp.numeroMittente = p_SMSIn.numeroDestinatario;
                                v_smsRisp.dataElaborazione = DateTime.Now;
                                v_smsRisp.testo = v_abbonamento.codice + Environment.NewLine + "Il credito dell'abbonamento e' insufficiente.";

                                p_context.SMSOut.Add(v_smsRisp);

                                p_SMSIn.notaElaborazione = "Credito insufficiente";
                                p_SMSIn.dataElaborazione = DateTime.Now;                               
                            }
                        }
                        else
                        {
                            p_SMSIn.notaElaborazione = "Abbonamento non valido";
                            p_SMSIn.dataElaborazione = DateTime.Now;
                        }
                    }
                    else
                    {
                        p_SMSIn.notaElaborazione = "L'importo del titolo è 0";
                        p_SMSIn.dataElaborazione = DateTime.Now;
                    }
                }
                else
                {
                    p_SMSIn.notaElaborazione = "E' stato indicato uno stallo inesistente";
                    p_SMSIn.dataElaborazione = DateTime.Now;
                }
            }

            p_SMSIn.dataElaborazione = DateTime.Now;            
            return risp;

        }

        private static Int32 getSaldo(SMSIn p_SMSIn, DbParkCtx p_context)
        {
            Int32 risp = -1;
            Cellulari v_cellulare = CellulariBD.getCellulareByNumero(p_SMSIn.numeroMittente, p_context);

            if (v_cellulare == null)
            {
                p_SMSIn.notaElaborazione = "Il cellulare che ha richiesto il saldo, non è abilitato";
                p_SMSIn.dataElaborazione = DateTime.Now;
            }
            else
            {
                Abbonamenti v_abbonamento = AbbonamentiBD.GetById(v_cellulare.idAbbonamento, p_context);

                if (v_abbonamento != null)
                {
                    //float v_saldo = (float)AbbonamentiBD.getCreditoResiduo(v_abbonamento, p_context);

                    decimal v_saldo = AbbonamentiBD.getCreditoResiduo(v_abbonamento, p_context);
                    // Manda l'SMS con le informazioni dl pagamento
                    SMSOut v_SMSOut = new SMSOut();

                    v_SMSOut.dataElaborazione = DateTime.Now;
                    v_SMSOut.numeroDestinatario = p_SMSIn.numeroMittente;
                    v_SMSOut.numeroMittente = p_SMSIn.numeroDestinatario;
                    v_SMSOut.idSMSIn = p_SMSIn.idSMSIn;

                    string v_testo = "";
                    v_testo = v_testo + "Credito residuo: " + (v_saldo / 100).ToString("0.00") + " Euro";

                    v_SMSOut.testo = v_abbonamento.codice + Environment.NewLine + v_testo;


                    p_SMSIn.dataElaborazione = DateTime.Now;
                    p_context.SMSOut.Add(v_SMSOut);
                    risp = 1;
                }
                else
                {
                    p_SMSIn.notaElaborazione = "Abbonamento non valido";
                    p_SMSIn.dataElaborazione = DateTime.Now;
                }
            }

            return risp;
        }

        private static Int32 getNumeroStallo(string p_testo)
        {
            Int32 risp = 0;
            Int32 v_separatore = 0;

            if (p_testo.Contains(" "))
                v_separatore = p_testo.IndexOf(" ");
            else if (p_testo.Contains("a"))
                v_separatore = p_testo.IndexOf("a");
            else if (p_testo.Contains("A"))
                v_separatore = p_testo.IndexOf("A");

            if (v_separatore > 1)
            {
                risp = Int32.Parse(p_testo.Substring(0, v_separatore));
            }
            return risp;
        }

        private static Int32 getMinutiRinnovo(string p_testo)
        {
            Int32 risp = 0;
            Int32 v_separatore = 0;

            if (p_testo.Contains(" "))
                v_separatore = p_testo.IndexOf(" ");
            else if (p_testo.Contains("a"))
                v_separatore = p_testo.IndexOf("a");
            else if (p_testo.Contains("A"))
                v_separatore = p_testo.IndexOf("A");

            if (v_separatore > 1)
            {
                risp = Int32.Parse(p_testo.Substring(v_separatore + 1));
            }

            return risp;
        }


    }
}
