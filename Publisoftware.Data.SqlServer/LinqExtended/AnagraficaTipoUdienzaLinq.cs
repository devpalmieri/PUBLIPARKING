using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaTipoUdienzaLinq
    {
        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_tipo_udienza> OrderByDefault(this IQueryable<anagrafica_tipo_udienza> p_query)
        {
            return p_query.OrderBy(d => d.tab_tipo_doc_entrate.descr_doc).ThenBy(d => d.descrizione);
        }

        /// <summary>
        /// Filtro per ID Tab Doc Entrata 
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idTabDocEntrata">ID Tab Doc Entrata</param>
        /// <returns></returns>
        public static IQueryable<anagrafica_tipo_udienza> WhereByIdTabDocEntrate(this IQueryable<anagrafica_tipo_udienza> p_query, int p_idTabDocEntrata)
        {
            return p_query.Where(w => w.id_tipo_doc_entrata == p_idTabDocEntrata);
        }
    }
}
