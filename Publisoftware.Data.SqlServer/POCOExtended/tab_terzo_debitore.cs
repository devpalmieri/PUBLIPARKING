using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_terzo_debitore.Metadata))]
    public partial class tab_terzo_debitore : ISoftDeleted, IGestioneStato
    {
        public const int ATT_ATT_ID = 1; //Attivo
        public const string ATT_ATT = "ATT-ATT"; //Attivo
        public const string ATT_CES = "ATT-CES"; //Cessato
        public const string ANN_ATT = "ANN-ATT"; //Annullato - Attivo
        public const string ANN_CES = "ANN-CES"; //Annullato - Cessato
        public const string ANN_IST = "ANN-IST"; //Annullato - Istanza
        public const string ANN_DTZ = "ANN-DTZ";
        public const string ANN_ANN = "ANN-ANN";
        public const string ATT = "ATT-";
        public const string ANN = "ANN-";

        public void setStatoAnnullato()
        {
            cod_stato = "ANN-" + cod_stato.Substring(4);
        }

        public void setStatoAttivo()
        {
            cod_stato = ATT_ATT;
        }

        public void setStatoCessato()
        {
            cod_stato = ATT_CES;
        }

        public class tipoPersonaTerzoDebitore
        {
            public int idTipo { get; set; }
            public string descrizione { get; set; }
        }

        public static List<tipoPersonaTerzoDebitore> getListTipi()
        {
            tipoPersonaTerzoDebitore v_fisica = new tipoPersonaTerzoDebitore();
            v_fisica.idTipo = 1;
            v_fisica.descrizione = "Persona Fisica";

            tipoPersonaTerzoDebitore v_giuridica = new tipoPersonaTerzoDebitore();
            v_giuridica.idTipo = 2;
            v_giuridica.descrizione = "Persona Giuridica";

            List<tipoPersonaTerzoDebitore> v_list = new List<tipoPersonaTerzoDebitore>();

            v_list.Add(v_fisica);
            v_list.Add(v_giuridica);

            return v_list;
        }

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public string dt_scadenza_contratto_String
        {
            get
            {
                if (dt_scadenza_contratto.HasValue)
                {
                    return dt_scadenza_contratto.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string importo_annuo_fitto_Euro
        {
            get
            {
                if (importo_annuo_fitto.HasValue)
                {
                    return importo_annuo_fitto.Value.ToString("C");
                }
                else
                {
                    return 0.ToString("C");
                }
            }
        }

        public string importo_mensile_fitto_Euro
        {
            get
            {
                if (importo_mensile_fitto.HasValue)
                {
                    return importo_mensile_fitto.Value.ToString("C");
                }
                else
                {
                    return 0.ToString("C");
                }
            }
        }

        public string importo_annuo_retrib_Euro
        {
            get
            {
                if (importo_annuo_retrib.HasValue)
                {
                    return importo_annuo_retrib.Value.ToString("C");
                }
                else
                {
                    return 0.ToString("C");
                }
            }
        }

        public string importo_annuo_pensione_Euro
        {
            get
            {
                if (importo_annuo_pens.HasValue)
                {
                    return importo_annuo_pens.Value.ToString("C");
                }
                else
                {
                    return 0.ToString("C");
                }
            }
        }

        public string importo_mensile_retrib_Euro
        {
            get
            {
                if (importo_mensile_retrib.HasValue)
                {
                    return importo_mensile_retrib.Value.ToString("C");
                }
                else
                {
                    return 0.ToString("C");
                }
            }
        }

        public string importo_mensile_pens_Euro
        {
            get
            {
                if (importo_mensile_pens.HasValue)
                {
                    return importo_mensile_pens.Value.ToString("C");
                }
                else
                {
                    return 0.ToString("C");
                }
            }
        }

        public string Nominativo
        {
            get
            {
                if (tab_terzo == null)
                {
                    return String.Empty;
                }

                if (tab_terzo.cod_fiscale != null)
                {
                    return tab_terzo.cognome + " " + tab_terzo.nome;
                }
                else
                {
                    return tab_terzo.rag_sociale;
                }
            }
        }

        public string CodiceFiscalePIVA
        {
            get
            {
                if (tab_terzo == null)
                {
                    return String.Empty;
                }

                if (tab_terzo.id_tipo_terzo == tab_terzo.TIPO_FISICO)
                {
                    return tab_terzo.cod_fiscale;
                }
                else
                {
                    return tab_terzo.p_iva;
                }
            }
        }

        public string indirizzoDisplay
        {
            get
            {
                if (tab_terzo == null)
                {
                    return String.Empty;
                }

                if (tab_terzo.tab_toponimi != null && tab_terzo.tab_toponimi.id_tipo_toponimo != 0)
                {
                    return tab_terzo.tab_toponimi.descrizione_toponimo;
                }
                else
                {
                    return tab_terzo.indirizzo;
                }
            }
        }

        public string cittaDisplay
        {
            get
            {
                if (tab_terzo == null)
                {
                    return String.Empty;
                }

                if (tab_terzo.tab_toponimi != null && tab_terzo.tab_toponimi.id_tipo_toponimo != 0)
                {
                    if (tab_terzo.tab_toponimi.ser_comuni != null)
                    {
                        return tab_terzo.tab_toponimi.ser_comuni.des_comune;
                    }
                    else
                    {
                        return tab_terzo.citta;
                    }
                }
                else
                {
                    return tab_terzo.citta;
                }
            }
        }

        public string civico
        {
            get
            {
                if (tab_terzo == null)
                {
                    return String.Empty;
                }

                if (tab_terzo.sigla_civico != null && tab_terzo.sigla_civico != string.Empty)
                {
                    return tab_terzo.nr_civico + "/" + tab_terzo.sigla_civico;
                }
                else
                {
                    return tab_terzo.nr_civico.ToString();
                }
            }
        }

        public void SetUserStato(int p_struttura, int p_risorsa)
        {
            data_stato = DateTime.Now;
            id_struttura_stato = p_struttura;
            id_risorsa_stato = p_risorsa;
        }

        public string dataAggiornamentoString
        {
            get
            {
                string v_data = "";

                if (data_aggiornamento.HasValue)
                {
                    v_data = data_aggiornamento.Value.ToShortDateString();
                }
                return v_data;
            }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

        }
    }
}
