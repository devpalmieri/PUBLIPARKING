using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_tipo_servizi.Metadata))]
    public partial class anagrafica_tipo_servizi
    {
        public const int GEST_ORDINARIA = 0;
        public const int ACCERTAMENTO = 1;
        public const int RISC_PRECOA = 2;
        public const int ING_FISC = 3;
        public const int ACCERT_OMESSO_PAGAM = 4;
        public const int ACCERT_OMESSO_PAGAM_ESECUTIVO = 31;
        public const int ACCERT_ESECUTIVO = 30;
        public const int SOLL_PRECOA = 5;
        public const int INTIM = 6;
        public const int AVVISI_CAUTELARI = 7;
        public const int AVVISI_PIGNORAMENTO_ORDINE_TERZO = 8;
        public const int AVVISI_PIGNORAMENTO_CITAZIONE_TERZO = 9;
        public const int AVVISI_PIGNORAMENTO_IMMOBILIARI = 10;
        public const int AVVISI_PIGNORAMENTO_MOBILIARI = 11;
        public const int SERVIZI_GENERICI = 15;
        public const int AVVISI_ORDINARI_NON_SOGGETTO_AD_ACCERTAMENTO = 20;
        public const int SERVIZI_RATEIZZAZIONE_NON_COATTIVI = 21; //Non usato
        public const int SERVIZI_PROCEDURE_CONCORSUALI = 22;
        public const int SERVIZI_TECNICI_DI_GESTIONE_IDRICA = 23;
        public const int SERVIZI_RATEIZZAZIONE_COA = 26; //Qualsiasi rateizzazione (coattiva e non)
        public const int SERVIZI_DEFINIZIONE_AGEVOLATA_COA_OLD = 27;
        public const int SERVIZI_DEFINIZIONE_AGEVOLATA_COA = 28;
        public const int SERVIZI_DICHIARAZIONE_STRAGIUDIZIALE = 29;
        public const int SERVIZI_RIMBORSO = 1028;
        public const int SERVIZI_COMUNICAZIONE_AI_CONTRIBUENTI = 1029;

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID")]
            public int id_tipo_servizio { get; set; }

            [Required]
            [DisplayName("Descrizione Tipo Servizio")]
            [StringLength(100)]
            public string descr_tiposervizio { get; set; }
        }
    }
}
