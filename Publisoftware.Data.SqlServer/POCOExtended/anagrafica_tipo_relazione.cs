using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_tipo_relazione.Metadata))]
    public partial class anagrafica_tipo_relazione : IValidator, ISoftDeleted, IGestioneStato
    {

        public const int AMMINISTRATORE_PT_ID = 3;
        public const int AMMINISTRATORE_UNICO_ID = 6;
        public const int EREDE_ID = 11;

        public const int AMMINISTRATORE_P_T = 34;

        [Obsolete("Usare RAPPRESENTANTE_LEGALE_NON_COOBBLIGATO", false)]
        public const int RAPPRESENTANTE_LEGALE_COOBBLIGATO = 55;
        public const int RAPPRESENTANTE_LEGALE_NON_COOBBLIGATO = 55;
        
        public const int TITOLARE_ID = 58;
        public const int TUTORE_ID = 61;
        public const int LIQUIDATORE_ID = 62;
        public const int RAPPRESENTANTE_LEGALE_INTESTATARIO = 66;
        public const int CURFALL_ID = 67;
        public const int COMMISSARIO_GIUDIZIARIO_ID = 68;
        public const int PROPRIETARIO_VEICOLO_ID = 69;
        public const int COMMISSARIO_LIQUIDATORE_ID = 84;

        public const int COOBBLIGATO_IN_SOLIDO_DI_PERSONA_GIURIDICA_ID = 56; // ex. 101;
        public const int COINTESTATARIO = 103;
        public const int SOCIO_ACCOMANDATARIO = 106;
        public const int COOBBLIGATO_IN_SOLIDO = 107;
        public const int COOBBLIGATO_IN_SOLIDO_GIUR_ID = 56;
        public const int SOCIO = 108;

        public const int REFERENTE_TERZO = 112;

        public const string ATT_ATT = "ATT-ATT";

        public anagrafica_tipo_relazione(int? p_flagAllineamento, string p_codTipoRelazione, string p_descTipoRelazione, string p_flagFisicaGiuridica)
        {
            flag_allineamento = p_flagAllineamento;
            cod_tipo_relazione = p_codTipoRelazione;
            desc_tipo_relazione = p_descTipoRelazione;
            flag_fisica_giuridica = p_flagFisicaGiuridica;
            
            if (!IsValid)
            {
                throw new ArgumentException("Error creating object", "anagrafica_tipo_relazione not valid");
            }
        }

        public static Dictionary<string, string> flagList = new Dictionary<string, string>()
        {
            { "0", "Tutti"},
            { "1", "Fisica"},
            { "2", "Giuridica"},
            { "4", "Ditta Individuale"}
        };

        public string flag_fisica_giuridica_desc
        {
            get { return flagList[flag_fisica_giuridica]; }
        }

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public bool IsValid
        {
            get { return checkValidity(); }
        }

        /// <summary>
        /// Verifica la validità dell'applicazione
        /// </summary>
        /// <returns></returns>
        protected bool checkValidity()
        {
            bool _isValid = false;

            if (string.IsNullOrEmpty(this.cod_tipo_relazione) || string.IsNullOrEmpty(this.desc_tipo_relazione) 
                || string.IsNullOrEmpty(this.flag_fisica_giuridica))
            {
                return _isValid;
            }

            else
            {
                _isValid = this.cod_tipo_relazione.Length > 0 && 
                           this.desc_tipo_relazione.Length > 0 &&
                           flagList.ContainsKey(this.flag_fisica_giuridica);
            }        
            return _isValid;
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

        internal sealed class Metadata
        {
            private Metadata()
            {
            }
            
            [DisplayName("Id")]
            public int id_tipo_relazione { get; set; }

            [DisplayName("Flag Allineamento")]
            public int flag_allineamento { get; set; }

            [Required]            
            [DisplayName("Codice")]
            public string cod_tipo_relazione { get; set; }

            [Required]
            [DisplayName("Descrizione")]
            public string desc_tipo_relazione { get; set; }

            [Required]
            [DisplayName("Tipologia")]
            public int flag_fisica_giuridica { get; set; }

            [Required]
            [DisplayName("Codice")]
            public string cod_stato { get; set; }
        }
    }
}
