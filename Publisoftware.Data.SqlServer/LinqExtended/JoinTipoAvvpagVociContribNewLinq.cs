using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinTipoAvvpagVociContribNewLinq
    {
        /// <summary>
        /// Filtro per ID Ente
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idEnte">ID Ente</param>
        /// <returns></returns>
        public static IQueryable<join_tipo_avvpag_voci_contrib_new> WhereByIdEnte(this IQueryable<join_tipo_avvpag_voci_contrib_new> p_query, int p_idEnte)
        {
            var v_retval = p_query.Where(d => d.id_ente == p_idEnte);

            if(v_retval.Count() == 0)
            {
                v_retval = p_query.Where(d => d.id_ente == anagrafica_ente.ID_ENTE_GENERICO);
            }

            return v_retval;
        }

        /// <summary>
        /// Filtro per ID TIPO AVV PAG
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idTipoAvvPag">ID Tipo Avv PAg</param>
        /// <returns></returns>
        public static IQueryable<join_tipo_avvpag_voci_contrib_new> WhereByIdTipoAvvPag(this IQueryable<join_tipo_avvpag_voci_contrib_new> p_query, int p_idTipoAvvPag)
        {
            return p_query.Where(d => d.id_tipo_avv_pag == p_idTipoAvvPag);
        }

        public static IQueryable<join_tipo_avvpag_voci_contrib_new> WhereByIdCodiceTributoMinisteriale(this IQueryable<join_tipo_avvpag_voci_contrib_new> p_query, string p_codice)
        {
            return p_query.Where(d => d.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(p_codice));
        }

        public static IQueryable<join_tipo_avvpag_voci_contrib_new> WhereByFlagAccertamento(this IQueryable<join_tipo_avvpag_voci_contrib_new> p_query, string p_flag)
        {
            return p_query.Where(d => d.tab_tipo_voce_contribuzione.flag_accertamento_contabile == p_flag);
        }
    }
}
