using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class SumImportoUnitaFattEmessione : ISumImportoUnita, IUnitaAccertamento
    {
        public decimal? idContribuente { get; set; }
        public decimal? idOggetto { get; set; }
        public decimal SumImpTotEmesso { get; set; }
        public int? NumGiorniContribuzione { get; set; }
        public DateTime? dataInizioContribuzione { get; set; }
        public DateTime? dataFineContribuzione { get; set; }
        public string AnnoRif { get; set; }
        public int idEntrata { get; set; }
        public decimal? importoUnita { get; set; }
    }

    public class TabUnitaContribuzioneFattEmissioneBD : EntityBD<tab_unita_contribuzione_fatt_emissione>
    {
        public TabUnitaContribuzioneFattEmissioneBD()
        {

        }

        /// <summary>
        /// Filtro per id avviso pagamento generato
        /// </summary>
        /// <param name="p_idAvvPag"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_unita_contribuzione_fatt_emissione> GetListByIdAvvPag(int p_idAvvPag, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.id_avv_pag_generato == p_idAvvPag).OrderBy(d => d.id_entrata);
        }

        public static IEnumerable<IUnitaAccertamento> GetUnitaFatturateAcc(tab_oggetti_contribuzione p_oc, IList<int> p_list_tipo_avviso, IList<tab_unita_contribuzione_fatt_emissione> p_unitaFattEmiss, 
                                                                           dbEnte p_dbContext)
        {
            IEnumerable<IUnitaAccertamento> v_unitaFattAcc = p_unitaFattEmiss.Where(unita => unita.id_oggetto == p_oc.id_oggetto)
                                                                                .Where(unita => unita.id_contribuente == p_oc.id_contribuente)
                                                                                .Where(unita => unita.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.VALIDO))
                                                                                .Where(unita => unita.id_tipo_voce_contribuzione == tab_tipo_voce_contribuzione.SANZIONI_TARI ||
                                                                                                unita.id_tipo_voce_contribuzione == tab_tipo_voce_contribuzione.SANZIONI_TARSU ||
                                                                                                unita.id_tipo_voce_contribuzione == tab_tipo_voce_contribuzione.SANZIONI_PROV)
                                                                                .Where(unita => p_list_tipo_avviso.Contains(unita.id_tipo_avv_pag_generato))
                                                                                .Select(unita => new SumImportoUnitaContribuzione
                                                                                {
                                                                                    idContribuente = unita.id_contribuente,
                                                                                    idOggetto = unita.id_oggetto,
                                                                                    AnnoRif = unita.anno_rif,
                                                                                    idEntrata = unita.id_entrata,
                                                                                    importoUnita = unita.importo_unita_contribuzione
                                                                                }).ToList();
            return v_unitaFattAcc;
        }

        public static IEnumerable<ISumImportoUnita> GetSumImportoEmesso(tab_oggetti_contribuzione p_oc, IList<int> p_list_tipo_avviso, 
                                                                        IList<tab_unita_contribuzione_fatt_emissione> p_unitaFattEmiss, dbEnte p_dbContext)
        {
            IEnumerable<ISumImportoUnita> v_SumImpUnitaAcc = p_unitaFattEmiss
                                                                    .Where(unita => unita.id_oggetto == p_oc.id_oggetto)
                                                                    .Where(unita => unita.id_contribuente == p_oc.id_contribuente)
                                                                    .Where(unita => unita.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.VALIDO))
                                                                    .Where(unita => unita.id_anagrafica_voce_contribuzione == anagrafica_voci_contribuzione.TARSU ||
                                                                                    unita.id_anagrafica_voce_contribuzione == anagrafica_voci_contribuzione.TARI_IMPOSTA ||
                                                                                    unita.id_anagrafica_voce_contribuzione == anagrafica_voci_contribuzione.TARI_QUOTA_FISSA ||
                                                                                    unita.id_anagrafica_voce_contribuzione == anagrafica_voci_contribuzione.TARSU_PROV)
                                                                    .Where(unita => p_list_tipo_avviso.Contains(unita.id_tipo_avv_pag_generato))
                                                                    .GroupBy(unita => new
                                                                    {
                                                                        unita.id_contribuente,
                                                                        unita.id_oggetto,
                                                                        unita.num_giorni_contribuzione,
                                                                        unita.periodo_rif_da,
                                                                        unita.periodo_rif_a,
                                                                        _annoRif = unita.anno_rif
                                                                    })
                                                                    .Select(unita => new SumImportoUnitaContribuzione
                                                                    {
                                                                        idContribuente = unita.Key.id_contribuente,
                                                                        idOggetto = unita.Key.id_oggetto,
                                                                        SumImpTotEmesso = (decimal)unita.Sum(imp => imp.flag_segno == tab_unita_contribuzione_fatt_emissione.FLAG_SEGNO_NEGATIVO ? -imp.importo_unita_contribuzione  : imp.importo_unita_contribuzione),
                                                                        NumGiorniContribuzione = unita.Key.num_giorni_contribuzione,
                                                                        dataInizioContribuzione = unita.Key.periodo_rif_da,
                                                                        dataFineContribuzione = unita.Key.periodo_rif_a,
                                                                        AnnoRif = unita.Key._annoRif
                                                                    }).ToList();
            return v_SumImpUnitaAcc;
        }
    }
}
