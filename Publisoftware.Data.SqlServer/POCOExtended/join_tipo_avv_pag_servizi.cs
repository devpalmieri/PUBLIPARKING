using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(join_tipo_avv_pag_servizi.Metadata))]
    public partial class join_tipo_avv_pag_servizi : ISoftDeleted
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
            [Required]
            [DisplayName("Servizio")]
            public int id_servizio { get; set; }
            [Required]
            [DisplayName("Tipo Avv Pag")]
            public int id_tipo_avv_pag { get; set; }
        }
    }
}
