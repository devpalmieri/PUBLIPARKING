using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class join_causali_motivazioni_causali_esito : ISoftDeleted
    {
        public const string ANN = "ANN-";
        public const string ATT = "ATT-";

        public bool IsSoftDeletable
        {
            get { return true; }
        }
    }
}
