using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_file_trasmessi.Metadata))]
    public partial class tab_file_trasmessi : ISoftDeleted
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
