using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(join_risorse_ser_comuni.Metadata))]
    public partial class join_risorse_ser_comuni : ISoftDeleted
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }
        public sealed class Metadata
        {
            private Metadata()
            {

            }
        }
    }
}
