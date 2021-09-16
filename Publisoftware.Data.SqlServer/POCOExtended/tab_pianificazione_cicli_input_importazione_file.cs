using Publisoftware.Data.CustomValidationAttrs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{

    [MetadataTypeAttribute(typeof(tab_pianificazione_cicli_input_importazione_file))]
    public partial class tab_pianificazione_cicli_input_importazione_file : IValidatableObject
    {

        public tab_pianificazione_cicli_input_importazione_file()
        {
            TypeDescriptor.AddProviderTransparent(
                new AssociatedMetadataTypeTypeDescriptionProvider(
                    typeof(tab_pianificazione_cicli_input_importazione_file), typeof(tab_pianificazione_cicli_input_importazione_file.Metadata)), typeof(tab_pianificazione_cicli_input_importazione_file));
        }

        public List<ValidationResult> validationErrors()
        {
            ValidationContext context = new ValidationContext(this, null, null);
            List<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(this, context, results, true);
            return results;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            return results;            
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Path F24")]
            public string path_f24 { get; set; }
        }
    }
}
