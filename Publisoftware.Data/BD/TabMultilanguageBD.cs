using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabMultilanguageBD : EntityBD<tab_multilanguage>
    {
        public TabMultilanguageBD()
        {

        }

        /// <summary>
        /// Filtro per tipo, chiave e lingua
        /// </summary>
        /// <param name="p_tipo"></param>
        /// <param name="p_chiave"></param>
        /// <param name="p_language"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static tab_multilanguage GetByTipoChiaveLanguage(string p_tipo, string p_chiave, string p_language, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(m => m.tipo.ToLower() == p_tipo.ToLower() && m.chiave.ToLower() == p_chiave.ToLower() && m.language.ToLower() == p_language.ToLower()).SingleOrDefault();
        }

        /// <summary>
        /// Filtro per tipo, codice lingua e chiavi
        /// </summary>
        /// <param name="p_tipo"></param>
        /// <param name="p_chiavi"></param>
        /// <param name="p_languageCode"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_multilanguage> GetListByTipoChiave(string p_tipo, IEnumerable<string> p_chiavi, string p_languageCode, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(l => l.tipo.ToUpper() == p_tipo.ToUpper() && l.language.ToUpper() == p_languageCode.ToUpper() && p_chiavi.Contains(l.chiave));
        }
    }
}
