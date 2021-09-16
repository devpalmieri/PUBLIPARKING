using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_pianificazione_cicli_input_rendicontazione.Metadata))]
    public partial class tab_pianificazione_cicli_input_rendicontazione
    {
        public tab_pianificazione_cicli_input_rendicontazione()
        {
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(tab_pianificazione_cicli_input_rendicontazione), typeof(tab_pianificazione_cicli_input_rendicontazione.Metadata)), typeof(tab_pianificazione_cicli_input_rendicontazione));
        }

        public List<ValidationResult> validationErrors()
        {
            ValidationContext context = new ValidationContext(this, null, null);
            List<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(this, context, results, true);
            return results;
        }
                      
        public sealed class Metadata
        {
            public Metadata()
            {
            }
            
            [RegularExpression("[0-9a-zA-Z]+([0-9a-zA-Z]*[-._+])*[0-9a-zA-Z]+@[0-9a-zA-Z]+([-.][0-9a-zA-Z]+)*([0-9a-zA-Z]*[.])[a-zA-Z]{2,6}", ErrorMessage = "Conto corrente riscossione non valido.")]                       
            public string ID_cc_riscossione1;
            [RegularExpression("[0-9a-zA-Z]+([0-9a-zA-Z]*[-._+])*[0-9a-zA-Z]+@[0-9a-zA-Z]+([-.][0-9a-zA-Z]+)*([0-9a-zA-Z]*[.])[a-zA-Z]{2,6}", ErrorMessage = "Secondo Conto corrente riscossione  non valido.")]
            public string ID_cc_riscossione2;
            [RegularExpression("[0-9a-zA-Z]+([0-9a-zA-Z]*[-._+])*[0-9a-zA-Z]+@[0-9a-zA-Z]+([-.][0-9a-zA-Z]+)*([0-9a-zA-Z]*[.])[a-zA-Z]{2,6}", ErrorMessage = "Terzo Conto corrente riscossione non valido.")]
            public string ID_cc_riscossione3;
            [RegularExpression("[0-9a-zA-Z]+([0-9a-zA-Z]*[-._+])*[0-9a-zA-Z]+@[0-9a-zA-Z]+([-.][0-9a-zA-Z]+)*([0-9a-zA-Z]*[.])[a-zA-Z]{2,6}", ErrorMessage = "Quarto corrente riscossione non valido.")]
            public string ID_cc_riscossione4;

        }
    }
}
