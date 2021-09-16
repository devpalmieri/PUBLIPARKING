using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_tipo_bene.Metadata))]
    public partial class tab_tipo_bene
    {
        //public const int TIPO_STIPENDIO = 1;
        //public const int TIPO_PENSIONE = 2;

        //public const int TIPO_IMMOBILI = 4;
        //public const int TIPO_CONTO_CORRENTE = 5;
        //public const int TIPO_VEICOLO = 6;
        //public const int TIPO_MOBILI = 7;
        //public const int TIPO_COMPENSO = 8;

        public const string SIGLA_VEICOLI = "VEI";
        public const string SIGLA_BANCHE = "DIS";
        public const string SIGLA_LOCAZIONE = "LOC";
        public const string SIGLA_STIPENDI = "STI";
        public const string SIGLA_PENSIONI = "PEN";
        public const string SIGLA_IMMOBILI = "IMM";
        public const string SIGLA_MOBILI = "MOB";
        public const string SIGLA_COMPENSO = "CMP";

        public const int ID_TIPO_BENE_LOCAZIONE_DA_SIATEL = 3;
        public const int ID_TIPO_BENE_DISPONIBILITA_CO_BANCHE = 5;
        public const int ID_TIPO_BENE_PRESUNTA_DISPONIBILITA_BANCHE = 9;

        public bool isVeicolo { 
            get {
                return this.codice_bene.CompareTo(tab_tipo_bene.SIGLA_VEICOLI) == 0;
            } 
        }

        public bool isDaTerzoDebitore { 
            get {
                return
                    this.codice_bene.CompareTo(tab_tipo_bene.SIGLA_LOCAZIONE) == 0
                    ||
                    this.codice_bene.CompareTo(tab_tipo_bene.SIGLA_STIPENDI) == 0
                    ||
                    this.codice_bene.CompareTo(tab_tipo_bene.SIGLA_PENSIONI) == 0
                    ||
                    this.codice_bene.CompareTo(tab_tipo_bene.SIGLA_BANCHE) == 0;
            } 
        }


        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Required]
            [DisplayName("Tipo Bene")]
            public string id_tipo_bene { get; set; }

        }
    }
}
