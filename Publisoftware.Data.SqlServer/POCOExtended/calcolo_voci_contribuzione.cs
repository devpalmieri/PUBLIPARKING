using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(calcolo_voci_contribuzione.Metadata))]
    public partial class calcolo_voci_contribuzione : ISoftDeleted
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
            public int id_calcolo_voci_contrib { get; set; }
        }
    }
}
