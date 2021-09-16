using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_immobili.Metadata))]
    public partial class tab_immobili
    {
        public static string TIPO_TERRENO = "T";
        public static string TIPO_FABBRICATO = "F";

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

        }
    }
}
