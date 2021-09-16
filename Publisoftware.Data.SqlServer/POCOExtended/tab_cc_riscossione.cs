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
    [MetadataTypeAttribute(typeof(tab_cc_riscossione.Metadata))]
    public partial class tab_cc_riscossione : ISoftDeleted
    {
        public const string FLAG_MODALITA_PAGAMENTO_BOLLETTINO = "0";
        public const string FLAG_MODALITA_PAGAMENTO_IUV = "1";

        public const string FLAG_TIPO_CC_CONCESSIONARIO = "0";
        public const string FLAG_TIPO_CC_CONCESSIONARIO_FIRMA_CONGIUNTA = "1";
        public const string FLAG_TIPO_CC_ENTE_GESTITO_DAL_CONCESSIONARIO = "2";

        public const string FLAG_TIPO_CC_CONCESSIONARIO_RIMBORSI = "6";
        public const string FLAG_TIPO_CC_ENTE_RIMBORSI = "7";

        public const string FLAG_TIPO_CC_ENTE = "8";
        public const string FLAG_TIPO_CC_TESORERIA = "9";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public string BancaAgenzia
        {
            get
            {
                return ANAGRAFICA_ABI_CAB != null ? ANAGRAFICA_ABI_CAB.BANCA + " - " + ANAGRAFICA_ABI_CAB.AGENZIA : string.Empty;
            }
        }

        public string IntestazioneConIban
        {
            get
            {
                return IBAN + " - " + intestazione_cc;
            }
        }


        public sealed class Metadata
        {
            private Metadata()
            {

            }

        }
    }
}
