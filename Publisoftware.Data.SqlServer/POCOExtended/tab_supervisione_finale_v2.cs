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
    [MetadataTypeAttribute(typeof(TAB_SUPERVISIONE_FINALE_V2.Metadata))]
    public partial class TAB_SUPERVISIONE_FINALE_V2 : ISoftDeleted, IGestioneStato
    {
        public const string ANN = "ANN-";
        public const string ATT = "ATT-";
        public const string VAL = "VAL-";
        public const string SSP = "SSP-";

        public const string ATT_ATT = "ATT-ATT";

        public const string VAL_VAL = "VAL-VAL";//pratica accettata
        public const string VAL_DIS = "VAL-DIS";
        public const string VAL_IF1 = "VAL-IF1";
        public const string VAL_IF2 = "VAL-IF2";
        public const string ANN_IFF = "ANN-IFF";
        public const string ANN_PEC = "ANN-PEC";
        public const string ANN_ANN = "ANN-ANN";



        public bool IsSoftDeletable
        {
            get { return true; }
        }

        /// <summary>
        /// PROVA DUE
        /// </summary>
        /// <param name="p_idStruttura"></param>
        /// <param name="p_idRisorsa"></param>
        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            DATA_STATO = DateTime.Now;
            ID_STRUTTURA_STATO = p_idStruttura;
            ID_RISORSA_STATO = p_idRisorsa;
        }

        public sealed class Metadata
        {
            private Metadata()
            {
            }
        }
    }
}
