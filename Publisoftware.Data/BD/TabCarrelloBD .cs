using Publisoftware.Data.POCOLight;
using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabCarrelloBD : EntityBD<tab_carrello>
    {
        #region Private Members
        private static ILogger logger = LoggerFactory.getInstance().getLogger<NLogger>("Publisoftware.Data.BD.TabCarrelloBD");
        #endregion Private Members

        #region Costructor
        public TabCarrelloBD()
        {

        }
        #endregion Costructor

        #region Public Methods
        /// <summary>
        /// Restituisce la lista di tutte le entità
        /// </summary>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <param name="p_includeEntities">Elenco di tabelle collegate da includere durante la select</param>
        /// <returns></returns>
        public static new IQueryable<tab_carrello> GetList(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            /// Ridefinisce la GetList per implementare la sicurezza di accesso sul contribuente
            return GetListInternal(p_dbContext, p_includeEntities).Where(d => p_dbContext.idContribuenteDefaultList.Count == 0 || p_dbContext.idContribuenteDefaultList.Contains(d.id_contribuente_versante.Value));
        }

        /// <summary>
        /// Restituisce l'entità a partire dalla chiave primaria
        /// </summary>
        /// <param name="p_id">Chiave primaria</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static new tab_carrello GetById(Int32 p_id, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c => c.id_carrello == p_id);
        }
        public static tab_carrello GetCarrelloByIdSessione(string p_idSessione, dbEnte p_dbContext)
        {
            logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            return GetList(p_dbContext).Where(d => d.id_pagamento_session == p_idSessione).FirstOrDefault();
        }
        public static int GetNextProgressive(Int32 p_id, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Max(x => x.id_carrello + 1);
        }
        /// <summary>
        /// Salva il carrello dei pagamenti
        /// </summary>
        /// <param name="carrello"></param>
        /// <param name="listRateCarrello"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static bool SaveCarrello(tab_carrello carrello,List<join_tab_carrello_tab_rate> listRateCarrello, dbEnte p_dbContext, out int p_idCarrello)
        {
            p_idCarrello = -1;
            using (var v_trans = p_dbContext.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
            {
                try
                {
                    if (carrello==null 
                        && (listRateCarrello==null || listRateCarrello.Count()<=0))
                    {
                        return false;
                    }
                    p_dbContext.tab_carrello.Add(carrello);
                    p_dbContext.SaveChanges();

                    p_idCarrello = carrello.id_carrello;
                    logger.LogInfoMessage(string.Format("Aggiunta del carrello {0} dei pagamenti.", carrello.identificativo_carrello));

                    foreach (join_tab_carrello_tab_rate rt in listRateCarrello)
                    {
                        rt.id_carrello = p_idCarrello;
                        p_dbContext.join_tab_carrello_tab_rate.Add(rt);

                    }
                    logger.LogInfoMessage(string.Format("Aggiunta dei pagamenti associati al carrello {0} dei pagamenti.", carrello.identificativo_carrello));

                    p_dbContext.SaveChanges();
                    v_trans.Commit();
                    logger.LogInfoMessage(string.Format("Salvataggio del carrello {0} dei pagamenti eseguito con successo.", carrello.identificativo_carrello));
                    return true;
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException  e)
                {
                    p_idCarrello = -1;
                    v_trans.Rollback();
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("L'Entità di tipo \"{0}\" in stato \"{1}\" presenta i seguenti errori di validazione:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        logger.LogException(string.Format("L'Entità di tipo {0} in stato {1} presenta i seguenti errori di validazione: ", eve.Entry.Entity.GetType().Name, eve.Entry.State), e, EnLogSeverity.Error);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Proprietà: \"{0}\", Errore: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                            logger.LogException(string.Format (" - Proprietà: {0}, Errore: {1}", ve.PropertyName, ve.ErrorMessage), e, EnLogSeverity.Error);
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    p_idCarrello = -1;
                    v_trans.Rollback();
                    logger.LogException("Errore in fase di salvataggio del carrello dei pagamenti. ", ex, EnLogSeverity.Error);
                    return false;
                }
            }
        }
        /// <summary>
        /// Salva il carrello dei pagamenti
        /// </summary>
        /// <param name="carrello"></param>
        /// <param name="listRateCarrello"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public async  static Task<int> SaveCarrelloAsync(tab_carrello carrello, List<join_tab_carrello_tab_rate> listRateCarrello, dbEnte p_dbContext)
        {
            int p_idCarrello = -1;
            using (var v_trans = p_dbContext.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
            {
                try
                {
                    if (carrello == null
                        && (listRateCarrello == null || listRateCarrello.Count() <= 0))
                    {
                        return -1;
                    }
                    p_dbContext.tab_carrello.Add(carrello);
                    await p_dbContext.SaveChangesAsync();

                    p_idCarrello = carrello.id_carrello;
                    logger.LogInfoMessage(string.Format("Aggiunta del carrello {0} dei pagamenti.", carrello.identificativo_carrello));

                    foreach (join_tab_carrello_tab_rate rt in listRateCarrello)
                    {
                        rt.id_carrello = p_idCarrello;
                        p_dbContext.join_tab_carrello_tab_rate.Add(rt);

                    }
                    logger.LogInfoMessage(string.Format("Aggiunta dei pagamenti associati al carrello {0} dei pagamenti.", carrello.identificativo_carrello));

                    await p_dbContext.SaveChangesAsync();
                    v_trans.Commit();
                    logger.LogInfoMessage(string.Format("Salvataggio del carrello {0} dei pagamenti eseguito con successo.", carrello.identificativo_carrello));
                    return p_idCarrello;
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    p_idCarrello = -1;
                    v_trans.Rollback();
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("L'Entità di tipo \"{0}\" in stato \"{1}\" presenta i seguenti errori di validazione:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        logger.LogException(string.Format("L'Entità di tipo {0} in stato {1} presenta i seguenti errori di validazione: ", eve.Entry.Entity.GetType().Name, eve.Entry.State), e, EnLogSeverity.Error);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Proprietà: \"{0}\", Errore: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                            logger.LogException(string.Format(" - Proprietà: {0}, Errore: {1}", ve.PropertyName, ve.ErrorMessage), e, EnLogSeverity.Error);
                        }
                    }
                    return p_idCarrello;
                }
                catch (Exception ex)
                {
                    p_idCarrello = -1;
                    v_trans.Rollback();
                    logger.LogException("Errore in fase di salvataggio del carrello dei pagamenti. ", ex, EnLogSeverity.Error);
                    return p_idCarrello;
                }
            }
        }
        public static bool UpdateAvvisoRateCarrello(List<tab_rata_avv_pag_light> listCarrello,  dbEnte p_dbContext, bool IsSuccess=true)
        {
            decimal tot_pag_avviso = 0;
            using (var v_trans = p_dbContext.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
            {
                try
                {
                    if (listCarrello == null || listCarrello.Count() <= 0)
                    {
                        return false;
                    }
                    List<int> idsAvvisi = listCarrello.Select(x => x.IdTabAvvPag).Distinct().ToList();
                    if (IsSuccess)
                    {
                        //05/02/2021
                        //SU INDICAZIONE DEL DOTTORE EVITO DI AGGIORNARE 
                        //IMP_TOT_PAGATO E STATO DELL'AVVISO
                        //foreach (int id in idsAvvisi)
                        //{
                        //    tot_pag_avviso = 0;
                        //    tot_pag_avviso = listCarrello.Where(x => x.IdTabAvvPag == id).Sum(x => x.Importo_Pagato);
                        //    tab_avv_pag avviso = TabAvvPagBD.GetById(id, p_dbContext);
                        //    avviso.imp_tot_pagato = avviso.imp_tot_pagato + tot_pag_avviso;
                        //    if (avviso.imp_tot_pagato == avviso.importo_tot_da_pagare ||
                        //        (avviso.imp_tot_pagato == avviso.importo_tot_da_pagare - 1))
                        //    {
                        //        avviso.cod_stato = tab_rata_avv_pag.ATT_PAG;
                        //    }
                        //    logger.LogInfoMessage(string.Format("Aggiornamento stato pagamento avviso {0}.", id));
                        //    p_dbContext.SaveChanges();
                        //}

                        foreach (tab_rata_avv_pag_light rt in listCarrello)
                        {
                            tab_rata_avv_pag rata = TabRataAvvPagBD.GetById(rt.id_rata_avv_pag, p_dbContext);
                            rata.imp_pagato = rata.imp_pagato + rt.Importo_Pagato;
                            //if (rata.imp_pagato == rata.imp_tot_rata ||
                            //    (rata.imp_pagato == rata.imp_tot_rata - 1))
                            //{
                            //    rata.cod_stato = tab_rata_avv_pag.ATT_PAG;
                            //}
                            //05/02/2021 - aggiornamento su indicazione del dottore
                            if (rata.imp_pagato >=rata.imp_tot_rata )
                            {
                                rata.cod_stato = anagrafica_stato_carrello.ATT_PGT;
                                rata.id_stato = anagrafica_stato_carrello.ATT_PGT_ID;
                            }
                            logger.LogInfoMessage(string.Format("Aggiornamento stato rata {0}.", rt.id_rata_avv_pag));
                            p_dbContext.SaveChanges();
                        }
                    }
                    else
                    {
                        foreach (tab_rata_avv_pag_light rt in listCarrello)
                        {
                            tab_rata_avv_pag rata = TabRataAvvPagBD.GetById(rt.id_rata_avv_pag, p_dbContext);
                           
                            rata.cod_stato = tab_rata_avv_pag.ATT_ATT;
                            rata.id_stato = tab_rata_avv_pag.ATT_ATT_ID;
                            logger.LogInfoMessage(string.Format("Aggiornamento stato rata {0}.", rt.id_rata_avv_pag));
                            p_dbContext.SaveChanges();
                        }
                    }
               

                    v_trans.Commit();
                    logger.LogInfoMessage("Aggiornamento pagamento avvisi eseguito con successo.");
                    return true;
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    v_trans.Rollback();
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("L'Entità di tipo \"{0}\" in stato \"{1}\" presenta i seguenti errori di validazione:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        logger.LogException(string.Format("L'Entità di tipo {0} in stato {1} presenta i seguenti errori di validazione: ", eve.Entry.Entity.GetType().Name, eve.Entry.State), e, EnLogSeverity.Error);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Proprietà: \"{0}\", Errore: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                            logger.LogException(string.Format(" - Proprietà: {0}, Errore: {1}", ve.PropertyName, ve.ErrorMessage), e, EnLogSeverity.Error);
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    v_trans.Rollback();
                    logger.LogException("Errore in fase di salvataggio del carrello dei pagamenti. ", ex, EnLogSeverity.Error);
                    return false;
                }
            }
        }

        public static bool UpdateAvvisoRateCarrelloByJoin(List<join_tab_carrello_tab_rate_light> listCarrello, dbEnte p_dbContext, bool IsSuccess = true)
        {
            decimal tot_pag_avviso = 0;
            using (var v_trans = p_dbContext.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
            {
                try
                {
                    if (listCarrello == null || listCarrello.Count() <= 0)
                    {
                        return false;
                    }
                    List<int> idsAvvisi = listCarrello.Select(x => x.id_tab_avv_pag).Distinct().ToList();
                    if (IsSuccess)
                    {
                        foreach (int id in idsAvvisi)
                        {
                            tot_pag_avviso = 0;
                            tot_pag_avviso = listCarrello.Where(x => x.id_tab_avv_pag == id).Sum(x => x.importo_da_pagare_rata);
                            tab_avv_pag avviso = TabAvvPagBD.GetById(id, p_dbContext);
                            avviso.imp_tot_pagato = avviso.imp_tot_pagato + tot_pag_avviso;
                            if (avviso.imp_tot_pagato == avviso.importo_tot_da_pagare ||
                                (avviso.imp_tot_pagato == avviso.importo_tot_da_pagare - 1))
                            {
                                avviso.cod_stato = tab_rata_avv_pag.ATT_PAG;
                            }
                            logger.LogInfoMessage(string.Format("Aggiornamento stato pagamento avviso {0}.", id));
                            p_dbContext.SaveChanges();
                        }

                        foreach (join_tab_carrello_tab_rate_light rt in listCarrello)
                        {
                            tab_rata_avv_pag rata = TabRataAvvPagBD.GetById(rt.id_rata, p_dbContext);
                            rata.imp_pagato = rata.imp_pagato + rt.importo_da_pagare_rata;
                            if (rata.imp_pagato == rata.imp_tot_rata ||
                                (rata.imp_pagato == rata.imp_tot_rata - 1))
                            {
                                rata.cod_stato = tab_rata_avv_pag.ATT_PAG;
                            }
                            logger.LogInfoMessage(string.Format("Aggiornamento stato rata {0}.", rt.id_rata));
                            p_dbContext.SaveChanges();
                        }
                    }
                    else
                    {
                        foreach (join_tab_carrello_tab_rate_light rt in listCarrello)
                        {
                            tab_rata_avv_pag rata = TabRataAvvPagBD.GetById(rt.id_rata, p_dbContext);

                            rata.cod_stato = tab_rata_avv_pag.ATT_ATT;
                            rata.id_stato = tab_rata_avv_pag.ATT_ATT_ID;
                            logger.LogInfoMessage(string.Format("Aggiornamento stato rata {0}.", rt.id_rata));
                            p_dbContext.SaveChanges();
                        }
                    }


                    v_trans.Commit();
                    logger.LogInfoMessage("Aggiornamento pagamento avvisi eseguito con successo.");
                    return true;
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    v_trans.Rollback();
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("L'Entità di tipo \"{0}\" in stato \"{1}\" presenta i seguenti errori di validazione:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        logger.LogException(string.Format("L'Entità di tipo {0} in stato {1} presenta i seguenti errori di validazione: ", eve.Entry.Entity.GetType().Name, eve.Entry.State), e, EnLogSeverity.Error);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Proprietà: \"{0}\", Errore: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                            logger.LogException(string.Format(" - Proprietà: {0}, Errore: {1}", ve.PropertyName, ve.ErrorMessage), e, EnLogSeverity.Error);
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    v_trans.Rollback();
                    logger.LogException("Errore in fase di salvataggio del carrello dei pagamenti. ", ex, EnLogSeverity.Error);
                    return false;
                }
            }
        }

        public static bool UpdateStatoCarrelloAndRate(tab_carrello carrello, List<join_tab_carrello_tab_rate> listRateCarrello,int p_idstato, string p_codstato, dbEnte p_dbContext)
        {
            using (var v_trans = p_dbContext.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
            {
                try
                {
                    if (carrello == null
                        && (listRateCarrello == null || listRateCarrello.Count() <= 0))
                    {
                        return false;
                    }
                    carrello.id_stato = p_idstato;
                    carrello.cod_stato = p_codstato;
                    p_dbContext.SaveChanges();

                    logger.LogInfoMessage(string.Format("Aggiornato lo stato del carrello {0} dei pagamenti.", carrello.identificativo_carrello));

                    foreach (join_tab_carrello_tab_rate rt in listRateCarrello)
                    {
                        rt.id_stato = p_idstato;
                        rt.cod_stato = p_codstato;
                        //p_dbContext.join_tab_carrello_tab_rate.Add(rt);

                    }
                    logger.LogInfoMessage(string.Format("Aggiornato lo stato dei pagamenti associati al carrello {0} dei pagamenti.", carrello.identificativo_carrello));

                    p_dbContext.SaveChanges();
                    v_trans.Commit();
                    logger.LogInfoMessage(string.Format("Salvataggio del carrello {0} dei pagamenti eseguito con successo.", carrello.identificativo_carrello));
                    return true;
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("L'Entità di tipo \"{0}\" in stato \"{1}\" presenta i seguenti errori di validazione:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        logger.LogException(string.Format("L'Entità di tipo {0} in stato {1} presenta i seguenti errori di validazione: ", eve.Entry.Entity.GetType().Name, eve.Entry.State), e, EnLogSeverity.Error);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Proprietà: \"{0}\", Errore: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                            logger.LogException(string.Format(" - Proprietà: {0}, Errore: {1}", ve.PropertyName, ve.ErrorMessage), e, EnLogSeverity.Error);
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    v_trans.Rollback();
                    logger.LogException("Errore in fase di aggiornamento dello stato del carrello dei pagamenti. ", ex, EnLogSeverity.Error);
                    return false;
                }
            }
        }

        #endregion Public Methods
    }
}
