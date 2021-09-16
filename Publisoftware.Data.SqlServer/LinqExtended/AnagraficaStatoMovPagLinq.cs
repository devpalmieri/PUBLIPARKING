﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaStatoMovPagLinq
    {
        public static anagrafica_stato_mov_pag WhereByCodice(this IQueryable<anagrafica_stato_mov_pag> p_query, string p_codStato)
        {
            return p_query.SingleOrDefault(d => d.cod_stato_mov_pag.ToUpper().Equals(p_codStato.ToUpper()));
        }
    }
}