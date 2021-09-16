using Publisoftware.Data.LinqExtended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class ICP_InsegneEsercizio
    {
        public decimal idContrinuente { get; set; }
        public int idTabDenuncia { get; set; }
        //public decimal supReale { get; set; }
        public decimal? supArrotondata { get; set; }
    }

    public class TabOggettiContribuzioneBD : EntityBD<tab_oggetti_contribuzione>
    {
        public TabOggettiContribuzioneBD()
        {

        }

        public static new IQueryable<tab_oggetti_contribuzione> GetList(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetListInternal(p_dbContext).Where(d => p_dbContext.idContribuenteDefaultList.Count == 0 || p_dbContext.idContribuenteDefaultList.Contains(d.id_contribuente));
        }

        public static new tab_oggetti_contribuzione GetById(Int32 p_id, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c => c.id_oggetto_contribuzione == p_id);
        }

        public static IQueryable<tab_oggetti_contribuzione> GetListByIdOggContrList(List<decimal> p_idListOggContr, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => p_idListOggContr.Contains(d.id_oggetto_contribuzione));

        }

        public static IQueryable<tab_oggetti_contribuzione> GetListByIdMacroentrataIdContribuente(int p_idMacroentrata, Decimal p_idContribuente, dbEnte p_dbContext)
        {
            List<tab_oggetti_contribuzione> v_listOggettiContribuzione = GetList(p_dbContext).Where(d => d.id_contribuente == p_idContribuente && d.id_entrata == p_idMacroentrata).ToList();

            v_listOggettiContribuzione = v_listOggettiContribuzione.Where(d => !d.data_fine_contribuzione.HasValue || (d.data_fine_contribuzione.HasValue && (d.data_fine_contribuzione.Value - d.data_inizio_contribuzione).Days > 1)).ToList();

            List<decimal> v_listIdOggetti = v_listOggettiContribuzione.Select(d => d.id_oggetto).Distinct().ToList();
            List<tab_oggetti_contribuzione> v_listOggettiContribuzioneFiltrata = new List<tab_oggetti_contribuzione>();

            foreach (decimal v_idOggetto in v_listIdOggetti)
            {
                tab_oggetti_contribuzione v_oggettiContribuzione = null;

                v_oggettiContribuzione = v_listOggettiContribuzione.Where(d => d.id_oggetto == v_idOggetto && d.cod_stato_oggetto.Equals(anagrafica_stato_oggetto.ATTIVO))
                                                                   .OrderByDescending(d => d.data_inizio_contribuzione)
                                                                   .ToList()
                                                                   .FirstOrDefault();

                if (v_oggettiContribuzione == null)
                {
                    v_oggettiContribuzione = v_listOggettiContribuzione.Where(d => d.id_oggetto == v_idOggetto && d.cod_stato_oggetto.Equals(anagrafica_stato_oggetto.CESSATO))
                                                                       .OrderByDescending(d => d.data_fine_contribuzione)
                                                                       .ToList()
                                                                       .FirstOrDefault();
                }

                if (v_oggettiContribuzione == null)
                {
                    v_oggettiContribuzione = v_listOggettiContribuzione.Where(d => d.id_oggetto == v_idOggetto && d.cod_stato_oggetto.StartsWith(anagrafica_stato_oggetto.ANN))
                                                                       .OrderByDescending(d => d.data_fine_contribuzione)
                                                                       .ToList()
                                                                       .FirstOrDefault();
                }

                if (v_oggettiContribuzione != null)
                {
                    v_listOggettiContribuzioneFiltrata.Add(v_oggettiContribuzione);
                }
            }

            return v_listOggettiContribuzioneFiltrata.AsQueryable();
        }

        public static IQueryable<tab_oggetti_contribuzione> GetListByIdContribuente(Decimal p_idContribuente, dbEnte p_dbContext)
        {
            List<tab_oggetti_contribuzione> v_listOggettiContribuzione = GetList(p_dbContext).Where(d => d.id_contribuente == p_idContribuente).ToList();

            v_listOggettiContribuzione = v_listOggettiContribuzione.Where(d => !d.data_fine_contribuzione.HasValue || (d.data_fine_contribuzione.HasValue && (d.data_fine_contribuzione.Value - d.data_inizio_contribuzione).Days > 1)).ToList();

            List<decimal> v_listIdOggetti = v_listOggettiContribuzione.Select(d => d.id_oggetto).Distinct().ToList();
            List<tab_oggetti_contribuzione> v_listOggettiContribuzioneFiltrata = new List<tab_oggetti_contribuzione>();

            foreach (decimal v_idOggetto in v_listIdOggetti)
            {
                tab_oggetti_contribuzione v_oggettiContribuzione = null;

                v_oggettiContribuzione = v_listOggettiContribuzione.Where(d => d.id_oggetto == v_idOggetto && d.cod_stato_oggetto.Equals(anagrafica_stato_oggetto.ATTIVO))
                                                                   .OrderByDescending(d => d.data_inizio_contribuzione)
                                                                   .ToList()
                                                                   .FirstOrDefault();

                if (v_oggettiContribuzione == null)
                {
                    v_oggettiContribuzione = v_listOggettiContribuzione.Where(d => d.id_oggetto == v_idOggetto && d.cod_stato_oggetto.Equals(anagrafica_stato_oggetto.CESSATO))
                                                                       .OrderByDescending(d => d.data_fine_contribuzione)
                                                                       .ToList()
                                                                       .FirstOrDefault();
                }

                if (v_oggettiContribuzione == null)
                {
                    v_oggettiContribuzione = v_listOggettiContribuzione.Where(d => d.id_oggetto == v_idOggetto && d.cod_stato_oggetto.StartsWith(anagrafica_stato_oggetto.ANN))
                                                                       .OrderByDescending(d => d.data_fine_contribuzione)
                                                                       .ToList()
                                                                       .FirstOrDefault();
                }

                if (v_oggettiContribuzione != null)
                {
                    v_listOggettiContribuzioneFiltrata.Add(v_oggettiContribuzione);
                }
            }

            return v_listOggettiContribuzioneFiltrata.AsQueryable();
        }

        public static IQueryable<tab_oggetti_contribuzione> GetListByIdListMacroentrata(List<int> p_idListMacroentrata, Decimal p_idContribuente, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.id_contribuente == p_idContribuente &&
                                                  (d.cod_stato_oggetto.Equals(anagrafica_stato_oggetto.ATTIVO) ||
                                                   d.cod_stato_oggetto.Equals(anagrafica_stato_oggetto.CESSATO) ||
                                                   d.cod_stato_oggetto.StartsWith(anagrafica_stato_oggetto.ANN)) &&
                                                   p_idListMacroentrata.Contains(d.id_entrata));
        }

        public static IQueryable<tab_oggetti_contribuzione> GetListVariazione(decimal p_idOggettoContribuzione, dbEnte p_dbContext)
        {
            tab_oggetti_contribuzione v_oggettoContribuzione = GetById(p_idOggettoContribuzione, p_dbContext);

            List<int> v_listIdEntrate = JoinEntrateMacroentrateBD.GetList(p_dbContext)
                                                                 .WhereIdMacroentrata(v_oggettoContribuzione.id_entrata)
                                                                 .Select(d => d.id_entrata)
                                                                 .ToList();

            return GetList(p_dbContext).Where(d => d.id_contribuente == v_oggettoContribuzione.id_contribuente &&
                                                   d.id_oggetto == v_oggettoContribuzione.id_oggetto &&
                                                   //!d.cod_stato_oggetto.StartsWith(anagrafica_stato_oggetto.ANN) &&
                                                   d.id_entrata == v_oggettoContribuzione.id_entrata);
        }

        public static IQueryable<tab_oggetti_contribuzione> GetListTARI(DateTime p_dataInizio, DateTime p_dataFine, int p_idServizio, dbEnte p_dbContext, bool p_simul = false, Decimal? p_idContribuente = null)
        {
            if (p_idServizio == anagrafica_tipo_servizi.GEST_ORDINARIA)
            {
                if (!p_simul)
                {
                    return GetList(p_dbContext)
                                        .Where(oc => oc.id_entrata == anagrafica_entrate.TARSU)
                                        .WhereAttivi(p_dataInizio, p_dataFine)
                                        .Where(oc => !p_idContribuente.HasValue || oc.id_contribuente == p_idContribuente)
                                        .Where(oc => !oc.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("1") && !oc.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("2"))
                                        .OrderBy(oc => new { oc.id_contribuente, oc.id_oggetto, oc.data_inizio_contribuzione });
                }
                else
                {
                    return GetList(p_dbContext)
                                        .WhereByIdEntrata(anagrafica_entrate.TARSU)
                                        .WhereRett(p_dataInizio, p_dataFine)
                                        .Where(oc => !p_idContribuente.HasValue || oc.id_contribuente == p_idContribuente)
                                        .Where(oc => !oc.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("1") && !oc.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("2"))
                                        .OrderBy(oc => new { oc.id_contribuente, oc.id_oggetto, oc.data_inizio_contribuzione });
                }
            }
            else if (p_idServizio == anagrafica_tipo_servizi.ACCERTAMENTO || p_idServizio == anagrafica_tipo_servizi.ACCERT_ESECUTIVO)
            {

                return GetList(p_dbContext)
                                    .WhereByIdEntrata(anagrafica_entrate.TARSU)
                                    .WhereAttiviAccertamenti(p_dataInizio, p_dataFine)
                                    .Where(oc => !p_idContribuente.HasValue || oc.id_contribuente == p_idContribuente)
                                    .OrderBy(oc => new { oc.id_contribuente, oc.id_oggetto, oc.data_inizio_contribuzione });

            }
            else
                return null;
        }


        public static IQueryable<tab_oggetti_contribuzione> GetListLMPVotive(DateTime p_dataInizio, DateTime p_dataFine, int p_idServizio, dbEnte p_dbContext, Decimal? p_idContribuente = null)
        {
            if (p_idServizio == anagrafica_tipo_servizi.GEST_ORDINARIA)
            {

                return GetList(p_dbContext)
                                    .WhereByIdEntrata(anagrafica_entrate.LMP_VOTIVE)
                                    .WhereAttivi(p_dataInizio, p_dataFine)
                                    .Where(oc => !p_idContribuente.HasValue || oc.id_contribuente == p_idContribuente)
                                    .Where(oc => !oc.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("1") && !oc.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("2"))
                                    .OrderBy(oc => new { oc.id_contribuente, oc.id_oggetto, oc.data_inizio_contribuzione });

            }
            else
                return null;

        }

        public static IQueryable<tab_oggetti_contribuzione> GetListICP(DateTime p_dataInizio, DateTime p_dataFine, int p_idServizio, dbEnte p_dbContext, Decimal? p_idContribuente = null, bool? p_includiInsEse = true)
        {
            if (p_idServizio == anagrafica_tipo_servizi.GEST_ORDINARIA)
            {

                return GetList(p_dbContext)
                                .WhereByIdEntrata(anagrafica_entrate.ICP)
                                .WhereAttivi(p_dataInizio, p_dataFine)
                                .Where(oc => !p_idContribuente.HasValue || oc.id_contribuente == p_idContribuente)
                                .Where(oc => (p_includiInsEse ?? true) ?
                                                                oc.anagrafica_categoria.tipo_insegna == oc.anagrafica_categoria.tipo_insegna :
                                                                oc.anagrafica_categoria.tipo_insegna != anagrafica_categoria.ICP_INSEGNA_ESERCIZIO)
                                .Where(oc => oc.anagrafica_categoria.macrocategoria == anagrafica_categoria.ICP_MACROCATEGORIA_PERM)
                                .Where(oc => oc.join_denunce_oggetti.Any(jdo => jdo.tab_denunce_contratti.cod_stato.StartsWith(anagrafica_stato_denunce.ATT)))
                                .Where(oc => !oc.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("1") && !oc.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("2"))
                                .OrderBy(oc => new { oc.id_contribuente, oc.id_oggetto, oc.data_inizio_contribuzione });

            }
            else
                return null;
        }

        public static IQueryable<tab_oggetti_contribuzione> GetListTOSAP(DateTime p_dataInizio, DateTime p_dataFine, int p_idServizio, string p_tipo_occupazione, dbEnte p_dbContext, Decimal? p_idContribuente = null)
        {
            if (p_idServizio == anagrafica_tipo_servizi.GEST_ORDINARIA)
            {

                return GetList(p_dbContext)
                                .WhereByIdEntrata(anagrafica_entrate.TOSAP)
                                .WhereAttivi(p_dataInizio, p_dataFine)
                                .Where(oc => !p_idContribuente.HasValue || oc.id_contribuente == p_idContribuente)
                                .Where(oc => p_tipo_occupazione == anagrafica_categoria.TIPO_OCC_ALTRA ?
                                            (
                                             !oc.anagrafica_categoria.tipo_spazio.StartsWith(anagrafica_categoria.TIPO_OCC_MERCATO_SETT) &&
                                             !oc.anagrafica_categoria.tipo_spazio.StartsWith(anagrafica_categoria.TIPO_OCC_PASSI_CARR) ||
                                                (
                                                    oc.anagrafica_categoria.tipo_spazio == "" ||
                                                    oc.anagrafica_categoria.tipo_spazio == null
                                                )
                                            ) :
                                             oc.anagrafica_categoria.tipo_spazio.StartsWith(p_tipo_occupazione)
                                      )
                                .Where(oc => !oc.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("1") && !oc.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("2"))
                                .OrderBy(oc => new { oc.id_contribuente, oc.id_oggetto, oc.data_inizio_contribuzione });
            }
            else
                return null;
        }

        public static IQueryable<tab_oggetti_contribuzione> GetListCOSAP(DateTime p_dataInizio, DateTime p_dataFine, int p_idServizio, string p_tipo_occupazione, dbEnte p_dbContext, Decimal? p_idContribuente = null)
        {
            if (p_idServizio == anagrafica_tipo_servizi.GEST_ORDINARIA)
            {

                return GetList(p_dbContext)
                                .WhereByIdEntrata(anagrafica_entrate.TOSAP)
                                .WhereAttivi(p_dataInizio, p_dataFine)
                                .Where(oc => !p_idContribuente.HasValue || oc.id_contribuente == p_idContribuente)
                                .Where(oc => oc.join_denunce_oggetti.Any(jdo => jdo.tab_denunce_contratti.cod_stato.StartsWith(anagrafica_stato_denunce.ATT)))
                                .Where(oc => p_tipo_occupazione == anagrafica_categoria.TIPO_OCC_ALTRA ?
                                            (
                                             !oc.anagrafica_categoria.tipo_spazio.StartsWith(anagrafica_categoria.TIPO_OCC_MERCATO_SETT) &&
                                             !oc.anagrafica_categoria.tipo_spazio.StartsWith(anagrafica_categoria.TIPO_OCC_PASSI_CARR) ||
                                                (
                                                    oc.anagrafica_categoria.tipo_spazio == "" ||
                                                    oc.anagrafica_categoria.tipo_spazio == null
                                                )
                                            ) :
                                             oc.anagrafica_categoria.tipo_spazio.StartsWith(p_tipo_occupazione)
                                      )
                                .Where(oc => !oc.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("1") && !oc.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("2"))
                                .OrderBy(oc => new { oc.id_contribuente, oc.id_oggetto, oc.data_inizio_contribuzione });
            }
            else
                return null;
        }

        public static IQueryable<tab_oggetti_contribuzione> GetListTARIG(DateTime p_dataInizio, DateTime p_dataFine, int p_idServizio, string p_tipo_occupazione, dbEnte p_dbContext, Decimal? p_idContribuente = null)
        {
            if (p_idServizio == anagrafica_tipo_servizi.GEST_ORDINARIA)
            {

                return GetList(p_dbContext)
                                .Where(oc => oc.id_entrata == anagrafica_entrate.TOSAP || oc.id_entrata == anagrafica_entrate.COSAP)
                                .WhereAttivi(p_dataInizio, p_dataFine)
                                .Where(oc => !p_idContribuente.HasValue || oc.id_contribuente == p_idContribuente)
                                .Where(oc => oc.anagrafica_categoria.tipo_spazio.StartsWith(p_tipo_occupazione))
                                .Where(oc => !oc.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("1") && !oc.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("2"))
                                .OrderBy(oc => new { oc.id_contribuente, oc.id_oggetto, oc.data_inizio_contribuzione });
            }
            else
                return null;
        }

        public static IQueryable<tab_oggetti_contribuzione> GetListICPbyDenuncia(DateTime p_dataInizio, DateTime p_dataFine, int p_idServizio, int p_idDenuncia, dbEnte p_dbContext, Decimal? p_idContribuente = null)
        {
            if (p_idServizio == anagrafica_tipo_servizi.GEST_ORDINARIA)
            {

                return GetList(p_dbContext)
                                    .WhereByIdEntrata(anagrafica_entrate.ICP)
                                    .WhereAttivi(p_dataInizio, p_dataFine)
                                    .Where(oc => !p_idContribuente.HasValue || oc.id_contribuente == p_idContribuente)
                                    .Where(oc => oc.anagrafica_categoria.tipo_insegna == anagrafica_categoria.ICP_INSEGNA_ESERCIZIO)
                                    .Where(oc => oc.anagrafica_categoria.macrocategoria == anagrafica_categoria.ICP_MACROCATEGORIA_PERM)
                                    .Where(oc => oc.join_denunce_oggetti.Any(jdo => jdo.tab_denunce_contratti.cod_stato.StartsWith(anagrafica_stato_denunce.ATT) &&
                                                                                    jdo.tab_denunce_contratti.id_tab_denunce_contratti == p_idDenuncia))
                                    .Where(oc => !oc.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("1") && !oc.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("2"))
                                    .OrderBy(oc => new { oc.id_contribuente, oc.id_oggetto, oc.data_inizio_contribuzione });

            }
            else
                return null;
        }

        public static IList<ICP_InsegneEsercizio> GetListICPSupArrIDE_GroupbyDenuncia(DateTime p_dataInizio, DateTime p_dataFine, dbEnte p_dbContext, Decimal? p_idContribuente = null)
        {
            var v_oggInsegnaEsercizio = GetList(p_dbContext)
                                        .WhereByIdEntrata(anagrafica_entrate.ICP)
                                        .WhereAttivi(p_dataInizio, p_dataFine)
                                        .Where(oc => !p_idContribuente.HasValue || oc.id_contribuente == p_idContribuente)
                                        .Where(oc => oc.anagrafica_categoria.tipo_insegna == anagrafica_categoria.ICP_INSEGNA_ESERCIZIO)
                                        .Where(oc => oc.anagrafica_categoria.macrocategoria == anagrafica_categoria.ICP_MACROCATEGORIA_PERM)
                                        .Where(oc => oc.join_denunce_oggetti.Any(jdo => jdo.tab_denunce_contratti.cod_stato.StartsWith(anagrafica_stato_denunce.ATT)))
                                        .Where(oc => !oc.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("1") &&
                                                     !oc.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("2"))
                                        .OrderBy(oc => new { oc.id_contribuente, oc.id_oggetto, oc.data_inizio_contribuzione })
                                        .GroupBy(oc => new
                                        {
                                            id_Contribuente = oc.id_contribuente,
                                            id_tab_denunce_contratti = oc.join_denunce_oggetti.Select(jdo => jdo.tab_denunce_contratti.id_tab_denunce_contratti).FirstOrDefault()//,
                                            }
                                                )
                                        .Select(oc => new ICP_InsegneEsercizio
                                        {
                                            idContrinuente = oc.Key.id_Contribuente,
                                            idTabDenuncia = oc.Key.id_tab_denunce_contratti,
                                            supArrotondata = oc.Sum(ogg => (
                                                                                    (((ogg.num_tot_unita_abitative ?? 0) / 100M) * ((ogg.num_tot_occupanti ?? 0) / 100M)) == 0 ? 0 :
                                                                                    (
                                                                                        (((ogg.num_tot_unita_abitative ?? 0) / 100M) * ((ogg.num_tot_occupanti ?? 0) / 100M)) > 0
                                                                                        &&
                                                                                        (((ogg.num_tot_unita_abitative ?? 0) / 100M) * ((ogg.num_tot_occupanti ?? 0) / 100M)) <= 1
                                                                                    ) ? 1 :
                                                                                    (((ogg.num_tot_unita_abitative ?? 0) / 100M) * ((ogg.num_tot_occupanti ?? 0) / 100M)) -
                                                                                        Math.Truncate(((ogg.num_tot_unita_abitative ?? 0) / 100M) * ((ogg.num_tot_occupanti ?? 0) / 100M)) == 0 ?
                                                                                    (((ogg.num_tot_unita_abitative ?? 0) / 100M) * ((ogg.num_tot_occupanti ?? 0) / 100M)) :
                                                                                    (
                                                                                        (((ogg.num_tot_unita_abitative ?? 0) / 100M * (ogg.num_tot_occupanti ?? 0) / 100M) -
                                                                                            Math.Truncate((((ogg.num_tot_unita_abitative ?? 0) / 100M) * ((ogg.num_tot_occupanti ?? 0) / 100M)))) < 0.5M
                                                                                            &&
                                                                                        ((((ogg.num_tot_unita_abitative ?? 0) / 100M) * ((ogg.num_tot_occupanti ?? 0) / 100M)) -
                                                                                            Math.Truncate((((ogg.num_tot_unita_abitative ?? 0) / 100M) * ((ogg.num_tot_occupanti ?? 0) / 100M)))) > 0
                                                                                    ) ?
                                                                                    Math.Truncate((((ogg.num_tot_unita_abitative ?? 0) / 100M) * ((ogg.num_tot_occupanti ?? 0) / 100M))) + 0.5M :
                                                                                    ((((ogg.num_tot_unita_abitative ?? 0) / 100M) * ((ogg.num_tot_occupanti ?? 0) / 100M)) -
                                                                                        Math.Truncate((((ogg.num_tot_unita_abitative ?? 0) / 100M) * ((ogg.num_tot_occupanti ?? 0) / 100M)))) == 0.5M ?
                                                                                    (((ogg.num_tot_unita_abitative ?? 0) / 100M) * ((ogg.num_tot_occupanti ?? 0) / 100M)) :
                                                                                    ((((ogg.num_tot_unita_abitative ?? 0) / 100M) * ((ogg.num_tot_occupanti ?? 0) / 100M)) -
                                                                                        Math.Truncate((((ogg.num_tot_unita_abitative ?? 0) / 100M) * ((ogg.num_tot_occupanti ?? 0) / 100M)))) > 0.5M ?
                                                                                        Math.Truncate((((ogg.num_tot_unita_abitative ?? 0) / 100M) * ((ogg.num_tot_occupanti ?? 0) / 100M))) + 1M :
                                                                                    (((ogg.num_tot_unita_abitative ?? 0) / 100M) * ((ogg.num_tot_occupanti ?? 0) / 100M))
                                                                                 ) * ((ogg.quantita_base ?? 1) < 2 ? 1 : ogg.quantita_base) * ((ogg.nolo_contatore ?? 1) == 0 ? 1 : (ogg.nolo_contatore ?? 1))
                                                                                  )
                                        }
                                                )
                                        .ToList();

            return v_oggInsegnaEsercizio;
        }

        public static tab_oggetti_contribuzione creaOggettoContribuzionePark(Int32 p_idDenunciaContratto, decimal p_idContribuente, decimal p_idOggetto, Int32 p_idCategoriaTariffaria,
                                                                            decimal p_importo, DateTime p_emissioneVerbale, Int32 p_idOperatore, Int32 p_idStrutturaStato,
                                                                            Int32 p_idVeicolo, Int32 p_idDenunciaContrattoRif, Int32 p_idEnte, Int32 p_idEntrata, dbEnte p_context)
        {
            tab_oggetti_contribuzione v_OggettoContribuzione = p_context.tab_oggetti_contribuzione.Create();

            v_OggettoContribuzione.id_ente = p_idEnte;
            v_OggettoContribuzione.id_ente_gestito = p_idEnte;
            v_OggettoContribuzione.id_entrata = p_idEntrata;
            v_OggettoContribuzione.id_oggetto = p_idOggetto;
            v_OggettoContribuzione.id_contribuente = p_idContribuente;
            v_OggettoContribuzione.id_stato_oggetto = 1;
            v_OggettoContribuzione.cod_stato_oggetto = tab_oggetti_contribuzione.ATT_ATT;
            v_OggettoContribuzione.id_categoria_tariffaria = p_idCategoriaTariffaria;
            v_OggettoContribuzione.quantita_base = (decimal)p_importo;
            v_OggettoContribuzione.data_inizio_contribuzione = p_emissioneVerbale;
            v_OggettoContribuzione.data_attivazione_ogg_contribuzione = p_emissioneVerbale;
            v_OggettoContribuzione.data_stato = p_emissioneVerbale;
            v_OggettoContribuzione.id_struttura_stato = p_idStrutturaStato;
            v_OggettoContribuzione.id_risorsa_stato = p_idOperatore;

            p_context.tab_oggetti_contribuzione.Add(v_OggettoContribuzione);


            join_denunce_oggetti v_Join = new join_denunce_oggetti();

            v_Join.id_denunce_contratti = p_idDenunciaContratto;
            v_Join.id_oggetti_contribuzione = (int)v_OggettoContribuzione.id_oggetto_contribuzione;
            v_Join.id_risorsa_acquisizione = p_idOperatore;
            v_Join.data_stato = DateTime.Now.Date;
            v_Join.data_creazione = p_emissioneVerbale;
            v_Join.num_ordine_den_ici = p_idVeicolo;
            v_Join.id_struttura_stato = p_idStrutturaStato;
            v_Join.id_risorsa_stato = p_idOperatore;

            if (p_idDenunciaContrattoRif > 0)
                v_Join.prog_num_ordine_den_ici = p_idDenunciaContrattoRif;

            p_context.join_denunce_oggetti.Add(v_Join);

            return v_OggettoContribuzione;
        }
    }
}
