using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_rimborsi : ISoftDeleted, IGestioneStato
    {
        public static string VAL_PRE = "VAL-PRE";
        public static string VAL_DIS = "VAL-DIS";
        public static string VAL_DCR = "VAL-DCR";
        public static string ANN_ANN = "ANN-ANN";
        public static string VAL_VAL = "VAL-VAL";
        public static string VAL_OLD = "VAL-OLD";
        public static string VAL_DEF = "VAL-DEF";

        public static string DOMICILIATO = "D";
        public static string BONIFICO = "B";

        public static string POSITIVO = "POS";
        public static string NEGATIVO = "NEG";

        public static string BENEFICIARIO_CONTRIBUENTE = "C";
        public static string BENEFICIARIO_REFERENTE = "R";
        public static string BENEFICIARIO_TERZO = "T";

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }

        public string importo_Euro
        {
            get
            {
                return importo.ToString("C");
            }
        }

        public string esito
        {
            get
            {
                if (!string.IsNullOrEmpty(ultimo_esito_rimborso))
                {
                    if (tipo_bonifico == POSITIVO)
                    {
                        return "Esito positivo";
                    }
                    else if (tipo_bonifico == NEGATIVO)
                    {
                        return "Esito negativo";
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string tipoRimborso
        {
            get
            {
                if (!string.IsNullOrEmpty(tipo_bonifico))
                {
                    if (tipo_bonifico == BONIFICO)
                    {
                        return "Bonifico ordinario";
                    }
                    else if (tipo_bonifico == DOMICILIATO)
                    {
                        return "Bonifico domiciliato";
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_creazione_rimborso_String
        {
            get
            {
                if (data_creazione_rimborso.HasValue)
                {
                    return data_creazione_rimborso.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_creazione_disposizione_rimborso_String
        {
            get
            {
                if (data_creazione_disposizione_rimborso.HasValue)
                {
                    return data_creazione_disposizione_rimborso.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_pagabilita_disposizione_rimborso_String
        {
            get
            {
                if (data_pagabilita_disposizione_rimborso.HasValue)
                {
                    return data_pagabilita_disposizione_rimborso.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string ultima_data_esito_rimborso_String
        {
            get
            {
                if (ultima_data_esito_rimborso.HasValue)
                {
                    return ultima_data_esito_rimborso.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_effettuazione_bonifico_rimborso_String
        {
            get
            {
                if (data_effettuazione_bonifico_rimborso.HasValue)
                {
                    return data_effettuazione_bonifico_rimborso.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public bool IsSoftDeletable
        {
            get { return true; }
        }
    }
}
