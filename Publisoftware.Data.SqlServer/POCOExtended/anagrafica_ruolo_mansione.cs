using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class anagrafica_ruolo_mansione : ISoftDeleted
    {
        // Non serve: public const string COD_RUOLO_MANSIONE_UFFICIALE_RISCOSSIONE = "UFFRIS";
        public const int COD_RUOLO_MANSIONE_UFFICIALE_RISCOSSIONE_ID = 12;

        // Non serve: public const string COD_RUOLO_MANSIONE_AVVOCATO = "LEGAVV";
        public const int COD_RUOLO_MANSIONE_AVVOCATO_ID = 11;

        public const int COD_RUOLO_MANSIONE_LEGALE_RAPPRESENTANTE_ID = 14;

        public bool IsSoftDeletable
        {
            get { return true; }
        }
    }
}
