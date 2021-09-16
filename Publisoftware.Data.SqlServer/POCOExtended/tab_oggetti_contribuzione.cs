using Publisoftware.Data.CustomValidationAttrs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_oggetti_contribuzione.Metadata))]
    public partial class tab_oggetti_contribuzione : ISoftDeleted, IGestioneStato
    {
        [Obsolete("usare anagrafica_stato_oggetto.ATT")]
        public const string ATT_ = anagrafica_stato_oggetto.ATT; // "ATT-";

        [Obsolete("usare anagrafica_stato_oggetto.ATTIVO")]
        public const string ATT_ATT = anagrafica_stato_oggetto.ATTIVO;

        [Obsolete("usare anagrafica_stato_oggetto.ANNULLATO")]
        public const string ANN_ANN = anagrafica_stato_oggetto.ANNULLATO; // "ANN-ANN";

        [Obsolete("usare anagrafica_stato_oggetto.CESSATO")]
        public const string ATT_CES = anagrafica_stato_oggetto.CESSATO; // "ATT-CES";

        [Obsolete("usare anagrafica_stato_oggetto.ATTIVO_ID")]
        public const int ATT_ATT_ID = anagrafica_stato_oggetto.ATTIVO_ID;

        [Obsolete("usare anagrafica_stato_oggetto.CESSATO_ID")]
        public const int ATT_CES_ID = anagrafica_stato_oggetto.CESSATO_ID; // 13;

        [Obsolete("usare anagrafica_stato_oggetto.ANNULLATO_ID")]
        public const int ANN_ANN_ID = anagrafica_stato_oggetto.ANNULLATO_ID; // 3

        /// <summary>
        /// Sospeso in attesa di consolidamento da una lista di trasmissione
        /// </summary>
        public const string SSP_TRA = "SSP-TRA";
        public const int SSP_TRA_ID = 100;
        public const string SOSPESO = "SSP-";
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

        public string data_inizio_contribuzione_String
        {
            get
            {
                return data_inizio_contribuzione.ToShortDateString();
            }
        }

        public string data_fine_contribuzione_String
        {
            get
            {
                if (data_fine_contribuzione.HasValue)
                {
                    return data_fine_contribuzione.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_attivazione_ogg_contribuzione_String
        {
            get
            {
                if (data_attivazione_ogg_contribuzione.HasValue)
                {
                    return data_attivazione_ogg_contribuzione.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_cessazione_ogg_contribuzione_String
        {
            get
            {
                if (data_cessazione_ogg_contribuzione.HasValue)
                {
                    return data_cessazione_ogg_contribuzione.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_variazione_String
        {
            get
            {
                if (data_variazione.HasValue)
                {
                    return data_variazione.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_allaciamento_fognatura_String
        {
            get
            {
                if (data_allaciamento_fognatura.HasValue)
                {
                    return data_allaciamento_fognatura.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string PeriodoContribuzione
        {
            get
            {
                return data_inizio_contribuzione_String + " - " + (data_fine_contribuzione_String == string.Empty ? "Ad oggi" : data_fine_contribuzione_String);
            }
        }

        public string PercentualePossesso
        {
            get
            {
                if (percentuale_contribuzione.HasValue)
                    return (int)percentuale_contribuzione.Value + "%";
                else
                    return string.Empty;
            }
        }

        public string Rendita
        {
            get
            {
                return String.Format("{0:0.00}", quantita_base) + " " + um_quantita_base;
            }
        }

        public string Diritto
        {
            get
            {
                if (anagrafica_tipo_diritto != null)
                {
                    return anagrafica_tipo_diritto.codice + " - " + anagrafica_tipo_diritto.descrizione;
                }
                else
                {
                    return String.Empty;
                }
            }
        }

        public string TipologiaImmobile
        {
            get
            {
                if (tab_oggetti == null) { return String.Empty; }
                if (tab_oggetti.flag_accatastamento == "F")
                {
                    return "Fabbricato";
                }
                else if (tab_oggetti.flag_accatastamento == "T")
                {
                    return "Terreno";
                }
                else
                {
                    return tab_oggetti.flag_accatastamento;
                }
            }
        }

        public string NumTotOccupanti
        {
            get
            {
                string numTotOccupanti = string.Empty;

                if (num_tot_occupanti.HasValue)
                {
                    if (num_tot_occupanti.Value != 0)
                    {
                        numTotOccupanti = num_tot_occupanti.Value.ToString();
                    }
                }
                else if (anagrafica_categoria != null &&
                         anagrafica_categoria.num_tot_persone.HasValue)
                {
                    numTotOccupanti = anagrafica_categoria.num_tot_persone.Value.ToString();
                }

                return numTotOccupanti;
            }
        }

        public string SuperficieTassabile
        {
            get
            {
                if (quantita_base.HasValue)
                {
                    return Decimal.Round(quantita_base.Value, 2) + " " + um_quantita_base;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public decimal? SuperficieTassabileDec { get { return quantita_base; } }

        public string SuperficieNonTassabile
        {
            get
            {
                if (spese_allacciamento.HasValue)
                {
                    return Decimal.Round(spese_allacciamento.Value, 2) + " " + um_quantita_base;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public decimal? SuperficieNonTassabileDec { get { return spese_allacciamento; } }


        public string SuperficieTotale
        {
            get
            {
                if (nolo_contatore.HasValue)
                {
                    return Decimal.Round(nolo_contatore.Value, 2) + " " + um_quantita_base;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public decimal? SuperficieTotaleDec { get { return nolo_contatore; } }


        public string Dimensioni
        {
            get
            {
                return Altezza + " x " + Lunghezza;
            }
        }

        public string Lunghezza
        {
            get
            {
                if (num_tot_occupanti.HasValue)
                {
                    return num_tot_occupanti.Value + " cm";
                }
                else
                {
                    return "0 cm";
                }
            }
        }

        public string Altezza
        {
            get
            {
                if (num_tot_unita_abitative.HasValue)
                {
                    return num_tot_unita_abitative.Value + " cm";
                }
                else
                {
                    return "0 cm";
                }
            }
        }

        public string NumFacce
        {
            get
            {
                if (quantita_base.HasValue)
                {
                    return Convert.ToInt32(quantita_base.Value).ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string NumOggetti
        {
            get
            {
                if (nolo_contatore.HasValue)
                {
                    return Convert.ToInt32(nolo_contatore.Value).ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string NumGiorniOcc
        {
            get
            {
                if (spese_contrattuali.HasValue)
                {
                    return Convert.ToInt32(spese_contrattuali.Value).ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string Contenuto
        {
            get
            {
                if (join_denunce_oggetti != null)
                {
                    return join_denunce_oggetti.Where(d => d.annotazioni != null && d.annotazioni != string.Empty).OrderByDescending(d => d.data_creazione).FirstOrDefault()?.annotazioni ?? String.Empty;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string CostoCartello
        {
            get
            {
                if (nolo_contatore.HasValue)
                {
                    return String.Format("{0:0.00}", nolo_contatore.Value) + " €";
                }
                else
                {
                    return "0 €";
                }
            }
        }

        public string DirittiRichiesta
        {
            get
            {
                if (spese_allacciamento.HasValue)
                {
                    return String.Format("{0:0.00}", spese_allacciamento.Value) + " €";
                }
                else
                {
                    return "0 €";
                }
            }
        }

        public string DirittiIstruttoria
        {
            get
            {
                if (spese_contrattuali.HasValue)
                {
                    return String.Format("{0:0.00}", spese_contrattuali.Value) + " €";
                }
                else
                {
                    return "0 €";
                }
            }
        }

        public string DirittiSopralluogo
        {
            get
            {
                if (deposito_cauzionale.HasValue)
                {
                    return String.Format("{0:0.00}", deposito_cauzionale.Value) + " €";
                }
                else
                {
                    return "0 €";
                }
            }
        }

        public string TipologiaUtenze
        {
            get
            {
                if (flag_immobile_storico == "A")
                {
                    return "Utenze alimentari";
                }
                else if (flag_immobile_storico == "N")
                {
                    return "Utenze non alimentari";
                }
                else
                {
                    return flag_immobile_storico;
                }
            }
        }

        public string OraInizioOcc
        {
            get
            {
                if (join_denunce_oggetti != null)
                {
                    return join_denunce_oggetti
                        .Where(d => d.num_ordine_den_ici.HasValue)
                        .OrderByDescending(d => d.data_creazione)
                        .FirstOrDefault()?.num_ordine_den_ici?.ToString() ?? String.Empty;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string OraFineOcc
        {
            get
            {
                if (join_denunce_oggetti != null)
                {
                    return join_denunce_oggetti
                        .Where(d => d.prog_num_ordine_den_ici.HasValue)
                        .OrderByDescending(d => d.data_creazione)
                        .FirstOrDefault()?.prog_num_ordine_den_ici?.ToString() ?? String.Empty;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string Denuncia
        {
            get
            {
                string v_denuncia = string.Empty;
                if (id_entrata == anagrafica_entrate.TASI || id_entrata == anagrafica_entrate.TARES_TARSU)
                {
                    if (flag_allaciamento_fognatura != null)
                    {
                        if (flag_allaciamento_fognatura == "1")
                        {
                            v_denuncia = "Oggetto derivato da accertamento di omessa denuncia del " + data_allaciamento_fognatura_String;
                        }
                        else if (flag_allaciamento_fognatura == "2")
                        {
                            v_denuncia = "Oggetto derivato da accertamento di infedele denuncia del " + data_allaciamento_fognatura_String;
                        }
                    }

                    else if (flag_accertamento == "1")
                    {
                        v_denuncia = $"Oggetto derivato da accertamento di omessa denuncia dal {data_inizio_accertamento?.ToString("dd/MM/yyyy")} al {data_scadenza_accertamento?.ToString("dd/MM/yyyy")}";
                    }
                    else if (flag_accertamento == "2")
                    {
                        v_denuncia = $"Oggetto derivato da accertamento di infedele denuncia dal {data_inizio_accertamento?.ToString("dd/MM/yyyy")} al {data_scadenza_accertamento?.ToString("dd/MM/yyyy")}";
                    }


                    else if (flag_ravvedimento == "1")
                    {
                        v_denuncia = $"Oggetto derivato da ravvedimento di omessa denuncia dal {data_inizio_ravvedimento?.ToString("dd/MM/yyyy")} al {data_scadenza_ravvedimento?.ToString("dd/MM/yyyy")}";
                    }
                    else if (flag_ravvedimento == "2")
                    {
                        v_denuncia = $"Oggetto derivato da ravvedimento di infedele denuncia dal {data_inizio_ravvedimento?.ToString("dd/MM/yyyy")} al {data_scadenza_ravvedimento?.ToString("dd/MM/yyyy")}";
                    }

                }
                return v_denuncia;
            }
        }

        public string CategoriaTariffaria
        {
            get
            {
                if (anagrafica_categoria != null)
                {
                    return anagrafica_categoria.sigla_cat_contr + " - " + anagrafica_categoria.des_cat_contr;
                }

                return string.Empty;
            }
        }

        public string TipoUtilizzo
        {
            get
            {
                return anagrafica_utilizzo?.des_utilizzo;
            }
        }

        public string AllacciamentoReteIdrica
        {
            get
            {
                if (flag_allaciamento_fognatura != null || flag_allaciamento_fognatura != "0")
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string AllacciamentoFognatura
        {
            get
            {
                if (flag_allaciamento_fognatura != null || flag_allaciamento_fognatura != "0")
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string AllacciamentoDepuratore
        {
            get
            {
                if (flag_allaciamento_depuratore != null || flag_allaciamento_depuratore != "0")
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }


        public string DepositoCauzionale
        {
            get
            {
                if (deposito_cauzionale.HasValue)
                {
                    return String.Format("{0:0.0000}", deposito_cauzionale.Value) + " €";
                }
                else
                {
                    return "0 €";
                }
            }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Obsolete("Usare data_scadenza_accertamento", true)]
            public Nullable<System.DateTime> data_scadenza_contribuzione_accertamento { get; set; }
        }
    }
}
