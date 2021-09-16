using Publisoftware.Data.CustomValidationAttrs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_allegati_digitali.Metadata))]
    public partial class tab_allegati_digitali : ISoftDeleted
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        [DisplayName("Data Creazione")]
        public string data_creazione_String
        {
            get
            {
                return data_creazione.ToShortDateString();
            }
            set
            {
                data_creazione = DateTime.Parse(value);
            }
        }

        public sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Id")]
            public int id_tab_allegati_digitali { get; set; }

            [DisplayName("Nome File")]
            public string nome_file { get; set; }

            [DisplayName("Formato File")]
            public string formato_file { get; set; }

            [DisplayName("Percorso File")]
            public string path_file { get; set; }

            [DisplayName("Data Creazione")]
            public DateTime data_creazione { get; set; }
        }
    }
}