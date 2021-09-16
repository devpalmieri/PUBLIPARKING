using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public static class MyExtensions
    {
        private static List<string> exceptProperties(String p_typeOrigine, String p_typeDestinazione)
        {
            List<String> risp = new List<string>();

            if (p_typeOrigine == "tab_avv_pag_fatt_emissione")
            {
                if (p_typeDestinazione == "tab_avv_pag")
                {
                    risp = new List<String> { "tab_unita_contribuzione", "tab_unita_contribuzione_fatt_emissione" };
                }
            }
            else if (p_typeOrigine == "tab_unita_contribuzione_fatt_emissione")
            {
                if (p_typeDestinazione == "tab_unita_contribuzione")
                {
                    risp = new List<String> { "tab_avv_pag" };
                }
            }

            return risp;
        }

        public static void setProperties(this Object destinazione, Object model, String[] arrayProperties, bool p_ancheNull = true)
        {
            foreach (String property in arrayProperties)
            {
                PropertyInfo propDest = destinazione.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
                PropertyInfo propOrig = model.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);

                if ((null != propDest && propDest.CanWrite) && (null != propOrig && propOrig.CanWrite) && (p_ancheNull || propOrig.GetValue(model) != null))
                {
                    propDest.SetValue(destinazione, propOrig.GetValue(model), null);
                }
            }
        }

        /// <summary>
        /// Ricopia le proprietà, dei soli tipi elementari, che sono comuni alle entità in input (evitando che siano caricati nuovi dati dal DB)
        /// </summary>
        /// <param name="destinazione"></param>
        /// <param name="model"></param>
        /// <param name="ctx"></param>
        // [Obsolete("SPERIMENTALE - DA TESTARE")]
        public static void setEntityElementaryProperties(this Object destinazione, Object model, dbEnte ctx)
        {
            var oldState = ctx.Configuration.LazyLoadingEnabled;
            ctx.Configuration.LazyLoadingEnabled = false;
            destinazione.setProperties(model: model, soloPropElementari: true, p_ancheNull: true);
            ctx.Configuration.LazyLoadingEnabled = oldState;
        }

        public static void setProperties(this Object destinazione, Object model, bool soloPropElementari = false, bool p_ancheNull = true)
        {
            var typeOrigine = ObjectContext.GetObjectType(model.GetType());
            var typeDesctinazione = ObjectContext.GetObjectType(destinazione.GetType());
            List<String> v_exceptProperties = exceptProperties(typeOrigine.Name, typeDesctinazione.Name);

            if (typeOrigine == typeof(tab_unita_contribuzione) && typeDesctinazione == typeof(tab_unita_contribuzione_fatt_emissione))
            {
                ((tab_unita_contribuzione_fatt_emissione)destinazione).periodo_contribuzione_String = ((tab_unita_contribuzione)model).periodo_contribuzione_String;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).periodo_rif_String = ((tab_unita_contribuzione)model).periodo_rif_String;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_unita_contribuzione = ((tab_unita_contribuzione)model).id_unita_contribuzione;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_ente = ((tab_unita_contribuzione)model).id_ente;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_ente_gestito = ((tab_unita_contribuzione)model).id_ente_gestito.Value;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_entrata = ((tab_unita_contribuzione)model).id_entrata;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_tipo_avv_pag_generato = ((tab_unita_contribuzione)model).id_tipo_avv_pag_generato;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).num_riga_avv_pag_generato = ((tab_unita_contribuzione)model).num_riga_avv_pag_generato;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_anagrafica_voce_contribuzione = ((tab_unita_contribuzione)model).id_anagrafica_voce_contribuzione;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_tipo_voce_contribuzione = ((tab_unita_contribuzione)model).id_tipo_voce_contribuzione;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).flag_tipo_addebito = ((tab_unita_contribuzione)model).flag_tipo_addebito;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).anno_rif = ((tab_unita_contribuzione)model).anno_rif;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).periodo_rif_da = ((tab_unita_contribuzione)model).periodo_rif_da;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).periodo_rif_a = ((tab_unita_contribuzione)model).periodo_rif_a;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).num_giorni_contribuzione = ((tab_unita_contribuzione)model).num_giorni_contribuzione;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).periodo_contribuzione_da = ((tab_unita_contribuzione)model).periodo_contribuzione_da;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).periodo_contribuzione_a = ((tab_unita_contribuzione)model).periodo_contribuzione_a;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_contribuente = ((tab_unita_contribuzione)model).id_contribuente.Value;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_oggetto = ((tab_unita_contribuzione)model).id_oggetto;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_oggetto_contribuzione = ((tab_unita_contribuzione)model).id_oggetto_contribuzione;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_fatt_consumi = ((tab_unita_contribuzione)model).id_fatt_consumi;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_intervento = ((tab_unita_contribuzione)model).id_intervento;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_avv_pag_collegato = ((tab_unita_contribuzione)model).id_avv_pag_collegato;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_spesa = ((tab_unita_contribuzione)model).id_spesa;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).flag_collegamento_unita_contribuzione = ((tab_unita_contribuzione)model).flag_collegamento_unita_contribuzione;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_unita_contribuzione_collegato = ((tab_unita_contribuzione)model).id_unita_contribuzione_collegato;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).um_unita = ((tab_unita_contribuzione)model).um_unita;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).flag_segno = ((tab_unita_contribuzione)model).flag_segno;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).quantita_unita_contribuzione = ((tab_unita_contribuzione)model).quantita_unita_contribuzione.Value;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).importo_unitario_contribuzione = ((tab_unita_contribuzione)model).importo_unitario_contribuzione.HasValue ? ((tab_unita_contribuzione)model).importo_unitario_contribuzione.Value : 0;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).importo_unita_contribuzione = ((tab_unita_contribuzione)model).importo_unita_contribuzione.HasValue ? ((tab_unita_contribuzione)model).importo_unita_contribuzione.Value : 0;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).importo_ridotto = ((tab_unita_contribuzione)model).importo_ridotto;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).importo_sanzioni_eliminate_eredi = ((tab_unita_contribuzione)model).importo_sanzioni_eliminate_eredi;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).importo_tributo = ((tab_unita_contribuzione)model).importo_tributo;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_agevolazione1 = ((tab_unita_contribuzione)model).id_agevolazione1;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).imp_agevolazione1 = ((tab_unita_contribuzione)model).imp_agevolazione1;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).durata_agevolazione1 = ((tab_unita_contribuzione)model).durata_agevolazione1;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).cod_agevolazione1 = ((tab_unita_contribuzione)model).cod_agevolazione1;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_agevolazione2 = ((tab_unita_contribuzione)model).id_agevolazione2;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).imp_agevolazione2 = ((tab_unita_contribuzione)model).imp_agevolazione2;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).durata_agevolazione2 = ((tab_unita_contribuzione)model).durata_agevolazione2;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).cod_agevolazione2 = ((tab_unita_contribuzione)model).cod_agevolazione2;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_agevolazione3 = ((tab_unita_contribuzione)model).id_agevolazione3;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).imp_agevolazione3 = ((tab_unita_contribuzione)model).imp_agevolazione3;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).durata_agevolazione3 = ((tab_unita_contribuzione)model).durata_agevolazione3;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).cod_agevolazione3 = ((tab_unita_contribuzione)model).cod_agevolazione3;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_agevolazione4 = ((tab_unita_contribuzione)model).id_agevolazione4;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).imp_agevolazione4 = ((tab_unita_contribuzione)model).imp_agevolazione4;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).durata_agevolazione4 = ((tab_unita_contribuzione)model).durata_agevolazione4;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).cod_agevolazione4 = ((tab_unita_contribuzione)model).cod_agevolazione4;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).imponibile_unita_contribuzione = ((tab_unita_contribuzione)model).imponibile_unita_contribuzione.HasValue ? ((tab_unita_contribuzione)model).imponibile_unita_contribuzione.Value : 0;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).aliquota_iva = ((tab_unita_contribuzione)model).aliquota_iva;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).iva_unita_contribuzione = ((tab_unita_contribuzione)model).iva_unita_contribuzione;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).flag_val = ((tab_unita_contribuzione)model).flag_val;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_stato = ((tab_unita_contribuzione)model).id_stato;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).cod_stato = ((tab_unita_contribuzione)model).cod_stato;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).data_stato = ((tab_unita_contribuzione)model).data_stato;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_struttura_stato = ((tab_unita_contribuzione)model).id_struttura_stato;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_risorsa_stato = ((tab_unita_contribuzione)model).id_risorsa_stato;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).num_avv_pag = ((tab_unita_contribuzione)model).num_avv_pag;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).anno_origine = ((tab_unita_contribuzione)model).anno_origine;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).testo1 = ((tab_unita_contribuzione)model).testo1;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).testo2 = ((tab_unita_contribuzione)model).testo2;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_profilo_bene = ((tab_unita_contribuzione)model).id_profilo_bene;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_tipo_voce_origine = ((tab_unita_contribuzione)model).id_tipo_voce_origine;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_avv_pag_origine = ((tab_unita_contribuzione)model).id_avv_pag_origine;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).numero_accertamento_contabile = ((tab_unita_contribuzione)model).numero_accertamento_contabile;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).data_accertamento_contabile = ((tab_unita_contribuzione)model).data_accertamento_contabile;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_avv_pag_voce_da_recuperare = ((tab_unita_contribuzione)model).id_avv_pag_voce_da_recuperare;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_avv_pag_riferimento_voce = ((tab_unita_contribuzione)model).id_avv_pag_riferimento_voce;

                ((tab_unita_contribuzione_fatt_emissione)destinazione).id_tipo_avvpag_origine = ((tab_unita_contribuzione)model).id_tipo_avvpag_origine;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).descr_tipo_avvpag_origine = ((tab_unita_contribuzione)model).descr_tipo_avvpag_origine;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).identificativo_avvpag_origine = ((tab_unita_contribuzione)model).identificativo_avvpag_origine;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).data_notifica_avvpag_origine = ((tab_unita_contribuzione)model).data_notifica_avvpag_origine;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).imp_maggiorazione_onere_riscossione_121 = ((tab_unita_contribuzione)model).imp_maggiorazione_onere_riscossione_121;
                ((tab_unita_contribuzione_fatt_emissione)destinazione).imp_maggiorazione_onere_riscossione_61_90 = ((tab_unita_contribuzione)model).imp_maggiorazione_onere_riscossione_61_90;


            }
            else if (typeOrigine == typeof(tab_avv_pag) && typeDesctinazione == typeof(tab_avv_pag_fatt_emissione))
            {
                ((tab_avv_pag_fatt_emissione)destinazione).id_ente = ((tab_avv_pag)model).id_ente;
                ((tab_avv_pag_fatt_emissione)destinazione).id_ente_gestito = ((tab_avv_pag)model).id_ente_gestito;
                ((tab_avv_pag_fatt_emissione)destinazione).id_contratto = ((tab_avv_pag)model).id_contratto;
                ((tab_avv_pag_fatt_emissione)destinazione).id_entrata = ((tab_avv_pag)model).id_entrata;
                ((tab_avv_pag_fatt_emissione)destinazione).id_contribuente_old = ((tab_avv_pag)model).id_contribuente_old;
                ((tab_avv_pag_fatt_emissione)destinazione).id_anag_contribuente = ((tab_avv_pag)model).id_anag_contribuente;
                ((tab_avv_pag_fatt_emissione)destinazione).id_tipo_avvpag = ((tab_avv_pag)model).id_tipo_avvpag;
                ((tab_avv_pag_fatt_emissione)destinazione).id_stato_avv_pag = ((tab_avv_pag)model).id_stato_avv_pag;
                ((tab_avv_pag_fatt_emissione)destinazione).cod_stato_avv_pag = ((tab_avv_pag)model).cod_stato_avv_pag;
                ((tab_avv_pag_fatt_emissione)destinazione).dt_stato_avvpag = ((tab_avv_pag)model).dt_stato_avvpag;
                ((tab_avv_pag_fatt_emissione)destinazione).dt_emissione = ((tab_avv_pag)model).dt_emissione;
                ((tab_avv_pag_fatt_emissione)destinazione).anno_riferimento = ((tab_avv_pag)model).anno_riferimento;
                ((tab_avv_pag_fatt_emissione)destinazione).periodo_riferimento_da = ((tab_avv_pag)model).periodo_riferimento_da;
                ((tab_avv_pag_fatt_emissione)destinazione).periodo_riferimento_a = ((tab_avv_pag)model).periodo_riferimento_a;
                ((tab_avv_pag_fatt_emissione)destinazione).id_tab_contr_doc = ((tab_avv_pag)model).id_tab_contr_doc;
                ((tab_avv_pag_fatt_emissione)destinazione).numero_avv_pag = ((tab_avv_pag)model).numero_avv_pag;
                ((tab_avv_pag_fatt_emissione)destinazione).barcode = ((tab_avv_pag)model).barcode;
                ((tab_avv_pag_fatt_emissione)destinazione).flag_spedizione_notifica = ((tab_avv_pag)model).flag_spedizione_notifica;
                ((tab_avv_pag_fatt_emissione)destinazione).id_tipo_spedizione_notifica = ((tab_avv_pag)model).id_tipo_spedizione_notifica;
                ((tab_avv_pag_fatt_emissione)destinazione).tipo_spedizione_notifica = ((tab_avv_pag)model).tipo_spedizione_notifica;
                ((tab_avv_pag_fatt_emissione)destinazione).numero_raccomandata = ((tab_avv_pag)model).numero_raccomandata;
                ((tab_avv_pag_fatt_emissione)destinazione).flag_iter_recapito_notifica = ((tab_avv_pag)model).flag_iter_recapito_notifica;
                ((tab_avv_pag_fatt_emissione)destinazione).flag_esito_sped_notifica = ((tab_avv_pag)model).flag_esito_sped_notifica;
                ((tab_avv_pag_fatt_emissione)destinazione).id_tipo_esito_notifica = ((tab_avv_pag)model).id_tipo_esito_notifica;
                ((tab_avv_pag_fatt_emissione)destinazione).tipo_esito_notifica = ((tab_avv_pag)model).tipo_esito_notifica;
                ((tab_avv_pag_fatt_emissione)destinazione).data_avvenuta_notifica = ((tab_avv_pag)model).data_avvenuta_notifica;
                ((tab_avv_pag_fatt_emissione)destinazione).id_notificatore = ((tab_avv_pag)model).id_notificatore;
                ((tab_avv_pag_fatt_emissione)destinazione).dt_scadenza_not = ((tab_avv_pag)model).dt_scadenza_not;
                ((tab_avv_pag_fatt_emissione)destinazione).data_ricezione = ((tab_avv_pag)model).data_ricezione;
                ((tab_avv_pag_fatt_emissione)destinazione).data_affissione_ap = ((tab_avv_pag)model).data_affissione_ap;
                ((tab_avv_pag_fatt_emissione)destinazione).data_notifica_avvdep = ((tab_avv_pag)model).data_notifica_avvdep;
                ((tab_avv_pag_fatt_emissione)destinazione).esito_notifica_avvdep = ((tab_avv_pag)model).esito_notifica_avvdep;
                ((tab_avv_pag_fatt_emissione)destinazione).imp_tot_avvpag = ((tab_avv_pag)model).imp_tot_avvpag;
                ((tab_avv_pag_fatt_emissione)destinazione).imp_imp_entr_avvpag = ((tab_avv_pag)model).imp_imp_entr_avvpag;
                ((tab_avv_pag_fatt_emissione)destinazione).imp_esente_iva_avvpag = ((tab_avv_pag)model).imp_esente_iva_avvpag;
                ((tab_avv_pag_fatt_emissione)destinazione).imp_iva_avvpag = ((tab_avv_pag)model).imp_iva_avvpag;
                ((tab_avv_pag_fatt_emissione)destinazione).imp_tot_spese_avvpag = ((tab_avv_pag)model).imp_tot_spese_avvpag;
                ((tab_avv_pag_fatt_emissione)destinazione).imp_spese_notifica = ((tab_avv_pag)model).imp_spese_notifica;
                ((tab_avv_pag_fatt_emissione)destinazione).imp_tot_pagato = ((tab_avv_pag)model).imp_tot_pagato;
                ((tab_avv_pag_fatt_emissione)destinazione).importo_tot_da_pagare = ((tab_avv_pag)model).importo_tot_da_pagare;
                ((tab_avv_pag_fatt_emissione)destinazione).imp_tot_avvpag_rid = ((tab_avv_pag)model).imp_tot_avvpag_rid;
                ((tab_avv_pag_fatt_emissione)destinazione).flag_rateizzazione = ((tab_avv_pag)model).flag_rateizzazione;
                ((tab_avv_pag_fatt_emissione)destinazione).data_rateizzazione = ((tab_avv_pag)model).data_rateizzazione;
                ((tab_avv_pag_fatt_emissione)destinazione).imp_rateizzato = ((tab_avv_pag)model).imp_rateizzato;
                ((tab_avv_pag_fatt_emissione)destinazione).periodicita_rate = ((tab_avv_pag)model).periodicita_rate;
                ((tab_avv_pag_fatt_emissione)destinazione).num_rate = ((tab_avv_pag)model).num_rate;
                ((tab_avv_pag_fatt_emissione)destinazione).data_scadenza_1_rata = ((tab_avv_pag)model).data_scadenza_1_rata;
                ((tab_avv_pag_fatt_emissione)destinazione).flag_rateizzazione_bis = ((tab_avv_pag)model).flag_rateizzazione_bis;
                ((tab_avv_pag_fatt_emissione)destinazione).data_rateizzazione_bis = ((tab_avv_pag)model).data_rateizzazione_bis;
                ((tab_avv_pag_fatt_emissione)destinazione).imp_rateizzato_bis = ((tab_avv_pag)model).imp_rateizzato_bis;
                ((tab_avv_pag_fatt_emissione)destinazione).periodicita_rate_bis = ((tab_avv_pag)model).periodicita_rate_bis;
                ((tab_avv_pag_fatt_emissione)destinazione).num_rate_bis = ((tab_avv_pag)model).num_rate_bis;
                ((tab_avv_pag_fatt_emissione)destinazione).data_scadenza_1_rata_bis = ((tab_avv_pag)model).data_scadenza_1_rata_bis;
                ((tab_avv_pag_fatt_emissione)destinazione).flag_adesione = ((tab_avv_pag)model).flag_adesione;
                ((tab_avv_pag_fatt_emissione)destinazione).data_adesione = ((tab_avv_pag)model).data_adesione;
                ((tab_avv_pag_fatt_emissione)destinazione).flag_riemissione = ((tab_avv_pag)model).flag_riemissione;
                ((tab_avv_pag_fatt_emissione)destinazione).num_avv_riemesso = ((tab_avv_pag)model).num_avv_riemesso;
                ((tab_avv_pag_fatt_emissione)destinazione).id_risorsa = ((tab_avv_pag)model).id_risorsa;
                ((tab_avv_pag_fatt_emissione)destinazione).dt_avv_pag = ((tab_avv_pag)model).dt_avv_pag;
                ((tab_avv_pag_fatt_emissione)destinazione).id_lista_emissione = ((tab_avv_pag)model).id_lista_emissione;
                ((tab_avv_pag_fatt_emissione)destinazione).id_lista_carico = ((tab_avv_pag)model).id_lista_carico;
                ((tab_avv_pag_fatt_emissione)destinazione).id_lista_scarico = ((tab_avv_pag)model).id_lista_scarico;
                ((tab_avv_pag_fatt_emissione)destinazione).flag_carico = ((tab_avv_pag)model).flag_carico;
                ((tab_avv_pag_fatt_emissione)destinazione).flag_scarico = ((tab_avv_pag)model).flag_scarico;
                ((tab_avv_pag_fatt_emissione)destinazione).id_stato = ((tab_avv_pag)model).id_stato;
                ((tab_avv_pag_fatt_emissione)destinazione).cod_stato = ((tab_avv_pag)model).cod_stato;
                ((tab_avv_pag_fatt_emissione)destinazione).data_stato = ((tab_avv_pag)model).data_stato;
                ((tab_avv_pag_fatt_emissione)destinazione).id_struttura_stato = ((tab_avv_pag)model).id_struttura_stato;
                ((tab_avv_pag_fatt_emissione)destinazione).id_risorsa_stato = ((tab_avv_pag)model).id_risorsa_stato;
                ((tab_avv_pag_fatt_emissione)destinazione).importo_ridotto = ((tab_avv_pag)model).importo_ridotto;
                ((tab_avv_pag_fatt_emissione)destinazione).id_tab_supervisione_finale = ((tab_avv_pag)model).id_tab_supervisione_finale;
                ((tab_avv_pag_fatt_emissione)destinazione).fonte_emissione = ((tab_avv_pag)model).fonte_emissione;
                ((tab_avv_pag_fatt_emissione)destinazione).testo1 = ((tab_avv_pag)model).testo1;
                ((tab_avv_pag_fatt_emissione)destinazione).testo2 = ((tab_avv_pag)model).testo2;
                ((tab_avv_pag_fatt_emissione)destinazione).testo3 = ((tab_avv_pag)model).testo3;
                ((tab_avv_pag_fatt_emissione)destinazione).testo4 = ((tab_avv_pag)model).testo4;
                ((tab_avv_pag_fatt_emissione)destinazione).identificativo_avv_pag = ((tab_avv_pag)model).identificativo_avv_pag;
                ((tab_avv_pag_fatt_emissione)destinazione).importo_sanzioni_eliminate_eredi = ((tab_avv_pag)model).importo_sanzioni_eliminate_eredi;
                ((tab_avv_pag_fatt_emissione)destinazione).importo_spese_coattive_sospese_su_preavvisi = ((tab_avv_pag)model).importo_spese_coattive_sospese_su_preavvisi;
                ((tab_avv_pag_fatt_emissione)destinazione).importo_pagato_con_atti_successivi = ((tab_avv_pag)model).importo_pagato_con_atti_successivi;
                ((tab_avv_pag_fatt_emissione)destinazione).dati_avviso_bonario = ((tab_avv_pag)model).dati_avviso_bonario;
                ((tab_avv_pag_fatt_emissione)destinazione).data_avviso_bonario = ((tab_avv_pag)model).data_avviso_bonario;
                ((tab_avv_pag_fatt_emissione)destinazione).data_notifica_avviso_bonario = ((tab_avv_pag)model).data_notifica_avviso_bonario;
                ((tab_avv_pag_fatt_emissione)destinazione).flag_notifica_eredi_generici = ((tab_avv_pag)model).flag_notifica_eredi_generici;
                ((tab_avv_pag_fatt_emissione)destinazione).importo_iscrizione_ruolo = ((tab_avv_pag)model).importo_iscrizione_ruolo;
                ((tab_avv_pag_fatt_emissione)destinazione).imp_maggiorazione_onere_riscossione_61_90 = ((tab_avv_pag)model).imp_maggiorazione_onere_riscossione_61_90;
                ((tab_avv_pag_fatt_emissione)destinazione).imp_maggiorazione_onere_riscossione_121 = ((tab_avv_pag)model).imp_maggiorazione_onere_riscossione_121;
                ((tab_avv_pag_fatt_emissione)destinazione).imp_tot_interesse_mora_giornaliero = ((tab_avv_pag)model).imp_tot_interesse_mora_giornaliero;
                ((tab_avv_pag_fatt_emissione)destinazione).flag_ricalcolo = ((tab_avv_pag)model).flag_ricalcolo;
                //string v_codice = "";
                //foreach (PropertyInfo v_property in destinazione.GetType().GetProperties())
                //{
                //    PropertyInfo propDest = destinazione.GetType().GetProperty(v_property.Name, BindingFlags.Public | BindingFlags.Instance);
                //    PropertyInfo propOrig = model.GetType().GetProperty(v_property.Name, BindingFlags.Public | BindingFlags.Instance);

                //    //TODO Davide: prova  - SPERIMENTALE
                //    bool isPrimitive = false;
                //    if (soloPropElementari)
                //    {
                //        bool isNullabile = Nullable.GetUnderlyingType(propDest.PropertyType) != null;

                //        Type propDestType = !isNullabile ? propDest.PropertyType : Nullable.GetUnderlyingType(propDest.PropertyType);
                //        isPrimitive = propDestType.IsPrimitive || propDestType == typeof(Decimal) || propDestType == typeof(String) || propDestType == typeof(DateTime);
                //    }

                //    if (!v_exceptProperties.Contains(propDest.Name))
                //    {
                //        if (propDest.Name.Contains("TAB_SUPERVISIONE_FINALE_V2") && (destinazione is tab_avv_pag))
                //        {
                //            //PropertyInfo propSForig_giusta = model.GetType().GetProperty("TAB_SUPERVISIONE_FINALE_V2", BindingFlags.Public | BindingFlags.Instance);
                //            //propDest.SetValue(propSForig_giusta, propSForig_giusta.GetValue(model), null);
                //        }
                //        else if ((null != propDest && propDest.CanWrite) && (null != propOrig && propOrig.CanWrite) && (!soloPropElementari || (isPrimitive)))
                //        {
                //            v_codice = v_codice + "((tab_avv_pag_fatt_emissione)destinazione)." + propOrig.Name + " = ((tab_avv_pag)model)." + propOrig.Name + "; ";
                //            propDest.SetValue(destinazione, propOrig.GetValue(model), null);
                //        }
                //    }
                //}
            }
            else
            {
                foreach (PropertyInfo v_property in destinazione.GetType().GetProperties())
                {
                    PropertyInfo propDest = destinazione.GetType().GetProperty(v_property.Name, BindingFlags.Public | BindingFlags.Instance);
                    PropertyInfo propOrig = model.GetType().GetProperty(v_property.Name, BindingFlags.Public | BindingFlags.Instance);

                    //TODO Davide: prova  - SPERIMENTALE
                    bool isPrimitive = false;
                    if (soloPropElementari)
                    {
                        bool isNullabile = Nullable.GetUnderlyingType(propDest.PropertyType) != null;

                        Type propDestType = !isNullabile ? propDest.PropertyType : Nullable.GetUnderlyingType(propDest.PropertyType);
                        isPrimitive = propDestType.IsPrimitive || propDestType == typeof(Decimal) || propDestType == typeof(String) || propDestType == typeof(DateTime);
                    }

                    if (!v_exceptProperties.Contains(propDest.Name))
                    {
                        if (propDest.Name.Contains("TAB_SUPERVISIONE_FINALE_V2") && (destinazione is tab_avv_pag))
                        {
                            //PropertyInfo propSForig_giusta = model.GetType().GetProperty("TAB_SUPERVISIONE_FINALE_V2", BindingFlags.Public | BindingFlags.Instance);
                            //propDest.SetValue(propSForig_giusta, propSForig_giusta.GetValue(model), null);
                        }
                        else if ((null != propDest && propDest.CanWrite) && (null != propOrig && propOrig.CanWrite) && (!soloPropElementari || (isPrimitive)) && (p_ancheNull || propOrig.GetValue(model) != null))
                        {
                            propDest.SetValue(destinazione, propOrig.GetValue(model), null);
                        }
                    }
                }
            }
        }

        public static string translateObjProperty<TSource, TProperty>(this TSource source, Expression<Func<TSource, TProperty>> propertyLambda, Dictionary<string, string> translationDictionary)
        {
            Type type = typeof(TSource);

            MemberExpression member = propertyLambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    propertyLambda.ToString()));

            string propertyName = member.Member.Name;

            if (translationDictionary != null && translationDictionary.ContainsKey(propertyName))
                return translationDictionary[propertyName];

            var ret = propertyLambda.Compile().Invoke(source);

            return ret == null ? "" : ret.ToString();
        }

        public static dynamic ToDynamic(this object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
                expando.Add(property.Name, property.GetValue(value));

            return expando as ExpandoObject;
        }

        public static decimal ArrotondaA5Decimali(this decimal p_origine)
        {
            decimal risp = p_origine;

            risp = Math.Round(p_origine, 5);

            if ((risp + 0.000005m) == p_origine)
            {
                risp = risp + (decimal)0.00001;
            }

            return risp;
        }

        public static decimal ArrotondaA4Decimali(this decimal p_origine)
        {
            decimal risp = p_origine;

            risp = Math.Round(p_origine, 4);

            if ((risp + 0.00005m) == p_origine)
            {
                risp = risp + (decimal)0.0001;
            }

            return risp;
        }

        public static decimal ArrotondaA2Decimali(this decimal p_origine)
        {
            decimal risp = p_origine;

            risp = Math.Round(p_origine, 2);

            if ((risp + 0.005m) == p_origine)
            {
                risp = risp + (decimal)0.01;
            }

            return risp;
        }

        public static decimal Arrotonda(this decimal p_origine)
        {
            decimal risp = p_origine;

            risp = Math.Round(p_origine);

            if ((risp + 0.5m) == p_origine)
            {
                risp = risp + 1;
            }

            return risp;
        }

        public static decimal ArrotondaAZeroSeNegativo(this decimal p_origine)
        {
            decimal risp = p_origine;

            if(p_origine < 0)
            {
                risp = 0;
            }           
            return risp;
        }

        public static string TruncateLength(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static void LoadAllProperties<T>(this T entity, dbEnte p_context)
        {
            ObjectContext v_objContext = ((IObjectContextAdapter)p_context).ObjectContext;

            Type type = entity.GetType();
            //context.Refresh(RefreshMode.ClientWins, entity);
            foreach (var property in type.GetProperties())
            {
                bool isNullabile = Nullable.GetUnderlyingType(property.PropertyType) != null;

                Type propDestType = !isNullabile ? property.PropertyType : Nullable.GetUnderlyingType(property.PropertyType);
                bool isPrimitive = propDestType.IsPrimitive || propDestType == typeof(Decimal) || propDestType == typeof(String) || propDestType == typeof(DateTime);
                if (!isPrimitive)
                {
                    v_objContext.LoadProperty(entity, property.Name);
                }

                //if (property.PropertyType.Name.StartsWith("ICollection"))
                //{
                //    context.LoadProperty(entity, property.Name);
                //    var itemCollection = property.GetValue(entity, null) as IEnumerable;
                //    foreach (object item in itemCollection)
                //    {
                //        item.LoadAllProperties(context);
                //    }
                //}
            }
        }


    }
}
