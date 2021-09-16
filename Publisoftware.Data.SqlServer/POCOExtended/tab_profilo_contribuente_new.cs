using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_profilo_contribuente_new : ISoftDeleted, IGestioneStato
    {
        public const string ATT_ATT = "ATT-ATT";
        public const string ATT = "ATT";
        public const string ANN_UFF = "ANN-UFF";
        public const string ANN = "ANN";

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

        private string _descrizioneBene = null;
        public string DescrizioneBene
        {
            get
            {
                string descrBeneStr = String.Empty;
                if (_descrizioneBene == null)
                {
                    switch (this.tab_tipo_bene.codice_bene)
                    {
                        case tab_tipo_bene.SIGLA_VEICOLI:
                            descrBeneStr = String.Format("{0} targa n. {1}", tab_tipo_bene.descrizione, tab_veicoli.targa);
                            break;
                        case tab_tipo_bene.SIGLA_LOCAZIONE:
                            descrBeneStr += String.Format("{0} da {1} n.contr. {2} data scad. {3}", tab_tipo_bene.descrizione, (tab_terzo_debitore != null) ? tab_terzo_debitore.Nominativo : String.Empty, tab_terzo_debitore.nr_registrazione_contratto, tab_terzo_debitore.dt_scadenza_contratto.HasValue ? tab_terzo_debitore.dt_scadenza_contratto.Value.ToShortDateString() : String.Empty);
                            break;
                        case tab_tipo_bene.SIGLA_STIPENDI:
                            descrBeneStr += String.Format("{0} da datore {1}", tab_tipo_bene.descrizione, (tab_terzo_debitore != null) ? tab_terzo_debitore.Nominativo : String.Empty);
                            break;
                        case tab_tipo_bene.SIGLA_PENSIONI:
                            descrBeneStr += String.Format("{0} da ente pensionistico {1}", tab_tipo_bene.descrizione, (tab_terzo_debitore != null) ? tab_terzo_debitore.Nominativo : String.Empty);
                            break;
                        case tab_tipo_bene.SIGLA_BANCHE:
                            descrBeneStr += String.Format("{0} di {1} ABI {2} CAB {3}", tab_tipo_bene.descrizione, (tab_terzo_debitore != null) ? tab_terzo_debitore.Nominativo : String.Empty, tab_terzo_debitore.abi_cc, tab_terzo_debitore.cab_cc);
                            break;
                        case tab_tipo_bene.SIGLA_IMMOBILI:
                            descrBeneStr = String.Format("{0} {1} con foglio {2}, numero {3}, denominatore {4}, subalterno {5} sito in {6}", tab_tipo_bene.descrizione, tab_immobili_coattivo.Descrizione, tab_immobili_coattivo.foglio, tab_immobili_coattivo.numero, tab_immobili_coattivo.denominatore, tab_immobili_coattivo.subalterno, tab_immobili_coattivo.indirizzoTotaleDisplay);
                            break;
                        case tab_tipo_bene.SIGLA_MOBILI:
                            descrBeneStr = String.Format("Beni Mobili Posseduti");
                            break;
                    }
                    _descrizioneBene = descrBeneStr;
                }

                return _descrizioneBene;
            }
        }

        private string _descrizioneBeneEstesa = null;
        public string DescrizioneBeneEstesa
        {
            get
            {
                string descrBeneStr = String.Empty;
                if (_descrizioneBeneEstesa == null)
                {
                    switch (tab_tipo_bene.codice_bene)
                    {
                        case tab_tipo_bene.SIGLA_VEICOLI:
                            descrBeneStr = String.Format("{0} {1} {2} con targa n. {3}", tab_tipo_bene.descrizione, tab_veicoli.marca, tab_veicoli.modello, tab_veicoli.targa);
                            break;
                        case tab_tipo_bene.SIGLA_LOCAZIONE:
                            decimal v_residuo = 0;
                            if (tab_terzo_debitore.importo_mensile_fitto.HasValue && tab_terzo_debitore.importo_mensile_fitto > 0)
                            {
                                if (tab_terzo_debitore.dt_scadenza_contratto.HasValue && tab_terzo_debitore.dt_scadenza_contratto.Value > System.DateTime.Now.AddDays(60))
                                {
                                    int v_mesi = Math.Abs((tab_terzo_debitore.dt_scadenza_contratto.Value.Month - System.DateTime.Now.AddDays(60).Month) + 12 * (tab_terzo_debitore.dt_scadenza_contratto.Value.Year - System.DateTime.Now.AddDays(60).Year));
                                    v_residuo = decimal.Multiply(v_mesi, tab_terzo_debitore.importo_mensile_fitto.Value);
                                }
                            }
                            else if (tab_terzo_debitore.importo_annuo_fitto.HasValue && tab_terzo_debitore.importo_annuo_fitto > 0)
                            {
                                if (tab_terzo_debitore.dt_scadenza_contratto.HasValue && tab_terzo_debitore.dt_scadenza_contratto.Value > System.DateTime.Now.AddDays(60))
                                {
                                    int v_mesi = Math.Abs((tab_terzo_debitore.dt_scadenza_contratto.Value.Month - System.DateTime.Now.AddDays(60).Month) + 12 * (tab_terzo_debitore.dt_scadenza_contratto.Value.Year - System.DateTime.Now.AddDays(60).Year));
                                    v_residuo = decimal.Multiply(v_mesi, Decimal.Divide(tab_terzo_debitore.importo_annuo_fitto.Value, 12));
                                }
                            }
                            descrBeneStr += String.Format("{0} da {1} n.contr. {2} con data scad. {3} e importo residuo stimato {4}", tab_tipo_bene.descrizione, (tab_terzo_debitore != null) ? tab_terzo_debitore.Nominativo : String.Empty, tab_terzo_debitore.nr_registrazione_contratto, tab_terzo_debitore.dt_scadenza_contratto.HasValue ? tab_terzo_debitore.dt_scadenza_contratto.Value.ToShortDateString() : String.Empty, v_residuo.ToString("C"));
                            break;
                        case tab_tipo_bene.SIGLA_STIPENDI:
                            string v_retribuzione = (tab_terzo_debitore.importo_mensile_retrib.HasValue && tab_terzo_debitore.importo_mensile_retrib > 0) ? tab_terzo_debitore.importo_mensile_retrib_Euro : (tab_terzo_debitore.importo_annuo_retrib.HasValue && tab_terzo_debitore.importo_annuo_retrib > 0 ? Decimal.Divide(tab_terzo_debitore.importo_annuo_retrib.Value, 12).ToString("C") : 0.ToString("C"));
                            descrBeneStr += String.Format("{0} da datore {1} con retribuzione mensile {2}", tab_tipo_bene.descrizione, (tab_terzo_debitore != null) ? tab_terzo_debitore.Nominativo : String.Empty, v_retribuzione);
                            break;
                        case tab_tipo_bene.SIGLA_PENSIONI:
                            string v_pensione = (tab_terzo_debitore.importo_mensile_pens.HasValue && tab_terzo_debitore.importo_mensile_pens > 0) ? tab_terzo_debitore.importo_mensile_pens_Euro : (tab_terzo_debitore.importo_annuo_pens.HasValue && tab_terzo_debitore.importo_annuo_pens > 0 ? Decimal.Divide(tab_terzo_debitore.importo_annuo_pens.Value, 12).ToString("C") : 0.ToString("C"));
                            descrBeneStr += String.Format("{0} da ente pensionistico {1} con pensione mensile {2}", tab_tipo_bene.descrizione, (tab_terzo_debitore != null) ? tab_terzo_debitore.Nominativo : String.Empty, v_pensione);
                            break;
                        case tab_tipo_bene.SIGLA_BANCHE:
                            descrBeneStr += String.Format("{0} di {1} ABI: {2} e CAB: {3}", tab_tipo_bene.descrizione, (tab_terzo_debitore != null) ? tab_terzo_debitore.Nominativo : String.Empty, tab_terzo_debitore.abi_cc, tab_terzo_debitore.cab_cc);
                            break;
                        case tab_tipo_bene.SIGLA_IMMOBILI:
                            descrBeneStr = String.Format("{0} {1} con FOGLIO {2}, NUMERO {3}, DENOMINATORE {4}, SUBALTERNO {5} sito in {6}", tab_tipo_bene.descrizione, tab_immobili_coattivo.Descrizione, tab_immobili_coattivo.foglio, tab_immobili_coattivo.numero, tab_immobili_coattivo.denominatore, tab_immobili_coattivo.subalterno, tab_immobili_coattivo.indirizzoTotaleDisplay);
                            break;
                        case tab_tipo_bene.SIGLA_MOBILI:
                            descrBeneStr = String.Format("Beni Mobili Posseduti");
                            break;
                    }
                    _descrizioneBeneEstesa = descrBeneStr;
                }

                return _descrizioneBeneEstesa;
            }
        }
    }
}
