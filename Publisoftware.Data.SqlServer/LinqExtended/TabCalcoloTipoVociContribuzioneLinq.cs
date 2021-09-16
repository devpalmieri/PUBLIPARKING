using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Publisoftware.Data.LinqExtended
{
    public static class TabCalcoloTipoVociContribuzioneLinq
    {
        public static IQueryable<tab_calcolo_tipo_voci_contribuzione> WhereByIdTipoVoceContribuzione(this IQueryable<tab_calcolo_tipo_voci_contribuzione> p_query, int p_IdTipoVoceContribuzione)
        {
            return p_query.Where(d => d.id_tipo_voce_contribuzione == p_IdTipoVoceContribuzione);
        }
    }
}
