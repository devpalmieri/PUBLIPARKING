using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabRilevatoreLinq
    {
        /// <summary>
        /// Filtro ID Rilevatore
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idRilevatore">p_idRilevatore Ricercato</param>
        /// <returns></returns>
        public static IQueryable<tab_rilevatore> WhereByIdRilevatore(this IQueryable<tab_rilevatore> p_query, int p_idRilevatore)
        {
            return p_query.Where(w => w.id_rilevatore == p_idRilevatore);
        }

        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_rilevatore> OrderByDefault(this IQueryable<tab_rilevatore> p_query)
        {
            return p_query.OrderBy(o => o.cognome).ThenBy(o => o.nome);
        }

        /// <summary>
        /// Transforma nella versione light
        /// </summary>
        /// <param name="iniziale"></param>
        /// <returns></returns>
        public static IQueryable<tab_rilevatore_light> ToLight(this IQueryable<tab_rilevatore> iniziale)
        {
            return iniziale.Select(l => new tab_rilevatore_light { cod_fiscale = l.cod_fiscale, cognome = l.cognome, cod_stato = l.cod_stato, email = l.email, flag_interna_esterna = l.flag_interna_esterna,
                                            flag_PF_PG = l.flag_PF_PG, id_rilevatore = l.id_rilevatore, indirizzo = l.indirizzo, nome = l.nome, nome_utente = l.nome_utente,
                                            password = l.password, p_iva = l.p_iva, rag_sociale = l.rag_sociale, tel_casa = l.tel_casa, tel_cellulare = l.tel_cellulare});
        }
    }
}
