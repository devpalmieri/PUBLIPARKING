using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_agevolazione.Metadata))]
    public partial class anagrafica_agevolazione : ISoftDeleted
    {

        public const string CALC_AGE_QF_QV = "0";
        public const string CALC_AGE_QF = "1";
        public const string CALC_AGE_QV = "2";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public string Percentuale
        {
            get
            {
                return Decimal.Round(aliquota_base_calcolo, 2) + " %";
            }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID")]
            public int id_anagrafica_agevolazione { get; set; }

            [Required(ErrorMessage = "Inserire l'entrata")]
            [DisplayName("Entrata")]
            public int id_entrata { get; set; }

            [Required(ErrorMessage = "Inserire il codice agevolazione")]
            [DisplayName("Tipo Agevolazione")]
            public int id_tipo_agevolazione { get; set; }

            [Required(ErrorMessage = "Inserire il tipo agevolazione")]
            [DisplayName("Codice Agevolazione")]
            public int cod_agevolazione { get; set; }

            [Required(ErrorMessage = "Inserire la descrizione dell'agevolazione")]
            [DisplayName("Descrizione Agevolazione")]
            public int des_agevolazione { get; set; }

            [Required(ErrorMessage = "Inserire il tipo di base calcolo")]
            [DisplayName("Tipo Base Calcolo")]
            public string tipo_base_calcolo { get; set; }

            [DisplayName("Id Fonte")]
            public int id_fonte { get; set; }

            [DisplayName("Id Provvedimento")]
            public int id_provvedimento { get; set; }

            [DisplayName("Anno")]
            [RegularExpression("[0-9]{0,}", ErrorMessage = "Formato non valido")]
            public int anno { get; set; }

            [Required(ErrorMessage = "Inserire l'aliquota di base calcolo")]
            [DisplayName("Aliquota Base Calcolo")]
            [RegularExpression("[\\d]{1,2}([.,][\\d]{1,4})?", ErrorMessage = "Formato non valido")]
            public int aliquota_base_calcolo { get; set; }

            [DisplayName("Importo Base Calcolo")]
            [RegularExpression("[\\d]{1,2}([.,][\\d]{1,4})?", ErrorMessage = "Formato non valido")]
            public int imp_base_calcolo { get; set; }

            [DisplayName("UM Agevolazione")]
            public string um_agevolazione { get; set; }

            [DisplayName("MacroCategoria")]
            public string macrocategoria { get; set; }

            [DisplayName("MacroCausale")]
            public string macrocausale { get; set; }
        }
    }
}
