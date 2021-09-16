using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_stato_intervento.Metadata))]
    public partial class anagrafica_stato_intervento : ISoftDeleted
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

            [DisplayName("ID")]
            public int id_anagrafica_stato { get; set; }
            [DisplayName("Codice Stato")]
            [StringLength(7)]
            [Required]
            public string cod_stato { get; set; }
            [DisplayName("Descrizione Stato")]
            [Required]
            public string desc_stato { get; set; }
        }
    }
}
