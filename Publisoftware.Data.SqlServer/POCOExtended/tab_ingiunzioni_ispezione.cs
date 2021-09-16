using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_ingiunzioni_ispezione : ISoftDeleted, IGestioneStato
    {
        public const string ANN_ANN = "ANN-ANN";
        public const string VAL_PRE = "VAL-PRE";
        public const string VAL_VAL = "VAL-VAL";
        public const string VAL_OLD = "VAL-OLD";
        public const string VAL = "VAL-";

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

        public string impDaPagare
        {
            get
            {
                return importo_da_pagare.HasValue ? importo_da_pagare.Value.ToString("C") : 0.ToString("C");
            }
        }

        public string tipoAtti
        {
            get
            {
                return flag_assoggettabile_atti_esecutivi == "1" ? "Atti cautelari ed esecutivi" : "Solo atti cautelari";
            }
        }
    }
}
