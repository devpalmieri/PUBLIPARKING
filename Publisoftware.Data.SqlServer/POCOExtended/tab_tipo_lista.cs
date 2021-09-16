using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_tipo_lista.Metadata))]
    public partial class tab_tipo_lista : ISoftDeleted
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public const string FLAG_TIPO_LISTA_C = "C";
        public const string FLAG_TIPO_LISTA_I = "I";

        public const string TIPOLISTA_RATEIZZAZIONE = "RAT";
        public const int TIPOLISTA_RATEIZZAZIONE_ID = 1117;

        public const string TIPOLISTA_EMISSIONE = "EME";
        public const int TIPOLISTA_EMISSIONE_ID = 1113;

        public const string TIPOLISTA_RIEMISSIONE = "REM";
        public const int TIPOLISTA_RIEMISSIONE_ID = 2141;

        public const string TIPOLISTA_EMISSIONE_RETTIFICA = "REM";
        public const int TIPOLISTA_EMISSIONE_RETTIFICA_ID = 1125;

        public const string TIPOLISTA_TRASMISSIONE = "TRA";
        public const string TIPOLISTA_TRASMISSIONE_AVVISI_SINGOLI = "TRS";
        public const string TIPOLISTA_TRASMISSIONE_CONTRIBUENTI = "CON";
        public const string TIPO_LISTA_SPEDIZIONE = "SPE";
        public const int TIPO_LISTA_SPEDIZIONE_ID = 1119;
        public const string TIPO_LISTA_RETT_TRASMISSIONE = "RTR";
        public const string TIPO_LISTA_RETT_EMISSIONE = "REM";
        public const string TIPOLISTA_NGE = "NGE";
        public const string TIPOLISTA_NOT = "NOT";
        public const string TIPOLISTA_DIP = "DIP";
        public const int TIPOLISTA_DIP_ID = 1126;

        public const string TIPOLISTA_COMUNICAZIONI = "DOC";
        public const int TIPO_LISTA_COMUNICAZIONI_ID = 2133;

        public const string TIPOLISTA_LISTA_DI_SGRAVIO = "SGR";
        public const int TIPOLISTA_LISTA_DI_SGRAVIO_ID = 1129;

        public const string TIPOLISTA_LISTA_DI_SGRAVIO_ISTANZAANNRET = "SGI";
        public const int TIPOLISTA_LISTA_DI_SGRAVIO_ISTANZAANNRET_ID = 2132;

        public const int TIPOLISTA_TRASMISSIONE_ID = 1114;

        public const string TIPOLISTA_LISTA_DI_SOSPENSIONE = "SSP";
        public const int TIPOLISTA_LISTA_DI_SOSPENSIONE_ID = 1131;

        public const string TIPOLISTA_CARICO = "CAR";

        public const string TIPOLISTA_LISTA_DI_DEFINIZIONE_AGEVOLATA = "DEF";
        public const int TIPOLISTA_LISTA_DI_DEFINIZIONE_AGEVOLATA_ID = 2134;

        // ------------------------------------------------------------
        // Lista Posizioni debitorie coattive
        public const string TIPOLISTA_LISTA_ISPEZIONI = "ISP";
        public const int TIPOLISTA_LISTA_ISPEZIONI_ID = 2135;
        // ------------------------------------------------------------

        //public const string TIPO_LISTA_RIEMISSIONE_TARI = "EME";
        //public const int TIPO_LISTA_RIEMISSIONE_TARI_ENTRATA = 30;

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Id")]
            public int id_tipo_lista { get; set; }

            [Required]
            [DisplayName("Entrata")]
            public int id_entrata { get; set; }

            [Required]
            [RegularExpression("[a-zA-Z0-9]{1,7}", ErrorMessage = "Formato non valido")]
            [DisplayName("Codice")]
            public string cod_lista { get; set; }

            [Required]
            [DisplayName("Descrizione")]
            public string desc_lista { get; set; }

            [Required]
            [RegularExpression("[A-Z]{1,1}", ErrorMessage = "Formato non valido")]
            [DisplayName("Tipo Lista")]
            public string flag_tipo_lista { get; set; }
        }
    }
}
