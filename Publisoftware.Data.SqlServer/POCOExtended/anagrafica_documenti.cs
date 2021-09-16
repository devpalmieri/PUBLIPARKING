using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_documenti.Metadata))]
    public partial class anagrafica_documenti : ISoftDeleted
    {
        static public string MACROCATEGORIA_ANAGRAFE = "ANA";
        static public string MACROCATEGORIA_DOMICILIO = "DOM";
        static public string MACROCATEGORIA_QUALITA_REFERENTE = "QUA";
        static public string MACROCATEGORIA_STATO_FISICO = "STF";
        static public string MACROCATEGORIA_STATO_GIURIDICO = "STG";
        static public string MACROCATEGORIA_PAGAMENTO = "PAG";
        static public string MACROCATEGORIA_OGGETTI_CONTRIBUZIONE = "OGG";
        static public string MACROCATEGORIA_REDDITI = "RED";
        static public string MACROCATEGORIA_NOTIFICHE = "NOT";
        static public string MACROCATEGORIA_SIATEL = "TER";
        static public string MACROCATEGORIA_DEBITORE = "DEB";
        static public string MACROCATEGORIA_TERZI = "DTZ";
        static public string MACROCATEGORIA_RIL_PROC_CONCORSUALI = "RPC";
        static public string MACROCATEGORIA_PROC_CONCORSUALI = "PCN";
        static public string MACROCATEGORIA_CITAZIONI = "CIT";
        static public string MACROCATEGORIA_RICORSI = "RIC";
        static public string MACROCATEGORIA_ISTANZA = "IST";
        static public string MACROCATEGORIA_VEICOLI = "VEI";
        static public string MACROCATEGORIA_PROVVEDIMENTI = "PRO";
        static public string MACROCATEGORIA_GENERICO = "GEN";
        static public string MACROCATEGORIA_SANZIONI = "SAN";
        static public string MACROCATEGORIA_LAVORAZIONE = "LAV";
        static public string MACROCATEGORIA_GENERICA_PROVVEDIMENTI = "PRV";
        static public string MACROCATEGORIA_IPOTECHE = "IPO";
        static public string MACROCATEGORIA_INSINUAZIONI = "INS";
        static public string MACROCATEGORIA_FIDEIUSSIONE = "FID";
        static public string MACROCATEGORIA_DENUNCE = "DEN";
        static public string MACROCATEGORIA_ACQUEDOTTO = "ACQ";
        static public string MACROCATEGORIA_AUTORIZZAZIONI = "AUT";

        static public string SIGLA_AVVISO_RIF = "AVRIF";
        static public string SIGLA_AVVISO_COLL = "AVCOL";
        static public string SIGLA_AVVISO_ORDINE = "PGORD";
        static public string SIGLA_AVVISO_IPOTECA = "AVIPO";
        static public string SIGLA_NOTIFICA_RELATA = "NOTREL";
        static public string SIGLA_DOC_OUTPUT = "DOCOUT";

        static public string SIGLA_COPIA_RICORSO = "RICPRE";
        static public string SIGLA_DOMANDA_CONCILIAZIONE = "DOCON";
        static public string SIGLA_COPIA_PROCURA = "PROCU";
        static public string SIGLA_ATTO_PIGNORAMENTO = "PGCIT";
        static public string SIGLA_COMUNICAZIONE_PROC_CONCORSUALE = "PCN01";
        static public string SIGLA_DOMANDA_AMM_PASSIVO = "DOAMP";
        static public string SIGLA_ESTRATTO_CONTRATTO_AFFIDAMENTO = "CONTR";
        static public string SIGLA_VISURA_CAMERALE = "VISCAM";
        static public string SIGLA_ALBO = "ALBO";
        static public string SIGLA_ESTRATTO_REGOLAMENTO = "REGOL";
        static public string SIGLA_COPIE_DOCUMENTAZIONI = "CODO";
        static public string SIGLA_COPIA_SENTENZA = "SENT";
        static public string SIGLA_COPIA_MEMORIA_DIFENSIVA = "MEMDIF";
        static public string SIGLA_SCHEMA_PRELIMINARE_CONTRODEDUZIONI = "CONTRDED";
        static public string SIGLA_DICHIARAZIONE_TERZO = "DTZ";
        static public string SIGLA_PRESENTAZIONE_DEBITO = "PREDEB";
        static public string SIGLA_ASSEVERAZIONE_INGIUNZIONI_TRASMESSE = "ASSINGTRA";
        static public string SIGLA_ASSEVERAZIONE_INGIUNZIONI_EMESSE = "ASSINGEME";
        static public string SIGLA_ASSEVERAZIONE_INTIMAZIONI_TRASMESSE = "ASSINTTRA";
        static public string SIGLA_ASSEVERAZIONE_INTIMAZIONI_EMESSE = "ASSINTEME";
        static public string SIGLA_ASSEVERAZIONE_PIGNORAMENTI_ORDINE_TERZO = "ASSPOTEME";
        static public string SIGLA_ASSEVERAZIONE_PIGNORAMENTI_CITAZIONE_TERZO = "ASSPCTEME";
        static public string SIGLA_ASSEVERAZIONE_RICORSI_TRASMESSI = "ASSATTITRA";
        static public string SIGLA_ASSEVERAZIONE_RICORSI_EMESSI = "ASSATTIEME";
        static public string SIGLA_ASSEVERAZIONE_IPOTECHE_TRASMESSE = "ASSATTITRA";
        static public string SIGLA_ASSEVERAZIONE_IPOTECHE_EMESSE = "ASSATTIEME";
        static public string SIGLA_ASSEVERAZIONE_INSINUAZIONI_TRASMESSE = "ASSATTITRA";
        static public string SIGLA_ASSEVERAZIONE_INSINUAZIONI_EMESSE = "ASSATTIEME";
        static public string SIGLA_ESTINZIONE_PROCEDURA_ESECUTIVA = "EST";
        static public string SIGLA_ORDINANZA_ASSEGNAZIONE = "OAS";
        static public string SIGLA_ORDINANZA_ESTINZIONE = "OES";
        static public string SIGLA_ORDINANZA_MANCATA_DICHIARAZIONE = "OMD";
        static public string SIGLA_ORDINANZA_RINVIO_UDIENZA = "ORU";
        static public string SIGLA_ORDINANZA_RINOTIFICA_CITAZIONE = "ONC";
        static public string SIGLA_ORDINANZA_FISSAZIONE_UDIENZA = "OFU";
        static public string SIGLA_DENUNCE_TARI = "TARI";
        static public string SIGLA_GESTIONE_CANONE_PATRIMONIALE_OCCUPAZIONE_SUOLO_PUBBLICO = "GCPOSP";
        static public string SIGLA_DENUNCE_IMU = "IMU";

        static public string SIGLA_AUTORIZZAZIONE_DOCUMENTO_IDENTITA = "AUT01";
        static public string SIGLA_AUTORIZZAZIONE_CERTIFICATO_MORTE = "AUT03";
        static public string SIGLA_AUTORIZZAZIONE_CERTIFICATO_REFERENTE = "AUT04";

        static public string CODICE_ASSEVERAZIONE = "ASSEVER";

        static public string APPLICAZIONE_ISTANZA = "IST";
        static public string APPLICAZIONE_PROCEDURE_CONCORSUALI = "PROCON";
        static public string APPLICAZIONE_FASCICOLI_AVVNOT = "FASC_AVVNOT";
        static public string APPLICAZIONE_FASCICOLI_DOCOUTPUT = "FASC_DOCOUTPUT";
        static public string APPLICAZIONE_FASCICOLI = "FASC";
        static public string APPLICAZIONE_TERZO_DBITORE = "TRZDEB";
        static public string APPLICAZIONE_DENUNCE = "DEN";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }


            [DisplayName("Id")]
            public int id_anagrafica_doc { get; set; }

            [RegularExpression("[a-zA-Z0-9]{1,10}", ErrorMessage = "Formato non valido")]
            [DisplayName("Codice")]
            public string sigla_doc { get; set; }

            [RegularExpression("[a-zA-Z0-9]{1,10}", ErrorMessage = "Formato non valido")]
            [DisplayName("Descrizione")]
            public string codice_doc { get; set; }

            [Required]
            [RegularExpression(@"^[\w\s]{1,200}$", ErrorMessage = "Formato non valido")]
            [DisplayName("Descrizione")]
            public string descrizione_doc { get; set; }

            [RegularExpression("[a-zA-Z0-9]{1,3}", ErrorMessage = "Formato non valido")]
            [DisplayName("Macrocategoria")]
            public string macrocategoria { get; set; }

            [Required]
            [DisplayName("Tipo Documento")]
            public int id_tipo_documento { get; set; }
        }
    }
}
