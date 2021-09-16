using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabDetRiscossoPeriodoListaBD : EntityBD<tab_det_riscosso_periodo_lista>
    {
        public TabDetRiscossoPeriodoListaBD()
        {

        }

        public static IQueryable<tab_det_riscosso_periodo_lista> searchDettConsuntivoListe(int p_id_ente, dbEnte p_dbContext, int p_id_entrata = -1, int p_id_lista_rif = -1, DateTime? p_data_periodo_a = null, DateTime? p_data_periodo_da = null, int p_id_ente_gestito = -1)
        {
            IQueryable<tab_det_riscosso_periodo_lista> res = GetList(p_dbContext).Where(c => c.id_ente == p_id_ente);

            if (p_id_ente_gestito > 0)
                res = res.Where(c => c.id_ente_gestito == p_id_ente_gestito);

            if (p_data_periodo_a.HasValue)
                res = res.Where(c => c.periodo_riscosso_a.Year == p_data_periodo_a.Value.Year && c.periodo_riscosso_a.Month == p_data_periodo_a.Value.Month && c.periodo_riscosso_a.Day == p_data_periodo_a.Value.Day);

            if (p_data_periodo_da.HasValue)
                res = res.Where(c => c.periodo_riscosso_a.Year == p_data_periodo_da.Value.Year && c.periodo_riscosso_a.Month == p_data_periodo_da.Value.Month && c.periodo_riscosso_a.Day == p_data_periodo_da.Value.Day);

            if (p_id_entrata > 0)
                res = res.Where(c => c.id_entrata == p_id_entrata);

            if (p_id_lista_rif > 0)
                res = res.Where(c => c.id_lista_riferimento == p_id_lista_rif);

            return res;
        }

        public static IEnumerable<tab_det_riscosso_periodo_lista> GetDettaglioByRiversamento(IQueryable<tab_det_riscosso_periodo_lista> p_consuntivi_dett, int p_id_ente, bool p_is_ente_stesso, dbEnte p_dbContext)
        {
            //Recupero tipi voce di interesse in funzione dell'ente di riversamento
            IQueryable<tab_riversamenti_contrattuali> riversamenti_di_interesse = p_is_ente_stesso ? 
                TabRiversamentiContrattualiBD.GetRiversamentiSuStessoEnte(p_id_ente, p_dbContext) 
                : TabRiversamentiContrattualiBD.GetRiversamentiSuAltroEnte(p_id_ente, p_dbContext);

#if DEBUG && false
            var riversamenti_di_interesseLIST = riversamenti_di_interesse.ToList();
            var riversamenti_di_interesseDISTINCT_PKs = riversamenti_di_interesse.Select(r => r.id_entrata_riscossa ?? -1).Distinct().ToList();
#endif

            IQueryable<tab_tipo_voce_contribuzione> tipiVoceContrib_di_interesse = TabTipoVoceContribuzioneBD.GetListByIdEntrate(riversamenti_di_interesse.Select(r => r.id_entrata_riscossa ?? -1).Distinct(), p_dbContext);
#if DEBUG && false
            var tipiVoceContrib_di_interesseLIST = tipiVoceContrib_di_interesse.ToList();
#endif

            IEnumerable<tab_det_riscosso_periodo_lista> lst_consuntivi_dett = p_consuntivi_dett.AsEnumerable();
#if DEBUG && false
            var lst_consuntivi_dettLIST = lst_consuntivi_dett.ToList();
#endif

            lst_consuntivi_dett = lst_consuntivi_dett.Where(dc =>
                //dettagli relativi ai tipi voce selezionati
                tipiVoceContrib_di_interesse.Select(tv => tv.id_tipo_voce_contribuzione).Contains(dc.id_tipo_voce_contribuzione)
                &&
                //Recupero tab_riversamenti relativo a tipo_voce_contrib di dettaglio corrente
                riversamenti_di_interesse.FirstOrDefault(r => r.anagrafica_entrate.tab_tipo_voce_contribuzione1.Any(tv => tv.id_tipo_voce_contribuzione == dc.id_tipo_voce_contribuzione)) != null
                //&&
                ////Verifica date di riferimento: dett_consuntivo.anno_rif in [tab_riversamenti.Anno_rif_entrata_da, tab_riversamenti.Anno_rif_entrata_a]
                //riversamenti_di_interesse.FirstOrDefault(r => r.anagrafica_entrate.tab_tipo_voce_contribuzione1.Any(tv => tv.id_tipo_voce_contribuzione == dc.id_tipo_voce_contribuzione)).Anno_rif_entrata_da <= Convert.ToInt32(dc.anno_riferimento_voce_contribuzione)
                //&&
                //riversamenti_di_interesse.FirstOrDefault(r => r.anagrafica_entrate.tab_tipo_voce_contribuzione1.Any(tv => tv.id_tipo_voce_contribuzione == dc.id_tipo_voce_contribuzione)).Anno_rif_entrata_a >= Convert.ToInt32(dc.anno_riferimento_voce_contribuzione)
            );

            return lst_consuntivi_dett;
        }

        public static IEnumerable<tab_det_riscosso_periodo_lista> GetListDettaglioByRiversamento(IEnumerable<tab_det_riscosso_periodo_lista> p_consuntivi_dett, int p_id_ente, bool p_is_ente_stesso, dbEnte p_dbContext)
        {
            //Recupero tipi voce di interesse in funzione dell'ente di riversamento
            IQueryable<tab_riversamenti_contrattuali> riversamenti_di_interesse = p_is_ente_stesso ?
                TabRiversamentiContrattualiBD.GetRiversamentiSuStessoEnte(p_id_ente, p_dbContext)
                : TabRiversamentiContrattualiBD.GetRiversamentiSuAltroEnte(p_id_ente, p_dbContext);

#if DEBUG && false
            var riversamenti_di_interesseLIST = riversamenti_di_interesse.ToList();
            var riversamenti_di_interesseDISTINCT_PKs = riversamenti_di_interesse.Select(r => r.id_entrata_riscossa ?? -1).Distinct().ToList();
#endif
            IQueryable<int> idsEntrate = riversamenti_di_interesse.Select(r => r.id_entrata_riscossa ?? -1).Distinct();
            IQueryable<tab_tipo_voce_contribuzione> tipiVoceContrib_di_interesse = TabTipoVoceContribuzioneBD.GetListByIdEntrate(idsEntrate, p_dbContext);
            //IQueryable<tab_tipo_voce_contribuzione> tipiVoceContrib_di_interesse = TabTipoVoceContribuzioneBD.GetListByIdEntrate(riversamenti_di_interesse.Select(r => r.id_entrata_riscossa ?? -1).Distinct(), p_dbContext);
#if DEBUG && false
            var tipiVoceContrib_di_interesseLIST = tipiVoceContrib_di_interesse.ToList();
#endif

            IEnumerable<tab_det_riscosso_periodo_lista> lst_consuntivi_dett = null;
#if DEBUG && false
            var lst_consuntivi_dettLIST = lst_consuntivi_dett.ToList();
#endif
            if (p_consuntivi_dett != null)
            {
                lst_consuntivi_dett = p_consuntivi_dett.Where(dc =>
                    //dettagli relativi ai tipi voce selezionati
                    tipiVoceContrib_di_interesse.Select(tv => tv.id_tipo_voce_contribuzione).Contains(dc.id_tipo_voce_contribuzione)
                    &&
                    //Recupero tab_riversamenti relativo a tipo_voce_contrib di dettaglio corrente
                    riversamenti_di_interesse.FirstOrDefault(r => r.anagrafica_entrate.tab_tipo_voce_contribuzione1.Any(tv => tv.id_tipo_voce_contribuzione == dc.id_tipo_voce_contribuzione)) != null
                );
            }
            return lst_consuntivi_dett;
        }
    }
}
