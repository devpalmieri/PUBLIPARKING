using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_notificatore.Metadata))]
    public partial class tab_notificatore : ISoftDeleted
    {
        public const string FLAG_PERSONA_GIURIDICA = "PP";
        public const string FLAG_PERSONA_FISICA = "PF";

        public const int POSTE_ID = 650;

        public bool IsSoftDeletable
        {
            get { return true; }
        }
        public string NominativoNotificatore
        {
            get
            {

                if (rag_sociale != null)
                {
                    return rag_sociale;
                }
                else
                {
                    return cognome + " " + nome;
                }
            }
        }
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Id")]
            public int id_notificatore { get; set; }

            [Required]
            [DisplayName("Ente")]
            public int id_ente { get; set; }

            [DisplayName("Flag Risorsa Interna/Esterna")]
            [RegularExpression("[0-9]{0,1}", ErrorMessage = "Formato non valido")]
            public string flag_risorsa_interna_esterna { get; set; }

            [DisplayName("Flag Persona Fisica/Giuridica")]
            [RegularExpression("([P]{1}[P]{1})|([P]{1}[F]{1})", ErrorMessage = "Formato non valido: PF o PP")]
            public string flag_persona_fisica_giuridica { get; set; }

            [DisplayName("Cognome")]
            public string cognome { get; set; }

            [DisplayName("Nome")]
            public string nome { get; set; }

            [DisplayName("Codice Fiscale")]
            [RegularExpression("[a-zA-Z]{6}[a-zA-Z0-9]{2}[a-zA-Z][a-zA-Z0-9]{2}[a-zA-Z][a-zA-Z0-9]{3}[a-zA-Z]", ErrorMessage = "Formato non valido")]
            public string cod_fiscale { get; set; }

            [DisplayName("P.IVA")]
            [RegularExpression("[0-9]{11}", ErrorMessage = "Formato non valido")]
            public string p_iva { get; set; }

            [DisplayName("Ragione Sociale")]
            public string rag_sociale { get; set; }

            [Required]
            [DisplayName("Fonte")]
            public string fonte { get; set; }

            [DisplayName("Indirizzo")]
            public string indirizzo { get; set; }

            [DisplayName("Data Nascita")]
            public DateTime? data_nascita { get; set; }

            [DisplayName("Email")]
            //[EmailAddress]
            [RegularExpression("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$", ErrorMessage = "Formato email non valido")]
            public string email { get; set; }

            [DisplayName("Telefono Casa")]
            [RegularExpression("[0-9]{0,}", ErrorMessage = "Formato non valido")]
            public string tel_casa { get; set; }

            [DisplayName("Telefono Cellulare")]
            [RegularExpression("[0-9]{0,}", ErrorMessage = "Formato non valido")]
            public string tel_cellulare { get; set; }
        }
    }
}
