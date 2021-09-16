using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class join_tab_cc_riscoss_tipo_avvpag : ISoftDeleted
    {

        public bool IsSoftDeletable
        {
            get { return true; }
        }
    }
}
