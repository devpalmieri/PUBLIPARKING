using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class anagrafica_parentela : ISoftDeleted
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }
    }
}
