using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinListaVociContribuzioneLinq
    {
        public static IQueryable<join_lista_voci_contribuzione> WhereByIdLista(this IQueryable<join_lista_voci_contribuzione> p_query, int p_idLista)
        {
            return p_query.Where(e => e.id_lista == p_idLista);
        }

        public static IQueryable<join_lista_voci_contribuzione> WhereByIdVoceContribuzione(this IQueryable<join_lista_voci_contribuzione> p_query, int p_idVoceContribuzione)
        {
            return p_query.Where(e => e.id_tipo_voce_contribuzione == p_idVoceContribuzione);
        }

        public static IQueryable<join_lista_voci_contribuzione_light> ToLight(this IQueryable<join_lista_voci_contribuzione> iniziale)
        {
            return iniziale.ToList().Select(d => new join_lista_voci_contribuzione_light
            {
                id_join_lista_voci_contribuzione = d.id_join_lista_voci_contribuzione,
                TipoVoceContribuzione = d.tab_tipo_voce_contribuzione.CodiceDescrizione,
                numero_accertamento_contabile = d.numero_accertamento_contabile,
                data_accertamento_contabile_String = d.data_accertamento_contabile.HasValue ? d.data_accertamento_contabile.Value.ToShortDateString() : string.Empty
            }).AsQueryable();
        }
    }
}
