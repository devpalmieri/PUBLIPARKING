using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_esito_sentenze.Metadata))]
    public partial class anagrafica_esito_sentenze : ISoftDeleted
    {
        public static int ACCOLTA_ID = 1;
        public static int RESPINTA_ID = 2;
        public static string ACCOLTA = "ACC";
        public static string RESPINTA = "RES";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public string DescrizioneSigla
        {
            get
            {
                return descrizione + " " + sigla;
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
