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
    
    public partial class anagrafica_tipo_spedizione_notifica
    {
        public anagrafica_tipo_spedizione_notifica()
        {
            this.tab_validazione_approvazione_liste = new HashSet<tab_validazione_approvazione_liste>();
            this.tab_validazione_approvazione_liste1 = new HashSet<tab_validazione_approvazione_liste>();
            this.tab_validazione_approvazione_liste2 = new HashSet<tab_validazione_approvazione_liste>();
            this.tab_validazione_approvazione_liste3 = new HashSet<tab_validazione_approvazione_liste>();
            this.tab_validazione_approvazione_liste4 = new HashSet<tab_validazione_approvazione_liste>();
            this.join_sezioni_stampa_tipo_avv_pag = new HashSet<join_sezioni_stampa_tipo_avv_pag>();
            this.tab_lista_sped_notifiche = new HashSet<tab_lista_sped_notifiche>();
            this.tab_sped_not_fatt_emissione = new HashSet<tab_sped_not_fatt_emissione>();
            this.tab_sped_not = new HashSet<tab_sped_not>();
            this.tab_calcolo_tipo_voci_contribuzione = new HashSet<tab_calcolo_tipo_voci_contribuzione>();
            this.tab_modalita_rate_avvpag = new HashSet<tab_modalita_rate_avvpag>();
            this.tab_modalita_rate_avvpag1 = new HashSet<tab_modalita_rate_avvpag>();
            this.tab_modalita_rate_avvpag2 = new HashSet<tab_modalita_rate_avvpag>();
            this.tab_modalita_rate_avvpag3 = new HashSet<tab_modalita_rate_avvpag>();
            this.tab_modalita_rate_avvpag4 = new HashSet<tab_modalita_rate_avvpag>();
            this.tab_modalita_spednot_doc_output = new HashSet<tab_modalita_spednot_doc_output>();
        }
    
        public int id_tipo_spedizione_notifica { get; set; }
        public string descr_tipo_spedizione_notifica { get; set; }
        public string sigla_tipo_spedizione_notifica { get; set; }
        public string flag_sped_not { get; set; }
        public int id_stampatore { get; set; }
        public Nullable<int> id_notificatore { get; set; }
    
        ///<summary><para>Relazione: FK_tab_validazione_approvazione_liste_anagrafica_tipo_spedizione_notifica_nel_PF</para> Chiave Origine: modalita_sped_not_nel_comune_PF</summary>
     public virtual ICollection<tab_validazione_approvazione_liste> tab_validazione_approvazione_liste { get; set; }
        ///<summary><para>Relazione: FK_tab_validazione_approvazione_liste_anagrafica_tipo_spedizione_notifica_nel_PG</para> Chiave Origine: modalita_sped_not_nel_comune_PG</summary>
     public virtual ICollection<tab_validazione_approvazione_liste> tab_validazione_approvazione_liste1 { get; set; }
        ///<summary><para>Relazione: FK_tab_validazione_approvazione_liste_anagrafica_tipo_spedizione_notifica_PF</para> Chiave Origine: modalita_sped_not_fuori_comune_PF</summary>
     public virtual ICollection<tab_validazione_approvazione_liste> tab_validazione_approvazione_liste2 { get; set; }
        ///<summary><para>Relazione: FK_tab_validazione_approvazione_liste_anagrafica_tipo_spedizione_notifica_PG</para> Chiave Origine: modalita_sped_not_fuori_comune_PG</summary>
     public virtual ICollection<tab_validazione_approvazione_liste> tab_validazione_approvazione_liste3 { get; set; }
        ///<summary><para>Relazione: FK_tab_validazione_approvazione_liste_anagrafica_tipo_spedizione_notifica</para> Chiave Origine: modalita_sped_not_estero</summary>
     public virtual ICollection<tab_validazione_approvazione_liste> tab_validazione_approvazione_liste4 { get; set; }
        ///<summary><para>Relazione: FK_join_sezioni_stampa_tipo_avv_pag_anagrafica_tipo_spedizione_notifica</para> Chiave Origine: id_tipo_spedizione_notifica</summary>
     public virtual ICollection<join_sezioni_stampa_tipo_avv_pag> join_sezioni_stampa_tipo_avv_pag { get; set; }
        ///<summary><para>Relazione: FK_tab_lista_sped_notifiche_anagrafica_tipo_spedizione_notifica</para> Chiave Origine: id_tipo_spedizione_notifica</summary>
     public virtual ICollection<tab_lista_sped_notifiche> tab_lista_sped_notifiche { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_anagrafica_tipo_spedizione_notifica</para> Chiave Origine: id_tipo_spedizione_notifica</summary>
     public virtual ICollection<tab_sped_not_fatt_emissione> tab_sped_not_fatt_emissione { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_anagrafica_tipo_spedizione_notifica</para> Chiave Origine: id_tipo_spedizione_notifica</summary>
     public virtual ICollection<tab_sped_not> tab_sped_not { get; set; }
        ///<summary><para>Relazione: FK_tab_calcolo_tipo_voci_contribuzione_anagrafica_tipo_spedizione_notifica</para> Chiave Origine: id_tipo_spedizione_notifica</summary>
     public virtual ICollection<tab_calcolo_tipo_voci_contribuzione> tab_calcolo_tipo_voci_contribuzione { get; set; }
        ///<summary><para>Relazione: FK_anagrafica_tipo_spedizione_notifica_anagrafica_stampatori</para> Chiave Origine: id_stampatore</summary>
     public virtual anagrafica_stampatori anagrafica_stampatori { get; set; }
        ///<summary><para>Relazione: FK_anagrafica_tipo_spedizione_notifica_tab_notificatore</para> Chiave Origine: id_notificatore</summary>
     public virtual tab_notificatore tab_notificatore { get; set; }
        ///<summary><para>Relazione: FK_tab_modalita_rate_avvpag_anagrafica_tipo_spedizione_notifica_comune_PF</para> Chiave Origine: modalita_sped_not_nel_comune_PF</summary>
     public virtual ICollection<tab_modalita_rate_avvpag> tab_modalita_rate_avvpag { get; set; }
        ///<summary><para>Relazione: FK_tab_modalita_rate_avvpag_anagrafica_tipo_spedizione_notifica_comune_PG</para> Chiave Origine: modalita_sped_not_nel_comune_PG</summary>
     public virtual ICollection<tab_modalita_rate_avvpag> tab_modalita_rate_avvpag1 { get; set; }
        ///<summary><para>Relazione: FK_tab_modalita_rate_avvpag_anagrafica_tipo_spedizione_notifica_estero</para> Chiave Origine: modalita_sped_not_estero</summary>
     public virtual ICollection<tab_modalita_rate_avvpag> tab_modalita_rate_avvpag2 { get; set; }
        ///<summary><para>Relazione: FK_tab_modalita_rate_avvpag_anagrafica_tipo_spedizione_notifica_PF</para> Chiave Origine: modalita_sped_not_fuori_comune_PF</summary>
     public virtual ICollection<tab_modalita_rate_avvpag> tab_modalita_rate_avvpag3 { get; set; }
        ///<summary><para>Relazione: FK_tab_modalita_rate_avvpag_anagrafica_tipo_spedizione_notifica_PG</para> Chiave Origine: modalita_sped_not_fuori_comune_PG</summary>
     public virtual ICollection<tab_modalita_rate_avvpag> tab_modalita_rate_avvpag4 { get; set; }
        ///<summary><para>Relazione: FK_tab_modalita_spednot_doc_output_anagrafica_tipo_spedizione_notifica</para> Chiave Origine: id_anagrafica_tipo_spedizione</summary>
     public virtual ICollection<tab_modalita_spednot_doc_output> tab_modalita_spednot_doc_output { get; set; }
    }
}
