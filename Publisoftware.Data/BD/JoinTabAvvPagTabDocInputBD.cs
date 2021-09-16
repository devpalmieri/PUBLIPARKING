using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.LinqExtended;
using System.Data.Entity;

namespace Publisoftware.Data.BD
{
    public class JoinTabAvvPagTabDocInputBD : EntityBD<join_tab_avv_pag_tab_doc_input>
    {
        public JoinTabAvvPagTabDocInputBD()
        {

        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> GetIstanzeByRicercaIstanze(string codiceIstanzaRicerca,
                                                                                            string codiceAvvisoRicerca,
                                                                                            DateTime? daIstanzaRicerca,
                                                                                            DateTime? aIstanzaRicerca,
                                                                                            string RGRRicerca,
                                                                                            string NumeroSentenzaRicerca,
                                                                                            int? idStatoDoc,
                                                                                            int idTipoDoc,
                                                                                            int? idTipoAvvpag,
                                                                                            int idTipoRateizzazione,
                                                                                            int idTipoDocEntrate,
                                                                                            List<int> idStatoList,
                                                                                            dbEnte p_dbContext)
        {
            IQueryable<join_tab_avv_pag_tab_doc_input> v_istanzeList = GetListInternal(p_dbContext).Where(d => d.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc == idTipoDoc &&
                                                                                                              !d.cod_stato.StartsWith(anagrafica_stato_doc.STATO_ANNULLATO));

            if (idTipoRateizzazione != -1)
            {
                if (idTipoRateizzazione == anagrafica_tipo_avv_pag.RATEIZZAZIONE_SINGOLA)
                {
                    v_istanzeList = v_istanzeList.Where(d => d.tab_avv_pag_fatt_emissione.id_tipo_avvpag == idTipoRateizzazione || d.tab_avv_pag_fatt_emissione.id_tipo_avvpag == anagrafica_tipo_avv_pag.RATEIZZAZIONE_SINGOLA_OLTRETERMINI);
                }
                else
                {
                    v_istanzeList = v_istanzeList.Where(d => d.tab_avv_pag_fatt_emissione.id_tipo_avvpag == idTipoRateizzazione);
                }
            }

            if (idTipoDocEntrate != -1)
            {
                v_istanzeList = v_istanzeList.Where(d => d.tab_doc_input.id_tipo_doc_entrate == idTipoDocEntrate);
            }

            if (!string.IsNullOrEmpty(codiceIstanzaRicerca))
            {
                v_istanzeList = v_istanzeList.Where(d => d.tab_doc_input.identificativo_doc_input.Trim().Equals(codiceIstanzaRicerca) ||
                                                         d.tab_doc_input.identificativo_doc_input.Trim().Equals(codiceIstanzaRicerca + "/R"));

            }
            else if (!string.IsNullOrEmpty(codiceAvvisoRicerca))
            {
                string p_identificativo2 = !string.IsNullOrEmpty(codiceAvvisoRicerca) ? codiceAvvisoRicerca.Replace("/", string.Empty).Replace("-", string.Empty).Trim() : string.Empty;
                string v_codice = string.Empty;
                string v_anno = string.Empty;
                string v_progressivo = string.Empty;

                if (!string.IsNullOrEmpty(p_identificativo2))
                {
                    v_codice = p_identificativo2.Substring(0, 4);
                    v_anno = p_identificativo2.Substring(4, 4);
                    if (p_identificativo2.Substring(8).All(char.IsDigit))
                    {
                        v_progressivo = Convert.ToInt32(p_identificativo2.Substring(8)).ToString();
                    }
                }

                v_istanzeList = v_istanzeList.Where(d => d.tab_doc_input.join_tab_avv_pag_tab_doc_input.Any(x => !x.cod_stato.StartsWith(anagrafica_stato_doc.STATO_ANNULLATO) &&
                                                                                                                 (x.tab_avv_pag.identificativo_avv_pag.Trim() == codiceAvvisoRicerca ||
                                                                                                                 (x.tab_avv_pag.anagrafica_tipo_avv_pag.cod_tipo_avv_pag == v_codice &&
                                                                                                                  x.tab_avv_pag.anno_riferimento == v_anno &&
                                                                                                                  x.tab_avv_pag.numero_avv_pag == v_progressivo))));
            }
            else if (!string.IsNullOrEmpty(RGRRicerca) && idTipoDoc == tab_tipo_doc_entrate.TIPO_DOC_RICORSI)
            {
                v_istanzeList = v_istanzeList.Where(d => d.tab_doc_input.tab_ricorsi.FirstOrDefault() != null &&
                                                         d.tab_doc_input.tab_ricorsi.FirstOrDefault().rgr == RGRRicerca);
            }
            else if (!string.IsNullOrEmpty(NumeroSentenzaRicerca) && idTipoDoc == tab_tipo_doc_entrate.TIPO_DOC_RICORSI)
            {
                v_istanzeList = v_istanzeList.Where(d => d.tab_doc_input.tab_ricorsi.FirstOrDefault() != null &&
                                                         d.tab_doc_input.tab_ricorsi.FirstOrDefault().tab_sentenze.FirstOrDefault() != null &&
                                                         d.tab_doc_input.tab_ricorsi.FirstOrDefault().tab_sentenze.FirstOrDefault().numero_sentenza == NumeroSentenzaRicerca);
            }
            else
            {
                if (daIstanzaRicerca.HasValue)
                {
                    daIstanzaRicerca = daIstanzaRicerca.Value;
                    v_istanzeList = v_istanzeList.Where(d => d.tab_doc_input.data_presentazione >= daIstanzaRicerca);
                }

                if (aIstanzaRicerca.HasValue)
                {
                    aIstanzaRicerca = aIstanzaRicerca.Value.AddHours(23).AddMinutes(59);
                    v_istanzeList = v_istanzeList.Where(d => d.tab_doc_input.data_presentazione <= aIstanzaRicerca);
                }

                if (idStatoDoc.HasValue)
                {
                    v_istanzeList = v_istanzeList.Where(d => d.tab_doc_input.id_stato == idStatoDoc);
                }
                else
                {
                    v_istanzeList = v_istanzeList.Where(d => idStatoList.Contains(d.tab_doc_input.id_stato));
                }

                if (idTipoAvvpag.HasValue)
                {
                    v_istanzeList = v_istanzeList.Where(d => d.tab_avv_pag1.id_tipo_avvpag == idTipoAvvpag);
                }
            }

            if (!string.IsNullOrEmpty(NumeroSentenzaRicerca) &&
                NumeroSentenzaRicerca == "-1")
            {
                v_istanzeList = v_istanzeList.Where(d => d.tab_doc_input.tab_ricorsi.FirstOrDefault() != null &&
                                                         d.tab_doc_input.tab_ricorsi.FirstOrDefault().tab_sentenze.FirstOrDefault() != null);
            }

            return v_istanzeList;
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> GetMotivazioniByRicercaIstanze(string codiceIstanzaRicerca, DateTime? daIstanzaRicerca, DateTime? aIstanzaRicerca, int? idStatoDoc, int idTipoDoc, int? idTipoAvvpag, int idTipoRateizzazione, List<int> idStatoList, dbEnte p_dbContext)
        {
            IQueryable<join_tab_avv_pag_tab_doc_input> v_istanzeList = GetListInternal(p_dbContext).Where(d => d.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc == idTipoDoc);

            if (idTipoRateizzazione != -1)
            {
                v_istanzeList = v_istanzeList.Where(d => d.tab_avv_pag_fatt_emissione.id_tipo_avvpag == idTipoRateizzazione);
            }

            if (!string.IsNullOrEmpty(codiceIstanzaRicerca))
            {
                v_istanzeList = v_istanzeList.Where(d => d.tab_doc_input.identificativo_doc_input.Trim() == codiceIstanzaRicerca);
            }
            else
            {
                if (daIstanzaRicerca.HasValue)
                {
                    v_istanzeList = v_istanzeList.Where(d => d.tab_doc_input.data_presentazione >= daIstanzaRicerca);
                }

                if (aIstanzaRicerca.HasValue)
                {
                    v_istanzeList = v_istanzeList.Where(d => d.tab_doc_input.data_presentazione <= aIstanzaRicerca);
                }

                if (idStatoDoc.HasValue)
                {
                    v_istanzeList = v_istanzeList.Where(d => d.id_stato == idStatoDoc);
                }
                else
                {
                    v_istanzeList = v_istanzeList.Where(d => idStatoList.Contains(d.id_stato));
                }

                if (idTipoAvvpag.HasValue)
                {
                    v_istanzeList = v_istanzeList.Where(d => d.tab_avv_pag1.id_tipo_avvpag == idTipoAvvpag);
                }
            }

            return v_istanzeList;
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> GetIstanzeByContribuente(decimal p_idcontribuente, DateTime? daIstanzaRicerca, DateTime? aIstanzaRicerca, int idTipoDoc, int idTipoDocEntrate, int idTipoRateizzazione, List<int> idStatoList, dbEnte p_dbContext)
        {
            IQueryable<join_tab_avv_pag_tab_doc_input> v_istanzeList = GetListInternal(p_dbContext).Where(d => d.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc == idTipoDoc && d.tab_avv_pag.id_anag_contribuente == p_idcontribuente);
            v_istanzeList = v_istanzeList.Where(d => idStatoList.Contains(d.tab_doc_input.id_stato));
            if (idTipoRateizzazione != -1)
            {
                if (idTipoRateizzazione == anagrafica_tipo_avv_pag.RATEIZZAZIONE_SINGOLA)
                {
                    v_istanzeList = v_istanzeList.Where(d => d.tab_avv_pag_fatt_emissione.id_tipo_avvpag == idTipoRateizzazione || d.tab_avv_pag_fatt_emissione.id_tipo_avvpag == anagrafica_tipo_avv_pag.RATEIZZAZIONE_SINGOLA_OLTRETERMINI);
                }
                else
                {
                    v_istanzeList = v_istanzeList.Where(d => d.tab_avv_pag_fatt_emissione.id_tipo_avvpag == idTipoRateizzazione);
                }
            }
            if (idTipoDocEntrate != -1)
            {
                v_istanzeList = v_istanzeList.Where(d => d.tab_doc_input.id_tipo_doc_entrate == idTipoDocEntrate);
            }

            if (daIstanzaRicerca.HasValue)
            {
                daIstanzaRicerca = daIstanzaRicerca.Value;
                v_istanzeList = v_istanzeList.Where(d => d.tab_doc_input.data_presentazione >= daIstanzaRicerca);
            }

            if (aIstanzaRicerca.HasValue)
            {
                aIstanzaRicerca = aIstanzaRicerca.Value.AddHours(23).AddMinutes(59);
                v_istanzeList = v_istanzeList.Where(d => d.tab_doc_input.data_presentazione <= aIstanzaRicerca);
            }



            return v_istanzeList;
        }

    }
}
