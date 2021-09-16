using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_autorita_giudiziaria.Metadata))]
    public partial class tab_autorita_giudiziaria : ISoftDeleted
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public static string SIGLA_AUTORITA_ACI = "ACI";
        public static string SIGLA_AUTORITA_CTP = "CTP";
        public static string SIGLA_AUTORITA_CTR = "CTR";
        public static string SIGLA_AUTORITA_GDP = "GDP";
        public static string SIGLA_AUTORITA_TRIB = "TRIB";
        public static string SIGLA_AUTORITA_TRIFAL = "TRIFAL";
        public static string SIGLA_AUTORITA_TAR = "TAR";
        public static string SIGLA_AUTORITA_COS = "COS";
        public static string SIGLA_AUTORITA_CAS = "CAS";
        public static string SIGLA_AUTORITA_COR = "COR";

        public static string AUTORITA_PROVINCIALE = "Commissione Tributaria Provinciale";
        public static string AUTORITA_REGIONALE = "Commissione Tributaria Regionale";
        public static string AUTORITA_GDP = "Giudice di Pace";
        public static string AUTORITA_TRIB = "Tribunale Ordinario";
        public static string AUTORITA_TAR = "Tribunale Amministrativo Regionale";
        public static string AUTORITA_COS = "Consiglio di Stato";
        public static string AUTORITA_CAS = "Corte di Cassazione";
        public static string AUTORITA_COR = "Corte di Appello";

        internal sealed class Metadata
        {
            private Metadata()
            {
            }
               
            [DisplayName("Id")]
            public int id_tab_autorita_giudiziaria { get; set; }

            [Required]
            [DisplayName("Tipo Documento Entrata")]
            public int id_tipo_doc_entrata { get; set; }

            [Required]            
            [DisplayName("Sigla")]
            public string sigla_autorita { get; set; }

            [Required]            
            [DisplayName("Descrizione")]
            public string descrizione_autorita { get; set; }

            [DisplayName("Territorio di competenza")]
            public string territorio_competenza { get; set; }

            [DisplayName("Comune")]
            public string comune_ubicazione { get; set; }

            [DisplayName("CAP")]
            [RegularExpression("[0-9]{5}", ErrorMessage = "Formato non valido")]
            public string cap_ubicazione { get; set; }

            [DisplayName("Indirizzo")]
            public string indirizzo_ubicazione { get; set; }
        }
    }
}
