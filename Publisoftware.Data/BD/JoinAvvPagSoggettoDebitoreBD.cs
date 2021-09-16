using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class JoinAvvPagSoggettoDebitoreBD : EntityBD<join_avv_pag_soggetto_debitore>
    {
        public JoinAvvPagSoggettoDebitoreBD()
        {

        }

        public static IQueryable<join_avv_pag_soggetto_debitore> GetAvvisiByRicercaAvvisoDaAccoppiare(tab_mov_pag p_tabMovPag,
                                                                                                      int p_idEntata,
                                                                                                      string p_anno,
                                                                                                      string p_codiceFiscalePiva,
                                                                                                      string p_cognome,
                                                                                                      string p_nome,
                                                                                                      string p_ragioneSociale,
                                                                                                      dbEnte p_dbContext)
        {
            List<int> v_serviziList = new List<int>();

            IQueryable<join_avv_pag_soggetto_debitore> v_avvisiList = GetListInternal(p_dbContext);

            v_avvisiList = v_avvisiList.Where(d => d.tab_avv_pag.id_entrata == p_idEntata &&
                                                   d.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.VAL_EME) &&
                                                   d.tab_avv_pag.tab_liste != null &&
                                                   d.tab_avv_pag.tab_liste.tab_tipo_lista.flag_tipo_lista == tab_tipo_lista.FLAG_TIPO_LISTA_C
                                                   //&& d.tab_avv_pag.tab_liste.cod_stato.StartsWith(tab_liste.DEF_SPE)
                                                   );

            if (p_idEntata == anagrafica_entrate.RISCOSSIONE_COATTIVA)
            {
                v_serviziList.AddRange(new List<int>() { anagrafica_tipo_servizi.ING_FISC,
                                                         anagrafica_tipo_servizi.SOLL_PRECOA,
                                                         anagrafica_tipo_servizi.INTIM,
                                                         anagrafica_tipo_servizi.AVVISI_CAUTELARI,
                                                         anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO,
                                                         anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO,
                                                         anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_IMMOBILIARI,
                                                         anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI,
                                                         anagrafica_tipo_servizi.SERVIZI_RATEIZZAZIONE_COA,
                                                         anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO});

                v_avvisiList = v_avvisiList.Where(d => v_serviziList.Contains(d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio));

                v_avvisiList = v_avvisiList.Where(d => !d.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.RETTIFICATO));

                if (!string.IsNullOrEmpty(p_anno))
                {
                    v_avvisiList = v_avvisiList.Where(d => d.tab_avv_pag.anno_riferimento == p_anno);
                }
            }
            else
            {
                if (p_tabMovPag.id_tipo_pagamento == tab_tipo_pagamento.F24 &&
                    p_tabMovPag.Flag_accertamento == "1" &&
                    p_tabMovPag.Flag_ravvedimento != "1")
                {
                    v_serviziList.AddRange(new List<int>() { anagrafica_tipo_servizi.ACCERTAMENTO,
                                                             anagrafica_tipo_servizi.RISC_PRECOA,
                                                             anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM,
                                                             anagrafica_tipo_servizi.ACCERT_ESECUTIVO,
                                                             anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO });

                    v_avvisiList = v_avvisiList.Where(d => v_serviziList.Contains(d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio));

                    v_avvisiList = v_avvisiList.Where(d => !d.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.RETTIFICATO));

                    if (!string.IsNullOrEmpty(p_anno))
                    {
                        v_avvisiList = v_avvisiList.Where(d => d.tab_avv_pag.anno_riferimento == p_anno ||
                                                               d.tab_avv_pag.tab_unita_contribuzione.Any(x => x.num_riga_avv_pag_generato == 1 &&
                                                                                                              x.id_entrata == p_idEntata &&
                                                                                                              x.anno_rif == p_anno));
                    }
                }
                else if (p_tabMovPag.id_tipo_pagamento == tab_tipo_pagamento.F24 &&
                         p_tabMovPag.Flag_accertamento != "1")
                {
                    v_serviziList.AddRange(new List<int>() { anagrafica_tipo_servizi.GEST_ORDINARIA,
                                                             anagrafica_tipo_servizi.AVVISI_ORDINARI_NON_SOGGETTO_AD_ACCERTAMENTO });

                    v_avvisiList = v_avvisiList.Where(d => v_serviziList.Contains(d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio));

                    v_avvisiList = v_avvisiList.Where(d => !d.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.RETTIFICATO) &&
                                                           !d.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.VAL_POP));

                    if (!string.IsNullOrEmpty(p_anno))
                    {
                        v_avvisiList = v_avvisiList.Where(d => (d.tab_avv_pag.anno_riferimento == p_anno &&
                                                               (d.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_POP ||
                                                                d.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_POP_SUPPLETIVO)) ||
                                                                d.tab_avv_pag.tab_unita_contribuzione.Any(x => x.num_riga_avv_pag_generato == 1 &&
                                                                                                               x.id_entrata == p_idEntata &&
                                                                                                               x.anno_rif == p_anno));
                    }
                }
                else if (p_tabMovPag.id_tipo_pagamento != tab_tipo_pagamento.F24 ||
                        (p_tabMovPag.id_tipo_pagamento == tab_tipo_pagamento.F24 &&
                         p_tabMovPag.Flag_accertamento == "1" &&
                         p_tabMovPag.Flag_ravvedimento == "1"))
                {
                    List<int> v_serviziList1 = new List<int>();
                    List<int> v_serviziList2 = new List<int>();

                    v_serviziList1 = new List<int>() { anagrafica_tipo_servizi.ACCERTAMENTO,
                                                       anagrafica_tipo_servizi.RISC_PRECOA,
                                                       anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM,
                                                       anagrafica_tipo_servizi.ACCERT_ESECUTIVO,
                                                       anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO};

                    v_serviziList2 = new List<int>() { anagrafica_tipo_servizi.GEST_ORDINARIA,
                                                       anagrafica_tipo_servizi.AVVISI_ORDINARI_NON_SOGGETTO_AD_ACCERTAMENTO };

                    if (!string.IsNullOrEmpty(p_anno))
                    {
                        v_avvisiList = v_avvisiList.Where(d => (v_serviziList1.Contains(d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio) &&
                                                               !d.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.RETTIFICATO) &&
                                                               (d.tab_avv_pag.anno_riferimento == p_anno ||
                                                                d.tab_avv_pag.tab_unita_contribuzione.Any(x => x.num_riga_avv_pag_generato == 1 &&
                                                                                                               x.id_entrata == p_idEntata &&
                                                                                                               x.anno_rif == p_anno)))
                                                               ||
                                                               (v_serviziList2.Contains(d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio) &&
                                                               !d.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.RETTIFICATO) &&
                                                               !d.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.VAL_POP) &&
                                                               ((d.tab_avv_pag.anno_riferimento == p_anno &&
                                                                (d.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_POP ||
                                                                 d.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_POP_SUPPLETIVO)) ||
                                                                 d.tab_avv_pag.tab_unita_contribuzione.Any(x => x.num_riga_avv_pag_generato == 1 &&
                                                                                                                x.id_entrata == p_idEntata &&
                                                                                                                x.anno_rif == p_anno)))
                                                               ||
                                                               (d.tab_avv_pag.anno_riferimento == p_anno &&
                                                                d.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_servizi.SERVIZI_RATEIZZAZIONE_COA));
                    }
                    else
                    {
                        v_avvisiList = v_avvisiList.Where(d => (v_serviziList1.Contains(d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio) &&
                                                               !d.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.RETTIFICATO))
                                                               ||
                                                               (v_serviziList2.Contains(d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio) &&
                                                               !d.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.RETTIFICATO) &&
                                                               !d.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.VAL_POP))
                                                               ||
                                                               d.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_servizi.SERVIZI_RATEIZZAZIONE_COA);
                    }
                }
            }

            if (string.IsNullOrEmpty(p_codiceFiscalePiva) &&
                string.IsNullOrEmpty(p_cognome) &&
                string.IsNullOrEmpty(p_nome) &&
                string.IsNullOrEmpty(p_ragioneSociale))
            {
                v_avvisiList = Enumerable.Empty<join_avv_pag_soggetto_debitore>().AsQueryable();
            }
            else
            {
                if (!string.IsNullOrEmpty(p_codiceFiscalePiva))
                {
                    v_avvisiList = v_avvisiList.Where(d => d.id_terzo_debitore != null &&
                                                          (d.tab_terzo.cod_fiscale == p_codiceFiscalePiva ||
                                                           d.tab_terzo.p_iva == p_codiceFiscalePiva));
                }

                if (!string.IsNullOrEmpty(p_cognome))
                {
                    v_avvisiList = v_avvisiList.Where(d => d.id_terzo_debitore != null &&
                                                           d.tab_terzo.cognome.Contains(p_cognome));
                }

                if (!string.IsNullOrEmpty(p_nome))
                {
                    v_avvisiList = v_avvisiList.Where(d => d.id_terzo_debitore != null &&
                                                           d.tab_terzo.nome.Contains(p_nome));
                }

                if (!string.IsNullOrEmpty(p_ragioneSociale))
                {
                    v_avvisiList = v_avvisiList.Where(d => d.id_terzo_debitore != null &&
                                                          (d.tab_terzo.rag_sociale.Replace("'", "").Contains(p_ragioneSociale) ||
                                                           d.tab_terzo.denominazione_commerciale.Replace("'", "").Contains(p_ragioneSociale)));
                }
            }

            v_avvisiList = v_avvisiList.Where(d => d.tab_avv_pag.tab_liste.tab_tipo_lista.flag_tipo_lista == tab_tipo_lista.FLAG_TIPO_LISTA_C);

            return v_avvisiList;
        }
    }
}
