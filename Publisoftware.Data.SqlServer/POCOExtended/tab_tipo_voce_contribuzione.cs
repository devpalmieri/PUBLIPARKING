using Publisoftware.Data.CustomValidationAttrs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_tipo_voce_contribuzione.Metadata))]
    public partial class tab_tipo_voce_contribuzione : ISoftDeleted
    {
        public const string CODICE_RSP = "RSP";
        public const string CODICE_ONV = "ONV";
        public const string CODICE_NOT = "NOT";
        public const string CODICE_SPE = "SPE";
        public const string CODICE_COA = "COA";
        public const string CODICE_AMC = "AMC";
        public const string CODICE_AGG = "AGG";
        public const string CODICE_SANZIONI = "SAN";
        public const string CODICE_INTERESSI = "INT";
        public const string CODICE_ENT = "ENT";
        public const string CODICE_AVV_COLLEGATO = "ACO";
        public const string CODICE_ILG = "ILG";
        public const string CODICE_IRA = "IRA";
        public const string CODICE_ONERI = "AGG";

        public const int ID_AVVISO_COLLEGATO = 19;
        public const int ID_ARROTONDAMENTO = 25;
        public const int ID_INTERESSE_RATEIZZAZIONE_GENERICO = 1308;

        public const string CREDITI_PRIVILEGIATI = "P";
        public const string CREDITI_CHIROGRAFARI = "C";

        public const int ONERI_COATTIVI_ID = 1303;
        public const int SPESE_ISCRIZIONE_FERMO_AMMINISTRATIVO_ID = 1307;
        public const int SPESE_ISCRIZIONE_IPOTECARIA_ID = 2122;
        public const int SPESE_ESECUTIVE = 61;
        public const int ID_SPESE_NOTIFICA = 3;
        public const int ID_SPESE_SPEDIZIONE = 8;
        public const int ID_SPESE_ARROTONDAMENTO = 25;
        public const int ID_ENTRATA_ICI = 2;
        public const int ID_ENTRATA_IMU = 79;
        public const int BOLLETTINI_SMARRITI = 27;
        public const int EUROGIRI_NON_IMPUTATI = 28;
        public const int PAGAMENTI_NON_IMPUTATI = 29;
        public const int STORNO = 30;
        public const int PAGANTI_NON_IDENTIFICATI = 31;
        public const int SPESE_STIPULA_CONTRATTUALE = 10;
        public const int DEPOSITO_CAUZIONALE = 11;

        public const int ID_TIPO_TARI = 82;
        public const int ID_TIPO_TEFA = 33;
        public const int ID_TIPO_TEFAG = 55;

        public const int SANZIONI_TARSU = 21;
        public const int SANZIONI_PROV = 65;
        public const int SANZIONI_TARI = 96;

        public const int INTERESSI_TARI = 83;
        public const int INTERESSI_TEFA = 35;

        public const int ID_TIPO_LMP_VOTIVE = 3200;
        public const int INTERESSI_LMP_VOTIVE = 3201;
        public const int SANZIONI_LMP_VOTIVE = 3202;
        public const int ONERI_RISCOSSIONE = 3226;

        public const int ID_TIPO_ICP = 53;
        public const int INTERESSI_ICP = 67;
        public const int SANZIONI_ICP = 68;

        public const int ID_TIPO_IMU = 79;
        public const int ID_TIPO_TASI = 88;

        public const int ID_TIPO_TOSAP = 59;
        public const int INTERESSI_TOSAP = 69;
        public const int SANZIONI_TOSAP = 70;

        public const int ID_TIPO_COSAP = 51;
        public const int INTERESSI_COSAP = 71;
        public const int SANZIONI_COSAP = 72;

        public const int ID_TIPO_TARIG = 54;
        public const int INTERESSI_TARIG = 94;
        public const int SANZIONI_TARIG = 95;

        public const int AFFISSIONE_MANIFESTI = 3310;
        public const int AREE_MERCATO = 3311;
        public const int SANZIONI_CDS = 40;


        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public string CodiceDescrizione
        {
            get { return (codice_tipo_voce != string.Empty && codice_tipo_voce != null) ? (codice_tipo_voce + " - " + descrizione_tipo_voce_contribuzione) : descrizione_tipo_voce_contribuzione; }
        }

        public bool flag_iva_AS_BOOLEAN
        {
            get
            {
                return flag_iva == "1" ? true : false;
            }
            set
            {
                flag_iva = value ? "1" : "0";
                ;
            }
        }
        public bool flag_accertamento_contabile_AS_BOOLEAN
        {
            get
            {
                return flag_accertamento_contabile == "1" ? true : false;
            }
            set
            {
                flag_accertamento_contabile = value ? "1" : "0";
                ;
            }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Id")]
            public int id_tipo_voce_contribuzione { get; set; }

            [DisplayName("Entrata")]
            public int id_entrata { get; set; }

#if NESSUN_CONTROLLO_tab_tipo_voce_contribuzione
#else
            [RegularExpression("[a-zA-Z0-9]{1,6}", ErrorMessage = "Formato non valido")]
#endif
            [DisplayName("Codice")]
            public string codice_tipo_voce { get; set; }

            [Required]
#if NESSUN_CONTROLLO_tab_tipo_voce_contribuzione
#else
            [RegularExpression(@"^[\w\s]{1,100}$", ErrorMessage = "Formato non valido")]
#endif
            [DisplayName("Descrizione")]
            public string descrizione_tipo_voce_contribuzione { get; set; }

            [DisplayName("Priorità Pagamento")]
            public int priorita_pagamento { get; set; }

#if NESSUN_CONTROLLO_tab_tipo_voce_contribuzione
#else
            [RegularExpression("[a-zA-Z0-9]{1,10}", ErrorMessage = "Formato non valido")]
#endif
            [DisplayName("Codice Tributo Ministeriale")]
            public string codice_tributo_ministeriale { get; set; }

#if NESSUN_CONTROLLO_tab_tipo_voce_contribuzione
#else
            [RegularExpression(@"^[\w\s]{1,200}$", ErrorMessage = "Formato non valido")]
#endif
            [DisplayName("Descrizione Gruppo")]
            public string descrizione_gruppo { get; set; }

            [Required]
#if NESSUN_CONTROLLO_tab_tipo_voce_contribuzione
#else
            [RegularExpression("[0-1]{1,1}", ErrorMessage = "Formato non valido")]
#endif
            [DisplayName("Flag Registro")]
            public string flag_registro { get; set; }

            [Description("Conto di bilancio")]
            public Nullable<int> id_conto_bilancio { get; set; }

            [Description("Aliquota iva")]
            public Nullable<decimal> aliquota_iva { get; set; }

        }
    }
}
