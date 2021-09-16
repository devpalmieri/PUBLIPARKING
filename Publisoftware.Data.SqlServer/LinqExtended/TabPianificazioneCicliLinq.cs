using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabPianificazioneCicliLinq
    {
        public static IQueryable<tab_pianificazione_cicli> WhereByIdPianificazioneCiclo(this IQueryable<tab_pianificazione_cicli> p_query, int p_idPianificazioneCiclo)
        {
            return p_query.Where(w => w.id_pianificazione_ciclo == p_idPianificazioneCiclo);
        }

        public static IQueryable<tab_pianificazione_cicli> WhereByIdTipoCiclo(this IQueryable<tab_pianificazione_cicli> p_query, int p_idTipoCiclo)
        {
            return p_query.Where(w => w.anagrafica_tipo_ciclo.id_tipo_ciclo == p_idTipoCiclo);
        }

        public static IQueryable<tab_pianificazione_cicli> WhereByIdEnte(this IQueryable<tab_pianificazione_cicli> p_query, int p_idEnte)
        {
            return p_query.Where(w => w.anagrafica_ente.id_ente == p_idEnte);
        }

        public static IQueryable<tab_pianificazione_cicli> WhereByCodStato(this IQueryable<tab_pianificazione_cicli> p_query, string p_codStato)
        {
            return p_query.Where(w => w.cod_stato == p_codStato);
        }

        public static IQueryable<tab_pianificazione_cicli> OrderByPriorita(this IQueryable<tab_pianificazione_cicli> p_query)
        {
            return p_query.OrderBy(o => o.priorita);
        }

        public static IQueryable<tab_pianificazione_cicli> OrderByDefault(this IQueryable<tab_pianificazione_cicli> p_query)
        {
            return p_query.OrderBy(o => o.id_pianificazione_ciclo);
        }
    }
}
