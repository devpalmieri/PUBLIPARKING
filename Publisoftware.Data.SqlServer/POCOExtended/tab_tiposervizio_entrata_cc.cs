using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{

    [MetadataTypeAttribute(typeof(tab_tipo_servizio_entrata_cc.Metadata))]
    public partial class tab_tipo_servizio_entrata_cc : ISoftDeleted
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
