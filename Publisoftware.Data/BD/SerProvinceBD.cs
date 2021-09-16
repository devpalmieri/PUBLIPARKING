using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class SerProvinceBD : EntityCacheBD<ser_province>
    {
        public SerProvinceBD()
        {

        }

        /// <summary>
        /// Ottiene la lista filtrata per parola chiave
        /// </summary>
        /// <param name="p_text"></param>
        /// <param name="p_dbContext"></param>
        /// <param name="p_includeEntities"></param>
        /// <returns></returns>
        public static IQueryable<ser_province> GetListContains(String p_text, dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetList(p_dbContext, p_includeEntities).Where(a => a.des_provincia.Contains(p_text) || a.sig_provincia.Contains(p_text) || a.ser_regioni.des_regione.Contains(p_text))
                                                          .OrderBy(o => o.des_provincia);
        }


        public static ser_province GetByCodProvincia(int v_codice, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.cod_provincia == v_codice).FirstOrDefault();
        }

        /// <summary>
        /// Controlla se la sigla della provincia è presente tra quelle valide
        /// </summary>
        /// <param name="p_siglaProvincia"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static bool isProvinciaValida(string p_siglaProvincia, dbEnte p_dbContext)
        {
            if (!string.IsNullOrEmpty(p_siglaProvincia))
            {
                return GetList(p_dbContext).ToList().Exists(d => d.sig_provincia.ToUpper().Equals(p_siglaProvincia.ToUpper()));
            }
            else
            {
                return true;
            }
        }
    }
}
