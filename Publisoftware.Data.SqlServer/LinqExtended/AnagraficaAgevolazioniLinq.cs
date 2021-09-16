using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaAgevolazioniLinq
    {
        public static IQueryable<anagrafica_agevolazione> WhereByMacrocategoriaOrNull(this IQueryable<anagrafica_agevolazione> p_query, string p_macrocategoria)
        {
            return p_query.Where(d => d.macrocategoria.Trim() == p_macrocategoria ||
                                      string.IsNullOrEmpty(d.macrocategoria));
        }

        public static IQueryable<anagrafica_agevolazione> WhereByIdEntrata(this IQueryable<anagrafica_agevolazione> p_query, int p_idEntrata)
        {
            return p_query.Where(ac => ac.id_entrata == p_idEntrata);
        }

        public static IQueryable<anagrafica_agevolazione> WhereByIdEnte(this IQueryable<anagrafica_agevolazione> p_query, int p_idEnte)
        {
            return p_query.Where(ac => ac.id_ente == p_idEnte);
        }

        public static IQueryable<anagrafica_agevolazione> WhereByIdEnteOrNull(this IQueryable<anagrafica_agevolazione> p_query, int p_idEnte)
        {
            return p_query.Where(ac => ac.id_ente == null || ac.id_ente == p_idEnte);
        }

        public static IQueryable<anagrafica_agevolazione> OrderByDefault(this IQueryable<anagrafica_agevolazione> p_query)
        {
            return p_query.OrderBy(e => e.des_agevolazione);
        }

        public static IQueryable<anagrafica_agevolazione_light> ToLight(this IQueryable<anagrafica_agevolazione> iniziale)
        {
            return iniziale.Select(d => new anagrafica_agevolazione_light
            {
                id_anagrafica_agevolazione = d.id_anagrafica_agevolazione,
                cod_agevolazione = d.cod_agevolazione,
                des_agevolazione = d.des_agevolazione,
                tipo_base_calcolo = d.anagrafica_tipo_agevolazione != null ? d.anagrafica_tipo_agevolazione.des_tipo_agevolazione : string.Empty
            }).AsQueryable();
        }
    }
}
