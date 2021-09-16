using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabCitazioniLinq
    {
        public static IQueryable<tab_citazioni> GroupPerComuneCap(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.GroupBy(p => new { p.cod_comune_soggetto_debitore, p.cap_soggetto_debitore }).Select(g => g.FirstOrDefault());
        }

        public static IQueryable<tab_citazioni> WhereByIdAutorita(this IQueryable<tab_citazioni> p_query, int p_idAutorita)
        {
            return p_query.Where(d => d.id_tab_autorita_giudiziaria == p_idAutorita);
        }

        public static IQueryable<tab_citazioni> WhereByCodStato(this IQueryable<tab_citazioni> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<tab_citazioni> WhereByCodStatoNot(this IQueryable<tab_citazioni> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<tab_citazioni> WhereByAvvisoCodStato(this IQueryable<tab_citazioni> p_query, string p_codStato)
        {
            return p_query.Where(d => d.tab_avv_pag.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<tab_citazioni> WhereByAvvisoCodStatoNot(this IQueryable<tab_citazioni> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.tab_avv_pag.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<tab_citazioni> WhereByAvvisoCodStatoContains(this IQueryable<tab_citazioni> p_query, string p_codStato)
        {
            return p_query.Where(d => d.tab_avv_pag.cod_stato.Contains(p_codStato));
        }

        public static IQueryable<tab_citazioni> WhereByAvvisoCodStatoContainsNot(this IQueryable<tab_citazioni> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.tab_avv_pag.cod_stato.Contains(p_codStato));
        }

        public static IQueryable<tab_citazioni> WhereByAvvisoIdTipoServizio(this IQueryable<tab_citazioni> p_query, int p_idServizio)
        {
            return p_query.Where(d => d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == p_idServizio);
        }

        public static IQueryable<tab_citazioni> WhereByAvvisoFlagEsitoSpedNot(this IQueryable<tab_citazioni> p_query, string p_flag)
        {
            return p_query.Where(d => d.tab_avv_pag.flag_esito_sped_notifica == p_flag);
        }

        public static IQueryable<tab_citazioni> WhereByIdUfficialeRiscossione(this IQueryable<tab_citazioni> p_query, int p_idRisorsa)
        {
            return p_query.Where(d => d.id_ufficiale_riscossione == p_idRisorsa || d.id_risorsa_procuratore_1 == p_idRisorsa);
        }

        public static IQueryable<tab_citazioni> WhereByIdUfficialeRiscossioneOrIdProcuratore1(this IQueryable<tab_citazioni> p_query, int p_idRisorsa)
        {
            return p_query.Where(d => d.id_risorsa_procuratore_1 == p_idRisorsa);
        }

        public static IQueryable<tab_citazioni> WhereByIdProcuratore1(this IQueryable<tab_citazioni> p_query, int p_idRisorsa)
        {
            return p_query.Where(d => d.id_ufficiale_riscossione == p_idRisorsa);
        }

        public static IQueryable<tab_citazioni> WhereByDataRestituzioneNull(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => d.data__restituzione_ufficiale_giudiziario == null);
        }

        public static IQueryable<tab_citazioni> WhereByDataRestituzioneNotNull(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => d.data__restituzione_ufficiale_giudiziario != null);
        }

        public static IQueryable<tab_citazioni> WhereByDataCaricamentoRuoloNull(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => d.data_caricamento_ruolo == null);
        }

        public static IQueryable<tab_citazioni> WhereByDataIscrizioneRuoloNull(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => d.data_iscrizione_ruolo == null);
        }

        public static IQueryable<tab_citazioni> WhereByDataCaricamentoRuoloNotNull(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => d.data_caricamento_ruolo != null);
        }

        public static IQueryable<tab_citazioni> WhereByDataIscrizioneRuoloIsNull(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => d.data_iscrizione_ruolo == null);
        }

        public static IQueryable<tab_citazioni> WhereByNumeroRegistro(this IQueryable<tab_citazioni> p_query, string p_RGE)
        {
            return p_query.Where(d => d.numero_registro_iscrizione_ruolo == p_RGE);
        }

        public static IQueryable<tab_citazioni> WhereByNumeroRegistroIsNull(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => d.numero_registro_iscrizione_ruolo == null);
        }

        public static IQueryable<tab_citazioni> WhereByIdTabAutoritaGiudiziariaNotNull(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => d.id_tab_autorita_giudiziaria != null);
        }

        public static IQueryable<tab_citazioni> WhereByIdAvvPagCitazione(this IQueryable<tab_citazioni> p_query, int p_idAvvPag)
        {
            return p_query.Where(d => d.id_avv_pag_citazione == p_idAvvPag);
        }

        public static IQueryable<tab_citazioni> WhereByIdContribuente(this IQueryable<tab_citazioni> p_query, decimal p_idContribuente)
        {
            return p_query.Where(d => d.id_contribuente == p_idContribuente);
        }

        public static IQueryable<tab_citazioni> WhereByFlagIscrizioneRuoloNull(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => string.IsNullOrEmpty(d.flag_iscrizione_ruolo) ||
                                      d.flag_iscrizione_ruolo == "0");
        }

        public static IQueryable<tab_citazioni> WhereByFlagIscrizioneRuoloNotNull(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => !string.IsNullOrEmpty(d.flag_iscrizione_ruolo) &&
                                      d.flag_iscrizione_ruolo != "0");
        }

        public static IQueryable<tab_citazioni> WhereByFlagIscrizioneRuolo(this IQueryable<tab_citazioni> p_query, string p_flag)
        {
            return p_query.Where(d => !string.IsNullOrEmpty(d.flag_iscrizione_ruolo) &&
                                      d.flag_iscrizione_ruolo == p_flag);
        }

        public static IQueryable<tab_citazioni> WhereByFlagRichiestaEstinzioneNull(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => string.IsNullOrEmpty(d.flag_richiesta_estinzione_procedura_esecutiva) ||
                                      d.flag_richiesta_estinzione_procedura_esecutiva == "0");
        }

        public static IQueryable<tab_citazioni> WhereByFlagRichiestaEstinzioneNotNull(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => !string.IsNullOrEmpty(d.flag_richiesta_estinzione_procedura_esecutiva) &&
                                      d.flag_richiesta_estinzione_procedura_esecutiva != "0");
        }

        public static IQueryable<tab_citazioni> WhereByFlagRichiestaEstinzione(this IQueryable<tab_citazioni> p_query, string p_flag)
        {
            return p_query.Where(d => !string.IsNullOrEmpty(d.flag_richiesta_estinzione_procedura_esecutiva) &&
                                      d.flag_richiesta_estinzione_procedura_esecutiva == p_flag);
        }

        public static IQueryable<tab_citazioni> WhereByFlagCaricamentoRichiestaEstinzioneNull(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => string.IsNullOrEmpty(d.flag_caricamento_ruolo_richiesta_estinzione_procedura_esecutiva) ||
                                      d.flag_caricamento_ruolo_richiesta_estinzione_procedura_esecutiva == "0");
        }

        public static IQueryable<tab_citazioni> WhereByFlagCaricamentoRichiestaEstinzioneNotNull(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => !string.IsNullOrEmpty(d.flag_caricamento_ruolo_richiesta_estinzione_procedura_esecutiva) &&
                                      d.flag_caricamento_ruolo_richiesta_estinzione_procedura_esecutiva != "0");
        }

        public static IQueryable<tab_citazioni> WhereByFlagCaricamentoRichiestaEstinzione(this IQueryable<tab_citazioni> p_query, string p_flag)
        {
            return p_query.Where(d => !string.IsNullOrEmpty(d.flag_caricamento_ruolo_richiesta_estinzione_procedura_esecutiva) &&
                                      d.flag_caricamento_ruolo_richiesta_estinzione_procedura_esecutiva == p_flag);
        }

        public static IQueryable<tab_citazioni> WhereByFlagAssegnazioneSommeNull(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => string.IsNullOrEmpty(d.flag_assegnazione_somme) ||
                                      d.flag_assegnazione_somme == "0");
        }

        public static IQueryable<tab_citazioni> WhereByFlagAssegnazioneSommeNotNull(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => !string.IsNullOrEmpty(d.flag_assegnazione_somme) &&
                                      d.flag_assegnazione_somme != "0");
        }

        public static IQueryable<tab_citazioni> WhereByFlagAssegnazioneSomme(this IQueryable<tab_citazioni> p_query, string p_flag)
        {
            return p_query.Where(d => !string.IsNullOrEmpty(d.flag_assegnazione_somme) &&
                                      d.flag_assegnazione_somme == p_flag);
        }

        public static IQueryable<tab_citazioni> WhereByFlagEstinzioneNull(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => string.IsNullOrEmpty(d.flag_estinzione_citazione) ||
                                      d.flag_estinzione_citazione == "0");
        }

        public static IQueryable<tab_citazioni> WhereByFlagEstinzioneNotNull(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => !string.IsNullOrEmpty(d.flag_estinzione_citazione) &&
                                      d.flag_estinzione_citazione != "0");
        }

        public static IQueryable<tab_citazioni> WhereByFlagEstinzione(this IQueryable<tab_citazioni> p_query, string p_flag)
        {
            return p_query.Where(d => !string.IsNullOrEmpty(d.flag_estinzione_citazione) &&
                                      d.flag_estinzione_citazione == p_flag);
        }

        public static IQueryable<tab_citazioni> WhereByFlagOrdinanzaMancataDichiarazioneNull(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => string.IsNullOrEmpty(d.flag_ordinanza_mancata_dichiarazione) ||
                                      d.flag_ordinanza_mancata_dichiarazione == "0");
        }

        public static IQueryable<tab_citazioni> WhereByFlagOrdinanzaMancataDichiarazioneNotNull(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => !string.IsNullOrEmpty(d.flag_ordinanza_mancata_dichiarazione) &&
                                      d.flag_ordinanza_mancata_dichiarazione != "0");
        }

        public static IQueryable<tab_citazioni> WhereByFlagOrdinanzaMancataDichiarazione(this IQueryable<tab_citazioni> p_query, string p_flag)
        {
            return p_query.Where(d => !string.IsNullOrEmpty(d.flag_ordinanza_mancata_dichiarazione) &&
                                      d.flag_ordinanza_mancata_dichiarazione == p_flag);
        }

        public static IQueryable<tab_citazioni> WhereByFlagOrdinanzaRinvioUdienzaNull(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => string.IsNullOrEmpty(d.flag_ordinanza_rinvio_udienza) ||
                                      d.flag_ordinanza_rinvio_udienza == "0");
        }

        public static IQueryable<tab_citazioni> WhereByFlagOrdinanzaRinvioUdienzaNotNull(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => !string.IsNullOrEmpty(d.flag_ordinanza_rinvio_udienza) &&
                                      d.flag_ordinanza_rinvio_udienza != "0");
        }

        public static IQueryable<tab_citazioni> WhereByFlagOrdinanzaRinvioUdienza(this IQueryable<tab_citazioni> p_query, string p_flag)
        {
            return p_query.Where(d => !string.IsNullOrEmpty(d.flag_ordinanza_rinvio_udienza) &&
                                      d.flag_ordinanza_rinvio_udienza == p_flag);
        }

        public static IQueryable<tab_citazioni> WhereByFlagOrdinanzaRinotificaNull(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => string.IsNullOrEmpty(d.flag_ordinanza_rinotifica_citazione) ||
                                      d.flag_ordinanza_rinotifica_citazione == "0");
        }

        public static IQueryable<tab_citazioni> WhereByFlagOrdinanzaRinotificaNotNull(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => !string.IsNullOrEmpty(d.flag_ordinanza_rinotifica_citazione) &&
                                      d.flag_ordinanza_rinotifica_citazione != "0");
        }

        public static IQueryable<tab_citazioni> WhereByFlagOrdinanzaRinotifica(this IQueryable<tab_citazioni> p_query, string p_flag)
        {
            return p_query.Where(d => !string.IsNullOrEmpty(d.flag_ordinanza_rinotifica_citazione) &&
                                      d.flag_ordinanza_rinotifica_citazione == p_flag);
        }

        public static IQueryable<tab_citazioni> WhereByFlagOrdinanzaFissazionePrimaUdienzaNull(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => string.IsNullOrEmpty(d.flag_ordinanza_fissazione_prima_udienza) ||
                                      d.flag_ordinanza_fissazione_prima_udienza == "0");
        }

        public static IQueryable<tab_citazioni> WhereByFlagOrdinanzaFissazionePrimaUdienzaNotNull(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => !string.IsNullOrEmpty(d.flag_ordinanza_fissazione_prima_udienza) &&
                                      d.flag_ordinanza_fissazione_prima_udienza != "0");
        }

        public static IQueryable<tab_citazioni> WhereByFlagOrdinanzaFissazionePrimaUdienza(this IQueryable<tab_citazioni> p_query, string p_flag)
        {
            return p_query.Where(d => !string.IsNullOrEmpty(d.flag_ordinanza_fissazione_prima_udienza) &&
                                      d.flag_ordinanza_fissazione_prima_udienza == p_flag);
        }

        public static IQueryable<tab_citazioni> WhereByFlagOrdinanzaNotNull(this IQueryable<tab_citazioni> p_query, string p_tipoOrdinanza)
        {
            switch (p_tipoOrdinanza)
            {
                case tab_citazioni.ORDINANZA_ASSEGNAZIONE:
                    return p_query.Where(d => !string.IsNullOrEmpty(d.flag_assegnazione_somme) &&
                                              d.flag_assegnazione_somme != "0");
                case tab_citazioni.ORDINANZA_ESTINZIONE:
                    return p_query.Where(d => !string.IsNullOrEmpty(d.flag_estinzione_citazione) &&
                                              d.flag_estinzione_citazione != "0");
                case tab_citazioni.ORDINANZA_MANCATA_DICHIARAZIONE:
                    return p_query.Where(d => !string.IsNullOrEmpty(d.flag_ordinanza_mancata_dichiarazione) &&
                                              d.flag_ordinanza_mancata_dichiarazione != "0");
                case tab_citazioni.ORDINANZA_RINVIO_UDIENZA:
                    return p_query.Where(d => !string.IsNullOrEmpty(d.flag_ordinanza_rinvio_udienza) &&
                                              d.flag_ordinanza_rinvio_udienza != "0");
                case tab_citazioni.ORDINANZA_RINOTIFICA:
                    return p_query.Where(d => !string.IsNullOrEmpty(d.flag_ordinanza_rinotifica_citazione) &&
                                              d.flag_ordinanza_rinotifica_citazione != "0");
                case tab_citazioni.ORDINANZA_FISSAZIONE_PRIMA_UDIENZA:
                    return p_query.Where(d => !string.IsNullOrEmpty(d.flag_ordinanza_fissazione_prima_udienza) &&
                                              d.flag_ordinanza_fissazione_prima_udienza != "0");
                default:
                    return p_query;
            }
        }

        public static IQueryable<tab_citazioni> WhereByImporto(this IQueryable<tab_citazioni> p_query, decimal p_importo)
        {
            return p_query.Where(d => Math.Abs((d.tab_avv_pag.imp_tot_avvpag.HasValue ? d.tab_avv_pag.imp_tot_avvpag.Value : 0) - p_importo) <= 1 &&
                                      Math.Abs((d.tab_avv_pag.imp_tot_avvpag.HasValue ? d.tab_avv_pag.imp_tot_avvpag.Value : 0) - p_importo) >= 0);
            //return p_query.Where(d => d.tab_avv_pag.imp_tot_avvpag.Value == p_importo);
        }

        public static IQueryable<tab_citazioni> WhereByDataPrimaUdienzaMinUgu(this IQueryable<tab_citazioni> p_query, DateTime p_data)
        {
            return p_query.Where(d => d.data_prima_udienza <= p_data);
        }

        public static IQueryable<tab_citazioni> WhereByAvvRifSenzaImmagineAvvisoPerSpedNotUffRiscossione(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => d.tab_avv_pag.tab_fascicolo_avvpag_allegati
                                                   .Where(x => x.tab_sped_not.flag_soggetto_debitore == tab_sped_not.FLAG_SOGGETTO_DEBITORE_UFFICIALE_RISCOSSIONE)
                                                   .FirstOrDefault() == null ||
                                     (d.tab_avv_pag.tab_fascicolo_avvpag_allegati
                                                   .Where(x => x.tab_sped_not.flag_soggetto_debitore == tab_sped_not.FLAG_SOGGETTO_DEBITORE_UFFICIALE_RISCOSSIONE)
                                                   .FirstOrDefault() != null &&
                                      d.tab_avv_pag.tab_fascicolo_avvpag_allegati
                                                   .Where(x => x.tab_sped_not.flag_soggetto_debitore == tab_sped_not.FLAG_SOGGETTO_DEBITORE_UFFICIALE_RISCOSSIONE)
                                                   .FirstOrDefault()
                                                   .join_documenti_fascicolo_avvpag_allegati
                                                   .Where(y => y.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_AVVISO_RIF).Count() == 0));
        }

        public static IQueryable<tab_citazioni> WhereByAvvRifConImmagineAvvisoPerSpedNotUffRiscossione(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => (d.tab_avv_pag.tab_fascicolo_avvpag_allegati
                                                    .Where(x => x.tab_sped_not.flag_soggetto_debitore == tab_sped_not.FLAG_SOGGETTO_DEBITORE_UFFICIALE_RISCOSSIONE)
                                                    .FirstOrDefault() != null &&
                                       d.tab_avv_pag.tab_fascicolo_avvpag_allegati
                                                    .Where(x => x.tab_sped_not.flag_soggetto_debitore == tab_sped_not.FLAG_SOGGETTO_DEBITORE_UFFICIALE_RISCOSSIONE)
                                                    .FirstOrDefault()
                                                    .join_documenti_fascicolo_avvpag_allegati
                                                    .Where(y => y.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_AVVISO_RIF).Count() > 0));
        }

        public static IQueryable<tab_citazioni> WhereByAvvRifConImmagineNotificheTrannePerSpedNotUffRiscossione(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => (d.tab_avv_pag.tab_fascicolo_avvpag_allegati
                                                    .Where(x => x.tab_sped_not.flag_soggetto_debitore != tab_sped_not.FLAG_SOGGETTO_DEBITORE_UFFICIALE_RISCOSSIONE)
                                                    .FirstOrDefault() != null &&
                                       d.tab_avv_pag.tab_fascicolo_avvpag_allegati
                                                    .Where(x => x.tab_sped_not.flag_soggetto_debitore != tab_sped_not.FLAG_SOGGETTO_DEBITORE_UFFICIALE_RISCOSSIONE)
                                                    .All(z => z.join_documenti_fascicolo_avvpag_allegati.Where(y => y.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_NOTIFICA_RELATA).Count() > 0)));
        }

        public static IQueryable<tab_citazioni> WhereByAvvisoNonInFascicoliConRinunciaEsecuzione(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => !d.tab_avv_pag.tab_fascicolo_avvpag_allegati.Any(x => x.tab_fascicolo.join_documenti_fascicolo.Any(z => z.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_ESTINZIONE_PROCEDURA_ESECUTIVA)));
        }

        public static IQueryable<tab_citazioni> WhereByFascicoloConRinunciaEsecuzione(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.Where(d => d.tab_doc_input.tab_fascicolo.FirstOrDefault() != null &&
                                      d.tab_doc_input.tab_fascicolo.FirstOrDefault().join_documenti_fascicolo.Any(z => z.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_ESTINZIONE_PROCEDURA_ESECUTIVA));
        }

        public static IQueryable<tab_citazioni> OrderByAvvisoDataEmissione(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.OrderBy(d => d.tab_avv_pag.dt_emissione);
        }

        public static IQueryable<tab_citazioni> OrderByAvvisoDataEmissioneDesc(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.OrderByDescending(d => d.tab_avv_pag.dt_emissione);
        }

        public static IQueryable<tab_citazioni> OrderByDataPrimaUdienza(this IQueryable<tab_citazioni> p_query)
        {
            return p_query.OrderBy(d => d.data_prima_udienza);
        }
    }
}
