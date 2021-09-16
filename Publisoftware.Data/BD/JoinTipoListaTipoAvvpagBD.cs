using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class JoinTipoListaTipoAvvpagBD : EntityBD<join_tipo_lista_tipo_avv_pag>
    {
        public JoinTipoListaTipoAvvpagBD()
        {

        }

        /// <summary>
        /// Filtro per l'id del tipo lista
        /// </summary>
        /// <param name="p_idTipoLista"></param>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static IQueryable<join_tipo_lista_tipo_avv_pag> GetListByIdTipoLista(int p_idTipoLista, dbEnte dbContext)
        {
            return GetList(dbContext).Where(j => j.id_tipo_lista == p_idTipoLista);
        }

        /// <summary>
        /// Filtro per l'id del tipo avviso
        /// </summary>
        /// <param name="p_idTipoAvvPag"></param>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static IQueryable<join_tipo_lista_tipo_avv_pag> GetListByIdTipoAvvPag(int p_idTipoAvvPag, dbEnte dbContext)
        {
            return GetList(dbContext).Where(j => j.id_tipo_avv_pag == p_idTipoAvvPag);
        }

        /// <summary>
        /// Filtro per l'id del tipo lista e l'id del tipo avviso
        /// </summary>
        /// <param name="p_idTipoLista"></param>
        /// <param name="p_idTipoAvvPag"></param>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static join_tipo_lista_tipo_avv_pag GetByJoinValues(int p_idTipoLista, int p_idTipoAvvPag, dbEnte dbContext)
        {
            return GetList(dbContext).SingleOrDefault(j => j.id_tipo_lista == p_idTipoLista && j.id_tipo_avv_pag == p_idTipoAvvPag);
        }
    }
}
