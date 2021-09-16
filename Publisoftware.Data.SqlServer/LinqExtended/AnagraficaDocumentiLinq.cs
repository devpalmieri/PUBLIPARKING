using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaDocumentiLinq
    {
        public static IQueryable<anagrafica_documenti> WhereByIdTipoDocumento(this IQueryable<anagrafica_documenti> p_query, int p_idTipoDocumento)
        {
            return p_query.Where(d => d.id_tipo_documento == p_idTipoDocumento);
        }

        public static IQueryable<anagrafica_documenti> WhereBySigla(this IQueryable<anagrafica_documenti> p_query, string p_sigla)
        {
            return p_query.Where(d => d.sigla_doc == p_sigla);
        }
        
        public static IQueryable<anagrafica_documenti> WhereByMacrocategoria(this IQueryable<anagrafica_documenti> p_query, string p_macrocategoria)
        {
            return p_query.Where(d => d.macrocategoria == p_macrocategoria);
        }

        public static IQueryable<anagrafica_documenti> WhereByMacrocategorie(this IQueryable<anagrafica_documenti> p_query, List<string> macrocategorie)
        {
            return p_query.Where(d => macrocategorie.Contains(d.macrocategoria));
        }

        public static IQueryable<anagrafica_documenti> WhereBySigle(this IQueryable<anagrafica_documenti> p_query, List<string> sigle)
        {
            return p_query.Where(d => sigle.Contains(d.sigla_doc));
        }

        public static IQueryable<anagrafica_documenti> OrderByDefault(this IQueryable<anagrafica_documenti> p_query)
        {
            return p_query.OrderBy(o => o.descrizione_doc);
        }

        public static IQueryable<anagrafica_documenti_light> ToLight(this IQueryable<anagrafica_documenti> iniziale)
        {
            return iniziale.ToList().Select(d => new anagrafica_documenti_light
            {
                id_anagrafica_doc = d.id_anagrafica_doc,
                descrizione_doc = d.descrizione_doc
            }).AsQueryable();
        }
    }
}
