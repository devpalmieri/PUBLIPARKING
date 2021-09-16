using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_tipo_spedizione_notifica.Metadata))]
    public partial class anagrafica_tipo_spedizione_notifica
    {
        // Messo Notificatore Speciale
        //[Obsolete("Se serve una sigla effettuare query - le sigle cambiano!", false)]
        public const string SIGLA_NME_MESSO_NOTIFICATORE_SPECIALE = "NME";
        // Messo Notificatore Speciale
        public const int SIGLA_NME_MESSO_NOTIFICATORE_SPECIALE_ID = 1;

        // Raccomandata AR
        [Obsolete("Se serve una sigla effettuare query - le sigle cambiano!", false)]
        public const string SIGLA_NAR_RACCOMANDATA_AR = "NAR";
        // Raccomandata AR
        public const int SIGLA_NAR_RACCOMANDATA_AR_ID = 2;

        [Obsolete("Usare SIGLA_NUR_UFFICIALE_RISCOSSIONE_ID")]
        public const int SIGLA_NUR_ID = 4;
        [Obsolete("Usare SIGLA_NUR_UFFICIALE_RISCOSSIONE", false)]
        public const string SIGLA_NUR = "NUR";

        // Ufficiale della Riscossione
        [Obsolete("Se serve una sigla effettuare query - le sigle cambiano!", false)]
        public const string SIGLA_NUR_UFFICIALE_RISCOSSIONE = "NUR";
        // Ufficiale della Riscossione
        public const int SIGLA_NUR_UFFICIALE_RISCOSSIONE_ID = 4;

        // Posta Ordinaria
        [Obsolete("Se serve una sigla effettuare query - le sigle cambiano!", false)]
        public const string SIGLA_SPP_POSTA_ORDINARIA = "SPP";
        // Posta Ordinaria
        public const int SIGLA_SPP_POSTA_ORDINARIA_ID = 5;

        [Obsolete("Usare SIGLA_NNS_NOTIFICA_ALLO_SPORTELLO", false)]
        public static string SIGLA_NNS = "NNS";
        [Obsolete("Usare SIGLA_NNS_NOTIFICA_ALLO_SPORTELLO_ID")]
        public static int SIGLA_NNS_ID = 9;

        [Obsolete("Se serve una sigla effettuare query - le sigle cambiano!", false)]
        public const string SIGLA_NNS_NOTIFICA_ALLO_SPORTELLO = "NNS";
        public const int SIGLA_NNS_NOTIFICA_ALLO_SPORTELLO_ID = 9;


        // Ufficiale Giudiziario
        [Obsolete("Se serve una sigla effettuare query - le sigle cambiano!", false)]
        public const string SIGLA_NUG_UFFICIALE_GIUDIZIARIO = "NUG";
        // Ufficiale Giudiziario
        public const int SIGLA_NUG_UFFICIALE_GIUDIZIARIO_ID = 10;

        [Obsolete("Usare SIGLA_NPC_SPEDIZIONE_PEC", false)]
        public static string SIGLA_SPEDIZIONE_PEC = "NPC";
        [Obsolete("Usare SIGLA_NPC_SPEDIZIONE_PEC", false)]
        public static string SIGLA_NPC = "NPC";
        //Sandro non è obsoleto è utilizzato 10/07/2018
        //[Obsolete("Usare SIGLA_NPC_SPEDIZIONE_PEC_ID")]
        public const int ID_SPEDIZIONE_PEC = 11;

        [Obsolete("Se serve una sigla effettuare query - le sigle cambiano!", false)]
        public const string SIGLA_NPC_SPEDIZIONE_PEC = "NPC";
        public const int SIGLA_NPC_SPEDIZIONE_PEC_ID = 11;

        // Raccomandata AG
        [Obsolete("Se serve una sigla effettuare query - le sigle cambiano!", false)]
        public const string SIGLA_NAG_RACCOMANDATA_AG = "NAG";
        // Raccomandata AG
        public const int SIGLA_NAG_RACCOMANDATA_AG_ID = 12;

        // Raccomandata Semplice
        [Obsolete("Se serve una sigla effettuare query - le sigle cambiano!", false)]
        public const string SIGLA_NAS_RACCOMANDATA_SEMPLICE = "NAS";
        // Raccomandata Semplice
        public const int SIGLA_NAS_RACCOMANDATA_SEMPLICE_ID = 13;

        // Posta Ordinaria Estero
        //[Obsolete("Usare SIGLA_SPE_POSTA_ORDINARIA_ESTERO")] Non più
        [Obsolete("Se serve una sigla effettuare query - le sigle cambiano!", false)]
        public const string SIGLA_SPE = "SPE";
        // Posta Ordinaria Estero
        [Obsolete("Usare SIGLA_SPE_POSTA_ORDINARIA_ESTERO_ID")]
        public const int SIGLA_SPE_ID = 14;

        // Posta Ordinaria Estero
        [Obsolete("Se serve una sigla effettuare query - le sigle cambiano!", false)]
        public const string SIGLA_SPE_POSTA_ORDINARIA_ESTERO = "SPE";
        // Posta Ordinaria Estero
        public const int SIGLA_SPE_POSTA_ORDINARIA_ESTERO_ID = 14;

        // Raccomandata AR Estero
        [Obsolete("Usare SIGLA_NRE_RACCOMANDATA_AR_ESTERO", false)]
        public const string SIGLA_NRE = "NRE";
        // Raccomandata AR Estero
        [Obsolete("Usare SIGLA_NRE_RACCOMANDATA_AR_ESTERO_ID", false)]
        public const int SIGLA_NRE_ID = 15;

        // Raccomandata AR Estero
        [Obsolete("Se serve una sigla effettuare query - le sigle cambiano!", false)]
        public const string SIGLA_NRE_RACCOMANDATA_AR_ESTERO = "NRE";
        // Raccomandata AR Estero
        public const int SIGLA_NRE_RACCOMANDATA_AR_ESTERO_ID = 15;

        public const int SIGLA_XAG_NEXIVE_ID = 22;
        public const string CONSEGNA_SPORTELLO = "SCS";
        // -----------------------------------------------------
        [Obsolete("Se serve una sigla effettuare query - le sigle cambiano!", false)]
        public static string SIGLA_SCS = "SCS";
        [Obsolete("Se serve una sigla effettuare query - le sigle cambiano!", false)]
        public static string SIGLA_SEM = "SEM";
        [Obsolete("Se serve una sigla effettuare query - le sigle cambiano!", false)]
        public static string SIGLA_SMS = "SMS";

        public const int ID_POSTA_ORDINARIA = 5;
        public const int ID_POSTA_ORDINARIA_NEXIVE = 17;
        public const int ID_POSTA_ORDINARIA_NEXIVE_ESTERO = 20;
        public const int ID_POSTA_RACCOMANDATA = 2; // "Raccomandata AR" delle poste
        public const int ID_POSTA_RACCOMANDATA_AG = 12;//"Raccomandata ATTO GIUDIZIARIO

        public const int ID_SPEDIZIONE_SMS = 16;
        public const int ID_SPEDIZIONE_EMAIL = 7;
        public const int ID_CONSEGNA_SPORTELLO = 8;

        public const int ID_NEXIVE_RACCOMANDATA_AR = 18; // "Raccomandata AR" nexive
        public const int ID_NEXIVE_RACCOMANDATA_SEMPLICE = 19;
        public const int ID_NEXIVE_RACCOMANDATA_AR_ESTERO = 21;

        public const string POSTA_ORDINARIA = "S";
        public const string POSTA_RACCOMANDATA = "N";
        public const string POSTA_RACCOMANDATA_AG = "G";
        // -----------------------------------------------------




        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID")]
            public int id_tipo_spedizione_notifica { get; set; }

            [Required]
            [DisplayName("Descrizione Tipo Notifica")]
            public string descr_tipo_spedizione_notifica { get; set; }

        }

    }
}
