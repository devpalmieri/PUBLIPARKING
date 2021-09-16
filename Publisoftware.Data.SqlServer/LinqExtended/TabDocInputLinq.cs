using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabDocInputLinq
    {
        public static IQueryable<tab_doc_input> WhereById(this IQueryable<tab_doc_input> p_query, int p_id)
        {
            return p_query.Where(d => d.id_tab_doc_input == p_id);
        }

        public static IQueryable<tab_doc_input> WhereByIdTipoDocEntrate(this IQueryable<tab_doc_input> p_query, int p_idTipoDocEntrata)
        {
            return p_query.Where(d => d.id_tipo_doc_entrate == p_idTipoDocEntrata);
        }

        public static IQueryable<tab_doc_input> WhereByIdContribuente(this IQueryable<tab_doc_input> p_query, decimal p_idContribuente)
        {
            return p_query.Where(d => d.id_contribuente == p_idContribuente);
        }

        public static IQueryable<tab_doc_input> WhereByIdTipoDocEntrate(this IQueryable<tab_doc_input> p_query, List<int?> p_tipoDocEntrataIdList)
        {
            return p_query.Where(d => p_tipoDocEntrataIdList.Contains(d.id_tipo_doc_entrate));
        }

        public static IQueryable<tab_doc_input> WhereByIdTipoDoc(this IQueryable<tab_doc_input> p_query, int p_idTipoDoc)
        {
            return p_query.Where(d => d.tab_tipo_doc_entrate.id_tipo_doc == p_idTipoDoc);
        }

        public static IQueryable<tab_doc_input> WhereByIdTipoDoc(this IQueryable<tab_doc_input> p_query, List<int?> p_tipoDocIdList)
        {
            return p_query.Where(d => p_tipoDocIdList.Contains(d.tab_tipo_doc_entrate.id_tipo_doc));
        }

        public static IQueryable<tab_doc_input> WhereByIdentificativo(this IQueryable<tab_doc_input> p_query, string p_identificativo)
        {
            return p_query.Where(d => d.identificativo_doc_input.Trim().Equals(p_identificativo.Trim()));
        }

        public static IQueryable<tab_doc_input> WhereByIdTabDocInputList(this IQueryable<tab_doc_input> p_query, IList<int> p_idTabDocInputList, int p_idTipoDoc)
        {
            return p_query.Where(d => p_idTabDocInputList.Contains(d.id_tab_doc_input) && d.tab_tipo_doc_entrate.id_tipo_doc == p_idTipoDoc);
        }

        public static IQueryable<tab_doc_input> WhereByIdStato(this IQueryable<tab_doc_input> p_query, int p_idStato)
        {
            return p_query.Where(d => d.id_stato == p_idStato);
        }

        public static IQueryable<tab_doc_input> WhereByCodStato(this IQueryable<tab_doc_input> p_query, string codStato)
        {
            return p_query.Where(d => d.cod_stato == codStato);
        }

        public static IQueryable<tab_doc_input> WhereByCodStato(this IQueryable<tab_doc_input> p_query, List<string> p_codStatoList)
        {
            return p_query.Where(d => p_codStatoList.Contains(d.cod_stato));
        }

        public static IQueryable<tab_doc_input> WhereByCodStatoNot(this IQueryable<tab_doc_input> p_query, string codStato)
        {
            return p_query.Where(d => d.cod_stato != codStato);
        }

        public static IQueryable<tab_doc_input> WhereByStatoRicorso(this IQueryable<tab_doc_input> p_query, string p_Stato)
        {
            return p_query.Where(d => d.tab_ricorsi.Count > 0 &&
                                      d.tab_ricorsi.FirstOrDefault().cod_stato.StartsWith(p_Stato));
        }

        public static IQueryable<tab_doc_input> WhereByIdAutoritaGiudiziaria(this IQueryable<tab_doc_input> p_query, int p_idAutoritaGiudiziaria)
        {
            return p_query.Where(d => d.tab_ricorsi.FirstOrDefault() != null &&
                                      d.tab_ricorsi.FirstOrDefault().id_tab_autorita_giudiziaria_ricorso == p_idAutoritaGiudiziaria);
        }
        public static IQueryable<tab_doc_input> WhereBySiglaAutoritaGiudiziaria(this IQueryable<tab_doc_input> p_query, string p_siglaAutoritaGiudiziaria)
        {
            return p_query.Where(d => d.tab_ricorsi.FirstOrDefault() != null &&
                                      d.tab_ricorsi.FirstOrDefault().tab_autorita_giudiziaria.sigla_autorita == p_siglaAutoritaGiudiziaria);
        }
        public static IQueryable<tab_doc_input> WhereBySenzaSentenza(this IQueryable<tab_doc_input> p_query)
        {
            return p_query.Where(d => d.tab_ricorsi.FirstOrDefault() != null &&
                                      d.tab_ricorsi.FirstOrDefault().tab_sentenze.Count == 0);
        }

        public static IQueryable<tab_doc_input> WhereBySenzaSentenzaOConSentenzaAcquisita(this IQueryable<tab_doc_input> p_query)
        {
            return p_query.Where(d => d.tab_ricorsi.FirstOrDefault() != null &&
                                      ((d.tab_ricorsi.FirstOrDefault().tab_sentenze.Count == 0) ||
                                        d.tab_ricorsi.FirstOrDefault().tab_sentenze.FirstOrDefault().cod_stato.Equals(tab_sentenze.ATT_ATT)));
        }

        public static IQueryable<tab_doc_input> WhereByConSentenzaAcquisita(this IQueryable<tab_doc_input> p_query, double p_offset)
        {
            DateTime v_data = DateTime.Now.AddDays(-p_offset);

            return p_query.Where(d => d.tab_ricorsi.FirstOrDefault() != null &&
                                      d.tab_ricorsi.FirstOrDefault().tab_sentenze.FirstOrDefault() != null &&
                                     //!d.tab_ricorsi.FirstOrDefault().id_sentenza_grado_precedente_giudizio.HasValue &&
                                      d.tab_ricorsi.FirstOrDefault().tab_sentenze.FirstOrDefault().cod_stato.Equals(tab_sentenze.ATT_ATT) &&
                                    (
                                     (d.tab_ricorsi.FirstOrDefault().tab_sentenze.FirstOrDefault().data_scadenza_appello.Value < v_data) ||

                                     //d.tab_ricorsi.FirstOrDefault().tab_sentenze.FirstOrDefault().anagrafica_esito_sentenze.sigla == anagrafica_esito_sentenze.RESPINTA ||

                                     (/*d.tab_ricorsi.FirstOrDefault().flag_tipo_ricorrente == tab_ricorsi.SOGGETTO_DEBITORE &&*/
                                      d.tab_ricorsi.FirstOrDefault().tab_sentenze.FirstOrDefault().anagrafica_esito_sentenze.sigla == anagrafica_esito_sentenze.ACCOLTA &&
                                     (d.tab_ricorsi.FirstOrDefault().tab_sentenze.FirstOrDefault().flag_ricorso_appello == null ||
                                      d.tab_ricorsi.FirstOrDefault().tab_sentenze.FirstOrDefault().flag_ricorso_appello == "0")) /*||*/

                                    //((d.tab_ricorsi.FirstOrDefault().flag_tipo_ricorrente == tab_ricorsi.CONCESSIONARIO ||
                                    //  d.tab_ricorsi.FirstOrDefault().flag_tipo_ricorrente == tab_ricorsi.ENTE ||
                                    //  d.tab_ricorsi.FirstOrDefault().flag_tipo_ricorrente == null) &&
                                    //  d.tab_ricorsi.FirstOrDefault().tab_sentenze.FirstOrDefault().flag_ricorso_appello == "0")
                                    )
                                );
        }

        public static IQueryable<tab_doc_input> WhereByConMediazione(this IQueryable<tab_doc_input> p_query)
        {
            return p_query.Where(d => d.tab_ricorsi.FirstOrDefault() != null &&
                                     (d.tab_ricorsi.FirstOrDefault().id_tab_doc_input == d.tab_ricorsi.FirstOrDefault().id_tab_doc_input_mediazione ||
                                      d.tab_ricorsi.FirstOrDefault().flag_richiesta_mediazione == tab_ricorsi.ConMediazione));
        }

        public static IQueryable<tab_doc_input> WhereBySenzaMediazione(this IQueryable<tab_doc_input> p_query)
        {
            return p_query.Where(d => d.tab_ricorsi.FirstOrDefault() != null &&
                                     (d.tab_ricorsi.FirstOrDefault().flag_richiesta_mediazione == null ||
                                      d.tab_ricorsi.FirstOrDefault().flag_richiesta_mediazione == tab_ricorsi.SenzaMediazione));
        }

        public static IQueryable<tab_doc_input> WhereByRGRNull(this IQueryable<tab_doc_input> p_query)
        {
            return p_query.Where(d => d.tab_ricorsi.FirstOrDefault() != null &&
                                      string.IsNullOrEmpty(d.tab_ricorsi.FirstOrDefault().rgr) &&
                                      d.tab_ricorsi.FirstOrDefault().id_tab_doc_input == d.tab_ricorsi.FirstOrDefault().id_tab_doc_input_mediazione);
        }

        public static IQueryable<tab_doc_input> WhereByRGR(this IQueryable<tab_doc_input> p_query, string p_rgr)
        {
            return p_query.Where(d => d.tab_ricorsi.FirstOrDefault() != null &&
                                      !string.IsNullOrEmpty(d.tab_ricorsi.FirstOrDefault().rgr) &&
                                      d.tab_ricorsi.FirstOrDefault().rgr.Equals(p_rgr));
        }

        public static IQueryable<tab_doc_input> WhereByDataScadenza(this IQueryable<tab_doc_input> p_query, double p_offset)
        {
            DateTime v_data = DateTime.Now.AddDays(-p_offset);

            return p_query.Where(d => d.tab_ricorsi.FirstOrDefault() != null &&
                                      d.tab_ricorsi.FirstOrDefault().data_scadenza_mediazione.HasValue &&
                                      DbFunctions.TruncateTime(d.tab_ricorsi.FirstOrDefault().data_scadenza_mediazione.Value) < v_data);
        }

        public static IQueryable<tab_doc_input> WhereByRangeDataPresentazione(this IQueryable<tab_doc_input> p_query, DateTime? daRicorsoPresentazione, DateTime? aRicorsoPresentazione)
        {
            if (daRicorsoPresentazione.HasValue)
            {
                p_query = p_query.Where(d => d.data_presentazione >= daRicorsoPresentazione.Value || !d.data_presentazione.HasValue);
            }

            if (aRicorsoPresentazione.HasValue)
            {
                aRicorsoPresentazione = aRicorsoPresentazione.Value.AddHours(23).AddMinutes(59);
                p_query = p_query.Where(d => d.data_presentazione <= aRicorsoPresentazione.Value || !d.data_presentazione.HasValue);
            }

            return p_query;
        }

        public static IQueryable<tab_doc_input> OrderByDefault(this IQueryable<tab_doc_input> p_query)
        {
            return p_query.OrderBy(o => o.data_esito);
        }

        public static IQueryable<tab_doc_input> OrderByAddettoLavorazioneDataPresentazione(this IQueryable<tab_doc_input> p_query, int p_idRisorsa)
        {
            return p_query.OrderByDescending(d => d.id_addetto_lavorazione == p_idRisorsa).ThenByDescending(d => d.data_presentazione);
        }

        public static IQueryable<tab_doc_input> OrderByDataPresentazioneDesc(this IQueryable<tab_doc_input> p_query)
        {
            return p_query.OrderByDescending(d => d.data_presentazione);
        }

        public static IQueryable<tab_doc_input> OrderByDataPresentazione(this IQueryable<tab_doc_input> p_query)
        {
            return p_query.OrderBy(d => d.data_presentazione);
        }

        public static IQueryable<tab_doc_input> OrderByDataScadenzaControdeduzioni(this IQueryable<tab_doc_input> p_query)
        {
            return p_query.OrderByDescending(d => d.tab_ricorsi.FirstOrDefault().data_scadenza_controdeduzioni_ricorso);
        }

        public static IQueryable<tab_doc_input> OrderByTipoDocDataPresentazione(this IQueryable<tab_doc_input> p_query)
        {
            return p_query.OrderBy(d => d.tab_tipo_doc_entrate.id_tipo_doc).ThenByDescending(d => d.data_presentazione);
        }

        public static IQueryable<tab_doc_input> WhereByDataPresentazioneIsMoreThanDaysOld(this IQueryable<tab_doc_input> p_query, int daysOld, DateTime dtNow)
        {
            return p_query.Where(x => DbFunctions.DiffDays(dtNow.Date, x.data_presentazione) > daysOld);
        }
        public static IQueryable<tab_doc_input> GroupByIdAutoritaGiudiziariaIdTipoDocEntrate(this IQueryable<tab_doc_input> p_query)
        {
            return p_query.GroupBy(p => new { p.id_tipo_doc_entrate, p.id_autorita_giudiziaria }).Select(g => g.FirstOrDefault());
        }
        public static IQueryable<tab_doc_input> OrderByIdAutoritaGiudiziariaIdTipoDocEntrate(this IQueryable<tab_doc_input> p_query)
        {
            return p_query.OrderBy(d => d.id_tipo_doc_entrate).ThenBy(d => d.id_autorita_giudiziaria);
        }
    }
}
