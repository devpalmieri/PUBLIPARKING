using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabTerzoLinq
    {
        public static tab_terzo WhereByCodiceFiscalePIva(this IQueryable<tab_terzo> p_query, string p_CfPiva)
        {
            if (p_CfPiva.Length == 16)
            {
                return p_query.Where(d => d.id_stato_terzo == anagrafica_stato_contribuente.ATT_ATT_ID && d.cod_fiscale.Equals(p_CfPiva.ToUpper())).SingleOrDefault();
            }
            else
            {
                return p_query.Where(d => d.id_stato_terzo == anagrafica_stato_contribuente.ATT_ATT_ID && d.p_iva.Equals(p_CfPiva)).SingleOrDefault();
            }
        }

        public static IQueryable<tab_terzo> OrderByDefaultAllGroupTipoPersona(this IQueryable<tab_terzo> p_query)
        {
            return p_query.OrderBy(o => o.id_tipo_terzo).ThenBy(o => o.cognome).ThenBy(o => o.nome).ThenBy(o => o.rag_sociale);
        }


        public static IQueryable<tab_terzo> WhereToponimoDaNormalizzare(this IQueryable<tab_terzo> p_query)
        {
            return p_query.Where(c => c.id_toponimo_normalizzato.HasValue);
        }

        public static List<tab_terzo_light> ToLight(this List<tab_terzo> iniziale)
        {
            return iniziale.Select(d => new tab_terzo_light
            {
                id_terzo = d.id_terzo,
                cognome = d.cognome,
                nome = d.nome,
                cod_fiscale = d.cod_fiscale,
                rag_sociale = d.rag_sociale,
                p_iva = d.p_iva,
                indirizzoDisplay = d.indirizzoDisplay,
                civico = d.civico,
                cittaDisplay = d.cittaDisplay
            }).ToList();
        }
    }
}
