using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_menu_orizzontale.Metadata))]
    public partial class tab_menu_orizzontale : ISoftDeleted
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }
        
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID Menu")]
            public int Id_Menu { get; set; }

            [DisplayName("ID Ente")]
            public int Id_Ente { get; set; }
            [DisplayName("ID Struttura")]
            public int Id_Struttura { get; set; }

            [DisplayName("Modalità Operativa")]
            public string ModOp { get; set; }
            [DisplayName("Voce Menù")]
            public string Descrizione { get; set; }
            [DisplayName("Titolo")]
            public string Title { get; set; }
            public string Action { get; set; }
            public string Controller { get; set; }
            public string URL { get; set; }


        }
    }
}
