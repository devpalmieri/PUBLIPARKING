using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabDocumentiLinq
    {
        public static IQueryable<tab_documenti> WhereByIdUtente(this IQueryable<tab_documenti> p_query, int p_idUtente)
        {
            return p_query.Where(c => c.id_utente == p_idUtente);
        }

        public static IQueryable<tab_documenti> WhereByMacrocategorie(this IQueryable<tab_documenti> p_query, List<string> macrocategorie)
        {
            return p_query.Where(d => macrocategorie.Contains(d.anagrafica_documenti.macrocategoria));
        }

        public static IQueryable<tab_documenti> WhereBySigle(this IQueryable<tab_documenti> p_query, List<string> sigle)
        {
            return p_query.Where(d => sigle.Contains(d.anagrafica_documenti.sigla_doc));
        }

        public static IList<tab_documenti_light> ToLight(this IQueryable<tab_documenti> iniziale)
        {
            return iniziale.ToList().Select(d => new tab_documenti_light
            {
                id_tab_documenti = d.id_tab_documenti,
                tipo_documento = d.anagrafica_documenti.descrizione_doc,
                num_documento = d.num_documento,
                descr_ente_rilascio_documento = d.descr_ente_rilascio_documento,
                data_rilascio_documento_String = d.data_rilascio_documento_String,
                data_validita_documento_da_String = d.data_validita_documento_da_String,
                data_validita_documento_a_String = d.data_validita_documento_a_String,
                data_rilascio_documento = d.data_rilascio_documento,
                data_validita_documento_da = d.data_validita_documento_da,
                data_validita_documento_a = d.data_validita_documento_a,
                color = ""
            }).ToList();
        }
    }
}
