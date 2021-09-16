using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class SerStatiEsteriNewBD : EntityCacheBD<ser_stati_esteri_new>
    {
        public SerStatiEsteriNewBD()
        {

        }

        public static new IQueryable<ser_stati_esteri_new> GetList(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetListInternal(p_dbContext, p_includeEntities).Where(a => a.flag_on_off == "1");
        }

        public static IQueryable<ser_stati_esteri_new> GetListAll(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetListInternal(p_dbContext, p_includeEntities);
        }

        /// <summary>
        /// Ottiene la lista filtrata per parola chiave
        /// </summary>
        /// <param name="p_text"></param>
        /// <param name="p_dbContext"></param>
        /// <param name="p_includeEntities"></param>
        /// <returns></returns>
        public static IQueryable<ser_stati_esteri_new> GetListContains(String p_text, dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetList(p_dbContext, p_includeEntities).Where(a => a.denominazione_stato.Contains(p_text) || a.sigla_stato.Contains(p_text))
                                                          .OrderBy(o => o.denominazione_stato);
        }

        /// <summary>
        /// Controlla se la sigla dello stato è presente tra quelle valide
        /// </summary>
        /// <param name="p_codiceStato"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static bool isStatoValido(string p_denominazioneStato, dbEnte p_dbContext)
        {
            // Sati caricando tutti gli stati in memoria:
            // return GetList(p_dbContext).ToList().Exists(d => d.denominazione_stato.ToUpper().Equals(p_denominazioneStato.ToUpper()));
            return GetList(p_dbContext).Where(x => x.denominazione_stato.ToUpper().Equals(p_denominazioneStato.ToUpper())).FirstOrDefault() != null;
        }
    }
}
