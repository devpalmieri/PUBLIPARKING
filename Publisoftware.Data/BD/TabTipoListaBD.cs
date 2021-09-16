using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabTipoListaBD : EntityBD<tab_tipo_lista>
    {
        public TabTipoListaBD()
        {

        }

        /// <summary>
        /// Ritorna i tab_tipo_lista la cui entrata matcha il valore dato in input
        /// </summary>
        /// <param name="p_idEntrata">ID Entrata</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static tab_tipo_lista GetByCodLista(string p_codLista, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(tl => tl.cod_lista.Equals(p_codLista.Trim())).SingleOrDefault();
        }
        public static tab_tipo_lista GetByCodListaAndIdEntrata(string p_codLista,int p_IdEntrata, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(tl => tl.cod_lista.Equals(p_codLista.Trim()) && tl.id_entrata== p_IdEntrata).SingleOrDefault();
        }
        //public static tab_tipo_lista GetByCodListaAndIdEntrata(string p_codLista, int p_IdEntrata, dbEnte p_dbContext)
        //{
        //    return GetList(p_dbContext).Where(tl => tl.cod_lista.Equals(p_codLista.Trim()) && tl.id_entrata == p_IdEntrata).SingleOrDefault();
        //}
        /// <summary>
        /// Ritorna i tab_tipo_lista la cui entrata matcha il valore dato in input
        /// </summary>
        /// <param name="p_idEntrata">ID Entrata</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_tipo_lista> GetListByIdEntrata(int p_idEntrata, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(tl => tl.id_entrata.HasValue && tl.id_entrata.Value == p_idEntrata);
        }

        /// <summary>
        /// Controllo se è presente un altro elemento con lo stesso codice
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <param name="p_codice">Codice</param>
        /// <returns></returns>
        public static bool CheckCodiceDuplicato(dbEnte p_dbContext, string p_codice)
        {
            return GetList(p_dbContext).Any(d => d.cod_lista.Equals(p_codice));
        }

        /// <summary>
        /// Restituisce il tipo Rateizzazione
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static tab_tipo_lista GetTipoRateizzazione(dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(t => t.cod_lista == tab_tipo_lista.TIPOLISTA_RATEIZZAZIONE).SingleOrDefault();
        }
    }
}
