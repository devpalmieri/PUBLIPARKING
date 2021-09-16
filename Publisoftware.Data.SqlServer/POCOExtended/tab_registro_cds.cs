using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_registro_cds.Metadata))]
    public partial class tab_registro_cds :  ISoftDeleted
    {
       

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public void SetUserStato()
        {
            data_stato = DateTime.Now;
           
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }
            [Required]
            [DisplayName("ID")]
            public int id_tab_cds { get; set; }
            [Required]
            [DisplayName("Nome")]
            public string Nome { get; set; }
            [Required]
            [DisplayName("Cognome")]
            public string Cognome { get; set; }
            [Required]
            [DisplayName("Email")]
            public string Email { get; set; }
            [DisplayName("Codice Fiscale")]
            [StringLength(16)]
           
            public string CFPagante { get; set; }
            
            string _tipo_pagante = string.Empty;

            public string TipoPagante
            {
                get
                {
                    if (!string.IsNullOrEmpty(CFPagante))
                        return anagrafica_tipo_contribuente.PERS_FISICA;
                    else
                        return anagrafica_tipo_contribuente.PERS_GIURIDICA;
                }
                set
                {
                    if (!string.IsNullOrEmpty(CFPagante))
                        _tipo_pagante = anagrafica_tipo_contribuente.PERS_FISICA;
                    else
                        _tipo_pagante = anagrafica_tipo_contribuente.PERS_GIURIDICA;
                }

            }
            [DisplayName("Partita Iva")]
            [StringLength(11)]
            public string PIvaPagante { get; set; }
            [Required]
            [DisplayName("Numero Avviso CDS")]
            [StringLength(20)]
            public string NumeroAvvisoCDS { get; set; }
            [Required]
            [DisplayName("Numero Avviso pagoPA")]
            public string NumeroAvvisoPPA { get; set; }
            [Required]
            [DisplayName("Importo")]
            public decimal ImportoPagato { get; set; }
            [Required]
            [DisplayName("Targa")]
            public string Targa { get; set; }

            public DateTime data_stato { get; set; }


        }
    }
}
