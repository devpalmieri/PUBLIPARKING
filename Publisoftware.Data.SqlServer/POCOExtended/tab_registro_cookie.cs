using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_registro_cookie.Metadata))]
    public partial class tab_registro_cookie : ISoftDeleted
    {

        public const string CONSENSO_ALL = "TOTALE";
        public const string CONSENSO_PARTIAL = "PARZIALE";
        public const string CONSENSO_DENIED = "NEGATO";
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        //public void SetUserStato()
        //{
        //    data_stato = DateTime.Now;

        //}
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            public int id_tab_registro_cookie { get; set; }

            public string indirizzo_ip { get; set; }
            public string headers { get; set; }

            public string session_id { get; set; }
            public DateTime data_prima_visita { get; set; }
            public DateTime data_ultima_visita { get; set; }
            public string consenso { get; set; }
            public bool consenso_necessari { get; set; }
            public bool consenso_preferenze { get; set; }
            public bool consenso_statistiche { get; set; }
            public DateTime data_stato { get; set; }


        }

    }
}
