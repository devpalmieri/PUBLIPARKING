using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_esclusione_rateizzazione : ISoftDeleted
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }
    }
}
