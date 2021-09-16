using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaTipoRecordBD : EntityBD<anagrafica_tipo_record>
    {
        public AnagraficaTipoRecordBD()
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
            return GetList(p_dbContext).Any(d => d.cod_tipo_record.Equals(p_codice));
        }

        /// <summary>
        /// Controllo se è presente un altro elemento con la stessa descrizione
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <param name="p_descrizione">Descrizione</param>
        /// <returns></returns>
        public static bool CheckDescrizioneDuplicato(dbEnte p_dbContext, string p_descrizione)
        {
            return GetList(p_dbContext).Any(d => d.descrizione_tipo_record.Equals(p_descrizione));
        }
    }
}
