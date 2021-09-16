using Publiparking.Core.Data.SqlServer.POCOExtended.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.SqlServer.Entities
{
    [MetadataTypeAttribute(typeof(tab_tipo_ente.Metadata))]
    public partial class tab_tipo_ente : ISoftDeleted
    {
        public const int CONCESSIONARIO_ID = 0;
        public const int COMUNE_ENTE_ID = 1;
        public const int COMMISSIONE_DISSESTO_ID = 2;
        public const int PROVINCIA_ID = 3;
        public const int REGIONE_ID = 4;
        public const int STATO_ID = 5;
        public const int CONTRIBUENTI_ID = 6;
        public const int CONSORZIO_ID = 7;
        public const int ALTRO_ID = 8;

        public string DescrizioneCodice
        {
            get
            {
                return String.Concat(desc_tipo_ente, " (", cod_tipo_ente, ")");
            }
        }

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID")]
            public int id_tipo_ente { get; set; }

            [Required(ErrorMessage = "Inserire il codice")]
            [RegularExpression("([a-zA-Z0-9]{1,10})", ErrorMessage = "Formato non valido")]
            [DisplayName("Codice")]
            public string cod_tipo_ente { get; set; }

            [Required(ErrorMessage = "Inserire la descrizione")]
            [RegularExpression(@"^[\w\s]{1,50}$", ErrorMessage = "Formato non valido")]
            [DisplayName("Descrizione")]
            public string desc_tipo_ente { get; set; }
        }
    }
}
