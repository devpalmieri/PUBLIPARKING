using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class anagrafica_categoria : ISoftDeleted
    {
        public const int ALTRO_ID = 2000;
        public const int BOLLO_AUTO = 3000;
        public const string MRCT = "MRCT";

        // Uso civile
        public const string MACROCATEGORIA_DOM = "DOM";
        // Uso commerciale
        public const string MACROCATEGORIA_ECO = "ECO";

        // Occupazione suolo permamente
        public const string MACROCATEGORIA_PSU = "PSU";
        // Occupazione suolo temporanea
        public const string MACROCATEGORIA_TSU = "TSU";

        public const string ICP_INSEGNA_ESERCIZIO = "IDE";
        public const string ICP_MACROCATEGORIA_PERM = "PERMANENTE";

        public const string TOSAP_MACROCATEGORIA_PERM = MACROCATEGORIA_PSU;
        public const string TOSAP_MACROCATEGORIA_TEMP = MACROCATEGORIA_TSU;

        public const string TIPO_OCC_MERCATO_GIORN = "MRCTDAY";
        public const string TIPO_OCC_MERCATO_SETT = "MRCT";
        public const string TIPO_OCC_PASSI_CARR = "PASSI";
        public const string TIPO_OCC_ALTRA = "ALTRA";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public string Descrizione_completa
        {
            get
            {
                return this.sigla_cat_contr + " --- " + this.des_cat_contr;
            }
        }

        public string TipoTosapCosap
        {
            get
            {
                if (macrocategoria == TOSAP_MACROCATEGORIA_TEMP)
                {
                    return "Temporaneo";
                }
                else if (macrocategoria == TOSAP_MACROCATEGORIA_PERM)
                {
                    return "Permanente";
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    } // class
}
