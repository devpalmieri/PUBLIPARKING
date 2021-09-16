using Publisoftware.Data.LinqExtended;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabDocInputBD : EntityBD<tab_doc_input>
    {
        public TabDocInputBD()
        {

        }

        public static new IQueryable<tab_doc_input> GetList(dbEnte dbContext)
        {
            return dbContext.tab_doc_input.Where(d => dbContext.idContribuenteDefaultList.Count == 0 || dbContext.idContribuenteDefaultList.Contains(d.id_contribuente.Value));
        }

        public static new tab_doc_input GetById(Int32 id, dbEnte dbContext)
        {
            return GetList(dbContext).SingleOrDefault(c => c.id_tab_doc_input == id);
        }

        public static IQueryable<tab_doc_input> GetListAnnullamentoRettificaAcquisite(dbEnte dbContext)
        {
            // N.B.: i cod_stato (e corrispondenti id) stanno in "anagrafica_stato_doc"
            return TabDocInputBD.GetList(dbContext)
                .Where(x => x.tab_tipo_doc_entrate.id_tipo_doc == tab_tipo_doc_entrate.TIPO_DOC_ANN_RET) //1
                 .WhereByCodStato(anagrafica_stato_doc.STATO_ACQUISITO) //"ACQ-ACQ"
                                                                        //.WhereByDataPresentazioneIsMoreThanDaysOld(20, dtNow)
                                                                        //.Where(x =>
                                                                        //    !x.join_tab_avv_pag_tab_doc_input.Any(y =>
                                                                        //        y.cod_stato == anagrafica_stato_doc.STATO_ACQUISITO //"ACQ-ACQ"
                                                                        //        || y.cod_stato == anagrafica_stato_doc.STATO_ASSEGNATA_LAVORAZIONE //"ASS-ASS"
                                                                        //        || y.cod_stato.StartsWith(anagrafica_stato_doc.STATO_VERIFICARE) //"VER-"*
                                                                        //        || y.cod_stato.StartsWith(anagrafica_stato_doc.STATO_DEFINITIVO) //"DEF-"*
                                                                        //    )
                                                                        //)
                .Where(x => x.join_tab_avv_pag_tab_doc_input.All(y => y.cod_stato.StartsWith(anagrafica_stato_doc.STATO_LAVORATO) || y.cod_stato.StartsWith(anagrafica_stato_doc.STATO_DEFINITIVO) || y.cod_stato.StartsWith(anagrafica_stato_doc.STATO_ANNULLATO))
                &&
                            x.join_tab_avv_pag_tab_doc_input.Any(y => y.cod_stato.StartsWith(anagrafica_stato_doc.STATO_LAVORATO))
                            )
                .Include(x => x.join_tab_avv_pag_tab_doc_input)
                .Include(x => x.join_tab_avv_pag_tab_doc_input.Select(z => z.tab_avv_pag1));
        }

        public static IQueryable<tab_doc_input> GetTabDocInputAvvocati(int id_tipo_doc_entrate,
                                                                       decimal? importoDa,
                                                                       decimal? importoA,
                                                                       bool isMediazione,
                                                                       int id_ruolo_mansione,
                                                                       dbEnte ctx)
        {
            IQueryable<tab_doc_input> tabDocInputQuery;

            DateTime v_data = DateTime.Now.AddDays(5);

            if (TabTipoDocEntrateBD.GetById(id_tipo_doc_entrate, ctx).id_tipo_doc == tab_tipo_doc_entrate.TIPO_DOC_RICORSI)
            {
                if (id_tipo_doc_entrate == tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_CTPro)
                {
                    if (isMediazione)
                    {
                        tabDocInputQuery = GetList(ctx).Where(x => x.id_tipo_doc_entrate == id_tipo_doc_entrate &&
                                                                   x.tab_autorita_giudiziaria.sigla_autorita == tab_autorita_giudiziaria.SIGLA_AUTORITA_CTP &&
                                                                  !x.id_addetto_lavorazione.HasValue &&
                                                                   x.tab_ricorsi.FirstOrDefault().importo_riferimento_ricorso > (importoDa != null ? importoDa.Value : 0) &&
                                                                   x.tab_ricorsi.FirstOrDefault().importo_riferimento_ricorso <= (importoA != null ? importoA.Value : decimal.MaxValue) &&
                                                                   string.IsNullOrEmpty(x.tab_ricorsi.FirstOrDefault().rgr) &&
                                                                   x.tab_ricorsi.FirstOrDefault().flag_richiesta_mediazione == "1" &&
                                                                   x.tab_ricorsi.FirstOrDefault().id_tab_doc_input == x.tab_ricorsi.FirstOrDefault().id_tab_doc_input_mediazione &&
                                                                   x.tab_ricorsi.FirstOrDefault().data_scadenza_mediazione.HasValue &&
                                                                   DbFunctions.TruncateTime(v_data) < DbFunctions.TruncateTime(x.tab_ricorsi.FirstOrDefault().data_scadenza_mediazione) &&
                                                                  (x.cod_stato == anagrafica_stato_doc.STATO_ACQUISITO));
                    }
                    else
                    {
                        tabDocInputQuery = GetList(ctx).Where(x => x.id_tipo_doc_entrate == id_tipo_doc_entrate &&
                                                                   x.tab_autorita_giudiziaria.sigla_autorita == tab_autorita_giudiziaria.SIGLA_AUTORITA_CTP &&
                                                                  !x.id_addetto_lavorazione.HasValue &&
                                                                   x.tab_ricorsi.FirstOrDefault().importo_riferimento_ricorso > (importoDa != null ? importoDa.Value : 0) &&
                                                                   x.tab_ricorsi.FirstOrDefault().importo_riferimento_ricorso <= (importoA != null ? importoA.Value : decimal.MaxValue) &&
                                                                  (!string.IsNullOrEmpty(x.tab_ricorsi.FirstOrDefault().rgr) || x.tab_ricorsi.FirstOrDefault().importo_riferimento_ricorso > x.tab_tipo_doc_entrate.importo_massimo_mediazione) &&
                                                                  (x.tab_ricorsi.FirstOrDefault().flag_richiesta_mediazione == "0" || string.IsNullOrEmpty(x.tab_ricorsi.FirstOrDefault().flag_richiesta_mediazione)) &&
                                                                 (!x.tab_ricorsi.FirstOrDefault().id_tab_doc_input_mediazione.HasValue || x.tab_ricorsi.FirstOrDefault().id_tab_doc_input != x.tab_ricorsi.FirstOrDefault().id_tab_doc_input_mediazione) &&
                                                                 ((x.tab_ricorsi.FirstOrDefault().data_scadenza_memorie_ricorso.HasValue &&
                                                                   DbFunctions.TruncateTime(v_data) < DbFunctions.TruncateTime(x.tab_ricorsi.FirstOrDefault().data_scadenza_memorie_ricorso)) ||
                                                                  (x.tab_ricorsi.FirstOrDefault().data_scadenza_controdeduzioni_ricorso.HasValue &&
                                                                   DbFunctions.TruncateTime(v_data) < DbFunctions.TruncateTime(x.tab_ricorsi.FirstOrDefault().data_scadenza_controdeduzioni_ricorso))) &&
                                                                  (x.cod_stato == anagrafica_stato_doc.STATO_ACQUISITO || x.cod_stato == anagrafica_stato_doc.STATO_DEF_ACQ));
                    }
                }
                else if (id_tipo_doc_entrate == tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_GDP ||
                         id_tipo_doc_entrate == tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_CITAZIONE_GDP)
                {
                    tabDocInputQuery = GetList(ctx).Where(x => x.id_tipo_doc_entrate == id_tipo_doc_entrate &&
                                                               x.tab_autorita_giudiziaria.sigla_autorita == tab_autorita_giudiziaria.SIGLA_AUTORITA_GDP &&
                                                              !x.id_addetto_lavorazione.HasValue &&
                                                               x.tab_ricorsi.FirstOrDefault().importo_riferimento_ricorso > (importoDa != null ? importoDa.Value : 0) &&
                                                               x.tab_ricorsi.FirstOrDefault().importo_riferimento_ricorso <= (importoA != null ? importoA.Value : decimal.MaxValue) &&
                                                               x.tab_ricorsi.FirstOrDefault().data_scadenza_memorie_ricorso.HasValue &&
                                                               DbFunctions.TruncateTime(v_data) < DbFunctions.TruncateTime(x.tab_ricorsi.FirstOrDefault().data_scadenza_memorie_ricorso) &&
                                                               x.cod_stato == anagrafica_stato_doc.STATO_ACQUISITO);
                }
                else
                {
                    tabDocInputQuery = GetList(ctx).Where(x => x.id_tipo_doc_entrate == id_tipo_doc_entrate &&
                                                              !x.id_addetto_lavorazione.HasValue &&
                                                               x.tab_ricorsi.FirstOrDefault().importo_riferimento_ricorso > (importoDa != null ? importoDa.Value : 0) &&
                                                               x.tab_ricorsi.FirstOrDefault().importo_riferimento_ricorso <= (importoA != null ? importoA.Value : decimal.MaxValue) &&
                                                             ((x.tab_ricorsi.FirstOrDefault().data_scadenza_memorie_ricorso.HasValue &&
                                                               DbFunctions.TruncateTime(v_data) < DbFunctions.TruncateTime(x.tab_ricorsi.FirstOrDefault().data_scadenza_memorie_ricorso)) ||
                                                              (x.tab_ricorsi.FirstOrDefault().data_scadenza_costituzione_giudizio_resistente.HasValue &&
                                                               DbFunctions.TruncateTime(v_data) < DbFunctions.TruncateTime(x.tab_ricorsi.FirstOrDefault().data_scadenza_costituzione_giudizio_resistente)) ||
                                                              (x.tab_ricorsi.FirstOrDefault().data_scadenza_costituzione_giudizio_ricorrente.HasValue &&
                                                               DbFunctions.TruncateTime(v_data) < DbFunctions.TruncateTime(x.tab_ricorsi.FirstOrDefault().data_scadenza_costituzione_giudizio_ricorrente))) &&
                                                               x.cod_stato == anagrafica_stato_doc.STATO_ACQUISITO);
                }
            }
            else if (TabTipoDocEntrateBD.GetById(id_tipo_doc_entrate, ctx).id_tipo_doc == tab_tipo_doc_entrate.TIPO_DOC_CITAZIONI)
            {
                if (id_ruolo_mansione == anagrafica_ruolo_mansione.COD_RUOLO_MANSIONE_UFFICIALE_RISCOSSIONE_ID)
                {
                    tabDocInputQuery = GetList(ctx).Where(x => x.id_tipo_doc_entrate == id_tipo_doc_entrate &&
                                                               x.tab_autorita_giudiziaria.sigla_autorita == tab_autorita_giudiziaria.SIGLA_AUTORITA_TRIB &&
                                                               x.cod_stato == anagrafica_stato_doc.STATO_ACQUISITO &&
                                                               x.tab_citazioni.FirstOrDefault().id_ufficiale_riscossione == null &&
                                                              !x.tab_citazioni.FirstOrDefault().tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO));
                }
                else
                {
                    tabDocInputQuery = GetList(ctx).Where(x => x.id_tipo_doc_entrate == id_tipo_doc_entrate &&
                                                               x.tab_autorita_giudiziaria.sigla_autorita == tab_autorita_giudiziaria.SIGLA_AUTORITA_TRIB &&
                                                               x.cod_stato == anagrafica_stato_doc.STATO_ACQUISITO &&
                                                              !x.id_addetto_lavorazione.HasValue &&
                                                              !x.tab_citazioni.FirstOrDefault().tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO));
                }
            }
            else
            {
                tabDocInputQuery = GetList(ctx).Where(x => x.id_tipo_doc_entrate == id_tipo_doc_entrate &&
                                                           x.id_autorita_giudiziaria != null &&
                                                          !x.id_addetto_lavorazione.HasValue &&
                                                           x.cod_stato == anagrafica_stato_doc.STATO_ACQUISITO);
            }

            return tabDocInputQuery;
        }
    }
}
