using Publisoftware.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_ente.Metadata))]
    public partial class anagrafica_ente : ISoftDeleted
    {
        public const int ID_ENTE_PUBLISERVIZI = 1;
        public const int ID_ENTE_GENERICO = 14;
        public const int ID_ENTE_COMUNE_DI_AUGUSTA = 26;
        public const int ID_ENTE_COMUNE_DI_BELLONA = 25;
        public const int ID_ENTE_COMUNE_DI_CAMPONELLELBA = 303;
        public const int ID_ENTE_COMUNE_DI_CASERTA = 2;
        public const int ID_ENTE_COMUNE_DI_POMPEI = 33;
        public const int ID_ENTE_COMUNE_DI_TORRE_DEL_GRECO = 30;
        public const int ID_ENTE_COMUNE_DI_LUSCIANO = 20;
        public const int ID_ENTE_COMUNE_DI_PASTORANO = 21;
        public const int ID_ENTE_COMUNE_DI_VITULAZIO = 36;
        public const int ID_ENTE_COMUNE_DI_GRAGNANO = 8;
        public const int ID_ENTE_COMUNE_DI_CASTELFRANCO = 302;
        public const int ID_ENTE_COMUNE_DI_CRISPIANO = 46;
        public const int ID_ENTE_COMUNE_DI_FIRENZE = 200;
        public const int ID_ENTE_COMUNE_DI_MONTE_SANT_ANGELO = 55;
        public const int ID_ENTE_COMUNE_DI_PAOLISI = 56;
        public const int ID_ENTE_COMUNE_DI_BOSCO_TRE_CASE = 57;
        public const int ID_ENTE_COMUNE_DI_PONTASSIEVE = 301;
        public const int ID_ENTE_COMUNE_DI_PIOMBINO = 304;
        public const int ID_ENTE_COMUNE_DI_SAN_LORENZELLO = 24;
        public const int ID_ENTE_COMUNE_DI_SIGNA = 300;
        public const int ID_ENTE_REGIONE_LOMBARDIA = 100;
        public const int ID_ENTE_CITL = 13;
        public const string FLAG_TIPO_GESTIONE_PAGOPA_PUBLISERVIZI = "3";
        public const string FLAG_TIPO_GESTIONE_PAGOPA_PARTNERTECNOLOGICO = "1";
        public const string FLAG_TIPO_GESTIONE_PAGOPA_SUPPORTO = "2";
        public const string AUX_DIGIT_1 = "1";
        public const string AUX_DIGIT_2 = "2";
        public const string AUX_DIGIT_3 = "3";

        private string prov_descr = string.Empty;
        private string comune_descr = string.Empty;
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public string password_dbD
        {
            get
            {
                return password_db!=null ? CryptMD5.Decrypt(password_db) : null;
            }
        }
        public string IndirizzoCompleto
        {
            get
            {
                if (string.IsNullOrEmpty(indirizzo)) indirizzo = string.Empty;
                if (string.IsNullOrEmpty(cap)) cap = string.Empty;
                if (string.IsNullOrEmpty(indirizzo)) indirizzo = string.Empty;
                if (ser_province ==null) 
                    prov_descr = string.Empty;
                else
                {
                    if (string.IsNullOrEmpty(ser_province.des_provincia))
                        prov_descr = string.Empty;
                    else
                        prov_descr = ser_province.sig_provincia;
                }
                if (ser_comuni == null)
                    comune_descr = string.Empty;
                else
                {
                    if (string.IsNullOrEmpty(ser_comuni.des_comune))
                        comune_descr = string.Empty;
                    else
                        comune_descr = ser_comuni.des_comune;
                }
                return indirizzo + ", " + cap + " - " + comune_descr + " (" + prov_descr + ")";
            }
        }
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID")]
            public int id_ente { get; set; }

            [Required]
            [StringLength(12)]
            [DisplayName("Codice Ente")]
            public string codice_ente { get; set; }

            [DisplayName("Codice ISTAT Ente")]
            public string cod_ente { get; set; }

            [Required]
            [DisplayName("Descrizione Ente")]
            public string descrizione_ente { get; set; }

            [RegularExpression("[0-9]{5}", ErrorMessage = "CAP non valido (es. 80100)")]
            [DisplayName("CAP")]
            public string cap { get; set; }

            [DisplayName("Indirizzo")]
            public string indirizzo { get; set; }

            [StringLength(50)]
            [DisplayName("Telefono 1")]
            public string tel1 { get; set; }

            [StringLength(50)]
            [DisplayName("Telefono 2")]
            public string tel2 { get; set; }

            // Per gli enti il CF è uguale all'IVA!
            //[RegularExpression("[A-Z]{6}[A-Z0-9]{2}[A-Z][A-Z0-9]{2}[A-Z][A-Z0-9]{3}[A-Z]", ErrorMessage = "Formato Codice Fiscale Non Valido")]
            [RegularExpression("[0-9]{11}", ErrorMessage = "Formato Codice Fiscale Non Valido")]
            [DisplayName("Codice Fiscale")]
            public string cod_fiscale { get; set; }

            [RegularExpression("[0-9]{11}", ErrorMessage = "Formato Partita IVA Non Valido")]
            [DisplayName("Partita IVA")]
            public string p_iva { get; set; }

            [Required]
            [DisplayName("Tipo Ente")]
            public int id_tipo_ente { get; set; }

            [DisplayName("Gestione pagamenti con PAGOPA")]
            public string flag_tipo_gestione_pagopa { get; set; }

            [DisplayName("Tipo gestione PAGOPA")]
            public string aux_digit_pagopa { get; set; }

            [StringLength(2)]
            [DisplayName("Codice segregazione attribuito dall'Ente")]
            public string codice_segregazione_pagopa { get; set; }
        }
    }
}
