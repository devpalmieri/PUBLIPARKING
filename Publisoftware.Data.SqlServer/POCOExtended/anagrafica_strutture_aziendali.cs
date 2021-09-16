using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_strutture_aziendali.Metadata))]
    public partial class anagrafica_strutture_aziendali : ISoftDeleted
    {
        public const int STRUTTURA_ADMIN = 99;

        //Il dottore vuole indicare tramite il campo prog_lav la tipologia della struttura
        public static int BACK_OFFICE = 1;
        public static int FRONT_OFFICE = 2;
        public static int ALL = 3;
        
        public static string TIPO_STRUTTURA_INTERNA = "1";
        public static string TIPO_STRUTTURA_SPORTELLO = "2";
        public static string TIPO_STRUTTURA_CALL_CENTER = "3";
        public static string TIPO_STRUTTURA_TECNICA = "4";
        public static string TIPO_STRUTTURA_SPORTELLO_PARK = "5";
        public static string TIPO_STRUTTURA_BACK_OFFICE = "6";
        public static string TIPO_STRUTTURA_INTERFACCIA = "7";
        public static string TIPO_STRUTTURA_SUPERVISIONE = "8";

        public static string TIPO_STRUTTURA_UFFICIO_TRIBUTI = "9"; //TODO: Far sparire dal codice
        //codici strutture aziendali 
        public static string COD_STRUTTURA_SERVIZI_LEGALI = "SLEG";
        //Tipi struttura non sottoposte a filtri di accesso alle liste
        public static IList<string> TipiStrutturaSuperiori = new List<string>() { anagrafica_strutture_aziendali.TIPO_STRUTTURA_INTERFACCIA,
                                                                                  anagrafica_strutture_aziendali.TIPO_STRUTTURA_SUPERVISIONE};
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public string CodiceDescrizione
        {
            get
            {
                return codice_struttura_aziendale != null ? codice_struttura_aziendale + " - " + descr_struttura : descr_struttura;
            }
        }

        public bool IsGenerica { get; set; }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID")]
            public int id_struttura_aziendale { get; set; }

            [Required]
            [DisplayName("Codice Struttura")]
            [StringLength(8)]
            public string codice_struttura_aziendale { get; set; }

            [Required]
            [DisplayName("Descr. Struttura")]
            [StringLength(100)]
            public string descr_struttura { get; set; }

            [StringLength(10)]
            [DisplayName("Tipo")]
            public string tipo_struttura { get; set; }

            [DisplayName("Indirizzo")]
            public string indirizzo { get; set; }

            [StringLength(15)]
            [DisplayName("Telefono")]
            public string telefono { get; set; }

            [DisplayName("EMail")]
            [StringLength(50)]
            public string email { get; set; }

            [DisplayName("Provincia")]
            [StringLength(2)]
            public string provincia { get; set; }

            [DisplayName("CAP")]
            [RegularExpression("[0-9]{5}", ErrorMessage = "Formato CAP non valido (Es: 80100)")]
            public string cap { get; set; }

            //Il dottore vuole usare questo campo per capire se la struttura è backoffice(1),frontoffice(2) o entrambe(3)
            [DisplayName("Modalità operativa")]
            [Required]
            public int prog_lav { get; set; }

            [Required]
            [DisplayName("Responsabile")]
            public int id_risorsa { get; set; }
        }
    }
}
