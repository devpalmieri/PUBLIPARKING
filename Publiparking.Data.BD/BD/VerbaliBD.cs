using Publisoftware.Data;
using Publisoftware.Data.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.BD
{
    public class VerbaliBD : EntityBD<Verbali>
    {
        public VerbaliBD()
        {

        }

        public static Verbali creaVerbaleDaPenale(Penali p_penale, DbParkCtx p_context)
        {
            Verbali risp = null;

            if (p_penale != null)
            {
                Verbali v_verbale = new Verbali();
                TipiVerbale v_defaultTipoVerbale = TipiVerbaleBD.getDefault(p_context);

                v_verbale.assenzatrasgressore = p_penale.assenzatrasgressore;
               
                //Caricamento causali per il tipo verbale di default
                //foreach (Causali v_causali in v_defaultTipoVerbale.Causali)
                //{
                //    CausaliVerbali v_Causaleverbale = new CausaliVerbali();
                //    v_Causaleverbale.idCausale = v_causali.idCausale;
                //    v_verbale.CausaliVerbali.Add(v_Causaleverbale);
                //}

                CausaliVerbali cv;
                decimal v_totale = 0;
                foreach (int codice in v_defaultTipoVerbale.Causali.Select(c=> c.idCausale))
                {
                    cv = new CausaliVerbali();
                    cv.Verbali = v_verbale;
                    cv.idCausale = codice;
                    p_context.CausaliVerbali.Add(cv);
                    Causali v_causale = CausaliBD.GetById(codice, p_context);
                    v_totale = v_totale + v_causale.importo;
                }
                v_verbale.totale = v_totale;


                //caricamento foto              
                //foreach (FotoPenali v_fotoPenale in p_penale.FotoPenali)
                //{
                //    FotoVerbali v_fotoVerbale = new FotoVerbali();
                //    v_fotoVerbale.fileFoto = v_fotoPenale.fileFoto;                    
                //    v_verbale.FotoVerbali.Add(v_fotoVerbale);
                //}
                FotoVerbali fv;
                foreach (string v_foto in p_penale.FotoPenali.Select(f=> f.fileFoto))
                {
                    fv = new FotoVerbali();
                    fv.Verbali = v_verbale;
                    fv.fileFoto = v_foto;
                    p_context.FotoVerbali.Add(fv);
                }
                
                v_verbale.data = p_penale.data;
                v_verbale.idOperatore = p_penale.idOperatore;
                v_verbale.idStallo = p_penale.idStallo;
                v_verbale.marca = p_penale.marca;
                v_verbale.modello = p_penale.modello;
                v_verbale.note = p_penale.note;
                v_verbale.targa = p_penale.targa;
                v_verbale.targaEstera = p_penale.targaEstera;
                v_verbale.tipoVeicolo = p_penale.tipoVeicolo;
                v_verbale.via = p_penale.via; //ubicazione
               
                risp = v_verbale;
            }

            return risp;
        }


        public static string getCodiceBollettinoDisponibileBySerie(string v_serieVerbale, DbParkCtx p_context)
        {
            if (GetList(p_context).Where(v => v.serie.Equals(v_serieVerbale)).Count() > 0)
            {
                return GetList(p_context).Where(v => v.serie.Equals(v_serieVerbale)).OrderByDescending(v => v.idVerbale).FirstOrDefault().codiceBollettino;
            }
            else
            {
                return "0";
            }
            
        }

        public static IQueryable<Verbali> getListVerbaliAusiliariDaMigrare(DbParkCtx p_context)
        {

            return GetList(p_context).Where(v => v.serie.Equals("A")).Where(v => !v.id_anag_contribuente.HasValue);

        }

        public static bool migraVerbaleAusiliari(Verbali p_verbale, int p_giorniScadenzaAvvPag, int p_idTipoOggetto, int p_idStruttura, string p_fonte, int p_idEnte, int p_idEntrata, DbParkCtx p_parkcontext, dbEnte p_entecontext)
        {
            bool retVal = false;
            Operatori v_operatore = OperatoriBD.GetById(p_verbale.idOperatore, p_parkcontext);
            if (v_operatore != null)
            {
                int v_idRisorsa = AnagraficaRisorseBD.GetByUsername(v_operatore.username, p_entecontext).id_risorsa;
                //viene creato un contribuente con cognome uguale a codicepenale
                tab_contribuente v_contribuente = TabContribuenteBD.creaContribuente(p_verbale.codiceBollettino, p_verbale.codiceBollettino, v_idRisorsa, p_fonte, p_idEnte, p_entecontext);
                Stalli v_stallo = StalliBD.GetById(p_verbale.idStallo, p_parkcontext);
                if (v_stallo != null)
                {
                    Int32 v_idTipoVeicolo = 0;
                    anagrafica_tipo_veicolo ana_tipo_veicolo = AnagraficaTipoVeicoloBD.getTipoVeicoloByDescrizione(p_verbale.tipoVeicolo, p_entecontext);
                    if (ana_tipo_veicolo != null)
                    {
                        v_idTipoVeicolo = ana_tipo_veicolo.id_tipo_veicolo;
                    }
                    tab_veicoli v_veicolo = TabVeicoliBD.creaVeicolo(p_verbale.targa, p_verbale.targaEstera, p_verbale.marca, p_verbale.modello, v_idTipoVeicolo, p_fonte, p_idStruttura, v_idRisorsa, p_entecontext);
                    tab_oggetti v_oggetto = TabOggettiBD.creaOggetto(v_stallo.numero, p_verbale.data, p_idStruttura, v_idRisorsa, v_veicolo.id_veicolo, v_stallo.idToponimo.Value, (double)v_stallo.X, (double)v_stallo.Y, p_idTipoOggetto, p_idEnte, p_idEntrata, p_entecontext);


                    foreach (FotoVerbali v_foto in p_verbale.FotoVerbali)
                    {
                        string v_nomeFile = v_foto.fileFoto.Substring(0, 4) + @"\" + v_foto.fileFoto.Substring(4, 2) + @"\" + v_foto.fileFoto;
                        JoinFileBD.creaJoinFile((int)v_oggetto.id_oggetto, v_nomeFile + ".jpg", p_verbale.data, v_idRisorsa, p_idStruttura, p_idEntrata, 5, "CDS", p_entecontext);
                    }

                    Configurazione v_configurazione = ConfigurazioneBD.GetList(p_parkcontext).FirstOrDefault();
                    tab_denunce_contratti v_denunciaContratto = TabDenunceContrattiBD.creaDenunciaContratto(v_contribuente.id_anag_contribuente, 119, p_verbale.codiceBollettino, "61A01", p_verbale.data, p_idStruttura, v_configurazione.idTrasgressoreAssenteCausale, v_idRisorsa, p_verbale.note, p_verbale.assenzatrasgressore, 38, "ATT-ATT", p_idEnte, p_idEntrata, p_entecontext);
                   // string v_barCode = "6102" + v_verbale.codiceBollettino.ToString().PadLeft(10, '0') + "00";
                    decimal v_importoRidotto = p_verbale.totale * ((100 - v_configurazione.percentualeRiduzioneVerbale) / 100);

                    anagrafica_tipo_avv_pag v_tipoAvvisoDaEmettere = AnagraficaTipoAvvPagBD.GetById(v_configurazione.idTipoAvvPag.Value, p_entecontext); // da modificare
                    tab_liste v_lista = TabListeBD.VerificaEsistenzaListaPREPRE(p_entecontext, p_idEnte, v_configurazione.idTipoAvvPag.Value,
                                                                           v_tipoAvvisoDaEmettere, v_configurazione.idTipoLista.Value, DateTime.Now);

                    if (v_lista == null)
                    {
                        int v_ProgressivoLista = TabProgListaBD.IncrementaProgressivoCorrente(p_idEnte, v_configurazione.idTipoLista.Value, DateTime.Now.Year, p_entecontext);
                        string v_parametri_calcolo = string.Empty;
                        v_lista = TabListeBD.CreaListaEmissione(p_idEnte, v_tipoAvvisoDaEmettere.id_entrata, v_configurazione.idTipoLista.Value, v_configurazione.idTipoAvvPag.Value, v_ProgressivoLista, DateTime.Now, DateTime.Now.Year.ToString(), string.Empty, p_entecontext);
                    }


                    tab_avv_pag v_avviso = TabAvvPagBD.creaAvvisoPark(v_contribuente.id_anag_contribuente, 61,
                                                                      p_verbale.codiceBollettino, p_verbale.codiceBollettino, p_verbale.totale, v_importoRidotto, p_verbale.data,
                                                                      p_idStruttura, v_idRisorsa, v_denunciaContratto.id_tab_denunce_contratti, p_fonte,
                                                                      v_lista.id_lista, p_giorniScadenzaAvvPag, p_idEnte, p_idEntrata, p_entecontext);
                   

                    int v_numRigo = 0;
                    foreach (CausaliVerbali v_causaliVerbali in p_verbale.CausaliVerbali)
                    {
                        Causali v_causale = CausaliBD.GetById(v_causaliVerbali.idCausale, p_parkcontext);
                        anagrafica_categoria v_ana_categoria = AnagraficaCategoriaBD.getAnagraficaByArticoloCommaSubCodice(v_causale.articolo + "-" + v_causale.codice + "-" + v_causale.subCodice, p_idEnte, p_idEntrata, p_entecontext);
                        tab_oggetti_contribuzione v_oggetto_contribuzione = TabOggettiContribuzioneBD.creaOggettoContribuzionePark(v_denunciaContratto.id_tab_denunce_contratti, v_contribuente.id_anag_contribuente, v_oggetto.id_oggetto, v_ana_categoria.id_categoria_contribuzione, v_causale.importo, p_verbale.data, v_idRisorsa, p_idStruttura, v_veicolo.id_veicolo, 0, p_idEnte, p_idEntrata, p_entecontext);
                        tab_unita_contribuzione v_unita_contribuzione = TabUnitaContribuzioneBD.creaUnitaContribuzionePark(v_avviso, v_configurazione.idTipoAvvPag.Value, v_numRigo++, v_contribuente.id_anag_contribuente, v_oggetto.id_oggetto, v_oggetto_contribuzione, v_veicolo.id_veicolo, v_causale.importo, p_verbale.data, p_idStruttura, v_idRisorsa, p_idEnte, p_idEntrata, p_entecontext);
                    }

                    tab_rata_avv_pag v_rata = TabRataAvvPagBD.creaRataPArk(v_avviso, p_verbale.totale, p_verbale.data, v_idRisorsa, p_idStruttura, p_entecontext);
                    Bollettini v_bollettino = BollettiniBD.GetById(Int32.Parse(p_verbale.codiceBollettino), p_parkcontext);
                    tab_boll_pag v_bollpag = TabBollPagBD.creaBollPagPark(v_bollettino.contoCorrente, v_contribuente.id_anag_contribuente, v_avviso, v_bollettino.codeLine.Value.ToString(), v_bollettino.checkCode.ToString().PadLeft(2, '0'), v_rata, p_verbale.totale, p_verbale.data, p_idStruttura, v_idRisorsa, p_entecontext);

                    //segna il verbale come importato
                    p_verbale.id_anag_contribuente = v_contribuente.id_anag_contribuente;
                    retVal = true;
                }


            }
            return retVal;
        }

        public static Verbali loadByCodiceBollettino(string pCodiceBollettino, string p_serieDefault, string p_serie, DbParkCtx p_context)
        {
            string v_serie = null;
            if (p_serie == null || p_serie.Length == 0)
            {
                v_serie = p_serieDefault;               
            }
            else
            {
                v_serie = p_serie;
            }
            return GetList(p_context).Where(v => v.codiceBollettino.Equals(pCodiceBollettino))
                                       .Where(v => v.serie.Equals(v_serie))
                                       .OrderByDescending(v => v.idVerbale).FirstOrDefault();
        }

        public static Verbali getLastVerbaleByIdOperatoreAndSerie(int pIdOperatrore, string p_serie, DbParkCtx p_context)
        {
            return GetList(p_context)
                        .Where(v => v.idOperatore.Equals(pIdOperatrore))
                        .Where(v => v.serie.Equals(p_serie))
                        .OrderByDescending(v => v.idVerbale).FirstOrDefault();
        }

        public static bool isValid(string pCodice, Int32 pIDStallo, string pSerieDefault, string pSerie, DbParkCtx p_context)
        {
            bool risp = false;
            Verbali v_verbale = loadByCodiceBollettino(pCodice,pSerieDefault,pSerie,p_context);

            if (v_verbale != null)
            {
                if (v_verbale.idStallo == pIDStallo & v_verbale.data.Date == DateTime.Now.Date)
                {
                    risp = true;
                }
            }

            return risp;
        }
    }
}
