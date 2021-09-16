using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_entrate.Metadata))]
    public partial class anagrafica_entrate : ISoftDeleted
    {
        public static int RISCOSSIONE_COATTIVA = 12;
        public static int NESSUNA_ENTRATA = 1;
        public static int ACQUEDOTTO = 13;
        public static int ACQUEDOTTO1 = 9;
        public static int SPESE_NOTIFICA = 27;
        public static int SPESE_RISCOSSIONE_COATTIVA = 29;
        public static int ENTRATA_NON_ATTRIBUIBILE = 99;
        public static int CDS = 6;
        public static int CDS_ACCERTAMENTI = 20;
        public static int CDS_SANZIONI_SOSTA = 26;
        public static int CDS_ZTL = 36;
        public static int SANZIONE_AMMINISTRATIVA = 201;
        public static int ICI = 3;
        public static int IMU = 25;
        public static int TASI = 33;
        public static int TARSU = 4;
        public static int TARES_TARSU = TARSU;

        public static int TARI = 30;
        public static int TEFA = 17;
        public static int TEFAG = 22;
        public static int TARSU_PROV = 19;
        public static int COSAP = 7;
        public static int TOSAP = 8;
        public static int ICP = 5;
        public static int TARSUG_TARIG = 21;
        public static int CIMP = 202;
        public static int DPA = 10;
        public static int LMP_VOTIVE = 1214;
        public static int ONERI_ACC_ESEC = 1215;
        public static int BOLLO_AUTO = 120;

        public static int ENTRATA_GENERICA = 1;

        public static int ONERI_RISCOSSIONE = 1215;
        public static int AFFISSIONE_MANIFESTI = 1222;
        public static int AREE_MERCATO = 1224;

        public static int TARES = 210; // "TARES - Tassa ambientale rifiuti e servizi"

        public static string NaturaEntrateTributariaT { get { return "T"; } }
        public static string NaturaEntratePatrimonialeExtratributariaE { get { return "E"; } }
        public static string NaturaEntrateSanzioneS { get { return "S"; } }
        public static string NaturaEntrateSpeseP { get { return "P"; } }
        public static string NaturaEntrateAggiRiscCoaA { get { return "A"; } }
        public static string NaturaEntrateEntrateRiscCoaC { get { return "C"; } }
        public static string NaturaEntrateGenericaG { get { return "G"; } }

        public const int ID_CODICE_TIPO_ENTRATA_COMUNI_ID = 1;
        public const int ID_CODICE_TIPO_ENTRATA_PROVINCIA_ID = 3;
        public const int ID_CODICE_TIPO_ENTRATA_REGIONE_ID = 4;
        public const int ID_CODICE_TIPO_ENTRATA_COMUNI_CONSORZI_DI_COMUNI_ID = 7;
        public const int ID_CODICE_TIPO_ENTRATA_COMUNI_ENTE_GENERICO_ID = 9;

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public string CodiceDescrizione
        {
            get
            {
                return codice_entrata + "/" + descrizione_entrata;
            }
        }

        public bool model_flag_iva
        {
            get
            {
                return flag_iva == "1";
            }
            set
            {
                flag_iva = value ? "1" : "0";
            }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID")]
            public int id_entrata { get; set; }

            [Required]
            [StringLength(6)]
            [DisplayName("Codice")]
            public string codice_entrata { get; set; }

            [Required]
            [DisplayName("Descrizione")]
            public string descrizione_entrata { get; set; }

            [DisplayName("Tipo ente competente")]
            public Nullable<int> id_codice_tipo_entrata { get; set; }

            //[Required]
            [StringLength(3)]
            [DisplayName("Sigla")]
            public string sigla_entrata { get; set; }

            [Required]
            [DisplayName("IVA")]
            public bool model_flag_iva { get; set; }

            [DisplayName("Natura entrata")]
            public int flag_natura_entrata { get; set; }

            [DisplayName("Entrata Primaria/Secondaria")]
            public int flag_trasmissione { get; set; }
        }
    }
}
