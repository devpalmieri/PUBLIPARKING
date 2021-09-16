using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_risorse.Metadata))]
    public partial class anagrafica_risorse : ISoftDeleted, IGestioneStato
    {
        public const int RISORSA_ADMIN = 2037;
        public const int RISORSA_GENERICA = 3449;
        public const int RISORSA_MARANO = 3289;
        public const string RISORSA_INTERNA = "0";
        public const string RISORSA_ESTERNA = "1";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public bool IsGenerica { get; set; }

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }

        public bool isResponsabile(int id_struttura)
        {
            return this.join_risorse_strutture.Any(jrs => jrs.id_struttura_aziendale == id_struttura && jrs.anagrafica_strutture_aziendali.id_risorsa.HasValue && jrs.anagrafica_strutture_aziendali.id_risorsa == this.id_risorsa);
        }

        [DisplayName("Risorsa")]
        public string CognomeNome
        {
            get
            {
                return cognome + " " + nome;
            }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID")]
            public int id_risorsa { get; set; }

            [DisplayName("Risorsa interna")]
            public string flag_interna_esterna { get; set; }

            [DisplayName("Nome")]
            [Required]
            [StringLength(50)]
            public string nome { get; set; }

            [DisplayName("Cognome")]
            [Required]
            [StringLength(50)]
            public string cognome { get; set; }

            [DisplayName("Indirizzo")]
            public string indirizzo { get; set; }

            [DisplayName("Telefono Cellulare")]
            [StringLength(30)]
            public string tel_cellulare { get; set; }

            [DisplayName("Telefono Casa")]
            [StringLength(30)]
            public string tel_casa { get; set; }
            
            //[EmailAddress]
            //[RegularExpression("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$", ErrorMessage = "Formato email non valido")]
            [Required(ErrorMessage = "Inserire un indirizzo email")]
            [DisplayName("Indirizzo e-mail")]
            public string email { get; set; }

            [DisplayName("Codice Fiscale")]
            [RegularExpression("^[a-zA-Z]{6}[a-zA-Z0-9]{2}[a-zA-Z][a-zA-Z0-9]{2}[a-zA-Z][a-zA-Z0-9]{3}[a-zA-Z]$", ErrorMessage = "Formato Codice Fiscale Non Valido")]
            public string cod_fiscale { get; set; }

            [DisplayName("Data Assunzione")]
            public Nullable<System.DateTime> data_assunzione { get; set; }

            [DisplayName("Data Cessazione")]
            public string data_cessazione { get; set; }

            [Required]
            [StringLength(15)]
            [DisplayName("Username")]
            public string username { get; set; }

            [Required]
            [StringLength(50)]
            [DisplayName("Password")]
            public string password { get; set; }

            [DisplayName("Data Password")]
            public Nullable<System.DateTime> data_password { get; set; }

            [Required]
            [DisplayName("Password Valida")]
            public int validita_password { get; set; }

            [StringLength(10)]
            [DisplayName("Matricola")]
            public string matricola { get; set; }

            [Required]
            [DisplayName("Lavorazione istanze")]
            public string flag_lavorazione_istanze { get; set; }
        }
    }
}
