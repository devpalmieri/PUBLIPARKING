using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabDettRiscossioneBD : EntityBD<tab_dett_riscossione>
    {
        public TabDettRiscossioneBD()
        {

        }

        /// <summary>
        /// Restituisce la lista di tutte le entità
        /// </summary>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <param name="p_includeEntities">Elenco di tabelle collegate da includere durante la select</param>
        /// <returns></returns>
        public static IQueryable<tab_dett_riscossione> GetList(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetListInternal(p_dbContext).Where(d => p_dbContext.idContribuenteDefaultList.Count == 0 || (d.id_contribuente != null && p_dbContext.idContribuenteDefaultList.Contains(d.id_contribuente.Value)));
        }

        /// <summary>
        /// Restituisce l'entità a partire dalla chiave primaria
        /// </summary>
        /// <param name="p_id">Chiave primaria</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static tab_dett_riscossione GetById(Int32 p_id, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c => c.id_tab_dett_riscossione == p_id);
        }

        /// <summary>
        /// Restituisce la lista di tutte le entità
        /// </summary>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <param name="p_includeEntities">Elenco di tabelle collegate da includere durante la select</param>
        /// <returns></returns>
        public static IQueryable<tab_dett_riscossione> GetListAttByidMovIdAvv(int p_idMovPag, int p_idAvvPag, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.id_mov_pag == p_idMovPag && c.id_avv_pag == p_idAvvPag && c.cod_stato.StartsWith(CodStato.ATT)).OrderBy(o => o.id_tab_dett_riscossione);
        }

        /// <summary>
        /// Restituisce la lista di tutte le entità
        /// </summary>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <param name="p_includeEntities">Elenco di tabelle collegate da includere durante la select</param>
        /// <returns></returns>
        public static IQueryable<tab_dett_riscossione> GetListAnnullaDettRiscossione(int p_idEnte, int p_idCcRiscossione, int p_idListaRiscoss, decimal p_idContribuente, int p_idMovPag, int p_idAvvPag, int p_idTipoAvvPag, DateTime p_dataAccredito, DateTime p_dataContabile, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.id_ente == p_idEnte && c.id_tab_cc_riscossione == p_idCcRiscossione && c.id_lista_riscossione == p_idListaRiscoss && c.id_contribuente == p_idContribuente && c.id_mov_pag == p_idMovPag && c.id_avv_pag == p_idAvvPag && c.id_tipo_avv_pag == p_idTipoAvvPag && c.data_accredito_pagamento == p_dataAccredito && c.data_contabile_pagamento == p_dataContabile && c.id_tipo_avv_pag_rendicontazione == 0).OrderBy(o => o.id_tab_dett_riscossione);
        }

        /// <summary>
        /// Restituisce la lista di tutte le entità
        /// </summary>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <param name="p_includeEntities">Elenco di tabelle collegate da includere durante la select</param>
        /// <returns></returns>
        public static IQueryable<tab_dett_riscossione> GetListAttByidMovNotIdAvv(int p_idMovPag, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.id_mov_pag == p_idMovPag && (c.id_avv_pag == null || c.id_avv_pag == 0) && c.cod_stato.StartsWith(CodStato.ATT)).OrderBy(o => o.id_tab_dett_riscossione);
        }

        public static tab_dett_riscossione CreaDettRiscossione(tab_mov_pag p_movpag, int p_idEntrata,int p_annoRifAppo, int p_idTipoAvvPagAppo, string p_codTipoAvvPagAppo,
                                                               int p_idTipoVoceContribuzioneAppo,string p_flagSegnoAppo, string p_fonteAppo,
                                                               decimal p_importoAppo,decimal p_imponibileAppo, decimal p_ivaAppo,dbEnte p_dbContext)
        {

            tab_tipo_voce_contribuzione v_tipoVoce = TabTipoVoceContribuzioneBD.GetById(p_idTipoVoceContribuzioneAppo, p_dbContext);
            anagrafica_tipo_avv_pag v_rowTipoAvvPag = AnagraficaTipoAvvPagBD.GetByIdTipoAvvPag(p_idTipoAvvPagAppo, p_dbContext);

            tab_dett_riscossione dett_Riscossione = new tab_dett_riscossione()
            {
                id_ente = p_movpag.tab_cc_riscossione.id_ente.Value,
                id_ente_gestito = null,
                id_entrata = p_idEntrata,
                id_contribuente = p_movpag.id_contribuente,
                id_avv_pag = 0,
                id_mov_pag = p_movpag.id_tab_mov_pag,
                id_tab_contribuzione = null,
                id_avv_pag_iniziale = 0,
                id_avv_pag_collegato = null,
                id_avv_pag_intermedio = null,
                id_tab_cc_riscossione = p_movpag.id_tab_cc_riscossione.Value,
                anno_rif = p_annoRifAppo,
                mese_rif = null,
                id_tipo_avv_pag = p_idTipoAvvPagAppo,
                id_tipo_avv_pag_rendicontazione = p_idTipoAvvPagAppo,
                cod_tipo_avv_pag = p_codTipoAvvPagAppo,
                id_tipo_voce_contribuzione = p_idTipoVoceContribuzioneAppo,
                cod_tipo_voce_contribuzione = v_tipoVoce.codice_tipo_voce,
                flag_fonte = null,
                id_stato_mov_pag = p_movpag.id_stato,
                cod_stato_mov_pag = p_movpag.cod_stato,
                data_accredito_pagamento = p_movpag.data_accredito.Value,                
                flag_segno = p_flagSegnoAppo,
                id_rendiconto = null,
                id_stato = tab_dett_riscossione.ATT_ATT_ID,
                cod_stato = tab_dett_riscossione.ATT_ATT,
                id_rendiconto_new = null,
                id_tipo_avv_pag_riscosso = p_idTipoAvvPagAppo,
                id_tipo_avv_pag_iniziale = p_idTipoAvvPagAppo,
                id_tipo_avv_pag_collegato = null,
                id_tipo_avv_pag_intermedio = null,
                fonte_avv_pag_riscosso = p_fonteAppo,
                fonte_avv_pag_iniziale = p_fonteAppo,
                fonte_avv_pag_collegato = null,
                fonte_avv_pag_intermedio = null,
                dt_emi_avv_pag_riscosso = null,
                dt_emi_avv_pag_iniziale = null,
                dt_emi_avv_pag_collegato = null,
                dt_emi_avv_pag_intermedio = null,
                id_contratto_avv_pag_riscosso = 0,
                id_contratto_avv_pag_iniziale = 0,
                id_contratto_avv_pag_collegato = null,
                id_contratto_avv_pag_intermedio = null,
                flag_doppio_aggio_avv_pag_riscosso = v_rowTipoAvvPag != null ?  v_rowTipoAvvPag.flag_doppio_aggio: string.Empty,
                flag_doppio_aggio_avv_pag_iniziale = v_rowTipoAvvPag != null ? v_rowTipoAvvPag.flag_doppio_aggio : string.Empty,
                flag_doppio_aggio_avv_pag_collegato = null,
                flag_doppio_aggio_avv_pag_intermedio = null                
        };


          

            if (p_flagSegnoAppo == tab_dett_riscossione.FLAG_SEGNO_POSITVO)
            {
                dett_Riscossione.importo_totale_riscosso = p_importoAppo;
                dett_Riscossione.imponibile_riscosso = p_imponibileAppo;
                dett_Riscossione.iva_riscossa = p_ivaAppo;
            }
            else
            {
                dett_Riscossione.importo_totale_riscosso = -p_importoAppo;
                dett_Riscossione.imponibile_riscosso = -p_imponibileAppo;
                dett_Riscossione.iva_riscossa = -p_ivaAppo;
            }
            if (p_movpag.data_valuta == null && p_movpag.data_operazione == null)
            {
                dett_Riscossione.data_contabile_pagamento = p_movpag.data_accredito.Value;
            }
            if (p_movpag.data_valuta != null && p_movpag.data_operazione != null)
            {
                if (p_movpag.data_valuta.Value > p_movpag.data_operazione.Value)
                {
                    dett_Riscossione.data_contabile_pagamento = p_movpag.data_operazione.Value;
                }
                else
                {
                    dett_Riscossione.data_contabile_pagamento = p_movpag.data_valuta.Value;
                }
                if (p_movpag.data_valuta.Value > p_movpag.data_accredito.Value)
                {
                    dett_Riscossione.data_contabile_pagamento = p_movpag.data_accredito.Value;
                }
                else
                {
                    dett_Riscossione.data_contabile_pagamento = p_movpag.data_valuta.Value;
                }
                if (p_movpag.data_operazione.Value > p_movpag.data_accredito.Value)
                {
                    dett_Riscossione.data_contabile_pagamento = p_movpag.data_accredito.Value;
                }
                else
                {
                    dett_Riscossione.data_contabile_pagamento = p_movpag.data_operazione.Value;
                }
            }

            if (v_tipoVoce != null)
            {
                dett_Riscossione.id_entrata_riscossa = v_tipoVoce.id_entrata_new;
            }
            else
            {
                dett_Riscossione.id_entrata_riscossa = v_rowTipoAvvPag.id_entrata;
            }

            //tab_tipo_voce_contribuzione v_rowVoceContribuzione = TabTipoVoceContribuzioneBD.GetByIdTipoVoceContribuzione(tab_tipo_voce_contribuzione.ID_ENTRATA_ICI, p_dbContext);
            dett_Riscossione.sigla_tipo_voce_contrib = v_tipoVoce.codice_tributo_ministeriale.Trim();// tab_tipo_voce_contribuzione.CODICE_ENT;

            p_dbContext.tab_dett_riscossione.Add(dett_Riscossione);

            return dett_Riscossione;     
        }


    }
}
