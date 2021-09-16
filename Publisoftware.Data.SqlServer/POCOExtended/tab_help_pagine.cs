using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_help_pagine.Metadata))]
    public partial class tab_help_pagine : ISoftDeleted
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

            [DisplayName("ID Help")]
            public int IDHelp { get; set; }

            [DisplayName("Pagina")]
            public string Action { get; set; }
            [DisplayName("Controller")]
            public string Controller { get; set; }
            [DisplayName("Titolo")]
            public string Title { get; set; }
            [DisplayName("Contenuto")]
            public string TextContent { get; set; }
            [DisplayName("Attivo")]
            public bool Active { get; set; }
            [DisplayName("Data Inserimento")]
            public DateTime InsertDate { get; set; }

           
        }
    }
}
