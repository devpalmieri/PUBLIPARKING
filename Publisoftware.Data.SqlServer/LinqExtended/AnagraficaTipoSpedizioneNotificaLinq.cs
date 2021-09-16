using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaTipoSpedizioneNotificaLinq
    {
        public static IQueryable<anagrafica_tipo_spedizione_notifica> WhereBySigla(this IQueryable<anagrafica_tipo_spedizione_notifica> p_query, string p_sigla)
        {
            return p_query.Where(d => d.sigla_tipo_spedizione_notifica.Equals(p_sigla));
        }

        public static IQueryable<anagrafica_tipo_spedizione_notifica> WhereByIdStampatore(this IQueryable<anagrafica_tipo_spedizione_notifica> p_query, int p_idStampatore)
        {
            return p_query.Where(d => d.id_stampatore == p_idStampatore);
        }

        public static IQueryable<anagrafica_tipo_spedizione_notifica> WhereByFlagSpedNot(this IQueryable<anagrafica_tipo_spedizione_notifica> p_query, string p_flag)
        {
            return p_query.Where(d => d.flag_sped_not.Contains(p_flag));
        }
    }
}
