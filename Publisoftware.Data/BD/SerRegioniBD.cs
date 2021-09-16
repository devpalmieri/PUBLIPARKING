using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class SerRegioniBD : EntityCacheBD<ser_regioni>
    {
        public SerRegioniBD()
        {

        }

        /// <summary>
        /// Ottiene la lista filtrata per parola chiave
        /// </summary>
        /// <param name="p_text"></param>
        /// <param name="p_dbContext"></param>
        /// <param name="p_includeEntities"></param>
        /// <returns></returns>
        public static IQueryable<ser_regioni> GetListContains(String p_text, dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetList(p_dbContext, p_includeEntities).Where(a => a.des_regione.Contains(p_text))
                                                          .OrderBy(o => o.des_regione);
        }

        /// <summary>
        /// Restituisce la regione partendo dal codice regione
        /// </summary>
        /// <param name="p_codRegione">Codice regione ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static ser_regioni GetByCodRegione(int p_codRegione, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(p => p.cod_regione.Equals(p_codRegione)).SingleOrDefault();
        }
    }
}
