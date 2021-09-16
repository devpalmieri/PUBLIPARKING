using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_categoria_tariffaria_cosap : ISoftDeleted
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public string Tariffa
        {
            get
            {
                return String.Format("{0:0.00000}", tariffa_base) + " €/MQ";
            }
        }
    }
}
