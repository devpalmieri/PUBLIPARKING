using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_immobili_coattivo.Metadata))]
    public partial class tab_immobili_coattivo : ISoftDeleted, IGestioneStato
    {
        public const String ATT_ATT = "ATT-ATT"; //Attivo
        public const String ATT_CES = "ATT-CES"; //Cessato
        public const String ANN_ATT = "ANN-ATT"; //Annullato - Attivo
        public const String ANN_CES = "ANN-CES"; //Annullato - Cessato
        public const String ANN_IST = "ANN-IST"; //Annullato - Istanza

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

        public string Descrizione
        {
            get
            {
                if (tab_categorie_fabbricati != null)
                {
                    return tab_categorie_fabbricati.descrizione;
                }
                else if (tab_qualita_terreno != null)
                {
                    return tab_qualita_terreno.descrizione;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string TipoImmobile
        {
            get
            {
                if (this.tipo_immobile == null)
                {
                    return "Non Rilevato";
                }

                else if (this.tipo_immobile == "F")
                {
                    return "Fabbricato";
                }
                else if (this.tipo_immobile == "T")
                {
                    return "Terreno";
                }
                return "Non Rilevato";
            }
        }

        public string DescrizioneBreve
        {
            get
            {
                if (tab_categorie_fabbricati != null)
                {
                    return tab_categorie_fabbricati.descrizione_breve;
                }
                else
                {
                    return tab_qualita_terreno.descrizione;
                }
            }
        }

        public string civico
        {
            get
            {
                if (sigla_civico != null && sigla_civico != string.Empty)
                {
                    if (nr_civico.HasValue)
                    {
                        return nr_civico.Value + "/" + sigla_civico;
                    }
                    else
                    {
                        return sigla_civico;
                    }
                }
                else
                {
                    if (nr_civico.HasValue)
                    {
                        return nr_civico.Value.ToString();
                    }
                    {
                        return string.Empty;
                    }
                }
            }
        }

        public string cittaDisplay
        {
            get
            {
                if (citta == null)
                {
                    return "";
                }
                else
                {
                    return citta;
                }
            }
        }

        public string indirizzoDisplay
        {
            get
            {
                if (indirizzo == null)
                {
                    return "";
                }
                else
                {
                    return indirizzo;
                }
            }
        }

        public string indirizzoTotaleDisplay
        {
            get
            {
                return ((indirizzoDisplay != null && indirizzoDisplay != string.Empty) ? indirizzoDisplay : string.Empty) +
                                             ((civico != null && civico != string.Empty && civico.Trim() != "0") ? " " + civico : string.Empty) +
                                             ((sigla_civico != null && sigla_civico != string.Empty && sigla_civico != "SNC") ? " " + sigla_civico.Trim() : string.Empty) +
                                             ((cittaDisplay != null && cittaDisplay != string.Empty) ? ", " + cittaDisplay : string.Empty) +
                                             ((prov != null && prov != string.Empty) ? " (" + prov + ")" : string.Empty);
            }
        }

        public string DatiCatastali
        {
            get
            {
                try
                {
                    string v_dati = "F:";

                    if (foglio != null)
                    {
                        v_dati = v_dati + this.foglio.ToString();
                    }
                    if (numero != null)
                    {
                        v_dati = v_dati + " / N:" + this.numero.ToString();
                    }
                    if (denominatore != null)
                    {
                        v_dati = v_dati + " / P:" + this.denominatore.ToString();
                    }
                    if (subalterno != null)
                    {
                        v_dati = v_dati + " / S:" + this.subalterno.ToString();
                    }

                    return v_dati;
                }
                catch
                {
                    return "errore";
                }
            }
        }
        //Sandro
        public string DatiCatastaliForPrint
        {
            get
            {
                try
                {
                    string v_data_cat = string.Empty;
                    if (foglio != null)
                    {
                        v_data_cat = "Foglio: " + foglio.ToString();
                    }
                    if (numero != null)
                    {
                        v_data_cat = v_data_cat + " Numero: " + numero.ToString();
                    }
                    if (denominatore != null)
                    {
                        v_data_cat = v_data_cat + " Particella: " + denominatore.ToString();
                    }
                    if (subalterno != null)
                    {
                        v_data_cat = v_data_cat + " Subalterno: " + subalterno.ToString();
                    }
                    return v_data_cat;
                }
                catch
                {
                    return "";
                }
                
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
