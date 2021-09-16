using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaUtilizzoBD : EntityBD<anagrafica_utilizzo>
    {
        public AnagraficaUtilizzoBD()
        {

        }

        /// <summary>
        /// Ottiene la lista filtrata per parola chiave
        /// </summary>
        /// <param name="p_text"></param>
        /// <param name="p_dbContext"></param>
        /// <param name="p_includeEntities"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_utilizzo> GetListContains(String p_text, dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetList(p_dbContext, p_includeEntities).Where(a => a.id_utilizzo.ToString().Contains(p_text) 
                || a.des_utilizzo.ToUpper().Contains(p_text.ToUpper()) 
                || a.codice.ToUpper().Contains(p_text.ToUpper())
                || a.flag_occupazione.Contains(p_text.ToUpper())
                || a.flag_proprieta.Contains(p_text.ToUpper())
                || a.flag_tipo_pubblicita.Contains(p_text.ToUpper()));
        }
    }
}
