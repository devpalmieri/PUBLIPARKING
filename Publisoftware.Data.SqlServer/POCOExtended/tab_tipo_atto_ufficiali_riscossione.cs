using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_tipo_atto_ufficiali_riscossione.Metadata))]
    public partial class tab_tipo_atto_ufficiali_riscossione : ISoftDeleted
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
