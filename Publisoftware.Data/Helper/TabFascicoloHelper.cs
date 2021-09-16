#if Spostato_nella_nuova_Dll_Publisoftware_Data_Bd_Pdf
using Publisoftware.Data.BD;
using Publisoftware.Data.LinqExtended;
using Publisoftware.Utility.Stringa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD.Helper
{
    public class TabFascicoloHelper
    {
        public static List<tab_fascicolo> CreaFascicolo(
            int p_idTabDocInput,
            int? p_idTabAvvPag,
            tab_parametri_portale p_parametriPortale,
            string codice_ente,
            int id_ente,
            int id_risorsa,
            dbEnte p_DB,
            bool isIntegrazione)
        {
            tab_doc_input v_tabDocInput = TabDocInputBD.GetById(p_idTabDocInput, p_DB);
            tab_avv_pag v_tabAvvPag = p_idTabAvvPag.HasValue ? TabAvvPagBD.GetById(p_idTabAvvPag.Value, p_DB) : null;

            return CreaFascicolo(v_tabDocInput, v_tabAvvPag, p_parametriPortale, codice_ente, id_ente, id_risorsa, p_DB, isIntegrazione);
        }

        public static List<tab_fascicolo> CreaFascicolo(
            tab_doc_input p_tabDocInput,
            tab_avv_pag p_tabAvvPag,
            tab_parametri_portale p_parametriPortale,
            string codice_ente,
            int id_ente,
            int id_risorsa,
            dbEnte p_DB,
            bool isIntegrazione)
        {
            if (p_tabDocInput == null)
            {
                throw new ArgumentException($"{nameof(p_tabDocInput)}");
            }

            List<tab_fascicolo> v_fascicoliList = new List<tab_fascicolo>();
            List<tab_avv_pag> v_tabAvvPagCollegatiList = new List<tab_avv_pag>();

            string v_macrocategoria = p_tabDocInput.tab_tipo_doc_entrate.id_tipo_doc == tab_tipo_doc_entrate.TIPO_DOC_RICORSI ? anagrafica_documenti.MACROCATEGORIA_RICORSI :
                                     (p_tabDocInput.tab_tipo_doc_entrate.id_tipo_doc == tab_tipo_doc_entrate.TIPO_DOC_IPOTECHE ? anagrafica_documenti.MACROCATEGORIA_IPOTECHE :
                                     (p_tabDocInput.tab_tipo_doc_entrate.id_tipo_doc == tab_tipo_doc_entrate.TIPO_DOC_CITAZIONI ? anagrafica_documenti.MACROCATEGORIA_CITAZIONI :
                                     (p_tabDocInput.tab_tipo_doc_entrate.id_tipo_doc == tab_tipo_doc_entrate.TIPO_DOC_PROCEDURA_CONCORSUALE ? anagrafica_documenti.MACROCATEGORIA_PROC_CONCORSUALI : string.Empty)));

            IList<join_tab_avv_pag_tab_doc_input> v_itemList = null;
            if (v_macrocategoria == anagrafica_documenti.MACROCATEGORIA_RICORSI &&
                p_tabAvvPag == null)
            {
                v_itemList = JoinTabAvvPagTabDocInputBD.GetList(p_DB)
                                                       .WhereByIdDocInput(p_tabDocInput.id_tab_doc_input)
                                                       .WhereByIdAvvPagNotNull()
                                                       .WhereByAvvisiEmessi()
                                                       .GroupByIdAvvPag()
                                                       .ToList();

                join_tab_avv_pag_tab_doc_input v_firstTabAvvPag = v_itemList.FirstOrDefault();

                if (v_firstTabAvvPag != null && v_firstTabAvvPag.tab_avv_pag != null)
                {
                    p_tabAvvPag = v_firstTabAvvPag.tab_avv_pag;

                    v_tabAvvPagCollegatiList.Add(p_tabAvvPag);

                    v_tabAvvPagCollegatiList.AddRange(v_itemList.Where(x => x.id_avv_pag_collegato != x.id_avv_pag)
                                                                .Select(x => x.tab_avv_pag1)
                                                                .ToList());
                }
                else
                {
                    p_tabAvvPag = JoinTabAvvPagTabDocInputBD.GetList(p_DB)
                                                            .WhereByIdDocInput(p_tabDocInput.id_tab_doc_input)
                                                            .FirstOrDefault().tab_avv_pag;

                    v_tabAvvPagCollegatiList.Add(p_tabAvvPag);
                }
            }
            else if (
                (v_macrocategoria == anagrafica_documenti.MACROCATEGORIA_CITAZIONI &&
                 p_tabAvvPag != null &&
                (p_tabAvvPag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO ||
                 p_tabAvvPag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_IMMOBILIARI ||
                 p_tabAvvPag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI))
                 ||
                (v_macrocategoria == anagrafica_documenti.MACROCATEGORIA_PROC_CONCORSUALI &&
                 p_tabAvvPag != null &&
                 p_tabAvvPag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.SERVIZI_PROCEDURE_CONCORSUALI))
            {
                v_tabAvvPagCollegatiList.Add(p_tabAvvPag);

                v_tabAvvPagCollegatiList.AddRange(p_tabAvvPag.tab_unita_contribuzione
                                                             .Where(d => d.id_avv_pag_collegato != null)
                                                             .Select(d => d.tab_avv_pag1)
                                                             .ToList());

                //Avviso di pignoramento con ordine al terzo
                if (v_macrocategoria == anagrafica_documenti.MACROCATEGORIA_CITAZIONI &&
                    p_tabAvvPag.TAB_SUPERVISIONE_FINALE_V21 != null &&
                    p_tabAvvPag.TAB_SUPERVISIONE_FINALE_V21.id_avvpag_preavviso_collegato.HasValue)
                {
                    v_tabAvvPagCollegatiList.Add(p_tabAvvPag.TAB_SUPERVISIONE_FINALE_V21.tab_avv_pag2);
                }
            }
            else if (v_macrocategoria == anagrafica_documenti.MACROCATEGORIA_IPOTECHE &&
                     p_tabAvvPag != null)
            {
                v_tabAvvPagCollegatiList.Add(p_tabAvvPag);

                v_tabAvvPagCollegatiList.AddRange(p_tabAvvPag.tab_unita_contribuzione
                                                             .Where(d => d.id_avv_pag_collegato != null)
                                                             .Select(d => d.tab_avv_pag1)
                                                             .ToList());

                //Avviso di pignoramento con ordine al terzo
                if (p_tabAvvPag.TAB_SUPERVISIONE_FINALE_V21 != null &&
                    p_tabAvvPag.TAB_SUPERVISIONE_FINALE_V21.id_avvpag_preavviso_collegato.HasValue)
                {
                    v_tabAvvPagCollegatiList.Add(p_tabAvvPag.TAB_SUPERVISIONE_FINALE_V21.tab_avv_pag2);
                }
            }

            //Intimazioni
            if (v_macrocategoria == anagrafica_documenti.MACROCATEGORIA_RICORSI ||
                v_macrocategoria == anagrafica_documenti.MACROCATEGORIA_CITAZIONI ||
                v_macrocategoria == anagrafica_documenti.MACROCATEGORIA_IPOTECHE)
            {
                List<tab_avv_pag> v_intimazioniList = new List<tab_avv_pag>();

                foreach (tab_avv_pag v_item in v_tabAvvPagCollegatiList)
                {
                    if (v_item.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ING_FISC)
                    {
                        TAB_JOIN_AVVCOA_INGFIS_V2 v_joinAvvCoaIng = v_item.TAB_JOIN_AVVCOA_INGFIS_V21
                                                                          .Where(d => !d.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO) && d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.INTIM)
                                                                          .Where(d => d.tab_avv_pag.flag_esito_sped_notifica != null && d.tab_avv_pag.flag_esito_sped_notifica == "1")
                                                                          .OrderByDescending(d => d.ID_JOIN_AVVCOA_INGFIS)
                                                                          .FirstOrDefault();

                        if (v_joinAvvCoaIng != null)
                        {
                            v_intimazioniList.Add(v_joinAvvCoaIng.tab_avv_pag);
                        }
                    }
                }

                v_tabAvvPagCollegatiList.AddRange(v_intimazioniList.GroupBy(p => new { p.id_tab_avv_pag }).Select(g => g.FirstOrDefault()).ToList());
            }

            if (p_tabAvvPag != null)
            {
                tab_fascicolo v_fascicolo = TabFascicoloBD.GetList(p_DB)
                                                          .WhereByIdTabDocInput(p_tabDocInput.id_tab_doc_input)
                                                          .WhereByIdAvvPag(p_tabAvvPag.id_tab_avv_pag)
                                                          .FirstOrDefault();

                if (v_fascicolo == null)
                {
                    v_fascicolo = InserisciFascicolo(p_tabDocInput.id_tab_doc_input, p_tabAvvPag, p_DB);
                }

                int v_index = 1;
                bool isNotificato = true;
                foreach (tab_avv_pag v_item in v_tabAvvPagCollegatiList)
                {
                    string v_anagraficaDocumentoAvviso = string.Empty;

                    if (v_macrocategoria == anagrafica_documenti.MACROCATEGORIA_RICORSI &&
                        v_index == 1)
                    {
                        v_anagraficaDocumentoAvviso = anagrafica_documenti.SIGLA_AVVISO_RIF;
                        isNotificato = true;
                    }
                    else if (v_macrocategoria != anagrafica_documenti.MACROCATEGORIA_RICORSI &&
                             v_index == 1)
                    {
                        v_anagraficaDocumentoAvviso = anagrafica_documenti.SIGLA_AVVISO_COLL;
                        isNotificato = false;
                    }
                    else if (v_macrocategoria == anagrafica_documenti.MACROCATEGORIA_CITAZIONI &&
                             p_tabAvvPag.TAB_SUPERVISIONE_FINALE_V21 != null &&
                             p_tabAvvPag.TAB_SUPERVISIONE_FINALE_V21.id_avvpag_preavviso_collegato.HasValue &&
                             p_tabAvvPag.TAB_SUPERVISIONE_FINALE_V21.id_avvpag_preavviso_collegato == v_item.id_tab_avv_pag)
                    {
                        v_anagraficaDocumentoAvviso = anagrafica_documenti.SIGLA_AVVISO_ORDINE;
                        isNotificato = true;
                    }
                    else if (v_macrocategoria == anagrafica_documenti.MACROCATEGORIA_IPOTECHE &&
                            p_tabAvvPag.TAB_SUPERVISIONE_FINALE_V21 != null &&
                            p_tabAvvPag.TAB_SUPERVISIONE_FINALE_V21.id_avvpag_preavviso_collegato.HasValue &&
                            p_tabAvvPag.TAB_SUPERVISIONE_FINALE_V21.id_avvpag_preavviso_collegato == v_item.id_tab_avv_pag)
                    {
                        v_anagraficaDocumentoAvviso = anagrafica_documenti.SIGLA_AVVISO_IPOTECA;
                        isNotificato = true;
                    }
                    else
                    {
                        v_anagraficaDocumentoAvviso = anagrafica_documenti.SIGLA_AVVISO_COLL;
                        isNotificato = true;
                    }

                    CercaAvvisiNotifiche(p_parametriPortale, v_item, v_fascicolo, codice_ente, id_ente, id_risorsa, v_macrocategoria, p_tabDocInput, v_anagraficaDocumentoAvviso, isNotificato, p_DB, isIntegrazione);
                    v_index++;
                }

                v_fascicoliList.Add(v_fascicolo);
            }

            return v_fascicoliList;
        }

        private static void CercaAvvisiNotifiche(
            tab_parametri_portale p_parametriPortale,
            tab_avv_pag p_avviso,
            tab_fascicolo p_Fascicolo,
            string codice_ente,
            int id_ente,
            int id_risorsa,
            string p_macrocategoria,
            tab_doc_input p_tabDocInput,
            string p_anagraficaDocumentoAvviso,
            bool isNotificato,
            dbEnte p_DB,
            bool isIntegrazione)
        {
            anagrafica_risorse v_risorsa = AnagraficaRisorseBD.GetList(p_DB)
                                                              .WhereByIdEnteAppartenenza(anagrafica_ente.ID_ENTE_PUBLISERVIZI)
                                                              .WhereByIdRuoloMansione(anagrafica_ruolo_mansione.COD_RUOLO_MANSIONE_LEGALE_RAPPRESENTANTE_ID)
                                                              .FirstOrDefault();

            //Il dottore e Melchiorre hanno voluto togliere l'asseverazione
            bool conAsseverazione = false;

            List<tab_sped_not> v_spedNotList = new List<tab_sped_not>();

            if (isNotificato)
            {
                v_spedNotList = TabSpedNotBD.GetList(p_DB)
                                            .WhereByIdAvviso(p_avviso.id_tab_avv_pag)
                                            .WhereByNotificati()
                                            .OrderByDataEsitoNotifica()
                                            .ToList();
            }
            else
            {
                v_spedNotList = TabSpedNotBD.GetList(p_DB)
                                            .WhereByIdAvviso(p_avviso.id_tab_avv_pag)
                                            .OrderByDataEsitoNotifica()
                                            .ToList();
            }

            foreach (tab_sped_not v_spedNot in v_spedNotList)
            {
                tab_fascicolo_avvpag_allegati v_allegato = TabFascicoloAvvPagAllegatiBD.GetList(p_DB)
                                                                                       .WhereByIdAvviso(p_avviso.id_tab_avv_pag)
                                                                                       .WhereByIdSpedNot(v_spedNot.id_sped_not)
                                                                                       .FirstOrDefault();

                if (v_allegato == null)
                {
                    v_allegato = InserisciAvvisiPagFascicolo(p_Fascicolo, p_avviso, v_spedNot, p_DB);
                }

                tab_documenti v_tabDocumento = new tab_documenti();

                //Cerca Immagini Avviso
                string v_pathAvviso = FileBDRWInfoHelper.GetAvviso(v_spedNot.id_sped_not, p_parametriPortale.url_img, p_parametriPortale.path_avvisi, codice_ente, p_DB);

                if (FileBDRWInfoHelper.RemoteFileExists(v_pathAvviso) &&
                   (!isIntegrazione ||
                    !v_allegato.join_documenti_fascicolo_avvpag_allegati.Any(x => x.tab_documenti.anagrafica_documenti.sigla_doc == p_anagraficaDocumentoAvviso)))
                {
                    v_tabDocumento = InserisciDocumento(p_macrocategoria, p_anagraficaDocumentoAvviso, p_parametriPortale, id_risorsa, p_DB);

                    InserisciJoinDocumentiAvviso(v_allegato, v_spedNot.id_sped_not, v_tabDocumento.id_tab_documenti, p_DB);

                    string v_completePath = InserisciAllegato(v_tabDocumento, p_parametriPortale, id_ente, p_Fascicolo.id_fascicolo, p_tabDocInput.anno.Value, p_DB);

                    FileBDRWInfoHelper.controllaDirectory(p_parametriPortale.path_upload_file.Replace("\\\\", "\\") + v_completePath);

                    string v_testo = null;

                    //if (conAsseverazione)
                    //{
                    //    if (p_avviso.fonte_emissione.Contains(tab_avv_pag.FONTE_IMPORTATA))
                    //    {
                    //        v_testo = TestiHelper.GetAsseverazioneAvvisoImportato(v_risorsa, new List<tab_avv_pag> { p_avviso });
                    //    }
                    //    else
                    //    {
                    //        v_testo = TestiHelper.GetAsseverazioneAvvisoEmesso(v_risorsa, new List<tab_avv_pag> { p_avviso }, null);
                    //    }
                    //}

                    PdfHelper.AggiungiBloccoCopiaConformeURL(v_pathAvviso, p_parametriPortale.path_upload_file.Replace("\\\\", "\\") + v_completePath, conAsseverazione, v_testo);
                }

                //Cerca Immagini Notifiche
                List<join_file> v_joinFileList = JoinFileBD.GetList(p_DB)
                                                           .WhereByIdRiferimento(v_spedNot.id_sped_not)
                                                           .WhereByTipologia(new List<string>() { join_file.TIPO_SPED_NOT })
                                                           .WhereByNomeFileValid()
                                                           .ToList();

                //bool isInserito = false;

                foreach (join_file v_joinFile in v_joinFileList)
                {
                    string v_pathRelata = FileBDRWInfoHelper.GetRelata(v_joinFile.id_join_file, p_parametriPortale.url_img, p_parametriPortale.path_notifiche, codice_ente, p_DB);

                    if (FileBDRWInfoHelper.RemoteFileExists(v_pathRelata) &&
                       (!isIntegrazione ||
                        !v_allegato.join_documenti_fascicolo_avvpag_allegati.Any(x => x.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_NOTIFICA_RELATA)))
                    {
                        //Il dottore ha voluto prendere tutte le immagini delle notifiche
                        //if (!isInserito)
                        //{
                        v_tabDocumento = InserisciDocumento(p_macrocategoria, anagrafica_documenti.SIGLA_NOTIFICA_RELATA, p_parametriPortale, id_risorsa, p_DB);

                        InserisciJoinDocumentiAvviso(v_allegato, v_spedNot.id_sped_not, v_tabDocumento.id_tab_documenti, p_DB);

                        //    isInserito = true;
                        //}

                        string v_completePath = InserisciAllegato(v_tabDocumento, p_parametriPortale, id_ente, p_Fascicolo.id_fascicolo, p_tabDocInput.anno.Value, p_DB);

                        FileBDRWInfoHelper.controllaDirectory(p_parametriPortale.path_upload_file.Replace("\\\\", "\\") + v_completePath);

                        string v_testo = null;

                        //if (conAsseverazione)
                        //{
                        //    if (p_avviso.fonte_emissione.Contains(tab_avv_pag.FONTE_IMPORTATA))
                        //    {
                        //        v_testo = TestiHelper.GetAsseverazioneNotificaImportata(v_risorsa, p_avviso);
                        //    }
                        //    else
                        //    {
                        //        v_testo = TestiHelper.GetAsseverazioneNotificaEmessa(v_risorsa, p_avviso);
                        //    }
                        //}

                        PdfHelper.CreaFileDaImmaginiConCopiaConformeURL(new List<string> { v_pathRelata }, p_parametriPortale.path_upload_file.Replace("\\\\", "\\") + v_completePath, conAsseverazione, v_testo);
                    }
                }
            }
        }

        private static join_documenti_fascicolo_avvpag_allegati InserisciJoinDocumentiAvviso(tab_fascicolo_avvpag_allegati p_allegato, int p_id_sped_not, int p_idDocumento, dbEnte p_DB)
        {
            p_allegato.id_tab_sped_not = p_id_sped_not;

            join_documenti_fascicolo_avvpag_allegati v_join = new join_documenti_fascicolo_avvpag_allegati();
            v_join.id_tab_documenti = p_idDocumento;
            v_join.id_fascicolo_allegati_avvpag = p_allegato.id_fascicolo_allegati_avvpag;

            p_DB.join_documenti_fascicolo_avvpag_allegati.Add(v_join);
            p_DB.SaveChanges();

            return v_join;
        }

        private static tab_fascicolo InserisciFascicolo(int p_idTabDocInput, tab_avv_pag p_avviso, dbEnte p_DB)
        {
            tab_fascicolo v_fascicolo = new tab_fascicolo();
            v_fascicolo.id_ente = p_avviso.id_ente;
            v_fascicolo.id_contribuente = p_avviso.id_anag_contribuente;
            v_fascicolo.id_doc_input = p_idTabDocInput;
            v_fascicolo.id_avv_pag = p_avviso.id_tab_avv_pag;
            v_fascicolo.data_scadenza = DateTime.Now;
            v_fascicolo.cod_stato = anagrafica_stato_doc.STATO_ACQUISITO;
            v_fascicolo.id_stato = anagrafica_stato_doc.STATO_ACQUISITO_ID;

            p_DB.tab_fascicolo.Add(v_fascicolo);
            p_DB.SaveChanges();

            return v_fascicolo;
        }

        private static tab_fascicolo_avvpag_allegati InserisciAvvisiPagFascicolo(tab_fascicolo p_fascicolo, tab_avv_pag p_tabAvvPag, tab_sped_not p_tabSpedNot, dbEnte p_DB)
        {
            tab_fascicolo_avvpag_allegati v_tabFascicoloAvvPagAllegato = new tab_fascicolo_avvpag_allegati();

            v_tabFascicoloAvvPagAllegato.id_fascicolo = p_fascicolo.id_fascicolo;
            v_tabFascicoloAvvPagAllegato.id_avvpag_rif_documenti = p_tabAvvPag.id_tab_avv_pag;
            v_tabFascicoloAvvPagAllegato.id_tab_sped_not = p_tabSpedNot.id_sped_not;
            v_tabFascicoloAvvPagAllegato.cod_stato = anagrafica_stato_doc.STATO_ACQUISITO;
            v_tabFascicoloAvvPagAllegato.id_stato = anagrafica_stato_doc.STATO_ACQUISITO_ID;

            p_DB.tab_fascicolo_avvpag_allegati.Add(v_tabFascicoloAvvPagAllegato);
            p_DB.SaveChanges();

            return v_tabFascicoloAvvPagAllegato;
        }

        private static tab_documenti InserisciDocumento(
            string p_macrocategoria,
            string p_siglaDoc,
            tab_parametri_portale p_parametriPortale,
            int id_risorsa,
            dbEnte p_DB)
        {
            anagrafica_documenti v_anagraficaDocumento = AnagraficaDocumentiBD.GetList(p_DB)
                                                                              .WhereByMacrocategoria(p_macrocategoria)
                                                                              .WhereBySigla(p_siglaDoc)
                                                                              .FirstOrDefault();

            tab_documenti v_tabDocumento = new tab_documenti();

            v_tabDocumento.modalita_acquisizione = "B";
            v_tabDocumento.id_anagrafica_documenti = v_anagraficaDocumento.id_anagrafica_doc;
            v_tabDocumento.id_risorsa_acquisizione = id_risorsa;
            v_tabDocumento.data_creazione = DateTime.Now;
            v_tabDocumento.cod_stato = tab_documenti.ATT_ATT;
            v_tabDocumento.id_stato = tab_documenti.ATT_ATT_ID;

            p_DB.tab_documenti.Add(v_tabDocumento);
            p_DB.SaveChanges();

            return v_tabDocumento;
        }

        private static string InserisciAllegato(tab_documenti p_tabDocumento, tab_parametri_portale p_parametriPortale, int p_idEnte, int p_idFascicolo, int p_anno, dbEnte p_DB)
        {
            string v_pathWithFile = FileBDRWInfoHelper.GetDocumentoFascicoloForUpload(p_idEnte,
                                                                                      p_tabDocumento.id_tab_documenti.ToString(),
                                                                                      p_anno,
                                                                                      p_idFascicolo.ToString(),
                                                                                      p_parametriPortale.path_upload_file,
                                                                                      p_parametriPortale.path_fascicoli) + ".pdf";

            v_pathWithFile = v_pathWithFile.Replace(p_parametriPortale.path_upload_file, string.Empty).Replace("\\\\", "\\");

            return FileBDRWInfoHelper.AllegaFile(v_pathWithFile, p_parametriPortale, p_DB).Item1;
        }
    }
}
#endif

