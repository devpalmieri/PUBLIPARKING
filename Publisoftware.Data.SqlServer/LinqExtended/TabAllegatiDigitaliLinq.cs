using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabAllegatiDigitaliLinq
    {
        public static IQueryable<tab_allegati_digitali> WhereByIdDocumento(this IQueryable<tab_allegati_digitali> p_query, int p_idTabDocumenti)
        {
            return p_query.Where(d => d.id_tab_documenti == p_idTabDocumenti);
        }

        public static IQueryable<tab_allegati_digitali> WhereByFileName(this IQueryable<tab_allegati_digitali> p_query, string p_nomeFile)
        {
            return p_query.Where(d => d.nome_file == p_nomeFile);
        }

        public static IList<tab_allegati_digitali_light> ToLight(this IQueryable<tab_allegati_digitali> iniziale)
        {
            return iniziale.ToList().Select(d => new tab_allegati_digitali_light
            {
                contatoreFile = string.Empty,
                id_tab_allegati_digitali = d.id_tab_allegati_digitali,
                nome_file = d.nome_file,
                formato_file = d.formato_file,
                path_file = d.path_file,
                data_creazione_String = d.data_creazione_String,
                data_creazione = d.data_creazione                
            }).ToList();
        }
    }
}
