using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabTipoEnteBD : EntityBD<tab_tipo_ente>
    {
        public TabTipoEnteBD()
        {

        }

        /// <summary>
        /// Controllo se è presente un altro elemento con la stessa descrizione
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <param name="p_descrizione">Descrizione</param>
        /// <returns></returns>
        public static bool CheckDescrizioneDuplicato(dbEnte p_dbContext, string p_descrizione)
        {
            return GetList(p_dbContext).Any(d => d.desc_tipo_ente.Equals(p_descrizione));
        }

        /// <summary>
        /// Controllo se è presente un altro elemento con lo stesso codice
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <param name="p_codice">Codice</param>
        /// <returns></returns>
        public static bool CheckCodiceDuplicato(dbEnte p_dbContext, string p_codice)
        {
            return GetList(p_dbContext).Any(d => d.cod_tipo_ente.Equals(p_codice));
        }

        /// <summary>
        /// Solo comune, provincia, regione, consorzio di comuni
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IList<tab_tipo_ente> GetTipiEnteLocale(dbEnte p_dbContext)
        {
            return GetList(p_dbContext)
                .Where(x =>
                    x.id_tipo_ente == tab_tipo_ente.COMUNE_ENTE_ID //1
                    || x.id_tipo_ente == tab_tipo_ente.PROVINCIA_ID //3
                    || x.id_tipo_ente == tab_tipo_ente.REGIONE_ID //4
                    || x.id_tipo_ente == tab_tipo_ente.CONSORZIO_ID) //7 - Consorziio di comuni
                    .ToList();
        }
    }
}
