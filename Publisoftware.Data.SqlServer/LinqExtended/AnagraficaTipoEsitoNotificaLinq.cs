using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaTipoEsitoNotificaLinq
    {
        public static IQueryable<anagrafica_tipo_esito_notifica> WhereByFLRRMessoNull(this IQueryable<anagrafica_tipo_esito_notifica> p_query)
        {
            return p_query.Where(d => string.IsNullOrEmpty(d.fl_rr_messo) || d.fl_rr_messo == "0" || d.fl_rr_messo == "2");
        }

        public static IQueryable<anagrafica_tipo_esito_notifica> WhereByFlagEsito(this IQueryable<anagrafica_tipo_esito_notifica> p_query, string p_flagEsito)
        {
            return p_query.Where(d => d.fl_not_nok == p_flagEsito);
        }
    }
}
