using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_esito_aci.Metadata))]
    public partial class tab_esito_aci: ISoftDeleted
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            //[Required]
            //[DisplayName("id tab esito aci")]
            //public int id_tab_esito_aci { get; set; }
            //[Required]
            //[DisplayName("id join tab supervisione profili")]
            //public int id_join_tab_supervisione_profili { get; set; }
            //[DisplayName("identificativo pratica interno")]
            //public int? identificativo_pratica_interno { get; set; }
            //[DisplayName("flag presentazione")]
            //public string flag_presentazione { get; set; }
            //[DisplayName("data presentazione")]
            //public DateTime? data_presentazione { get; set; }
            //[Required]
            //[DisplayName("protocollo aci presentazione")]
            //public string protocollo_aci_presentazione { get; set; }
            //[Required]
            //[DisplayName("data protocollo aci presentazione")]
            //public DateTime data_protocollo_aci_presentazione { get; set; }
            //[DisplayName("codice risposta presentazione")]
            //public string codice_risposta_presentazione { get; set; }
            //[DisplayName("codice errore presentazione")]
            //public string codice_errore_presentazione { get; set; }
            //[DisplayName("descrizione presentazione")]
            //public string descrizione_presentazione { get; set; }
            //[Required]
            //[DisplayName("response presentazione")]
            //public string response_presentazione { get; set; }
            //[DisplayName("flag accettazione")]
            //public string flag_accettazione { get; set; }
            //[DisplayName("data accettazione")]
            //public DateTime? data_accettazione { get; set; }
            //[DisplayName("codice risposta accettazione")]
            //public string codice_risposta_accettazione { get; set; }
            //[DisplayName("codice errore accettazione")]
            //public string codice_errore_accettazione { get; set; }
            //[DisplayName("descrizione accettazione")]
            //public string descrizione_accettazione { get; set; }
            //[Required]
            //[DisplayName("response accettazione")]
            //public string response_accettazione { get; set; }
            //[DisplayName("cod stato")]
            //public string cod_stato { get; set; }
            //[DisplayName("flag on off")]
            //public string flag_on_off { get; set; }
        }
    }
}
