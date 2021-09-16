using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.LinqExtended;
using System.Data.Entity;
using Publisoftware.Utility.Log;
using System.Transactions;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace Publisoftware.Data.BD
{
    public class TabListeBD : EntityBD<tab_liste>
    {
        private static ILogger m_logger = LoggerFactory.getInstance().getLogger<NLogger>("TabListeBD");

        public class ControlloListeOUT
        {
            public ControlloListeOUT() { segnalazioni = new List<string>(); }

            public int count_avvisi_annullati { get; set; }
            public int count_avvisi_validi { get; set; }

            public decimal tot_imp_avvisi_annullati { get; set; }
            public decimal tot_imp_avvisi_validi { get; set; }
            public decimal tot_imp_avvisi_validi_imponibile { get; set; }
            public decimal tot_imp_avvisi_validi_iva { get; set; }

            public List<string> segnalazioni { get; set; }
        }

        public TabListeBD()
        {

        }

        public static IQueryable<tab_liste> GetListByTipo(int p_idTipoLista, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).diTipo(p_idTipoLista);
        }

        public static tab_liste CreaListaEmissione(int p_idEnte, int p_idEntrata, int p_idTipoLista, int p_idTipoAvvPag, int p_progressivoLista, DateTime p_dataLista, string p_annoRiferimento,
                                        string p_parametri, dbEnte p_dbContext)
        {
            return CreaLista(p_idEnte, p_idEntrata, p_idTipoLista, p_idTipoAvvPag, p_progressivoLista, p_dataLista, p_annoRiferimento,
                                       p_parametri, tab_liste.PRE_PRE, p_dbContext);
        }

        public static string CostruisciIdentificativoLista(int idEnte, int idTipoLista, string annoRiferimento, int progressivoLista, dbEnte dbContext)
        {
            string codEnte = AnagraficaEnteBD.GetById(idEnte, dbContext).cod_ente.PadLeft(6, '0');
            string codTipoLista = TabTipoListaBD.GetById(idTipoLista, dbContext).cod_lista.PadLeft(3, '0');

            // es.:"030000EME20180000005":
            //                   "030000"+"EME"        +"2018"                          +"0000005"           
            return String.Concat(codEnte, codTipoLista, annoRiferimento.PadLeft(4, '0'), progressivoLista.ToString().PadLeft(7, '0'));
        }

        public static string CostruisciDescrizioneLista(int idTipoLista, int idTipoAvvPag, dbEnte dbContext)
        {
            return $"{TabTipoListaBD.GetById(idTipoLista, dbContext).desc_lista} {AnagraficaTipoAvvPagBD.GetByIdTipoAvvPag(idTipoAvvPag, dbContext).descr_tipo_avv_pag}";
        }

        public static tab_liste CreaLista(int p_idEnte, int p_idEntrata, int p_idTipoLista, int p_idTipoAvvPag, int p_progressivoLista, DateTime p_dataLista, string p_annoRiferimento,
                                        string p_parametri, string cod_stato, dbEnte p_dbContext)
        {

            //string v_codEnte = AnagraficaEnteBD.GetById(p_idEnte, p_dbContext).cod_ente.PadLeft(6, '0');
            //string v_codTipoLista = TabTipoListaBD.GetById(p_idTipoLista, p_dbContext).cod_lista.PadLeft(3, '0');
            //string v_annoRiferimento = p_annoRiferimento.PadLeft(4, '0');
            //string v_progressivoLista = p_progressivoLista.ToString().PadLeft(7, '0');
            string identificativoLista = CostruisciIdentificativoLista(
                idEnte: p_idEnte,
                idTipoLista: p_idTipoLista,
                annoRiferimento: p_annoRiferimento,
                progressivoLista: p_progressivoLista,
                dbContext: p_dbContext);

            // string v_descrizione = $"{TabTipoListaBD.GetById(p_idTipoLista, p_dbContext).desc_lista} {AnagraficaTipoAvvPagBD.GetByIdTipoAvvPag(p_idTipoAvvPag, p_dbContext).descr_tipo_avv_pag}";
            string v_descrizione = CostruisciDescrizioneLista(idTipoLista: p_idTipoLista, idTipoAvvPag: p_idTipoAvvPag, dbContext: p_dbContext);
            tab_liste lista = new tab_liste()
            {
                id_ente = p_idEnte,
                id_entrata = p_idEntrata,
                id_tipo_lista = p_idTipoLista,
                numero_lista = identificativoLista, //v_codEnte + v_codTipoLista + v_annoRiferimento + v_progressivoLista,
                anno_rif = p_annoRiferimento,
                descr_lista = v_descrizione,
                data_lista = p_dataLista.Date,
                id_tipo_avv_pag = p_idTipoAvvPag,
                parametri_calcolo = p_parametri,
                cod_stato = cod_stato,
                identificativo_lista = identificativoLista //v_codEnte + v_codTipoLista + v_annoRiferimento + v_progressivoLista,
            };

            p_dbContext.tab_liste.Add(lista);
            return lista;
        }


        public static tab_liste CreaListaWithDataLista(int p_idEnte, int p_idEntrata, int p_idTipoLista, int p_idTipoAvvPag, int p_progressivoLista, DateTime p_dataLista, string p_annoRiferimento,
                                string p_parametri, string cod_stato, DateTime p_data_lista, dbEnte p_dbContext)
        {

            //string v_codEnte = AnagraficaEnteBD.GetById(p_idEnte, p_dbContext).cod_ente.PadLeft(6, '0');
            //string v_codTipoLista = TabTipoListaBD.GetById(p_idTipoLista, p_dbContext).cod_lista.PadLeft(3, '0');
            //string v_annoRiferimento = p_annoRiferimento.PadLeft(4, '0');
            //string v_progressivoLista = p_progressivoLista.ToString().PadLeft(7, '0');
            string identificativoLista = CostruisciIdentificativoLista(
                idEnte: p_idEnte,
                idTipoLista: p_idTipoLista,
                annoRiferimento: p_annoRiferimento,
                progressivoLista: p_progressivoLista,
                dbContext: p_dbContext);

            // string v_descrizione = $"{TabTipoListaBD.GetById(p_idTipoLista, p_dbContext).desc_lista} {AnagraficaTipoAvvPagBD.GetByIdTipoAvvPag(p_idTipoAvvPag, p_dbContext).descr_tipo_avv_pag}";
            string v_descrizione = CostruisciDescrizioneLista(idTipoLista: p_idTipoLista, idTipoAvvPag: p_idTipoAvvPag, dbContext: p_dbContext);
            tab_liste lista = new tab_liste()
            {
                id_ente = p_idEnte,
                id_entrata = p_idEntrata,
                id_tipo_lista = p_idTipoLista,
                numero_lista = identificativoLista, //v_codEnte + v_codTipoLista + v_annoRiferimento + v_progressivoLista,
                anno_rif = p_annoRiferimento,
                descr_lista = v_descrizione,
                data_lista = p_data_lista,
                id_tipo_avv_pag = p_idTipoAvvPag,
                parametri_calcolo = p_parametri,
                cod_stato = cod_stato,
                //data_creazione=p_data_creazione,
                identificativo_lista = identificativoLista //v_codEnte + v_codTipoLista + v_annoRiferimento + v_progressivoLista,
            };

            p_dbContext.tab_liste.Add(lista);
            return lista;
        }

        public static IQueryable<tab_liste> GetListRateizzazioniAnnoCorrente(dbEnte p_dbContext)
        {
            return GetList(p_dbContext)
                    .Where(l => l.data_lista.Year == DateTime.Now.Year).OrderByDescending(l => l.id_lista)
                    .Where(l => l.tab_tipo_lista.cod_lista == tab_tipo_lista.TIPOLISTA_RATEIZZAZIONE);
        }
        public static IQueryable<tab_liste> GetListEmissioneAnnoCorrente(int p_IdTipoLista,dbEnte p_dbContext)
        {
            return GetList(p_dbContext)
                    .Where(l => l.data_lista.Year == DateTime.Now.Year).OrderByDescending(l => l.id_lista)
                    .Where(l => l.tab_tipo_lista.id_tipo_lista == p_IdTipoLista)
                    .Where(l => l.cod_stato == tab_liste.DEF_STA);
        }
        public static IQueryable<tab_liste> GetListEmissioneByDataLista(int p_IdTipoLista, DateTime p_data_lista, dbEnte p_dbContext)
        {
            return GetList(p_dbContext)
                    .Where(l => l.data_lista == p_data_lista).OrderByDescending(l => l.id_lista)
                    .Where(l => l.tab_tipo_lista.id_tipo_lista == p_IdTipoLista);
                    //.Where(l => l.cod_stato == tab_liste.DEF_STA);
        }
        public static IQueryable<tab_liste> GetListEmissioneByAnnoRiferimento(int p_IdTipoLista,int p_annoRif, dbEnte p_dbContext)
        {
            return GetList(p_dbContext)
                    .Where(l => l.data_lista.Year == p_annoRif).OrderByDescending(l => l.id_lista)
                    .Where(l => l.tab_tipo_lista.id_tipo_lista == p_IdTipoLista)
                    .Where(l => l.cod_stato == tab_liste.DEF_STA);
        }
        public static IQueryable<tab_liste> GetListDefinizioneAgevolataAnnoCorrente(dbEnte p_dbContext)
        {
            return GetList(p_dbContext)
                    .Where(l => l.data_lista.Year == DateTime.Now.Year).OrderByDescending(l => l.id_lista)
                    .Where(l => l.tab_tipo_lista.cod_lista == tab_tipo_lista.TIPOLISTA_LISTA_DI_DEFINIZIONE_AGEVOLATA);
        }


        public static bool CheckIdentificativo(string p_identificativo, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Any(d => d.identificativo_lista.ToUpper().Trim().Equals(p_identificativo.ToUpper().Trim()));
        }

        public static tab_liste GetByIdentificativo(string p_identificativo, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.identificativo_lista.ToUpper().Trim().Equals(p_identificativo.ToUpper().Trim())).FirstOrDefault();
        }

        public static bool controlloAutomaticoListeTrasmissione(int p_idLista, dbEnte p_dbContext)
        {
            p_dbContext.Database.Log = msg => System.Diagnostics.Debug.WriteLine(msg);

            ControlloListeOUT ctrlOut = new ControlloListeOUT();

            try
            {
                ctrlOut = ControlloLista(p_idLista, p_dbContext);
            }
            catch (Exception ex)
            {
                m_logger.LogException("Errore nell'esecuzione dei controlli lista: ", ex, Utility.Log.EnLogSeverity.Fatal);
                return false;
            }

            dbEnte savingCtx = p_dbContext;

            tab_liste selLista = TabListeBD.GetById(p_idLista, savingCtx);

            //Avvisi con stato da aggiornare
            //Gli avvisi annullati nei controlli sono inseriti in tab_avvisi_anomali_liste_carico => ne prelevo gli ID
            IQueryable<int> AvvisiAnomaliIds = TabAvvisiAnomaliListeCaricoBD.GetListByListaEmissione(selLista.id_ente, p_idLista, savingCtx)
                .Where(x => x.id_avv_pag_fatt_emissione != null)
                .Select(aa => aa.id_avv_pag_fatt_emissione.Value).Distinct();
            //Prendo da avvpag_fatt_emissione gli avvisi anomali che non sono stati già annullati....
            List<tab_avv_pag_fatt_emissione> lstAvvisiAnomali = TabAvvPagFattEmissioneBD.GetList(savingCtx).Where(afe => AvvisiAnomaliIds.Contains(afe.id_tab_avv_pag))
                                                                    .Where(afe => !afe.cod_stato.Equals(anagrafica_stato_avv_pag.ANNULLATO_ANNULLATO))
                                                                    .ToList();
            //...annullo l'avviso anomalo cambiandogli stato su fatt_emissione
            lstAvvisiAnomali.ForEach(currAvvFE =>
            {
                currAvvFE.cod_stato = anagrafica_stato_avv_pag.ANNULLATO_ANNULLATO;
                currAvvFE.id_stato = anagrafica_stato_avv_pag.ANNULLATO_ANNULLATO_ID;
            });

            var v_group = selLista.tab_avv_pag_fatt_emissione.GroupBy(d => d.cod_stato).Select(g => new { g.Key, count = g.Count(), totale = g.Sum(t => t.imp_tot_avvpag_rid) }).OrderBy(o => o.Key);


            int avvValidi = 0;
            decimal avvValidiImp = 0;
            int avvAnnullati = 0;
            decimal avvAnnullatiImp = 0;


            foreach (var v_rec in v_group)
            {
                if (v_rec.Key.Contains(CodStato.VAL))
                {
                    avvValidi = v_rec.count;
                    avvValidiImp = v_rec.totale.Value; ;
                }
                else if (v_rec.Key.Contains(CodStato.ANN))
                {
                    avvAnnullati = v_rec.count;
                    avvAnnullatiImp = v_rec.totale.Value; ;
                }

            }
            int avvControllati = avvValidi + avvAnnullati;
            decimal avvControllatiImp = avvValidiImp + avvAnnullatiImp;


            //Aggiornamento statistiche lista tenendo conto delle anomalie riscontrate
            selLista.num_avvpag = avvControllati.ToString();
            selLista.imp_tot_lista = avvControllatiImp;
            //selLista.imp_tot_imponibile = ctrlOut.tot_imp_avvisi_validi_imponibile;
            //selLista.imp_tot_iva = ctrlOut.tot_imp_avvisi_validi_iva;


            //Aggiorno stato lista controllata
            selLista.cod_stato = tab_liste.PRE_DEF;

            //Aggiornamento flag tab_validazione_approvazione
            tab_validazione_approvazione_liste tValLista = TabValidazioneApprovazioneListeBD.GetValidForLista(selLista.id_lista, savingCtx);
            if (tValLista == null)
            {
                tValLista = new tab_validazione_approvazione_liste
                {
                    cod_stato = tab_validazione_approvazione_liste.ATT_ATT
                };

                tab_modalita_rate_avvpag_view v_modRate = selLista.custom_prop_modalita_rateizzazione_ing_fisc;
                tValLista.setProperties(v_modRate, true);

                selLista.tab_validazione_approvazione_liste.Add(tValLista);
            }

            tValLista.flag_controlli_anagrafici = "2";
            tValLista.flag_validazione_avvisi = "2";

            try
            {
                savingCtx.SaveChanges();
            }
            catch (Exception ex)
            {
                m_logger.LogException("Errore nell'esecuzione degli aggiornamenti post controlli: ", ex, Utility.Log.EnLogSeverity.Fatal);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Calcola statistiche su avv. annullati e validi dopo esecuzione controlli
        /// </summary>
        /// <param name="p_idEnte"></param>
        /// <param name="p_idLista"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static ControlloListeOUT GetStatisticheControlliLista(int p_idEnte, int p_idLista, dbEnte p_dbContext)
        {
            ControlloListeOUT retObj = new ControlloListeOUT();

            IQueryable<tab_avv_pag_fatt_emissione> AvvisiAnnullati = TabAvvisiAnomaliListeCaricoBD.GetListByListaEmissione(p_idEnte, p_idLista, p_dbContext)
                                                                           .Where(a => a.cod_stato.StartsWith(CodStato.ANN))
                                                                           .Select(a => a.tab_avv_pag_fatt_emissione).Distinct();

            //IQueryable<tab_avv_pag_fatt_emissione> AvvisiAnomali = TabAvvPagFattEmissioneBD.GetListByListaEmissione(p_idLista, p_dbContext).Where(afe => AvvisiControllatiIds.Contains(afe.id_tab_avv_pag)).AsNoTracking();
            retObj.count_avvisi_annullati = AvvisiAnnullati.Count();
            retObj.tot_imp_avvisi_annullati = AvvisiAnnullati.Where(aa => aa.imp_tot_avvpag_rid.HasValue).Select(aa => aa.imp_tot_avvpag_rid.Value).DefaultIfEmpty(0).Sum();

            IQueryable<tab_avv_pag_fatt_emissione> AvvisiOK = TabAvvisiAnomaliListeCaricoBD.GetListByListaEmissione(p_idEnte, p_idLista, p_dbContext)
                                                                           .Where(a => !a.cod_stato.StartsWith(CodStato.ANN))
                                                                           .Select(a => a.tab_avv_pag_fatt_emissione).Distinct();

            retObj.count_avvisi_validi = AvvisiOK.Count();
            retObj.tot_imp_avvisi_validi = AvvisiOK.Where(a => a.imp_tot_avvpag_rid.HasValue).Select(a => a.imp_tot_avvpag_rid.Value).DefaultIfEmpty(0).Sum();

            //Campo imp_tot_imponibile?
            retObj.tot_imp_avvisi_validi_imponibile = AvvisiOK.Where(a => a.imp_imp_entr_avvpag.HasValue).Select(a => a.imp_imp_entr_avvpag.Value).DefaultIfEmpty(0).Sum();
            retObj.tot_imp_avvisi_validi_iva = AvvisiOK.Where(a => a.imp_iva_avvpag.HasValue).Select(a => a.imp_iva_avvpag.Value).DefaultIfEmpty(0).Sum();

            return retObj;
        }

        public static ControlloListeOUT GetStatisticheControlliListaOLD(int p_idEnte, int p_idLista, dbEnte p_dbContext)
        {
            ControlloListeOUT retObj = new ControlloListeOUT();

            IQueryable<int> AvvisiAnomaliIds = TabAvvisiAnomaliListeCaricoBD.GetListByListaEmissione(p_idEnte, p_idLista, p_dbContext)
                .Where(x => x.id_avv_pag_fatt_emissione != null)
                .Select(aa => aa.id_avv_pag_fatt_emissione.Value).Distinct();

            IQueryable<tab_avv_pag_fatt_emissione> AvvisiAnomali = TabAvvPagFattEmissioneBD.GetListByListaEmissione(p_idLista, p_dbContext).Where(afe => AvvisiAnomaliIds.Contains(afe.id_tab_avv_pag)).AsNoTracking();
            retObj.count_avvisi_annullati = AvvisiAnomali.Count();
            retObj.tot_imp_avvisi_annullati = AvvisiAnomali.Where(aa => aa.imp_tot_avvpag_rid.HasValue).Select(aa => aa.imp_tot_avvpag_rid.Value).DefaultIfEmpty(0).Sum();

            IQueryable<tab_avv_pag_fatt_emissione> AvvisiOk = TabAvvPagFattEmissioneBD.GetListByListaEmissione(p_idLista, p_dbContext).WhereByCodStato(CodStato.VAL).Where(afe => !AvvisiAnomaliIds.Contains(afe.id_tab_avv_pag)).AsNoTracking();
            retObj.count_avvisi_validi = AvvisiOk.Count();
            retObj.tot_imp_avvisi_validi = AvvisiOk.Where(a => a.imp_tot_avvpag_rid.HasValue).Select(a => a.imp_tot_avvpag_rid.Value).DefaultIfEmpty(0).Sum();
            //Campo imp_tot_imponibile?
            retObj.tot_imp_avvisi_validi_imponibile = AvvisiOk.Where(a => a.imp_imp_entr_avvpag.HasValue).Select(a => a.imp_imp_entr_avvpag.Value).DefaultIfEmpty(0).Sum();
            retObj.tot_imp_avvisi_validi_iva = AvvisiOk.Where(a => a.imp_iva_avvpag.HasValue).Select(a => a.imp_iva_avvpag.Value).DefaultIfEmpty(0).Sum();

            return retObj;
        }

        public static ControlloListeOUT ControlloLista(int p_idLista, dbEnte p_dbContext)
        {
            p_dbContext.Database.Log = msg => System.Diagnostics.Debug.WriteLine(msg);

            ControlloListeOUT retObj = new ControlloListeOUT();

            List<string> retMsgs = new List<string>();

            //Dictionary che associa ad ogni codice di controllo previsto la relativa query: come funzione che riceve in input una tab_lista e il contesto e restituisce in output la lista degli avvisi sui quali è riscontrata quella particolare anomalia
            #region "Definizione query di controllo automatico"
            Dictionary<string, Func<tab_liste, dbEnte, IQueryable<tab_avv_pag_fatt_emissione>>> dictSelezioniAvvisi = new Dictionary<string, Func<tab_liste, dbEnte, IQueryable<tab_avv_pag_fatt_emissione>>>
            {
                [anagrafica_controlli_qualita_emissione_avvpag.CODE_DATNOT] = (inLista, inCtx) =>
                {
                    if (inLista.anagrafica_tipo_avv_pag.flag_notifica != "1") return new List<tab_avv_pag_fatt_emissione>().AsQueryable();
                    return TabAvvPagFattEmissioneBD.GetListByListaEmissione(inLista.id_lista, inCtx).NonNotificati();
                },
                [anagrafica_controlli_qualita_emissione_avvpag.CODE_MAXNOT] = (inLista, inCtx) =>
                {
                    if (inLista.anagrafica_tipo_avv_pag.flag_notifica != "1") return new List<tab_avv_pag_fatt_emissione>().AsQueryable();

                    tab_modalita_rate_avvpag_view modRate = inLista.custom_prop_modalita_rateizzazione_ing_fisc;
                    if (modRate == null || !modRate.GG_massimi_data_notifica.HasValue)
                    {
                        return new List<tab_avv_pag_fatt_emissione>().AsQueryable();
                    }

                    int salva_gg_massimi_data_notifica = modRate.custom_prop_gg_max_data_notifica;
                    DateTime dataDa = DateTime.Now.AddDays(-salva_gg_massimi_data_notifica);

                    return TabAvvPagFattEmissioneBD.GetListByListaEmissione(inLista.id_lista, inCtx).Where(afe => afe.data_avvenuta_notifica.HasValue && afe.data_avvenuta_notifica.Value < dataDa);
                },
                [anagrafica_controlli_qualita_emissione_avvpag.CODE_IMPMIN] = (inLista, inCtx) =>
                {
                    tab_modalita_rate_avvpag_view modRate = inLista.custom_prop_modalita_rateizzazione_ing_fisc;
                    decimal impMinimo = 0;
                    if (modRate != null && modRate.importo_minimo_da_pagare.HasValue)
                    {
                        impMinimo = modRate.importo_minimo_da_pagare.Value;
                    }
                    else
                    {
                        return new List<tab_avv_pag_fatt_emissione>().AsQueryable();
                    }

                    return TabAvvPagFattEmissioneBD.GetListByListaEmissione(inLista.id_lista, inCtx).Where(afe => afe.importo_tot_da_pagare < impMinimo);
                },

                [anagrafica_controlli_qualita_emissione_avvpag.CODE_CONFIS] = (inLista, inCtx) =>
                {
                    IQueryable<tab_avv_pag_fatt_emissione> v_listaPersoneFisicheSencaCF =

                    TabAvvPagFattEmissioneBD.GetListByListaEmissione(inLista.id_lista, inCtx).WhereByContribuenteIdTipoContribuente(anagrafica_tipo_contribuente.PERS_FISICA_ID)
                        .Where(avv => string.IsNullOrEmpty(avv.tab_contribuente.cod_fiscale) || avv.tab_contribuente.cod_fiscale.Trim().Length != 16);

                    return v_listaPersoneFisicheSencaCF;
                },


                [anagrafica_controlli_qualita_emissione_avvpag.CODE_AVVPRE] = (inLista, inCtx) =>
                {
                    tab_modalita_rate_avvpag_view modRate = inLista.custom_prop_modalita_rateizzazione_ing_fisc;
                    int anni_prescizione_avv = DateTime.Now.Year;
                    if (modRate != null && modRate.AA_prescrizione_avviso.HasValue)
                    {
                        anni_prescizione_avv = modRate.AA_prescrizione_avviso.Value;
                    }
                    else
                    {
                        return new List<tab_avv_pag_fatt_emissione>().AsQueryable();
                    }

                    int annoMax = DateTime.Now.Year - anni_prescizione_avv;
                    string annoMaxStr = annoMax.ToString();

                    IQueryable<int> TipiVoceENTids = TabTipoVoceContribuzioneBD.GetList(inCtx).Where(tv => tv.codice_tributo_ministeriale == tab_tipo_voce_contribuzione.CODICE_ENT).Select(tv => tv.id_tipo_voce_contribuzione);

                    return TabAvvPagFattEmissioneBD.GetListByListaEmissione(inLista.id_lista, inCtx)
                        .Where(afe => afe.tab_unita_contribuzione_fatt_emissione.Any(ufe => TipiVoceENTids.Contains(ufe.id_tipo_voce_contribuzione) && annoMaxStr.CompareTo(ufe.anno_rif) > 0));
                },
                [anagrafica_controlli_qualita_emissione_avvpag.CODE_DECSAN] = (inLista, inCtx) =>
                {
                    IQueryable<int> TipiVoceSANids = TabTipoVoceContribuzioneBD.GetList(inCtx).Where(tv => tv.codice_tributo_ministeriale == tab_tipo_voce_contribuzione.CODICE_SANZIONI).Select(tv => tv.id_tipo_voce_contribuzione);

                    return TabAvvPagFattEmissioneBD.GetListByListaEmissione(inLista.id_lista, inCtx).WhereByContribuenteIdTipoContribuente(anagrafica_tipo_contribuente.PERS_FISICA_ID)
                        .Where(afe => afe.tab_contribuente.cod_stato_contribuente.StartsWith(anagrafica_stato_contribuente.DEC))
                        .Where(afe => afe.tab_unita_contribuzione_fatt_emissione.Any(ufe => TipiVoceSANids.Contains(ufe.id_tipo_voce_contribuzione)));
                },
                [anagrafica_controlli_qualita_emissione_avvpag.CODE_CONDEC] = (inLista, inCtx) =>
                {
                    return TabAvvPagFattEmissioneBD.GetListByListaEmissione(inLista.id_lista, inCtx).WhereByContribuenteIdTipoContribuente(anagrafica_tipo_contribuente.PERS_FISICA_ID)
                        .Where(afe => afe.tab_contribuente.cod_stato_contribuente.Equals(anagrafica_stato_contribuente.DEC_DEC))
                        //non ho coobligati
                        .Where(afe => !afe.tab_contribuente.join_referente_contribuente.Any(jrc => (jrc.flag_coobbligato == tab_referente.COOBBLIGATO || jrc.flag_coobbligato == tab_referente.COOBBLIGATO_PARZIALE || jrc.flag_coobbligato == tab_referente.GARANTE) && jrc.cod_stato.StartsWith(CodStato.ATT)));
                },
                [anagrafica_controlli_qualita_emissione_avvpag.CODE_CONSCO] = (inLista, inCtx) =>
                {
                    return TabAvvPagFattEmissioneBD.GetListByListaEmissione(inLista.id_lista, inCtx)
                        .Where(afe => afe.tab_contribuente.cod_stato_contribuente.Contains(CodStato.ATT) && afe.tab_contribuente.flag_irreperibilita_definitiva == "1");
                },
                [anagrafica_controlli_qualita_emissione_avvpag.CODE_REFSCO] = (inLista, inCtx) =>
                {
                    IQueryable<int> avvisiConSoggDebIrreperibileIds = JoinAvvPagFattEmissioneSoggettoDebitoreBD.GetList(inCtx).Where(j => j.id_tab_avv_pag.HasValue && j.id_join_referente_contribuente.HasValue && j.cod_stato.StartsWith(CodStato.ATT) && j.join_referente_contribuente.tab_referente.flag_irreperibilita_definitiva == "1").Select(j => j.id_tab_avv_pag.Value);
                    return TabAvvPagFattEmissioneBD.GetListByListaEmissione(inLista.id_lista, inCtx).Where(afe => avvisiConSoggDebIrreperibileIds.Contains(afe.id_tab_avv_pag));
                },
                [anagrafica_controlli_qualita_emissione_avvpag.CODE_TERSCO] = (inLista, inCtx) =>
                {
                    IQueryable<int> avvisiConSoggDebIrreperibileIds = JoinAvvPagFattEmissioneSoggettoDebitoreBD.GetList(inCtx).Where(j => j.id_tab_avv_pag.HasValue && j.id_terzo_debitore.HasValue && j.cod_stato.StartsWith(CodStato.ATT) && j.tab_terzo.flag_irreperibilita_definitiva == "1").Select(j => j.id_tab_avv_pag.Value);
                    return TabAvvPagFattEmissioneBD.GetListByListaEmissione(inLista.id_lista, inCtx).Where(afe => avvisiConSoggDebIrreperibileIds.Contains(afe.id_tab_avv_pag));
                },
                [anagrafica_controlli_qualita_emissione_avvpag.CODE_CONEMI] = (inLista, inCtx) =>
                {
                    return TabAvvPagFattEmissioneBD.GetListByListaEmissione(inLista.id_lista, inCtx)
                        .Where(afe => afe.tab_contribuente.cod_stato_contribuente.Contains(CodStato.ATT) && afe.tab_contribuente.flag_ricerca_indirizzo_emigrazione == "1");
                },
                [anagrafica_controlli_qualita_emissione_avvpag.CODE_REFEMI] = (inLista, inCtx) =>
                {
                    IQueryable<int> avvisiConSoggDebEmigratoIds = JoinAvvPagFattEmissioneSoggettoDebitoreBD.GetList(inCtx).Where(j => j.id_tab_avv_pag.HasValue && j.id_join_referente_contribuente.HasValue && j.cod_stato.StartsWith(CodStato.ATT) && j.join_referente_contribuente.tab_referente.flag_ricerca_indirizzo_emigrazione == "1").Select(j => j.id_tab_avv_pag.Value);
                    return TabAvvPagFattEmissioneBD.GetListByListaEmissione(inLista.id_lista, inCtx).Where(afe => avvisiConSoggDebEmigratoIds.Contains(afe.id_tab_avv_pag)); ;
                }
            };
            #endregion

            tab_liste selLista = TabListeBD.GetById(p_idLista, p_dbContext);
            if (selLista == null)
                throw new Exception(String.Format("Lista id:{0} non trovata nel DB.", p_idLista));

            anagrafica_tipo_avv_pag tipoAvv_selLista = selLista.anagrafica_tipo_avv_pag;
            if (tipoAvv_selLista == null)
                throw new Exception(String.Format("Tipo avviso per lista id:{0} non trovato nel DB.", p_idLista));

            //Prelevo controlli che vanno eseguiti per la lista in input
            IQueryable<anagrafica_controlli_qualita_emissione_avvpag> ControlliDaEseguire = AnagraficaControlliQualitaEmissioneAvvPagBD.getControlliFor(selLista.id_tipo_lista, selLista.id_ente, selLista.id_entrata, tipoAvv_selLista.id_servizio, p_dbContext).AsNoTracking();

            List<tab_avvisi_anomali_liste_carico> lstAnomalieRiscontrate = new List<tab_avvisi_anomali_liste_carico>();
            //Ciclo su controlli: per ogni controllo da eseguire invoco la query relativa come l'elemento della dictionary che ha quel particolare codice di controllo 
            //ESEMPIO: dictSelezioniAvvisi[currCtrl.codice_controllo](selLista, p_dbContext)
            ControlliDaEseguire.ToList().ForEach(currCtrl =>
            {
                if (tab_tipo_lista.TIPOLISTA_TRASMISSIONE == selLista.tab_tipo_lista.cod_lista
                    &&
                    currCtrl.codice_controllo != anagrafica_controlli_qualita_emissione_avvpag.CODE_DATNOT
                    &&
                    currCtrl.codice_controllo != anagrafica_controlli_qualita_emissione_avvpag.CODE_MAXNOT
                    &&
                    currCtrl.codice_controllo != anagrafica_controlli_qualita_emissione_avvpag.CODE_IMPMIN
                    &&
                    currCtrl.codice_controllo != anagrafica_controlli_qualita_emissione_avvpag.CODE_AVVPRE
                    &&
                    currCtrl.codice_controllo != anagrafica_controlli_qualita_emissione_avvpag.CODE_DECSAN
                    &&
                    currCtrl.codice_controllo != anagrafica_controlli_qualita_emissione_avvpag.CODE_CONDEC
                    &&
                    currCtrl.codice_controllo != anagrafica_controlli_qualita_emissione_avvpag.CODE_CONFIS
                    )
                {
                    retMsgs.Add(String.Format("lista trasmissione:{0} codice controllo qualità non gestito ({1}):{2}", selLista.id_lista, currCtrl.codice_controllo, currCtrl.descrizione_controllo));
                    return;
                }

                //per le liste di emissione il controllo automatico non viene effettuato
                //if (tab_tipo_lista.TIPOLISTA_EMISSIONE == selLista.tab_tipo_lista.cod_lista
                //    &&
                //    currCtrl.codice_controllo != anagrafica_controlli_qualita_emissione_avvpag.CODE_REFEMI
                //    &&
                //    currCtrl.codice_controllo != anagrafica_controlli_qualita_emissione_avvpag.CODE_CONEMI
                //    &&
                //    currCtrl.codice_controllo != anagrafica_controlli_qualita_emissione_avvpag.CODE_TERSCO
                //    &&
                //    currCtrl.codice_controllo != anagrafica_controlli_qualita_emissione_avvpag.CODE_REFSCO
                //    &&
                //    currCtrl.codice_controllo != anagrafica_controlli_qualita_emissione_avvpag.CODE_CONSCO
                //    &&
                //    currCtrl.codice_controllo != anagrafica_controlli_qualita_emissione_avvpag.CODE_DECSAN
                //    &&
                //    currCtrl.codice_controllo != anagrafica_controlli_qualita_emissione_avvpag.CODE_CONDEC
                //    )
                //{
                //    retMsgs.Add(String.Format("lista emissione:{0} codice controllo qualità non gestito ({1}):{2}", selLista.id_lista, currCtrl.codice_controllo, currCtrl.descrizione_controllo));
                //    return;
                //}

                //Prelevo avvisi interessati dal controllo in esame
                if (!dictSelezioniAvvisi.ContainsKey(currCtrl.codice_controllo))
                {
                    retMsgs.Add(String.Format("lista:{0} codice controllo qualità non gestito ({1}):{2}", selLista.id_lista, currCtrl.codice_controllo, currCtrl.descrizione_controllo));
                    return;
                }

                IQueryable<tab_avv_pag_fatt_emissione> avvErrati = dictSelezioniAvvisi[currCtrl.codice_controllo](selLista, p_dbContext); //.AsNoTracking();

                //da precedenti RUN => skippo avvisi gia registrati
                IQueryable<int> avvAnomaliPreIds = TabAvvisiAnomaliListeCaricoBD.GetList(p_dbContext)
                    .Where(aa => aa.id_ente == selLista.id_ente && aa.id_lista == selLista.id_lista && aa.cod_stato.Equals(tab_avvisi_anomali_liste_carico.ATT_ATT))
                    .Where(aa => aa.id_anagrafica_controllo == currCtrl.id_anagrafica_controllo)
                    .Where(x => x.id_avv_pag_fatt_emissione != null)
                    .Select(aa => aa.id_avv_pag_fatt_emissione.Value)
                    .Distinct();

                avvErrati = avvErrati.Where(a => !avvAnomaliPreIds.Contains(a.id_tab_avv_pag));

                //Ciclo su avvisi x inserimento anomalie riscontrate (SOLO NUOVE ANOMALIE)
                avvErrati.ToList().ForEach(currAvvFE =>
                {
                    //Casi particolari
                    int id_ref = 0;
                    if (anagrafica_controlli_qualita_emissione_avvpag.CODE_REFSCO == currCtrl.codice_controllo)
                    {
                        id_ref = currAvvFE.join_avv_pag_fatt_emissione_soggetto_debitore.Where(j => j.id_join_referente_contribuente.HasValue && j.cod_stato.StartsWith(CodStato.ATT) && j.join_referente_contribuente.tab_referente.flag_irreperibilita_definitiva == "1").FirstOrDefault()?.join_referente_contribuente.id_tab_referente ?? 0;
                    }
                    else if (anagrafica_controlli_qualita_emissione_avvpag.CODE_REFEMI == currCtrl.codice_controllo)
                    {
                        id_ref = currAvvFE.join_avv_pag_fatt_emissione_soggetto_debitore.Where(j => j.id_join_referente_contribuente.HasValue && j.cod_stato.StartsWith(CodStato.ATT) && j.join_referente_contribuente.tab_referente.flag_ricerca_indirizzo_emigrazione == "1").FirstOrDefault()?.join_referente_contribuente.id_tab_referente ?? 0;
                    }

                    int id_trz = anagrafica_controlli_qualita_emissione_avvpag.CODE_TERSCO == currCtrl.codice_controllo
                                ? currAvvFE.join_avv_pag_fatt_emissione_soggetto_debitore.Where(j => j.id_terzo_debitore.HasValue && j.cod_stato.StartsWith(CodStato.ATT) && j.tab_terzo.flag_irreperibilita_definitiva == "1").FirstOrDefault()?.id_terzo_debitore ?? 0
                                : 0;

                    //INSERT anomalia dell'avviso
                    tab_avvisi_anomali_liste_carico newAvvAnomalo = new tab_avvisi_anomali_liste_carico
                    {
                        id_ente = selLista.id_ente,
                        id_lista = selLista.id_lista,
                        id_avv_pag_fatt_emissione = currAvvFE.id_tab_avv_pag,
                        tab_avv_pag_fatt_emissione = currAvvFE,
                        id_anagrafica_controllo = currCtrl.id_anagrafica_controllo,
                        id_contribuente = currAvvFE.id_anag_contribuente,
                        cod_stato_avv_pag_fatt_emissione = tab_avvisi_anomali_liste_carico.ANN_ANN,
                        cod_stato = tab_avvisi_anomali_liste_carico.ATT_ATT
                    };

                    //Casi particolari
                    if (id_ref > 0)
                        newAvvAnomalo.id_referente = id_ref;
                    if (id_trz > 0)
                        newAvvAnomalo.id_terzo = id_trz;

                    lstAnomalieRiscontrate.Add(newAvvAnomalo);
                });
            });

            //FASE PRE Calcolo totali
            //Dovrebbe conteggiare prima gli eventuali controlli manuali...DA VERIFICARE IL SENSO
            IQueryable<join_controlli_avvpag_fatt_emissione> jControlliAvvisi = JoinControlliAvvpagFattEmissioneBD.GetListByListaEmissione(selLista.id_lista, p_dbContext).WhereByCodStato(CodStato.VAL);
            var jControlliAvvisi_grouped = jControlliAvvisi.GroupBy(j => j.id_anagrafica_controllo).Select(g => new { id_anagrafica_controllo = g.Key, num_elementi = g.Count() }).AsNoTracking();

            List<tab_controlli_qualita_liste_carico> lstNuoviControlli = new List<tab_controlli_qualita_liste_carico>();
            //Ciclo su controlli manuali...
            jControlliAvvisi_grouped.ToList().ForEach(currGroup =>
            {
                //Se trovo già un record relativo....faccio aggiornamento
                tab_controlli_qualita_liste_carico CurrControllo = TabControlliQualitaListeCaricoBD.GetList(p_dbContext).WhereByIdLista(selLista.id_lista).Where(cq => cq.id_anagrafica_controllo == currGroup.id_anagrafica_controllo).WhereByCodStatoContains(CodStato.VAL).FirstOrDefault();
                if (CurrControllo != null)
                {
                    CurrControllo.num_avvpag_controllati = currGroup.num_elementi;
                }
                else
                {//altrimenti lo inserisco
                    tab_controlli_qualita_liste_carico nuovoControllo = new tab_controlli_qualita_liste_carico
                    {
                        id_ente = selLista.id_ente,
                        id_lista = selLista.id_lista,
                        id_anagrafica_controllo = currGroup.id_anagrafica_controllo,
                        num_avvpag_controllati = currGroup.num_elementi,
                        cod_stato = CodStato.VAL_VAL
                    };

                    lstNuoviControlli.Add(nuovoControllo);
                }
            });


            //controlli non automatici nel db (run precedenti)
            IQueryable<tab_avvisi_anomali_liste_carico> AvvAnomaliDB = TabAvvisiAnomaliListeCaricoBD.GetList(p_dbContext).Where(aa => aa.id_ente == selLista.id_ente && aa.id_lista == selLista.id_lista && aa.cod_stato.StartsWith(CodStato.VAL));//.Where(aa => aa.id_anagrafica_controllo.HasValue);
            var AvvAnomali_grouped_DB = AvvAnomaliDB.GroupBy(aa => aa.id_anagrafica_controllo).Select(g => new { id_anagrafica_controllo = g.Key, num_elementi = g.Count(), imp_elementi = g.Sum(x => x.tab_avv_pag_fatt_emissione.imp_tot_avvpag_rid ?? 0) }).AsNoTracking().ToList();
            //Aggiungo i nuovi elementi creati a quelli del DB
            var AvvAnomali_grouped_NEW = lstAnomalieRiscontrate.GroupBy(aa => aa.id_anagrafica_controllo).Select(g => new { id_anagrafica_controllo = g.Key, num_elementi = g.Count(), imp_elementi = g.Sum(x => x.tab_avv_pag_fatt_emissione.imp_tot_avvpag_rid ?? 0) }).ToList();
            AvvAnomali_grouped_DB.AddRange(AvvAnomali_grouped_NEW);
            AvvAnomali_grouped_DB = AvvAnomali_grouped_DB.GroupBy(aa => aa.id_anagrafica_controllo).Select(g => new { id_anagrafica_controllo = g.Key, num_elementi = g.Sum(x => x.num_elementi), imp_elementi = g.Sum(x => x.imp_elementi) }).ToList();

            //Ciclo su anomalie riscontrate
            AvvAnomali_grouped_DB.ForEach(currGroup =>
            {
                tab_controlli_qualita_liste_carico CurrControllo = TabControlliQualitaListeCaricoBD.GetList(p_dbContext).WhereByIdLista(selLista.id_lista).Where(cq => cq.id_anagrafica_controllo == currGroup.id_anagrafica_controllo).WhereByCodStatoContains(CodStato.VAL).FirstOrDefault();
                //Verfico che non sia gia da inserire (nuovo creato)
                if (CurrControllo == null)
                {
                    CurrControllo = lstNuoviControlli.Where(cq => cq.id_anagrafica_controllo == currGroup.id_anagrafica_controllo).FirstOrDefault();
                }

                if (CurrControllo != null)
                {
                    CurrControllo.num_avvpag_annullati = currGroup.num_elementi;
                    CurrControllo.imp_avvpag_annullati = currGroup.imp_elementi;
                    CurrControllo.num_avvpag_controllati = selLista.tab_avv_pag_fatt_emissione.Count();
                    CurrControllo.imp_avvpag_controllati = selLista.tab_avv_pag_fatt_emissione.Sum(avv => avv.imp_tot_avvpag_rid ?? 0);
                }
                else
                {
                    tab_controlli_qualita_liste_carico nuovoControllo = new tab_controlli_qualita_liste_carico
                    {
                        id_ente = selLista.id_ente,
                        id_lista = selLista.id_lista,
                        id_anagrafica_controllo = currGroup.id_anagrafica_controllo,
                        num_avvpag_annullati = currGroup.num_elementi,
                        imp_avvpag_annullati = currGroup.imp_elementi,
                        cod_stato = CodStato.VAL_VAL
                    };
                    nuovoControllo.num_avvpag_controllati = selLista.tab_avv_pag_fatt_emissione.Count();
                    nuovoControllo.imp_avvpag_controllati = selLista.tab_avv_pag_fatt_emissione.Sum(avv => avv.imp_tot_avvpag_rid ?? 0);
                    lstNuoviControlli.Add(nuovoControllo);
                }
            });

            //Scrittura nel DB
            p_dbContext.tab_controlli_qualita_liste_carico.AddRange(lstNuoviControlli);
            p_dbContext.tab_avvisi_anomali_liste_carico.AddRange(lstAnomalieRiscontrate);
            //   p_dbContext.SaveChanges();

            //Calcolo statistiche POST elaborazioni controlli
            retObj = GetStatisticheControlliLista(selLista.id_ente, selLista.id_lista, p_dbContext);
            retObj.segnalazioni.AddRange(retMsgs);

            return retObj;
        }


        public static tab_liste VerificaEsistenzaListaPREPRE(
         dbEnte p_specificDB, int v_idEnte, int v_idTipoAvvPagDaEmettere, anagrafica_tipo_avv_pag v_tipoAvvisoDaEmettere, int v_idTipoListaEmissione, DateTime v_dataLista)
        {
            tab_liste listaEmissione = TabListeBD.GetList(p_specificDB).Where(l =>
                                    l.id_ente == v_idEnte
                                    && l.id_entrata == v_tipoAvvisoDaEmettere.id_entrata
                                    && l.id_tipo_lista == v_idTipoListaEmissione
                                    && l.anno_rif == v_dataLista.Year.ToString()
                                    && l.id_tipo_avv_pag == v_idTipoAvvPagDaEmettere)
                                   .Where(l => l.cod_stato == tab_liste.PRE_PRE)
                                .FirstOrDefault();
            return listaEmissione;
        }

        public static tab_liste VerificaEsistenzaListaPREPREByIdServizio(
    dbEnte p_specificDB, int v_idEnte, int v_idServizio,int v_idTipoListaEmissione, DateTime v_dataLista)
        {
            tab_liste listaEmissione = TabListeBD.GetList(p_specificDB).Where(l =>
                                    l.id_ente == v_idEnte
                                    && l.id_tipo_lista == v_idTipoListaEmissione
                                    && l.anno_rif == v_dataLista.Year.ToString()
                                    && l.anagrafica_tipo_avv_pag.id_servizio == v_idServizio)
                                   .Where(l => l.cod_stato == tab_liste.PRE_PRE)
                                .FirstOrDefault();
            return listaEmissione;
        }

        public static tab_liste VerificaEsistenzaListaPREPREByIdServizio(dbEnte p_specificDB, int v_idEnte, int v_idServizio, int v_idTipoListaEmissione)
        {
            tab_liste listaEmissione = TabListeBD.GetList(p_specificDB).Where(l =>
                                    l.id_ente == v_idEnte
                                    && l.id_tipo_lista == v_idTipoListaEmissione
                                    && l.anagrafica_tipo_avv_pag.id_servizio == v_idServizio)
                                   .Where(l => l.cod_stato == tab_liste.PRE_PRE)
                                .FirstOrDefault();
            return listaEmissione;
        }

        public static tab_liste VerificaEsistenzaListaPREPREByIdEntrata(dbEnte p_specificDB, int p_idEnte, int p_idTipoListaEmissione, int p_idEntrata)
        {
            tab_liste listaEmissione = TabListeBD.GetList(p_specificDB).Where(l =>
                                    l.id_ente == p_idEnte
                                    && l.id_tipo_lista == p_idTipoListaEmissione
                                    && l.id_entrata == p_idEntrata)
                                   .Where(l => l.cod_stato == tab_liste.PRE_PRE)
                                .FirstOrDefault();
            return listaEmissione;
        }

        public static bool AnnullaConsolidamento(int p_idLista, dbEnte p_enteContext, int p_idStruttura = 0, int p_idRisora = 0)
        {
            //Controllo se la lista è annullabile


            //annullamento lista definitiva -- Parte generica

            //Elenco ID Avvisi Per il Filtro sulle cancellazioni
            string v_sqlIdAvvPag = string.Concat("SELECT a.", nameof(tab_avv_pag.id_tab_avv_pag), " FROM ", nameof(tab_avv_pag), " a WHERE a.", nameof(tab_avv_pag.id_lista_emissione), " = ", p_idLista.ToString());
           

            //v_sqlIdAvvPag = v_sqlIdAvvPag + "and a.id_anag_contribuente in "+
            //    @"(146100)";

            //Pulizia tab_boll_pag
            string v_sqlBolPag = string.Concat("UPDATE ", nameof(tab_boll_pag), " SET FLAG_ON_OFF = '0'");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlBolPag = string.Concat(v_sqlBolPag, ", ", nameof(tab_boll_pag.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                         ", ", nameof(tab_boll_pag.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                         ", ", nameof(tab_boll_pag.data_stato), " = @data_stato");
            }

            v_sqlBolPag = string.Concat(v_sqlBolPag, " WHERE ", nameof(tab_boll_pag.id_tab_avv_pag), " in (", v_sqlIdAvvPag, ")");

            //Pulizia tab_rata_avv_pag
            string v_sqlRataAvvPag = string.Concat("UPDATE ", nameof(tab_rata_avv_pag), " SET FLAG_ON_OFF = '0'");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlRataAvvPag = string.Concat(v_sqlRataAvvPag, ", ", nameof(tab_rata_avv_pag.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                         ", ", nameof(tab_rata_avv_pag.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                         ", ", nameof(tab_rata_avv_pag.data_stato), " = @data_stato");
            }

            v_sqlRataAvvPag = string.Concat(v_sqlRataAvvPag, " WHERE ", nameof(tab_rata_avv_pag.id_tab_avv_pag), " in (", v_sqlIdAvvPag, ")");

            //Pulizia tab_contribuzione
            string v_sqlContribuzione = string.Concat("UPDATE ", nameof(tab_contribuzione), " SET FLAG_ON_OFF = '0'");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlContribuzione = string.Concat(v_sqlContribuzione, ", ", nameof(tab_contribuzione.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                         ", ", nameof(tab_contribuzione.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                         ", ", nameof(tab_contribuzione.data_stato), " = @data_stato");
            }

            v_sqlContribuzione = string.Concat(v_sqlContribuzione, " WHERE ", nameof(tab_contribuzione.id_avv_pag), " in (", v_sqlIdAvvPag, ")");

            //Pulizia tab_unita_contribuzione
            string v_sqlUnitaContribuzione = string.Concat("UPDATE ", nameof(tab_unita_contribuzione), " SET FLAG_ON_OFF = '0'");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlUnitaContribuzione = string.Concat(v_sqlUnitaContribuzione, ", ", nameof(tab_unita_contribuzione.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                         ", ", nameof(tab_unita_contribuzione.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                         ", ", nameof(tab_unita_contribuzione.data_stato), " = @data_stato");
            }

            v_sqlUnitaContribuzione = string.Concat(v_sqlUnitaContribuzione, " WHERE ", nameof(tab_unita_contribuzione.id_avv_pag_generato), " in (", v_sqlIdAvvPag, ")");

            //Pulizia join_avv_pag_soggetto_debitore
            string v_sqlAvvPagSoggettoDebitore = string.Concat("UPDATE ", nameof(join_avv_pag_soggetto_debitore), " SET FLAG_ON_OFF = '0'");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlAvvPagSoggettoDebitore = string.Concat(v_sqlAvvPagSoggettoDebitore, ", ", nameof(join_avv_pag_soggetto_debitore.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                         ", ", nameof(join_avv_pag_soggetto_debitore.id_risorsa_stato), " = ", p_idRisora.ToString());
            }

            v_sqlAvvPagSoggettoDebitore = string.Concat(v_sqlAvvPagSoggettoDebitore, " WHERE ", nameof(join_avv_pag_soggetto_debitore.id_tab_avv_pag), " in (", v_sqlIdAvvPag, ")");

            //Pulizia TAB_JOIN_AVVCOA_INGFIS_V2
            string v_sqlJoinAvvCoaIngFisV2 = string.Concat("DELETE ", nameof(TAB_JOIN_AVVCOA_INGFIS_V2));
            v_sqlJoinAvvCoaIngFisV2 = string.Concat(v_sqlJoinAvvCoaIngFisV2, " WHERE ", nameof(TAB_JOIN_AVVCOA_INGFIS_V2.ID_AVVISO_COATTIVO), " in (", v_sqlIdAvvPag, ")");

            //Pulizia TAB_SUPERVISIONE_FINALE_V2
            string v_sqlSupervisioneFinaleV2 = string.Concat("UPDATE ", nameof(TAB_SUPERVISIONE_FINALE_V2), " SET ", nameof(TAB_SUPERVISIONE_FINALE_V2.ID_AVVPAG_EMESSO), " = NULL");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlSupervisioneFinaleV2 = string.Concat(v_sqlSupervisioneFinaleV2, ", ", nameof(TAB_SUPERVISIONE_FINALE_V2.ID_STRUTTURA_STATO), " = ", p_idStruttura.ToString(),
                                                         ", ", nameof(TAB_SUPERVISIONE_FINALE_V2.ID_RISORSA_STATO), " = ", p_idRisora.ToString(),
                                                         ", ", nameof(TAB_SUPERVISIONE_FINALE_V2.DATA_STATO), " = @data_stato");
            }

            v_sqlSupervisioneFinaleV2 = string.Concat(v_sqlSupervisioneFinaleV2, " WHERE ", nameof(TAB_SUPERVISIONE_FINALE_V2.ID_AVVPAG_EMESSO), " IS NOT NULL AND ",
                                                                                            nameof(TAB_SUPERVISIONE_FINALE_V2.ID_AVVPAG_EMESSO), " in (", v_sqlIdAvvPag, ")");

            //Pulizia tab_sped_not
            string v_sqlSpedNot = string.Concat("UPDATE ", nameof(tab_sped_not), " SET FLAG_ON_OFF = '0'");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlSpedNot = string.Concat(v_sqlSpedNot, ", ", nameof(tab_sped_not.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                         ", ", nameof(tab_sped_not.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                         ", ", nameof(tab_sped_not.data_stato), " = @data_stato");
            }

            v_sqlSpedNot = string.Concat(v_sqlSpedNot, " WHERE ", nameof(tab_sped_not.id_avv_pag), " in (", v_sqlIdAvvPag, ")");

            //Pulizia tab_citazioni
            string v_sqlTabCitazioni = string.Concat("UPDATE ", nameof(tab_citazioni), " SET FLAG_ON_OFF = '0'");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlTabCitazioni = string.Concat(v_sqlTabCitazioni, ", ", nameof(tab_citazioni.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                         ", ", nameof(tab_citazioni.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                         ", ", nameof(tab_citazioni.data_stato), " = @data_stato");
            }

            v_sqlTabCitazioni = string.Concat(v_sqlTabCitazioni, " WHERE FLAG_ON_OFF = '1' AND ", nameof(tab_citazioni.id_avv_pag_citazione), " in (", v_sqlIdAvvPag, ")");

            //Pulizia tab_doc_input
            string v_sqlTabDocInput = string.Concat("UPDATE ", nameof(tab_doc_input), " SET FLAG_ON_OFF = '0'");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlTabDocInput = string.Concat(v_sqlTabDocInput, ", ", nameof(tab_doc_input.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                         ", ", nameof(tab_doc_input.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                         ", ", nameof(tab_doc_input.data_stato), " = @data_stato");
            }

            v_sqlTabDocInput = string.Concat(v_sqlTabDocInput, " WHERE FLAG_ON_OFF = '1' AND ", nameof(tab_doc_input.id_tab_doc_input), " in (SELECT ",
                                nameof(tab_citazioni.id_tab_doc_citazione), " FROM ", nameof(tab_citazioni), " WHERE ",
                                nameof(tab_citazioni.id_avv_pag_citazione), " in (", v_sqlIdAvvPag, "))");

            //Pulizia tab_avv_pag
            string v_sqlAvvPag = string.Concat("UPDATE ", nameof(tab_avv_pag), " SET FLAG_ON_OFF = '0'");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlAvvPag = string.Concat(v_sqlAvvPag, ", ", nameof(tab_avv_pag.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                         ", ", nameof(tab_avv_pag.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                         ", ", nameof(tab_avv_pag.data_stato), " = @data_stato");
            }

            v_sqlAvvPag = string.Concat(v_sqlAvvPag, " WHERE ", nameof(tab_avv_pag.id_tab_avv_pag), " in (", v_sqlIdAvvPag, ")");

            //Ripristino tab_avv_pag_fatt_emissione
            string v_sqlAvvPagFattEmissione = string.Concat("UPDATE ", nameof(tab_avv_pag_fatt_emissione), " SET ", nameof(tab_avv_pag_fatt_emissione.cod_stato), " = '", anagrafica_stato_avv_pag.VAL_PRE, "' ",
                                                                 ", ", nameof(tab_avv_pag_fatt_emissione.cod_stato_avv_pag), " = '", anagrafica_stato_avv_pag.VAL_PRE, "' ",
                                                                 ", ", nameof(tab_avv_pag_fatt_emissione.id_stato), " = ", anagrafica_stato_avv_pag.VAL_PRE_ID, " ",
                                                                 ", ", nameof(tab_avv_pag_fatt_emissione.id_stato_avv_pag), " = ", anagrafica_stato_avv_pag.VAL_PRE_ID, " ");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlAvvPagFattEmissione = string.Concat(v_sqlAvvPagFattEmissione, ", ", nameof(tab_avv_pag_fatt_emissione.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                         ", ", nameof(tab_avv_pag_fatt_emissione.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                         ", ", nameof(tab_avv_pag_fatt_emissione.data_stato), " = @data_stato");
            }

            v_sqlAvvPagFattEmissione = string.Concat(v_sqlAvvPagFattEmissione, " WHERE ", nameof(tab_avv_pag_fatt_emissione.cod_stato), " = '", anagrafica_stato_avv_pag.ANNULLATO_ANNULLATO, "' AND ",
                                                                                          nameof(tab_avv_pag_fatt_emissione.cod_stato_avv_pag), " = '", anagrafica_stato_avv_pag.ANNULLATO_ANNULLATO, "' AND ",
                                                                                          nameof(tab_avv_pag_fatt_emissione.id_lista_emissione), " = ", p_idLista.ToString());

            //Ripristina lo stato degli avvisi collegati se non sono in altri atti successivi
            string v_sqlAvvPagToValEme = string.Concat("UPDATE ", nameof(tab_avv_pag), " SET ",
                                                                            nameof(tab_avv_pag.cod_stato), " = '", anagrafica_stato_avv_pag.VAL_EME, "', ",
                                                                            nameof(tab_avv_pag.id_stato), " = ", anagrafica_stato_avv_pag.VAL_EME_ID, ", ",
                                                                            nameof(tab_avv_pag.cod_stato_avv_pag), " = '", anagrafica_stato_avv_pag.VAL_EME, "', ",
                                                                            nameof(tab_avv_pag.id_stato_avv_pag), " = ", anagrafica_stato_avv_pag.VAL_EME_ID);

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlAvvPagToValEme = string.Concat(v_sqlAvvPagToValEme, ", ", nameof(tab_avv_pag.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                         ", ", nameof(tab_avv_pag.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                         ", ", nameof(tab_avv_pag.data_stato), " = @data_stato");
            }

            v_sqlAvvPagToValEme = string.Concat(v_sqlAvvPagToValEme, " WHERE ",
                    nameof(tab_avv_pag.id_tab_avv_pag), " IN (SELECT u.", nameof(tab_unita_contribuzione.id_avv_pag_collegato), 
                                                              " FROM ", nameof(tab_unita_contribuzione), " u ",
                                                              " WHERE u.FLAG_ON_OFF = '0' AND u.", nameof(tab_unita_contribuzione.id_avv_pag_collegato), " IS NOT NULL ",
                                                              "   AND u.", nameof(tab_unita_contribuzione.id_avv_pag_generato), " in (", v_sqlIdAvvPag, " AND a.FLAG_ON_OFF = '0'))",
           " AND ", nameof(tab_avv_pag.id_tab_avv_pag), " NOT IN (SELECT uv.", nameof(tab_unita_contribuzione.id_avv_pag_collegato),
                                                              " FROM ", nameof(tab_unita_contribuzione), " uv ",
                                                              " WHERE uv.FLAG_ON_OFF = '1' AND uv.", nameof(tab_unita_contribuzione.id_avv_pag_collegato), " IS NOT NULL) ",
           " AND ", nameof(tab_avv_pag.cod_stato_avv_pag), " = '", anagrafica_stato_avv_pag.VALIDO_COATTIVO, "'");


            //Ripristino tab_liste
            string v_sqlTabListe = string.Concat("UPDATE ", nameof(tab_liste), " SET ",
                                                            nameof(tab_liste.cod_stato), " = '", tab_liste.PRE_CAL, "' ");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlTabListe = string.Concat(v_sqlTabListe, ", ", nameof(tab_liste.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                             ", ", nameof(tab_liste.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                             ", ", nameof(tab_liste.data_stato), " = @data_stato");
            }

            v_sqlTabListe = string.Concat(v_sqlTabListe, " WHERE ", nameof(tab_liste.id_lista), " = ", p_idLista.ToString());


            string v_sqlTabValidazione = string.Concat("UPDATE ", nameof(tab_validazione_approvazione_liste), " SET FLAG_ON_OFF = '0' ");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlTabValidazione = string.Concat(v_sqlTabValidazione, ", ", nameof(tab_validazione_approvazione_liste.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                             ", ", nameof(tab_validazione_approvazione_liste.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                             ", ", nameof(tab_validazione_approvazione_liste.data_stato), " = @data_stato");
            }

            v_sqlTabValidazione = string.Concat(v_sqlTabValidazione, " WHERE ", nameof(tab_liste.id_lista), " = ", p_idLista.ToString());


            TimeSpan v_timeOut = new TimeSpan(1, 0, 0);
            using (TransactionScope v_trans = new TransactionScope(TransactionScopeOption.Required, v_timeOut))
            {
                try
                {
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlBolPag, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlRataAvvPag, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlContribuzione, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlUnitaContribuzione, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlAvvPagSoggettoDebitore, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlJoinAvvCoaIngFisV2);
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlSupervisioneFinaleV2, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlSpedNot, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    int v_numAvvisi = p_enteContext.Database.ExecuteSqlCommand(v_sqlAvvPag, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlAvvPagFattEmissione, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlAvvPagToValEme, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlTabListe, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlTabCitazioni, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlTabDocInput, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlTabValidazione, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);

                    v_trans.Complete();
                }
                catch (Exception ex)
                {
                    //loggare eventualmente
                    m_logger.LogException("Errore nell'annullamento del consolidamento: ", ex, Utility.Log.EnLogSeverity.Fatal);
                    return false;
                }
            }

            //Annullamento lista definitiva -- Parte specifica per tipo avviso/servizio

            return true;
        }

        public static bool AnnullaAvvisiNonAssMesso(int p_idListaSpedNot, dbEnte p_enteContext, int p_idStruttura = 0, int p_idRisora = 0)
        {
            //Controllo se la lista è annullabile


            //annullamento lista definitiva -- Parte generica

            //Elenco ID Avvisi Per il Filtro sulle cancellazioni
            string v_sqlIdAvvPag = string.Concat(" SELECT DISTINCT a.", 
                                                        nameof(tab_sped_not.id_avv_pag), 
                                                 " FROM ", 
                                                        nameof(tab_sped_not), " a,", 
                                                        nameof(tab_lista_sped_notifiche), " b,", 
                                                        nameof(tab_notificatore), " c",
                                                 " WHERE " ,
                                                        " a.", nameof(tab_sped_not.id_lista_sped_not), " = b.", nameof(tab_lista_sped_notifiche.id_lista),
                                                 " AND " ,
                                                        " c.", nameof(tab_notificatore.id_notificatore), " = a.", nameof(tab_sped_not.id_notificatore),
                                                 " AND " ,
                                                        " ( ", 
                                                                " c.", nameof(tab_notificatore.flag_persona_fisica_giuridica), " = 'PP'",
                                                                " OR ",
                                                                " a.", nameof(tab_sped_not.id_notificatore), " = 0 ",
                                                        " ) ",
                                                 " AND ",
                                                        " b.", nameof(tab_lista_sped_notifiche.id_tipo_spedizione_notifica), " = 1",
                                                 " AND ",
                                                        " a.", nameof(tab_sped_not.id_notificatore), " IS NOT NULL",
                                                 " AND ",
                                                        " b.", nameof(tab_lista_sped_notifiche.id_lista), " = ",   p_idListaSpedNot.ToString(),
                                                 " AND ",
                                                        " a.FLAG_ON_OFF = '1'",
                                                 " AND ",
                                                        " b.FLAG_ON_OFF = '1'",
                                                 " AND ",
                                                        " c.FLAG_ON_OFF = '1'");


            //v_sqlIdAvvPag = v_sqlIdAvvPag + "and a.id_anag_contribuente in "+
            //    @"(146100)";

            //Elenco ID Contribuente Per il Filtro sulle cancellazioni
            string v_sqlIdContribuente = string.Concat(" SELECT DISTINCT d.",
                                                            nameof(tab_avv_pag.id_anag_contribuente),
                                                       " FROM ",
                                                            nameof(tab_sped_not), " a,",
                                                            nameof(tab_lista_sped_notifiche), " b,",
                                                            nameof(tab_notificatore), " c,",
                                                            nameof(tab_avv_pag), " d",
                                                       " WHERE ",
                                                            " a.", nameof(tab_sped_not.id_lista_sped_not), " = b.", nameof(tab_lista_sped_notifiche.id_lista),
                                                       " AND ",
                                                            " c.", nameof(tab_notificatore.id_notificatore), " = a.", nameof(tab_sped_not.id_notificatore),
                                                       " AND ",
                                                            " a.", nameof(tab_sped_not.id_avv_pag), " = d.", nameof(tab_avv_pag.id_tab_avv_pag),
                                                       " AND ",
                                                            " ( ",
                                                                    " c.", nameof(tab_notificatore.flag_persona_fisica_giuridica), " = 'PP' ",
                                                                    " OR ",
                                                                    " a.", nameof(tab_sped_not.id_notificatore), " = 0 ",
                                                            " ) ",
                                                       " AND ",
                                                            " b.", nameof(tab_lista_sped_notifiche.id_tipo_spedizione_notifica), " = 1 ",
                                                       " AND ",
                                                            " a.", nameof(tab_sped_not.id_notificatore), " IS NOT NULL ",
                                                       " AND ",
                                                            " b.", nameof(tab_lista_sped_notifiche.id_lista), " = ", p_idListaSpedNot.ToString(),
                                                       " AND ",
                                                            " a.FLAG_ON_OFF = '1'",
                                                       " AND ",
                                                            " b.FLAG_ON_OFF = '1'",
                                                       " AND ",
                                                            " c.FLAG_ON_OFF = '1'",
                                                       " AND ",
                                                            " d.FLAG_ON_OFF = '1'");

            string v_sqlIdContribuenteAvvNonAss = string.Concat(" SELECT DISTINCT d.",
                                                                    nameof(tab_avv_pag.id_anag_contribuente),
                                                                " FROM ",
                                                                    nameof(tab_sped_not), " a,",
                                                                    nameof(tab_lista_sped_notifiche), " b,",
                                                                    nameof(tab_notificatore), " c,",
                                                                    nameof(tab_avv_pag), " d",
                                                                " WHERE ",
                                                                    " a.", nameof(tab_sped_not.id_lista_sped_not), " = b.", nameof(tab_lista_sped_notifiche.id_lista),
                                                                " AND ",
                                                                    " c.", nameof(tab_notificatore.id_notificatore), " = a.", nameof(tab_sped_not.id_notificatore),
                                                                " AND ",
                                                                    " a.", nameof(tab_sped_not.id_avv_pag), " = d.", nameof(tab_avv_pag.id_tab_avv_pag),
                                                                " AND ",
                                                                    " ( ",
                                                                            " c.", nameof(tab_notificatore.flag_persona_fisica_giuridica), " = 'PP' ",
                                                                            " OR ",
                                                                            " a.", nameof(tab_sped_not.id_notificatore), " = 0 ",
                                                                    " ) ",
                                                                " AND ",
                                                                    " b.", nameof(tab_lista_sped_notifiche.id_tipo_spedizione_notifica), " = 1 ",
                                                                " AND ",
                                                                    " a.", nameof(tab_sped_not.id_notificatore), " IS NOT NULL ",
                                                                " AND ",
                                                                    " b.", nameof(tab_lista_sped_notifiche.id_lista), " = ", p_idListaSpedNot.ToString(),
                                                                " AND ",
                                                                    " a.FLAG_ON_OFF = '1'",
                                                                " AND ",
                                                                    " b.FLAG_ON_OFF = '1'",
                                                                " AND ",
                                                                    " c.FLAG_ON_OFF = '1'",
                                                                " AND ",
                                                                    " d.FLAG_ON_OFF = '0'");


            //Elenco ID Lista Emissione
            string v_sqlIdListaEmissione = string.Concat(" SELECT DISTINCT d.",
                                                            nameof(tab_avv_pag.id_lista_emissione),
                                                        " FROM ",
                                                            nameof(tab_sped_not), " a,",
                                                            nameof(tab_lista_sped_notifiche), " b,",
                                                            nameof(tab_notificatore), " c,",
                                                            nameof(tab_avv_pag), " d",
                                                        " WHERE ",
                                                            " a.", nameof(tab_sped_not.id_lista_sped_not), " = b.", nameof(tab_lista_sped_notifiche.id_lista),
                                                        " AND ",
                                                            " c.", nameof(tab_notificatore.id_notificatore), " = a.", nameof(tab_sped_not.id_notificatore),
                                                        " AND ",
                                                            " a.", nameof(tab_sped_not.id_avv_pag), " = d.", nameof(tab_avv_pag.id_tab_avv_pag),
                                                        " AND ",
                                                            " ( ",
                                                                    " c.", nameof(tab_notificatore.flag_persona_fisica_giuridica), " = 'PP'",
                                                                    " OR ",
                                                                    " a.", nameof(tab_sped_not.id_notificatore), " = 0 ",
                                                            " ) ",
                                                        " AND ",
                                                            " b.", nameof(tab_lista_sped_notifiche.id_tipo_spedizione_notifica), " = 1",
                                                        " AND ",
                                                            " a.", nameof(tab_sped_not.id_notificatore), " IS NOT NULL",
                                                        " AND ",
                                                            " b.", nameof(tab_lista_sped_notifiche.id_lista), " = ", p_idListaSpedNot.ToString(),
                                                        " AND ",
                                                            " a.FLAG_ON_OFF = '1'",
                                                        " AND ",
                                                            " b.FLAG_ON_OFF = '1'",
                                                        " AND ",
                                                            " c.FLAG_ON_OFF = '1'",
                                                        " AND ",
                                                            " d.FLAG_ON_OFF = '1'");

            //Pulizia tab_boll_pag
            string v_sqlBolPag = string.Concat("UPDATE ", nameof(tab_boll_pag), " SET FLAG_ON_OFF = '0'");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlBolPag = string.Concat(v_sqlBolPag, ", ", nameof(tab_boll_pag.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                         ", ", nameof(tab_boll_pag.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                         ", ", nameof(tab_boll_pag.data_stato), " = @data_stato");
            }

            v_sqlBolPag = string.Concat(v_sqlBolPag, " WHERE ", nameof(tab_boll_pag.id_tab_avv_pag), " in (", v_sqlIdAvvPag, ")");

            //Pulizia tab_rata_avv_pag
            string v_sqlRataAvvPag = string.Concat("UPDATE ", nameof(tab_rata_avv_pag), " SET FLAG_ON_OFF = '0'");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlRataAvvPag = string.Concat(v_sqlRataAvvPag, 
                                                ", ", nameof(tab_rata_avv_pag.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                ", ", nameof(tab_rata_avv_pag.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                ", ", nameof(tab_rata_avv_pag.data_stato), " = @data_stato");
            }

            v_sqlRataAvvPag = string.Concat(v_sqlRataAvvPag, " WHERE ", nameof(tab_rata_avv_pag.id_tab_avv_pag), " in (", v_sqlIdAvvPag, ")");

            //Pulizia tab_contribuzione
            string v_sqlContribuzione = string.Concat("UPDATE ", nameof(tab_contribuzione), " SET FLAG_ON_OFF = '0'");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlContribuzione = string.Concat(v_sqlContribuzione, 
                                                   ", ", nameof(tab_contribuzione.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                   ", ", nameof(tab_contribuzione.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                   ", ", nameof(tab_contribuzione.data_stato), " = @data_stato");
            }

            v_sqlContribuzione = string.Concat(v_sqlContribuzione, " WHERE ", nameof(tab_contribuzione.id_avv_pag), " in (", v_sqlIdAvvPag, ")");

            //Pulizia tab_unita_contribuzione
            string v_sqlUnitaContribuzione = string.Concat("UPDATE ", nameof(tab_unita_contribuzione), " SET FLAG_ON_OFF = '0'");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlUnitaContribuzione = string.Concat(v_sqlUnitaContribuzione, 
                                                        ", ", nameof(tab_unita_contribuzione.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                        ", ", nameof(tab_unita_contribuzione.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                        ", ", nameof(tab_unita_contribuzione.data_stato), " = @data_stato");
            }

            v_sqlUnitaContribuzione = string.Concat(v_sqlUnitaContribuzione, " WHERE ", nameof(tab_unita_contribuzione.id_avv_pag_generato), " in (", v_sqlIdAvvPag, ")");

            //Pulizia join_avv_pag_soggetto_debitore
            string v_sqlAvvPagSoggettoDebitore = string.Concat("UPDATE ", nameof(join_avv_pag_soggetto_debitore), " SET FLAG_ON_OFF = '0'");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlAvvPagSoggettoDebitore = string.Concat(v_sqlAvvPagSoggettoDebitore, 
                                                            ", ", nameof(join_avv_pag_soggetto_debitore.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                            ", ", nameof(join_avv_pag_soggetto_debitore.id_risorsa_stato), " = ", p_idRisora.ToString());
            }

            v_sqlAvvPagSoggettoDebitore = string.Concat(v_sqlAvvPagSoggettoDebitore, " WHERE ", nameof(join_avv_pag_soggetto_debitore.id_tab_avv_pag), " in (", v_sqlIdAvvPag, ")");

            //Pulizia TAB_JOIN_AVVCOA_INGFIS_V2
            string v_sqlJoinAvvCoaIngFisV2 = string.Concat("DELETE ", nameof(TAB_JOIN_AVVCOA_INGFIS_V2));
            v_sqlJoinAvvCoaIngFisV2 = string.Concat(v_sqlJoinAvvCoaIngFisV2, " WHERE ", nameof(TAB_JOIN_AVVCOA_INGFIS_V2.ID_AVVISO_COATTIVO), " in (", v_sqlIdAvvPag, ")");

            //Pulizia TAB_SUPERVISIONE_FINALE_V2
            string v_sqlSupervisioneFinaleV2 = string.Concat("UPDATE ", nameof(TAB_SUPERVISIONE_FINALE_V2), " SET ", nameof(TAB_SUPERVISIONE_FINALE_V2.ID_AVVPAG_EMESSO), " = NULL");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlSupervisioneFinaleV2 = string.Concat(v_sqlSupervisioneFinaleV2, 
                                                          ", ", nameof(TAB_SUPERVISIONE_FINALE_V2.ID_STRUTTURA_STATO), " = ", p_idStruttura.ToString(),
                                                          ", ", nameof(TAB_SUPERVISIONE_FINALE_V2.ID_RISORSA_STATO), " = ", p_idRisora.ToString(),
                                                          ", ", nameof(TAB_SUPERVISIONE_FINALE_V2.DATA_STATO), " = @data_stato");
            }

            v_sqlSupervisioneFinaleV2 = string.Concat(v_sqlSupervisioneFinaleV2, " WHERE ", nameof(TAB_SUPERVISIONE_FINALE_V2.ID_AVVPAG_EMESSO), " IS NOT NULL AND ",
                                                                                            nameof(TAB_SUPERVISIONE_FINALE_V2.ID_AVVPAG_EMESSO), " in (", v_sqlIdAvvPag, ")");

            //Pulizia tab_sped_not
            string v_sqlSpedNot = string.Concat("UPDATE ", nameof(tab_sped_not), " SET FLAG_ON_OFF = '0' ");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlSpedNot = string.Concat(v_sqlSpedNot, ", ", nameof(tab_sped_not.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                           ", ", nameof(tab_sped_not.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                           ", ", nameof(tab_sped_not.data_stato), " = @data_stato");
            }

            v_sqlSpedNot = string.Concat(v_sqlSpedNot, " WHERE ", nameof(tab_sped_not.id_avv_pag), " in (", v_sqlIdAvvPag, ")");

            //Pulizia tab_citazioni
            string v_sqlTabCitazioni = string.Concat("UPDATE ", nameof(tab_citazioni), " SET FLAG_ON_OFF = '0'");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlTabCitazioni = string.Concat(v_sqlTabCitazioni, 
                                                  ", ", nameof(tab_citazioni.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                  ", ", nameof(tab_citazioni.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                  ", ", nameof(tab_citazioni.data_stato), " = @data_stato");
            }

            v_sqlTabCitazioni = string.Concat(v_sqlTabCitazioni, " WHERE FLAG_ON_OFF = '1' AND ", nameof(tab_citazioni.id_avv_pag_citazione), " in (", v_sqlIdAvvPag, ")");

            //Pulizia tab_doc_input
            string v_sqlTabDocInput = string.Concat("UPDATE ", nameof(tab_doc_input), " SET FLAG_ON_OFF = '0'");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlTabDocInput = string.Concat(v_sqlTabDocInput, 
                                                 ", ", nameof(tab_doc_input.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                 ", ", nameof(tab_doc_input.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                 ", ", nameof(tab_doc_input.data_stato), " = @data_stato");
            }

            v_sqlTabDocInput = string.Concat(v_sqlTabDocInput, " WHERE FLAG_ON_OFF = '1' AND ", nameof(tab_doc_input.id_tab_doc_input), " in (SELECT ",
                                nameof(tab_citazioni.id_tab_doc_citazione), " FROM ", nameof(tab_citazioni), " WHERE ",
                                nameof(tab_citazioni.id_avv_pag_citazione), " in (", v_sqlIdAvvPag, "))");

            //Pulizia tab_avv_pag
            string v_sqlAvvPag = string.Concat("UPDATE ", nameof(tab_avv_pag), " SET FLAG_ON_OFF = '0'");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlAvvPag = string.Concat(v_sqlAvvPag, ", ", nameof(tab_avv_pag.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                         ", ", nameof(tab_avv_pag.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                         ", ", nameof(tab_avv_pag.data_stato), " = @data_stato");
            }

            v_sqlAvvPag = string.Concat(v_sqlAvvPag, " WHERE ", nameof(tab_avv_pag.id_tab_avv_pag), " in (", v_sqlIdAvvPag, ")");

            //Ripristino tab_avv_pag_fatt_emissione
            string v_sqlAvvPagFattEmissione = string.Concat("UPDATE ", nameof(tab_avv_pag_fatt_emissione), " SET ", nameof(tab_avv_pag_fatt_emissione.cod_stato), " = '", anagrafica_stato_avv_pag.VAL_PRE, "' ",
                                                            ", ", nameof(tab_avv_pag_fatt_emissione.id_lista_emissione), " = @idListaNew",
                                                            ", ", nameof(tab_avv_pag_fatt_emissione.cod_stato_avv_pag), " = '", anagrafica_stato_avv_pag.VAL_PRE, "' ",
                                                            ", ", nameof(tab_avv_pag_fatt_emissione.id_stato), " = ", anagrafica_stato_avv_pag.VAL_PRE_ID, " ",
                                                            ", ", nameof(tab_avv_pag_fatt_emissione.id_stato_avv_pag), " = ", anagrafica_stato_avv_pag.VAL_PRE_ID, " ");


            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlAvvPagFattEmissione = string.Concat(v_sqlAvvPagFattEmissione, 
                                                         ", ", nameof(tab_avv_pag_fatt_emissione.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                         ", ", nameof(tab_avv_pag_fatt_emissione.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                         ", ", nameof(tab_avv_pag_fatt_emissione.data_stato), " = @data_stato");
            }

            v_sqlAvvPagFattEmissione = string.Concat(v_sqlAvvPagFattEmissione, " WHERE ", nameof(tab_avv_pag_fatt_emissione.cod_stato), " = '", anagrafica_stato_avv_pag.ANNULLATO_ANNULLATO, "' AND ",
                                                                                          nameof(tab_avv_pag_fatt_emissione.cod_stato_avv_pag), " = '", anagrafica_stato_avv_pag.ANNULLATO_ANNULLATO, "' AND ",
                                                                                          nameof(tab_avv_pag_fatt_emissione.id_lista_emissione), " = @idListaOld ", " AND ",
                                                                                          nameof(tab_avv_pag_fatt_emissione.id_anag_contribuente), " in (", v_sqlIdContribuenteAvvNonAss, ")");

            //Ripristina lo stato degli avvisi collegati se non sono in altri atti successivi
            string v_sqlAvvPagToValEme = string.Concat("UPDATE ", nameof(tab_avv_pag), " SET ",
                                                                  nameof(tab_avv_pag.cod_stato), " = '", anagrafica_stato_avv_pag.VAL_EME, "', ",
                                                                  nameof(tab_avv_pag.id_stato), " = ", anagrafica_stato_avv_pag.VAL_EME_ID, ", ",
                                                                  nameof(tab_avv_pag.cod_stato_avv_pag), " = '", anagrafica_stato_avv_pag.VAL_EME, "', ",
                                                                  nameof(tab_avv_pag.id_stato_avv_pag), " = ", anagrafica_stato_avv_pag.VAL_EME_ID);

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlAvvPagToValEme = string.Concat(v_sqlAvvPagToValEme, 
                                                    ", ", nameof(tab_avv_pag.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                    ", ", nameof(tab_avv_pag.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                    ", ", nameof(tab_avv_pag.data_stato), " = @data_stato");
            }

            v_sqlAvvPagToValEme = string.Concat(v_sqlAvvPagToValEme, " WHERE ",
                    nameof(tab_avv_pag.id_tab_avv_pag), " IN (SELECT u.", nameof(tab_unita_contribuzione.id_avv_pag_collegato),
                                                        " FROM ", nameof(tab_unita_contribuzione), " u ",
                                                        " WHERE u.FLAG_ON_OFF = '0' AND u.", nameof(tab_unita_contribuzione.id_avv_pag_collegato), " IS NOT NULL ",
                                                        " AND u.", nameof(tab_unita_contribuzione.id_avv_pag_generato), " in (", v_sqlIdAvvPag, " AND a.FLAG_ON_OFF = '0'))",
                                                        " AND ", nameof(tab_avv_pag.id_tab_avv_pag), " NOT IN (SELECT uv.", nameof(tab_unita_contribuzione.id_avv_pag_collegato),
                                                        " FROM ", nameof(tab_unita_contribuzione), " uv ",
                                                        " WHERE uv.FLAG_ON_OFF = '1' AND uv.", nameof(tab_unita_contribuzione.id_avv_pag_collegato), " IS NOT NULL) ",
                                                        " AND ", nameof(tab_avv_pag.cod_stato_avv_pag), " = '", anagrafica_stato_avv_pag.VALIDO_COATTIVO, "'");


            //Lista SpedNot DEF-CON
            string v_sqlListaSpedNot = string.Concat("UPDATE ", nameof(tab_lista_sped_notifiche), " SET COD_STATO = 'SPE-PRE', NUM_SPED_NOT = NUM_SPED_NOT - @numNonAss" );

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlListaSpedNot = string.Concat(v_sqlListaSpedNot,
                                                        ", ", nameof(tab_unita_contribuzione.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                        ", ", nameof(tab_unita_contribuzione.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                        ", ", nameof(tab_unita_contribuzione.data_stato), " = @data_stato");
            }

            v_sqlListaSpedNot = string.Concat(v_sqlListaSpedNot, " WHERE ", nameof(tab_lista_sped_notifiche.id_lista), " = ", p_idListaSpedNot);

            tab_liste v_ListaOld;

            TimeSpan v_timeOut = new TimeSpan(1, 0, 0);
            using (TransactionScope v_trans = new TransactionScope(TransactionScopeOption.Required, v_timeOut))
            {
                try
                {

                    p_enteContext.Database.ExecuteSqlCommand(v_sqlBolPag, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlRataAvvPag, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlContribuzione, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlUnitaContribuzione, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlAvvPagSoggettoDebitore, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlJoinAvvCoaIngFisV2);
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlSupervisioneFinaleV2, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);

                    int v_numAvvisi = p_enteContext.Database.ExecuteSqlCommand(v_sqlAvvPag, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);

                    if (v_numAvvisi > 0)
                    {
                        v_ListaOld = TabListeBD.GetList(p_enteContext).Where(idLista => idLista.join_lst_emissione_lst_sped_not.Any(idListaSped => idListaSped.id_lista_sped_not == p_idListaSpedNot)).SingleOrDefault();

                        int v_ProgressivoLista = TabProgListaBD.IncrementaProgressivoCorrente(v_ListaOld.id_ente, v_ListaOld.id_tipo_lista, v_ListaOld.data_lista.Year, p_enteContext);

                        tab_liste v_ListaNew = CreaLista(v_ListaOld.id_ente, v_ListaOld.id_entrata, v_ListaOld.id_tipo_lista, v_ListaOld.id_tipo_avv_pag, v_ProgressivoLista,
                                                         v_ListaOld.data_lista, v_ListaOld.anno_rif, v_ListaOld.parametri_calcolo, tab_liste.PRE_CAL, p_enteContext);

                        v_ListaOld.descr_lista += " Not. Messo";
                        v_ListaNew.descr_lista += " Not. Racc.";

                        p_enteContext.SaveChanges();

                        p_enteContext.Database.ExecuteSqlCommand(v_sqlAvvPagFattEmissione, new SqlParameter("@idListaNew", v_ListaNew.id_lista),
                                                                                           new SqlParameter("@idListaOld", v_ListaOld.id_lista),
                                                                                           new SqlParameter("@data_stato", DateTime.Now));

                        string v_num_avvpag = TabAvvPagBD.GetList(p_enteContext).Where(a => a.id_lista_emissione == v_ListaOld.id_lista)
                                                         .Where(a => a.cod_stato.StartsWith(CodStato.VAL)).Count().ToString();

                        decimal? v_imp_tot_lista = TabAvvPagBD.GetList(p_enteContext).Where(a => a.id_lista_emissione == v_ListaOld.id_lista)
                                                              .Where(a => a.cod_stato.StartsWith(CodStato.VAL)).Select(a => a.imp_tot_avvpag).Sum();

                        //Aggiornamento totali lista
                        v_ListaOld.imp_tot_lista = v_imp_tot_lista;
                        v_ListaOld.num_avvpag = v_num_avvpag;

                        p_enteContext.SaveChanges();

                        p_enteContext.Database.ExecuteSqlCommand(v_sqlAvvPagToValEme, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                        p_enteContext.Database.ExecuteSqlCommand(v_sqlTabCitazioni, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                        p_enteContext.Database.ExecuteSqlCommand(v_sqlTabDocInput, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);

                        int? v_numSpedNonAss = p_enteContext.Database.ExecuteSqlCommand(v_sqlSpedNot, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);

                        p_enteContext.Database.ExecuteSqlCommand(v_sqlListaSpedNot, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null,
                                                                                                      new SqlParameter("@numNonAss", v_numSpedNonAss ?? 0));
                        

                        v_trans.Complete();
                    }
                    else
                    {
                        p_enteContext.Database.ExecuteSqlCommand(v_sqlListaSpedNot, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);

                        v_trans.Complete();
                    }
                }
                catch (Exception ex)
                {
                    //loggare eventualmente
                    m_logger.LogException("Errore nell'annullamento avvisi da : ", ex, Utility.Log.EnLogSeverity.Fatal);
                    return false;
                }
            }

            return true;
        }

        public static IQueryable<tab_liste> GetListCautelariNonConsolidate(int p_idEnte, DateTime p_dataTaglio, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(l => l.tab_tipo_lista.cod_lista == tab_tipo_lista.TIPOLISTA_EMISSIONE)
                                       .WhereByIdEnte(p_idEnte)
                                       .Where(l => l.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI &&
                                                   l.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO &&
                                                   l.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO &&
                                                   l.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_IMMOBILIARI &&
                                                   l.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI)
                                       .Where(l => l.data_lista > p_dataTaglio)
                                       .Where(l => !l.cod_stato.StartsWith(tab_liste.ANN) && !l.cod_stato.StartsWith(tab_liste.DEF));
        }

        public static bool AnnullaListaPerRicalcolo(int p_idLista, dbEnte p_enteContext, int p_idStruttura = 0, int p_idRisora = 0)
        {

            string v_sqlIdAvvPagFattEmissione = string.Concat("SELECT a.", nameof(tab_avv_pag_fatt_emissione.id_tab_avv_pag), " FROM ", nameof(tab_avv_pag_fatt_emissione), " a WHERE a.", nameof(tab_avv_pag_fatt_emissione.id_lista_emissione), " = ", p_idLista.ToString());

            //Pulizia tab_unita_contribuzione
            string v_sqlUnitaContribuzioneFattEmissione = string.Concat("UPDATE ", nameof(tab_unita_contribuzione_fatt_emissione), " SET FLAG_ON_OFF = '0'");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlUnitaContribuzioneFattEmissione = string.Concat(v_sqlUnitaContribuzioneFattEmissione, ", ", nameof(tab_unita_contribuzione_fatt_emissione.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                         ", ", nameof(tab_unita_contribuzione_fatt_emissione.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                         ", ", nameof(tab_unita_contribuzione_fatt_emissione.data_stato), " = @data_stato");
            }

            v_sqlUnitaContribuzioneFattEmissione = string.Concat(v_sqlUnitaContribuzioneFattEmissione, " WHERE ", nameof(tab_unita_contribuzione_fatt_emissione.id_avv_pag_generato), " in (", v_sqlIdAvvPagFattEmissione, ")");



            //Pulizia join_avv_pag_soggetto_debitore
            string v_sqlAvvPagSoggettoDebitoreFattEmissione = string.Concat("UPDATE ", nameof(join_avv_pag_fatt_emissione_soggetto_debitore), " SET FLAG_ON_OFF = '0'");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlAvvPagSoggettoDebitoreFattEmissione = string.Concat(v_sqlAvvPagSoggettoDebitoreFattEmissione, ", ", nameof(join_avv_pag_fatt_emissione_soggetto_debitore.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                         ", ", nameof(join_avv_pag_fatt_emissione_soggetto_debitore.id_risorsa_stato), " = ", p_idRisora.ToString());
            }

            v_sqlAvvPagSoggettoDebitoreFattEmissione = string.Concat(v_sqlAvvPagSoggettoDebitoreFattEmissione, " WHERE ", nameof(join_avv_pag_fatt_emissione_soggetto_debitore.id_tab_avv_pag), " in (", v_sqlIdAvvPagFattEmissione, ")");



            //Pulizia tab_avv_pag
            string v_sqlAvvPagFattEmissione = string.Concat("UPDATE ", nameof(tab_avv_pag_fatt_emissione), " SET FLAG_ON_OFF = '0'");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlAvvPagFattEmissione = string.Concat(v_sqlAvvPagFattEmissione, ", ", nameof(tab_avv_pag_fatt_emissione.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                         ", ", nameof(tab_avv_pag_fatt_emissione.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                         ", ", nameof(tab_avv_pag_fatt_emissione.data_stato), " = @data_stato");
            }

            v_sqlAvvPagFattEmissione = string.Concat(v_sqlAvvPagFattEmissione, " WHERE ", nameof(tab_avv_pag_fatt_emissione.id_tab_avv_pag), " in (", v_sqlIdAvvPagFattEmissione, ")");

            //Ripristino tab_liste
            string v_sqlTabListe = string.Concat("UPDATE ", nameof(tab_liste), " SET ",
                                                            nameof(tab_liste.cod_stato), " = '", tab_liste.PRE_PRE, "' ");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlTabListe = string.Concat(v_sqlTabListe, ", ", nameof(tab_liste.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                             ", ", nameof(tab_liste.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                             ", ", nameof(tab_liste.data_stato), " = @data_stato");
            }

            v_sqlTabListe = string.Concat(v_sqlTabListe, " WHERE ", nameof(tab_liste.id_lista), " = ", p_idLista.ToString());


            string v_sqlTabValidazione = string.Concat("UPDATE ", nameof(tab_validazione_approvazione_liste), " SET FLAG_ON_OFF = '0' ");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlTabValidazione = string.Concat(v_sqlTabValidazione, ", ", nameof(tab_validazione_approvazione_liste.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                             ", ", nameof(tab_validazione_approvazione_liste.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                             ", ", nameof(tab_validazione_approvazione_liste.data_stato), " = @data_stato");
            }

            v_sqlTabValidazione = string.Concat(v_sqlTabValidazione, " WHERE ", nameof(tab_liste.id_lista), " = ", p_idLista.ToString());

            string v_sqlJoincontrolli = string.Concat("UPDATE ", nameof(join_controlli_avvpag_fatt_emissione), " SET FLAG_ON_OFF = '0'");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlJoincontrolli = string.Concat(v_sqlJoincontrolli, ", ", nameof(join_controlli_avvpag_fatt_emissione.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                         ", ", nameof(join_controlli_avvpag_fatt_emissione.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                         ", ", nameof(join_controlli_avvpag_fatt_emissione.data_stato), " = @data_stato");
            }

            v_sqlJoincontrolli = string.Concat(v_sqlJoincontrolli, " WHERE ", nameof(join_controlli_avvpag_fatt_emissione.id_lista), " = ", p_idLista.ToString());



            TimeSpan v_timeOut = new TimeSpan(1, 0, 0);
            using (TransactionScope v_trans = new TransactionScope(TransactionScopeOption.Required, v_timeOut))
            {
                try
                {
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlUnitaContribuzioneFattEmissione, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlAvvPagSoggettoDebitoreFattEmissione, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    int v_numAvvisi = p_enteContext.Database.ExecuteSqlCommand(v_sqlAvvPagFattEmissione, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlTabListe, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlTabValidazione, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlJoincontrolli, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    v_trans.Complete();
                }
                catch (Exception ex)
                {
                    //loggare eventualmente
                    m_logger.LogException("Errore nell'annullamento del consolidamento: ", ex, Utility.Log.EnLogSeverity.Fatal);
                    return false;
                }
            }

            return true;
        }
    }
}
