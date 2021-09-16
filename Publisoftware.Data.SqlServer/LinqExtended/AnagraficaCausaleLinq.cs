using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaCausaleLinq
    {
        public static IQueryable<anagrafica_causale> GroupByDescrizioneCausale(this IQueryable<anagrafica_causale> p_query)
        {
            return p_query.GroupBy(p => new { p.descr_causale }).Select(g => g.FirstOrDefault());
        }

        public static IQueryable<anagrafica_causale> WhereByFlagTrattamento(this IQueryable<anagrafica_causale> p_query, string p_flagTrattamento)
        {
            return p_query.Where(d => d.flag_trattamento.Equals(p_flagTrattamento));
        }

        public static IQueryable<anagrafica_causale> WhereBySiglaCausale(this IQueryable<anagrafica_causale> p_query, string p_siglaCausale)
        {
            return p_query.Where(d => d.sigla_tipo_causale.Trim().Equals(p_siglaCausale));
        }

        public static IQueryable<anagrafica_causale> WhereBySiglaCausaleEntrata(this IQueryable<anagrafica_causale> p_query)
        {
            return p_query.Where(d => d.sigla_tipo_causale.Equals(anagrafica_causale.SIGLA_OGG) || 
                                      d.sigla_tipo_causale.Equals(anagrafica_causale.SIGLA_AGE) || 
                                      d.sigla_tipo_causale.Equals(anagrafica_causale.SIGLA_CNT) ||
                                      d.sigla_tipo_causale.Equals(anagrafica_causale.SIGLA_LET) || 
                                      d.sigla_tipo_causale.Equals(anagrafica_causale.SIGLA_SAN));
        }

        public static IQueryable<anagrafica_causale> WhereByIdEntrataIdServizioOrNull(this IQueryable<anagrafica_causale> p_query, int p_idEntrata, int p_idServizio)
        {
            return p_query.Where(d => (d.id_entrata == p_idEntrata || d.id_entrata == null) &&
                                      (d.id_tipo_servizio == p_idServizio || d.id_tipo_servizio == null));
        }

        public static IQueryable<anagrafica_causale> WhereBySiglaCausaleListNot(this IQueryable<anagrafica_causale> p_query, List<string> p_siglaCausaleList)
        {
            return p_query.Where(d => !p_siglaCausaleList.Contains(d.sigla_tipo_causale));
        }

        public static IQueryable<anagrafica_causale> WhereByIdTipoDoc(this IQueryable<anagrafica_causale> p_query, int p_idTipoDoc)
        {
            return p_query.Where(d => d.id_tipo_doc == p_idTipoDoc);
        }

        public static IQueryable<anagrafica_causale> WhereByIdTipoAvvPag(this IQueryable<anagrafica_causale> p_query, int p_idTipoAvvPag)
        {
            return p_query.Where(d => d.id_tipo_avvpag == p_idTipoAvvPag || d.id_tipo_avvpag == null);
        }

        public static IQueryable<anagrafica_causale> WhereByIdEntrata(this IQueryable<anagrafica_causale> p_query, int p_idEntrata)
        {
            return p_query.Where(d => d.id_entrata == p_idEntrata);
        }

        public static IQueryable<anagrafica_causale> WhereByIdEntrataOrNull(this IQueryable<anagrafica_causale> p_query, int p_idEntrata)
        {
            return p_query.Where(d => d.id_entrata == p_idEntrata || d.id_entrata == null);
        }

        public static IQueryable<anagrafica_causale> WhereByIdTipoServizio(this IQueryable<anagrafica_causale> p_query, int p_idTipoServizio)
        {
            return p_query.Where(d => d.id_tipo_servizio == p_idTipoServizio);
        }

        public static IQueryable<anagrafica_causale> WhereByFlagOperazione(this IQueryable<anagrafica_causale> p_query, string p_flagOperazione)
        {
            return p_query.Where(d => d.flag_tipo_operazione.Equals(p_flagOperazione));
        }

        public static IQueryable<anagrafica_causale> OrderByDefault(this IQueryable<anagrafica_causale> p_query)
        {
            return p_query.OrderBy(e => e.sigla_tipo_causale);
        }

        public static IQueryable<anagrafica_causale_light> ToLight(this IQueryable<anagrafica_causale> iniziale)
        {
            return iniziale.ToList().Select(d => new anagrafica_causale_light
            {
                id_causale = d.id_causale,
                cod_causale = d.cod_causale,
                descr_causale = d.descr_causale,
                flag_trattamento = d.flag_trattamento,
                sigla_tipo_causale = d.sigla_tipo_causale,
                isButtonAbilitato = true,
                messaggio = string.Empty,
                isButtonAbilitatoSanzioni = true,
                messaggioSanzioni = string.Empty
            }).AsQueryable();
        }
    }
}
