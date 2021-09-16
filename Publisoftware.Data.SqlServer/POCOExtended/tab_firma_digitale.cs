using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_firma_digitale.Metadata))]
    public partial class tab_firma_digitale : ISoftDeleted
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
            [DisplayName("id tab firma digitale")]
            public int id_tab_firma_digitale { get; set; }
            [DisplayName("id risorsa")]
            public int? id_risorsa { get; set; }
            [DisplayName("firma uno")]
            public byte[] firma_uno { get; set; }
            [DisplayName("firma 2")]
            public byte[] firma_2 { get; set; }
            [DisplayName("firma 3")]
            public byte[] firma_3 { get; set; }
            [DisplayName("firma 4")]
            public byte[] firma_4 { get; set; }
            [DisplayName("firma 5")]
            public byte[] firma_5 { get; set; }
            [DisplayName("timbro")]
            public byte[] timbro { get; set; }
            [DisplayName("sigla")]
            public byte[] sigla { get; set; }
            [MaxLength(1)]
            [DisplayName("flag on off")]
            public string flag_on_off { get; set; }

        }
    }
}
