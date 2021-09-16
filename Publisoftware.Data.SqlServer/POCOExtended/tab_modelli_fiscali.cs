using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_modelli_fiscali : ISoftDeleted, IValidator, IValidatableObject
    {
        public const string tipo_Stipendio= "STI";
        public const string tipo_Pensione = "PEN";
        public const int id_tipo_bene_stipendi = 1;
        public const int id_tipo_bene_pensione = 2;

        public List<ValidationResult> validationErrors()
        {
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(tab_modelli_fiscali), typeof(tab_modelli_fiscali.Metadata)), typeof(tab_modelli_fiscali));

            ValidationContext context = new ValidationContext(this, null, null);
            List<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(this, context, results, true);

            return results;
        }
        public sealed class Metadata
        {
            private Metadata()
            {
            }
            public int id_modello_fiscale { get; set; }
            public int? id_ente { get; set; }
            public string codice_fornitura { get; set; }
            public int? progressivo_fornitura { get; set; }
            public string data_fornitura { get; set; }
            public int? anno_imposta { get; set; }
            public int? codice_regione { get; set; }
            public string codice_catastale_comune { get; set; }
            public string tipo_modello { get; set; }
            public string descrizione_modello { get; set; }
            public string identificativo_telematico_dichiarazione { get; set; }
            public string cf_dichiarante { get; set; }
            public string cf_piva_sost_di_imposta { get; set; }
            public string sigla_tipo_bene { get; set; }
            public int? id_tipo_bene { get; set; }
            public decimal? importo_annuo { get; set; }
            public int? gg_fruizione_annui { get; set; }
            public string cod_stato { get; set; }
        }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (id_modello_fiscale < 0)
            {
                yield return new ValidationResult
                 ("Errore", new[] { "id_tab_mov_pag" });
            }
        }
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public bool IsValid
        {
            get { return checkValidity(); }
        }

        protected bool checkValidity()
        {
            return this.validationErrors().Count == 0;
        }

        //public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        //{
        //    data_stato = DateTime.Now;
        //    id_struttura_stato = p_idStruttura;
        //    id_risorsa_stato = p_idRisorsa;
        //}
    }
   
}
