using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaEnteLinq
    {
        public static IQueryable<anagrafica_ente> WhereByIdEnteOrGenerico(this IQueryable<anagrafica_ente> p_query, int p_idEnte)
        {
            return p_query.Where(e => e.id_ente == p_idEnte || 
                                      e.id_ente == anagrafica_ente.ID_ENTE_GENERICO);
        }

        public static IQueryable<anagrafica_ente> WhereByCodiceEnte(this IQueryable<anagrafica_ente> p_query, string p_codiceEnte)
        {
            return p_query.Where(e => e.cod_ente == p_codiceEnte);
        }

        public static IQueryable<anagrafica_ente> WhereByCF(this IQueryable<anagrafica_ente> p_query, string p_cf)
        {
            return p_query.Where(e => e.cod_fiscale == p_cf);
        }

        public static IQueryable<anagrafica_ente> WhereByPIVA(this IQueryable<anagrafica_ente> p_query, string p_pIva)
        {
            return p_query.Where(e => e.p_iva == p_pIva);
        }
        public static IQueryable<anagrafica_ente> WhereByCodFiscOrPIVA(this IQueryable<anagrafica_ente> p_query, string p_dominio)
        {
            return p_query.Where(w => ((w.cod_fiscale != null) && w.cod_fiscale == p_dominio) || ((w.p_iva != null) && w.p_iva == p_dominio));
        }
        public static IQueryable<anagrafica_ente> WhereByEntiEsistenti(this IQueryable<anagrafica_ente> p_query)
        {
            return p_query.Where(e => e.nome_db != null && e.user_name_db != null && e.password_db != null && e.indirizzo_ip_db != null && !e.descrizione_ente.Contains("SOSTA") && !e.descrizione_ente.Contains("REFLUE"));
        }

        public static IQueryable<anagrafica_ente> WhereByFlagSosta(this IQueryable<anagrafica_ente> p_query, string p_flag)
        {
            return p_query.Where(e => e.flag_sosta == p_flag);
        }

        public static IQueryable<anagrafica_ente> OrderByDefault(this IQueryable<anagrafica_ente> p_query)
        {
            return p_query.OrderBy(e => e.descrizione_ente);
        }
    }
}
