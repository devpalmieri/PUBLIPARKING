using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinFileLinq
    {
        public static IQueryable<join_file> WhereByIdRiferimento(this IQueryable<join_file> p_query, int p_idRiferimento)
        {
            return p_query.Where(d => d.id_riferimento == p_idRiferimento);
        }

        public static IQueryable<join_file> WhereByIdRiferimento(this IQueryable<join_file> p_query, decimal p_idRiferimento)
        {
            return p_query.Where(d => d.id_riferimento == p_idRiferimento);
        }

        public static IQueryable<join_file> WhereByTipologia(this IQueryable<join_file> p_query, List<string> p_tipoList)
        {
            return p_query.Where(d => p_tipoList.Contains(d.cod_tipo_record));
        }

        public static IQueryable<join_file> WhereByNomeFileValid(this IQueryable<join_file> p_query)
        {
            return p_query.Where(d => d.nome_file != null && d.nome_file != string.Empty);
        }

        public static IQueryable<join_file> OrderByDataCreazioneDesc(this IQueryable<join_file> p_query)
        {
            return p_query.OrderByDescending(d => d.data_creazione_file);
        }

        public static IQueryable<join_file_light> ToLight(this IQueryable<join_file> iniziale)
        {
            return iniziale.ToList().Select(d => new join_file_light
            {
                id_join_file = d.id_join_file,
                nome_file = d.nome_file,
                id_riferimento = d.id_riferimento
            }).AsQueryable();
        }
    }
}
