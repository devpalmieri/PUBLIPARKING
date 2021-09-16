using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class join_causale_doc_new : ISoftDeleted
    {
        public static string FLAG_ACQUISIZIONE = "A";
        public static string FLAG_LAVORAZIONE = "L";

        public bool IsSoftDeletable
        {
            get { return true; }
        }
    }
}
