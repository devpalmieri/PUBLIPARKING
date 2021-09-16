using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_tipo_pagamento.Metadata))]
    public partial class anagrafica_tipo_pagamento : ISoftDeleted
    {
        public static int BOLPOSTA = 5;
        public static int BONBANCA = 3;
        public static int POSTAGIRO = 6;
        public static int ACCRTELEM = 7;
        public static int MODF24 = 2;
        public static int POS = 8;
        public static int BONTELEM = 10;
        public static int BOLTELEM = 11;
        public static int CCENTE = 12;
        public static int CCGENERICO = 13;

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

        }
    }
}
