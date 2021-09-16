using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_categoria_tariffaria_ZTL : ISoftDeleted
    {

        public bool IsSoftDeletable
        {
            get { return true; }
        }
    }
}
