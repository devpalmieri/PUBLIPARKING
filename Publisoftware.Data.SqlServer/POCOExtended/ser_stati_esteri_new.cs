using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(ser_stati_esteri_new.Metadata))]
    public partial class ser_stati_esteri_new : ISoftDeleted
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Id")]
            public int id_stato { get; set; }

            [Required]
            [DisplayName("Sigla")]
            [RegularExpression("[A-Z]{2,2}", ErrorMessage = "Formato non valido")]
            public string sigla_stato { get; set; }

            [Required]
            [DisplayName("Stato")]
            public string denominazione_stato { get; set; }
        }
    }
}
