using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_categoria_tariffaria_tasi.Metadata))]
    public partial class tab_categoria_tariffaria_tasi : ISoftDeleted, IValidatableObject
    {
        public List<ValidationResult> validationErrors()
        {
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(tab_categoria_tariffaria_tasi),
                typeof(tab_categoria_tariffaria_tasi.Metadata)), typeof(tab_categoria_tariffaria_tasi));

            ValidationContext context = new ValidationContext(this, null, null);
            List<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(this, context, results, true);

            return results;
        }

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public string isEsente
        {
            get
            {
                if (esente)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string Rivaluatazione
        {
            get
            {
                return Convert.ToString((Decimal.Add((Decimal.Multiply(rivalutazione_rendita_catastale, 100)), -100))) + " %";
            }
        }

        public string AliquotaRidotta
        {
            get
            {
                return Convert.ToString((Decimal.Multiply(aliquota_base, 100))) + " %";
            }
        }

        public string Moltiplicatore
        {
            get
            {
                return Convert.ToString(coefficiente_categoria) + " %";
            }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID")]
            public int id_categoria_tariffaria_tasi { get; set; }

            [DisplayName("Categoria")]
            public int id_anagrafica_categoria { get; set; }
                       
            [DisplayName("Utilizzo")]
            public string id_utilizzo { get; set; }

            [Required]
            [DisplayName("Anno")]
            public int anno { get; set; }

            [Required]
            [DisplayName("Unità di misura")]
            public string um { get; set; }

            [Required]
            [DisplayName("Rivalutazione rendita catastale")]
            public decimal rivalutazione_rendita_catastale { get; set; }

            [Required]
            [DisplayName("Aliquota base")]
            public decimal aliquota_base { get; set; }

            [Required]
            [DisplayName("Coefficiente categoria")]
            public decimal coefficiente_categoria { get; set; }

            [Required]
            [DisplayName("Esente")]
            public bool esente { get; set; }

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (id_anagrafica_categoria == null && id_utilizzo == null)
            {
                yield return new ValidationResult
                     ("Inserire almeno un valore tra categoria e utilizzo", new[] { "id_anagrafica_categoria" });
            }
        }
    }
}
