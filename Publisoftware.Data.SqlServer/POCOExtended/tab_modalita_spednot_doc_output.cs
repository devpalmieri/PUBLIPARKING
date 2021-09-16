using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_modalita_spednot_doc_output.Metadata))]
    public partial class tab_modalita_spednot_doc_output : ISoftDeleted
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
