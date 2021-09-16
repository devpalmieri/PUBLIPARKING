using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.LinqExtended;
using System.Data.Entity;
using Publisoftware.Data.POCOLight;
using Publisoftware.Utility.Log;
using System.Transactions;
using System.Data.SqlClient;
using System.Data;

namespace Publisoftware.Data.BD
{
    public class JoinTabIspezioniCoattivoTipoIspezioneBD : EntityBD<join_tab_ispezioni_coattivo_tipo_ispezione>
    {
        #region Private Members
        private static ILogger logger = LoggerFactory.getInstance().getLogger<NLogger>("Publisoftware.Data.BD.JoinTabIspezioniCoattivoTipoIspezioneBD");
        #endregion Private Members

        #region Costructor
        public JoinTabIspezioniCoattivoTipoIspezioneBD()
        { }
        #endregion Costructor

        #region Public Methods
        public static IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> GetListTipoIspezioni(string p_siglaTipoIspezione, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.tab_ispezioni_coattivo_new != null && c.cod_stato == CodStato.VAL_VAL && c.tab_tipo_ispezione.sigla_tipo_ispezione == p_siglaTipoIspezione && c.flag_fine_ispezione == "0" && (c.tab_ispezioni_coattivo_new.flag_emissione_avviso_coattivo == null || c.tab_ispezioni_coattivo_new.flag_emissione_avviso_coattivo == "0") && (c.tab_ispezioni_coattivo_new.flag_fine_ispezione_totale == null || c.tab_ispezioni_coattivo_new.flag_fine_ispezione_totale == "0"));
            //x test inserimento
            //return GetList(p_dbContext).Where(c => c.tab_ispezioni_coattivo_new != null && c.tab_ispezioni_coattivo_new.cfiscale_piva_soggetto_ispezione == "PRLLSN43L25A483L" && c.cod_stato == CodStato.VAL_VAL && c.tab_tipo_ispezione.sigla_tipo_ispezione == p_siglaTipoIspezione );
        }

        public static IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> GetListTipoIspezioniDaSupervisionare(dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.tab_ispezioni_coattivo_new != null && c.cod_stato == CodStato.VAL_VAL && c.flag_fine_ispezione == "2" && c.flag_esito_ispezione == "1" && (c.tab_ispezioni_coattivo_new.flag_supervisione_finale == null || c.tab_ispezioni_coattivo_new.flag_supervisione_finale == "0"));
            //x test                
            //return GetList(p_dbContext).Where(c => c.id_tab_ispezione_coattivo== 66989) ;
            //(c.id_tab_ispezione_coattivo== 67186 || c.id_tab_ispezione_coattivo == 67092 || c.id_tab_ispezione_coattivo == 67067 || c.id_tab_ispezione_coattivo == 67153)&& 67186,67092,67067,67153
        }
        /// <summary>
        /// Recupera le ispezioni del coattivo
        /// da aggiornare
        /// </summary>
        /// <param name="sigla"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static List<join_tab_ispezioni_coattivo_tipo_ispezione> GetListTipoIspezioniDaAggiornare(string sigla, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.tab_ispezioni_coattivo_new.cod_stato == CodStato.VAL_VAL && c.cod_stato == CodStato.VAL_VAL
                && c.flag_esito_ispezione == "0" && c.tab_tipo_ispezione.sigla_tipo_ispezione.ToUpper() == sigla.ToUpper()).ToList();

        }
        /// <summary>
        /// Avvia il processo per la chiusura/annullamento
        /// delle ispezioni del coattivo
        /// in base alla sigla relativa 
        /// alla tipologia di ispezione
        /// </summary>
        /// <param name="sigla">Sigla relativa al tipo di ispezione</param>
        /// <param name="p_dbContext">Ente-dbEnte corrente</param>
        public static bool ChiudiOrAnnullaIspezioni(string sigla, dbEnte p_dbContext, out string errorDescription)
        {
            bool bExec = false;
            errorDescription = string.Empty;
            string v_sqlCommand = string.Empty;
            string v_insertCommand = string.Empty;
            string v_update = string.Empty;
            if (sigla.Length > 0)
            {
                TimeSpan v_timeOut = new TimeSpan(1, 0, 0);
                using (TransactionScope v_trans = new TransactionScope(TransactionScopeOption.Required, v_timeOut))
                {
                    try
                    {
                        List<join_tab_ispezioni_coattivo_tipo_ispezione> listIspezioni = GetList(p_dbContext).Where(c => c.tab_ispezioni_coattivo_new.cod_stato == CodStato.VAL_VAL && c.cod_stato == CodStato.VAL_VAL
                         && c.flag_esito_ispezione == "0" && c.tab_tipo_ispezione.sigla_tipo_ispezione.ToUpper() == sigla.ToUpper()).ToList();

                        if (listIspezioni != null)
                        {
                            v_sqlCommand = GetExecSqlCommand(sigla, out v_update, out errorDescription);
                            if (!string.IsNullOrEmpty(errorDescription))
                                return false;

                            if (!string.IsNullOrEmpty(v_sqlCommand))
                            {
                                if (v_update == join_tab_ispezioni_coattivo_tipo_ispezione.CLOSE)
                                {
                                    p_dbContext.Database.ExecuteSqlCommand(v_sqlCommand
                                       , new SqlParameter("@FLAG_ESITO_ISPEZIONE", "2")
                                       , new SqlParameter("@FLAG_FINE_ISPEZIONE", "2")
                                       , new SqlParameter("@SIGLA", sigla));

                                    if (sigla=="DIS")
                                    {
                                        v_insertCommand= GetInsertExecSqlCommand(sigla,  out errorDescription, p_dbContext.IdRisorsa, p_dbContext.IdStruttura);
                                        p_dbContext.Database.ExecuteSqlCommand(v_insertCommand);
                                    }
                                }
                                else
                                {
                                    p_dbContext.Database.ExecuteSqlCommand(v_sqlCommand
                                       , new SqlParameter("@COD_STATO", join_tab_ispezioni_coattivo_tipo_ispezione.ANN_ANN)
                                       , new SqlParameter("@SIGLA", sigla));
                                }
                                bExec = true;
                            }

                            if (bExec)
                            {
                                p_dbContext.SaveChanges();
                                v_trans.Complete();
                            }
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        errorDescription = "Errore in fase di aggiornamento delle ispezioni: " + ex.Message;
                        logger.LogException("Errore in fase di chiusura/annullamento delle ispezioni coattivo.", ex, EnLogSeverity.Error);
                        return false;
                    }
                }
            }
            errorDescription = "Tipo di ispezione da aggiornare non presente.";
            return false;
        }

        /// <summary>
        /// Restituisce un elenco con lo stato delle ispezioni del coattivo
        /// di tipo ACI eseguite
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static List<Dettaglio_Verifica_Stato_Ispezioni_Coattivo_ACI_Light> GetIspezioniCoattivoFatte(dbEnte p_dbContext, out string errorDescription)
        {
            errorDescription = string.Empty;
            try
            {
                var result = (from j in p_dbContext.join_tab_ispezioni_coattivo_tipo_ispezione
                              join i in p_dbContext.tab_ispezioni_coattivo_new on j.id_tab_ispezione_coattivo equals i.id_tab_ispezione_coattivo
                              where i.cod_stato == tab_ispezioni_coattivo_new.VAL_OLD && j.id_tab_tipo_ispezione == 1
                              && j.data_fine_ispezione > i.data_rilevazione_morosita & j.flag_fine_ispezione != "0" //j.cod_stato == tab_ispezioni_coattivo_new.VAL_VAL & i.cod_stato == tab_ispezioni_coattivo_new.VAL_VAL
                              select new { j.data_fine_ispezione.Value.Year, j.data_fine_ispezione.Value.Month, j.id_tab_tipo_ispezione } into x
                              group x by new { x.Year, x.Month } into g                              
                              orderby g.Key.Year descending, g.Key.Month ascending
                              select new Dettaglio_Verifica_Stato_Ispezioni_Coattivo_ACI_Light()
                              {
                                  AnnoString = g.Key.Year.ToString(),
                                  Anno = g.Key.Year,
                                  Mese = g.Key.Month,
                                  TotaleParziale = g.Select(x => x.id_tab_tipo_ispezione).Count()
                              }).ToList();
                if (result != null)
                {
                    Dettaglio_Verifica_Stato_Ispezioni_Coattivo_ACI_Light tot = new Dettaglio_Verifica_Stato_Ispezioni_Coattivo_ACI_Light(0, 20, result.Sum(x => x.TotaleParziale));
                    result.Add(tot);
                }
                return result;
            }
            catch (Exception ex)
            {
                errorDescription = "Errore in fase del caricamento delle ispezioni ACI eseguite: " + ex.Message;
                logger.LogException("Errore in fase del caricamento delle ispezioni ACI eseguite.", ex, EnLogSeverity.Error);
                return null;
            }
        }
        /// <summary>
        /// Restituisce un elenco con lo stato delle ispezioni del coattivo
        /// di tipo ACI riciclate
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static List<Dettaglio_Verifica_Stato_Ispezioni_Coattivo_ACI_Light> GetIspezioniCoattivoRiciclate(dbEnte p_dbContext, out string errorDescription)
        {
            errorDescription = string.Empty;
            try
            {
                var result = (from j in p_dbContext.join_tab_ispezioni_coattivo_tipo_ispezione
                              join i in p_dbContext.tab_ispezioni_coattivo_new on j.id_tab_ispezione_coattivo equals i.id_tab_ispezione_coattivo
                              where i.cod_stato == tab_ispezioni_coattivo_new.VAL_OLD && j.id_tab_tipo_ispezione == 1
                              && j.data_fine_ispezione < i.data_rilevazione_morosita & j.flag_fine_ispezione != "0"
                              select new { j.data_fine_ispezione.Value.Year, j.data_fine_ispezione.Value.Month, j.id_tab_tipo_ispezione } into x
                              group x by new { x.Year, x.Month } into g
                              orderby g.Key.Year descending, g.Key.Month ascending
                              select new Dettaglio_Verifica_Stato_Ispezioni_Coattivo_ACI_Light()
                              {
                                  AnnoString = g.Key.Year.ToString(),
                                  Anno = g.Key.Year,
                                  Mese = g.Key.Month,
                                  TotaleParziale = g.Select(x => x.id_tab_tipo_ispezione).Count()
                              }).ToList();
                if (result != null)
                {
                    Dettaglio_Verifica_Stato_Ispezioni_Coattivo_ACI_Light tot = new Dettaglio_Verifica_Stato_Ispezioni_Coattivo_ACI_Light(0, 20, result.Sum(x => x.TotaleParziale));
                    result.Add(tot);
                }
                return result;
            }
            catch (Exception ex)
            {
                errorDescription = "Errore in fase del caricamento delle ispezioni ACI riciclate: " + ex.Message;
                logger.LogException("Errore in fase del caricamento delle ispezioni ACI riciclate.", ex, EnLogSeverity.Error);
                return null;
            }
        }
        #endregion Public Methods

        #region Private Methods
        /// <summary>
        /// Costruisce l'SQL per l'esecuzione
        /// dell'aggiornamento
        /// </summary>
        /// <param name="sigla">Tipo ispezione</param>
        /// <param name="updateType">Tipo di aggiornamento da eseguire - out</param>
        /// <returns></returns>
        private static string GetExecSqlCommand(string sigla, out string updateType, out string errorDescription)
        {
            string sqlCommand = string.Empty;
            errorDescription = string.Empty;
            updateType = join_tab_ispezioni_coattivo_tipo_ispezione.CLOSE;
            try
            {
                switch (sigla.Trim().ToUpper())
                {
                    case tab_tipo_ispezione.ACI:
                    case tab_tipo_ispezione.IMMOBILI:
                    case tab_tipo_ispezione.SIATEL:
                        sqlCommand = string.Concat("UPDATE J SET ", nameof(join_tab_ispezioni_coattivo_tipo_ispezione.cod_stato), " = @COD_STATO ",
                                                        "FROM ", nameof(join_tab_ispezioni_coattivo_tipo_ispezione),
                                                        " J INNER JOIN ", nameof(tab_ispezioni_coattivo_new), " I ON J.", nameof(join_tab_ispezioni_coattivo_tipo_ispezione.id_tab_ispezione_coattivo), "=I.", nameof(tab_ispezioni_coattivo_new.id_tab_ispezione_coattivo),
                                                        " INNER JOIN ", nameof(tab_tipo_ispezione), " T ON J.", nameof(join_tab_ispezioni_coattivo_tipo_ispezione.id_tab_tipo_ispezione), "=T.", nameof(tab_tipo_ispezione.id_tab_tipo_ispezione),
                                                        " WHERE J.", nameof(join_tab_ispezioni_coattivo_tipo_ispezione.cod_stato), " = '", CodStato.VAL_VAL, "' AND I.", nameof(tab_ispezioni_coattivo_new.cod_stato), " = '", CodStato.VAL_VAL,
                                                        "' AND J.", nameof(join_tab_ispezioni_coattivo_tipo_ispezione.flag_esito_ispezione), "='0'", " AND T.", nameof(tab_tipo_ispezione.sigla_tipo_ispezione), " = @SIGLA");

                        updateType = join_tab_ispezioni_coattivo_tipo_ispezione.UNDO;
                        return sqlCommand;
                    case tab_tipo_ispezione.LOCAZIONI:
                    case tab_tipo_ispezione.DISPONIBILITA_FINANZIARIA:
                        sqlCommand = string.Concat("UPDATE J SET ", nameof(join_tab_ispezioni_coattivo_tipo_ispezione.flag_esito_ispezione), " = @FLAG_ESITO_ISPEZIONE, ",
                                                        nameof(join_tab_ispezioni_coattivo_tipo_ispezione.flag_fine_ispezione), " = @FLAG_FINE_ISPEZIONE ",
                                                        "FROM ", nameof(join_tab_ispezioni_coattivo_tipo_ispezione),
                                                        " J INNER JOIN ", nameof(tab_ispezioni_coattivo_new), " I ON J.", nameof(join_tab_ispezioni_coattivo_tipo_ispezione.id_tab_ispezione_coattivo), "=I.", nameof(tab_ispezioni_coattivo_new.id_tab_ispezione_coattivo),
                                                        " INNER JOIN ", nameof(tab_tipo_ispezione), " T ON J.", nameof(join_tab_ispezioni_coattivo_tipo_ispezione.id_tab_tipo_ispezione), "=T.", nameof(tab_tipo_ispezione.id_tab_tipo_ispezione),
                                                        " WHERE J.", nameof(join_tab_ispezioni_coattivo_tipo_ispezione.cod_stato), " = '", CodStato.VAL_VAL, "' AND I.", nameof(tab_ispezioni_coattivo_new.cod_stato), " = '", CodStato.VAL_VAL,
                                                        "' AND J.", nameof(join_tab_ispezioni_coattivo_tipo_ispezione.flag_esito_ispezione), "='0'", " AND T.", nameof(tab_tipo_ispezione.sigla_tipo_ispezione), " = @SIGLA");
                        updateType = join_tab_ispezioni_coattivo_tipo_ispezione.CLOSE;
                        return sqlCommand;
                }
            }
            catch (Exception ex)
            {

                errorDescription = "Errore in fase di costruzione del comando di aggiornamento: " + ex.Message;
                logger.LogException("Errore in fase di chiusura/annullamento delle ispezioni coattivo.", ex, EnLogSeverity.Error);
                return null;
            }
            return null;
        }
        /// <summary>
        /// Costruisce l'SQL per l'esecuzione
        /// dell'aggiornamento
        /// </summary>
        /// <param name="sigla">Tipo ispezione</param>
        /// <param name="updateType">Tipo di aggiornamento da eseguire - out</param>
        /// <returns></returns>
        private static string GetInsertExecSqlCommand(string sigla, out string errorDescription, int p_idRisorsa, int p_idStruttura)
        {
            string sqlCommand = string.Empty;
            errorDescription = string.Empty;
            try
            {
                switch (sigla.Trim().ToUpper())
                {
                    case tab_tipo_ispezione.ACI:
                    case tab_tipo_ispezione.IMMOBILI:
                    case tab_tipo_ispezione.SIATEL:
                    case tab_tipo_ispezione.LOCAZIONI:
                        return sqlCommand;
                    
                    case tab_tipo_ispezione.DISPONIBILITA_FINANZIARIA:

                        sqlCommand = string.Concat("INSERT INTO ", nameof(TAB_SUPERVISIONE_FINALE_V2), "(", nameof(TAB_SUPERVISIONE_FINALE_V2.ID_TIPO_AVVPAG_DA_EMETTERE), ",",
                            nameof(TAB_SUPERVISIONE_FINALE_V2.COD_STATO), ",",
                            nameof(TAB_SUPERVISIONE_FINALE_V2.ID_TAB_ISPEZIONE_COATTIVO), ",", nameof(TAB_SUPERVISIONE_FINALE_V2.ID_CONTRIBUENTE), ",",
                            nameof(TAB_SUPERVISIONE_FINALE_V2.ID_ENTE), ",", nameof(TAB_SUPERVISIONE_FINALE_V2.DATA_SUPERVISIONE_FINALE), ",",
                             nameof(TAB_SUPERVISIONE_FINALE_V2.ID_SUPERVISORE_FINALE), ", FLAG_ON_OFF, ",
                             nameof(TAB_SUPERVISIONE_FINALE_V2.ID_RISORSA_STATO), ", ", nameof(TAB_SUPERVISIONE_FINALE_V2.ID_STRUTTURA_STATO), ", ", nameof(TAB_SUPERVISIONE_FINALE_V2.DATA_STATO), ") ",
                             " SELECT " , anagrafica_tipo_avv_pag.RICHIESTA_STRAGIUDIZIALE, ",'", TAB_SUPERVISIONE_FINALE_V2.VAL_VAL, "', J.", nameof(join_tab_ispezioni_coattivo_tipo_ispezione.id_tab_ispezione_coattivo), ", I.", nameof(tab_ispezioni_coattivo_new.id_contribuente),
                             ", ", nameof(tab_ispezioni_coattivo_new.id_ente), ", '", DateTime.Now, "', ", p_idRisorsa.ToString(), ", '1', ", p_idRisorsa, ", ", p_idStruttura, ", '", DateTime.Now,
                             "' FROM ", nameof(tab_ispezioni_coattivo_new), " I INNER JOIN ", nameof(join_tab_ispezioni_coattivo_tipo_ispezione), " J ON  I.",
                              nameof(tab_ispezioni_coattivo_new.id_tab_ispezione_coattivo), "= J.", nameof(join_tab_ispezioni_coattivo_tipo_ispezione.id_tab_ispezione_coattivo),
                              " WHERE I.FLAG_ON_OFF='1' AND J.FLAG_ON_OFF='1' AND I.", nameof(tab_ispezioni_coattivo_new.cod_stato), "='", tab_ispezioni_coattivo_new.VAL_VAL, "' AND J.",
                              nameof(join_tab_ispezioni_coattivo_tipo_ispezione.flag_esito_ispezione), "='2' AND J.", nameof(join_tab_ispezioni_coattivo_tipo_ispezione.flag_fine_ispezione), "='2' AND ",
                               nameof(join_tab_ispezioni_coattivo_tipo_ispezione.id_tab_tipo_ispezione), "=2");
                        return sqlCommand;
                }
            }
            catch (Exception ex)
            {

                errorDescription = "Errore in fase di costruzione del comando di aggiornamento: " + ex.Message;
                logger.LogException("Errore in fase di chiusura/annullamento delle ispezioni coattivo.", ex, EnLogSeverity.Error);
                return null;
            }
            return null;
        }
        #endregion Private Methods

    }
}
