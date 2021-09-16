using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinLstEmissioneLstSpedNotLinq
    {
        public static IQueryable<join_lst_emissione_lst_sped_not> WhereByIdLista(this IQueryable<join_lst_emissione_lst_sped_not> p_query, int p_idLista)
        {
            return p_query.Where(d => d.id_lista_emissione == p_idLista);
        }
    }
}
