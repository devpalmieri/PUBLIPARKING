using Publisoftware.Data.LinqExtended;
using Publisoftware.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public partial class JoinDenunceOggettiBD : EntityBD<join_denunce_oggetti>
    {
        public JoinDenunceOggettiBD()
        {

        }


        public IList<join_denunce_oggetti> GetListNotAnnByIdOggettoContribuzione(dbEnte dbContext, int idOggettoContribuzione)
        {
            return JoinDenunceOggettiBD.GetList(dbContext)
                .WhereByIdOggettoContribuzione(idOggettoContribuzione)
                .WhereByCodStatoNot(anagrafica_stato_denunce.ANN)
                .WhereByDenunciaCodStatoNot(anagrafica_stato_denunce.ANN)
                .OrderByDataPresentazione()
                .ToList();
        }
        public IList<join_denunce_oggetti> GetListNotAnnByIdContribuente(dbEnte dbContext, decimal id_contribuente)
        {
            return JoinDenunceOggettiBD.GetList(dbContext)
                .WhereByIdContribuente(id_contribuente)
                .WhereByCodStatoNot(anagrafica_stato_denunce.ANN)
                .WhereByDenunciaCodStatoNot(anagrafica_stato_denunce.ANN)
                .OrderByDataPresentazione()
                .ToList();
        }

        public static async Task<tab_denunce_contratti> SelectRecordTabDenunceContrattiWithTabTipoDocEntrate(dbEnte dbContext, decimal id_oggetto_contribuzione)
        {
            return await JoinDenunceOggettiBD.GetList(dbContext)
                .WhereByIdOggettoContribuzioneDec(id_oggetto_contribuzione)
                .WhereCodStatoNullOrNotAnn()
                .WhereDenunceContrattiNotNull()
                .Include(x => x.tab_denunce_contratti.tab_tipo_doc_entrate)
                .OrderByDescending(x => x.id_denunce_contratti)
                .Select(x => x.tab_denunce_contratti)
                .FirstOrDefaultAsync();
        }


        public class JoinDenunceOggettiWithTabDenunceContratti
        {
            public decimal id_oggetti_contribuzione { get; set; }
            public join_denunce_oggetti JoinDenunceOggetti { get; set; }
            public tab_denunce_contratti TabDenunceContratti { get; set; }
            public tab_tipo_doc_entrate TabTipoDocEntrate { get; set; }
        }

        public static async Task<IList<JoinDenunceOggettiWithTabDenunceContratti>> SelectRecordJoinDenunceOggettiWithTabDenunceContratti(dbEnte dbContext, IList<decimal> idOggettoContribuzioneList)
        {
            IList<JoinDenunceOggettiWithTabDenunceContratti> ret = await JoinDenunceOggettiBD.GetList(dbContext)
                .WhereByIdOggettoContribuzioneDecListAndNotNull(idOggettoContribuzioneList)
                .WhereCodStatoNullOrNotAnn()
                .WhereDenunceContrattiNotNull()
                .OrderByDescending(x => x.id_denunce_contratti)
                //.Include(x => x.tab_denunce_contratti)
                //.Include(x => x.tab_denunce_contratti.tab_tipo_doc_entrate)
                .Select(x => new JoinDenunceOggettiWithTabDenunceContratti
                {
                    id_oggetti_contribuzione = x.id_oggetti_contribuzione.Value,
                    JoinDenunceOggetti = x,
                    TabDenunceContratti = x.tab_denunce_contratti,
                    TabTipoDocEntrate = x.tab_denunce_contratti.tab_tipo_doc_entrate
                }
                )
                .ToListAsync();

            return ret;
        }

        public static async Task<IDictionary<decimal, JoinDenunceOggettiBD.JoinDenunceOggettiWithTabDenunceContratti>>
            SelectJoinDenunceOggettiWithTabDenunceContrattiDic(
                dbEnte dbContext,
                IList<decimal> idOggettoDiContribuzioneList)
        {
            // NOTA: Non usare "ToDictionaryAsync", meglio costruire il IDictionary a mano,
            //       che in caso di chiavi ripetute vererbbe lanciata eccezione
            //       (oppure usi ToLookup...)
            var joinDenunceOggettiWithTabDenunceContrattiList = await JoinDenunceOggettiBD.SelectRecordJoinDenunceOggettiWithTabDenunceContratti(
                dbContext, idOggettoDiContribuzioneList);
            IDictionary<decimal, JoinDenunceOggettiBD.JoinDenunceOggettiWithTabDenunceContratti> joinDenunceOggettiWithTabDenunceContrattiDic =
                new Dictionary<decimal, JoinDenunceOggettiBD.JoinDenunceOggettiWithTabDenunceContratti>();
            foreach (var item in joinDenunceOggettiWithTabDenunceContrattiList)
            {
                joinDenunceOggettiWithTabDenunceContrattiDic[item.id_oggetti_contribuzione] = item;
            }
            return joinDenunceOggettiWithTabDenunceContrattiDic;
        }
    } // class
}
