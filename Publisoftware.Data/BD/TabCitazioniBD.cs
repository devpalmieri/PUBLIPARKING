using Publisoftware.Data.POCOLight;
using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabCitazioniBD : EntityBD<tab_citazioni>
    {
        #region Private Members
        private static ILogger logger = LoggerFactory.getInstance().getLogger<NLogger>("Publisoftware.Data.BD.JoinTabIspezioniCoattivoTipoIspezioneBD");
        #endregion Private Members

        public TabCitazioniBD()
        {

        }

        /// <summary>
        /// Ottiene la lista filtrata per parola chiave
        /// </summary>    
        /// <param name="p_text"></param>
        /// <param name="p_dbContext"></param>
        /// <param name="p_includeEntities"></param>
        /// <returns></returns>
        public static IQueryable<tab_citazioni> GetListCitazioniAcquisite(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetList(p_dbContext, p_includeEntities).Where(w => w.cod_stato == "ATT-ACQ" && (w.flag_assegnazione_ufficiale_riscossione == null || w.flag_assegnazione_ufficiale_riscossione == "0"));
        }

        /// <summary>
        /// Restituisce l'elenco del numero di citazioni
        /// raggruppate per Autorità Giudiziaria
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static List<AcquisizioniRG_Citazioni_Light> GetAcquisizioneRGCitazioni(dbEnte p_dbContext, out string errorDescription)
        {
            errorDescription = string.Empty;
            try
            {
                var result = (from j in p_dbContext.tab_citazioni
                              join i in p_dbContext.tab_autorita_giudiziaria on j.id_tab_autorita_giudiziaria equals i.id_tab_autorita_giudiziaria
                              where j.id_tab_autorita_giudiziaria > 0
                              && j.flag_iscrizione_ruolo !=null
                              && j.data_iscrizione_ruolo ==null
                              && j.numero_registro_iscrizione_ruolo==null
                              
                              select new { j.id_tab_autorita_giudiziaria, i.descrizione_autorita } into x
                              group x by new { x.id_tab_autorita_giudiziaria, x.descrizione_autorita } into g
                              orderby g.Key.descrizione_autorita  ascending
                              select new AcquisizioniRG_Citazioni_Light()
                              {
                                  id_autorita_giudiziaria=g.Key.id_tab_autorita_giudiziaria.HasValue ? g.Key.id_tab_autorita_giudiziaria.Value  : 0,
                                  AutoritaGiudiziaria=g.Key.descrizione_autorita,
                                  Totale = g.Select(x => x.id_tab_autorita_giudiziaria).Count()
                              }).ToList();
                
                return result;
            }
            catch (Exception ex)
            {
                errorDescription = "Errore in fase di caricamento delle autorità giudiziarie: " + ex.Message;
                logger.LogException("Errore in fase di caricamento delle autorità giudiziarie.", ex, EnLogSeverity.Error);
                return null;
            }
        }
    }
}
