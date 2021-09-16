using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public class PSBaseEntity<T, TMetadata> : IValidator
    {
        public List<ValidationResult> validationErrors()
        {
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(T), typeof(TMetadata)), typeof(T));

            ValidationContext context = new ValidationContext(this, null, null);
            List<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(this, context, results, true);

            return results;
        }

        // IsValid effettua la validazione ogni volta che viene chiamata,
        // il che accade tipicamente una sola volta, se si dovesse usare chiamandola più volte,
        // usare una variabile membro
        public bool IsValid
        {
            get
            {
                return validationErrors().Count == 0;
            }
        }
    }
}
