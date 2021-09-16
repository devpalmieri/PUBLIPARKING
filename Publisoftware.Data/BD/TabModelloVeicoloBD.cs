using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabModelloVeicoloBD : EntityBD<tab_modello_veicolo>
    {
        public TabModelloVeicoloBD()
        {

        }

        /// <summary>
        /// Ottiene la lista filtrata per parola chiave
        /// </summary>
        /// <param name="p_text"></param>
        /// <param name="p_dbContext"></param>
        /// <param name="p_includeEntities"></param>
        /// <returns></returns>
        public static IQueryable<tab_modello_veicolo> GetListContains(String p_text, dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetList(p_dbContext, p_includeEntities).Where(a => a.descr_modello.Contains(p_text) || a.descr_serie.Contains(p_text) || a.anagrafica_tipo_veicolo.descrizione.Contains(p_text) || a.tab_marca_veicolo.descr_marca.Contains(p_text));
        }
    }
}
