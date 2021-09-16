using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabMovPagLinq
    {
        public static IQueryable<tab_mov_pag> OrderByDefault(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.OrderBy(d => d.data_operazione);
        }

        public static IQueryable<tab_mov_pag> OrderByDataAccreditoDesc(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.OrderByDescending(d => d.data_accredito);
        }

        public static IQueryable<tab_mov_pag> OrderByDataAccredito(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.OrderBy(d => d.data_accredito);
        }

        public static IQueryable<tab_mov_pag> WhereByRangeDataOperazione(this IQueryable<tab_mov_pag> p_query, DateTime p_dataDa, DateTime p_dataA)
        {
            return p_query.Where(d => d.data_operazione >= p_dataDa &&
                                     d.data_operazione <= p_dataA);

        }

        public static IQueryable<tab_mov_pag> WhereByAnagTipoPagamento(this IQueryable<tab_mov_pag> p_query, int p_tipo_pag)
        {
            return p_query.Where(d => d.tab_tipo_pagamento.anagrafica_tipo_pagamento.id_tipo_pagamento == p_tipo_pag);

        }
        public static IQueryable<tab_mov_pag> OrderByDataOperazioneDesc(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.OrderByDescending(d => d.data_operazione);
        }

        public static IQueryable<tab_mov_pag> WhereByIdMovPag(this IQueryable<tab_mov_pag> p_query, int p_idMovPag)
        {
            return p_query.Where(d => d.id_tab_mov_pag == p_idMovPag);
        }

        public static IQueryable<tab_mov_pag> WhereByIdContribuente(this IQueryable<tab_mov_pag> p_query, decimal p_idContribuente)
        {
            return p_query.Where(d => d.id_contribuente == p_idContribuente);
        }

        public static IQueryable<tab_mov_pag> WhereNotByIdContribuente(this IQueryable<tab_mov_pag> p_query, decimal p_idContribuente)
        {
            return p_query.Where(d => d.id_contribuente != p_idContribuente);
        }

        public static IQueryable<tab_mov_pag> WhereByIdContribuenteNull(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.Where(d => d.id_contribuente == null);
        }

        public static IQueryable<tab_mov_pag> WhereNotByIdContribuenteNull(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.Where(d => d.id_contribuente != null);
        }

        public static IQueryable<tab_mov_pag> WhereByNonAccoppiato(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.Where(d => d.join_avv_pag_mov_pag.Count == 0);
        }

        public static IQueryable<tab_mov_pag> WhereByIdTabCCRiscossione(this IQueryable<tab_mov_pag> p_query, int p_idTabCCRiscossione)
        {
            return p_query.Where(d => d.id_tab_cc_riscossione == p_idTabCCRiscossione);
        }

        public static IQueryable<tab_mov_pag> WhereByStatoVepRav(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.Where(d => d.cod_stato.StartsWith(anagrafica_stato_mov_pag.VEP) ||
                                      d.cod_stato.StartsWith(anagrafica_stato_mov_pag.RAV));
        }

        public static IQueryable<tab_mov_pag> WhereByCodStato(this IQueryable<tab_mov_pag> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<tab_mov_pag> WhereByCodStato(this IQueryable<tab_mov_pag> p_query, List<string> p_codStatoList)
        {
            return p_query.Where(d => p_codStatoList.Contains(d.cod_stato));
        }

        public static IQueryable<tab_mov_pag> WhereByStatoNonAnnullato(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(anagrafica_stato_mov_pag.ANNULLATO));
        }

        public static IQueryable<tab_mov_pag> WhereByStatoNonSmarrito(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(anagrafica_stato_mov_pag.SMARRITO));
        }

        public static IQueryable<tab_mov_pag> WhereByStatoNonMNC(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(anagrafica_stato_mov_pag.MNC));
        }

        public static IQueryable<tab_mov_pag> WhereByStatoNonMNCQUA(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(anagrafica_stato_mov_pag.MNC_QUA));
        }

        public static IQueryable<tab_mov_pag> WhereByStatoSmarrito(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.Where(d => d.cod_stato.StartsWith(anagrafica_stato_mov_pag.SMARRITO));
        }

        public static IQueryable<tab_mov_pag> WhereByDaAccoppiareAncora(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.Where(d => d.importo_mov_pagato > (d.join_avv_pag_mov_pag.Where(x => !x.cod_stato.StartsWith(join_avv_pag_mov_pag.ANN)).Count() > 0 ? d.join_avv_pag_mov_pag.Where(x => !x.cod_stato.StartsWith(join_avv_pag_mov_pag.ANN)).Sum(x => x.importo_pagato) : 0));
        }

        public static IQueryable<tab_mov_pag> WhereByRangeOperazione(this IQueryable<tab_mov_pag> p_query, DateTime? p_da, DateTime? p_a)
        {
            return p_query.Where(d => (d.data_operazione.HasValue ? d.data_operazione.Value : DateTime.MinValue) >= (p_da.HasValue ? p_da.Value : DateTime.MinValue) &&
                                      (d.data_operazione.HasValue ? d.data_operazione.Value : DateTime.MinValue) <= (p_a.HasValue ? p_a.Value : DateTime.MaxValue));
        }

        public static IQueryable<tab_mov_pag> WhereByRangeAccredito(this IQueryable<tab_mov_pag> p_query, DateTime? p_da, DateTime? p_a)
        {
            return p_query.Where(d => (d.data_accredito.HasValue ? d.data_accredito.Value : DateTime.MinValue) >= (p_da.HasValue ? p_da.Value : DateTime.MinValue) &&
                                      (d.data_accredito.HasValue ? d.data_accredito.Value : DateTime.MinValue) <= (p_a.HasValue ? p_a.Value : DateTime.MaxValue));
        }

        public static IQueryable<tab_mov_pag> WhereByDataAccreditoMinData(this IQueryable<tab_mov_pag> p_query, DateTime? p_data)
        {
            return p_query.Where(d => (d.data_accredito.HasValue ? d.data_accredito.Value : DateTime.MinValue) <= (p_data.HasValue ? p_data.Value : DateTime.MaxValue));
        }

        public static IQueryable<tab_mov_pag> WhereByDataAccreditoMagData(this IQueryable<tab_mov_pag> p_query, DateTime? p_data)
        {
            return p_query.Where(d => (d.data_accredito.HasValue ? d.data_accredito.Value : DateTime.MinValue) > (p_data.HasValue ? p_data.Value : DateTime.MinValue));
        }

        public static IQueryable<tab_mov_pag> WhereByRangeImporti(this IQueryable<tab_mov_pag> p_query, decimal? p_da, decimal? p_a)
        {
            return p_query.Where(d => (d.importo_mov_pagato.HasValue ? d.importo_mov_pagato.Value : 0) >= (p_da.HasValue ? p_da.Value : 0) &&
                                      (d.importo_mov_pagato.HasValue ? d.importo_mov_pagato.Value : 0) <= (p_a.HasValue ? p_a.Value : decimal.MaxValue));
        }

        public static IQueryable<tab_mov_pag> WhereByBollettiniPostali(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.Where(d => d.id_tipo_pagamento == tab_tipo_pagamento.BollettinoCCPostale ||
                                      d.id_tipo_pagamento == tab_tipo_pagamento.BollInternetICI ||
                                      d.id_tipo_pagamento == tab_tipo_pagamento.BollOrdinarioICI ||
                                      d.id_tipo_pagamento == tab_tipo_pagamento.BollettiniDematerializzati);
        }

        public static IQueryable<tab_mov_pag> WhereByBollettiniPostaliOrSmarritoRitrovato(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.Where(d => d.id_tipo_pagamento == tab_tipo_pagamento.BollettinoCCPostale ||
                                      d.id_tipo_pagamento == tab_tipo_pagamento.BollInternetICI ||
                                      d.id_tipo_pagamento == tab_tipo_pagamento.BollOrdinarioICI ||
                                      d.id_tipo_pagamento == tab_tipo_pagamento.BollettiniDematerializzati ||
                                      d.id_tipo_pagamento == tab_tipo_pagamento.BollettinoSmarritoRitrovato);
        }

        public static IQueryable<tab_mov_pag> WhereByBollettiniPostaliOrSmarritoRitrovatoOrPostaGiroOrTelematico(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.Where(d => d.id_tipo_pagamento == tab_tipo_pagamento.BollettinoCCPostale ||
                                      d.id_tipo_pagamento == tab_tipo_pagamento.BollInternetICI ||
                                      d.id_tipo_pagamento == tab_tipo_pagamento.BollOrdinarioICI ||
                                      d.id_tipo_pagamento == tab_tipo_pagamento.BollettiniDematerializzati ||
                                      d.id_tipo_pagamento == tab_tipo_pagamento.BollettinoSmarritoRitrovato ||
                                      d.id_tipo_pagamento == tab_tipo_pagamento.Postagiro ||
                                      d.id_tipo_pagamento == tab_tipo_pagamento.EuroGiro ||
                                      d.id_tipo_pagamento == tab_tipo_pagamento.EuroGiroRitrovato ||
                                      d.id_tipo_pagamento == tab_tipo_pagamento.AccreditoTelematico ||
                                      d.id_tipo_pagamento == tab_tipo_pagamento.AccreditoInternet ||
                                      d.id_tipo_pagamento == tab_tipo_pagamento.BollInternetICI);
        }

        public static IQueryable<tab_mov_pag> WhereByBonifici(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.Where(d => d.id_tipo_pagamento == tab_tipo_pagamento.Bonifico ||
                                      d.id_tipo_pagamento == tab_tipo_pagamento.BonificoICI ||
                                      d.id_tipo_pagamento == tab_tipo_pagamento.BonificoEstero);
        }

        public static IQueryable<tab_mov_pag> WhereByPostagiro(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.Where(d => d.id_tipo_pagamento == tab_tipo_pagamento.Postagiro ||
                                      d.id_tipo_pagamento == tab_tipo_pagamento.EuroGiro ||
                                      d.id_tipo_pagamento == tab_tipo_pagamento.EuroGiroRitrovato);
        }

        public static IQueryable<tab_mov_pag> WhereByF24(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.Where(d => d.id_tipo_pagamento == tab_tipo_pagamento.F24);
        }

        public static IQueryable<tab_mov_pag> WhereByNotF24(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.Where(d => d.id_tipo_pagamento != tab_tipo_pagamento.F24);
        }

        public static IQueryable<tab_mov_pag> WhereByF24IMU(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.Where(d => d.id_tipo_pagamento == tab_tipo_pagamento.F24 &&
                                      d.Id_entrata_riscossa == anagrafica_entrate.IMU);
        }

        public static IQueryable<tab_mov_pag> WhereByNotF24IMU(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.Where(d => d.id_tipo_pagamento != tab_tipo_pagamento.F24 &&
                                      d.Id_entrata_riscossa != anagrafica_entrate.IMU);
        }

        public static IQueryable<tab_mov_pag> WhereByNotIMU(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.Where(d => !d.Id_entrata_riscossa.HasValue ||
                                      (d.Id_entrata_riscossa != anagrafica_entrate.IMU &&
                                       d.Id_entrata_riscossa != anagrafica_entrate.ICI &&
                                       d.Id_entrata_riscossa != anagrafica_entrate.TASI) ||
                                     ((d.Id_entrata_riscossa == anagrafica_entrate.IMU ||
                                       d.Id_entrata_riscossa == anagrafica_entrate.ICI ||
                                       d.Id_entrata_riscossa == anagrafica_entrate.TASI) 
                                        &&
                                      (d.Flag_accertamento == "1" || 
                                       d.Flag_ravvedimento == "1")));
        }

        public static IQueryable<tab_mov_pag> WhereByTelematico(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.Where(d => d.id_tipo_pagamento == tab_tipo_pagamento.AccreditoTelematico ||
                                      d.id_tipo_pagamento == tab_tipo_pagamento.AccreditoInternet ||
                                      d.id_tipo_pagamento == tab_tipo_pagamento.BollInternetICI);
        }

        public static IQueryable<tab_mov_pag> WhereByCCEnte(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.Where(d => d.id_tipo_pagamento == tab_tipo_pagamento.CCGestitoEnte);
        }

        public static IQueryable<tab_mov_pag> WhereByCCGenerico(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.Where(d => d.id_tipo_pagamento == tab_tipo_pagamento.CCGenerico);
        }

        public static IQueryable<tab_mov_pag> WhereByPagamentoSmarrito(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.Where(d => d.id_tipo_pagamento == tab_tipo_pagamento.BollettinoSmarrito);
        }

        public static IQueryable<tab_mov_pag> WhereByPagamentoSmarritoRitrovato(this IQueryable<tab_mov_pag> p_query)
        {
            return p_query.Where(d => d.id_tipo_pagamento == tab_tipo_pagamento.BollettinoSmarritoRitrovato);
        }

        public static IQueryable<tab_mov_pag> WhereByCognomeRagioneSociale(this IQueryable<tab_mov_pag> p_query, string cognomeRagioneSociale)
        {
            return p_query.Where(d => d.cognome_rag_soc.ToUpper().Equals(cognomeRagioneSociale.ToUpper()));
        }

        public static IQueryable<tab_mov_pag> WhereByCodFisPIvaORCognomeRagSoc(this IQueryable<tab_mov_pag> p_query, string codFisPIva, string cognomeRagioneSociale)
        {
            return p_query.Where(d => d.cognome_rag_soc.ToUpper().Equals(cognomeRagioneSociale.ToUpper()) || d.codice_fiscale.ToUpper().Equals(codFisPIva.ToUpper()));
        }

        public static IQueryable<tab_mov_pag> WhereByCodFisPIvaPagante(this IQueryable<tab_mov_pag> p_query, string codFisPIva)
        {
            return p_query.Where(d => d.codice_fiscale.ToUpper().Equals(codFisPIva.ToUpper()) ||
                                      d.cf_piva_pagante.ToUpper().Equals(codFisPIva.ToUpper()));
        }

        public static IQueryable<tab_mov_pag> WhereByIdContribuenteOrCodFisPIva(this IQueryable<tab_mov_pag> p_query, decimal p_idContribuente, string codFisPIva)
        {
            return p_query.Where(d => d.id_contribuente == p_idContribuente || d.codice_fiscale.ToUpper().Equals(codFisPIva.ToUpper()));
        }

        public static IQueryable<tab_mov_pag> WhereByIdTerzoOrCodFisPIva(this IQueryable<tab_mov_pag> p_query, decimal p_idTerzo, string codFisPIva)
        {
            return p_query.Where(d => d.Id_terzo == p_idTerzo || d.codice_fiscale.ToUpper().Equals(codFisPIva.ToUpper()));
        }

        public static IQueryable<tab_mov_pag> WhereByIUV(this IQueryable<tab_mov_pag> p_query, string p_codline)
        {
            return p_query.Where(d => d.code_line.Equals(p_codline));
        }

        public static IQueryable<tab_mov_pag> WhereByPagante(this IQueryable<tab_mov_pag> p_query, string codiceFiscalePIva, string cognomeRagioneSociale, string nome)
        {
            if (!string.IsNullOrEmpty(codiceFiscalePIva))
            {
                p_query = p_query.Where(d => d.codice_fiscale.Equals(codiceFiscalePIva));
            }

            if (!string.IsNullOrEmpty(cognomeRagioneSociale))
            {
                p_query = p_query.Where(d => d.cognome_rag_soc.Equals(cognomeRagioneSociale));
            }

            if (!string.IsNullOrEmpty(nome))
            {
                p_query = p_query.Where(d => d.nome.Equals(nome));
            }

            return p_query;
        }

        public static IQueryable<tab_mov_pag> WhereByAnnoRif(this IQueryable<tab_mov_pag> p_query, int? p_AnnoRif)
        {
            return p_query.Where(d => (d.anno_riferimento.HasValue ? d.anno_riferimento.Value : 0) == (p_AnnoRif.HasValue ? p_AnnoRif.Value : DateTime.Now.Year));
        }

        public static IQueryable<tab_mov_pag> WhereByComuneImmobile(this IQueryable<tab_mov_pag> p_query, string p_ComuneImm)
        {
            return p_query.Where(d => d.comune_ubicazione_immobili.Equals(p_ComuneImm));
        }

        public static IQueryable<tab_mov_pag> WhereByImportoPagato(this IQueryable<tab_mov_pag> p_query, decimal? p_importo)
        {
            if (p_importo.HasValue)
            {
                //Il dottore ha voluto filtrare per l'importo esatto
                //return p_query.Where(d => System.Math.Abs((System.Math.Round((d.importo_mov_pagato.HasValue ? d.importo_mov_pagato.Value : 0) - p_importo.Value))) <= 1);
                return p_query.Where(d => d.importo_mov_pagato.Value == p_importo);
            }
            else
            {
                return p_query;
            }
        }

        public static IQueryable<tab_mov_pag> WhereByImportoPagatoUgualeMinore(this IQueryable<tab_mov_pag> p_query, decimal? p_importo)
        {
            if (p_importo.HasValue)
            {
                return p_query.Where(d => d.importo_mov_pagato.Value <= p_importo);
            }
            else
            {
                return p_query;
            }
        }

        [Obsolete("In caso di null non si può usare DateTime.MinValue, i campi sql server datetime hanno un range cahe va dal 1/1/1753 al 31/12/9999")]
        public static IQueryable<tab_mov_pag> WhereByDataOperazione(this IQueryable<tab_mov_pag> p_query, DateTime p_dataOperazione)
        {
            return p_query.Where(d => (d.data_operazione.HasValue ? DbFunctions.TruncateTime(d.data_operazione.Value) : DbFunctions.TruncateTime(DateTime.MinValue)) == DbFunctions.TruncateTime(p_dataOperazione));
        }

        public static IQueryable<tab_mov_pag> WhereByDataOperazioneNew(this IQueryable<tab_mov_pag> p_query, DateTime? p_dataOperazione)
        {
            if (p_dataOperazione != null)
            {
                return p_query.Where(d => DbFunctions.TruncateTime(d.data_operazione) == DbFunctions.TruncateTime(p_dataOperazione));
            }
            else
            {
                return p_query;
            }
        }

        public static IQueryable<tab_mov_pag> WhereByDataAccreditoNew(this IQueryable<tab_mov_pag> p_query, DateTime? p_dataOperazione)
        {
            if (p_dataOperazione != null)
            {
                return p_query.Where(d => DbFunctions.TruncateTime(d.data_accredito) == DbFunctions.TruncateTime(p_dataOperazione));
            }
            else
            {
                return p_query;
            }
        }

        [Obsolete("In caso di null non si può usare DateTime.MinValue, i campi sql server datetime hanno un range cahe va dal 1/1/1753 al 31/12/9999")]
        public static IQueryable<tab_mov_pag> WhereByDataValuta(this IQueryable<tab_mov_pag> p_query, DateTime? p_dataValuta)
        {
            return p_query.Where(d => (d.data_valuta.HasValue ? DbFunctions.TruncateTime(d.data_valuta.Value) : DbFunctions.TruncateTime(DateTime.MinValue)) == DbFunctions.TruncateTime(p_dataValuta));
        }

        public static IQueryable<tab_mov_pag> WhereByDataValutaNew(this IQueryable<tab_mov_pag> p_query, DateTime? p_dataValuta)
        {
            if (p_dataValuta != null)
            {
                return p_query.Where(d => DbFunctions.TruncateTime(d.data_valuta) == DbFunctions.TruncateTime(p_dataValuta));
            }
            else
            {
                return p_query;
            }
        }

        public static IQueryable<tab_mov_pag> WhereByLimiteDataSmarrimento(this IQueryable<tab_mov_pag> p_query, DateTime p_dataOperazione)
        {
            if (p_dataOperazione != DateTime.MaxValue)
            {
                p_dataOperazione = p_dataOperazione.AddDays(10);
            }
            return p_query.Where(d => (d.data_accredito.HasValue ? DbFunctions.TruncateTime(d.data_accredito.Value) : DbFunctions.TruncateTime(DateTime.MinValue)) <= DbFunctions.TruncateTime(p_dataOperazione));
        }

        public static IQueryable<tab_mov_pag> WhereByIdRisorsaAssegnazioneOrNull(this IQueryable<tab_mov_pag> p_query, int p_idRisorsa)
        {
            return p_query.Where(d => d.id_risorsa_assegnazione == p_idRisorsa ||
                                     !d.id_risorsa_assegnazione.HasValue);
        }

        public static IQueryable<tab_mov_pag_light> ToLight(this IQueryable<tab_mov_pag> iniziale)//, decimal p_idContribuente)
        {
            return iniziale.ToList().Select(d => new tab_mov_pag_light
            {
                id_tab_mov_pag = d.id_tab_mov_pag,
                DataOperazione = d.data_operazione_String,
                DataAccredito = d.data_accredito_String,
                DataValuta = d.data_valuta_String,
                Importo = d.importo_mov_pagato_Euro,
                ImportoAccoppiato = d.join_avv_pag_mov_pag.Where(x => !x.cod_stato.StartsWith(join_avv_pag_mov_pag.ANN)).ToList().Count > 0 ?
                                    d.join_avv_pag_mov_pag.Where(x => !x.cod_stato.StartsWith(join_avv_pag_mov_pag.ANN)).ToList().Sum(x => x.importo_pagato).ToString("C") :
                                    0.ToString("C"),
                ImportoDaAccoppiare = d.importo_mov_pagato.HasValue ?
                                        d.join_avv_pag_mov_pag.Where(x => !x.cod_stato.StartsWith(join_avv_pag_mov_pag.ANN)).ToList().Count > 0 ?
                                       (d.importo_mov_pagato.Value - d.join_avv_pag_mov_pag.Where(x => !x.cod_stato.StartsWith(join_avv_pag_mov_pag.ANN)).ToList().Sum(x => x.importo_pagato)).ToString("C") :
                                        d.importo_mov_pagato.Value.ToString("C") :
                                      0.ToString("C"),
                TipoPagamento = d.tab_tipo_pagamento.anagrafica_tipo_pagamento != null ? d.tab_tipo_pagamento.anagrafica_tipo_pagamento.desc_tipo_pagamento : string.Empty,
                CodStato = d.cod_stato,
                Stato = d.Stato,
                nome_file = d.nome_file_immagine,
                TipoPulsante = d.TipoPulsante,
                Contribuente = d.ContribuenteDisplay,
                id_contribuente = d.id_contribuente,
                iuv = d.Codice,//code_line,
                causale_pagamento = d.causale_pagamento,
                //AvvisiPagatiVisibile = d.join_avv_pag_mov_pag.Where(x => x.tab_avv_pag.id_anag_contribuente == p_idContribuente).Count() > 1 ? true : false
                AvvisiPagatiVisibile = true,
                RisorsaAssegnazione = d.anagrafica_risorse != null ? d.anagrafica_risorse.CognomeNome : string.Empty,
                CfPIVAContribuente = d.codice_fiscale,
                CfPIVAPagante = d.cf_piva_pagante
            }).AsQueryable();
        }

        public static IQueryable<tab_mov_pag> DefaultIncludesPagamenti(this IQueryable<tab_mov_pag> qry)
        {
            return qry.Include(x => x.tab_tipo_pagamento)
                      .Include(x => x.tab_tipo_pagamento.anagrafica_tipo_pagamento);
        }
    }
}
