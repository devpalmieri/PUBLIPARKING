using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_validazione_approvazione_liste.Metadata))]
    public partial class tab_validazione_approvazione_liste : ISoftDeleted,IGestioneStato
    {
        public const string ANN_ANN = "ANN-ANN";
        public const string ATT_ATT = "ATT-ATT";
        public const string PRE_ACQ = "PRE-ACQ";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        /// <summary>
        /// Gestisce l'aggiornamento dei campi utente dello stato
        /// </summary>
        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }

        public List<DateTime> rate_scadenze {
            get {
                return new DateTime?[] { this.data_scadenza_rata_1, this.data_scadenza_rata_2,
                    this.data_scadenza_rata_3, this.data_scadenza_rata_4,
                    this.data_scadenza_rata_5, this.data_scadenza_rata_6,
                    this.data_scadenza_rata_7, this.data_scadenza_rata_8,
                    this.data_scadenza_rata_9, this.data_scadenza_rata_10,
                    this.data_scadenza_rata_11, this.data_scadenza_rata_12 }
                .ToList()
                .Where(d => d.HasValue)
                .Cast<DateTime>()
                .ToList();
            }
        }

        //[Required]
        //[Range(1, Int32.MaxValue, ErrorMessage = "Nessuna Struttura Selezionata")]
        //[DisplayName("Struttura Approvazione")]
        //public int id_struttura_approvazione_required {
        //    get
        //    {
        //        if (id_struttura_approvazione.HasValue)
        //            return id_struttura_approvazione.Value;
        //        else
        //            return 0;
        //    }
        //    set
        //    {
        //        id_struttura_approvazione = id_struttura_approvazione_required;
        //    }

        //}


        //[Required]
        //[Range(1, Int32.MaxValue, ErrorMessage = "Nessuna Risorsa Selezionata")]
        //[DisplayName("Risorsa Approvazione")]
        //public int id_risorsa_approvazione_required
        //{
        //    get
        //    {
        //        if (id_risorsa_approvazione.HasValue)
        //            return id_risorsa_approvazione.Value;
        //        else
        //            return 0;
        //    }
        //    set
        //    {
        //        id_risorsa_approvazione = id_risorsa_approvazione_required;
        //    }

        //}

        [DisplayName("Doppia Notifica Amministratori P.G.")]
        public bool flag_notifica_amministratori_PG_NOT_NULL
        {
            get
            {
                return this.flag_notifica_amministratori_PG.HasValue ? this.flag_notifica_amministratori_PG.Value : false;
            }

            set
            {
                this.flag_notifica_amministratori_PG = value;
            }
        }

        [DisplayName("Rinotifica Amministratori P.G.")]
        public bool flag_rinotifica_amministratori_PG_NOT_NULL
        {
            get
            {
                return this.flag_rinotifica_amministratori_PG.HasValue ? this.flag_rinotifica_amministratori_PG.Value : false;
            }

            set
            {
                this.flag_rinotifica_amministratori_PG = value;
            }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID Validazione")]
            public int id_validazione_approvazione_liste { get; set; }
            [DisplayName("ID Lista")]
            public int id_lista { get; set; }

            //[Required]
            [Range(1, Int32.MaxValue, ErrorMessage = "Nessuna Struttura Selezionata")]
            [DisplayName("Struttura Approvazione")]
            public int? id_struttura_approvazione { get; set; }
           
            //[Required]
            [Range(1, Int32.MaxValue, ErrorMessage = "Nessuna Risorsa Selezionata")]
            [DisplayName("Risorsa Approvazione")]
            public int? id_risorsa_approvazione { get; set; }

            //[Required(ErrorMessage = "Inserire una determina")]
            [DisplayName("Determina")]
            public string numero_determina { get; set; }

           // [Required(ErrorMessage = "Inserire una data di approvazione")]                        
            [DisplayName("Data riferimenti di approvazione")]
            public DateTime data_approvazione_determina { get; set; }

            [DisplayName("Data scadenza rata unica")]
            public DateTime data_scadenza_rata_unica { get; set; }
            [DisplayName("Data scadenza prima rata ")]
            public DateTime data_scadenza_rata_1 { get; set; }
            [DisplayName("Data scadenza seconda rata ")]
            public DateTime data_scadenza_rata_2 { get; set; }
            [DisplayName("Data scadenza terza rata ")]
            public DateTime data_scadenza_rata_3 { get; set; }
            [DisplayName("Data scadenza quarta rata ")]
            public DateTime data_scadenza_rata_4 { get; set; }
            [DisplayName("Data scadenza quinta rata ")]
            public DateTime data_scadenza_rata_5 { get; set; }
            [DisplayName("Data scadenza sesta rata ")]
            public DateTime data_scadenza_rata_6 { get; set; }
            [DisplayName("Data scadenza settima rata ")]
            public DateTime data_scadenza_rata_7 { get; set; }
            [DisplayName("Data scadenza ottava rata ")]
            public DateTime data_scadenza_rata_8 { get; set; }
            [DisplayName("Data scadenza nona rata ")]
            public DateTime data_scadenza_rata_9 { get; set; }
            [DisplayName("Data scadenza decima rata ")]
            public DateTime data_scadenza_rata_10 { get; set; }
            [DisplayName("Data scadenza undicesima rata ")]
            public DateTime data_scadenza_rata_11 { get; set; }
            [DisplayName("Data scadenza dodicesima rata ")]
            public DateTime data_scadenza_rata_12 { get; set; }

            [DisplayName("Modalità sped/notifica P.F. fuori Comune")]
            [Required]
            public Nullable<int> modalita_sped_not_fuori_comune_PF { get; set; }
            [DisplayName("Modalità sped/notifica P.G. fuori Comune")]
            [Required]
            public Nullable<int> modalita_sped_not_fuori_comune_PG { get; set; }
            [DisplayName("Modalità sped/notifica P.F. nel Comune")]
            [Required]
            public Nullable<int> modalita_sped_not_nel_comune_PF { get; set; }
            [DisplayName("Modalità sped/notifica P.G. nel Comune")]
            [Required]
            public Nullable<int> modalita_sped_not_nel_comune_PG { get; set; }
            [DisplayName("Modalità sped/notifica per Estero")]
            [Required]
            public Nullable<int> modalita_sped_not_estero { get; set; }
        }
    }
}
