using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class join_avv_pag_soggetto_debitore : ISoftDeleted, IGestioneStato
    {
        public const string ANN_ANN = "ANN-ANN";
        public const string ANN = "ANN-";
        public const string ATT_ATT = "ATT-ATT";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            id_risorsa_stato = p_idRisorsa;
            id_struttura_stato = p_idStruttura;
        }

        public string ContribuenteReferente
        {
            get
            {
                if (join_referente_contribuente != null)
                {
                    return join_referente_contribuente.tab_referente.referenteDisplay;
                }
                else
                {
                    return tab_avv_pag.tab_contribuente.contribuenteDisplay;
                }
            }
        }
        public string ContribuenteReferenteStampa
        {
            get
            {
                if (join_referente_contribuente != null)
                {
                    return join_referente_contribuente.tab_referente.referenteNominativoDisplay;
                }
                else
                {
                    return tab_avv_pag.tab_contribuente.contribuenteNominativoDisplay;
                }
            }
        }
        public string CodFiscalePiva
        {
            get
            {
                if (join_referente_contribuente != null)
                {
                    return join_referente_contribuente.tab_referente.codFiscalePivaDisplay;
                }
                else
                {
                    return tab_avv_pag.tab_contribuente.codFiscalePivaDisplay;
                }
            }
        }

    }
}
