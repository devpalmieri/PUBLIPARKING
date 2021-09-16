using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_ente_servizi.Metadata))]
    public partial class tab_ente_servizi : ISoftDeleted
    {
        public const string CON_flag_tipo_gestione_gestito_dall_concessionario = "CON";
        public const string IMP_flag_tipo_gestione_gestito_dall_ente = "IMP";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Required]
            [DisplayName("Entrata")]
            public int id_entrata { get; set; }

            [Required]
            [DisplayName("Tipo servizio")]
            public int id_tipo_servizio { get; set; }

        }
    }
}
