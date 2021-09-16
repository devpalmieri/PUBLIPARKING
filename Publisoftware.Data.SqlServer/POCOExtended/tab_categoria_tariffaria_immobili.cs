using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_categoria_tariffaria_immobili.Metadata))]
    public partial class tab_categoria_tariffaria_immobili : ISoftDeleted
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public string isEsente
        {
            get
            {
                if (esente)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string Rivaluatazione
        {
            get
            {
                return Convert.ToString((Decimal.Add((Decimal.Multiply(rivalutazione_rendita_catastale, 100)), -100))) + " %";
            }
        }

        public string AliquotaRidotta1
        {
            get
            {
                return Convert.ToString((Decimal.Multiply(aliquota_base_semestre1, 100))) + " %";
            }
        }

        public string AliquotaRidotta2
        {
            get
            {
                return Convert.ToString((Decimal.Multiply(aliquota_base_semestre2, 100))) + " %";
            }
        }

        public string Moltiplicatore
        {
            get
            {
                return Convert.ToString(coefficiente_categoria) + " %";
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
