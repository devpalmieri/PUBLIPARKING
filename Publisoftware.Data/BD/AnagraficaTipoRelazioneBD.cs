﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaTipoRelazioneBD : EntityBD<anagrafica_tipo_relazione>
    {
        public AnagraficaTipoRelazioneBD()
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
                return GetList(p_dbContext).Any(d => d.cod_tipo_relazione.Equals(p_codice));
        }
    }
}