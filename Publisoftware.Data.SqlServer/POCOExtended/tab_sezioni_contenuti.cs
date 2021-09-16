using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_sezioni_contenuti.Metadata))]
    public partial class tab_sezioni_contenuti : ISoftDeleted
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }
        public const string TAG_AVVISI = "Avvisi";
        public const string TAG_SOLLECITI = "Solleciti";
        public const string TAG_FERMI = "Fermi";
        public const string TAG_ACCERTAMENTO = "Accertamento";
        public const string TAG_ANNULLA_RETTIFICA = "annulla_rett";
        public const string TAG_RATEIZZAZIONE = "Rateizzazione";

        public const string TAG_SOSPENSIONE = "Sospensione";
        public const string TAG_PIGNORAMENTI = "Pignoramenti";
        public const string TAG_RIMBORSO = "Rimborso";
        public const string TAG_IMPUGNAZIONE = "Impugnazione";

        public const string TAG_RISCOSSIONE = "Riscossione";
        public const string TAG_IMPUGNABILITA = "Impugnabilita";

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID Sezione")]
            public int Id_Sezione { get; set; }

            [DisplayName("ID Ente")]
            public int Id_Ente { get; set; }
            [DisplayName("ID Struttura")]
            public int id_struttura { get; set; }

            [DisplayName("Modalità Operativa")]
            public string ModOp { get; set; }
            [DisplayName("Tag")]
            public string Tag { get; set; }
            [DisplayName("Voce Menù")]
            public string MainMenu { get; set; }
            [DisplayName("Titolo")]
            public string Title { get; set; }
            [DisplayName("Sottotitolo")]
            public string SubTitle { get; set; }
            [DisplayName("Contenuti")]
            public string Description { get; set; }

           
        }
    }
}
