using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(join_ogg_contatore.Metadata))]
    public partial class join_ogg_contatore : ISoftDeleted
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
        }
    }
}
