//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Publisoftware.Data
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    [MetadataTypeAttribute(typeof(tab_rilevazione_acquedotto.Metadata))]
    public partial class tab_rilevazione_acquedotto : ISoftDeleted
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
            [DisplayName("ID")]
            public decimal id_tab_rilevazione_acquedotto { get; set; }
            [DisplayName("ID Ente")]
            public int id_ente { get; set; }
            public int id_ente_gestito { get; set; }
            public int id_entrata { get; set; }
            public int id_lista { get; set; }
            public string cod_lista { get; set; }
            public int anno_lista { get; set; }
            public int prog_lista { get; set; }
            public int cod_comune { get; set; }
            public string cap { get; set; }
            public int id_rilevatore { get; set; }
            public int id_zona { get; set; }
            public int id_area { get; set; }
            public decimal id_contribuente { get; set; }
            public string codice_contribuente { get; set; }
            public decimal id_nucleo_coabitazione { get; set; }
            public decimal id_anagrafe { get; set; }
            public decimal id_oggetto { get; set; }
            public string codice_utenza { get; set; }
            public string flag_utenza_irregolare_censita { get; set; }
            public string codice_utenza_collegata { get; set; }
            public decimal id_oggetto_contribuzione { get; set; }
            public string flag_contrib_residente { get; set; }
            public string flag_contrib_occupante { get; set; }
            public string flag_contrib_inquilino { get; set; }
            public string flag_contrib_proprietario { get; set; }
            public string flag_attivo { get; set; }
            public decimal id_proprietario { get; set; }
            public decimal id_coniuge_convivente { get; set; }
            public string flag_sconosciuto { get; set; }
            public string flag_deceduto { get; set; }
            public string flag_trasferito { get; set; }
            public string flag_vendita { get; set; }
            public string flag_assente { get; set; }
            public string flag_errata_utenza_abusiva { get; set; }
            public string flag_non_disponibile { get; set; }
            public string flag_nuovo_occupante { get; set; }
            public string flag_utenza_non_identificata { get; set; }
            public string flag_dati_anagrafici_intestatario { get; set; }
            public string flag_utenza_intestatari_duplicati { get; set; }
            public int prog_lista_riferimento { get; set; }
            public string tipo_pf_pg_occupante { get; set; }
            public string cognome_rag_sociale_occupante { get; set; }
            public string nome_den_economica_occupante { get; set; }
            public string codice_fiscale_occupante { get; set; }
            public string p_iva_occupante { get; set; }
            public string nome_ref { get; set; }
            public string cognome_ref { get; set; }
            public string rag_sociale_ref { get; set; }
            public string cod_fiscale_ref { get; set; }
            public string p_iva_ref { get; set; }
            public string tipo_relazione_ref { get; set; }
            public string indirizzo_ref { get; set; }
            public string citta_ref { get; set; }
            public string nr_civico_ref { get; set; }
            public string sigla_civico_ref { get; set; }
            public string scala_ref { get; set; }
            public string piano_ref { get; set; }
            public string interno_ref { get; set; }
            public string edificio_ref { get; set; }
            public string condominio_ref { get; set; }
            public string frazione_ref { get; set; }
            public string macrocategoria_utenza { get; set; }
            public int id_categoria_tariffaria_utenza { get; set; }
            public int id_nuova_categoria_tariffaria_utenza { get; set; }
            public string annotazioni { get; set; }
            public string indirizzo_variato { get; set; }
            public int nr_civico_da { get; set; }
            public string sigla_civico_da { get; set; }
            public int nr_civico_a { get; set; }
            public string sigla_civico_a { get; set; }
            public string scala { get; set; }
            public string piano { get; set; }
            public string interno { get; set; }
            public string cod_parco { get; set; }
            public string cod_edificio { get; set; }
            public string condominio { get; set; }
            public string frazione { get; set; }
            public int id_toponimo_nuova_utenza_censita { get; set; }
            public System.DateTime data_assegnazione_modulo { get; set; }
            public System.DateTime data_rilevazione { get; set; }
            public int ora_assente { get; set; }
            public int minuti_assente { get; set; }
            public System.DateTime data_restituzione_modulo { get; set; }
            public string flag_priorita_rilevazione { get; set; }
            public int id_stato { get; set; }
            public string cod_stato { get; set; }
            public System.DateTime data_stato { get; set; }
            public int id_struttura_stato { get; set; }
            public int id_risorsa_stato { get; set; }

        }
    }
}
