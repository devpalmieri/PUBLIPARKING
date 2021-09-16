using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_registro_pag_senza_avviso.Metadata))]
    public partial class tab_registro_pag_senza_avviso : ISoftDeleted
    {

        public const string TIPO_PAGAMENTO_SPUNTISTA = "SPUNTISTA";
        public const string TIPO_PAGAMENTO_CDS = "CDS";
        public const string TIPO_PAGAMENTO_AFF = "AFFISIONI";
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
            public int id_tab_pag_senza_avviso { get; set; }

            public int id_ente { get; set; }
            public decimal id_contribuente { get; set; }

            public int id_spuntista { get; set; }
            [Required]
            [DisplayName("Nome")]
            public string Nome { get; set; }
            [Required]
            [DisplayName("Cognome")]
            public string Cognome { get; set; }
            [DisplayName("Ragione Sociale")]
            public string RagSoc { get; set; }
            [Required]
            [DisplayName("Email")]
            public string Email { get; set; }


            [Required]
            [DisplayName("Cellulare")]
            public string Mobilel { get; set; }

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
            [DisplayName("Numero Avviso")]
            [StringLength(20)]
            public string NumeroAvviso { get; set; }
            [Required]
            [DisplayName("Numero Avviso pagoPA")]
            public string NumeroAvvisoPPA { get; set; }
            [Required]
            [DisplayName("Importo")]
            public decimal ImportoPagato { get; set; }
            public int id_area_mercato { get; set; }
            public string ubicazione_area_mercato { get; set; }
            public int NumPiazzolePrenotate { get; set; }

            public decimal MQPiazzolaSpuntisti { get; set; }
            public int id_carrello { get; set; }
            public DateTime data_mercato_prenotata { get; set; }
            public DateTime data_verbale { get; set; }
            public int anno_rif { get; set; }
            public string tipo_pagamento { get; set; }
            public DateTime data_stato { get; set; }


        }
    }
}
