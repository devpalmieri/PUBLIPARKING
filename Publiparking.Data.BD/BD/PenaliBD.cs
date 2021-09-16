using Publisoftware.Data;
using Publisoftware.Data.BD;
using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.BD
{
    public class PenaliBD : EntityBD<Penali>
    {
        private static ILogger m_logger = LoggerFactory.getInstance().getLogger<NLogger>("PenaliBD");


        public PenaliBD()
        {

        }
        public static IQueryable<Penali> getListPenaliDaElaborare(int p_ggtolleranza, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("getListPenaliDaElaborare"), EnLogSeverity.Debug);

            if (p_ggtolleranza > 0)
            {
                DateTime v_datariferimento = DateTime.Now.AddDays(-p_ggtolleranza);
                return GetList(p_context).Where(p => !p.dataElaborazione.HasValue)
                                         .Where(p => p.data > v_datariferimento);

            }
            else
            {
                return GetList(p_context).Where(p => !p.dataElaborazione.HasValue);
            }
        }


        public static IQueryable<Penali> getListPenaliDaMigrareBySerie(string p_serie, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("getListPenaliDaMigrareBySerie"), EnLogSeverity.Debug);

            return GetList(p_context).Where(p => p.dataElaborazione.HasValue)
                                     .Where(p => !p.id_anag_contribuente.HasValue);

        }
        public static void setPagata(Penali p_penale, translog p_titolo)
        {
            m_logger.LogMessage(String.Format("setPagata"), EnLogSeverity.Debug);

            p_penale.pagata = true;
            p_penale.dataPagamento = p_titolo.tlPayDateTime;
            p_penale.codiceTitoloPagante = p_titolo.tlLicenseNo;
            p_penale.dataElaborazione = DateTime.Now;
        }

        public static bool setNonPagata(Penali p_penale, string p_serieVerbale, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("setNonPagata"), EnLogSeverity.Debug);

            bool risp = false;

            p_penale.pagata = false;
            p_penale.dataElaborazione = DateTime.Now;

            Verbali v_verbale = null;

            if (p_serieVerbale.Length > 0)
            {
                v_verbale = VerbaliBD.creaVerbaleDaPenale(p_penale, p_context);
            }

            if (v_verbale != null)
            {
                string v_numeroVerbale = VerbaliBD.getCodiceBollettinoDisponibileBySerie(p_serieVerbale, p_context);
                v_verbale.codiceBollettino = (long.Parse(v_numeroVerbale) + 1).ToString();
                v_verbale.serie = p_serieVerbale;

                p_context.Verbali.Add(v_verbale);

            }

            return risp;
        }


        public static bool migraPenalePagata(Penali p_penale, string p_serieVerbale, Int32 p_idTipoOggetto, Int32 p_idStruttura, string p_fonte, Int32 p_idEnte, Int32 p_idEntrata, DbParkCtx p_parkcontext, dbEnte p_entecontext)
        {
            m_logger.LogMessage(String.Format("migraPenalePagata"), EnLogSeverity.Debug);
            bool risp = false;

            Operatori v_operatore = OperatoriBD.GetById(p_penale.idOperatore, p_parkcontext);
            if (v_operatore != null)
            {
                Int32 v_idRisorsa = AnagraficaRisorseBD.GetByUsername(v_operatore.username, p_entecontext).id_risorsa;
                //viene creato un contribuente con cognome uguale a codicepenale
                tab_contribuente v_contribuente = TabContribuenteBD.creaContribuente(p_penale.codice, p_penale.codice + "/" + p_serieVerbale, v_idRisorsa, p_fonte, p_idEnte, p_entecontext);
                Stalli v_stallo = StalliBD.GetById(p_penale.idStallo, p_parkcontext);
                if (v_stallo != null)
                {
                    Int32 v_idTipoVeicolo = 0;
                    anagrafica_tipo_veicolo ana_tipo_veicolo = AnagraficaTipoVeicoloBD.getTipoVeicoloByDescrizione(p_penale.tipoVeicolo, p_entecontext);
                    if (ana_tipo_veicolo != null)
                    {
                        v_idTipoVeicolo = ana_tipo_veicolo.id_tipo_veicolo;
                    }
                    tab_veicoli v_veicolo = TabVeicoliBD.creaVeicolo(p_penale.targa, p_penale.targaEstera, p_penale.marca, p_penale.modello, v_idTipoVeicolo, p_fonte, p_idStruttura, v_idRisorsa, p_entecontext);
                    tab_oggetti v_oggetto = TabOggettiBD.creaOggetto(v_stallo.numero, p_penale.data, p_idStruttura, v_idRisorsa, v_veicolo.id_veicolo, v_stallo.idToponimo.Value, (double)v_stallo.X, (double)v_stallo.Y, p_idTipoOggetto, p_idEnte, p_idEntrata, p_entecontext);

                    foreach (FotoPenali v_foto in p_penale.FotoPenali)
                    {
                        string v_nomeFile = v_foto.fileFoto.Substring(0, 4) + @"\" + v_foto.fileFoto.Substring(4, 2) + @"\" + v_foto.fileFoto;
                        JoinFileBD.creaJoinFile((int)v_oggetto.id_oggetto, v_nomeFile + ".jpg", p_penale.data, v_idRisorsa, p_idStruttura, p_idEntrata, 5, "CDS", p_entecontext);
                    }

                    Configurazione v_configurazione = ConfigurazioneBD.GetList(p_parkcontext).FirstOrDefault();
                    tab_denunce_contratti v_denunciaContratto = TabDenunceContrattiBD.creaDenunciaContratto((int)v_contribuente.id_anag_contribuente, 143, p_penale.codice, "61Z01", p_penale.data, p_idStruttura, v_configurazione.idTrasgressoreAssenteCausale, v_idRisorsa, p_penale.note, p_penale.assenzatrasgressore, 46, "ATT-PAG", p_idEnte, p_idEntrata, p_entecontext);
                    tab_oggetti_contribuzione v_oggetti = TabOggettiContribuzioneBD.creaOggettoContribuzionePark(v_denunciaContratto.id_tab_denunce_contratti, v_contribuente.id_anag_contribuente, v_oggetto.id_oggetto, 883, p_penale.totale, p_penale.data, v_idRisorsa, p_idStruttura, v_veicolo.id_veicolo, 0, p_idEnte, p_idEntrata, p_entecontext);

                }
                p_penale.id_anag_contribuente = v_contribuente.id_anag_contribuente;//marca la penale come importata
                risp = true;
            }
            return risp;
        }


        public static bool migraPenaleConVerbale(Penali p_penale, string p_serieVerbale, string p_contoCorrente, Int32 p_giorniScadenzaAvvPag, Int32 p_idTipoOggetto, Int32 p_idStruttura, string p_fonte, Int32 p_idEnte, Int32 p_idEntrata, DbParkCtx p_parkcontext, dbEnte p_entecontext)
        {
            m_logger.LogMessage(String.Format("migraPenaleConVerbale"), EnLogSeverity.Debug);

            bool risp = false;
            Verbali v_verbale = new Verbali();

            if (p_penale.idVerbale.HasValue)
                v_verbale = VerbaliBD.GetById(p_penale.idVerbale.Value, p_parkcontext);

            Operatori v_operatore = OperatoriBD.GetById(p_penale.idOperatore, p_parkcontext);
            if (v_operatore != null)
            {
                Int32 v_idRisorsa = AnagraficaRisorseBD.GetByUsername(v_operatore.username, p_entecontext).id_risorsa;
                //viene creato un contribuente con cognome uguale a codicepenale
                tab_contribuente v_contribuente = TabContribuenteBD.creaContribuente(p_penale.codice, p_penale.codice + "/" + p_serieVerbale, v_idRisorsa, p_fonte, p_idEnte, p_entecontext);
                Stalli v_stallo = StalliBD.GetById(p_penale.idStallo, p_parkcontext);
                if (v_stallo != null)
                {
                    Int32 v_idTipoVeicolo = 0;
                    anagrafica_tipo_veicolo ana_tipo_veicolo = AnagraficaTipoVeicoloBD.getTipoVeicoloByDescrizione(p_penale.tipoVeicolo, p_entecontext);
                    if (ana_tipo_veicolo != null)
                    {
                        v_idTipoVeicolo = ana_tipo_veicolo.id_tipo_veicolo;
                    }
                    tab_veicoli v_veicolo = TabVeicoliBD.creaVeicolo(p_penale.targa, p_penale.targaEstera, p_penale.marca, p_penale.modello, v_idTipoVeicolo, p_fonte, p_idStruttura, v_idRisorsa, p_entecontext);
                    tab_oggetti v_oggetto = TabOggettiBD.creaOggetto(v_stallo.numero, p_penale.data, p_idStruttura, v_idRisorsa, v_veicolo.id_veicolo, v_stallo.idToponimo.Value, (double)v_stallo.X, (double)v_stallo.Y, p_idTipoOggetto, p_idEnte, p_idEntrata, p_entecontext);

                    foreach (FotoPenali v_foto in p_penale.FotoPenali)
                    {
                        string v_nomeFile = v_foto.fileFoto.Substring(0, 4) + @"\" + v_foto.fileFoto.Substring(4, 2) + @"\" + v_foto.fileFoto;
                        JoinFileBD.creaJoinFile((int)v_oggetto.id_oggetto, v_nomeFile + ".jpg", p_penale.data, v_idRisorsa, p_idStruttura, p_idEntrata, 5, "CDS", p_entecontext);
                    }
                    Configurazione v_configurazione = ConfigurazioneBD.GetList(p_parkcontext).FirstOrDefault();
                    tab_denunce_contratti v_denunciaContrattoPenale = TabDenunceContrattiBD.creaDenunciaContratto((int)v_contribuente.id_anag_contribuente, 143, p_penale.codice, "61Z01", p_penale.data, p_idStruttura, v_configurazione.idTrasgressoreAssenteCausale, v_idRisorsa, p_penale.note, p_penale.assenzatrasgressore, 46, "ATT-PAG", p_idEnte, p_idEntrata, p_entecontext);
                    if (v_verbale != null)
                    {
                        tab_denunce_contratti v_denunciaContrattoVerbale = TabDenunceContrattiBD.creaDenunciaContratto(v_contribuente.id_anag_contribuente, 144, v_verbale.codiceBollettino, "61A02", v_verbale.data, p_idStruttura, v_configurazione.idTrasgressoreAssenteCausale, v_idRisorsa, v_verbale.note, v_verbale.assenzatrasgressore, 38, "ATT-ATT", p_idEnte, p_idEntrata, p_entecontext);
                        string v_barCode = "6102" + v_verbale.codiceBollettino.ToString().PadLeft(10, '0') + "00";
                        decimal v_importoRidotto = v_verbale.totale * ((100 - v_configurazione.percentualeRiduzioneVerbale) / 100);

                        anagrafica_tipo_avv_pag v_tipoAvvisoDaEmettere = AnagraficaTipoAvvPagBD.GetById(v_configurazione.idTipoAvvPag.Value, p_entecontext); // da modificare
                        tab_liste v_lista = TabListeBD.VerificaEsistenzaListaPREPRE(p_entecontext, p_idEnte, v_configurazione.idTipoAvvPag.Value,
                                                                               v_tipoAvvisoDaEmettere, v_configurazione.idTipoLista.Value, DateTime.Now);

                        if (v_lista == null)
                        {
                            int v_ProgressivoLista = TabProgListaBD.IncrementaProgressivoCorrente(p_idEnte,v_configurazione.idTipoLista.Value, DateTime.Now.Year, p_entecontext);
                            string v_parametri_calcolo = string.Empty;
                            v_lista = TabListeBD.CreaListaEmissione(p_idEnte, v_tipoAvvisoDaEmettere.id_entrata, v_configurazione.idTipoLista.Value, v_configurazione.idTipoAvvPag.Value, v_ProgressivoLista, DateTime.Now, DateTime.Now.Year.ToString(), string.Empty, p_entecontext);
                        }


                        tab_avv_pag v_avviso = TabAvvPagBD.creaAvvisoPark(v_contribuente.id_anag_contribuente, v_configurazione.idTipoAvvPagPenaleConVerbale.Value,
                                                                          v_verbale.codiceBollettino, v_barCode, v_verbale.totale, v_importoRidotto, v_verbale.data,
                                                                          p_idStruttura, v_idRisorsa, v_denunciaContrattoVerbale.id_tab_denunce_contratti, p_fonte,
                                                                          v_lista.id_lista, p_giorniScadenzaAvvPag, p_idEnte, p_idEntrata, p_entecontext);

                        int v_numRigo = 0;
                        foreach (CausaliVerbali v_causaliVerbali in v_verbale.CausaliVerbali)
                        {
                            Causali v_causale = CausaliBD.GetById(v_causaliVerbali.idCausale, p_parkcontext);
                            anagrafica_categoria v_ana_categoria = AnagraficaCategoriaBD.getAnagraficaByArticoloCommaSubCodice(v_causale.articolo + "-" + v_causale.codice + "-" + v_causale.subCodice, p_idEnte, p_idEntrata, p_entecontext);
                            tab_oggetti_contribuzione v_oggetto_contribuzione = TabOggettiContribuzioneBD.creaOggettoContribuzionePark(v_denunciaContrattoVerbale.id_tab_denunce_contratti, v_contribuente.id_anag_contribuente, v_oggetto.id_oggetto, v_ana_categoria.id_categoria_contribuzione, v_causale.importo, v_verbale.data, v_idRisorsa, p_idStruttura, v_veicolo.id_veicolo, 0, p_idEnte, p_idEntrata, p_entecontext);
                            tab_unita_contribuzione v_unita_contribuzione = TabUnitaContribuzioneBD.creaUnitaContribuzionePark(v_avviso, v_configurazione.idTipoAvvPag.Value, v_numRigo++, v_contribuente.id_anag_contribuente, v_oggetto.id_oggetto, v_oggetto_contribuzione, v_veicolo.id_veicolo, v_causale.importo, v_verbale.data, p_idStruttura, v_idRisorsa, p_idEnte, p_idEntrata, p_entecontext);
                        }
                        tab_rata_avv_pag v_rata = TabRataAvvPagBD.creaRataPArk(v_avviso, v_verbale.totale, v_verbale.data, v_idRisorsa, p_idStruttura, p_entecontext);
                        string v_checkDidit = (long.Parse(v_barCode) % 93).ToString();
                        tab_boll_pag v_bollpag = TabBollPagBD.creaBollPagPark(p_contoCorrente, v_contribuente.id_anag_contribuente, v_avviso, v_barCode, v_checkDidit.ToString().PadLeft(2, '0'), v_rata, v_verbale.totale, v_verbale.data, p_idStruttura, v_idRisorsa, p_entecontext);

                        //segna il verbale come importato
                        v_verbale.id_anag_contribuente = v_contribuente.id_anag_contribuente;
                    }
                    else
                    {
                        tab_denunce_contratti v_denunciaContrattoVerbale = TabDenunceContrattiBD.creaDenunciaContratto(v_contribuente.id_anag_contribuente, 211, p_penale.codice, "61A03", p_penale.data, p_idStruttura, v_configurazione.idTrasgressoreAssenteCausale, v_idRisorsa, p_penale.note, p_penale.assenzatrasgressore, 38, "ATT-ATT", p_idEnte, p_idEntrata, p_entecontext);
                        string v_barCode = "6103" + v_verbale.codiceBollettino.ToString().PadLeft(10, '0') + "00";
                        decimal v_importoRidotto = 0;

                        anagrafica_tipo_avv_pag v_tipoAvvisoDaEmettere = AnagraficaTipoAvvPagBD.GetById(v_configurazione.idTipoAvvPag.Value, p_entecontext); // da modificare
                        tab_liste v_lista = TabListeBD.VerificaEsistenzaListaPREPRE(p_entecontext, p_idEnte, v_configurazione.idTipoAvvPag.Value,
                                                                               v_tipoAvvisoDaEmettere, v_configurazione.idTipoLista.Value, DateTime.Now);

                        if (v_lista == null)
                        {
                            int v_ProgressivoLista = TabProgListaBD.IncrementaProgressivoCorrente(p_idEnte,v_configurazione.idTipoLista.Value, DateTime.Now.Year, p_entecontext);
                            string v_parametri_calcolo = string.Empty;
                            v_lista = TabListeBD.CreaListaEmissione(p_idEnte, v_tipoAvvisoDaEmettere.id_entrata, v_configurazione.idTipoLista.Value, v_configurazione.idTipoAvvPag.Value, v_ProgressivoLista, DateTime.Now, DateTime.Now.Year.ToString(), string.Empty, p_entecontext);
                        }
                        tab_avv_pag v_avviso = TabAvvPagBD.creaAvvisoPark(v_contribuente.id_anag_contribuente, v_configurazione.idTipoAvvPagSenzaVerbale.Value,
                                                                         p_penale.codice, v_barCode, p_penale.totale, v_importoRidotto, p_penale.data,
                                                                         p_idStruttura, v_idRisorsa, v_denunciaContrattoVerbale.id_tab_denunce_contratti, p_fonte,
                                                                         v_lista.id_lista, p_giorniScadenzaAvvPag, p_idEnte, p_idEntrata, p_entecontext);

                        anagrafica_categoria v_ana_categoria = AnagraficaCategoriaBD.GetById(v_configurazione.idAnagraficaCategoriaDefault.Value, p_entecontext);
                        tab_oggetti_contribuzione v_oggetto_contribuzione = TabOggettiContribuzioneBD.creaOggettoContribuzionePark(v_denunciaContrattoVerbale.id_tab_denunce_contratti, v_contribuente.id_anag_contribuente, v_oggetto.id_oggetto, v_ana_categoria.id_categoria_contribuzione, p_penale.totale, p_penale.data, v_idRisorsa, p_idStruttura, v_veicolo.id_veicolo, 0, p_idEnte, p_idEntrata, p_entecontext);
                        tab_unita_contribuzione v_unita_contribuzione = TabUnitaContribuzioneBD.creaUnitaContribuzionePark(v_avviso, v_configurazione.idTipoAvvPagSenzaVerbale.Value, 1, v_contribuente.id_anag_contribuente, v_oggetto.id_oggetto, v_oggetto_contribuzione, v_veicolo.id_veicolo, p_penale.totale, p_penale.data, p_idStruttura, v_idRisorsa, p_idEnte, p_idEntrata, p_entecontext);
                        tab_rata_avv_pag v_rata = TabRataAvvPagBD.creaRataPArk(v_avviso, p_penale.totale, p_penale.data, v_idRisorsa, p_idStruttura, p_entecontext);
                        string v_checkDidit = (long.Parse(v_barCode) % 93).ToString();
                        tab_boll_pag v_bollpag = TabBollPagBD.creaBollPagPark(p_contoCorrente, v_contribuente.id_anag_contribuente, v_avviso, "", "", v_rata, p_penale.totale, p_penale.data, p_idStruttura, v_idRisorsa, p_entecontext);
                    }
                    tab_oggetti_contribuzione v_oggetto_contribuzione_penale = TabOggettiContribuzioneBD.creaOggettoContribuzionePark(v_denunciaContrattoPenale.id_tab_denunce_contratti, v_contribuente.id_anag_contribuente, v_oggetto.id_oggetto, 883, p_penale.totale, p_penale.data, v_idRisorsa, p_idStruttura, v_veicolo.id_veicolo, 0, p_idEnte, p_idEntrata, p_entecontext);
                }
                p_penale.id_anag_contribuente = v_contribuente.id_anag_contribuente;//marca la penale come importata
                risp = true;
            }
            return risp;
        }

        public static bool isValid(string pCodice, Int32 pIDStallo, DbParkCtx p_parkcontext)
        {
            m_logger.LogMessage(String.Format("isValid"), EnLogSeverity.Debug);

            bool risp = false;
            Penali v_penale =  GetList(p_parkcontext).Where(p => p.codice.Equals(pCodice))
                .OrderByDescending(p=> p.idPenale)
                .FirstOrDefault();

            if (v_penale != null)
            {
                if (v_penale.idStallo == pIDStallo & v_penale.data.Date == DateTime.Now.Date)
                {
                    risp = true;
                }
            }

            return risp;
        }

        public static Penali loadByCodice(string pcodice, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("loadByCodice"), EnLogSeverity.Debug);
            return GetList(p_context).Where(p => p.codice.Equals(pcodice))
                                     .OrderByDescending(p => p.idPenale)
                                     .FirstOrDefault();

        }

        public static string generaCodice(DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("generaCodice"), EnLogSeverity.Debug);
            Random v_random = new Random();
            string risp = "";

            while ((risp.Length < 12 || AbbonamentiBD.existByCodice(risp, p_context) || existByCodice(risp, p_context)))
                risp = (v_random.NextDouble() * 1000000000000).ToString("0");

            return risp;
        }

        public static bool existByCodice(string p_codice, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("existByCodice"), EnLogSeverity.Debug);
            return GetList(p_context).Where(a => a.codice.Equals(p_codice)).Any() ? true : false;

        }
    }
}
