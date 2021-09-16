using Publisoftware.Data.LinqExtended;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class JoinOggettoDatiMetriciBD : EntityBD<join_oggetto_dati_metrici>
    {
        public JoinOggettoDatiMetriciBD()
        {

        }

        // TODO: forse si deve reintrodurre id_contribuente in join_oggetto_dati_metrici
        public static async Task<decimal> GetTotSuperficieCatastoMetrico(dbEnte ctx, tab_oggetti_contribuzione oggCont, decimal id_contribuente, decimal onNullReturn = 0M)
        {
            IQueryable<join_oggetto_dati_metrici> totAuperficieCatastoMetricoQuery = JoinOggettoDatiMetriciBD.GetList(ctx)
                .WhereByIdOggettoM(oggCont.id_oggetto)
                // .WhereByIdContribuente(id_contribuente)
                .WhereCodStatoLikeAttivo()
                .WhereByMqOccupazioneDichiaratiNotNull();
#if DEBUG && false
            var lstDbg = await totAuperficieCatastoMetricoQuery.ToListAsync();
#endif
            decimal tot_superficie_catasto_metrico = await totAuperficieCatastoMetricoQuery.SumAsync(x => x.mq_occupazione_dichiarati) ?? onNullReturn;
            return tot_superficie_catasto_metrico;
        }

        //public static async Task<IList<tab_dati_metrici_catastali>> FindDatiMetriciImuBy(
        //    string foglio,
        //    string numero,
        //    string particella,
        //    string subalterno)
        //{ 
        //}

        public static async Task<IList<join_oggetto_dati_metrici>> GetImuByIdOggetto(dbEnte ctx, decimal id_oggetto)
        {
            return await JoinOggettoDatiMetriciBD.GetList(ctx)
                    .Where(x =>
                        x.cod_stato.StartsWith(CodStato.ATT) &&
                        x.id_dati_metrici_catastali != null &&
                        x.tab_dati_metrici_catastali.cod_stato == tab_dati_metrici_catastali.ATTIVO_IMU &&
                        x.id_oggetto == id_oggetto)
                    .Include(x=>x.tab_dati_metrici_catastali)
                    .ToListAsync(); ;
        }

        public static async Task<IList<join_oggetto_dati_metrici>> GetTarsuByIdOggetto(dbEnte ctx, decimal id_oggetto)
        {
            return await JoinOggettoDatiMetriciBD.GetList(ctx)
                    .Where(x =>
                        x.cod_stato.StartsWith(CodStato.ATT) &&
                        x.id_dati_metrici_catastali != null &&
                        x.tab_dati_metrici_catastali.cod_stato == tab_dati_metrici_catastali.ATTIVO_TARSU &&
                        x.id_oggetto == id_oggetto)
                    .Include(x => x.tab_dati_metrici_catastali)
                    .ToListAsync(); ;
        }

        public static async Task<join_oggetto_dati_metrici> GetFirstByOggettoAndContribuenteWithOggettiAndCatasto(dbEnte ctx, decimal id_oggetto, decimal id_anag_contribuente)
        {
            join_oggetto_dati_metrici j = await JoinOggettoDatiMetriciBD.GetList(ctx)
                .Where(x =>
                        x.id_oggetto == id_oggetto &&
                        x.id_oggetto != null &&
                        x.id_dati_metrici_catastali != null &&
                        x.id_contribuente == id_anag_contribuente)
                .Include(x => x.tab_oggetti)
                .Include(x => x.tab_dati_metrici_catastali)
                .OrderBy(x => x.id_join_oggetto_catasto)
                .FirstOrDefaultAsync();
            return j;

        }
    }
}
