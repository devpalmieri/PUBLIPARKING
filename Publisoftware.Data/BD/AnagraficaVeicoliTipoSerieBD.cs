using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaVeicoliTipoSerieBD : EntityBD<anagrafica_veicoli_tipo_serie>
    {
        public AnagraficaVeicoliTipoSerieBD()
        {

        }

        /// <summary>
        /// Ottiene la lista filtrata per parola chiave
        /// </summary>
        /// <param name="p_text"></param>
        /// <param name="p_dbContext"></param>
        /// <param name="p_includeEntities"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_veicoli_tipo_serie> GetListContains(String p_text, dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetList(p_dbContext, p_includeEntities).Where(a => a.tipo.Contains(p_text) || a.serie.Contains(p_text) || a.anagrafica_veicoli_marche.descrizione.Contains(p_text));
        }

        /// <summary>
        /// Controllo se è presente un altro elemento con lo stesso tipo
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <param name="p_tipo">Tipo</param>
        /// <returns></returns>
        public static bool CheckTipoDuplicato(dbEnte p_dbContext, string p_tipo)
        {
                return GetList(p_dbContext).Any(d => d.tipo.Equals(p_tipo));
        }

        /// <summary>
        /// Controllo se è presente un altro elemento con la stessa serie
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <param name="p_serie">Serie</param>
        /// <returns></returns>
        public static bool CheckSerieDuplicato(dbEnte p_dbContext, string p_serie)
        {
                return GetList(p_dbContext).Any(d => d.serie.Equals(p_serie));
        }
    }
}
