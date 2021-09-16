using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class join_tipo_lista_tipo_avv_pag : ISoftDeleted
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }


    }
}
