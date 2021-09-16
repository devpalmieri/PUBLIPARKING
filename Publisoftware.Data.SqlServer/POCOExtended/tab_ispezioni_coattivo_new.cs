using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public enum TIPO_INTERROGAZIONE
    {
        TUTTE,
        IN_ISPEZIONE,
        ISPEZIONE_POSITIVA,
        ISPEZIONE_NEGATIVA,
        AVV_DA_EMETTERE
        //,ISPEZIONE_NEGATIVA_DA_RIEMETTERE
    }

    public class MorositaFascia
    {
        public int idFascia { get; set; }
        public int numero { get; set; }
        public decimal totale { get; set; }
    }

    [MetadataTypeAttribute(typeof(tab_ispezioni_coattivo_new.Metadata))]
    public partial class tab_ispezioni_coattivo_new : ISoftDeleted, IGestioneStato
    {
        public static String VAL = "VAL-";
        public static String VAL_VAL = "VAL-VAL";
        public static String VAL_OLD = "VAL-OLD";
        public static String VAL_PRE = "VAL-PRE";

        public static int ANN_ANN_ID = 183;
        public static String ANN_ANN = "ANN-ANN";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        /// <summary>
        /// Gestisce l'aggiornamento dei campi utente dello stato
        /// </summary>
        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }

        public string nominativoRagSoc
        {
            get
            {
                if (tab_referente != null)
                {
                    return tab_referente.referenteNominativoDisplay;
                }
                else
                {
                    return tab_contribuente.nominativoDisplay;
                }
            }
        }

        public string tipoRelazione
        {
            get
            {
                if (tab_referente != null && tab_referente.join_referente_contribuente.Where(d => d.id_anag_contribuente == id_contribuente).FirstOrDefault() != null)
                {
                    return tab_referente.join_referente_contribuente.Where(d => d.id_anag_contribuente == id_contribuente).OrderByDescending(d => d.id_join_referente_contribuente).FirstOrDefault().anagrafica_tipo_relazione.desc_tipo_relazione;
                }
                else
                {
                    return "Contribuente";
                }
            }
        }

        public string risorsaSupervisione
        {
            get
            {
                if (id_risorsa_supervisione.HasValue)
                {
                    return anagrafica_risorse.CognomeNome;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string impMorosita
        {
            get
            {
                //if (tab_referente != null)
                //{
                //    return totale_morosita_soggetto_ispezione.HasValue ? totale_morosita_soggetto_ispezione.Value.ToString("C") : 0.ToString("C");
                //}
                //else
                //{
                    return totale_morosita.HasValue ? totale_morosita.Value.ToString("C") : 0.ToString("C");
                //}
            }
        }

        public string impMorositaFermo
        {
            get
            {
                return totale_morosita_assoggettabile_preavviso_fermo.HasValue ? totale_morosita_assoggettabile_preavviso_fermo.Value.ToString("C") : 0.ToString("C");
            }
        }

        public string impMorositaIpoteca
        {
            get
            {
                return totale_morosita_assoggettabile_preavviso_ipoteca.HasValue ? totale_morosita_assoggettabile_preavviso_ipoteca.Value.ToString("C") : 0.ToString("C");
            }
        }

        public string impMorositaAssoggettabileAttiEsecutivi
        {
            get
            {
                return totale_morosita_assoggettabile_atti_esecutivi.HasValue ? totale_morosita_assoggettabile_atti_esecutivi.Value.ToString("C") : 0.ToString("C");
            }
        }

        public string attoEmesso
        {
            get
            {
                if ((flag_esito_ispezione_totale == null || flag_esito_ispezione_totale == "0") &&
                    (flag_fine_ispezione_totale == null || flag_fine_ispezione_totale == "0") &&
                    (flag_supervisione_finale == null || flag_supervisione_finale == "0"))
                {
                    return string.Empty;
                }
                else if (flag_esito_ispezione_totale == "1" &&
                         flag_supervisione_finale == "1")
                {
                    string v_result = string.Empty;
                    tab_avv_pag v_avvpagEmesso = TAB_SUPERVISIONE_FINALE_V2.Where(d => d.ID_AVVPAG_EMESSO != null && !d.COD_STATO.StartsWith(Publisoftware.Data.TAB_SUPERVISIONE_FINALE_V2.ANN)).FirstOrDefault() != null ? TAB_SUPERVISIONE_FINALE_V2.Where(d => d.ID_AVVPAG_EMESSO != null && !d.COD_STATO.StartsWith(Publisoftware.Data.TAB_SUPERVISIONE_FINALE_V2.ANN)).FirstOrDefault().tab_avv_pag : null;

                    if (v_avvpagEmesso == null)
                    {
                        v_result = "Atto cautelare o esecutivo non emesso";
                    }
                    else
                    {
                        v_result = v_avvpagEmesso.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " " + v_avvpagEmesso.identificativo_avv_pag;
                    }

                    return v_result;
                }
                else if (flag_esito_ispezione_totale == "2" &&
                         flag_fine_ispezione_totale == "1")
                {
                    return "Nessun atto cautelare o esecutivo emesso";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string esitoIsp
        {
            get
            {
                if ((flag_esito_ispezione_totale == null || flag_esito_ispezione_totale == "0") &&
                   (flag_fine_ispezione_totale == null || flag_fine_ispezione_totale == "0") &&
                   (flag_supervisione_finale == null || flag_supervisione_finale == "0"))
                {
                    return "Ispezioni non completate";
                }
                else if (flag_esito_ispezione_totale == "1" &&
                         flag_fine_ispezione_totale == "1")
                {
                    return "Positivo";
                }
                else if (flag_esito_ispezione_totale == "2" &&
                         flag_fine_ispezione_totale == "1")
                {
                    return "Negativo";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string fineIsp
        {
            get
            {
                if (flag_fine_ispezione_totale == null)
                {
                    return string.Empty;
                }
                else if (flag_fine_ispezione_totale == "1")
                {
                    return "Positivo";
                }
                else if (flag_fine_ispezione_totale == "0")
                {
                    return "Negativo";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string supervisione
        {
            get
            {
                if (flag_supervisione_finale == null)
                {
                    return string.Empty;
                }
                else if (flag_supervisione_finale == "1")
                {
                    return "Positivo";
                }
                else if (flag_supervisione_finale == "0")
                {
                    return "Negativo";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_rilevazione_morosita_String
        {
            get
            {
                if (data_rilevazione_morosita.HasValue)
                {
                    return data_rilevazione_morosita.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID Contribuente Ispezione")]
            public int id_contribuente { get; set; }

            [DisplayName("Soggetto Ispezione")]
            public string cfiscale_piva_soggetto_ispezione { get; set; }
        }
    }
}
