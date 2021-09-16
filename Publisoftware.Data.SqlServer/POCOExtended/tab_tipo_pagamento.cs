using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_tipo_pagamento.Metadata))]
    public partial class tab_tipo_pagamento : ISoftDeleted
    {
        public static int BollettinoCCPostale = 1;
        public static int Bonifico = 2;
        public static int BollInternetICI = 3;
        public static int BollettinoSmarrito = 4;
        public static int BollettinoSmarritoRitrovato = 5;
        public static int EuroGiro = 6;
        public static int EuroGiroRitrovato = 7;
        public static int Postagiro = 8;
        public static int BollOrdinarioICI = 10;
        public static int PagamentoF24ICI = 14;
        public static int BonificoICI = 15;
        public static int BollettiniDematerializzati = 17;
        public static int PagamentiImportatiICIIMU = 23;
        public static int BonificoEstero = 24;
        public static int DispGiroConto = 25;
        public static int VersAssPostaliNonStandard = 26;
        public static int AccrPostinoTelematico = 27;
        public static int CartaCredito = 28;
        public static int F24 = 29;
        public static int AccreditoTelematico = 9;
        public static int AccreditoInternet = 12;
        public static int CCGestitoEnte = 30;
        public static int CCGenerico = 31;
        public static int PagoPA = 32;

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
            public int id_tipo_pag { get; set; }

            [Required(ErrorMessage = "Inserire il codice")]
            [DisplayName("Codice")]
            public string codice_pag { get; set; }

            [Required(ErrorMessage = "Inserire la descrizione")]
            [DisplayName("Descrizione")]
            public string descrizione_pag { get; set; }

            [DisplayName("Importo massimo")]
            [RegularExpression(@"[\d]{1,4}([.,][\d]{1,2})?", ErrorMessage = "Formato non valido")]
            public decimal importo_massimo { get; set; }
        }
    }
}
