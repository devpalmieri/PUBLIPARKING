using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_fascicolo : ISoftDeleted, IGestioneStato
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }

        public bool IsDichiarazioneTerzoPresente
        {
            get
            {
                return join_documenti_fascicolo.Any(d => d.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_DICHIARAZIONE_TERZO);
            }
        }

        public bool IsFascicoloCompleto(bool p_ConCitazioneUfficiale = true)
        {
            bool ImmaginiAvvisi = tab_fascicolo_avvpag_allegati.All(d => d.join_documenti_fascicolo_avvpag_allegati.Any(x => x.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_AVVISO_RIF ||
                                                                                                                             x.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_AVVISO_COLL ||
                                                                                                                             x.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_AVVISO_ORDINE));

            bool ImmaginiNotifiche = tab_fascicolo_avvpag_allegati.Where(d => d.tab_sped_not.flag_soggetto_debitore != tab_sped_not.FLAG_SOGGETTO_DEBITORE_UFFICIALE_RISCOSSIONE).All(d => d.join_documenti_fascicolo_avvpag_allegati.Any(x => x.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_NOTIFICA_RELATA));

            bool Documenti = true;

            bool Asseverazioni = true;

            //Al momento controlla solo la completezza dei fascicoli delle citazioni (escludendo anagrafica_documenti.SIGLA_ESTINZIONE_PROCEDURA_ESECUTIVA)
            if (tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc == tab_tipo_doc_entrate.TIPO_DOC_CITAZIONI)
            {
                if (p_ConCitazioneUfficiale)
                {
                    Documenti = join_documenti_fascicolo.Any(d => d.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_ATTO_PIGNORAMENTO) &&
                                //join_documenti_fascicolo.Any(d => d.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_DICHIARAZIONE_TERZO) &&
                                join_documenti_fascicolo.Any(d => d.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_COPIA_PROCURA);
                }
                else
                {
                    Documenti = join_documenti_fascicolo.Any(d => d.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_COPIA_PROCURA);
                }

                if (tab_fascicolo_avvpag_allegati.Any(d => d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ING_FISC))
                {
                    Asseverazioni = Asseverazioni && join_documenti_fascicolo.Any(d => d.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_ASSEVERAZIONE_INGIUNZIONI_EMESSE ||
                                                                                       d.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_ASSEVERAZIONE_INGIUNZIONI_TRASMESSE);
                }

                if (tab_fascicolo_avvpag_allegati.Any(d => d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.INTIM))
                {
                    Asseverazioni = Asseverazioni && join_documenti_fascicolo.Any(d => d.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_ASSEVERAZIONE_INTIMAZIONI_EMESSE ||
                                                                                       d.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_ASSEVERAZIONE_INTIMAZIONI_TRASMESSE);
                }

                if (tab_fascicolo_avvpag_allegati.Any(d => d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO))
                {
                    Asseverazioni = Asseverazioni && join_documenti_fascicolo.Any(d => d.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_ASSEVERAZIONE_PIGNORAMENTI_ORDINE_TERZO);
                }

                if (tab_fascicolo_avvpag_allegati.Any(d => d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO))
                {
                    Asseverazioni = Asseverazioni && join_documenti_fascicolo.Any(d => d.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_ASSEVERAZIONE_PIGNORAMENTI_CITAZIONE_TERZO);
                }
            }

            return ImmaginiAvvisi && ImmaginiNotifiche && Documenti && Asseverazioni;
        }
    }
}
