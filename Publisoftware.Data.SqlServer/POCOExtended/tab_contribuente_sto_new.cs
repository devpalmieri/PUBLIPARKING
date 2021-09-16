using Publisoftware.Data.CustomValidationAttrs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_contribuente_sto_new : ISoftDeleted, IGestioneStato
    {
        public const string ATT = "ATT-";
        public const string ATT_ATT = "ATT-ATT";

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_risorsa_stato = p_idRisorsa;
            id_struttura_stato = p_idStruttura;
        
            if (cod_stato == null)
            {
                cod_stato = anagrafica_stato_contribuente.ATT_ATT;
            }
            if (cod_stato_contribuente == null)
            {
                cod_stato_contribuente = anagrafica_stato_contribuente.ATT_ATT;
            }
        }

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public string cittaDisplay
        {
            get
            {
                if (tab_toponimi != null && tab_toponimi.id_tipo_toponimo != 0)
                {
                    if (tab_toponimi.ser_comuni != null)
                    {
                        return tab_toponimi.ser_comuni.des_comune;
                    }
                    else
                    {
                        return citta;
                    }
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
                if (tab_toponimi != null && tab_toponimi.id_tipo_toponimo != 0)
                {
                    return tab_toponimi.descrizione_toponimo;
                }
                else
                {
                    return indirizzo;
                }
            }
        }
        public string civico
        {
            get
            {
                if (sigla_civico != null && sigla_civico != string.Empty)
                {
                    if (nr_civico.HasValue && nr_civico != 0)
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
       
        public string indirizzoRaccomandata
        {
            get
            {
                return ((indirizzoDisplay != null && indirizzoDisplay != string.Empty) ? indirizzoDisplay : string.Empty) +
                                            ((civico != null && civico != string.Empty && civico.Trim() != "0") ? " " + civico : string.Empty) +
                                            ((sigla_civico != null && sigla_civico != string.Empty && sigla_civico != "SNC") ? " " + sigla_civico.Trim() : string.Empty) +
                                            ((condominio != null && condominio != string.Empty) ? " " + condominio.Trim() : string.Empty) +
                                            ((colore != null && colore != string.Empty) ? " " + colore.Trim() : string.Empty) +
                                            ((km.HasValue) ? ", km: " + km : string.Empty);
            }
        }

        public string CapComuneProvinciaPerStampa
        {
            get
            {
                return ((cap != null && cap != string.Empty) ? cap : string.Empty) + " " +
                    ((cittaDisplay != null && cittaDisplay != string.Empty) ? cittaDisplay : string.Empty) +
                    ((prov != null && prov != string.Empty) ? " " + prov : string.Empty);
            }
        }
    }
}
