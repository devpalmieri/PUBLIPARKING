using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.BD.Exceptions;
using Publisoftware.Data.LinqExtended;
using Publisoftware.Utility.Log;

namespace Publisoftware.Data.BD
{
    public class TabAvvPagBD : EntityBD<tab_avv_pag>
    {
        private const string FLAG_CUR_FALLIMENTARE = "F";
        private const string FLAG_EREDI = "E";
        private const string FLAG_TRZ_DEB = "T";
        private const string FLAG_SOGG_DEB = "D";

        private static ILogger m_logger = LoggerFactory.getInstance().getLogger<NLogger>("TabAvvPagBD");

        public TabAvvPagBD()
        {

        }

        public static tab_avv_pag CreaAvviso_Rettifica(tab_avv_pag_fatt_emissione p_avvisoFattEmissione, dbEnte p_specificDB)
        {
            m_logger.LogMessage(String.Format("--> Creazione avviso da avv fatt emissione {0}", p_avvisoFattEmissione.id_tab_avv_pag), EnLogSeverity.Debug);
            m_logger.LogMessage(String.Format("Creazione avviso da avv fatt emissione {0}", p_avvisoFattEmissione.id_tab_avv_pag), EnLogSeverity.Info);

            //Crea entity
            tab_avv_pag v_avvisoDef = p_specificDB.tab_avv_pag.Create(); // new tab_avv_pag();
            p_specificDB.tab_avv_pag.Add(v_avvisoDef);

            v_avvisoDef.setProperties(p_avvisoFattEmissione, true);
            //v_avvisoDef.tab_contribuente = null;
            //v_avvisoDef.id_tab_avv_pag = 0;
            //v_avvisoDef.anagrafica_stato_avv_pag = AnagraficaStatoAvvPagBD.GetById(v_avvisoDef.id_stato_avv_pag, p_specificDB);

            int? IdListaScarico = v_avvisoDef.id_lista_scarico;
            v_avvisoDef.tab_liste = null;
            v_avvisoDef.id_lista_scarico = IdListaScarico; // v_avvisoDef.id_lista_emissione;
            v_avvisoDef.id_lista_emissione = p_avvisoFattEmissione.id_lista_emissione;

            tab_avv_pag avvisoOriginale = TabAvvPagBD.GetById(v_avvisoDef.num_avv_riemesso.HasValue ? v_avvisoDef.num_avv_riemesso.Value : 0, p_specificDB);
            if (avvisoOriginale == null)
            {
                throw new Exception(String.Format("Avviso originale id: {0} relativo a avv fatt. id: {1} non trovato", v_avvisoDef.num_avv_riemesso, v_avvisoDef.id_tab_avv_pag));
            }

            //MIMMO:NON PRESENTE NEL DOCUMENTO
            //Modifica stato avviso originale => RET + preserva seconda parte
            avvisoOriginale.cod_stato = String.Format(CodStato.RET + "{0}", avvisoOriginale.cod_stato.Substring(4, 3));
            avvisoOriginale.id_stato = AnagraficaStatoAvvPagBD.GetIdFromCodStato(avvisoOriginale.cod_stato, p_specificDB);

            if (v_avvisoDef.fonte_emissione != tab_avv_pag.FONTE_IMPORTATA)
            {
                //mov pag
                List<join_avv_pag_mov_pag> movimentiOriginale = avvisoOriginale.join_avv_pag_mov_pag1.Where(j => !j.cod_stato.StartsWith(CodStato.ANN)).ToList();

                movimentiOriginale.ForEach(currMov =>
                {
                    currMov.cod_stato = String.Format("ANN-{0}", currMov.cod_stato.Substring(4, 3));

                    //Copia mov pag x nuovo avviso
                    join_avv_pag_mov_pag nuovoMov = new join_avv_pag_mov_pag();
                    nuovoMov.setProperties(currMov);
                    nuovoMov.id_avv_pag_mov_pag = 0;
                    nuovoMov.cod_stato = String.Format("{0}-ACC", currMov.cod_stato.Substring(0, 3));

                    p_specificDB.join_avv_pag_mov_pag.Add(nuovoMov);
                });

                v_avvisoDef.importo_tot_da_pagare += v_avvisoDef.imp_tot_pagato;
                v_avvisoDef.imp_tot_pagato = 0;
            }

            int id_ente = v_avvisoDef.id_ente;
            int id_ente_gestito = v_avvisoDef.id_ente_gestito;
            //MIMMO:FINE NON PRESENTE NEL DOCUMENTO
            //pag.36 Consolida_unita_contribuzione
            IQueryable<tab_unita_contribuzione_fatt_emissione> v_unita_fatt_emissioneList = p_avvisoFattEmissione.tab_unita_contribuzione_fatt_emissione.AsQueryable();
            m_logger.LogMessage(String.Format("num. unita contribuzione da consolidare: {0}", v_unita_fatt_emissioneList.Count()), EnLogSeverity.Debug);

            foreach (tab_unita_contribuzione_fatt_emissione v_unita_fatt_emissione in v_unita_fatt_emissioneList)
            {//pag.36 LOOP_CONSOLIDA_UNITA_CONTRIBUZIONE
                m_logger.LogMessage(String.Format("--> --> Lavorazione unita contribuzione fatt emissione: {0}", v_unita_fatt_emissione.id_unita_contribuzione), EnLogSeverity.Debug);

                //Se unita contribuzione è relativa ad avviso...nuova aggiunta
                if (v_unita_fatt_emissione.id_avv_pag_collegato.HasValue)
                {//pag.36 UNITA_CONTRIBUZIONE_AVVISO_COLLEGATO
                    tab_avv_pag avvColl = v_unita_fatt_emissione.tab_avv_pag;

                    //Aggiornamenti avviso collegato
                    anagrafica_tipo_avv_pag anaTipoAvvConsolidato = AnagraficaTipoAvvPagBD.GetById(v_avvisoDef.id_tipo_avvpag, p_specificDB);
                    avvColl.id_stato = anaTipoAvvConsolidato.id_stato_avvpag_collegati.HasValue ? anaTipoAvvConsolidato.id_stato_avvpag_collegati.Value : 0;
                    avvColl.cod_stato = anaTipoAvvConsolidato.cod_stato_avvpag_collegati;

                    if (
                        (v_unita_fatt_emissione.importo_sanzioni_eliminate_eredi == null || v_unita_fatt_emissione.importo_sanzioni_eliminate_eredi.Value == 0)
                        && (avvColl.importoTotDaPagare == v_unita_fatt_emissione.importo_unita_contribuzione
                            ||
                            avvColl.importoTotDaPagare - v_unita_fatt_emissione.importo_unita_contribuzione < 1
                            ||
                            v_unita_fatt_emissione.importo_unita_contribuzione - avvColl.importoTotDaPagare < 1
                            ))
                    {
                        m_logger.LogMessage(String.Format("Riferimento ad avviso collegato (id:{0}) con importi coincidenti e agevolazione {1}", v_unita_fatt_emissione.id_avv_pag_collegato, v_unita_fatt_emissione.importo_sanzioni_eliminate_eredi), EnLogSeverity.Debug);


                        //Consolida unità
                        //pag.38 INSERISCI_UNITA_CONSOLIDATA
                        m_logger.LogMessage(String.Format("Creazione unita contribuzione da avv fatt emissione {0}", v_unita_fatt_emissione.id_unita_contribuzione), EnLogSeverity.Debug);
                        //v_unita_fatt_emissione.LoadAllProperties(p_specificDB);
                        tab_unita_contribuzione v_unitaDef = p_specificDB.tab_unita_contribuzione.Create(); // TabUnitaContribuzioneBD.Consolida(v_unita_fatt_emissione);
                        v_unitaDef.setProperties(v_unita_fatt_emissione, true);
                        //v_unitaDef.tab_contribuente = null;

                        v_avvisoDef.tab_unita_contribuzione.Add(v_unitaDef);
                        if (p_avvisoFattEmissione.id_tab_supervisione_finale.HasValue)
                        {
                            m_logger.LogMessage(String.Format("Rilevata supervisione(id:{0}). Creazione JOIN con ingiunzione: {1}", p_avvisoFattEmissione.id_tab_supervisione_finale.Value, v_unita_fatt_emissione.id_avv_pag_collegato.Value), EnLogSeverity.Debug);
                            TAB_JOIN_AVVCOA_INGFIS_V2 nuovaJoinAI = new TAB_JOIN_AVVCOA_INGFIS_V2()
                            {
                                ID_ENTE = p_avvisoFattEmissione.id_ente,
                                ID_INGIUNZIONE = v_unita_fatt_emissione.id_avv_pag_collegato.Value
                            };
                            v_avvisoDef.TAB_JOIN_AVVCOA_INGFIS_V2.Add(nuovaJoinAI);
                            //Aggiunta non necessaria al ctx => se lo porta dall'avviso creato...verifica
                        }

                    }
                    else if (
                        (avvColl.importoTotDaPagare - (v_unita_fatt_emissione.importo_unita_contribuzione + v_unita_fatt_emissione.importo_sanzioni_eliminate_eredi) < 1)
                        ||
                        ((v_unita_fatt_emissione.importo_unita_contribuzione + v_unita_fatt_emissione.importo_sanzioni_eliminate_eredi) - avvColl.importoTotDaPagare < 1)
                        )
                    {
                        m_logger.LogMessage(String.Format("Riferimento ad avviso collegato (id:{0}) con importi coincidenti e agevolazione {1}", v_unita_fatt_emissione.id_avv_pag_collegato, v_unita_fatt_emissione.importo_sanzioni_eliminate_eredi), EnLogSeverity.Debug);
                        avvColl.importo_sanzioni_eliminate_eredi = v_unita_fatt_emissione.importo_sanzioni_eliminate_eredi;
                        avvColl.importo_tot_da_pagare = v_unita_fatt_emissione.importo_unita_contribuzione;

                        //Consolida unità
                        //pag.38 INSERISCI_UNITA_CONSOLIDATA
                        m_logger.LogMessage(String.Format("Creazione unita contribuzione da avv fatt emissione {0}", v_unita_fatt_emissione.id_unita_contribuzione), EnLogSeverity.Debug);
                        //tab_unita_contribuzione v_unitaDef = TabUnitaContribuzioneBD.Consolida(v_unita_fatt_emissione);
                        tab_unita_contribuzione v_unitaDef = p_specificDB.tab_unita_contribuzione.Create(); // TabUnitaContribuzioneBD.Consolida(v_unita_fatt_emissione);
                        v_unitaDef.setProperties(v_unita_fatt_emissione, true);


                        v_unitaDef.tab_contribuente = null;
                        v_unitaDef.importo_sanzioni_eliminate_eredi = 0;

                        v_avvisoDef.tab_unita_contribuzione.Add(v_unitaDef);

                        if (p_avvisoFattEmissione.id_tab_supervisione_finale.HasValue)
                        {
                            m_logger.LogMessage(String.Format("Rilevata supervisione(id:{0}). Creazione JOIN con ingiunzione: {1}", p_avvisoFattEmissione.id_tab_supervisione_finale.Value, v_unita_fatt_emissione.id_avv_pag_collegato.Value), EnLogSeverity.Debug);
                            TAB_JOIN_AVVCOA_INGFIS_V2 nuovaJoinAI = new TAB_JOIN_AVVCOA_INGFIS_V2()
                            {
                                ID_ENTE = p_avvisoFattEmissione.id_ente,
                                ID_INGIUNZIONE = v_unita_fatt_emissione.id_avv_pag_collegato.Value
                            };
                            v_avvisoDef.TAB_JOIN_AVVCOA_INGFIS_V2.Add(nuovaJoinAI);
                            //Aggiunta non necessaria al ctx => se lo porta dall'avviso creato...verifica
                        }
                    }
                    else if (avvColl.importoTotDaPagare < v_unita_fatt_emissione.importo_unita_contribuzione)
                    {
                        m_logger.LogMessage(String.Format("Avviso {0} eliminato per anomalia importi rilevata su unita di contribuzione {1}", v_avvisoDef.id_anag_contribuente, v_unita_fatt_emissione.id_unita_contribuzione), EnLogSeverity.Warn);
                        throw new Exception("Avviso da eliminare: anomalia importi");
                    }
                    else if (avvColl.importoTotDaPagare - v_unita_fatt_emissione.importo_unita_contribuzione > 1 && v_unita_fatt_emissione.importo_sanzioni_eliminate_eredi == 0)
                    {
                        m_logger.LogMessage(String.Format("Avviso {0} eliminato per anomalia importi rilevata su unita di contribuzione {1}", v_avvisoDef.id_anag_contribuente, v_unita_fatt_emissione.id_unita_contribuzione), EnLogSeverity.Warn);
                        throw new Exception("Avviso da eliminare: anomalia importi");
                    }

                }
                else
                {
                    //pag.36 vai a inserisci_unita
                    m_logger.LogMessage(String.Format("Creazione unita contribuzione da avv fatt emissione {0}", v_unita_fatt_emissione.id_unita_contribuzione), EnLogSeverity.Debug);
                    //tab_unita_contribuzione v_unitaDef = TabUnitaContribuzioneBD.Consolida(v_unita_fatt_emissione);
                    tab_unita_contribuzione v_unitaDef = p_specificDB.tab_unita_contribuzione.Create(); // TabUnitaContribuzioneBD.Consolida(v_unita_fatt_emissione);
                    v_unitaDef.setProperties(v_unita_fatt_emissione, true);
                    v_unitaDef.tab_contribuente = null;

                    v_avvisoDef.tab_unita_contribuzione.Add(v_unitaDef);
                }
            }

            return v_avvisoDef;
        }

        /// <summary>
        /// Crea l'avviso consolidato con le relative unita di contribuzione
        /// </summary>
        /// <param name="p_avvisoFattEmissione"></param>
        /// <param name="p_identificativoAvvPag"></param>
        /// <param name="p_progressivoTipoAvvPag"></param>
        /// <param name="listeSpedNot"></param>
        /// <param name="p_specificDB"></param>
        /// <returns></returns>
        public static tab_avv_pag CreaAvviso_Rateizza(string tipo_esito, tab_avv_pag_fatt_emissione p_avvisoFattEmissione, string p_identificativoAvvPag, string p_progressivoTipoAvvPag, dbEnte p_specificDB)
        {//pag.2 
            m_logger.LogMessage(String.Format("--> Creazione avviso da avv fatt emissione {0}, con identificativo: {1} e numero: {2}", p_avvisoFattEmissione.id_tab_avv_pag, p_identificativoAvvPag, p_progressivoTipoAvvPag), EnLogSeverity.Debug);
            m_logger.LogMessage(String.Format("Creazione avviso da avv fatt emissione {0}, con identificativo: {1} e numero: {2}", p_avvisoFattEmissione.id_tab_avv_pag, p_identificativoAvvPag, p_progressivoTipoAvvPag), EnLogSeverity.Info);
            //Crea entity
            tab_avv_pag v_avvisoDef = p_specificDB.tab_avv_pag.Create(); // new tab_avv_pag();
            v_avvisoDef.setProperties(p_avvisoFattEmissione, true);
            v_avvisoDef.identificativo_avv_pag = p_identificativoAvvPag;
            v_avvisoDef.numero_avv_pag = p_progressivoTipoAvvPag;
            v_avvisoDef.tab_contribuente = null;
            v_avvisoDef.id_tab_avv_pag = 0;
            v_avvisoDef.anagrafica_stato = AnagraficaStatoAvvPagBD.GetById(v_avvisoDef.id_stato, p_specificDB);
            //l'avviso viene creato con cod_stato SSP-, una volta consolidato, il cod_stato deve essere messo a VAL-EME
            v_avvisoDef.cod_stato = anagrafica_stato_avv_pag.VAL_EME;
            v_avvisoDef.id_stato = anagrafica_stato_avv_pag.VAL_EME_ID;
            v_avvisoDef.cod_stato_avv_pag = anagrafica_stato_avv_pag.VAL_EME;
            v_avvisoDef.id_stato_avv_pag = anagrafica_stato_avv_pag.VAL_EME_ID;

            //potrebbe servire se non funziona
            //modifiche fatte nel consolidamento normale dopo il test e la verifica che il set properties non funzionava
            //v_avvisoDef.TAB_SUPERVISIONE_FINALE_V21 = p_avvisoFattEmissione.TAB_SUPERVISIONE_FINALE_V2;//pezza xkè la setproperties non funziona bene
            ////TODO Davide: DA TESTARE NUOVO METODO DI COPIA ENTITA'
            ////v_avvisoDef.setEntityProperties(p_avvisoFattEmissione, p_specificDB);
            //v_avvisoDef.identificativo_avv_pag = p_identificativoAvvPag;
            //v_avvisoDef.numero_avv_pag = p_progressivoTipoAvvPag;
            //v_avvisoDef.tab_contribuente = null;
            //v_avvisoDef.id_tab_avv_pag = 0;
            //v_avvisoDef.anagrafica_stato_avv_pag = AnagraficaStatoAvvPagBD.GetById(v_avvisoDef.id_stato_avv_pag, p_specificDB);//pezza xkè la setproperties non funziona bene          
            //fine
            //

            //VISTO CHE LA FUNZIONE ConsolidaAvviso_Rateizza è sempre chiamata indipendentemente daLL'ESITO DELL'ISTANZA, si effettua sempre il controllo  
            //if (tipo_esito == anagrafica_causale.SIGLA_ACC) prima di salvare.
            if (tipo_esito == anagrafica_causale.SIGLA_ACC)
            {
                p_specificDB.tab_avv_pag.Add(v_avvisoDef);
            }

            int id_ente = v_avvisoDef.id_ente;
            int id_ente_gestito = v_avvisoDef.id_ente_gestito;

            //SI CONTROLLA SEMPRE CHE L iSTANZA SIA STATA ACCOLTA perchè la funzione 
            if (tipo_esito == anagrafica_causale.SIGLA_ACC)
            {
                //MANCA DA PAG.2 a 6 fino a Consolida_unita_contribuzione

                //pag.6 Consolida_unita_contribuzione
                IQueryable<tab_unita_contribuzione_fatt_emissione> v_unita_fatt_emissioneList = p_avvisoFattEmissione.tab_unita_contribuzione_fatt_emissione.AsQueryable();
                m_logger.LogMessage(String.Format("num. unita contribuzione da consolidare: {0}", v_unita_fatt_emissioneList.Count()), EnLogSeverity.Debug);

                foreach (tab_unita_contribuzione_fatt_emissione v_unita_fatt_emissione in v_unita_fatt_emissioneList)
                {
                    m_logger.LogMessage(String.Format("--> --> Lavorazione unita contribuzione fatt emissione: {0}", v_unita_fatt_emissione.id_unita_contribuzione), EnLogSeverity.Debug);

                    //MIMMO:MANCA NEL DOCUMENTO
                    if (p_avvisoFattEmissione.id_tab_supervisione_finale.HasValue
                        &&
                        v_unita_fatt_emissione.id_avv_pag_collegato.HasValue)
                    {
                        m_logger.LogMessage(String.Format("Rilevata supervisione(id:{0}). Creazione JOIN con ingiunzione: {1}", p_avvisoFattEmissione.id_tab_supervisione_finale.Value, v_unita_fatt_emissione.id_avv_pag_collegato.Value), EnLogSeverity.Debug);
                        TAB_JOIN_AVVCOA_INGFIS_V2 nuovaJoinAI = new TAB_JOIN_AVVCOA_INGFIS_V2()
                        {
                            ID_ENTE = p_avvisoFattEmissione.id_ente,
                            ID_INGIUNZIONE = v_unita_fatt_emissione.id_avv_pag_collegato.Value
                        };
                        v_avvisoDef.TAB_JOIN_AVVCOA_INGFIS_V2.Add(nuovaJoinAI);
                        //Aggiunta non necessaria al ctx => se lo porta dall'avviso creato...verifica
                    }
                    //MIMMO:FINE MANCA NEL DOCUMENTO
                    //Se unita contribuzione è relativa ad avviso...nuova aggiunta
                    if (v_unita_fatt_emissione.id_avv_pag_collegato.HasValue)
                    {
                        tab_avv_pag avvColl = v_unita_fatt_emissione.tab_avv_pag;

                        if (
                            (v_unita_fatt_emissione.importo_sanzioni_eliminate_eredi == null || v_unita_fatt_emissione.importo_sanzioni_eliminate_eredi.Value == 0)
                            && (avvColl.importoTotDaPagare == v_unita_fatt_emissione.importo_unita_contribuzione
                                ||
                                avvColl.importoTotDaPagare - v_unita_fatt_emissione.importo_unita_contribuzione < 1
                                ||
                                v_unita_fatt_emissione.importo_unita_contribuzione - avvColl.importoTotDaPagare < 1
                                ))
                        {
                            m_logger.LogMessage(String.Format("Riferimento ad avviso collegato (id:{0}) con importi coincidenti e agevolazione {1}", v_unita_fatt_emissione.id_avv_pag_collegato, v_unita_fatt_emissione.importo_sanzioni_eliminate_eredi), EnLogSeverity.Debug);
                            //Aggiornamenti avviso collegato
                            //pag.6 MIMMO:QUESTO AGGIORNAMENTO DELL'AVVISO COLLEGATO NEL DOCUMENTO è EFFETTUATO SEMPRE E NON SOLO QUANDO SI 
                            //VERIFICA LA CONDIZIONE. BISOGNA VERIFCARE SE CORRETTO.
                            anagrafica_tipo_avv_pag anaTipoAvvConsolidato = AnagraficaTipoAvvPagBD.GetById(v_avvisoDef.id_tipo_avvpag, p_specificDB);

                            //Modifica fatta da Giulivo: non era possibile valorizzare avvColl.id_stato = 0 nel caso in cui anaTipoAvvConsolidato.id_stato_avvpag_collegati = null per ovvi motivi
                            //avvColl.id_stato = anaTipoAvvConsolidato.id_stato_avvpag_collegati.HasValue ? anaTipoAvvConsolidato.id_stato_avvpag_collegati.Value : 0;
                            //avvColl.cod_stato = anaTipoAvvConsolidato.cod_stato_avvpag_collegati;
                            avvColl.id_stato = anaTipoAvvConsolidato.id_stato_avvpag_collegati.HasValue ? anaTipoAvvConsolidato.id_stato_avvpag_collegati.Value : avvColl.id_stato;
                            avvColl.cod_stato = !string.IsNullOrEmpty(anaTipoAvvConsolidato.cod_stato_avvpag_collegati) ? anaTipoAvvConsolidato.cod_stato_avvpag_collegati : avvColl.cod_stato;

                            avvColl.flag_rateizzazione_bis = "1";

                            //ricopia le voci dell'unità di contribuzione anche sull'avviso (Ingiunzione) collagato
                            avvColl.sanzioni_eliminate_definizione_agevolata = v_unita_fatt_emissione.sanzioni_eliminate_definizione_agevolata;
                            avvColl.interessi_eliminati_definizione_agevolata = v_unita_fatt_emissione.interessi_eliminati_definizione_agevolata;



                            //Consolida unità
                            m_logger.LogMessage(String.Format("Creazione unita contribuzione da avv fatt emissione {0}", v_unita_fatt_emissione.id_unita_contribuzione), EnLogSeverity.Debug);
                            //pag.7 vai a INSERISCI UNITA CONTRIBUZIONE
                            //tab_unita_contribuzione v_unitaDef = TabUnitaContribuzioneBD.Consolida(v_unita_fatt_emissione);
                            tab_unita_contribuzione v_unitaDef = p_specificDB.tab_unita_contribuzione.Create(); // TabUnitaContribuzioneBD.Consolida(v_unita_fatt_emissione);
                            v_unitaDef.setProperties(v_unita_fatt_emissione, true);

                            v_unitaDef.tab_contribuente = null;

                            v_avvisoDef.tab_unita_contribuzione.Add(v_unitaDef);

                        }
                        //prima condizione pag.7
                        else if (
                            (avvColl.importoTotDaPagare - (v_unita_fatt_emissione.importo_unita_contribuzione + v_unita_fatt_emissione.importo_sanzioni_eliminate_eredi) < 1)
                            ||
                            ((v_unita_fatt_emissione.importo_unita_contribuzione + v_unita_fatt_emissione.importo_sanzioni_eliminate_eredi) - avvColl.importoTotDaPagare < 1)
                            )
                        {
                            m_logger.LogMessage(String.Format("Riferimento ad avviso collegato (id:{0}) con importi coincidenti e agevolazione {1}", v_unita_fatt_emissione.id_avv_pag_collegato, v_unita_fatt_emissione.importo_sanzioni_eliminate_eredi), EnLogSeverity.Debug);
                            avvColl.importo_sanzioni_eliminate_eredi = v_unita_fatt_emissione.importo_sanzioni_eliminate_eredi;//nel documento è importo_agevolazioni_1
                            avvColl.importo_tot_da_pagare = v_unita_fatt_emissione.importo_unita_contribuzione;

                            //pag.7 INSERISCI_UNITA_CONTRIBUZIONE 
                            m_logger.LogMessage(String.Format("Creazione unita contribuzione da avv fatt emissione {0}", v_unita_fatt_emissione.id_unita_contribuzione), EnLogSeverity.Debug);
                            //tab_unita_contribuzione v_unitaDef = TabUnitaContribuzioneBD.Consolida(v_unita_fatt_emissione);
                            tab_unita_contribuzione v_unitaDef = p_specificDB.tab_unita_contribuzione.Create(); // TabUnitaContribuzioneBD.Consolida(v_unita_fatt_emissione);
                            v_unitaDef.setProperties(v_unita_fatt_emissione, true);

                            v_unitaDef.tab_contribuente = null;
                            v_unitaDef.importo_sanzioni_eliminate_eredi = 0;

                            v_avvisoDef.tab_unita_contribuzione.Add(v_unitaDef);
                        }
                        //seconda condizione pag.7
                        else if (avvColl.importoTotDaPagare < v_unita_fatt_emissione.importo_unita_contribuzione)
                        {
                            m_logger.LogMessage(String.Format("Avviso {0} eliminato per anomalia importi rilevata su unita di contribuzione {1}", v_avvisoDef.id_anag_contribuente, v_unita_fatt_emissione.id_unita_contribuzione), EnLogSeverity.Warn);
                            throw new CollegatoVariatoCalcException("Avviso da eliminare: anomalia importi");
                        }
                        else if (avvColl.importoTotDaPagare - v_unita_fatt_emissione.importo_unita_contribuzione > 1 && v_unita_fatt_emissione.importo_sanzioni_eliminate_eredi == 0)
                        {
                            m_logger.LogMessage(String.Format("Avviso {0} eliminato per anomalia importi rilevata su unita di contribuzione {1}", v_avvisoDef.id_anag_contribuente, v_unita_fatt_emissione.id_unita_contribuzione), EnLogSeverity.Warn);
                            throw new CollegatoVariatoCalcException("Avviso da eliminare: anomalia importi");
                        }
                        //se l'avviso ha cambiato stato e non è più valido / rateizzato non si deve consolidate
                        else if (!avvColl.cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO) || (avvColl.flag_rateizzazione_bis != null && avvColl.flag_rateizzazione_bis.Equals("1"))
                                || avvColl.TAB_JOIN_AVVCOA_INGFIS_V21.Any(c => c.tab_avv_pag.flag_rateizzazione_bis != null && c.tab_avv_pag.flag_rateizzazione_bis.Equals("1") && !c.tab_avv_pag.cod_stato.Contains(CodStato.ANN)))
                        {
                            m_logger.LogMessage(String.Format("Avviso {0} in stato non valido o rateizzato su unita di contribuzione {1}", v_avvisoDef.id_anag_contribuente, v_unita_fatt_emissione.id_unita_contribuzione), EnLogSeverity.Warn);
                            throw new CollegatoVariatoCalcException("Avviso da eliminare: anomalia avviso collegato");
                        }
                    }
                    else
                    {//pag.6 se id_avv_pag_collegato =NULL INSERISCE SOLO L UNITA
                        m_logger.LogMessage(String.Format("Creazione unita contribuzione da avv fatt emissione {0}", v_unita_fatt_emissione.id_unita_contribuzione), EnLogSeverity.Debug);
                        //pag.7 INSERISCI_UNITA_CONTRIBUZIONE 
                        //tab_unita_contribuzione v_unitaDef = TabUnitaContribuzioneBD.Consolida(v_unita_fatt_emissione);
                        tab_unita_contribuzione v_unitaDef = p_specificDB.tab_unita_contribuzione.Create(); // TabUnitaContribuzioneBD.Consolida(v_unita_fatt_emissione);
                        v_unitaDef.setProperties(v_unita_fatt_emissione, true);

                        v_unitaDef.tab_contribuente = null;

                        v_avvisoDef.tab_unita_contribuzione.Add(v_unitaDef);
                    }
                }
            }

            #region "OLD: da eliminare dopo test"
            //if (v_avvisoDef.tab_liste.tab_tipo_lista.cod_lista != tab_tipo_lista.TIPOLISTA_TRASMISSIONE)
            //{
            //    List<tab_sped_not> nuoveTSN = creaSpedNotAvv(p_specificDB, v_avvisoDef, listeSpedNot);
            //    if (nuoveTSN != null && nuoveTSN.Count > 0)
            //        p_specificDB.tab_sped_not.AddRange(nuoveTSN);

            //    aggiornaAvviso(v_avvisoDef.id_tab_avv_pag, p_specificDB);

            //    DateTime oggi = DateTime.Now;

            //    if (nuoveTSN != null && nuoveTSN.Count > 0)
            //    {
            //        int tipo_sped_not = nuoveTSN.FirstOrDefault().tab_lista_sped_notifiche.id_tipo_spedizione_notifica;
            //        string sigla_sped_not = nuoveTSN.FirstOrDefault().tab_lista_sped_notifiche.anagrafica_tipo_spedizione_notifica.sigla_tipo_spedizione_notifica;

            //        anagrafica_voci_contribuzione ana_voce_contrib = AnagraficaVociContribuzioneBD.GetList(p_specificDB).Where(avc => avc.cod_tributo_ministeriale == sigla_sped_not).FirstOrDefault();

            //        tab_calcolo_tipo_voci_contribuzione calcolo_tvc = TabCalcoloTipoVociContribuzioneBD.GetList(p_specificDB).Where(tvc => tvc.id_ente == id_ente
            //            && tvc.id_ente_gestito == id_ente_gestito
            //            && tvc.id_anagrafica_voce_contribuzione == ana_voce_contrib.id_anagrafica_voce_contribuzione
            //            && tvc.modalita_calcolo == true
            //            && tvc.periodo_validita_da <= oggi
            //            && tvc.periodo_validita_a >= oggi
            //            && !tvc.cod_stato.Contains(CodStato.ANN))
            //            .FirstOrDefault();


            //        int numRigaToUse = v_avvisoDef.tab_unita_contribuzione.Max(uc => uc.num_riga_avv_pag_generato).HasValue ? v_avvisoDef.tab_unita_contribuzione.Max(uc => uc.num_riga_avv_pag_generato).Value + 1 : 1;

            //        tab_unita_contribuzione uc_spese_notifica = new tab_unita_contribuzione()
            //        {
            //            num_riga_avv_pag_generato = numRigaToUse,
            //            id_ente = id_ente,
            //            id_ente_gestito = id_ente_gestito,
            //            id_entrata = ana_voce_contrib.id_entrata,
            //            id_tipo_voce_contribuzione = ana_voce_contrib.id_tipo_voce_contribuzione,
            //            id_anagrafica_voce_contribuzione = ana_voce_contrib.id_anagrafica_voce_contribuzione,
            //            flag_tipo_addebito = tab_unita_contribuzione_fatt_emissione.FLAG_TIPO_ADDEBITO_NORMALE,
            //            flag_segno = tab_unita_contribuzione_fatt_emissione.FLAG_SEGNO_POSITVO,
            //            periodo_rif_da = oggi,
            //            periodo_rif_a = oggi,
            //            periodo_contribuzione_da = oggi,
            //            periodo_contribuzione_a = oggi,
            //            anno_rif = oggi.Year.ToString(),
            //            anno_origine = oggi.Year,
            //            um_unita = "€",
            //            quantita_unita_contribuzione = 1,
            //            cod_stato = CodStato.ATT_ATT,
            //            data_stato = oggi,
            //            importo_unita_contribuzione = calcolo_tvc.importo_fisso
            //        };

            //        v_avvisoDef.tab_unita_contribuzione.Add(uc_spese_notifica);

            //        //Arrotonda
            //        TabAvvPagBD.arrotondaAvvPag(v_avvisoDef, uc_spese_notifica.num_riga_avv_pag_generato.Value + 1);

            //        ReturnedObject obj = inizializzaInputCreaRate(v_avvisoDef, p_specificDB);

            //        TabAvvPagBD.creaRate(obj, 1, DateTime.Now, p_specificDB);
            //    }

            //}
            ////LISTA TRASMISSIONE
            //else
            //{
            //    IQueryable<tab_rata_avv_pag_fatt_emissione> v_rate_fatt_emissioneList = p_avvisoFattEmissione.tab_rata_avv_pag_fatt_emissione.AsQueryable();

            //    foreach (tab_rata_avv_pag_fatt_emissione v_rata_fatt_emissione in v_rate_fatt_emissioneList)
            //    {
            //        tab_rata_avv_pag v_rataDef = new tab_rata_avv_pag();
            //        v_rataDef.setProperties(v_rata_fatt_emissione);
            //        v_rataDef.tab_avv_pag = v_avvisoDef;
            //        v_avvisoDef.tab_rata_avv_pag.Add(v_rataDef);
            //    }
            //}
            #endregion

            return v_avvisoDef;
        }


        /// <summary>
        /// Crea l'avviso consolidato con le relative unita di contribuzione
        /// </summary>
        /// <param name="p_avvisoFattEmissione"></param>
        /// <param name="p_identificativoAvvPag"></param>
        /// <param name="p_progressivoTipoAvvPag"></param>
        /// <param name="listeSpedNot"></param>
        /// <param name="p_specificDB"></param>
        /// <returns></returns>
        public static tab_avv_pag CreaAvviso(tab_avv_pag_fatt_emissione p_avvisoFattEmissione, string p_identificativoAvvPag, string p_progressivoTipoAvvPag, dbEnte p_specificDB)
        {
            m_logger.LogMessage(String.Format("--> Creazione avviso da avv fatt emissione {0}, con identificativo: {1} e numero: {2}", p_avvisoFattEmissione.id_tab_avv_pag, p_identificativoAvvPag, p_progressivoTipoAvvPag), EnLogSeverity.Debug);
            m_logger.LogMessage(String.Format("Creazione avviso da avv fatt emissione {0}, con identificativo: {1} e numero: {2}", p_avvisoFattEmissione.id_tab_avv_pag, p_identificativoAvvPag, p_progressivoTipoAvvPag), EnLogSeverity.Info);
            //Crea entity                    
            tab_avv_pag v_avvisoDef = p_specificDB.tab_avv_pag.Create();
            v_avvisoDef.setProperties(p_avvisoFattEmissione, true);

            if (p_avvisoFattEmissione.TAB_SUPERVISIONE_FINALE_V2 != null)
            {
                p_avvisoFattEmissione.TAB_SUPERVISIONE_FINALE_V2.tab_avv_pag = v_avvisoDef;
                p_avvisoFattEmissione.TAB_SUPERVISIONE_FINALE_V2.ID_TIPO_AVVPAG_EMESSO = v_avvisoDef.id_tipo_avvpag;
            }

            v_avvisoDef.identificativo_avv_pag = p_identificativoAvvPag;
            v_avvisoDef.numero_avv_pag = p_progressivoTipoAvvPag;
            v_avvisoDef.tab_contribuente = null;
            v_avvisoDef.id_tab_avv_pag = 0;
            v_avvisoDef.anagrafica_stato = AnagraficaStatoAvvPagBD.GetById(v_avvisoDef.id_stato, p_specificDB);//pezza xkè la setproperties non funziona bene           
            int v_idListaEmissione = p_avvisoFattEmissione.id_lista_emissione.HasValue ? p_avvisoFattEmissione.id_lista_emissione.Value : 0;

            tab_validazione_approvazione_liste tval = TabValidazioneApprovazioneListeBD.GetValidForLista(v_idListaEmissione, p_specificDB);
            if (tval != null)
            {
                DateTime? v_scadenza = tval.data_scadenza_rata_1;
                if (tval.numero_rate.HasValue && tval.numero_rate.Value == 1)
                {
                    v_scadenza = tval.data_scadenza_rata_unica;
                }
                v_avvisoDef.num_rate = tval.numero_rate;
                v_avvisoDef.periodicita_rate = tval.periodicita_rate;
                v_avvisoDef.data_scadenza_1_rata = v_scadenza;
            }
            p_specificDB.tab_avv_pag.Add(v_avvisoDef);


            if (v_avvisoDef.anagrafica_tipo_avv_pag.id_servizio.Equals(anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO) ||
                   v_avvisoDef.anagrafica_tipo_avv_pag.id_tipo_avvpag.Equals(anagrafica_tipo_avv_pag.FERMO_AMMINISTRATIVO) ||
                   v_avvisoDef.anagrafica_tipo_avv_pag.id_tipo_avvpag.Equals(anagrafica_tipo_avv_pag.IPOTECA)
                  )
            {
                //verifico se è stato effettuato un pagamento sul preavviso o sull'ordine collegato all'avviso
                if (p_avvisoFattEmissione.TAB_SUPERVISIONE_FINALE_V2 != null)
                {
                    int v_idAvviso = p_avvisoFattEmissione.TAB_SUPERVISIONE_FINALE_V2.id_avvpag_preavviso_collegato.HasValue ?
                                     p_avvisoFattEmissione.TAB_SUPERVISIONE_FINALE_V2.id_avvpag_preavviso_collegato.Value : 0;

                    tab_avv_pag v_avviso = TabAvvPagBD.GetById(v_idAvviso, p_specificDB);
                    if (v_avviso.importoTotDaPagare == 0)
                    {
                        m_logger.LogMessage(String.Format("Avviso {0} non valido poichè è stato effettuato  o è in corso un pagamento sul collegato {1}", v_avvisoDef.id_anag_contribuente, v_avviso.id_tab_avv_pag), EnLogSeverity.Warn);
                        throw new CollegatoVariatoCalcException("Avviso da eliminare: anomalia avviso collegato (pagamento)");
                    }
                }
            }




            int id_ente = v_avvisoDef.id_ente;
            int id_ente_gestito = v_avvisoDef.id_ente_gestito;


            IQueryable<tab_unita_contribuzione_fatt_emissione> v_unita_fatt_emissioneList = p_avvisoFattEmissione.tab_unita_contribuzione_fatt_emissione.AsQueryable();
            m_logger.LogMessage(String.Format("num. unita contribuzione da consolidare: {0}", v_unita_fatt_emissioneList.Count()), EnLogSeverity.Debug);

            foreach (tab_unita_contribuzione_fatt_emissione v_unita_fatt_emissione in v_unita_fatt_emissioneList)
            {
                m_logger.LogMessage(String.Format("--> --> Lavorazione unita contribuzione fatt emissione: {0}", v_unita_fatt_emissione.id_unita_contribuzione), EnLogSeverity.Debug);

                if (v_unita_fatt_emissione.id_avv_pag_collegato.HasValue &&
                    p_avvisoFattEmissione.id_entrata.Equals(anagrafica_entrate.RISCOSSIONE_COATTIVA) &&
                    !p_avvisoFattEmissione.anagrafica_tipo_avv_pag.id_servizio.Equals(anagrafica_tipo_servizi.ING_FISC)
                    ) // il record viene creato solo per gli avvisi coattivi diversi dall'ingiunzione
                {
                    TAB_JOIN_AVVCOA_INGFIS_V2 nuovaJoinAI = new TAB_JOIN_AVVCOA_INGFIS_V2()
                    {
                        ID_ENTE = p_avvisoFattEmissione.id_ente,
                        ID_INGIUNZIONE = v_unita_fatt_emissione.id_avv_pag_collegato.Value
                    };
                    v_avvisoDef.TAB_JOIN_AVVCOA_INGFIS_V2.Add(nuovaJoinAI);
                    //Aggiunta non necessaria al ctx => se lo porta dall'avviso creato...verifica
                }


                //fine pag.38
                //Se unita contribuzione è relativa ad avviso...nuova aggiunta
                int SALVA_ID_UNITA_CONTRIBUZIONE_PRINCIPALE_FATT_EMISSIONE = 0;

                tab_unita_contribuzione v_unitaPrincipaleDef = null;
                if (v_unita_fatt_emissione.id_avv_pag_collegato.HasValue)
                {
                    SALVA_ID_UNITA_CONTRIBUZIONE_PRINCIPALE_FATT_EMISSIONE = v_unita_fatt_emissione.id_unita_contribuzione;
                    tab_avv_pag avvColl = v_unita_fatt_emissione.tab_avv_pag;
                    if (
                        (avvColl.importoTotDaPagare == v_unita_fatt_emissione.importo_unita_contribuzione
                            ||
                            avvColl.importoTotDaPagare - v_unita_fatt_emissione.importo_unita_contribuzione < 1
                            ||
                            v_unita_fatt_emissione.importo_unita_contribuzione - avvColl.importoTotDaPagare < 1
                            ))
                    {
                        m_logger.LogMessage(String.Format("Riferimento ad avviso collegato (id:{0}) con importi coincidenti e agevolazione {1}", v_unita_fatt_emissione.id_avv_pag_collegato, v_unita_fatt_emissione.importo_sanzioni_eliminate_eredi), EnLogSeverity.Debug);
                        //Aggiornamenti avviso collegato(non presente nel documento)
                        anagrafica_tipo_avv_pag anaTipoAvvConsolidato = AnagraficaTipoAvvPagBD.GetById(v_avvisoDef.id_tipo_avvpag, p_specificDB);
                        if (anaTipoAvvConsolidato != null && anaTipoAvvConsolidato.id_stato_avvpag_collegati.HasValue && !String.IsNullOrWhiteSpace(anaTipoAvvConsolidato.cod_stato_avvpag_collegati))
                        {
                            avvColl.id_stato = anaTipoAvvConsolidato.id_stato_avvpag_collegati.HasValue ? anaTipoAvvConsolidato.id_stato_avvpag_collegati.Value : 0;
                            avvColl.cod_stato = anaTipoAvvConsolidato.cod_stato_avvpag_collegati;
                        }
                        else
                            m_logger.LogMessage("Anagrafica Tipo Avviso non contiene info su stato avvpag collegato: stati non modificati", EnLogSeverity.Debug);
                        //fine Aggiornamenti avviso collegato(non presente nel documento)
                        //pag.37 INSERISCI_UNITà_CONSOLIDATA
                        m_logger.LogMessage(String.Format("Creazione unita contribuzione da avv fatt emissione {0}", v_unita_fatt_emissione.id_unita_contribuzione), EnLogSeverity.Debug);
                        //tab_unita_contribuzione v_unitaDef = TabUnitaContribuzioneBD.Consolida(v_unita_fatt_emissione);
                        v_unitaPrincipaleDef = p_specificDB.tab_unita_contribuzione.Create(); // TabUnitaContribuzioneBD.Consolida(v_unita_fatt_emissione);
                        v_unitaPrincipaleDef.setProperties(v_unita_fatt_emissione, true);

                        v_unitaPrincipaleDef.tab_tipo_voce_contribuzione = TabTipoVoceContribuzioneBD.GetList(p_specificDB).FirstOrDefault(tv => tv.id_tipo_voce_contribuzione == v_unitaPrincipaleDef.id_tipo_voce_contribuzione);
                        //Generato 
                        v_unitaPrincipaleDef.tab_avv_pag = null;
                        //Collegati
                        v_unitaPrincipaleDef.tab_avv_pag1 = v_unita_fatt_emissione.tab_avv_pag;
                        //Origine
                        v_unitaPrincipaleDef.tab_avv_pag2 = v_unita_fatt_emissione.tab_avv_pag1;
                        v_unitaPrincipaleDef.tab_contribuente = null;

                        v_avvisoDef.tab_unita_contribuzione.Add(v_unitaPrincipaleDef);
                        //fine pag.37 INSERISCI_UNITà_CONSOLIDATA
                    }
                    //secomnda condizione pag.37
                    //Commentato perchè inglobato nella funzione di eliminazione eredi 15-09-2017
                    else if (
                        (avvColl.importoTotDaPagare - (v_unita_fatt_emissione.importo_unita_contribuzione + v_unita_fatt_emissione.importo_sanzioni_eliminate_eredi) < 1)
                        ||
                        ((v_unita_fatt_emissione.importo_unita_contribuzione + v_unita_fatt_emissione.importo_sanzioni_eliminate_eredi) - avvColl.importoTotDaPagare < 1)
                        )
                    {
                        m_logger.LogMessage(String.Format("Riferimento ad avviso collegato (id:{0}) con importi coincidenti e agevolazione {1}", v_unita_fatt_emissione.id_avv_pag_collegato, v_unita_fatt_emissione.importo_ridotto), EnLogSeverity.Debug);

                        //Aggiornaento avviso collegato
                        avvColl.importo_sanzioni_eliminate_eredi = v_unita_fatt_emissione.importo_sanzioni_eliminate_eredi;
                        avvColl.importo_tot_da_pagare = v_unita_fatt_emissione.importo_unita_contribuzione;

                        //pag.37 INSERISCI_UNITà_CONSOLIDATA
                        m_logger.LogMessage(String.Format("Creazione unita contribuzione da avv fatt emissione {0}", v_unita_fatt_emissione.id_unita_contribuzione), EnLogSeverity.Debug);
                        //tab_unita_contribuzione v_unitaDef = TabUnitaContribuzioneBD.Consolida(v_unita_fatt_emissione);
                        v_unitaPrincipaleDef = p_specificDB.tab_unita_contribuzione.Create(); // TabUnitaContribuzioneBD.Consolida(v_unita_fatt_emissione);
                        v_unitaPrincipaleDef.setProperties(v_unita_fatt_emissione, true);

                        v_unitaPrincipaleDef.tab_tipo_voce_contribuzione = TabTipoVoceContribuzioneBD.GetList(p_specificDB).FirstOrDefault(tv => tv.id_tipo_voce_contribuzione == v_unitaPrincipaleDef.id_tipo_voce_contribuzione);
                        //Generato 
                        v_unitaPrincipaleDef.tab_avv_pag = null;
                        //Collegati
                        v_unitaPrincipaleDef.tab_avv_pag1 = v_unita_fatt_emissione.tab_avv_pag;
                        //Origine
                        v_unitaPrincipaleDef.tab_avv_pag2 = v_unita_fatt_emissione.tab_avv_pag1;
                        v_unitaPrincipaleDef.tab_contribuente = null;
                        v_unitaPrincipaleDef.importo_sanzioni_eliminate_eredi = 0;

                        v_avvisoDef.tab_unita_contribuzione.Add(v_unitaPrincipaleDef);
                        //fine pag.37 INSERISCI_UNITà_CONSOLIDATA
                    }
                    //terzo condizione pag.37
                    else if (avvColl.importoTotDaPagare < v_unita_fatt_emissione.importo_unita_contribuzione)
                    {
                        m_logger.LogMessage(String.Format("Avviso {0} eliminato per anomalia importi rilevata su unita di contribuzione {1}", v_avvisoDef.id_anag_contribuente, v_unita_fatt_emissione.id_unita_contribuzione), EnLogSeverity.Warn);
                        throw new CollegatoVariatoCalcException("Avviso da eliminare: anomalia importi");
                    }
                    //quarto condizione pag.37
                    else if (avvColl.importoTotDaPagare - v_unita_fatt_emissione.importo_unita_contribuzione > 1 && v_unita_fatt_emissione.importo_sanzioni_eliminate_eredi == 0)
                    {
                        m_logger.LogMessage(String.Format("Avviso {0} eliminato per anomalia importi rilevata su unita di contribuzione {1}", v_avvisoDef.id_anag_contribuente, v_unita_fatt_emissione.id_unita_contribuzione), EnLogSeverity.Warn);
                        throw new CollegatoVariatoCalcException("Avviso da eliminare: anomalia importi");
                    }
                    //se l'avviso ha cambiato stato e non è più valido / rateizzato non si deve consolidate
                    else if (!avvColl.cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO) || (avvColl.flag_rateizzazione_bis != null && avvColl.flag_rateizzazione_bis.Equals("1"))
                            || avvColl.TAB_JOIN_AVVCOA_INGFIS_V21.Any(c => c.tab_avv_pag.flag_rateizzazione_bis != null && c.tab_avv_pag.flag_rateizzazione_bis.Equals("1") && !c.tab_avv_pag.cod_stato.Contains(CodStato.ANN)))
                    {
                        m_logger.LogMessage(String.Format("Avviso {0} in stato non valido o rateizzato su unita di contribuzione {1}", v_avvisoDef.id_anag_contribuente, v_unita_fatt_emissione.id_unita_contribuzione), EnLogSeverity.Warn);
                        throw new CollegatoVariatoCalcException("Avviso da eliminare: anomalia avviso collegato");
                    }
                    else if (JoinTabCarrelloTabRateBD.GetList(p_specificDB)
                                    .Any(c => c.tab_rata_avv_pag.id_tab_avv_pag == avvColl.id_tab_avv_pag
                                           && !c.tab_carrello.cod_stato.StartsWith(tab_carrello.ANN)))
                    {
                        //si controlla se è stato effettuato o è in corso un pagamento pagopa sull'avviso collegato
                        m_logger.LogMessage(String.Format("Avviso {0} non valido poichè è stato effettuato  o è in corso un pagamento pagopa sul collegato {1}", v_avvisoDef.id_anag_contribuente, avvColl.id_tab_avv_pag), EnLogSeverity.Warn);
                        throw new CollegatoVariatoCalcException("Avviso da eliminare: anomalia avviso collegato (pagamento pago PA)");
                    }
                }
                else//pag.36 se id avviso collegato è null
                {
                    //pag.37 INSERISCI_UNITà_CONSOLIDATA
                    m_logger.LogMessage(String.Format("Creazione unita contribuzione da avv fatt emissione {0}", v_unita_fatt_emissione.id_unita_contribuzione), EnLogSeverity.Debug);
                    //tab_unita_contribuzione v_unitaDef = TabUnitaContribuzioneBD.Consolida(v_unita_fatt_emissione);
                    tab_unita_contribuzione v_unitaDef = p_specificDB.tab_unita_contribuzione.Create(); // TabUnitaContribuzioneBD.Consolida(v_unita_fatt_emissione);
                    v_unitaDef.setProperties(v_unita_fatt_emissione, true);
                    v_unitaDef.tab_tipo_voce_contribuzione = TabTipoVoceContribuzioneBD.GetList(p_specificDB).FirstOrDefault(tv => tv.id_tipo_voce_contribuzione == v_unitaDef.id_tipo_voce_contribuzione);
                    //Generato 
                    v_unitaDef.tab_avv_pag = null;
                    //Collegati
                    v_unitaDef.tab_avv_pag1 = v_unita_fatt_emissione.tab_avv_pag;
                    //Origine
                    v_unitaDef.tab_avv_pag2 = v_unita_fatt_emissione.tab_avv_pag1;
                    v_unitaDef.id_unita_contribuzione = 0;
                    v_unitaDef.id_avv_pag_generato = 0;

                    if (v_unita_fatt_emissione.id_unita_contribuzione_collegato == SALVA_ID_UNITA_CONTRIBUZIONE_PRINCIPALE_FATT_EMISSIONE)
                    {
                        v_unitaDef.id_unita_contribuzione_collegato = v_unitaPrincipaleDef.id_unita_contribuzione;
                    }

                    v_avvisoDef.tab_unita_contribuzione.Add(v_unitaDef);
                }
                //fine pag.37 INSERISCI_UNITà_CONSOLIDATA

            }

            /*
             * commentato in data 18/02/2021 perchè ha cambiato lo stato agli oggetti_contribuzione RET dopo il calcolo simulato
             * 
            //Cambia stato in ATT-ATT a oggetti di riferimento x le unita contribuzione consolidate
            IQueryable<decimal> oggettiIds = v_unita_fatt_emissioneList.Where(ufe => ufe.id_oggetto.HasValue).Select(ufe => ufe.id_oggetto.Value).Distinct();
            IQueryable<decimal> oggettiContribIds = v_unita_fatt_emissioneList.Where(ufe => ufe.id_oggetto_contribuzione.HasValue).Select(ufe => ufe.id_oggetto_contribuzione.Value).Distinct();

            TabOggettiBD.GetList(p_specificDB).Where(ogg => ogg.cod_stato_oggetto != tab_oggetti.ATT_ATT && oggettiIds.Contains(ogg.id_oggetto)).ToList().ForEach(ogg =>
            {
                ogg.cod_stato_oggetto = tab_oggetti.ATT_ATT;
                ogg.id_stato_oggetto = tab_oggetti.ATT_ATT_ID;
            });
            TabOggettiContribuzioneBD.GetList(p_specificDB).Where(oggC => oggC.cod_stato_oggetto != tab_oggetti_contribuzione.ATT_ATT && oggettiContribIds.Contains(oggC.id_oggetto_contribuzione)).ToList().ForEach(oggC =>
            {
                if (oggC.data_fine_contribuzione != null)
                {
                    oggC.cod_stato_oggetto = tab_oggetti_contribuzione.ATT_CES;
                    oggC.id_stato_oggetto = tab_oggetti_contribuzione.ATT_CES_ID;
                }
                else
                {
                    oggC.cod_stato_oggetto = tab_oggetti_contribuzione.ATT_ATT;
                    oggC.id_stato_oggetto = tab_oggetti_contribuzione.ATT_ATT_ID;
                }
            });
            */

            #region "OLD: da eliminare dopo test"
            //if (v_avvisoDef.tab_liste.tab_tipo_lista.cod_lista != tab_tipo_lista.TIPOLISTA_TRASMISSIONE)
            //{
            //    List<tab_sped_not> nuoveTSN = creaSpedNotAvv(p_specificDB, v_avvisoDef, listeSpedNot);
            //    if (nuoveTSN != null && nuoveTSN.Count > 0)
            //        p_specificDB.tab_sped_not.AddRange(nuoveTSN);

            //    aggiornaAvviso(v_avvisoDef.id_tab_avv_pag, p_specificDB);

            //    DateTime oggi = DateTime.Now;

            //    if (nuoveTSN != null && nuoveTSN.Count > 0)
            //    {
            //        int tipo_sped_not = nuoveTSN.FirstOrDefault().tab_lista_sped_notifiche.id_tipo_spedizione_notifica;
            //        string sigla_sped_not = nuoveTSN.FirstOrDefault().tab_lista_sped_notifiche.anagrafica_tipo_spedizione_notifica.sigla_tipo_spedizione_notifica;

            //        anagrafica_voci_contribuzione ana_voce_contrib = AnagraficaVociContribuzioneBD.GetList(p_specificDB).Where(avc => avc.cod_tributo_ministeriale == sigla_sped_not).FirstOrDefault();

            //        tab_calcolo_tipo_voci_contribuzione calcolo_tvc = TabCalcoloTipoVociContribuzioneBD.GetList(p_specificDB).Where(tvc => tvc.id_ente == id_ente
            //            && tvc.id_ente_gestito == id_ente_gestito
            //            && tvc.id_anagrafica_voce_contribuzione == ana_voce_contrib.id_anagrafica_voce_contribuzione
            //            && tvc.modalita_calcolo == true
            //            && tvc.periodo_validita_da <= oggi
            //            && tvc.periodo_validita_a >= oggi
            //            && !tvc.cod_stato.Contains(CodStato.ANN))
            //            .FirstOrDefault();


            //        int numRigaToUse = v_avvisoDef.tab_unita_contribuzione.Max(uc => uc.num_riga_avv_pag_generato).HasValue ? v_avvisoDef.tab_unita_contribuzione.Max(uc => uc.num_riga_avv_pag_generato).Value + 1 : 1;

            //        tab_unita_contribuzione uc_spese_notifica = new tab_unita_contribuzione()
            //        {
            //            num_riga_avv_pag_generato = numRigaToUse,
            //            id_ente = id_ente,
            //            id_ente_gestito = id_ente_gestito,
            //            id_entrata = ana_voce_contrib.id_entrata,
            //            id_tipo_voce_contribuzione = ana_voce_contrib.id_tipo_voce_contribuzione,
            //            id_anagrafica_voce_contribuzione = ana_voce_contrib.id_anagrafica_voce_contribuzione,
            //            flag_tipo_addebito = tab_unita_contribuzione_fatt_emissione.FLAG_TIPO_ADDEBITO_NORMALE,
            //            flag_segno = tab_unita_contribuzione_fatt_emissione.FLAG_SEGNO_POSITVO,
            //            periodo_rif_da = oggi,
            //            periodo_rif_a = oggi,
            //            periodo_contribuzione_da = oggi,
            //            periodo_contribuzione_a = oggi,
            //            anno_rif = oggi.Year.ToString(),
            //            anno_origine = oggi.Year,
            //            um_unita = "€",
            //            quantita_unita_contribuzione = 1,
            //            cod_stato = CodStato.ATT_ATT,
            //            data_stato = oggi,
            //            importo_unita_contribuzione = calcolo_tvc.importo_fisso
            //        };

            //        v_avvisoDef.tab_unita_contribuzione.Add(uc_spese_notifica);

            //        //Arrotonda
            //        TabAvvPagBD.arrotondaAvvPag(v_avvisoDef, uc_spese_notifica.num_riga_avv_pag_generato.Value + 1);

            //        ReturnedObject obj = inizializzaInputCreaRate(v_avvisoDef, p_specificDB);

            //        TabAvvPagBD.creaRate(obj, 1, DateTime.Now, p_specificDB);
            //    }

            //}
            ////LISTA TRASMISSIONE
            //else
            //{
            //    IQueryable<tab_rata_avv_pag_fatt_emissione> v_rate_fatt_emissioneList = p_avvisoFattEmissione.tab_rata_avv_pag_fatt_emissione.AsQueryable();

            //    foreach (tab_rata_avv_pag_fatt_emissione v_rata_fatt_emissione in v_rate_fatt_emissioneList)
            //    {
            //        tab_rata_avv_pag v_rataDef = new tab_rata_avv_pag();
            //        v_rataDef.setProperties(v_rata_fatt_emissione);
            //        v_rataDef.tab_avv_pag = v_avvisoDef;
            //        v_avvisoDef.tab_rata_avv_pag.Add(v_rataDef);
            //    }
            //}
            #endregion

            return v_avvisoDef;
        }

        /// <summary>
        /// Restituisce la lista di tutte le entità
        /// </summary>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <param name="p_includeEntities">Elenco di tabelle collegate da includere durante la select</param>
        /// <returns></returns>
        public static new IQueryable<tab_avv_pag> GetList(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            /// Ridefinisce la GetList per implementare la sicurezza di accesso sul contribuente
            return GetListInternal(p_dbContext, p_includeEntities).Where(d => p_dbContext.idContribuenteDefaultList.Count == 0 || p_dbContext.idContribuenteDefaultList.Contains(d.id_anag_contribuente));
        }

        /// <summary>
        /// Restituisce l'entità a partire dalla chiave primaria
        /// </summary>
        /// <param name="p_id">Chiave primaria</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static new tab_avv_pag GetById(Int32 p_id, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c => c.id_tab_avv_pag == p_id);
        }
        /// <summary>
        /// Restituisce tutti gli avvisi appartenenti all'elenco di ID Entrate indicato.
        /// L'elenco è ordinato per ID Entrata, ID Tipo Avviso e Data Emissione
        /// </summary>
        /// <param name="p_idEntrateList">Elenco di ID Entrate</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_avv_pag> GetListByIdEntrata(List<int> p_idEntrateList, dbEnte p_dbContext)
        {
            m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            return GetList(p_dbContext).Where(d => p_idEntrateList.Contains(d.id_entrata))
                                       .OrderBy(d => d.id_entrata).ThenBy(d => d.id_tipo_avvpag).ThenBy(d => d.dt_emissione);
        }
        /// <summary>
        /// Restituisce tutti gli avvisi di un contribuente      
        /// </summary>
        /// <param name="p_idContribuente">ID Contribuente ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_avv_pag> GetListByIdContribuente(decimal p_idContribuente, dbEnte p_dbContext)
        {
            m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            return GetList(p_dbContext).Where(d => d.id_anag_contribuente == p_idContribuente);
        }
        /// <summary>
        /// Restituisce tutti gli avvisi appartenenti all'elenco di ID Entrate, di un contribuente che superano il valore MIN_IMPORTO_DA_PAGARE
        /// L'elenco è ordinato per ID Entrata, ID Tipo Avviso e Data Emissione
        /// </summary>
        /// <param name="p_idEntrateList">Elenco di ID Entrate</param>
        /// <param name="p_idContribuente">ID Contribuente ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_avv_pag> GetListByIdEntrataIdContribuente(List<int> p_idEntrateList, int p_IdContribuente, string p_stato, dbEnte p_dbContext)
        {
            m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            return GetList(p_dbContext).Where(d => p_idEntrateList.Contains(d.id_entrata) && d.id_anag_contribuente == p_IdContribuente
                                              && d.anagrafica_stato.cod_stato_riferimento.Contains(p_stato) && d.importo_tot_da_pagare >= tab_avv_pag.MIN_IMPORTO_DA_PAGARE)
                                       .OrderBy(d => d.id_entrata).ThenBy(d => d.id_tipo_avvpag).ThenBy(d => d.dt_emissione);
        }

        public static IQueryable<tab_avv_pag> GetListByIdEntrataIdTipoAvvPagIdContribuenteStatoSSPANNDAR(int p_idEntrata, int p_idTipoAvvPag, Decimal p_idContribuente, dbEnte p_dbContext)
        {
            m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            return GetList(p_dbContext).Where(d => d.id_entrata == p_idEntrata && d.id_tipo_avvpag == p_idTipoAvvPag
                                                && d.id_anag_contribuente == p_idContribuente
                                                && (d.anagrafica_stato.cod_stato_riferimento.Equals(anagrafica_stato_avv_pag.SOSPESO_ISTANZA) || d.anagrafica_stato.cod_stato_riferimento.Contains(anagrafica_stato_avv_pag.ANNULLATO) || d.anagrafica_stato.cod_stato_riferimento.Contains(anagrafica_stato_avv_pag.DARETTIFICARE))
                                                && d.importo_tot_da_pagare >= tab_avv_pag.MIN_IMPORTO_DA_PAGARE)
                                       .OrderBy(d => d.dt_emissione);
        }

        /// <summary>
        /// Restituisce tutti gli avvisi che hanno ID Tipo Avviso, Anno Riferimento e Numero indicati
        /// </summary>
        /// <param name="p_tipoAvviso">ID Tipo Avviso ricercato</param>
        /// <param name="p_annoRiferimento">Anno di riferimento</param>
        /// <param name="p_numero">Numero progressivo dell'avviso</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_avv_pag> GetListByTipoAvvisoAnnoNumeroProgressivo(int p_tipoAvviso, string p_annoRiferimento, string p_numero, dbEnte p_dbContext)
        {
            m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            return p_dbContext.tab_avv_pag.Where(d => d.id_tipo_avvpag == p_tipoAvviso && d.anno_riferimento.Equals(p_annoRiferimento) && d.numero_avv_pag.Equals(p_numero));
        }

        /// <summary>
        /// Restituisce la lista degli avvisi appartenenti ad una lista
        /// </summary>
        /// <param name="p_idLista">ID Lista ricercata</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_avv_pag> GetListByIdLista(int p_idLista, dbEnte p_dbContext)
        {
            m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            return GetList(p_dbContext).Where(l => l.id_lista_emissione != null && l.id_lista_emissione.Equals(p_idLista))
                                       .OrderBy(o => o.id_tab_avv_pag);
        }


        /// <summary>
        /// Restituisce la lista degli avvisi appartenenti per l' idContribuente, l'idEntrata, idTipoAvviso ed annoRif
        /// </summary>
        /// <param name="p_idContribuente">Id Contribuente</param>
        /// <param name="p_idEntrata">Id Entrata</param>
        /// <param name="p_idTipoAvvPag">Id Tipo Avviso</param>
        /// <param name="p_annoRiferimento">Anno di riferimento</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static IQueryable<tab_avv_pag> GetListAvvisiFatturati(decimal p_idContribuente, int p_idEntrata, int? p_idTipoAvvPag, string p_annoRiferimento, dbEnte p_dbContext)
        {
            //m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);
            IQueryable<tab_avv_pag> retval =
            GetList(p_dbContext).Where(avv => avv.id_anag_contribuente.Equals(p_idContribuente))
                                                   .Where(avv => avv.id_entrata.Equals(p_idEntrata))
                                                   .Where(avv => avv.cod_stato.Contains(anagrafica_stato_avv_pag.VALIDO) ||
                                                                 avv.cod_stato.Contains(anagrafica_stato_avv_pag.SOSPESO) ||
                                                                 avv.cod_stato.Contains(anagrafica_stato_avv_pag.DARETTIFICARE))
                                                   .Where(avv => avv.tab_unita_contribuzione.Any(ar => ar.anno_rif == p_annoRiferimento && ar.id_entrata == p_idEntrata && ar.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Contains(tab_tipo_voce_contribuzione.CODICE_ENT)))
                                                   .Where(avv => !avv.join_tab_avv_pag_tab_doc_input.Any(jadi => jadi.cod_stato == anagrafica_stato_doc.STATO_DEF_ACCOLTA))
                                                   .Where(avv => !p_idTipoAvvPag.HasValue || avv.id_tipo_avvpag == p_idTipoAvvPag.Value);

            return retval;
        }


        public static IQueryable<tab_avv_pag> GetListAvvisiFatturatiSuppl(decimal p_idContribuente, int p_idEntrata, decimal p_idOggetto, decimal? p_quantitaOggContrib, int? p_idToponimo,
                                                                          DateTime? p_DataInizioOggContrib, DateTime? p_DataFineOggContrib, string p_annoRiferimento, dbEnte p_dbContext)
        {
            //m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);
            IQueryable<tab_avv_pag> retval;

            retval = GetList(p_dbContext).Where(avv => avv.id_anag_contribuente.Equals(p_idContribuente))
                                         .Where(avv => avv.id_entrata.Equals(p_idEntrata))
                                         .Where(avv => avv.cod_stato.Contains(anagrafica_stato_avv_pag.DARETTIFICARE) ||
                                                       avv.cod_stato.Contains(anagrafica_stato_avv_pag.SOSPESO))
                                         .Where(avv => avv.tab_unita_contribuzione.Any(ar => ar.anno_rif == p_annoRiferimento && ar.id_entrata == p_idEntrata))
                                         .Where(avv => avv.tab_unita_contribuzione.Any(unita => unita.id_oggetto == p_idOggetto));

            if (!retval.Any())
            {

                retval = GetList(p_dbContext).Where(avv => avv.id_anag_contribuente.Equals(p_idContribuente))
                                             .Where(avv => avv.id_entrata.Equals(p_idEntrata))
                                             .Where(avv => avv.tab_unita_contribuzione.Any(ar => ar.anno_rif == p_annoRiferimento && ar.id_entrata == p_idEntrata))
                                             .Where(avv => avv.tab_unita_contribuzione.Any(unita => unita.id_anagrafica_voce_contribuzione == anagrafica_voci_contribuzione.TARI_IMPOSTA &&
                                                                                           unita.id_oggetto == p_idOggetto &&
                                                                                           (
                                                                                                unita.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.VALIDO) ||
                                                                                                unita.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.SOSPESO)
                                                                                           ) &&
                                                                                           unita.quantita_unita_contribuzione == p_quantitaOggContrib.Value
                                                                                           &&
                                                                                           avv.cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO)));

            }

            if (!retval.Any())
            {
                DateTime DtFineContrib = DateTime.Parse("31/12/" + p_annoRiferimento);

                retval = GetList(p_dbContext).Where(avv => avv.id_anag_contribuente.Equals(p_idContribuente))
                                             .Where(avv => avv.id_entrata.Equals(p_idEntrata))
                                             .Where(avv => avv.tab_unita_contribuzione.Any(unita => unita.tab_oggetti.id_toponimo == p_idToponimo))
                                             .Where(avv => avv.tab_unita_contribuzione.Any(ar => ar.anno_rif == p_annoRiferimento && ar.id_entrata == p_idEntrata))
                                             .Where(avv => avv.tab_unita_contribuzione.Any(unita => unita.id_oggetto == p_idOggetto &&
                                                                                          (
                                                                                             unita.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.SOSPESO) ||
                                                                                             (
                                                                                                    unita.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.VALIDO) &&
                                                                                                    unita.quantita_unita_contribuzione == p_quantitaOggContrib.Value &&
                                                                                                    avv.cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO)
                                                                                             )
                                                                                           )))
                                             .Where(avv => avv.tab_unita_contribuzione.Any(unita => unita.periodo_contribuzione_da == p_DataInizioOggContrib &&
                                                                                                    p_DataFineOggContrib.HasValue ?
                                                                                                    unita.periodo_contribuzione_a == p_DataFineOggContrib :
                                                                                                    unita.periodo_contribuzione_a == DtFineContrib));
            }

            return retval;
        }

        public static IQueryable<tab_avv_pag> GetListAOPByListaEmissione(IQueryable<int> v_idsListaEmissione, dbEnte p_context)
        {
            m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            DateTime v_dataScadenza = DateTime.Now.AddDays(60);
            IQueryable<tab_avv_pag> risp = GetList(p_context).Where(a => v_idsListaEmissione.Contains(a.id_lista_emissione.Value))
                               .Where(a => a.anagrafica_stato.flag_validita.Equals("1"))
                               .Where(a => (a.anagrafica_tipo_avv_pag.flag_notifica == anagrafica_tipo_avv_pag.FLAG_NOTIFICA_NO && a.flag_esito_sped_notifica.Equals("1")) || a.anagrafica_tipo_avv_pag.flag_notifica == anagrafica_tipo_avv_pag.FLAG_NOTIFICA_SI)
                               .WhereByImportoDaPagare(1)
                               .Where(a => a.tab_rata_avv_pag.Any(r => r.cod_stato.Substring(1, 4) != CodStato.ANN &&
                                        r.num_rata == a.tab_rata_avv_pag.Where(rr => !rr.cod_stato.StartsWith(CodStato.ANN)).Max(rr => rr.num_rata) &&
                                        r.dt_scadenza_rata > v_dataScadenza))
                                        .OrderBy(a => a.dt_emissione);

            return risp;
        }

        public static IQueryable<tab_avv_pag> GetAvvisoPOPElaborato(int p_idAvvPag, dbEnte p_context)
        {

            IQueryable<tab_avv_pag> risp = GetList(p_context).Where(avv => avv.num_avv_riemesso == p_idAvvPag)
                                                             .Where(avv => !avv.cod_stato.StartsWith(CodStato.ANN));

            return risp;
        }

        public static IQueryable<tab_avv_pag> GetListAOPByListaTipiAvviso(IQueryable<int> v_idsListaTipiAvviso, dbEnte p_context)
        {
            m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            DateTime v_dataScadenza = DateTime.Now.AddDays(60);
            IQueryable<tab_avv_pag> risp = GetList(p_context).Where(a => v_idsListaTipiAvviso.Contains(a.id_tipo_avvpag))
                                                             .Where(a => a.anagrafica_stato.flag_validita.Equals("1"))
                                                             .Where(a => a.anagrafica_tipo_avv_pag.flag_notifica == anagrafica_tipo_avv_pag.FLAG_NOTIFICA_NO || (a.flag_esito_sped_notifica.Equals("1") && a.anagrafica_tipo_avv_pag.flag_notifica == anagrafica_tipo_avv_pag.FLAG_NOTIFICA_SI))
                                                             .WhereByImportoDaPagare(1)
                                  .Where(a => a.tab_rata_avv_pag.Any(r => !r.cod_stato.StartsWith(CodStato.ANN) &&
                                        r.num_rata == a.tab_rata_avv_pag.Where(rr => !rr.cod_stato.StartsWith(CodStato.ANN)).Max(rr => rr.num_rata) &&
                                        r.dt_scadenza_rata > v_dataScadenza))
                                                            .OrderBy(a => a.dt_emissione);
            int c = risp.Count();
            return risp;
        }

        public static IQueryable<tab_avv_pag> GetListIngiunzioniRateizzabili(dbEnte p_dbContext)
        {
            return GetList(p_dbContext)
                    .Where(d => d.anagrafica_stato.cod_stato_riferimento.StartsWith(anagrafica_stato_avv_pag.VALIDO))
                    .WhereByNotSpeditiConNotificaNonNotificati()
                    .WhereByNotRateizzato()
                    //.WhereByIngECautelariPignoramentiSollecitiAdIng()
                    .Where(a => a.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ING_FISC);
        }

        public static IQueryable<tab_avv_pag> GetListAvvisiCoattivi(dbEnte p_dbContext)
        {
            return GetList(p_dbContext)
                    .WhereByValido(anagrafica_stato_avv_pag.VALIDO)
                    .WhereByNotSpeditiConNotificaNonNotificati()
                    .WhereByNotRateizzato()
                    .WhereByIngECautelariPignoramentiSollecitiAdIng()
                    .Where(a => a.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI ||
                                a.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.INTIM);
        }
        /// <summary>
        /// Lista pre-avvisi (fermo - ipoteca)
        /// </summary>
        public static IQueryable<tab_avv_pag> GetListPreavvisi(dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(a =>
                ((a.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI || a.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO)
                && (
                    (
                       (a.id_tipo_avvpag == anagrafica_tipo_avv_pag.PRE_FERMO_AMM && a.cod_stato == anagrafica_stato_avv_pag.VAL_EME) ||
                       (a.id_tipo_avvpag == anagrafica_tipo_avv_pag.FERMO_AMM_OLD && a.cod_stato == anagrafica_stato_avv_pag.VAL_PFM)
                    )
                    || a.id_tipo_avvpag == anagrafica_tipo_avv_pag.PRE_IPOTECA)
                    || (a.id_tipo_avvpag == anagrafica_tipo_avv_pag.PIGN_TERZI_ORD && a.cod_stato == anagrafica_stato_avv_pag.VAL_EME)
                    )
              );
        }

        /// <summary>
        /// Lista pre-avvisi (fermo - ipoteca)
        /// </summary>
        public static IQueryable<tab_avv_pag> GetListPignoramentiConOordine(dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(a => a.id_entrata == anagrafica_entrate.RISCOSSIONE_COATTIVA
                && a.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO
                && (a.id_tipo_avvpag == anagrafica_tipo_avv_pag.PIGN_TERZI_ORD && a.cod_stato == anagrafica_stato_avv_pag.VAL_EME)
              );
        }


        /// <summary>
        /// Restituisce l'entità a partire da num_avv_riemesso
        /// </summary>
        /// <param name="p_numAvvRiemesso"></param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static tab_avv_pag GetByNumAvvPagRiemesso(Int32 p_numAvvRiemesso, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c => c.num_avv_riemesso == p_numAvvRiemesso);
        }

        public static bool CheckIdentificativo(string p_identificativo, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Any(d => d.identificativo_avv_pag.Equals(p_identificativo));
        }


        public static bool isAssoggettabileAttiEsecutivi(tab_avv_pag p_ingiunzione, dbEnte p_specificDB)
        {
            DateTime v_dataCorrente = DateTime.Now;
            bool v_flagAssoggettabileEsecutivi = false;
            tab_modalita_rate_avvpag_view v_modalitaRateIng = TabModalitaRateAvvPagViewBD.GetByIdTipoAvvPagAndIdEnte(p_ingiunzione.id_tipo_avvpag, p_ingiunzione.id_ente, p_specificDB);

            if (p_ingiunzione.data_ricezione.Value.AddDays(p_ingiunzione.gg_sospensione_generati).AddDays(p_ingiunzione.gg_sospensione_trasmessi).AddDays(v_modalitaRateIng.GG_massimi_data_notifica.Value) >= v_dataCorrente)
            {
                v_flagAssoggettabileEsecutivi = true;
            }
            else
            {
                if (p_ingiunzione.flag_tipo_atto_successivo == "2" && p_ingiunzione.data_avviso_bonario.Value.AddDays(p_ingiunzione.gg_sospensione_trasmessi).AddDays(v_modalitaRateIng.GG_massimi_data_notifica.Value) >= v_dataCorrente)
                {
                    v_flagAssoggettabileEsecutivi = true;
                }
                else
                {
                    TAB_JOIN_AVVCOA_INGFIS_V2 v_ultimaJoinIngFis = p_ingiunzione.TAB_JOIN_AVVCOA_INGFIS_V21.Where(j =>
                                              !j.tab_avv_pag.cod_stato.StartsWith(CodStato.ANN)
                                            && j.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.INTIM
                                            && j.tab_avv_pag.data_ricezione.HasValue
                                            && j.tab_avv_pag.flag_esito_sped_notifica.Equals("1")
                                            && j.tab_avv_pag.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.Any(r => r.periodo_validita_da <= v_dataCorrente && r.periodo_validita_a > v_dataCorrente && r.cod_stato.Equals(tab_modalita_rate_avvpag_view.ATT_ATT)
                                                                                                                    && j.tab_avv_pag.data_ricezione.Value.AddDays(r.GG_massimi_data_notifica.Value + j.tab_avv_pag.gg_sospensione_generati) >= v_dataCorrente)
                                            )
                                            .OrderByDescending(j => j.tab_avv_pag.dt_emissione).FirstOrDefault();

                    if (v_ultimaJoinIngFis != null)
                    {
                        v_flagAssoggettabileEsecutivi = true;
                    }
                }
            }

            return v_flagAssoggettabileEsecutivi;
        }



        public static void AnnullaIngiunzioniRateizzateScadute(dbEnte p_dbContext, int p_id_ente)
        {
            DateTime v_dataTaglio = DateTime.Now.AddMonths(-2);
            IQueryable<tab_avv_pag> v_ingiunzioniDaAnnullareList = TabAvvPagBD.GetList(p_dbContext)
                .WhereByIdEnte(p_id_ente).Where(a => a.id_ente_gestito == p_id_ente)
                .WhereByIdTipoServizio(anagrafica_tipo_servizi.ING_FISC)
                .WhereByCodStato(anagrafica_stato_avv_pag.VALIDO).Where(a => a.flag_rateizzazione_bis.Equals("1"))
                .Where(a => (a.imp_tot_pagato < a.imp_tot_avvpag - 1))
                .Select(s => new { avv_pag = s, impRate = s.tab_rata_avv_pag.Where(r => !r.cod_stato.StartsWith(CodStato.ANN) && r.dt_scadenza_rata < v_dataTaglio).Sum(r => r.imp_tot_rata), pagatoAvviso = s.imp_tot_pagato })
                .Where(w => w.pagatoAvviso < w.impRate).Select(s => s.avv_pag);

            foreach (tab_avv_pag v_avviso in v_ingiunzioniDaAnnullareList)
            {
                v_avviso.flag_rateizzazione_bis = "9";
            }
        }

        public static IQueryable<tab_avv_pag> GetAvvisiByRicercaAvviso(int? IdEntrata,
                                                                       int? IdTipoAvviso,
                                                                       string codiceAvvisoRicerca,
                                                                       DateTime? daAvvisoRicerca,
                                                                       DateTime? aAvvisoRicerca,
                                                                       string barcodeAvvisoRicerca,
                                                                       dbEnte p_dbContext)
        {
            IQueryable<tab_avv_pag> v_avvisiList = GetListInternal(p_dbContext);

            if (!string.IsNullOrEmpty(codiceAvvisoRicerca))
            {
                string p_identificativo2 = !string.IsNullOrEmpty(codiceAvvisoRicerca) ? codiceAvvisoRicerca.Replace("/", string.Empty).Replace("-", string.Empty).Trim() : string.Empty;
                string v_codice = string.Empty;
                string v_anno = string.Empty;
                string v_progressivo = string.Empty;

                if (!string.IsNullOrEmpty(p_identificativo2))
                {
                    v_codice = p_identificativo2.Substring(0, 4);
                    v_anno = p_identificativo2.Substring(4, 4);
                    if (p_identificativo2.Substring(8).All(char.IsDigit))
                    {
                        v_progressivo = Convert.ToInt32(p_identificativo2.Substring(8)).ToString();
                    }
                }

                v_avvisiList = v_avvisiList.Where(d => d.identificativo_avv_pag.Trim() == codiceAvvisoRicerca || (d.anagrafica_tipo_avv_pag.cod_tipo_avv_pag == v_codice &&
                                                                                                                  d.anno_riferimento == v_anno &&
                                                                                                                  d.numero_avv_pag == v_progressivo));
            }
            else
            {
                if (daAvvisoRicerca.HasValue)
                {
                    v_avvisiList = v_avvisiList.Where(d => d.dt_emissione >= daAvvisoRicerca);
                }

                if (aAvvisoRicerca.HasValue)
                {
                    v_avvisiList = v_avvisiList.Where(d => d.dt_emissione <= aAvvisoRicerca);
                }

                //if (IdEntrata.HasValue /*&& IdEntrata != 1*/)
                //{
                //    v_avvisiList = v_avvisiList.Where(d => d.id_entrata == IdEntrata.Value);
                //}

                if (IdTipoAvviso.HasValue)
                {
                    v_avvisiList = v_avvisiList.Where(d => d.id_tipo_avvpag == IdTipoAvviso.Value);
                }

                if (!string.IsNullOrEmpty(barcodeAvvisoRicerca))
                {
                    v_avvisiList = v_avvisiList.Where(d => d.tab_sped_not.Any(x => x.barcode.Trim().Equals(barcodeAvvisoRicerca)));
                }
            }
            return v_avvisiList;
        }

        public static IQueryable<tab_avv_pag> GetAvvisiByNumAvviso(string codiceAvvisoRicerca, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(x => x.identificativo_avv_pag.Trim() == codiceAvvisoRicerca)
                                                 .WhereByCodStato(anagrafica_stato_avv_pag.VAL_EME)
                                                 .Where(d => d.importo_tot_da_pagare.Value > 1);
        }
        public static IQueryable<tab_avv_pag> GetAvvisiByCodAvvisoPagoPA(string codiceAvvisoRicerca, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).WhereByCodStato(anagrafica_stato_avv_pag.VAL_EME)
                                                 .Where(d => d.importo_tot_da_pagare.Value > 1)
                                                 .Where(x => x.tab_rata_avv_pag.Any(r => r.codice_pagamento_pagopa.Trim().Equals(codiceAvvisoRicerca) && r.cod_stato == tab_rata_avv_pag.ATT_ATT));
        }

        public static IQueryable<tab_avv_pag> GetAvvisiByRicercaAvvisoDaAccoppiare(tab_mov_pag p_tabMovPag,
                                                                                   int p_idEntata,
                                                                                   string p_anno,
                                                                                   string p_codiceFiscalePiva,
                                                                                   string p_cognome,
                                                                                   string p_nome,
                                                                                   string p_ragioneSociale,
                                                                                   dbEnte p_dbContext)
        {
            List<int> v_serviziList = new List<int>();

            IQueryable<tab_avv_pag> v_avvisiList = GetListInternal(p_dbContext);

            v_avvisiList = v_avvisiList.Where(d => d.id_entrata == p_idEntata &&
                                                   d.cod_stato.StartsWith(anagrafica_stato_avv_pag.VAL_EME) &&
                                                   d.tab_liste != null &&
                                                   d.tab_liste.tab_tipo_lista.flag_tipo_lista == tab_tipo_lista.FLAG_TIPO_LISTA_C
                                                   //&& d.tab_liste.cod_stato.StartsWith(tab_liste.DEF_SPE)
                                                   );

            if (p_idEntata == anagrafica_entrate.RISCOSSIONE_COATTIVA)
            {
                v_serviziList.AddRange(new List<int>() { anagrafica_tipo_servizi.ING_FISC,
                                                         anagrafica_tipo_servizi.SOLL_PRECOA,
                                                         anagrafica_tipo_servizi.INTIM,
                                                         anagrafica_tipo_servizi.AVVISI_CAUTELARI,
                                                         anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO,
                                                         anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO,
                                                         anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_IMMOBILIARI,
                                                         anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI,
                                                         anagrafica_tipo_servizi.SERVIZI_RATEIZZAZIONE_COA,
                                                         anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO });

                v_avvisiList = v_avvisiList.Where(d => v_serviziList.Contains(d.anagrafica_tipo_avv_pag.id_servizio));

                v_avvisiList = v_avvisiList.Where(d => !d.cod_stato.StartsWith(anagrafica_stato_avv_pag.RETTIFICATO));

                if (!string.IsNullOrEmpty(p_anno))
                {
                    v_avvisiList = v_avvisiList.Where(d => d.anno_riferimento == p_anno);
                }
            }
            else
            {
                if (p_tabMovPag.id_tipo_pagamento == tab_tipo_pagamento.F24 &&
                    p_tabMovPag.Flag_accertamento == "1" &&
                    p_tabMovPag.Flag_ravvedimento != "1")
                {
                    v_serviziList.AddRange(new List<int>() { anagrafica_tipo_servizi.ACCERTAMENTO,
                                                             anagrafica_tipo_servizi.RISC_PRECOA,
                                                             anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM,
                                                             anagrafica_tipo_servizi.ACCERT_ESECUTIVO,
                                                             anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO });

                    v_avvisiList = v_avvisiList.Where(d => v_serviziList.Contains(d.anagrafica_tipo_avv_pag.id_servizio));

                    v_avvisiList = v_avvisiList.Where(d => !d.cod_stato.StartsWith(anagrafica_stato_avv_pag.RETTIFICATO));

                    if (!string.IsNullOrEmpty(p_anno))
                    {
                        v_avvisiList = v_avvisiList.Where(d => d.anno_riferimento == p_anno ||
                                                               d.tab_unita_contribuzione.Any(x => x.num_riga_avv_pag_generato == 1 &&
                                                                                                  x.id_entrata == p_idEntata &&
                                                                                                  x.anno_rif == p_anno));
                    }
                }
                else if (p_tabMovPag.id_tipo_pagamento == tab_tipo_pagamento.F24 &&
                         p_tabMovPag.Flag_accertamento != "1")
                {
                    v_serviziList.AddRange(new List<int>() { anagrafica_tipo_servizi.GEST_ORDINARIA,
                                                             anagrafica_tipo_servizi.AVVISI_ORDINARI_NON_SOGGETTO_AD_ACCERTAMENTO });

                    v_avvisiList = v_avvisiList.Where(d => v_serviziList.Contains(d.anagrafica_tipo_avv_pag.id_servizio));

                    v_avvisiList = v_avvisiList.Where(d => !d.cod_stato.StartsWith(anagrafica_stato_avv_pag.RETTIFICATO) &&
                                                           !d.cod_stato.StartsWith(anagrafica_stato_avv_pag.VAL_POP));

                    if (!string.IsNullOrEmpty(p_anno))
                    {
                        v_avvisiList = v_avvisiList.Where(d => (d.anno_riferimento == p_anno &&
                                                               (d.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_POP ||
                                                                d.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_POP_SUPPLETIVO)) ||
                                                                d.tab_unita_contribuzione.Any(x => x.num_riga_avv_pag_generato == 1 &&
                                                                                                   x.id_entrata == p_idEntata &&
                                                                                                   x.anno_rif == p_anno));
                    }
                }
                else if (p_tabMovPag.id_tipo_pagamento != tab_tipo_pagamento.F24 ||
                        (p_tabMovPag.id_tipo_pagamento == tab_tipo_pagamento.F24 &&
                         p_tabMovPag.Flag_accertamento == "1" &&
                         p_tabMovPag.Flag_ravvedimento == "1"))
                {
                    List<int> v_serviziList1 = new List<int>();
                    List<int> v_serviziList2 = new List<int>();

                    v_serviziList1 = new List<int>() { anagrafica_tipo_servizi.ACCERTAMENTO,
                                                       anagrafica_tipo_servizi.RISC_PRECOA,
                                                       anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM,
                                                       anagrafica_tipo_servizi.ACCERT_ESECUTIVO,
                                                       anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO};

                    v_serviziList2 = new List<int>() { anagrafica_tipo_servizi.GEST_ORDINARIA,
                                                       anagrafica_tipo_servizi.AVVISI_ORDINARI_NON_SOGGETTO_AD_ACCERTAMENTO };

                    if (!string.IsNullOrEmpty(p_anno))
                    {
                        v_avvisiList = v_avvisiList.Where(d => (v_serviziList1.Contains(d.anagrafica_tipo_avv_pag.id_servizio) &&
                                                               !d.cod_stato.StartsWith(anagrafica_stato_avv_pag.RETTIFICATO) &&
                                                               (d.anno_riferimento == p_anno ||
                                                                d.tab_unita_contribuzione.Any(x => x.num_riga_avv_pag_generato == 1 &&
                                                                                                   x.id_entrata == p_idEntata &&
                                                                                                   x.anno_rif == p_anno)))
                                                               ||
                                                               (v_serviziList2.Contains(d.anagrafica_tipo_avv_pag.id_servizio) &&
                                                               !d.cod_stato.StartsWith(anagrafica_stato_avv_pag.RETTIFICATO) &&
                                                               !d.cod_stato.StartsWith(anagrafica_stato_avv_pag.VAL_POP) &&
                                                               ((d.anno_riferimento == p_anno &&
                                                                (d.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_POP ||
                                                                 d.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_POP_SUPPLETIVO)) ||
                                                                 d.tab_unita_contribuzione.Any(x => x.num_riga_avv_pag_generato == 1 &&
                                                                                                    x.id_entrata == p_idEntata &&
                                                                                                    x.anno_rif == p_anno)))
                                                               ||
                                                               (d.anno_riferimento == p_anno &&
                                                                d.id_tipo_avvpag == anagrafica_tipo_servizi.SERVIZI_RATEIZZAZIONE_COA));
                    }
                    else
                    {
                        v_avvisiList = v_avvisiList.Where(d => (v_serviziList1.Contains(d.anagrafica_tipo_avv_pag.id_servizio) &&
                                                               !d.cod_stato.StartsWith(anagrafica_stato_avv_pag.RETTIFICATO))
                                                               ||
                                                               (v_serviziList2.Contains(d.anagrafica_tipo_avv_pag.id_servizio) &&
                                                               !d.cod_stato.StartsWith(anagrafica_stato_avv_pag.RETTIFICATO) &&
                                                               !d.cod_stato.StartsWith(anagrafica_stato_avv_pag.VAL_POP))
                                                               ||
                                                               d.id_tipo_avvpag == anagrafica_tipo_servizi.SERVIZI_RATEIZZAZIONE_COA);
                    }
                }
            }

            if (string.IsNullOrEmpty(p_codiceFiscalePiva) &&
                string.IsNullOrEmpty(p_cognome) &&
                string.IsNullOrEmpty(p_nome) &&
                string.IsNullOrEmpty(p_ragioneSociale))
            {
                v_avvisiList = Enumerable.Empty<tab_avv_pag>().AsQueryable();
            }
            else
            {
                if (!string.IsNullOrEmpty(p_codiceFiscalePiva))
                {
                    v_avvisiList = v_avvisiList.Where(d => d.tab_contribuente.cod_fiscale == p_codiceFiscalePiva ||
                                                           d.tab_contribuente.p_iva == p_codiceFiscalePiva);
                }

                if (!string.IsNullOrEmpty(p_cognome))
                {
                    v_avvisiList = v_avvisiList.Where(d => d.tab_contribuente.cognome.Contains(p_cognome));
                }

                if (!string.IsNullOrEmpty(p_nome))
                {
                    v_avvisiList = v_avvisiList.Where(d => d.tab_contribuente.nome.Contains(p_nome));
                }

                if (!string.IsNullOrEmpty(p_ragioneSociale))
                {
                    v_avvisiList = v_avvisiList.Where(d => d.tab_contribuente.rag_sociale.Replace("'", "").Contains(p_ragioneSociale) ||
                                                           d.tab_contribuente.denominazione_commerciale.Replace("'", "").Contains(p_ragioneSociale));
                }
            }

            v_avvisiList = v_avvisiList.Where(d => d.tab_liste.tab_tipo_lista.flag_tipo_lista == tab_tipo_lista.FLAG_TIPO_LISTA_C);

            return v_avvisiList;
        }

        public static IQueryable<tab_avv_pag> GetListIngiunzioniPerIspezioni(int p_idEnte, List<Int32> p_serviziScartoList, dbEnte p_context, DateTime? p_dataEmissione = null, decimal? p_idContribuente = null, Boolean p_escludiIngiunzioniInIspezione = true)
        {
            DateTime v_oggi = DateTime.Now;
            if (p_dataEmissione != null)
            {
                v_oggi = p_dataEmissione.Value;
            }

            List<Int32> v_serviziScartoIspezioniList = new List<Int32>() { anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO,
                                                                      anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO,
                                                                      anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_IMMOBILIARI,
                                                                      anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI,
                                                                      anagrafica_tipo_servizi.AVVISI_CAUTELARI };

            IQueryable<tab_avv_pag> risp = GetList(p_context);

            if (p_idContribuente != null)
            {
                risp = risp.Where(i => i.id_anag_contribuente == p_idContribuente);
            }

            List<int> v_serviziIspezionabili = new List<int>() { anagrafica_tipo_servizi.ING_FISC, anagrafica_tipo_servizi.ACCERT_ESECUTIVO, anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO };

            risp = risp.WhereByServiziList(v_serviziIspezionabili)
                        .WhereByCodStato(CodStato.VAL)
                        .WhereByValido()
                        .Where(a => !String.IsNullOrEmpty(a.flag_esito_sped_notifica) && a.flag_esito_sped_notifica.Equals("1"))
                        //.Where(a=> a.importo_tot_da_pagare >= 1)
                        .Where(a => a.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.Any(r => r.periodo_validita_da <= v_oggi && r.periodo_validita_a > v_oggi && r.cod_stato.Equals(tab_modalita_rate_avvpag_view.ATT_ATT)
                                && a.importo_tot_da_pagare > r.importo_minimo_da_pagare //Controlla l'importo minimo non pagato per il tipo avviso
                                && a.data_ricezione.HasValue
                                && System.Data.Entity.DbFunctions.AddDays(a.data_ricezione.Value, r.GG_minimi_data_notifica.Value) < v_oggi)
                            )
                        .WhereNonRateizzataOrAnnullata()
                        .WhereNonRottamata()
                        //contribuente con stato valido ai fini dell'emissione
                        .Where(a => a.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("0"))
                        //Scarta i contribuenti che si trovano in una ispezione valida recente
                        .Where(a => !p_escludiIngiunzioniInIspezione || !a.tab_contribuente.tab_ispezioni_coattivo_new.Any(i => i.cod_stato == tab_ispezioni_coattivo_new.VAL_VAL))
                        //Taglia le ingiunzioni contenute in atti rateizzati
                        .Where(a => !a.TAB_JOIN_AVVCOA_INGFIS_V21.Any(coa => coa.tab_avv_pag.flag_rateizzazione_bis.Equals("1") && !coa.tab_avv_pag.cod_stato.Contains(anagrafica_stato_avv_pag.ANNULLATO)))
                        //Taglia le ingiunzioni contenute in atti sospesi
                        .Where(a => !a.TAB_JOIN_AVVCOA_INGFIS_V21.Any(coa => coa.tab_avv_pag.cod_stato.Contains(anagrafica_stato_avv_pag.SOSPESO)))

                        //scarta le ingiunzioni finite in atti successivi (per i servizi elencati: Pignoramenti) non annullati e notificati
                        .Where(a => !a.TAB_JOIN_AVVCOA_INGFIS_V21.Any(coa => p_serviziScartoList.Contains(coa.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio)
                                                                          && !coa.tab_avv_pag.cod_stato.Contains(anagrafica_stato_avv_pag.ANNULLATO)
                                                                          && a.flag_esito_sped_notifica.Equals("1")))

                        //Verifica sui Fermi/Ipoteca da preavvisi di ipoteca tenendo conto della validità
                        .Where(a => !a.TAB_JOIN_AVVCOA_INGFIS_V21.Any(coa => (coa.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI
                                                                            && coa.tab_avv_pag.TAB_SUPERVISIONE_FINALE_V2.Where(s => s.COD_STATO == TAB_SUPERVISIONE_FINALE_V2.VAL_VAL).Any(p => p.id_avvpag_preavviso_collegato != null))
                                        && !coa.tab_avv_pag.cod_stato.Contains(anagrafica_stato_avv_pag.ANNULLATO)
                                        && (
                                            coa.tab_avv_pag.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.Any(r => r.periodo_validita_da <= v_oggi && r.periodo_validita_a > v_oggi && (SqlFunctions.DateAdd("day", r.GG_massimi_data_emissione, coa.tab_avv_pag.dt_emissione.Value) > v_oggi))))
                                            )

                    //.WhereIngiunzioneInAttoSuccessivoAncoraPagabile()
                    //Non prende le ingiunzioni contenute in fatt_emissione in avvisi del servizio indicato (esecutivi e cautelari)
                    .Where(i => !i.tab_unita_contribuzione_fatt_emissione.Any(s => s.tab_avv_pag_fatt_emissione.tab_liste.cod_stato.StartsWith(tab_liste.PRE)
                                && s.cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO) && v_serviziScartoIspezioniList.Contains(s.anagrafica_tipo_avv_pag.id_servizio)))
                    ;

            if (p_serviziScartoList.Contains(anagrafica_tipo_servizi.AVVISI_CAUTELARI))
            {
                //Verifica sui Preavvisi di Fermo/Preavvisi di Ipoteca senza tenere conto della validità
                risp = risp.Where(a => !a.TAB_JOIN_AVVCOA_INGFIS_V21.Any(coa => (coa.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI && coa.tab_avv_pag.flag_esito_sped_notifica == "1"
                                                                    && coa.tab_avv_pag.TAB_SUPERVISIONE_FINALE_V2.Where(s => s.COD_STATO == TAB_SUPERVISIONE_FINALE_V2.VAL_VAL).Any(p => p.id_avvpag_preavviso_collegato == null)
                                                                    && (coa.tab_avv_pag.TAB_SUPERVISIONE_FINALE_V22.Where(s => s.COD_STATO == TAB_SUPERVISIONE_FINALE_V2.VAL_VAL).Count() == 0
                                                                        || coa.tab_avv_pag.TAB_SUPERVISIONE_FINALE_V22.Where(s => s.COD_STATO == TAB_SUPERVISIONE_FINALE_V2.VAL_VAL).Any(w => w.ID_AVVPAG_EMESSO == null)))
                                        && !coa.tab_avv_pag.cod_stato.Contains(anagrafica_stato_avv_pag.ANNULLATO))
                            );
            }


            return risp;
        }

        public static decimal GetTotaleMorositaIngiunzioni(int p_idEnte, decimal p_idContribuente, DateTime p_dataRilevazione, dbEnte p_context)
        {
            return GetList(p_context).WhereByIdEnte(p_idEnte)
                                     .WhereByIdContribuente(p_idContribuente)
                                     .WhereByIdTipoServizio(anagrafica_tipo_servizi.ING_FISC)
                                     .WhereByCodStato(CodStato.VAL)
                                     .WhereByValido()
                                     .Where(a => !String.IsNullOrEmpty(a.flag_esito_sped_notifica) && a.flag_esito_sped_notifica.Equals("1"))
                                     //.Where(a => a.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.Any(r => r.periodo_validita_da <= p_dataRilevazione && r.periodo_validita_a > p_dataRilevazione && r.cod_stato.Equals(tab_modalita_rate_avvpag_view.ATT_ATT)
                                     //         && a.importo_tot_da_pagare > r.importo_minimo_da_pagare //Controlla l'importo minimo non pagato per il tipo avviso
                                     //                                                                 //&& a.imp_tot_avvpag_rid > r.importo_minimo_emissione_avvpag
                                     //         && a.data_ricezione.HasValue
                                     //         && System.Data.Entity.DbFunctions.AddDays(a.data_ricezione.Value, r.GG_minimi_data_notifica.Value) < p_dataRilevazione)
                                     //       )
                                     .WhereNonRateizzataOrAnnullata()
                                     .WhereNonRottamata()
                                     //.Where(a => a.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("0")) //contribuente con stato valido ai fini dell'emissione                                                    
                                     //.WhereNonPrescritta()
                                     .Sum(a => a.importo_tot_da_pagare).GetValueOrDefault(0);
        }


        public static DateTime? GetDataPrescrizione(tab_avv_pag p_ingiunzione)
        {
            //Verificare se sono corretti gli utilizzi di data emissione e data ricezione
            int v_minAnniRinnovo = 0;

            List<int> v_serviziTestabili = new List<int>() { anagrafica_tipo_servizi.ING_FISC, anagrafica_tipo_servizi.ACCERT_ESECUTIVO, anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO };
            DateTime v_ultimaDataValidita;

            //Se l'avviso non è del tipo giusto non restituisce nulla
            if (v_serviziTestabili.Contains(p_ingiunzione.anagrafica_tipo_avv_pag.id_servizio))
            {
                //Numero anni di rinnovo per l'ingiunzione
                if (p_ingiunzione.anagrafica_tipo_avv_pag.id_entrata_avvpag_collegati.HasValue)
                {
                    v_minAnniRinnovo = p_ingiunzione.anagrafica_tipo_avv_pag.anagrafica_entrate1.AA_prescrizione_entrata;
                }
                else
                {
                    v_minAnniRinnovo = p_ingiunzione.tab_contribuzione.Where(i => i.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_ENT)
                                                                                        .Select(s => s.tab_tipo_voce_contribuzione.anagrafica_entrate.AA_prescrizione_entrata).Min();
                }

                //Ultimo atto che contiene l'ingiunzione
                tab_avv_pag v_ultimoSuccessivo = p_ingiunzione.TAB_JOIN_AVVCOA_INGFIS_V21.Where(j => (!j.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO))
                                                                                                    && j.tab_avv_pag.flag_esito_sped_notifica == "1")
                                                                                         .OrderByDescending(x => x.tab_avv_pag.data_ricezione)
                                                                                         .Select(s => s.tab_avv_pag)
                                                                                         .FirstOrDefault();

                //Se non ci sono atti successivi notificati
                if (v_ultimoSuccessivo is null)
                {
                    if (p_ingiunzione.flag_esito_sped_notifica == "1")
                    {
                        //Se l'ingiunzione è notificata, usa la data di ricezione
                        v_ultimaDataValidita = p_ingiunzione.data_ricezione.Value;
                    }
                    else if (String.IsNullOrEmpty(p_ingiunzione.flag_esito_sped_notifica))
                    {
                        //L'ingiunzione è ancora senza esito e usa la data di emissione
                        v_ultimaDataValidita = p_ingiunzione.dt_emissione.Value;
                    }
                    else
                    {
                        //L'ingiunzione ha una notifica negativa e quindi non è valida
                        return null;
                    }

                    if (p_ingiunzione.gg_sospensione_trasmessi > 0)
                    {
                        v_ultimaDataValidita.AddDays(p_ingiunzione.gg_sospensione_generati);
                    }

                    if (p_ingiunzione.flag_tipo_atto_successivo.Equals("2"))
                    {
                        if (p_ingiunzione.data_avviso_bonario.HasValue && v_ultimaDataValidita < p_ingiunzione.data_avviso_bonario.Value)
                        {
                            v_ultimaDataValidita = p_ingiunzione.data_avviso_bonario.Value;
                        }
                    }
                }
                else
                {
                    if (v_ultimoSuccessivo.data_ricezione.HasValue)
                    {
                        v_ultimaDataValidita = v_ultimoSuccessivo.data_ricezione.Value;
                    }
                    else
                    {
                        v_ultimaDataValidita = v_ultimoSuccessivo.dt_emissione.Value;
                    }
                }

                //Sposta in avanti l'ultima validità per il numero di anni selezionato
                v_ultimaDataValidita = v_ultimaDataValidita.AddYears(v_minAnniRinnovo);

                return v_ultimaDataValidita;
            }
            else
            {
                return null;
            }

            ////Verificare se sono corretti gli utilizzi di data emissione e data ricezione
            //DateTime v_inizioCovid = new DateTime(2020, 3, 8); //Data di inizio del covid 08/03/2020
            //DateTime v_fineCovid = new DateTime(2020, 5, 31, 23, 59, 59); //Data di inizio del covid 31/05/2020
            //int v_giorniSospensioneCovid = 85; //Giorni di sospensione del covid
            //Boolean v_ultimoPrecedenteCovid = false;
            //int v_minAnniRinnovo = 0;

            //List<int> v_serviziTestabili = new List<int>() { anagrafica_tipo_servizi.ING_FISC, anagrafica_tipo_servizi.ACCERT_ESECUTIVO, anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO };
            //DateTime v_ultimaDataValidita;

            ////Se l'avviso non è del tipo giusto non restituisce nulla
            //if (v_serviziTestabili.Contains(p_ingiunzione.anagrafica_tipo_avv_pag.id_servizio))
            //{
            //    //Elenco delle entrate presenti nell'ingiunzione
            //    List<anagrafica_entrate> v_entrate = p_ingiunzione.tab_contribuzione.Where(i => i.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_ENT)
            //                                                                        .Select(s => s.tab_tipo_voce_contribuzione.anagrafica_entrate).Distinct().ToList();

            //    //Ultimo atto che contiene l'ingiunzione
            //    tab_avv_pag v_ultimoSuccessivo = p_ingiunzione.TAB_JOIN_AVVCOA_INGFIS_V21.Where(j => (!j.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO))
            //                                                                                        && j.tab_avv_pag.flag_esito_sped_notifica == "1")
            //                                                                             .OrderByDescending(x => x.tab_avv_pag.data_ricezione)
            //                                                                             .Select(s => s.tab_avv_pag)
            //                                                                             .FirstOrDefault();

            //    //Se non ci sono atti successivi notificati
            //    if (v_ultimoSuccessivo is null)
            //    {
            //        if (p_ingiunzione.flag_esito_sped_notifica == "1")
            //        {
            //            //Se l'ingiunzione è notificata, usa la data di ricezione
            //            v_ultimaDataValidita = p_ingiunzione.data_ricezione.Value;

            //            //Verifica se attraversa il periodo covid.
            //            //Sarebbe più corretto vedere anche se scade prima della fine del covid
            //            if (p_ingiunzione.data_ricezione.Value <= v_fineCovid)
            //            {
            //                v_ultimoPrecedenteCovid = true;
            //            }
            //        }
            //        else if (String.IsNullOrEmpty(p_ingiunzione.flag_esito_sped_notifica))
            //        {
            //            //L'ingiunzione è ancora senza esito e usa la data di emissione
            //            v_ultimaDataValidita = p_ingiunzione.dt_emissione.Value;

            //            //Verifica se attraversa il periodo covid.
            //            //In questo case non server vedere anche se scade prima dell'inizio covid perchè vorrebbe dire che sono più di 3 anni che non scarichiamo gli esiti
            //            if (p_ingiunzione.dt_emissione.Value <= v_fineCovid)
            //            {
            //                v_ultimoPrecedenteCovid = true;
            //            }
            //        }
            //        else
            //        {
            //            //L'ingiunzione ha una notifica negativa e quindi non è valida
            //            return null;
            //        }
            //    }
            //    else
            //    {
            //        v_ultimaDataValidita = v_ultimoSuccessivo.data_ricezione.Value;

            //        //Verifica se l'ultimo atto successivo è precedente alla fine covid.
            //        if (v_ultimoSuccessivo.data_ricezione.Value <= v_fineCovid)
            //        {
            //            v_ultimoPrecedenteCovid = true;
            //        }

            //        //Se è presente un successivo, ricarica la lista delle entrate perchè ne potrebbe contenere di tipi diversi
            //        v_entrate = v_ultimoSuccessivo.tab_contribuzione.Where(i => i.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_ENT)
            //                                                        .Select(s => s.tab_tipo_voce_contribuzione.anagrafica_entrate).Distinct().ToList();

            //    }

            //    //Prende il perdiodo di rinnovo minimo tra tutte le entrate presenti nell'avviso
            //    v_minAnniRinnovo = v_entrate.Select(s => s.AA_prescrizione_entrata).Min();

            //    //Verifica se è presente solo il bollo auto e in questo caso porta la fine validità a fine anno
            //    if (v_entrate.Count == 1 && v_entrate.First().id_entrata == anagrafica_entrate.BOLLO_AUTO)
            //    {
            //        v_ultimaDataValidita = new DateTime(v_ultimaDataValidita.Year, 12, 31, 23, 59, 59);
            //    }

            //    //Sposta in avanti l'ultima validità per il numero di anni selezionato
            //    v_ultimaDataValidita = v_ultimaDataValidita.AddYears(v_minAnniRinnovo);

            //    //Se attraversa il covid, aggiunge i giorni di proroga
            //    if (v_ultimoPrecedenteCovid && v_ultimaDataValidita >= v_inizioCovid)
            //    {
            //        v_ultimaDataValidita = v_ultimaDataValidita.AddDays(v_giorniSospensioneCovid);
            //    }

            //    return v_ultimaDataValidita;
            //}
            //else
            //{
            //    return null;
            //}
        }

        public static tab_avv_pag creaAvvisoPark(decimal p_idContribuente, int p_tipoAvvPag, string p_codiceBollettino, string p_barCode,
                                             decimal p_importo, decimal p_importoRidotto, DateTime p_dataEmissione, Int32 p_idStruttura,
                                             Int32 p_idOperatore, Int32 p_idDenunciaContratto, string p_fonte, Int32 p_idLista, Int32 p_giorniScadenza,
                                             Int32 p_idEnte, Int32 p_idEntrata, dbEnte p_context)
        {
            tab_avv_pag v_avviso = p_context.tab_avv_pag.Create();

            //creazione dell'avviso
            v_avviso.id_ente = p_idEnte;
            v_avviso.id_ente_gestito = p_idEnte;
            v_avviso.id_entrata = p_idEntrata;
            v_avviso.id_anag_contribuente = p_idContribuente;
            v_avviso.id_tipo_avvpag = p_tipoAvvPag;
            v_avviso.id_tab_contr_doc = p_idDenunciaContratto;
            v_avviso.cod_stato_avv_pag = anagrafica_stato_avv_pag.VAL_CON;
            v_avviso.id_stato_avv_pag = anagrafica_stato_avv_pag.VAL_CON_ID;
            v_avviso.id_stato = anagrafica_stato_avv_pag.VAL_CON_ID;
            v_avviso.dt_emissione = p_dataEmissione;
            v_avviso.anno_riferimento = p_dataEmissione.Year.ToString();
            v_avviso.numero_avv_pag = p_codiceBollettino;
            v_avviso.barcode = p_barCode;
            v_avviso.flag_spedizione_notifica = "1";
            v_avviso.flag_esito_sped_notifica = "1";
            v_avviso.data_avvenuta_notifica = p_dataEmissione;
            v_avviso.imp_tot_avvpag = p_importo;
            if (p_importoRidotto > 0)
                v_avviso.importo_ridotto = p_importoRidotto;
            v_avviso.imp_imp_entr_avvpag = 0;
            v_avviso.imp_esente_iva_avvpag = 0;
            //v_avviso.imp_IVA_avvpag = 0;
            v_avviso.imp_tot_spese_avvpag = 0;
            v_avviso.imp_spese_notifica = 0;
            v_avviso.imp_tot_pagato = 0;
            v_avviso.importo_tot_da_pagare = p_importo;
            v_avviso.imp_tot_avvpag_rid = p_importo;
            v_avviso.flag_rateizzazione = "1";
            v_avviso.data_rateizzazione = p_dataEmissione;
            v_avviso.imp_rateizzato = p_importo;
            v_avviso.periodicita_rate = 1;
            v_avviso.num_rate = 1;
            v_avviso.data_scadenza_1_rata = p_dataEmissione.AddDays(p_giorniScadenza);
            v_avviso.id_risorsa = p_idOperatore;
            v_avviso.dt_avv_pag = p_dataEmissione;
            v_avviso.fonte_emissione = p_fonte;
            v_avviso.id_lista_emissione = p_idLista;

            v_avviso.data_stato = p_dataEmissione;
            v_avviso.id_struttura_stato = p_idStruttura;
            v_avviso.id_risorsa_stato = p_idOperatore;

            p_context.tab_avv_pag.Add(v_avviso);

            return v_avviso;
        }

        /// <returns>ritorna lo IUV (getIuvOrCodicePagamento = true) o il codice avviso pago PA (getIuvOrCodicePagamento = false)</returns>
        //public static string calcolaIuvOrCodiceAvvisoPagoPA(tab_avv_pag v_avviso, tab_rata_avv_pag v_rata, bool getIuvOrCodicePagamento, dbEnte ctx)
        //{
        //    string v_returnValue = string.Empty;
        //    anagrafica_ente v_ente = AnagraficaEnteBD.GetById(v_avviso.id_ente, ctx);

        //    //Non è PAGO PA
        //    if (string.IsNullOrEmpty(v_ente.flag_tipo_gestione_pagopa) || v_ente.flag_tipo_gestione_pagopa.Equals("0"))
        //    {
        //        string v_bar_code = v_avviso.identificativo_avv_pag.Trim() + v_rata.num_rata.ToString("D2");

        //        v_iuv = v_bar_code + calcolaCheckDigit(v_bar_code);
        //        v_codicepagamento = string.Empty
        //        if (getIuvOrCodicePagamento)
        //        {
        //            v_returnValue = v_bar_code + calcolaCheckDigit(v_bar_code); //Iuv
        //        }
        //        else
        //        {
        //            v_returnValue = string.Empty; //Codice pagamento
        //        }
        //    }
        //    else //è PAGO PA
        //    {
        //        string v_salva_aux_digit_pagopa;
        //        string v_salva_application_code_pagopa;
        //        string v_salvacodice_segregazione_pagopa;
        //        string v_salvacodice_struttura_ente_pagopa;

        //        tab_modalita_rate_avvpag_view v_modalitaAvvisoEmissione = TabModalitaRateAvvPagViewBD.GetByIdTipoAvvPagAndIdEnteAndRange(v_avviso.id_tipo_avvpag, v_avviso.id_ente, ctx);

        //        //if (v_modalitaAvvisoEmissione == null)
        //        //{
        //        //    v_modalitaAvvisoEmissione = TabModalitaRateAvvPagViewBD.GetByIdServizioAndIdEnte(v_avviso.anagrafica_tipo_avv_pag.id_servizio, v_avviso.id_ente, ctx);
        //        //}

        //        if (v_modalitaAvvisoEmissione != null &&
        //            string.IsNullOrEmpty(v_modalitaAvvisoEmissione.aux_digit_pagopa))
        //        {
        //            v_salva_aux_digit_pagopa = v_ente.aux_digit_pagopa;
        //            v_salva_application_code_pagopa = v_ente.application_code_pagopa;
        //            v_salvacodice_segregazione_pagopa = v_ente.codice_segregazione_pagopa;
        //            v_salvacodice_struttura_ente_pagopa = v_ente.codice_struttura_ente_pagopa;
        //        }
        //        else if (v_modalitaAvvisoEmissione != null &&
        //                 !string.IsNullOrEmpty(v_modalitaAvvisoEmissione.aux_digit_pagopa))
        //        {
        //            v_salva_aux_digit_pagopa = v_modalitaAvvisoEmissione.aux_digit_pagopa;
        //            v_salva_application_code_pagopa = v_modalitaAvvisoEmissione.application_code_pagopa;
        //            v_salvacodice_segregazione_pagopa = v_modalitaAvvisoEmissione.codice_segregazione_pagopa;
        //            v_salvacodice_struttura_ente_pagopa = v_modalitaAvvisoEmissione.codice_struttura_ente_pagopa;
        //        }
        //        else
        //        {
        //            v_salva_aux_digit_pagopa = string.Empty;
        //            v_salva_application_code_pagopa = string.Empty;
        //            v_salvacodice_segregazione_pagopa = string.Empty;
        //            v_salvacodice_struttura_ente_pagopa = string.Empty;
        //        }

        //        //if (!string.IsNullOrEmpty(v_ente.aux_digit_pagopa))
        //        //{
        //        //    //vuol dire che i dati per comporre il codice avviso e lo iuv di pagopa 
        //        //    //si trovano su anagrafica_ente perché SONO UGUALI per tutti i tipi servizio 
        //        //    //o i tipi avviso gestiti per l’ ENTE CREDITORE a cui l’avviso appartiene
        //        //    v_salva_aux_digit_pagopa = v_ente.aux_digit_pagopa;
        //        //    v_salva_application_code_pagopa = v_ente.application_code_pagopa;
        //        //    v_salvacodice_segregazione_pagopa = v_ente.codice_segregazione_pagopa;
        //        //    v_salvacodice_struttura_ente_pagopa = v_ente.codice_struttura_ente_pagopa;
        //        //}
        //        //else
        //        //{
        //        //    tab_modalita_rate_avvpag_view v_modalitaAvvisoEmissione = TabModalitaRateAvvPagViewBD.GetByIdTipoAvvPagAndIdEnteAndRange(v_avviso.id_tipo_avvpag, v_avviso.id_ente, ctx);
        //        //    v_salva_aux_digit_pagopa = v_modalitaAvvisoEmissione.aux_digit_pagopa;
        //        //    v_salva_application_code_pagopa = v_modalitaAvvisoEmissione.application_code_pagopa;
        //        //    v_salvacodice_segregazione_pagopa = v_modalitaAvvisoEmissione.codice_segregazione_pagopa;
        //        //    v_salvacodice_struttura_ente_pagopa = v_modalitaAvvisoEmissione.codice_struttura_ente_pagopa;
        //        //}

        //        int v_annoPagoPA = v_avviso.dt_emissione.HasValue ? v_avviso.dt_emissione.Value.Year : DateTime.Now.Year;
        //        int v_progressivoIuv = TabProgIuvBD.IncrementaProgressivoCorrente(v_avviso.anagrafica_tipo_avv_pag.cod_tipo_avv_pag, v_annoPagoPA, ctx);
        //        string v_iuvBase = string.Empty;
        //        string v_codice_iuv_pagopa = string.Empty;
        //        string v_codice_avviso_pagopa = string.Empty;

        //        if (v_ente.flag_tipo_gestione_pagopa.Equals(anagrafica_ente.FLAG_TIPO_GESTIONE_PAGOPA_PUBLISERVIZI))
        //        {
        //            //il pagamento dell’avviso è gestito con pagopa su conto corrente di Publiservizi 
        //            //cioè Publiservizi è Ente Creditore di pagopa
        //            v_iuvBase = v_ente.codice_ente_pagopa +
        //                        v_avviso.anagrafica_tipo_avv_pag.cod_tipo_avv_pag + //4 crt
        //                        v_annoPagoPA.ToString().Substring(2, 2) + //2crt
        //                        v_progressivoIuv.ToString("D7"); //7crt

        //            v_codice_iuv_pagopa = v_iuvBase;
        //            v_codice_avviso_pagopa = v_salva_aux_digit_pagopa + v_iuvBase;
        //        }
        //        else if (v_ente.flag_tipo_gestione_pagopa.Equals(anagrafica_ente.FLAG_TIPO_GESTIONE_PAGOPA_PARTNERTECNOLOGICO))
        //        {
        //            //il pagamento dell’avviso è gestito con pagopa su conto corrente dell’Ente e 
        //            //Publiservizi è partner tecnologico dell’ Ente cioè è direttamente l’Ente Creditore di pagopa       

        //            v_iuvBase = v_avviso.anagrafica_tipo_avv_pag.cod_tipo_avv_pag + //4 crt
        //                        v_annoPagoPA.ToString().Substring(2, 2) + //2crt
        //                        v_progressivoIuv.ToString("D7"); //7crt

        //            v_codice_iuv_pagopa = v_salvacodice_segregazione_pagopa +
        //                                  v_iuvBase +
        //                                  calcolaCheckDigit(v_salva_aux_digit_pagopa + v_salvacodice_segregazione_pagopa + v_iuvBase);

        //            v_codice_avviso_pagopa = v_salva_aux_digit_pagopa + v_codice_iuv_pagopa;
        //        }
        //        else if (v_ente.flag_tipo_gestione_pagopa.Equals(anagrafica_ente.FLAG_TIPO_GESTIONE_PAGOPA_SUPPORTO))
        //        {
        //            //il pagamento dell’avviso è gestito con pagopa su conto corrente dell’Ente e 
        //            //Publiservizi effettua attività di supporto per l’Ente creditore ma deve produrre 
        //            //l’avviso analogico ed i bollettini pagopa 

        //            if (v_salva_aux_digit_pagopa == "1" &&
        //                !string.IsNullOrEmpty(v_salvacodice_segregazione_pagopa) &&
        //                !v_salvacodice_segregazione_pagopa.Equals("0"))
        //            {
        //                v_iuvBase = v_salvacodice_segregazione_pagopa +
        //                            v_avviso.anagrafica_tipo_avv_pag.cod_tipo_avv_pag + //4 crt
        //                            v_annoPagoPA.ToString().Substring(2, 2) + //2crt
        //                            v_progressivoIuv.ToString("D7"); //7crt

        //                v_codice_iuv_pagopa = v_iuvBase;
        //                v_codice_avviso_pagopa = v_salva_aux_digit_pagopa + v_codice_iuv_pagopa;

        //            }
        //            else if (v_salva_aux_digit_pagopa == anagrafica_ente.AUX_DIGIT_1 &&
        //                    !string.IsNullOrEmpty(v_salvacodice_struttura_ente_pagopa) &&
        //                    !v_salvacodice_struttura_ente_pagopa.Equals("0"))
        //            {
        //                v_iuvBase = v_salvacodice_struttura_ente_pagopa +
        //                            v_avviso.anagrafica_tipo_avv_pag.cod_tipo_avv_pag + //4 crt
        //                            v_annoPagoPA.ToString().Substring(2, 2) + //2crt
        //                            v_progressivoIuv.ToString("D7"); //7crt

        //                v_codice_iuv_pagopa = v_iuvBase;
        //                v_codice_avviso_pagopa = v_salva_aux_digit_pagopa + v_codice_iuv_pagopa;
        //            }
        //            else if (v_salva_aux_digit_pagopa == anagrafica_ente.AUX_DIGIT_2 &&
        //               !string.IsNullOrEmpty(v_salvacodice_struttura_ente_pagopa) &&
        //               !v_salvacodice_struttura_ente_pagopa.Equals("0")
        //               )
        //            {
        //                v_iuvBase = v_salvacodice_struttura_ente_pagopa +
        //                            v_avviso.anagrafica_tipo_avv_pag.cod_tipo_avv_pag + //4 crt
        //                            v_annoPagoPA.ToString().Substring(2, 2) + //2crt
        //                            v_progressivoIuv.ToString("D7"); //7crt

        //                v_codice_iuv_pagopa = v_iuvBase + calcolaCheckDigit(v_salva_aux_digit_pagopa + v_salvacodice_struttura_ente_pagopa + v_iuvBase);
        //                v_codice_avviso_pagopa = v_salva_aux_digit_pagopa + v_codice_iuv_pagopa;
        //            }
        //            else if (v_salva_aux_digit_pagopa == anagrafica_ente.AUX_DIGIT_3)
        //            {
        //                v_iuvBase = v_avviso.anagrafica_tipo_avv_pag.cod_tipo_avv_pag + //4 crt
        //                            v_annoPagoPA.ToString().Substring(2, 2) + //2crt
        //                            v_progressivoIuv.ToString("D7"); //7crt

        //                v_codice_iuv_pagopa = v_salvacodice_segregazione_pagopa +
        //                                      v_iuvBase +
        //                                      calcolaCheckDigit(v_salva_aux_digit_pagopa + v_salvacodice_segregazione_pagopa +
        //                                      v_iuvBase);

        //                v_codice_avviso_pagopa = v_salva_aux_digit_pagopa + v_codice_iuv_pagopa;
        //            }
        //            else
        //            {

        //            }
        //        }

        //        if (getIuvOrCodicePagamento)
        //        {
        //            v_returnValue = v_codice_iuv_pagopa; //Iuv
        //        }
        //        else
        //        {
        //            v_returnValue = v_codice_avviso_pagopa; //Codice pagamento
        //        }
        //    }

        //    return v_returnValue;
        //}


        public static void calcolaIuvOrCodiceAvvisoPagoPA(tab_avv_pag v_avviso, tab_rata_avv_pag v_rata, dbEnte ctx, out string v_iuv, out string v_codicepagamento)
        {
            string v_returnValue = string.Empty;
            anagrafica_ente v_ente = AnagraficaEnteBD.GetById(v_avviso.id_ente, ctx);

            //Non è PAGO PA
            if (string.IsNullOrEmpty(v_ente.flag_tipo_gestione_pagopa) || v_ente.flag_tipo_gestione_pagopa.Equals("0"))
            {
                string v_bar_code = v_avviso.identificativo_avv_pag.Trim() + v_rata.num_rata.ToString("D2");

                v_iuv = v_bar_code + calcolaCheckDigit(v_bar_code);
                v_codicepagamento = string.Empty;
            }
            else //è PAGO PA
            {
                string v_salva_aux_digit_pagopa;
                string v_salva_application_code_pagopa;
                string v_salvacodice_segregazione_pagopa;
                string v_salvacodice_struttura_ente_pagopa;

                tab_modalita_rate_avvpag_view v_modalitaAvvisoEmissione = TabModalitaRateAvvPagViewBD.GetByIdTipoAvvPagAndIdEnteAndRange(v_avviso.id_tipo_avvpag, v_avviso.id_ente, ctx);

                //if (v_modalitaAvvisoEmissione == null)
                //{
                //    v_modalitaAvvisoEmissione = TabModalitaRateAvvPagViewBD.GetByIdServizioAndIdEnte(v_avviso.anagrafica_tipo_avv_pag.id_servizio, v_avviso.id_ente, ctx);
                //}

                if (v_modalitaAvvisoEmissione != null)
                {
                    v_salva_aux_digit_pagopa = string.IsNullOrEmpty(v_modalitaAvvisoEmissione.aux_digit_pagopa) ? v_ente.aux_digit_pagopa : v_modalitaAvvisoEmissione.aux_digit_pagopa;
                    v_salva_application_code_pagopa = string.IsNullOrEmpty(v_modalitaAvvisoEmissione.application_code_pagopa) ? v_ente.application_code_pagopa : v_modalitaAvvisoEmissione.application_code_pagopa;
                    v_salvacodice_segregazione_pagopa = string.IsNullOrEmpty(v_modalitaAvvisoEmissione.codice_segregazione_pagopa) ? v_ente.codice_segregazione_pagopa : v_modalitaAvvisoEmissione.codice_segregazione_pagopa;
                    v_salvacodice_struttura_ente_pagopa = string.IsNullOrEmpty(v_modalitaAvvisoEmissione.codice_struttura_ente_pagopa) ? v_ente.codice_struttura_ente_pagopa : v_modalitaAvvisoEmissione.codice_struttura_ente_pagopa;
                }
                else
                {
                    v_salva_aux_digit_pagopa = string.Empty;
                    v_salva_application_code_pagopa = string.Empty;
                    v_salvacodice_segregazione_pagopa = string.Empty;
                    v_salvacodice_struttura_ente_pagopa = string.Empty;
                }



                //if (v_modalitaAvvisoEmissione != null &&
                //    string.IsNullOrEmpty(v_modalitaAvvisoEmissione.aux_digit_pagopa))
                //{
                //    v_salva_aux_digit_pagopa = v_ente.aux_digit_pagopa;
                //    v_salva_application_code_pagopa = v_ente.application_code_pagopa;
                //    v_salvacodice_segregazione_pagopa = v_ente.codice_segregazione_pagopa;
                //    v_salvacodice_struttura_ente_pagopa = v_ente.codice_struttura_ente_pagopa;
                //}
                //else if (v_modalitaAvvisoEmissione != null &&
                //         !string.IsNullOrEmpty(v_modalitaAvvisoEmissione.aux_digit_pagopa))
                //{
                //    v_salva_aux_digit_pagopa = v_modalitaAvvisoEmissione.aux_digit_pagopa;
                //    v_salva_application_code_pagopa = v_modalitaAvvisoEmissione.application_code_pagopa;
                //    v_salvacodice_segregazione_pagopa = v_modalitaAvvisoEmissione.codice_segregazione_pagopa;
                //    v_salvacodice_struttura_ente_pagopa = v_modalitaAvvisoEmissione.codice_struttura_ente_pagopa;
                //}
                //else
                //{
                //    v_salva_aux_digit_pagopa = string.Empty;
                //    v_salva_application_code_pagopa = string.Empty;
                //    v_salvacodice_segregazione_pagopa = string.Empty;
                //    v_salvacodice_struttura_ente_pagopa = string.Empty;
                //}

                //if (!string.IsNullOrEmpty(v_ente.aux_digit_pagopa))
                //{
                //    //vuol dire che i dati per comporre il codice avviso e lo iuv di pagopa 
                //    //si trovano su anagrafica_ente perché SONO UGUALI per tutti i tipi servizio 
                //    //o i tipi avviso gestiti per l’ ENTE CREDITORE a cui l’avviso appartiene
                //    v_salva_aux_digit_pagopa = v_ente.aux_digit_pagopa;
                //    v_salva_application_code_pagopa = v_ente.application_code_pagopa;
                //    v_salvacodice_segregazione_pagopa = v_ente.codice_segregazione_pagopa;
                //    v_salvacodice_struttura_ente_pagopa = v_ente.codice_struttura_ente_pagopa;
                //}
                //else
                //{
                //    tab_modalita_rate_avvpag_view v_modalitaAvvisoEmissione = TabModalitaRateAvvPagViewBD.GetByIdTipoAvvPagAndIdEnteAndRange(v_avviso.id_tipo_avvpag, v_avviso.id_ente, ctx);
                //    v_salva_aux_digit_pagopa = v_modalitaAvvisoEmissione.aux_digit_pagopa;
                //    v_salva_application_code_pagopa = v_modalitaAvvisoEmissione.application_code_pagopa;
                //    v_salvacodice_segregazione_pagopa = v_modalitaAvvisoEmissione.codice_segregazione_pagopa;
                //    v_salvacodice_struttura_ente_pagopa = v_modalitaAvvisoEmissione.codice_struttura_ente_pagopa;
                //}

                int v_annoPagoPA = v_avviso.dt_emissione.HasValue ? v_avviso.dt_emissione.Value.Year : DateTime.Now.Year;
                int v_progressivoIuv = 0;
                string v_iuvBase = string.Empty;
                string v_codice_iuv_pagopa = string.Empty;
                string v_codice_avviso_pagopa = string.Empty;

                if (v_ente.flag_tipo_gestione_pagopa.Equals(anagrafica_ente.FLAG_TIPO_GESTIONE_PAGOPA_PUBLISERVIZI))
                {
                    v_progressivoIuv = TabProgIuvBD.IncrementaProgressivoCorrente("0000", v_annoPagoPA, ctx);
                    //il pagamento dell’avviso è gestito con pagopa su conto corrente di Publiservizi 
                    //cioè Publiservizi è Ente Creditore di pagopa
                    v_iuvBase = v_ente.codice_ente_pagopa +
                                //v_avviso.anagrafica_tipo_avv_pag.cod_tipo_avv_pag + //4 crt
                                v_annoPagoPA.ToString().Substring(2, 2) + //2crt
                                "0" + //cifra aggiuntiva
                                v_progressivoIuv.ToString("D7"); //7crt

                    v_codice_iuv_pagopa = v_salvacodice_segregazione_pagopa +
                                         v_iuvBase +
                                         calcolaCheckDigit(v_salva_aux_digit_pagopa + v_salvacodice_segregazione_pagopa + v_iuvBase);

                    v_codice_avviso_pagopa = v_salva_aux_digit_pagopa + v_codice_iuv_pagopa;
                }
                else if (v_ente.flag_tipo_gestione_pagopa.Equals(anagrafica_ente.FLAG_TIPO_GESTIONE_PAGOPA_PARTNERTECNOLOGICO))
                {
                    v_progressivoIuv = TabProgIuvBD.IncrementaProgressivoCorrente(v_avviso.anagrafica_tipo_avv_pag.cod_tipo_avv_pag, v_annoPagoPA, ctx);
                    //il pagamento dell’avviso è gestito con pagopa su conto corrente dell’Ente e 
                    //Publiservizi è partner tecnologico dell’ Ente cioè è direttamente l’Ente Creditore di pagopa       

                    v_iuvBase = v_avviso.anagrafica_tipo_avv_pag.cod_tipo_avv_pag + //4 crt
                                v_annoPagoPA.ToString().Substring(2, 2) + //2crt
                                v_progressivoIuv.ToString("D7"); //7crt

                    v_codice_iuv_pagopa = v_salvacodice_segregazione_pagopa +
                                          v_iuvBase +
                                          calcolaCheckDigit(v_salva_aux_digit_pagopa + v_salvacodice_segregazione_pagopa + v_iuvBase);

                    v_codice_avviso_pagopa = v_salva_aux_digit_pagopa + v_codice_iuv_pagopa;
                }
                else if (v_ente.flag_tipo_gestione_pagopa.Equals(anagrafica_ente.FLAG_TIPO_GESTIONE_PAGOPA_SUPPORTO))
                {
                    v_progressivoIuv = TabProgIuvBD.IncrementaProgressivoCorrente(v_avviso.anagrafica_tipo_avv_pag.cod_tipo_avv_pag, v_annoPagoPA, ctx);
                    //il pagamento dell’avviso è gestito con pagopa su conto corrente dell’Ente e 
                    //Publiservizi effettua attività di supporto per l’Ente creditore ma deve produrre 
                    //l’avviso analogico ed i bollettini pagopa 

                    if (v_salva_aux_digit_pagopa == "1" &&
                        !string.IsNullOrEmpty(v_salvacodice_segregazione_pagopa) &&
                        !v_salvacodice_segregazione_pagopa.Equals("0"))
                    {
                        v_iuvBase = v_salvacodice_segregazione_pagopa +
                                    v_avviso.anagrafica_tipo_avv_pag.cod_tipo_avv_pag + //4 crt
                                    v_annoPagoPA.ToString().Substring(2, 2) + //2crt
                                    v_progressivoIuv.ToString("D7"); //7crt

                        v_codice_iuv_pagopa = v_iuvBase;
                        v_codice_avviso_pagopa = v_salva_aux_digit_pagopa + v_codice_iuv_pagopa;

                    }
                    else if (v_salva_aux_digit_pagopa == anagrafica_ente.AUX_DIGIT_1 &&
                            !string.IsNullOrEmpty(v_salvacodice_struttura_ente_pagopa) &&
                            !v_salvacodice_struttura_ente_pagopa.Equals("0"))
                    {
                        v_iuvBase = v_salvacodice_struttura_ente_pagopa +
                                    v_avviso.anagrafica_tipo_avv_pag.cod_tipo_avv_pag + //4 crt
                                    v_annoPagoPA.ToString().Substring(2, 2) + //2crt
                                    v_progressivoIuv.ToString("D7"); //7crt

                        v_codice_iuv_pagopa = v_iuvBase;
                        v_codice_avviso_pagopa = v_salva_aux_digit_pagopa + v_codice_iuv_pagopa;
                    }
                    else if (v_salva_aux_digit_pagopa == anagrafica_ente.AUX_DIGIT_2 &&
                       !string.IsNullOrEmpty(v_salvacodice_struttura_ente_pagopa) &&
                       !v_salvacodice_struttura_ente_pagopa.Equals("0")
                       )
                    {
                        v_iuvBase = v_salvacodice_struttura_ente_pagopa +
                                    v_avviso.anagrafica_tipo_avv_pag.cod_tipo_avv_pag + //4 crt
                                    v_annoPagoPA.ToString().Substring(2, 2) + //2crt
                                    v_progressivoIuv.ToString("D7"); //7crt

                        v_codice_iuv_pagopa = v_iuvBase + calcolaCheckDigit(v_salva_aux_digit_pagopa + v_salvacodice_struttura_ente_pagopa + v_iuvBase);
                        v_codice_avviso_pagopa = v_salva_aux_digit_pagopa + v_codice_iuv_pagopa;
                    }
                    else if (v_salva_aux_digit_pagopa == anagrafica_ente.AUX_DIGIT_3)
                    {
                        v_iuvBase = v_avviso.anagrafica_tipo_avv_pag.cod_tipo_avv_pag + //4 crt
                                    v_annoPagoPA.ToString().Substring(2, 2) + //2crt
                                    v_progressivoIuv.ToString("D7"); //7crt

                        v_codice_iuv_pagopa = v_salvacodice_segregazione_pagopa +
                                              v_iuvBase +
                                              calcolaCheckDigit(v_salva_aux_digit_pagopa + v_salvacodice_segregazione_pagopa +
                                              v_iuvBase);

                        v_codice_avviso_pagopa = v_salva_aux_digit_pagopa + v_codice_iuv_pagopa;
                    }
                    else
                    {

                    }
                }

                v_iuv = v_codice_iuv_pagopa;
                v_codicepagamento = v_codice_avviso_pagopa;
            }
        }

        /// <summary>
        /// Restituisce il codice CBILL o
        /// il codice di tassonomia PagoPA
        /// </summary>
        /// <param name="v_avviso"></param>
        /// <param name="v_rata"></param>
        /// <param name="isCbill"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static string calcolaCBillOrCodiceTassonomiaPagoPA(tab_avv_pag v_avviso, bool isCbill, dbEnte ctx)
        {
            string v_returnValue = string.Empty;
            int id_entrata_avvpag = 0;
            anagrafica_ente v_ente = AnagraficaEnteBD.GetById(v_avviso.id_ente, ctx);

            if (string.IsNullOrEmpty(v_ente.flag_tipo_gestione_pagopa) || v_ente.flag_tipo_gestione_pagopa.Equals("0"))
                return v_returnValue;

            if (isCbill)
            {
                v_returnValue = v_ente.CBILL;
            }
            else
            {
                //nel caso della rateizzazione l'entrata viene presa dall'avviso rateizzato
                if (v_avviso.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.SERVIZI_RATEIZZAZIONE_COA ||
                    v_avviso.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.SERVIZI_RATEIZZAZIONE_NON_COATTIVI)
                {
                    id_entrata_avvpag = v_avviso.tab_unita_contribuzione.Where(u => u.id_tipo_voce_contribuzione == 19).FirstOrDefault().tab_avv_pag1.id_entrata;
                }
                else
                {
                    anagrafica_tipo_avv_pag v_tipo_avvpag = AnagraficaTipoAvvPagBD.GetById(v_avviso.id_tipo_avvpag, ctx);
                    if (v_tipo_avvpag.id_entrata_avvpag_collegati.HasValue && v_tipo_avvpag.id_entrata_avvpag_collegati.Value != 12)
                        id_entrata_avvpag = v_tipo_avvpag.id_entrata_avvpag_collegati.Value;
                    else
                        id_entrata_avvpag = v_tipo_avvpag.id_entrata;
                }

                tab_tassonomia_pagopa v_tass_pagopa = TabTassonomiaPagoPaBD.GetByIdTipoEnteAndIdEntrata(v_ente.id_tipo_ente.Value, id_entrata_avvpag, ctx);
                v_returnValue = v_tass_pagopa.Codice_tassonomia_pagopa;
            }


            return v_returnValue;
        }
        /// <summary>
        /// Restituisce l'entità a partire dalla chiave primaria
        /// </summary>
        /// <param name="p_id">Chiave primaria</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public async static Task<tab_avv_pag> GetByIdAsync(Int32 p_id, dbEnte p_dbContext)
        {
            return await GetList(p_dbContext).SingleOrDefaultAsync(c => c.id_tab_avv_pag == p_id);
        }


        public static string calcolaCheckDigit(string p_code)
        {
            long chk = Convert.ToInt64(p_code);
            chk = chk % 93;
            string ret = chk.ToString();
            if (ret.Length == 0)
            {
                ret = "00";
            }
            else if (ret.Length == 1)
            {
                ret = "0" + ret;
            }
            return ret;
        }



    }
}
