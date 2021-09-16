using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabConsuntivoDettListaBD : EntityBD<tab_consuntivo_dett_lista_carico>
    {
        public TabConsuntivoDettListaBD()
        { 

        }

        public static IQueryable<tab_consuntivo_dett_lista_carico> GetDettagliFor(IQueryable<tab_consuntivo_lista_carico> p_consuntivi, dbEnte p_dbContext)
        {
            return p_consuntivi.SelectMany(c => c.tab_consuntivo_dett_lista_carico);
        }
        public static IEnumerable<tab_consuntivo_dett_lista_carico> GetListDettagliFor(IEnumerable<tab_consuntivo_lista_carico> p_consuntivi, dbEnte p_dbContext)
        {
            return p_consuntivi.SelectMany(c => c.tab_consuntivo_dett_lista_carico);
        }
        /// <summary>
        /// Ritorna il dettaglio dei consuntivi in input il cui tipo voce di contribuzione va a riversarsi sull'ente stesso o su altri enti in funzione del flag dato in input
        /// </summary>
        /// <param name="p_consuntivi">consuntivi lista di interesse</param>
        /// <param name="p_is_ente_stesso">dettagli riversati su ente stesso</param>
        /// <param name="p_dbContext">Ctx</param>
        /// <returns></returns>
        public static IEnumerable<tab_consuntivo_dett_lista_carico> GetDettaglioByRiversamento(
            IQueryable<tab_consuntivo_lista_carico> p_consuntivi,int p_id_ente, bool p_is_ente_stesso, dbEnte p_dbContext, int id_lista, bool isListaDiTrasmissione)
        {
            // Recupero tipi voce di interesse in funzione dell'ente di riversamento
            IQueryable<tab_riversamenti_contrattuali> riversamenti_di_interesse = p_is_ente_stesso 
                ? TabRiversamentiContrattualiBD.GetRiversamentiSuStessoEnte(p_id_ente, p_dbContext)
                : TabRiversamentiContrattualiBD.GetRiversamentiSuAltroEnte(p_id_ente, p_dbContext);

#if DEBUG
            var dddList = riversamenti_di_interesse.ToList();
#endif

            IQueryable<tab_tipo_voce_contribuzione> tipiVoceContrib_di_interesse = TabTipoVoceContribuzioneBD.GetListByIdEntrate(
                riversamenti_di_interesse.Select(r => r.id_entrata_riscossa ?? -1).Distinct(), p_dbContext);

            //Upcast necessario per confronto anni (string vs int)
            IQueryable<tab_consuntivo_dett_lista_carico> consuntivi_dett_query = GetDettagliFor(p_consuntivi, p_dbContext);
            if (isListaDiTrasmissione)
            {
                consuntivi_dett_query = consuntivi_dett_query.Where(x => x.id_lista_collegata == id_lista || x.id_lista_collegata == null);
            }
            else
            {
                consuntivi_dett_query = consuntivi_dett_query.Where(x => x.id_lista_collegata == id_lista);
            }
            IEnumerable<tab_consuntivo_dett_lista_carico> p_consuntivi_dett = consuntivi_dett_query
                .AsEnumerable();
#if DEBUG

            IList<tab_consuntivo_dett_lista_carico> p_con_tutti = GetDettagliFor(p_consuntivi, p_dbContext)
                .ToList();
            var lllSoloCollegata = p_consuntivi_dett.ToList();

            var tipiVoceContrib_di_interesseList = tipiVoceContrib_di_interesse.ToList();
            var riversamenti_di_interesseList = riversamenti_di_interesse.ToList();
#endif

            p_consuntivi_dett = p_consuntivi_dett.Where(dc =>
                //dettagli relativi ai tipi voce selezionati
                dc.id_tipo_voce_contribuzione.HasValue && tipiVoceContrib_di_interesse.Select(tv => tv.id_tipo_voce_contribuzione).Contains(dc.id_tipo_voce_contribuzione.Value)
                &&
                //Recupero tab_riversamenti relativo a tipo_voce_contrib di dettaglio corrente
                riversamenti_di_interesse.FirstOrDefault(r => r.anagrafica_entrate.tab_tipo_voce_contribuzione1.Any(tv => tv.id_tipo_voce_contribuzione == dc.id_tipo_voce_contribuzione)) != null
                //riversamenti_di_interesse.Any(r => r.anagrafica_entrate.tab_tipo_voce_contribuzione1.Any(tv => tv.id_tipo_voce_contribuzione == dc.id_tipo_voce_contribuzione) && r.Anno_rif_entrata_da <= Convert.ToInt32(dc.anno_rif) && r.Anno_rif_entrata_a >= Convert.ToInt32(dc.anno_rif))
                //&&
                ////Verifica date di riferimento: dett_consuntivo.anno_rif in [tab_riversamenti.Anno_rif_entrata_da, tab_riversamenti.Anno_rif_entrata_a]
                //riversamenti_di_interesse.FirstOrDefault(r => r.anagrafica_entrate.tab_tipo_voce_contribuzione1.Any(tv => tv.id_tipo_voce_contribuzione == dc.id_tipo_voce_contribuzione)).Anno_rif_entrata_da <= Convert.ToInt32(dc.anno_rif)
                //&&
                //riversamenti_di_interesse.FirstOrDefault(r => r.anagrafica_entrate.tab_tipo_voce_contribuzione1.Any(tv => tv.id_tipo_voce_contribuzione == dc.id_tipo_voce_contribuzione)).Anno_rif_entrata_a >= Convert.ToInt32(dc.anno_rif)
            );

            return p_consuntivi_dett;
        }

        public static IEnumerable<tab_consuntivo_dett_lista_carico> GetListDettaglioByRiversamento(
    IEnumerable<tab_consuntivo_lista_carico> p_consuntivi, int p_id_ente, bool p_is_ente_stesso, dbEnte p_dbContext, int id_lista, bool isListaDiTrasmissione)
        {
            // Recupero tipi voce di interesse in funzione dell'ente di riversamento
            IQueryable<tab_riversamenti_contrattuali> riversamenti_di_interesse = p_is_ente_stesso
                ? TabRiversamentiContrattualiBD.GetRiversamentiSuStessoEnte(p_id_ente, p_dbContext)
                : TabRiversamentiContrattualiBD.GetRiversamentiSuAltroEnte(p_id_ente, p_dbContext);

#if DEBUG
            var dddList = riversamenti_di_interesse.ToList();
#endif

            IQueryable<tab_tipo_voce_contribuzione> tipiVoceContrib_di_interesse = TabTipoVoceContribuzioneBD.GetListByIdEntrate(
                riversamenti_di_interesse.Select(r => r.id_entrata_riscossa ?? -1).Distinct(), p_dbContext);

            //Upcast necessario per confronto anni (string vs int)
            IEnumerable<tab_consuntivo_dett_lista_carico> consuntivi_dett_query = GetListDettagliFor(p_consuntivi, p_dbContext);
            if (isListaDiTrasmissione)
            {
                consuntivi_dett_query = consuntivi_dett_query.Where(x => x.id_lista_collegata == id_lista || x.id_lista_collegata == null);
            }
            else
            {
                consuntivi_dett_query = consuntivi_dett_query.Where(x => x.id_lista_collegata == id_lista);
            }
            IEnumerable<tab_consuntivo_dett_lista_carico> p_consuntivi_dett = consuntivi_dett_query
                .AsEnumerable();
#if DEBUG

            IList<tab_consuntivo_dett_lista_carico> p_con_tutti = GetListDettagliFor(p_consuntivi, p_dbContext)
                .ToList();
            var lllSoloCollegata = p_consuntivi_dett.ToList();

            var tipiVoceContrib_di_interesseList = tipiVoceContrib_di_interesse.ToList();
            var riversamenti_di_interesseList = riversamenti_di_interesse.ToList();
#endif

            p_consuntivi_dett = p_consuntivi_dett.Where(dc =>
                //dettagli relativi ai tipi voce selezionati
                dc.id_tipo_voce_contribuzione.HasValue && tipiVoceContrib_di_interesse.Select(tv => tv.id_tipo_voce_contribuzione).Contains(dc.id_tipo_voce_contribuzione.Value)
                &&
                //Recupero tab_riversamenti relativo a tipo_voce_contrib di dettaglio corrente
                riversamenti_di_interesse.FirstOrDefault(r => r.anagrafica_entrate.tab_tipo_voce_contribuzione1.Any(tv => tv.id_tipo_voce_contribuzione == dc.id_tipo_voce_contribuzione)) != null
            );

            return p_consuntivi_dett;
        }

    }
}
