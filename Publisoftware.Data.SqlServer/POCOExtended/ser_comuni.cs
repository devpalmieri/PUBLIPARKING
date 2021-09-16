using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(ser_comuni.Metadata))]
    public partial class ser_comuni
    {
        public int id
        {
            get { return cod_comune; }
        }

        public string descrizione
        {
            get { return des_comune; }
        }



        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Id")]
            public int cod_comune { get; set; }

            [Required]
            [DisplayName("Descrizione")]
            public string des_comune { get; set; }

            [Required]
            [DisplayName("CAP")]
            [RegularExpression("[0-9]{5}", ErrorMessage = "Formato non valido")]
            public string cap_comune { get; set; }

            [Required]
            [RegularExpression("[a-zA-Z0-9]{1,4}", ErrorMessage = "Formato non valido")]
            [DisplayName("Codice catasto")]
            public string cod_catasto { get; set; }

            [DisplayName("Codice comune tribunale")]
            public int cod_comune_tribunale { get; set; }

            [Required]
            [DisplayName("Provincia")]
            public int cod_provincia { get; set; }

            [Required]
            [DisplayName("Regione")]
            public int cod_regione { get; set; }
        }
    }
}
