using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.LinqExtended;
using System.Data.Entity;

namespace Publisoftware.Data.BD
{
    public class JoinTipoAvvpagServiziBD : EntityBD<join_tipo_avv_pag_servizi> 
    {
        public JoinTipoAvvpagServiziBD()
        {

        }

        /// <summary>
        /// Ottiene la lista filtrata per parola chiave
        /// </summary>
        /// <param name="p_text"></param>
        /// <param name="p_dbContext"></param>
        /// <param name="p_includeEntities"></param>
        /// <returns></returns>
        public static IQueryable<join_tipo_avv_pag_servizi> GetListContains(String p_text, dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            if (p_text == null) p_text = "";

            return GetList(p_dbContext, p_includeEntities).Where(a => p_text == ""
                || a.id_join_servizi_tipo_avv_pag.ToString().Contains(p_text)
                || a.anagrafica_servizi.descr_servizio.ToUpper().Contains(p_text.ToUpper())
                );
        }

        /// <summary>
        /// Filtro per l'id del tipo avviso
        /// </summary>
        /// <param name="p_idTipoAvvPag"></param>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static IQueryable<join_tipo_avv_pag_servizi> GetListByIdTipoAvvPag(int p_idTipoAvvPag, dbEnte dbContext)
        {
            return GetList(dbContext).Where(j => j.id_tipo_avv_pag == p_idTipoAvvPag);
        }

        /// <summary>
        /// Filtro per l'id del tipo avviso e l'id servizio
        /// </summary>
        /// <param name="p_idServizio"></param>
        /// <param name="p_idTipoAvvPag"></param>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static join_tipo_avv_pag_servizi GetByJoinValues(int p_idServizio, int p_idTipoAvvPag, dbEnte dbContext)
        {
            return GetList(dbContext).SingleOrDefault(j => j.id_servizio == p_idServizio && j.id_tipo_avv_pag == p_idTipoAvvPag);
        }
    }
}
