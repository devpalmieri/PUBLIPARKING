using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabAvvisiAnomaliListeCaricoLinq
    {
        public static IQueryable<tab_avvisi_anomali_liste_carico> WhereById(this IQueryable<tab_avvisi_anomali_liste_carico> p_query, int p_idTabAvvAnomali)
        {
            return p_query.Where(d => d.id_tab_avvisi_anomali_liste_carico == p_idTabAvvAnomali);
        }

        public static IQueryable<tab_avvisi_anomali_liste_carico> WhereAvvisiAnomali(this IQueryable<tab_avvisi_anomali_liste_carico> p_query)
        {
            return p_query.Where(w => w.tipo_record_controllato.Trim() == "F1" || w.tipo_record_controllato.Trim() == "N2"
                                   || w.tipo_record_controllato.Trim() == "N3" || w.tipo_record_controllato.Trim() == "N4"
            );
        } 
    }

}
