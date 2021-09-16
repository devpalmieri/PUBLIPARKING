using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaTipoVeicoloBD : EntityBD<anagrafica_tipo_veicolo>
    {
        public AnagraficaTipoVeicoloBD()
        {

        }

        /// <summary>
        /// Controllo se è presente un altro elemento con lo stesso codice
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <param name="p_codice">Codice</param>
        /// <returns></returns>
        public static bool CheckCodiceDuplicato(dbEnte p_dbContext, string p_codice)
        {
            return GetList(p_dbContext).Any(d => d.cod.Equals(p_codice));
        }

        /// <summary>
        /// Controllo se è presente un altro elemento con la stessa descrizione
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <param name="p_descrizione">Descrizione</param>
        /// <returns></returns>
        public static bool CheckDescrizioneDuplicato(dbEnte p_dbContext, string p_descrizione)
        {
            return GetList(p_dbContext).Any(d => d.descrizione.Equals(p_descrizione));
        }

        public static anagrafica_tipo_veicolo getTipoVeicoloByDescrizione(string p_tipoVeicolo, dbEnte p_context)
        {
            return GetList(p_context).Where(v => v.cod.Equals(p_tipoVeicolo)).FirstOrDefault();
        }
    }
}
