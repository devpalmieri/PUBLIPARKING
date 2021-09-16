using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    // TEST: DB Formazione (10.2.1.196\db_generale_formazione)
    //       contribuente n. 42866 - DMZFNC49S23B963N
    //       pagina: http://localhost:51592/portale/P0110F0010A0140 (Istanze Atti Coattivi -> Istanze Annullamento/Rettifica -> Present. istanze di annul./rett.)
    //       Aggiustare dati "soggetto debitore" per "Tipo avviso: 5511 / Atto di pignoramento di crediti del debitore presso terzi"!
    /*
     * TabAvvPagLinq.cs
     * 
     * 
        public static IList<tab_avv_pag_dto> ToLightNew(this IQueryable<tab_avv_pag> iniziale)
        {
            var ttt = iniziale.Take(3).ToList();
            foreach (var t in ttt)
            {
                if (t.id_tab_avv_pag == 2932152)
                {
                    ;
                    if (t.join_avv_pag_soggetto_debitore.Count > 0)
                    {
                        var y = t.join_avv_pag_soggetto_debitore.First();
                        var cod_stato = y.cod_stato;
                        var id_terzo_debitore = y.id_terzo_debitore;
                        var join_referente_contribuente = new tab_avv_pag_dto.join_avv_pag_soggetto_debitore_dto.join_referente_contribuente_dto
                        {
                            anagrafica_tipo_relazione = new tab_avv_pag_dto.anagrafica_tipo_relazione_dto
                            {
                                desc_tipo_relazione = y.join_referente_contribuente?.anagrafica_tipo_relazione?.desc_tipo_relazione
                            },
                            tab_referente = new tab_avv_pag_dto.join_avv_pag_soggetto_debitore_dto.join_referente_contribuente_dto.tab_referente_dto
                            {
                                anagrafica_tipo_contribuente = new tab_avv_pag_dto.anagrafica_tipo_contribuente_dto
                                {
                                    sigla_tipo_contribuente = y.join_referente_contribuente?.tab_referente?.anagrafica_tipo_contribuente?.sigla_tipo_contribuente
                                },
                                cod_fiscale = y.join_referente_contribuente?.tab_referente?.cod_fiscale,
                                cognome = y.join_referente_contribuente?.tab_referente.cognome,
                                denominazione_commerciale = y.join_referente_contribuente?.tab_referente?.denominazione_commerciale,
                                nome = y.join_referente_contribuente?.tab_referente.nome,
                                p_iva = y.join_referente_contribuente?.tab_referente?.p_iva,
                                rag_sociale = y.join_referente_contribuente?.tab_referente?.rag_sociale,
                                tab_contribuente = new tab_avv_pag_dto.tab_contribuente_dto
                                {
                                    anagrafica_tipo_contribuente = new tab_avv_pag_dto.anagrafica_tipo_contribuente_dto
                                    {
                                        sigla_tipo_contribuente = y.join_referente_contribuente?.tab_referente?.tab_contribuente?.anagrafica_tipo_contribuente?.sigla_tipo_contribuente
                                    },
                                    cod_fiscale = y.join_referente_contribuente?.tab_referente?.tab_contribuente?.cod_fiscale,
                                    cognome = y.join_referente_contribuente?.tab_referente?.tab_contribuente?.cognome,
                                    denominazione_commerciale = y.join_referente_contribuente?.tab_referente?.tab_contribuente?.denominazione_commerciale,
                                    nome = y.join_referente_contribuente?.tab_referente?.tab_contribuente?.nome,
                                    p_iva = y.join_referente_contribuente?.tab_referente?.tab_contribuente?.p_iva,
                                    rag_sociale = y.join_referente_contribuente?.tab_referente?.tab_contribuente?.rag_sociale,
                                }
                            }
                        };
                    }
                }
            }

            var retlist = iniziale.Select(d => new tab_avv_pag_dto
            {
                // -----------------------------------------
                // NumeroIstanza = string.Empty,
                // TipoIstanza = string.Empty,
                // DataPresentazione = string.Empty,
                // -----------------------------------------

                id_tab_avv_pag = d.id_tab_avv_pag,
                id_tipo_avvpag = d.id_tipo_avvpag,
                Identificativo = d.identificativo_avv_pag,
                NumeroAvviso = d.identificativo_avv_pag,
                dt_emissione = d.dt_emissione, // -> dt_emissione_String 
                imp_tot_avvpag_rid_orig = d.imp_tot_avvpag_rid, //-> imp_tot_avvpag_rid, Importo
                imp_tot_avvpag = d.imp_tot_avvpag, // -> imp_tot_avvpag_Euro

                // -------------------------------------------------
                // importoSpeseNotificaDecimal, importoSpeseCoattiveDecimal, importoSpeseCoattiveDecimal, ImportoSpeseNotifica, ImportoSpeseCoattive
                // -
                // importoSpeseNotificaDecimal = d.tab_contribuzione.Where(x => (x.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_NOT || x.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_SPE) && d.tab_contribuzione1.Select(y => y.id_avv_pag).ToList().Contains(x.id_avv_pag_iniziale.HasValue ? x.id_avv_pag_iniziale.Value : -1)).Sum(x => x.importo_residuo),
                // importoSpeseCoattiveDecimal = d.importoSpeseCoattiveDecimal,
                // ImportoSpeseNotifica = d.ImportoSpeseNotifica,
                // ImportoSpeseCoattive = d.ImportoSpeseCoattive,
                anagrafica_tipo_avv_pag_id_servizio = d.anagrafica_tipo_avv_pag.id_servizio,
                tab_contribuzione = d.tab_contribuzione.Select(y =>
                    new tab_avv_pag_dto.tab_contribuzione_dto
                    {
                        tab_tipo_voce_contribuzione_codice_tributo_ministeriale = y.tab_tipo_voce_contribuzione.codice_tributo_ministeriale
                    }).ToList(),
                tab_contribuzione1 = d.tab_contribuzione1.Select(y =>
                    new tab_avv_pag_dto.tab_contribuzione_dto1
                    {
                        id_avv_pag = y.id_avv_pag
                    }).ToList(),
                // -
                // -------------------------------------------------

                // -------------------------------------------------
                // Rate = d.Rate,
                flag_rateizzazione_bis = d.flag_rateizzazione_bis,
                num_rate_bis = d.num_rate_bis,
                num_rate = d.num_rate,
                // -------------------------------------------------

                Targa = d.flag_iter_recapito_notifica,

                // -------------------------------------------------
                // SpeditoNotificato = d.SpeditoNotificato,
                data_ricezione = d.data_ricezione,
                flag_spedizione_notifica = d.flag_spedizione_notifica,
                flag_esito_sped_notifica = d.flag_esito_sped_notifica,
                data_avvenuta_notifica = d.data_avvenuta_notifica,
                // -------------------------------------------------

                // -------------------------------------------------
                imp_tot_pagato_orig = d.imp_tot_pagato, // -> imp_tot_pagato è nullable in tab_avv_pag non nullable in tab_avv_pag_light!
                // -> imp_tot_pagato_Euro,
                importo_tot_da_pagare_orig = d.importo_tot_da_pagare, // -> importo_tot_da_pagare, ImportoDaPagare = d.ImportoDaPagare,
                                                                      // -------------------------------------------------

                // --------------------------------------------------------------------
                //stato = d.stato,
                //cod_stato = d.anagrafica_stato.cod_stato_riferimento,
                anagrafica_stato = new tab_avv_pag_dto.anagrafica_stato_dto
                {
                    desc_stato_riferimento = d.anagrafica_stato.desc_stato_riferimento
                },
                codStatoReale = d.cod_stato,
                // --------------------------------------------------------------------

                // --------------------------------------------------------------------
                // Adesione = d.Adesione,
                flag_adesione = d.flag_adesione,
                data_adesione = d.data_adesione,
                // --------------------------------------------------------------------

                // --------------------------------------------------------------------
                //TipoBene = d.TipoBene, //=="OBSOLETO"
                id_tab_supervisione_finale = d.id_tab_supervisione_finale != null ? d.id_tab_supervisione_finale.Value : -1,
                IntimazioneCorrelata = String.Empty, // -> d.IntimazioneCorrelata,
                importo_ridotto = d.importo_ridotto, // -> impRidottoPerAdesione = d.impRidottoPerAdesione,
                // --------------------------------------------------------------------

                // --------------------------------------------------------------------
                //    TAB_SUPERVISIONE_FINALE_V21= d.TAB_SUPERVISIONE_FINALE_V21, //->id_avvpag_preavviso_collegato
                id_avvpag_preavviso_collegato =
                    (d.TAB_SUPERVISIONE_FINALE_V21 != null && d.TAB_SUPERVISIONE_FINALE_V21.id_avvpag_preavviso_collegato.HasValue)
                        ? d.TAB_SUPERVISIONE_FINALE_V21.id_avvpag_preavviso_collegato.Value : -1,// d.TAB_SUPERVISIONE_FINALE_V21!=null ? d.TAB_SUPERVISIONE_FINALE_V21.id_avvpag_preavviso_collegato :null,
                // --------------------------------------------------------------------

                // --------------------------------------------------------------------
                IsIstanzaVisibile = d.join_tab_avv_pag_tab_doc_input.Count > 0,

                data_avviso_bonario = d.data_avviso_bonario,
                dati_avviso_bonario = d.dati_avviso_bonario,
                TAB_JOIN_AVVCOA_INGFIS_V21 = d.TAB_JOIN_AVVCOA_INGFIS_V21.Select(y =>
                    new tab_avv_pag_dto.TAB_JOIN_AVVCOA_INGFIS_V2_DTO
                    {
                        tab_avv_pag = new tab_avv_pag_dto.TAB_JOIN_AVVCOA_INGFIS_V2_DTO.tab_avv_pag_dto
                        {
                            anagrafica_tipo_avv_pag = new tab_avv_pag_dto.TAB_JOIN_AVVCOA_INGFIS_V2_DTO.tab_avv_pag_dto.anagrafica_tipo_avv_pag_dto
                            {
                                id_servizio = y.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio
                            },
                            anagrafica_stato = new tab_avv_pag_dto.TAB_JOIN_AVVCOA_INGFIS_V2_DTO.tab_avv_pag_dto.anagrafica_stato_dto
                            {
                                cod_stato_riferimento = y.tab_avv_pag.anagrafica_stato.cod_stato_riferimento
                            },
                            cod_stato = y.tab_avv_pag.cod_stato,
                            flag_esito_sped_notifica = y.tab_avv_pag.flag_esito_sped_notifica,
                            dt_emissione = y.tab_avv_pag.dt_emissione
                        }
                    }).ToList(),  // -> d.ExistsAtti, d.ExistsAttiIntimSoll,  d.ExistsAttiCoattivi, IsProvvedimentoPresentabile //---
                // -------------------------------------------------
                // Tutto ciò che è relativo a 
                //          soggettoDebitore = d.SoggettoDebitore,
                //          soggettoDebitoreTerzo = d.SoggettoDebitoreTerzo,
                tab_contribuente = new tab_avv_pag_dto.tab_contribuente_dto
                {
                    anagrafica_tipo_contribuente = new tab_avv_pag_dto.anagrafica_tipo_contribuente_dto
                    {
                        sigla_tipo_contribuente = d.tab_contribuente.anagrafica_tipo_contribuente.sigla_tipo_contribuente
                    },
                    cod_fiscale = d.tab_contribuente.cod_fiscale,
                    cognome = d.tab_contribuente.cognome,
                    denominazione_commerciale = d.tab_contribuente.denominazione_commerciale,
                    nome = d.tab_contribuente.nome,
                    p_iva = d.tab_contribuente.p_iva,
                    rag_sociale = d.tab_contribuente.rag_sociale
                },
                join_avv_pag_soggetto_debitore = d.join_avv_pag_soggetto_debitore.Select(y =>
                    new tab_avv_pag_dto.join_avv_pag_soggetto_debitore_dto
                    {
                        cod_stato = y.cod_stato,
                        id_terzo_debitore = y.id_terzo_debitore,
                        join_referente_contribuente = new tab_avv_pag_dto.join_avv_pag_soggetto_debitore_dto.join_referente_contribuente_dto
                        {
                            anagrafica_tipo_relazione = new tab_avv_pag_dto.anagrafica_tipo_relazione_dto
                            {
                                desc_tipo_relazione = y.join_referente_contribuente.anagrafica_tipo_relazione.desc_tipo_relazione
                            },
                            tab_referente = new tab_avv_pag_dto.join_avv_pag_soggetto_debitore_dto.join_referente_contribuente_dto.tab_referente_dto
                            {
                                anagrafica_tipo_contribuente = new tab_avv_pag_dto.anagrafica_tipo_contribuente_dto
                                {
                                    sigla_tipo_contribuente = y.join_referente_contribuente.tab_referente.anagrafica_tipo_contribuente.sigla_tipo_contribuente
                                },
                                cod_fiscale = y.join_referente_contribuente.tab_referente.cod_fiscale,
                                cognome = y.join_referente_contribuente.tab_referente.cognome,
                                denominazione_commerciale = y.join_referente_contribuente.tab_referente.denominazione_commerciale,
                                nome = y.join_referente_contribuente.tab_referente.nome,
                                p_iva = y.join_referente_contribuente.tab_referente.p_iva,
                                rag_sociale = y.join_referente_contribuente.tab_referente.rag_sociale,
                                tab_contribuente = new tab_avv_pag_dto.tab_contribuente_dto
                                {
                                    anagrafica_tipo_contribuente = new tab_avv_pag_dto.anagrafica_tipo_contribuente_dto
                                    {
                                        sigla_tipo_contribuente = y.join_referente_contribuente.tab_referente.tab_contribuente.anagrafica_tipo_contribuente.sigla_tipo_contribuente
                                    },
                                    cod_fiscale = y.join_referente_contribuente.tab_referente.tab_contribuente.cod_fiscale,
                                    cognome = y.join_referente_contribuente.tab_referente.tab_contribuente.cognome,
                                    denominazione_commerciale = y.join_referente_contribuente.tab_referente.tab_contribuente.denominazione_commerciale,
                                    nome = y.join_referente_contribuente.tab_referente.tab_contribuente.nome,
                                    p_iva = y.join_referente_contribuente.tab_referente.tab_contribuente.p_iva,
                                    rag_sociale = y.join_referente_contribuente.tab_referente.tab_contribuente.rag_sociale,
                                }
                            }
                        },
                        tab_terzo = new tab_avv_pag_dto.join_avv_pag_soggetto_debitore_dto.tab_terzo_dto
                        {
                            cod_fiscale = y.tab_terzo.cod_fiscale,
                            cognome = y.tab_terzo.cognome,
                            nome = y.tab_terzo.nome,
                            rag_sociale = y.tab_terzo.rag_sociale
                        },
                        tab_avv_pag = new tab_avv_pag_dto.join_avv_pag_soggetto_debitore_dto.tab_avv_pag_restrited_dto
                        {
                            tab_contribuente = new tab_avv_pag_dto.tab_contribuente_dto
                            {
                                anagrafica_tipo_contribuente = new tab_avv_pag_dto.anagrafica_tipo_contribuente_dto
                                {
                                    sigla_tipo_contribuente = y.tab_avv_pag.tab_contribuente.anagrafica_tipo_contribuente.sigla_tipo_contribuente
                                },
                                cod_fiscale = y.tab_avv_pag.tab_contribuente.cod_fiscale,

                                cognome = y.tab_avv_pag.tab_contribuente.cognome,
                                denominazione_commerciale = y.tab_avv_pag.tab_contribuente.denominazione_commerciale,
                                nome = y.tab_avv_pag.tab_contribuente.nome,
                                p_iva = y.tab_avv_pag.tab_contribuente.p_iva,
                                rag_sociale = y.tab_avv_pag.tab_contribuente.rag_sociale
                            }
                        }
                    }).ToList(),
                // -------------------------------------------------

                // -------------------------------------------------
                // Contribuente = d.tab_contribuente != null ? d.tab_contribuente.contribuenteDisplay : string.Empty,
                // -------------------------------------------------

                // -------------------------------------------------
                // IsIstanzaPresentabile, dataMassimaPresentazioneIstanza, dataMassimaPagamentoAvviso, IsAvvisoPagabile
                anagrafica_tipo_avv_pag = new tab_avv_pag_dto.anagrafica_tipo_avv_pag_dto
                {
                    tab_modalita_rate_avvpag = d.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.Select(y =>
                        new tab_avv_pag_dto.anagrafica_tipo_avv_pag_dto.tab_modalita_rate_avvpag_dto
                        {
                            GG_definitivita_avviso = y.GG_definitivita_avviso,
                            GG_scadenza_pagamento = y.GG_scadenza_pagamento,
                            GG_massimi_data_emissione = y.GG_massimi_data_emissione
                        }).ToList()
                },
                // -------------------------------------------------

                // avvisoBonario = d.avvisoBonario,
                importo_sgravio = d.importo_sgravio,
                // importo_sgravio_Euro = d.importo_sgravio_Euro,
                color = ""
            }).ToList();//.ToListAsync();

            return retlist;
        }
        */

    /*
     * IstanzeController.cs:
     * 
     * 
    [HttpPost]
    public ActionResult GridUpdateAvvisiCoaAnnRett_new(int IdEntrata, int IdTipoAvviso)
    {
#if DEBUG
        int cc = Interlocked.Increment(ref _gridUpdateAvvisiCoaAnnRett_COUNT);
        logger.LogInfoMessage($"GridUpdateAvvisiCoaAnnRett ({cc}) begin");
        var sWatch = new System.Diagnostics.Stopwatch();
        sWatch.Start();
#endif
        var v_avvisiList = TabAvvPagBD.GetList(dbContextReadOnly).AsNoTracking()
                                            .WhereByIdContribuente(Sessione.getCurrentContribuente().id_anag_contribuente)
                                            .WhereByIdEntrata(IdEntrata)
                                            .WhereByIdTipoAvvPag(IdTipoAvviso)
                                            //.WhereByValido()
                                            //.WhereByCodStato(new List<string> { anagrafica_stato_avv_pag.VAL_EME, anagrafica_stato_avv_pag.VAL_FIS })
                                            //Luigi-Deve essere VAL-EME e non valido, per scartare gli avvisi finiti in atti successivi
                                            .WhereByValido(anagrafica_stato_avv_pag.VAL_EME)
                                            .WhereByFonteEmissioneEsclusa(tab_avv_pag.FONTE_IMPORTATA)
                                            .WhereByNotRateizzato()
                                            //Luigi-Con questo filtro non si possono fare istanze su avvisi su cui Rosa non ha caricato le notifiche
                                            //.WhereByNotSpeditiConNotificaNonNotificati()
                                            .OrderByPrioritaVisualizzazione()
                                            .ToLightNew();

#if DEBUG
        string infoStr = $"\tGridUpdateAvvisiCoaAnnRett ({cc}) end: time {sWatch.Elapsed:hh\\:mm\\:ss\\.fff}";
        logger.LogInfoMessage(infoStr);

#endif
        return Json(new { data = v_avvisiList }, JsonRequestBehavior.AllowGet);
    }
    */
    public class tab_avv_pag_dto : BaseEntity<tab_avv_pag_light>
    {
        public class tab_contribuzione_dto
        {
            public int? id_avv_pag_iniziale { get; set; }
            public string tab_tipo_voce_contribuzione_codice_tributo_ministeriale { get; set; }
            public decimal importo_residuo { get; set; }
        }
        public class tab_contribuzione_dto1
        {
            public int id_avv_pag { get; set; }
        }

        #region Classi innestate (leggi commento!)

        // Perché? Perchè nella light ci sono proprietà innestate in proprietà innestate in etc...
        // ognuna è una query, che esplodono. poiché mi è difficile tenere traccia di tutte le proprietà
        // innestate, duplico la struttura con le sequenti classi DTO: dopo potrò essere in grado di semplificare!
        // In particolare "join_avv_pag_soggetto_debitore_dto" serve per la proprietà "SoggettoDebitore"

        public class anagrafica_tipo_contribuente_dto
        {
            public string sigla_tipo_contribuente { get; set; }
        }
        public class anagrafica_tipo_relazione_dto
        {
            public string desc_tipo_relazione { get; set; }
        }

        public class tab_contribuente_dto
        {
            public anagrafica_tipo_contribuente_dto anagrafica_tipo_contribuente { get; set; }
            public bool isPersonaFisica { get { return anagrafica_tipo_contribuente.sigla_tipo_contribuente == Publisoftware.Data.anagrafica_tipo_contribuente.PERS_FISICA; } }
            public string nome { get; set; }
            public string cognome { get; set; }
            public string rag_sociale { get; set; }
            public string cod_fiscale { get; set; }
            public string denominazione_commerciale { get; set; }
            public string p_iva { get; set; }

            // TODO: unificare con corrispondente proprietà in tab_contribuente
            public string contribuenteDisplay
            {
                get
                {
                    if (isPersonaFisica)
                    {
                        return nome + " " + cognome + " - " + cod_fiscale;
                    }
                    else if (rag_sociale != null && rag_sociale != string.Empty && denominazione_commerciale != null && denominazione_commerciale != string.Empty)
                    {
                        return rag_sociale + " - " + denominazione_commerciale + " - " + p_iva;
                    }
                    else if (rag_sociale != null && rag_sociale != string.Empty)
                    {
                        return rag_sociale + " - " + p_iva;
                    }
                    else if (denominazione_commerciale != null && denominazione_commerciale != string.Empty)
                    {
                        return denominazione_commerciale + " - " + p_iva;
                    }
                    else
                    {
                        return nome + " " + cognome + " - " + cod_fiscale + "/" + p_iva;
                    }
                }
            }
        }

        public class join_avv_pag_soggetto_debitore_dto
        {
            public class tab_terzo_dto
            {
                public string nome { get; set; }
                public string cognome { get; set; }
                public string rag_sociale { get; set; }
                public string cod_fiscale { get; set; }

                public string terzoNominativoDisplay
                {
                    get
                    {
                        if (cod_fiscale != null && cod_fiscale != string.Empty) { return nome + " " + cognome; }
                        else { return rag_sociale; }
                    }
                }
            }
            public tab_terzo_dto tab_terzo { get; set; }


            public class tab_avv_pag_restrited_dto
            {
                public tab_contribuente_dto tab_contribuente { get; set; }
            }
            public tab_avv_pag_restrited_dto tab_avv_pag { get; set; }

            public class join_referente_contribuente_dto
            {
                public class tab_referente_dto
                {
                    public anagrafica_tipo_contribuente_dto anagrafica_tipo_contribuente { get; set; }


                    public tab_avv_pag_dto.tab_contribuente_dto tab_contribuente { get; set; }

                    public bool isPersonaFisica
                    {
                        get
                        {
                            if (tab_contribuente != null)
                            {
                                return tab_contribuente.isPersonaFisica;
                            }
                            else
                            {
                                if (anagrafica_tipo_contribuente != null)
                                {
                                    return anagrafica_tipo_contribuente.sigla_tipo_contribuente == Publisoftware.Data.anagrafica_tipo_contribuente.PERS_FISICA;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                    }

                    // TODO unificare con corrispondente proprietà in tab_referente
                    public string referenteDisplay
                    {
                        get
                        {
                            if (tab_contribuente != null)
                            {
                                if (tab_contribuente.isPersonaFisica)
                                {
                                    return tab_contribuente.nome + " " + tab_contribuente.cognome + " - " + tab_contribuente.cod_fiscale;
                                }
                                else if (tab_contribuente.rag_sociale != null && tab_contribuente.rag_sociale != string.Empty && tab_contribuente.denominazione_commerciale != null && tab_contribuente.denominazione_commerciale != string.Empty)
                                {
                                    return tab_contribuente.rag_sociale + " - " + tab_contribuente.denominazione_commerciale + " - " + tab_contribuente.p_iva;
                                }
                                else if (tab_contribuente.rag_sociale != null && tab_contribuente.rag_sociale != string.Empty)
                                {
                                    return tab_contribuente.rag_sociale + " - " + tab_contribuente.p_iva;
                                }
                                else if (tab_contribuente.denominazione_commerciale != null && tab_contribuente.denominazione_commerciale != string.Empty)
                                {
                                    return tab_contribuente.denominazione_commerciale + " - " + tab_contribuente.p_iva;
                                }
                                else
                                {
                                    return tab_contribuente.nome + " " + tab_contribuente.cognome + " - " + tab_contribuente.cod_fiscale + "/" + tab_contribuente.p_iva;
                                }
                            }
                            else
                            {
                                if (isPersonaFisica)
                                {
                                    return nome + " " + cognome + " - " + cod_fiscale;
                                }
                                else if (rag_sociale != null && rag_sociale != string.Empty && denominazione_commerciale != null && denominazione_commerciale != string.Empty)
                                {
                                    return rag_sociale + " - " + denominazione_commerciale + " - " + p_iva;
                                }
                                else if (rag_sociale != null && rag_sociale != string.Empty)
                                {
                                    return rag_sociale + " - " + p_iva;
                                }
                                else if (denominazione_commerciale != null && denominazione_commerciale != string.Empty)
                                {
                                    return denominazione_commerciale + " - " + p_iva;
                                }
                                else
                                {
                                    return nome + " " + cognome + " - " + cod_fiscale + "/" + p_iva;
                                }
                            }
                        }
                    }

                    public string nome { get; set; }
                    public string cognome { get; set; }
                    public string rag_sociale { get; set; }
                    public string cod_fiscale { get; set; }
                    public string denominazione_commerciale { get; set; }
                    public string p_iva { get; set; }
                }
                public tab_referente_dto tab_referente { get; set; }
                public anagrafica_tipo_relazione_dto anagrafica_tipo_relazione { get; set; }
            }
            public join_referente_contribuente_dto join_referente_contribuente { get; set; }

            public int? id_terzo_debitore { get; set; }
            public string cod_stato { get; set; }
        }

        #endregion

        #region Classi innestate (leggi commento!) per le prop. ExistsAtti*


        #endregion

        public class anagrafica_stato_dto
        {
            public string desc_stato_riferimento { get; set; }
            public string cod_stato_riferimento { get; set; }
        }
        public anagrafica_stato_dto anagrafica_stato { get; set; }

        public string NumeroIstanza { get; set; } = string.Empty;
        public string TipoIstanza { get; set; } = string.Empty;
        public string DataPresentazione { get; set; } = string.Empty;

        public int id_tab_avv_pag { get; set; }
        public int id_tipo_avvpag { get; set; }
        public string Identificativo { get; set; }
        public string NumeroAvviso { get; set; }
        public DateTime? dt_emissione { get; set; }
        public string dt_emissione_String { get { return dt_emissione?.ToString("dd/MM/yyyy") ?? string.Empty; } }

        public decimal? imp_tot_avvpag_rid_orig { get; set; }
        public decimal imp_tot_avvpag_rid { get { return imp_tot_avvpag_rid_orig ?? 0M; } }
        public decimal? imp_tot_avvpag { get; set; }
        public decimal imp_tot_avvpag_Euro { get { return imp_tot_avvpag ?? 0M; } }
        public string Importo { get { return imp_tot_avvpag_rid_orig?.ToString("C") ?? 0.ToString("C"); } }

        public ICollection<tab_contribuzione_dto> tab_contribuzione { get; set; }
        public ICollection<tab_contribuzione_dto1> tab_contribuzione1 { get; set; }
        public int anagrafica_tipo_avv_pag_id_servizio { get; set; }

        // TODO unificare con corrispondente proprietà in tab_avv_pag
        public bool IsAttoSuccessivoIngiunzione
        {
            get
            {
                return anagrafica_tipo_avv_pag_id_servizio == anagrafica_tipo_servizi.INTIM ||
                       anagrafica_tipo_avv_pag_id_servizio == anagrafica_tipo_servizi.SOLL_PRECOA ||
                       anagrafica_tipo_avv_pag_id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI ||
                       anagrafica_tipo_avv_pag_id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO ||
                       anagrafica_tipo_avv_pag_id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO ||
                       anagrafica_tipo_avv_pag_id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_IMMOBILIARI ||
                       anagrafica_tipo_avv_pag_id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI;

            }
        }

        #region Modificate per eseguire le query delle proprietà "in memory" (vedi selezione tab_contribuzione, tab_contribuzione1)

        // TODO unificare con corrispondente proprietà in tab_avv_pag
        public decimal importoSpeseNotificaDecimal
        {
            get
            {
                return tab_contribuzione
                    .Where(x =>
                        (x.tab_tipo_voce_contribuzione_codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_NOT
                            || x.tab_tipo_voce_contribuzione_codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_SPE)
                        && tab_contribuzione1.Select(y => y.id_avv_pag).Contains(x.id_avv_pag_iniziale.HasValue ? x.id_avv_pag_iniziale.Value : -1)).Sum(x => x.importo_residuo);
            }
        }

        // TODO unificare con corrispondente proprietà in tab_avv_pag
        public decimal importoSpeseCoattiveDecimal
        {
            get
            {
                if (IsAttoSuccessivoIngiunzione && tab_contribuzione.Any(d => d.tab_tipo_voce_contribuzione_codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_COA && tab_contribuzione1.Select(x => x.id_avv_pag).ToList().Contains(d.id_avv_pag_iniziale.HasValue ? d.id_avv_pag_iniziale.Value : -1)))
                {
                    return tab_contribuzione.Where(d => d.tab_tipo_voce_contribuzione_codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_COA && tab_contribuzione1.Select(x => x.id_avv_pag).ToList().Contains(d.id_avv_pag_iniziale.HasValue ? d.id_avv_pag_iniziale.Value : -1)).Sum(d => d.importo_residuo);
                }
                else
                {
                    return 0;
                }
            }
        }

        // TODO unificare con corrispondente proprietà in tab_avv_pag
        public string ImportoSpeseNotifica { get { return importoSpeseNotificaDecimal.ToString("C"); } }
        // TODO unificare con corrispondente proprietà in tab_avv_pag
        public string ImportoSpeseCoattive { get { return importoSpeseCoattiveDecimal.ToString("C"); } }

        #endregion

        public string flag_rateizzazione_bis { get; set; }
        public Nullable<int> num_rate_bis { get; set; }
        public Nullable<int> num_rate { get; set; }


        // TODO unificare con corrispondente proprietà in tab_avv_pag
        [DisplayName("Numero Rate")]
        public string Rate
        {
            get
            {
                if (flag_rateizzazione_bis == "2")
                {
                    return "Rateizzato";
                }
                else if (flag_rateizzazione_bis == "1" && (flag_adesione == "0" || flag_adesione == null))
                {
                    if (num_rate_bis.HasValue)
                    {
                        return num_rate_bis.Value + " su istanza";
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else if (flag_rateizzazione_bis == "1" && flag_adesione == "1")
                {
                    if (num_rate_bis.HasValue)
                    {
                        return num_rate_bis.Value + " su adesione";
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    if (num_rate.HasValue && num_rate <= 1)
                    {
                        return "1";
                    }
                    else if (num_rate.HasValue)
                    {
                        return num_rate.Value.ToString();
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
        }

        public string Targa { get; set; }


        public string flag_spedizione_notifica { get; set; }
        public string flag_esito_sped_notifica { get; set; }
        public Nullable<System.DateTime> data_avvenuta_notifica { get; set; }

        // TODO unificare con corrispondente proprietà in tab_avv_pag
        [DisplayName("Spedito/Notificato")]
        public string SpeditoNotificato
        {
            get
            {
                if (flag_spedizione_notifica == "0" || flag_spedizione_notifica == null)
                {
                    return "Spedito";
                }
                else
                {
                    if (flag_esito_sped_notifica == "0" || flag_esito_sped_notifica == null)
                    {
                        return "Non notificato";
                    }
                    else
                    {
                        //Il dottore ha voluto mostrare la data di ricezione al posto di quella di avvenuta notifica
                        //Poi ha cambiato idea ed ha voluto far vedere entrambi o l'una o l'altra o nessuna a seconda se sono null o no
                        //if (data_avvenuta_notifica.HasValue)
                        if (data_ricezione.HasValue)
                        {
                            //return "Notificato il " + data_avvenuta_notifica.Value.ToShortDateString();
                            if (data_avvenuta_notifica.HasValue)
                            {
                                return "Ricezione:" + data_ricezione.Value.ToShortDateString() + "<br> Notifica: " + data_avvenuta_notifica.Value.ToShortDateString();
                            }
                            else
                            {
                                return "Ricezione:" + data_ricezione.Value.ToShortDateString();
                            }
                        }
                        else if (data_avvenuta_notifica.HasValue)
                        {
                            if (data_ricezione.HasValue)
                            {
                                return "Ricezione:" + data_ricezione.Value.ToShortDateString() + "<br> Notifica: " + data_avvenuta_notifica.Value.ToShortDateString();
                            }
                            else
                            {
                                return "Notifica: " + data_avvenuta_notifica.Value.ToShortDateString();
                            }
                        }
                        else
                        {
                            return "Notificato";
                        }
                    }
                }
            }
        }

        decimal? _imp_tot_pagato_orig;
        public decimal? imp_tot_pagato_orig { get { return _imp_tot_pagato_orig; } set { _imp_tot_pagato_orig = value; } }
        public decimal imp_tot_pagato { get { return _imp_tot_pagato_orig ?? 0; } }
        public string imp_tot_pagato_Euro { get { return imp_tot_pagato.ToString("C"); } }

        public decimal? importo_tot_da_pagare_orig { get; set; }
        public decimal importo_tot_da_pagare { get { return importo_tot_da_pagare_orig ?? 0; } }
        public string importo_tot_da_pagare_Euro { get { return importo_tot_da_pagare.ToString("C"); } }
        public string ImportoDaPagare { get { return importo_tot_da_pagare_Euro; } }

        public string stato { get { return anagrafica_stato?.desc_stato_riferimento ?? String.Empty; } }
        public string cod_stato { get { return anagrafica_stato?.cod_stato_riferimento ?? String.Empty; } }
        public string codStatoReale { get; set; }

        // ----------------------------------------------------------
        public string flag_adesione { get; set; }
        public Nullable<System.DateTime> data_adesione { get; set; }
        // TODO unificare con corrispondente proprietà in tab_avv_pag
        public string data_adesione_String { get { return data_adesione?.ToString("dd/MM/yyyy") ?? String.Empty; } }
       
        // TODO unificare con corrispondente proprietà in tab_avv_pag
        public string Adesione
        {
            get
            {
                if (flag_adesione == "1")
                {
                    if (data_adesione.HasValue)
                    {
                        return data_adesione_String;
                    }
                    else
                    {
                        return "Si";
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        // ----------------------------------------------------------

        const string TipoBene = "OBSOLETO";
        public int id_tab_supervisione_finale { get; set; }
        public string Atti { get; set; }
        public string IntimazioneCorrelata { get; set; }

        public decimal? importo_ridotto { get; set; }
        public string importo_ridotto_Euro { get { return importo_ridotto?.ToString("C") ?? 0M.ToString("C"); } }
        public string impRidottoPerAdesione
        {
            get
            {
                //Il dottore ha voluto inserire questo campo per gli avvisi di accertamento (id_servizio=1) che mostrano l'eventuale importo ridotto se i termini non sono scaduti ancora
                double v_giorni = (DateTime.Now - (data_avvenuta_notifica.HasValue ? data_avvenuta_notifica.Value : DateTime.Now)).TotalDays;

                if (v_giorni > 0 && v_giorni < 30 && importo_ridotto_Euro != string.Empty)
                {
                    return importo_ridotto_Euro + " (entro il " + data_avvenuta_notifica.Value.AddMonths(1).ToShortDateString() + ")";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        //public int? _id_avvpag_preavviso_collegato;
        //public int id_avvpag_preavviso_collegato { get { return _id_avvpag_preavviso_collegato ?? -1; } set { _id_avvpag_preavviso_collegato=value; } }
        public int id_avvpag_preavviso_collegato { get; set; }

        public bool IsIstanzaVisibile { get; set; }

        // ----------------------------------------------------------------
        public class TAB_JOIN_AVVCOA_INGFIS_V2_DTO
        {
            public class tab_avv_pag_dto
            {
                public class anagrafica_tipo_avv_pag_dto
                {
                    public int id_servizio { get; set; }

                }
                public anagrafica_tipo_avv_pag_dto anagrafica_tipo_avv_pag { get; set; }

                public class anagrafica_stato_dto
                {
                    public string cod_stato_riferimento { get; set; }
                }
                public anagrafica_stato_dto anagrafica_stato { get; set; }

                public string cod_stato { get; set; }
                public string flag_esito_sped_notifica { get; set; }
                public DateTime? dt_emissione { get; set; }
            }
            public tab_avv_pag_dto tab_avv_pag { get; set; }
        }
        public string dati_avviso_bonario { get; set; }
        public DateTime? data_avviso_bonario { get; set; }
        public string avvisoBonario
        {
            get
            {
                if (!string.IsNullOrEmpty(dati_avviso_bonario))
                {
                    return dati_avviso_bonario + " - " + (data_avviso_bonario?.ToString("dd/MM/yyyy") ?? String.Empty);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public ICollection<TAB_JOIN_AVVCOA_INGFIS_V2_DTO> TAB_JOIN_AVVCOA_INGFIS_V21 { get; set; }

        // TODO unificare con corrispondente proprietà in tab_avv_pag
        public bool ExistsAtti { get { return TAB_JOIN_AVVCOA_INGFIS_V21.Count > 0; } }

        // TODO unificare con corrispondente proprietà in tab_avv_pag
        public bool ExistsAttiIntimSoll
        {
            get
            {
                return TAB_JOIN_AVVCOA_INGFIS_V21.Any(d => d.tab_avv_pag != null && ((d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.INTIM ||
                                                            d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.SOLL_PRECOA) &&
                                                            (d.tab_avv_pag.anagrafica_stato.cod_stato_riferimento.Contains(anagrafica_stato_avv_pag.VALIDO) ||
                                                             d.tab_avv_pag.anagrafica_stato.cod_stato_riferimento.Contains(anagrafica_stato_avv_pag.SOSPESO)))) || !string.IsNullOrEmpty(avvisoBonario);
            }
        }
        // TODO unificare con corrispondente proprietà in tab_avv_pag
        public bool ExistsAttiCoattivi
        {
            get
            {
                return TAB_JOIN_AVVCOA_INGFIS_V21.Any(d => d.tab_avv_pag != null && ((d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO ||
                                                            d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO ||
                                                            d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_IMMOBILIARI ||
                                                            d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI ||
                                                            d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI) &&
                                                            (d.tab_avv_pag.anagrafica_stato.cod_stato_riferimento.Contains(anagrafica_stato_avv_pag.VALIDO) ||
                                                             d.tab_avv_pag.anagrafica_stato.cod_stato_riferimento.Contains(anagrafica_stato_avv_pag.SOSPESO))));
            }
        }

        public bool IsProvvedimentoPresentabile
        {
            get
            {
                if (TAB_JOIN_AVVCOA_INGFIS_V21.Where(d => d.tab_avv_pag != null &&
                                                         !d.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO) &&
                                                          d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio > anagrafica_tipo_servizi.INTIM &&
                                                         (d.tab_avv_pag.flag_esito_sped_notifica == "1" || (d.tab_avv_pag.dt_emissione.HasValue && d.tab_avv_pag.dt_emissione.Value.AddDays(90) > DateTime.Now)))
                                             .Count() > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public ICollection<join_avv_pag_soggetto_debitore_dto> join_avv_pag_soggetto_debitore { get; set; }
        public tab_contribuente_dto tab_contribuente { get; set; }

        // TODO: unificare con SoggettoDebitore in tab_avv_pag
        public string soggettoDebitore
        {
            // N.B.: le seguenti LinQ sono tutte "in memory" senza alcuna query!
            get
            {
                if (join_avv_pag_soggetto_debitore.Any(d => d.join_referente_contribuente != null && d.id_terzo_debitore != null && d.cod_stato.Equals(Data.join_avv_pag_soggetto_debitore.ATT_ATT)))
                {
                    join_avv_pag_soggetto_debitore_dto v_joinAvvPaggSoggDeb = join_avv_pag_soggetto_debitore.Where(d => d.join_referente_contribuente != null && d.id_terzo_debitore != null && d.cod_stato.Equals(Data.join_avv_pag_soggetto_debitore.ATT_ATT)).FirstOrDefault();
                    return "Soggetto debitore: " + v_joinAvvPaggSoggDeb.tab_terzo.terzoNominativoDisplay +
                        " terzo debitore di " + v_joinAvvPaggSoggDeb.join_referente_contribuente.tab_referente.referenteDisplay +
                        ", referente del contribuente " + tab_contribuente.contribuenteDisplay +
                        ", in qualità di " + (v_joinAvvPaggSoggDeb.join_referente_contribuente.anagrafica_tipo_relazione.desc_tipo_relazione);
                }
                else if (join_avv_pag_soggetto_debitore.Any(d => d.join_referente_contribuente != null && d.cod_stato.Equals(Data.join_avv_pag_soggetto_debitore.ATT_ATT)))
                {
                    join_avv_pag_soggetto_debitore_dto v_joinAvvPaggSoggDeb = join_avv_pag_soggetto_debitore.Where(d => d.join_referente_contribuente != null && d.cod_stato.Equals(Data.join_avv_pag_soggetto_debitore.ATT_ATT)).FirstOrDefault();
                    return "Soggetto debitore: " + v_joinAvvPaggSoggDeb.join_referente_contribuente.tab_referente.referenteDisplay +
                        " in qualità di " + (v_joinAvvPaggSoggDeb.join_referente_contribuente.anagrafica_tipo_relazione.desc_tipo_relazione) +
                        " del contribuente " + tab_contribuente.contribuenteDisplay;
                }
                else if (join_avv_pag_soggetto_debitore.Any(d => d.id_terzo_debitore != null && d.cod_stato.Equals(Data.join_avv_pag_soggetto_debitore.ATT_ATT)))
                {
                    join_avv_pag_soggetto_debitore_dto v_joinAvvPaggSoggDeb = join_avv_pag_soggetto_debitore.Where(d => d.id_terzo_debitore != null && d.cod_stato.Equals(Data.join_avv_pag_soggetto_debitore.ATT_ATT)).FirstOrDefault();
                    return "Soggetto debitore: " + v_joinAvvPaggSoggDeb.tab_terzo.terzoNominativoDisplay +
                        " terzo debitore del contribuente " + tab_contribuente.contribuenteDisplay;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        //public string soggettoDebitoreTerzo { get; set; }
        public string SoggettoDebitoreTerzo
        {
            get
            {
                if (join_avv_pag_soggetto_debitore.Any(d => d.join_referente_contribuente != null && d.id_terzo_debitore != null && d.cod_stato.Equals(Data.join_avv_pag_soggetto_debitore.ATT_ATT)))
                {
                    join_avv_pag_soggetto_debitore_dto v_joinAvvPaggSoggDeb = join_avv_pag_soggetto_debitore.Where(d => d.join_referente_contribuente != null && d.id_terzo_debitore != null && d.cod_stato.Equals(Data.join_avv_pag_soggetto_debitore.ATT_ATT)).FirstOrDefault();
                    return "Terzo debitore di " + v_joinAvvPaggSoggDeb.join_referente_contribuente.tab_referente.referenteDisplay +
                        ", referente del contribuente " + v_joinAvvPaggSoggDeb.tab_avv_pag.tab_contribuente.contribuenteDisplay +
                        ", in qualità di " + (v_joinAvvPaggSoggDeb.join_referente_contribuente.anagrafica_tipo_relazione.desc_tipo_relazione);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        //public string Contribuente { get; set; }
        public string Contribuante { get { return tab_contribuente.contribuenteDisplay; } }


        // -------------------------------------------------------------------------------------
        // public bool IsIstanzaPresentabile { get; set; }
        public class anagrafica_tipo_avv_pag_dto
        {
            public class tab_modalita_rate_avvpag_dto
            {
                public Nullable<int> GG_definitivita_avviso { get; set; }
                public int? GG_scadenza_pagamento { get; set; }
                public int? GG_massimi_data_emissione { get; set; }
            }
            public ICollection<tab_modalita_rate_avvpag_dto> tab_modalita_rate_avvpag { get; set; }
        }
        public anagrafica_tipo_avv_pag_dto anagrafica_tipo_avv_pag { get; set; }
        public bool IsIstanzaPresentabile
        {
            get
            {
                if (anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault() != null &&
                    DateTime.Now.Date > (data_ricezione.HasValue ?
                    data_ricezione.Value.AddDays(anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault().GG_definitivita_avviso.HasValue ? anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault().GG_definitivita_avviso.Value : 0).Date :
                    dt_emissione.Value.AddDays(anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault().GG_definitivita_avviso.HasValue ? anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault().GG_definitivita_avviso.Value : 0)).Date)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public string dataMassimaPresentazioneIstanza
        {
            get
            {
                if (anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault() != null &&
                    DateTime.Now > (data_ricezione.HasValue ?
                    data_ricezione.Value.AddDays(anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault().GG_definitivita_avviso.HasValue ? anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault().GG_definitivita_avviso.Value : 0) :
                      dt_emissione.Value.AddDays(anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault().GG_massimi_data_emissione.HasValue ? anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault().GG_massimi_data_emissione.Value : 0)))
                {
                    return (data_ricezione.HasValue ?
                            data_ricezione.Value.AddDays(anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault().GG_definitivita_avviso.HasValue ? anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault().GG_definitivita_avviso.Value : 0) :
                            dt_emissione.Value.AddDays(anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault().GG_massimi_data_emissione.HasValue ? anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault().GG_massimi_data_emissione.Value : 0)).ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public string dataMassimaPagamentoAvviso
        {
            get
            {
                if (anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault() != null &&
                    DateTime.Now > (data_ricezione.HasValue ?
                    data_ricezione.Value.AddDays(anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault().GG_scadenza_pagamento.HasValue ? anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault().GG_scadenza_pagamento.Value : 0) :
                    dt_emissione.Value.AddDays(anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault().GG_massimi_data_emissione.HasValue ? anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault().GG_massimi_data_emissione.Value : 0)))
                {
                    return (data_ricezione.HasValue ?
                            data_ricezione.Value.AddDays(anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault().GG_scadenza_pagamento.HasValue ? anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault().GG_scadenza_pagamento.Value : 0) :
                            dt_emissione.Value.AddDays(anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault().GG_massimi_data_emissione.HasValue ? anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault().GG_massimi_data_emissione.Value : 0)).ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public bool IsAvvisoPagabile
        {
            get
            {
                if (anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault() != null &&
                    DateTime.Now > (data_ricezione.HasValue ?
                    data_ricezione.Value.AddDays(anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault().GG_scadenza_pagamento.HasValue ? anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault().GG_scadenza_pagamento.Value : 0) :
                    dt_emissione.Value.AddDays(anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault().GG_massimi_data_emissione.HasValue ? anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault().GG_massimi_data_emissione.Value : 0)))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        // -------------------------------------------------------------------------------------

        public DateTime? data_ricezione { get; set; }
        public string data_ricezione_String { get { return data_ricezione?.ToString("dd/MM/yyyy") ?? String.Empty; } }

        public decimal? importo_sgravio { get; set; }
        public string importo_sgravio_Euro { get { return importo_sgravio?.ToString("C") ?? 0.ToString("C"); } }

        public string color { get; set; }
    }
}
