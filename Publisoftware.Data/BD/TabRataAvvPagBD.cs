using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.LinqExtended;
using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;

namespace Publisoftware.Data.BD
{
    public class TabRataAvvPagBD : EntityBD<tab_rata_avv_pag>
    {
        public TabRataAvvPagBD()
        {

        }

        /// <summary>
        /// Controlla se l'elenco delle rate relativo ad un avviso è mostrabile
        /// </summary>
        /// <param name="p_idAvvPag"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static bool ShowElencoRate(int p_idAvvPag, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).WhereByIdAvvPagSenzaRataUnica(p_idAvvPag).Count() > 0;
        }
        //WhereByIdRata
        public static IQueryable<tab_rata_avv_pag> GetRataByIUV(string p_iuv, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).WhereByIUV(p_iuv);
        }

        /// <summary>
        /// Controlla se la rata unica relativa ad un avviso è mostrabile
        /// </summary>
        /// <param name="p_idAvvPag"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static bool ShowRataUnica(int p_idAvvPag, dbEnte p_dbContext)
        {
            if (GetList(p_dbContext).WhereByIdAvvPagSoloRataUnica(p_idAvvPag).Count() > 0)
            {
                if (ShowElencoRate(p_idAvvPag, p_dbContext))
                {
                    if (PagamentoRataUnicaAttivo(p_idAvvPag, p_dbContext))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Controlla se è possibile il pagamento in un'unica soluzione
        /// </summary>
        /// <param name="p_idAvvPag"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static bool PagamentoRataUnicaAttivo(int p_idAvvPag, dbEnte p_dbContext)
        {
            if (GetList(p_dbContext).WhereByIdAvvPagSenzaRataUnica(p_idAvvPag).Sum(d => d.imp_pagato) > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static tab_rata_avv_pag creaRataPArk(tab_avv_pag p_tab_avv_pag, decimal p_importo, DateTime p_emissioneVerbale, Int32 p_idRisorsa, Int32 p_idStrutturaStato, dbEnte p_context)
        {

            tab_rata_avv_pag v_rataAvvPag = p_context.tab_rata_avv_pag.Create();

            v_rataAvvPag.tab_avv_pag = p_tab_avv_pag;
            v_rataAvvPag.num_rata = 0;
            v_rataAvvPag.imp_tot_rata = p_importo;
            v_rataAvvPag.flag_pagamento = "0";
            v_rataAvvPag.cod_stato = tab_rata_avv_pag.ATT_ATT;
            v_rataAvvPag.id_stato = tab_rata_avv_pag.ATT_ATT_ID;
            v_rataAvvPag.data_stato = p_emissioneVerbale;
            v_rataAvvPag.id_struttura_stato = p_idStrutturaStato;
            v_rataAvvPag.id_risorsa_stato = p_idRisorsa;


            p_context.tab_rata_avv_pag.Add(v_rataAvvPag);

            return v_rataAvvPag;
        }
        public static bool UpdateStatoRata(int id_rata, int id_stato, string cod_stato, dbEnte p_context)
        {
            tab_rata_avv_pag v_rataAvvPag = GetList(p_context).Where(d => d.id_rata_avv_pag == id_rata).FirstOrDefault();
            if (v_rataAvvPag != null)
            {
                v_rataAvvPag.id_risorsa_stato = id_stato;
                v_rataAvvPag.cod_stato = cod_stato;
                p_context.SaveChanges();
                return true;
            }
            return false;
        }
        public async static Task<bool> UpdateStatoRataAsync(int id_rata, int id_stato, string cod_stato, dbEnte p_context)
        {
            tab_rata_avv_pag v_rataAvvPag = await GetList(p_context).Where(d => d.id_rata_avv_pag == id_rata).FirstOrDefaultAsync();
            if (v_rataAvvPag != null)
            {
                v_rataAvvPag.id_risorsa_stato = id_stato;
                v_rataAvvPag.cod_stato = cod_stato;
                p_context.SaveChanges();
                return true;
            }
            return false;
        }
        public static bool UpdateStatoRataByAvviso(int id_avviso, int id_stato, string cod_stato, dbEnte p_context)
        {
            List<tab_rata_avv_pag> v_rataList = GetList(p_context).Where(d => d.id_tab_avv_pag == id_avviso).ToList();
            if (v_rataList != null)
            {
                if (v_rataList.Count() > 0)
                {
                    foreach (tab_rata_avv_pag avv in v_rataList)
                    {
                        avv.id_risorsa_stato = id_stato;
                        avv.cod_stato = cod_stato;
                    }
                    p_context.SaveChanges();

                }

                return true;
            }
            return false;
        }
        public async static Task<bool> UpdateStatoRataByAvvisoAsync(int id_avviso, int id_stato, string cod_stato, dbEnte p_context)
        {
            List<tab_rata_avv_pag> v_rataList = await GetList(p_context).Where(d => d.id_tab_avv_pag == id_avviso).ToListAsync();
            if (v_rataList != null)
            {
                if (v_rataList.Count() > 0)
                {
                    foreach (tab_rata_avv_pag avv in v_rataList)
                    {
                        avv.id_risorsa_stato = id_stato;
                        avv.cod_stato = cod_stato;
                    }
                    p_context.SaveChanges();

                }

                return true;
            }
            return false;
        }
        public static bool UpdateStatoRataByListAvvisi(List<int> idsList, int id_stato, string cod_stato, dbEnte p_context)
        {
            List<tab_rata_avv_pag> v_rataList = GetList(p_context).Where(d => idsList.Contains(d.id_tab_avv_pag)).ToList();
            if (v_rataList != null)
            {
                if (v_rataList.Count() > 0)
                {
                    foreach (tab_rata_avv_pag avv in v_rataList)
                    {
                        avv.id_risorsa_stato = id_stato;
                        avv.cod_stato = cod_stato;
                    }
                    p_context.SaveChanges();

                }

                return true;
            }
            return false;
        }
        public async static Task<tab_rata_avv_pag> GetRataByIdAsync(int p_idRata, dbEnte p_dbContext)
        {
            return await GetList(p_dbContext).Where(r => r.id_rata_avv_pag.Equals(p_idRata)).FirstOrDefaultAsync();
        }

    }
}
