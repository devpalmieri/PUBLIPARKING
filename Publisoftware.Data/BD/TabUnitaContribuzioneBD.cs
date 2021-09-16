using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class SumImportoUnitaContribuzione : ISumImportoUnita, IUnitaAccertamento 
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

    public class TabUnitaContribuzioneBD : EntityBD<tab_unita_contribuzione>
    {
        public TabUnitaContribuzioneBD()
        {

        }

        public static tab_unita_contribuzione Consolida(tab_unita_contribuzione_fatt_emissione p_uc_fatt_emissione)
        {
            tab_unita_contribuzione v_unitaDef = new tab_unita_contribuzione();
            v_unitaDef.setProperties(p_uc_fatt_emissione);

            return v_unitaDef;
        }

        public static IQueryable<tab_unita_contribuzione> GetUnitaAnnullateODiAvvisiCollegatiAnnullati(dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => ((d.id_avv_pag_collegato.HasValue && (d.tab_avv_pag1.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO) || d.tab_avv_pag1.cod_stato.StartsWith(anagrafica_stato_avv_pag.RETTIFICATO))) || 
                                                     d.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.ANNULLATO)) &&
                                                   d.tab_avv_pag.cod_stato.Equals(anagrafica_stato_avv_pag.VAL_EME) &&
                                                  !d.tab_avv_pag.fonte_emissione.Contains(tab_avv_pag.FONTE_IMPORTATA) &&
                                                  (d.tab_avv_pag.flag_rateizzazione_bis == null || d.tab_avv_pag.flag_rateizzazione_bis != "1"));
        }

        public static Int32 CheckUnitaContribFatturate(IQueryable<tab_unita_contribuzione> p_UnitaAppo, tab_oggetti_contribuzione p_oc, int p_annoRiferimento, dbEnte p_dbContext)
        {
            Int32 v_unitaFatt = 0;

            IQueryable<tab_unita_contribuzione> v_UnitaAppo = p_UnitaAppo; 

            if (v_UnitaAppo != null)
            {
                v_unitaFatt = v_UnitaAppo.Where(unita => unita.id_oggetto == null)
                                         .Where(unita => unita.id_contribuente == p_oc.id_contribuente).Count(); //.ToList();

                if (v_unitaFatt == 0)
                {
                    v_unitaFatt = v_UnitaAppo.Where(unita => unita.id_oggetto == p_oc.id_oggetto)
                                             .Where(unita => unita.id_contribuente == p_oc.id_contribuente)
                                             .Where(unita => unita.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.DARETTIFICARE) && 
                                                             !unita.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO)).Count();
                }

                if (v_unitaFatt == 0)
                {
                    v_unitaFatt = v_UnitaAppo.Where(unita => unita.id_contribuente == p_oc.id_contribuente)
                                             .Where(unita => unita.id_anagrafica_voce_contribuzione == anagrafica_voci_contribuzione.TARI_IMPOSTA)
                                             .Where(unita => unita.quantita_unita_contribuzione == p_oc.quantita_base)
                                             .Where(unita => unita.tab_oggetti.id_toponimo == p_oc.tab_oggetti.id_toponimo)
                                             .Where(unita => unita.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.VALIDO) || 
                                                             unita.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.SOSPESO))
                                             .Where(unita => !unita.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO)).Count();
                }

                if (v_unitaFatt == 0)
                {
                    v_unitaFatt = v_UnitaAppo.Where(unita => unita.id_contribuente == p_oc.id_contribuente)
                                             .Where(unita => unita.tab_oggetti.id_toponimo == p_oc.tab_oggetti.id_toponimo)
                                             .Where(unita => unita.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.SOSPESO) || 
                                                   (unita.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.VALIDO) && unita.quantita_unita_contribuzione == p_oc.quantita_base))
                                             .Where(unita => unita.periodo_contribuzione_da == p_oc.data_inizio_contribuzione)
                                             .Where(unita => unita.periodo_contribuzione_a == (p_oc.data_fine_contribuzione.HasValue ? p_oc.data_fine_contribuzione : new DateTime(p_annoRiferimento,12,31)))
                                             .Where(unita => !unita.tab_avv_pag.cod_stato.Contains(anagrafica_stato_avv_pag.ANNULLATO)).Count();
                }
            }

            return v_unitaFatt;
        }

        public static IList<tab_unita_contribuzione> CheckUnitaContribFatturateAcc(IQueryable<tab_unita_contribuzione> p_UnitaAppo, tab_oggetti_contribuzione p_oc, int p_annoRiferimento, 
                                                                                   char? p_flagTipoAcc, dbEnte p_dbContext)
        {
            IList<tab_unita_contribuzione> v_unitaFatt = null;

            IQueryable<tab_unita_contribuzione> v_UnitaAppo = p_UnitaAppo;

            if (v_UnitaAppo != null)
            {
                    v_unitaFatt = GetList(p_dbContext).Where(unita => unita.id_oggetto == p_oc.id_oggetto)
                                                      .Where(unita => unita.id_contribuente == p_oc.id_contribuente)
                                                      .Where(unita => unita.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.DARETTIFICARE))
                                                      .Where(unita => unita.id_tipo_avv_pag_generato == anagrafica_tipo_avv_pag.AVVISO_ORDINARIO_TARI ||
                                                                      unita.id_tipo_avv_pag_generato == anagrafica_tipo_avv_pag.AVVISO_ORDINARIO_TARI_NOT ||
                                                                      unita.id_tipo_avv_pag_generato == anagrafica_tipo_avv_pag.AVVISO_SUPPLETIVO_TARI ||
                                                                      unita.id_tipo_avv_pag_generato == anagrafica_tipo_avv_pag.AVVISO_SUPPLETIVO_TARI_NOT ||
                                                                      unita.id_tipo_avv_pag_generato == anagrafica_tipo_avv_pag.AVVISO_ACC_INFEDELE_TARI ||
                                                                      unita.id_tipo_avv_pag_generato == anagrafica_tipo_avv_pag.AVVISO_ACC_OMESSA_TARI ||
                                                                      unita.id_tipo_avv_pag_generato == anagrafica_tipo_avv_pag.AVVISO_ACC_OMESSA_ESEC_TARI ||
                                                                      unita.id_tipo_avv_pag_generato == anagrafica_tipo_avv_pag.AVVISO_ACC_INFEDELE_ESEC_TARI).ToList();
                if (v_unitaFatt.Count == 0)
                {

                    if (p_flagTipoAcc == 'I')
                    {
                        v_unitaFatt = v_UnitaAppo.Where(unita => unita.id_oggetto == p_oc.id_oggetto)
                                                        .Where(unita => unita.id_contribuente == p_oc.id_contribuente)
                                                        .Where(unita => unita.id_anagrafica_voce_contribuzione == anagrafica_voci_contribuzione.TARI_IMPOSTA ||
                                                                        unita.id_anagrafica_voce_contribuzione == anagrafica_voci_contribuzione.TARSU)
                                                        .Where(unita => (
                                                                             (
                                                                                unita.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.DARETTIFICARE) &&
                                                                                unita.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.ANNULLATO)
                                                                             ) ||
                                                                             !unita.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.ANNULLATO)
                                                                        ))
                                                        .Where(unita => unita.id_tipo_avv_pag_generato == anagrafica_tipo_avv_pag.AVVISO_ACC_INFEDELE_TARI ||
                                                                        unita.id_tipo_avv_pag_generato == anagrafica_tipo_avv_pag.AVVISO_ACC_OMESSA_TARI ||
                                                                        unita.id_tipo_avv_pag_generato == anagrafica_tipo_avv_pag.AVVISO_ACC_OMESSA_ESEC_TARI ||
                                                                        unita.id_tipo_avv_pag_generato == anagrafica_tipo_avv_pag.AVVISO_ACC_INFEDELE_ESEC_TARI)
                                                        .Where(unita => unita.anno_rif == p_annoRiferimento.ToString()).ToList();

                    }
                    else
                    { 
                        v_unitaFatt = v_UnitaAppo.Where(unita => unita.id_oggetto == p_oc.id_oggetto)
                                                        .Where(unita => unita.id_contribuente == p_oc.id_contribuente)
                                                        .Where(unita => unita.id_anagrafica_voce_contribuzione == anagrafica_voci_contribuzione.TARI_IMPOSTA ||
                                                                        unita.id_anagrafica_voce_contribuzione == anagrafica_voci_contribuzione.TARSU)
                                                        .Where(unita => (
                                                                             (
                                                                                unita.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.DARETTIFICARE) &&
                                                                                unita.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.ANNULLATO)
                                                                             ) ||
                                                                             !unita.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.ANNULLATO)
                                                                        ))
                                                        .Where(unita => unita.id_tipo_avv_pag_generato == anagrafica_tipo_avv_pag.AVVISO_ORDINARIO_TARI ||
                                                                        unita.id_tipo_avv_pag_generato == anagrafica_tipo_avv_pag.AVVISO_ORDINARIO_TARI_NOT ||
                                                                        unita.id_tipo_avv_pag_generato == anagrafica_tipo_avv_pag.AVVISO_SUPPLETIVO_TARI ||
                                                                        unita.id_tipo_avv_pag_generato == anagrafica_tipo_avv_pag.AVVISO_SUPPLETIVO_TARI_NOT ||
                                                                        unita.id_tipo_avv_pag_generato == anagrafica_tipo_avv_pag.AVVISO_ACC_INFEDELE_TARI ||
                                                                        unita.id_tipo_avv_pag_generato == anagrafica_tipo_avv_pag.AVVISO_ACC_OMESSA_TARI ||
                                                                        unita.id_tipo_avv_pag_generato == anagrafica_tipo_avv_pag.AVVISO_ACC_OMESSA_ESEC_TARI ||
                                                                        unita.id_tipo_avv_pag_generato == anagrafica_tipo_avv_pag.AVVISO_ACC_INFEDELE_ESEC_TARI)
                                                        .Where(unita => unita.anno_rif == p_annoRiferimento.ToString()).ToList();
                    }
                }
            }

            return v_unitaFatt;
        }

        public static IList<tab_unita_contribuzione> GetUnitaFatturate(tab_oggetti_contribuzione p_oc, List<int> p_list_tipo_avviso , int p_annoRiferimento, int p_idEntrata, int p_idAnagVoceCotribuzione, dbEnte p_dbContext)
        {
            IList<tab_unita_contribuzione> v_unitaFatt = GetList(p_dbContext).Where(unita => unita.id_oggetto == p_oc.id_oggetto)
                                                                    .Where(unita => unita.id_contribuente == p_oc.id_contribuente)
                                                                    .Where(unita => unita.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.VALIDO))
                                                                    .Where(unita => unita.id_anagrafica_voce_contribuzione == p_idAnagVoceCotribuzione)
                                                                    .Where(unita => unita.id_entrata == p_idEntrata)
                                                                    .Where(unita => unita.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO))
                                                                    .Where(unita => p_list_tipo_avviso.Contains(unita.id_tipo_avv_pag_generato))
                                                                    .Where(unita => unita.flag_tipo_addebito != tab_unita_contribuzione.FLAG_TIPO_ADDEBITO_CONGUAGLIO)
                                                                    .Where(unita => unita.anno_rif == p_annoRiferimento.ToString()).ToList();
            return v_unitaFatt;
        }

        public static IEnumerable<IUnitaAccertamento> GetUnitaFatturateAcc(tab_oggetti_contribuzione p_oc, IList<int> p_list_tipo_avviso, dbEnte p_dbContext)
        {
            IEnumerable<IUnitaAccertamento> v_unitaFattAcc =  GetList(p_dbContext).Where(unita => unita.id_oggetto == p_oc.id_oggetto)
                                                                                     .Where(unita => unita.id_contribuente == p_oc.id_contribuente)
                                                                                     .Where(unita => unita.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO))
                                                                                     .Where(unita => unita.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.VALIDO))
                                                                                     .Where(unita => unita.id_tipo_voce_contribuzione == tab_tipo_voce_contribuzione.SANZIONI_TARI ||
                                                                                                     unita.id_tipo_voce_contribuzione == tab_tipo_voce_contribuzione.SANZIONI_TARSU ||
                                                                                                     unita.id_tipo_voce_contribuzione == tab_tipo_voce_contribuzione.SANZIONI_PROV)
                                                                                     .Where(unita => p_list_tipo_avviso.Contains(unita.id_tipo_avv_pag_generato))
                                                                                      .Select(unita => new SumImportoUnitaContribuzione()
                                                                                      {
                                                                                          idContribuente = unita.id_contribuente,
                                                                                          idOggetto = unita.id_oggetto,
                                                                                          AnnoRif = unita.anno_rif,
                                                                                          idEntrata = unita.id_entrata,
                                                                                          importoUnita = unita.importo_unita_contribuzione ?? 0 
                                                                                      }).ToList();

            return v_unitaFattAcc;
        }

        public static IEnumerable<ISumImportoUnita> GetSumImportoEmesso(tab_oggetti_contribuzione p_oc, IList<int> p_list_tipo_avviso, dbEnte p_dbContext)
        {

            IEnumerable<ISumImportoUnita> v_SumImpUnitaAcc = GetList(p_dbContext)
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
                                                                        SumImpTotEmesso = unita.Sum(imp => imp.flag_segno == tab_unita_contribuzione_fatt_emissione.FLAG_SEGNO_NEGATIVO ? -imp.importo_unita_contribuzione ?? 0 : imp.importo_unita_contribuzione ?? 0),
                                                                        NumGiorniContribuzione = unita.Key.num_giorni_contribuzione,
                                                                        dataInizioContribuzione = unita.Key.periodo_rif_da,
                                                                        dataFineContribuzione = unita.Key.periodo_rif_a,
                                                                        AnnoRif = unita.Key._annoRif
                                                                    }).ToList();
            return v_SumImpUnitaAcc;
        }

        /// <summary>
        /// Restituisce l'entità a partire da id_unita_collegata
        /// </summary>
        /// <param name="p_idUnitaCollegata"></param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static tab_unita_contribuzione GetByidUnitaCollegata(Int32 p_idUnitaCollegata, dbEnte p_dbContext)

        {
            return GetList(p_dbContext).SingleOrDefault(c => c.id_unita_contribuzione_collegato == p_idUnitaCollegata);
        }

        /// <summary>
        /// Restituisce la liste delle unità contribuzione da annullare quando si trattano avvisi non coattivi pagati nei termini
        /// </summary>
        /// <param name="p_idAvvPag">ID Avviso ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_unita_contribuzione> GetListTrattFuoriTermini(int p_idEnte, int p_idEnteG, decimal p_idContribuente, int p_idAvvPagPagato, DateTime p_dataPagamento, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.tab_avv_pag.dt_emissione > p_dataPagamento && d.id_avv_pag_generato == p_idAvvPagPagato && (!d.cod_stato.StartsWith(CodStato.ANN)) && d.tab_avv_pag.cod_stato.StartsWith(CodStato.VAL) && d.id_ente == p_idEnte && d.id_ente_gestito == p_idEnteG && d.id_contribuente == p_idContribuente && (d.cod_stato.StartsWith(CodStato.VAL) || d.cod_stato.StartsWith(CodStato.ATT))).OrderBy(d => d.id_unita_contribuzione);
        }

        /// <summary>
        /// Restituisce la liste delle unità contribuzione da annullare quando si trattano avvisi non coattivi pagati nei termini
        /// </summary>
        /// <param name="p_idAvvPag">ID Avviso ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_unita_contribuzione> GetListCollegati(int p_idEnte, int p_idEnteG, decimal p_idContribuente, int p_idAvvPagAccreditato, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.id_avv_pag_collegato == p_idAvvPagAccreditato && (!d.cod_stato.StartsWith(CodStato.ANN)) && d.tab_avv_pag.cod_stato.StartsWith(CodStato.VAL) && d.id_avv_pag_collegato != null && d.id_ente == p_idEnte && d.id_ente_gestito == p_idEnteG && d.id_contribuente == p_idContribuente).OrderBy(z => z.tab_avv_pag.dt_emissione);
        }

        /// <summary>
        /// vers. 29/09/2016 Restituisce la liste delle unità contribuzione da annullare quando si trattano avvisi non coattivi pagati nei termini
        /// </summary>
        /// <param name="p_idAvvPag">ID Avviso ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_unita_contribuzione> GetListIngCollegate(int p_idEnte, int p_idEnteG, int p_idAvvPagCollegati, int p_idAvvPagAccredito, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.id_avv_pag_collegato == p_idAvvPagCollegati && d.id_avv_pag_generato != p_idAvvPagAccredito && (!d.cod_stato.StartsWith(CodStato.ANN)) && d.tab_avv_pag.cod_stato.StartsWith(CodStato.VAL) && d.id_ente == p_idEnte && d.id_ente_gestito == p_idEnteG).OrderBy(z => z.tab_avv_pag.dt_emissione);
        }

        /// <summary>
        /// Restituisce la liste delle unità contribuzione da annullare quando si trattano avvisi non coattivi pagati nei termini
        /// </summary>
        /// <param name="p_idAvvPag">ID Avviso ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_unita_contribuzione> GetListAnnullaNonCoaEntroTermini(int p_idEnte, int p_idEnteG, decimal p_idContribuente, int p_idAvvPagAccreditato, decimal p_importoPagato, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.id_avv_pag_collegato == p_idAvvPagAccreditato && d.importo_unita_contribuzione == p_importoPagato && d.tab_avv_pag.cod_stato.Equals(anagrafica_stato_avv_pag.ANN_PAG) && d.tab_avv_pag.cod_stato.StartsWith(CodStato.VAL) && d.id_ente == p_idEnte && d.id_ente_gestito == p_idEnteG && d.id_contribuente == p_idContribuente);
        }

        /// <summary>
        /// annullamento pagamento ingiunzione
        /// </summary>
        /// <param name="p_idAvvPag">ID Avviso ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_unita_contribuzione> GetListCollegatiValidiCongeneratiValidi(int p_idAvvPag, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.id_avv_pag_collegato == p_idAvvPag && 
                                                   !d.cod_stato.StartsWith(CodStato.ANN) && 
                                                   d.tab_avv_pag.cod_stato.StartsWith(CodStato.VAL));
        }

        public static IQueryable<tab_unita_contribuzione> GetListIdAvvisoGenerato(int p_idAvvPag, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.id_avv_pag_generato == p_idAvvPag);
        }

        public static tab_unita_contribuzione creaUnitaContribuzionePark(tab_avv_pag p_tab_avv_pag, int p_tipoAvvPag, int p_NumRiga, decimal p_idContribuente, decimal p_idOggetto, tab_oggetti_contribuzione p_oggettoContribuzione, 
                                   int p_idVeicolo, decimal p_importo, DateTime p_emissioneVerbale, int p_idStrutturaStato, int p_idRisorsa, int p_idEnte, int p_idEntrata, 
                                   dbEnte p_context)
        {
            tab_unita_contribuzione v_unitaContribuzione = p_context.tab_unita_contribuzione.Create();

            v_unitaContribuzione.id_ente = p_idEnte;
            v_unitaContribuzione.id_ente_gestito = p_idEnte;
            v_unitaContribuzione.id_entrata = p_idEntrata;
            v_unitaContribuzione.id_tipo_avv_pag_generato = p_tipoAvvPag;
            v_unitaContribuzione.tab_avv_pag = p_tab_avv_pag;
            v_unitaContribuzione.num_riga_avv_pag_generato = p_NumRiga;
            v_unitaContribuzione.id_anagrafica_voce_contribuzione = 153;
            v_unitaContribuzione.id_tipo_voce_contribuzione = 52;
            v_unitaContribuzione.flag_tipo_addebito = "1";
            v_unitaContribuzione.anno_rif = p_emissioneVerbale.Year.ToString();
            v_unitaContribuzione.periodo_rif_da = p_emissioneVerbale;
            v_unitaContribuzione.periodo_rif_a = p_emissioneVerbale;
            v_unitaContribuzione.periodo_contribuzione_da = p_emissioneVerbale;
            v_unitaContribuzione.periodo_contribuzione_a = p_emissioneVerbale;
            v_unitaContribuzione.id_contribuente = p_idContribuente;
            v_unitaContribuzione.id_oggetto = p_idOggetto;
            v_unitaContribuzione.tab_oggetti_contribuzione = p_oggettoContribuzione;
            v_unitaContribuzione.id_fatt_consumi = p_idVeicolo;
            v_unitaContribuzione.flag_segno = "1";
            v_unitaContribuzione.quantita_unita_contribuzione = 1;
            v_unitaContribuzione.importo_unitario_contribuzione = p_importo;
            v_unitaContribuzione.importo_unita_contribuzione = p_importo;
            v_unitaContribuzione.flag_val = "1";
            v_unitaContribuzione.cod_stato = anagrafica_stato_unita_contribuzione.VAL_CON;
            v_unitaContribuzione.id_stato = anagrafica_stato_unita_contribuzione.VAL_CON_ID;            
            v_unitaContribuzione.data_stato = p_emissioneVerbale;
            v_unitaContribuzione.id_struttura_stato = p_idStrutturaStato;
            v_unitaContribuzione.id_risorsa_stato = p_idRisorsa;

            p_context.tab_unita_contribuzione.Add(v_unitaContribuzione);
           
            return v_unitaContribuzione;
        }


    }
}
