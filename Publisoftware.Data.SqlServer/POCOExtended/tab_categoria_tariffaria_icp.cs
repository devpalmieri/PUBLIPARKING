using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_categoria_tariffaria_icp.Metadata))]
    public partial class tab_categoria_tariffaria_icp : ISoftDeleted
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public string QuantitaBaseDa
        {
            get
            {
                return String.Format("{0:0.0000}", quantita_base_da) + " mq";
            }
        }

        public string QuantitaBaseA
        {
            get
            {
                return String.Format("{0:0.0000}", quantita_base_a) + " mq";
            }
        }

        public string ImportoUnitarioBase
        {
            get
            {
                return String.Format("{0:0.0000}", imp_unitario_base) + " €/mq";
            }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

        }
    }
}
