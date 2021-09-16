//------------------------------------------------------------------------------
// <auto-generated>
//     Codice generato da un modello.
//
//     Le modifiche manuali a questo file potrebbero causare un comportamento imprevisto dell'applicazione.
//     Se il codice viene rigenerato, le modifiche manuali al file verranno sovrascritte.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Publisoftware.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tab_sped_not_fatt_emissione
    {
        public int id_sped_not_fatt_emissione { get; set; }
        public Nullable<int> id_lista_sped_not { get; set; }
        public Nullable<int> id_dett_lista_sped_not { get; set; }
        public int progr_lista { get; set; }
        public string barcode { get; set; }
        public int id_tipo_lista { get; set; }
        public Nullable<decimal> id_contribuente { get; set; }
        public Nullable<decimal> id_destinatario { get; set; }
        public string nominativo_recapito { get; set; }
        public string indicativo_rappresentanza { get; set; }
        public string nominativo_rappresentanza { get; set; }
        public Nullable<int> id_avv_pag { get; set; }
        public Nullable<int> num_avv_pag { get; set; }
        public Nullable<int> id_doc_output { get; set; }
        public Nullable<int> num_doc_output { get; set; }
        public Nullable<int> cod_comune { get; set; }
        public string des_comune { get; set; }
        public string cap { get; set; }
        public Nullable<int> id_zona { get; set; }
        public Nullable<int> id_area { get; set; }
        public Nullable<int> id_toponimo { get; set; }
        public string indirizzo_recapito { get; set; }
        public Nullable<int> num_civico { get; set; }
        public string sigla { get; set; }
        public string piano { get; set; }
        public string interno { get; set; }
        public string edificio { get; set; }
        public string condominio { get; set; }
        public string frazione { get; set; }
        public string indirizzo_nuovo { get; set; }
        public Nullable<int> nr_civico_nuovo { get; set; }
        public string sigla_nuovo { get; set; }
        public string scala_nuovo { get; set; }
        public string piano_nuovo { get; set; }
        public string interno_nuovo { get; set; }
        public string edificio_nuovo { get; set; }
        public string condominio_nuovo { get; set; }
        public string frazione_nuovo { get; set; }
        public string cognome_ricevente { get; set; }
        public string nome_ricevente { get; set; }
        public Nullable<int> id_notificatore { get; set; }
        public Nullable<System.DateTime> dt_assegnazione_notifica { get; set; }
        public Nullable<System.DateTime> dt_restituzione_notifica { get; set; }
        public Nullable<System.DateTime> dt_spedizione_notifica { get; set; }
        public Nullable<System.DateTime> dt_scadenza_notifica { get; set; }
        public Nullable<int> id_stato_sped_not { get; set; }
        public Nullable<int> id_tipo_esito_notifica { get; set; }
        public string numero_raccomandata { get; set; }
        public Nullable<System.DateTime> data_esito_notifica { get; set; }
        public Nullable<int> id_lista_affissione_ap { get; set; }
        public Nullable<System.DateTime> data_affissione_ap { get; set; }
        public Nullable<int> id_lista_invio_avvdep { get; set; }
        public Nullable<System.DateTime> data_spedizione_avvdep { get; set; }
        public string numero_raccomandata_avvdep { get; set; }
        public Nullable<System.DateTime> data_notifica_avvdep { get; set; }
        public Nullable<int> id_stato_avvdep { get; set; }
        public Nullable<int> id_stato { get; set; }
        public string cod_stato { get; set; }
        public Nullable<System.DateTime> data_stato { get; set; }
        public Nullable<int> id_struttura_stato { get; set; }
        public Nullable<int> id_risorsa_stato { get; set; }
        public Nullable<int> id_tipo_avv_pag { get; set; }
        public Nullable<int> id_tipo_esito_avvdep { get; set; }
        public string cod_zona { get; set; }
        public Nullable<decimal> cod_area { get; set; }
        public string prov { get; set; }
        public string cod_fiscale_recapito { get; set; }
        public string barcode_avvdep { get; set; }
        public Nullable<int> id_risorsa_restituzione { get; set; }
        public Nullable<int> id_terzo_debitore { get; set; }
        public Nullable<int> id_referente { get; set; }
        public string cod_ente { get; set; }
        public Nullable<int> id_cc_riscossione { get; set; }
        public Nullable<int> id_risorsa_resp_emissione_avvpag { get; set; }
        public Nullable<int> id_risorsa_resp_notifica { get; set; }
        public Nullable<decimal> id_contribuente_STO { get; set; }
        public Nullable<int> id_referente_STO { get; set; }
        public Nullable<int> id_terzo_STO { get; set; }
        public Nullable<int> id_domicilio { get; set; }
        public string pec_destinatario { get; set; }
        public string flag_destinatario_spednot { get; set; }
        public string email_destinatario { get; set; }
        public string cellulare_destinatario { get; set; }
        public string descr_oggetto_destinatario { get; set; }
        public string testo_comunicazione { get; set; }
        public string cognome_ragsoc_contribuente { get; set; }
        public string codfis_piva_contribuente { get; set; }
        public string cognome_ragsoc_referente { get; set; }
        public string codfis_piva_referente { get; set; }
        public string cognome_ragsoc_terzo { get; set; }
        public string codfis_piva_terzo { get; set; }
        public string flag_domicilio { get; set; }
        public string cognome_ragsoc_domicilio { get; set; }
        public Nullable<int> id_tipo_spedizione_notifica { get; set; }
        public Nullable<int> id_avv_pag_fatt_emissione { get; set; }
        public string numero_atto_giudiziario { get; set; }
    
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_anagrafica_risorse</para> Chiave Origine: id_risorsa_stato</summary>
     public virtual anagrafica_risorse anagrafica_risorse { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_anagrafica_risorse_emissione</para> Chiave Origine: id_risorsa_resp_emissione_avvpag</summary>
     public virtual anagrafica_risorse anagrafica_risorse1 { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_anagrafica_risorse_notifica</para> Chiave Origine: id_risorsa_resp_notifica</summary>
     public virtual anagrafica_risorse anagrafica_risorse2 { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_anagrafica_stato_sped_not</para> Chiave Origine: id_stato_sped_not</summary>
     public virtual anagrafica_stato_sped_not anagrafica_stato_sped_not { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_anagrafica_stato_sped_not1</para> Chiave Origine: id_stato</summary>
     public virtual anagrafica_stato_sped_not anagrafica_stato_sped_not1 { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_anagrafica_stato_sped_not2</para> Chiave Origine: id_stato_avvdep</summary>
     public virtual anagrafica_stato_sped_not anagrafica_stato_sped_not2 { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_anagrafica_strutture_aziendali</para> Chiave Origine: id_struttura_stato</summary>
     public virtual anagrafica_strutture_aziendali anagrafica_strutture_aziendali { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_anagrafica_tipo_avv_pag</para> Chiave Origine: id_tipo_avv_pag</summary>
     public virtual anagrafica_tipo_avv_pag anagrafica_tipo_avv_pag { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_anagrafica_tipo_esito_notifica</para> Chiave Origine: id_tipo_esito_notifica</summary>
     public virtual anagrafica_tipo_esito_notifica anagrafica_tipo_esito_notifica { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_anagrafica_tipo_esito_notifica1</para> Chiave Origine: id_tipo_esito_avvdep</summary>
     public virtual anagrafica_tipo_esito_notifica anagrafica_tipo_esito_notifica1 { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_anagrafica_tipo_spedizione_notifica</para> Chiave Origine: id_tipo_spedizione_notifica</summary>
     public virtual anagrafica_tipo_spedizione_notifica anagrafica_tipo_spedizione_notifica { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_tab_avv_pag</para> Chiave Origine: id_avv_pag</summary>
     public virtual tab_avv_pag tab_avv_pag { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_tab_avv_pag_fatt_emissione</para> Chiave Origine: id_avv_pag_fatt_emissione</summary>
     public virtual tab_avv_pag_fatt_emissione tab_avv_pag_fatt_emissione { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_tab_domicilio</para> Chiave Origine: id_domicilio</summary>
     public virtual tab_domicilio tab_domicilio { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_tab_lista_sped_notifiche</para> Chiave Origine: id_lista_sped_not</summary>
     public virtual tab_lista_sped_notifiche tab_lista_sped_notifiche { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_tab_lista_sped_notifiche1</para> Chiave Origine: id_lista_affissione_ap</summary>
     public virtual tab_lista_sped_notifiche tab_lista_sped_notifiche1 { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_tab_lista_sped_notifiche2</para> Chiave Origine: id_lista_invio_avvdep</summary>
     public virtual tab_lista_sped_notifiche tab_lista_sped_notifiche2 { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_tab_notificatore</para> Chiave Origine: id_notificatore</summary>
     public virtual tab_notificatore tab_notificatore { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_tab_referente_sto</para> Chiave Origine: id_referente_STO</summary>
     public virtual tab_referente_sto tab_referente_sto { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_tab_terzo_debitore</para> Chiave Origine: id_terzo_debitore</summary>
     public virtual tab_terzo_debitore tab_terzo_debitore { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_tab_toponimi</para> Chiave Origine: id_toponimo</summary>
     public virtual tab_toponimi tab_toponimi { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_tab_contribuente_sto_new</para> Chiave Origine: id_contribuente_STO</summary>
     public virtual tab_contribuente_sto_new tab_contribuente_sto_new { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_tab_cc_riscossione</para> Chiave Origine: id_cc_riscossione</summary>
     public virtual tab_cc_riscossione tab_cc_riscossione { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_tab_contribuente</para> Chiave Origine: id_contribuente</summary>
     public virtual tab_contribuente tab_contribuente { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_tab_referente</para> Chiave Origine: id_referente</summary>
     public virtual tab_referente tab_referente { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_tab_terzo_sto</para> Chiave Origine: id_terzo_STO</summary>
     public virtual tab_terzo_sto tab_terzo_sto { get; set; }
    }
}
