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
    
    public partial class ser_comuni
    {
        public ser_comuni()
        {
            this.anagrafica_ente_gestito = new HashSet<anagrafica_ente_gestito>();
            this.anagrafica_ente_riversamento = new HashSet<anagrafica_ente_riversamento>();
            this.join_risorse_ser_comuni = new HashSet<join_risorse_ser_comuni>();
            this.tab_anagrafe = new HashSet<tab_anagrafe>();
            this.tab_autorita_giudiziaria = new HashSet<tab_autorita_giudiziaria>();
            this.tab_dett_lista_sped_not = new HashSet<tab_dett_lista_sped_not>();
            this.tab_sped_not = new HashSet<tab_sped_not>();
            this.tab_storico_anagrafe = new HashSet<tab_storico_anagrafe>();
            this.tab_toponimi = new HashSet<tab_toponimi>();
            this.tab_zone = new HashSet<tab_zone>();
            this.tab_contribuente = new HashSet<tab_contribuente>();
            this.tab_contribuente1 = new HashSet<tab_contribuente>();
            this.tab_referente = new HashSet<tab_referente>();
            this.anagrafica_risorse = new HashSet<anagrafica_risorse>();
            this.tab_ente_escluso_emissione = new HashSet<tab_ente_escluso_emissione>();
            this.anagrafica_ente = new HashSet<anagrafica_ente>();
        }
    
        public int cod_comune { get; set; }
        public string des_comune { get; set; }
        public string cap_comune { get; set; }
        public string cod_catasto { get; set; }
        public int cod_comune_tribunale { get; set; }
        public int cod_regione { get; set; }
        public int cod_provincia { get; set; }
        public int f_comune_sto { get; set; }
        public string cod_istat { get; set; }
    
        ///<summary><para>Relazione: FK_anagrafica_ente_gestito_ser_comuni</para> Chiave Origine: cod_comune</summary>
     public virtual ICollection<anagrafica_ente_gestito> anagrafica_ente_gestito { get; set; }
        ///<summary><para>Relazione: FK_anagrafica_ente_riversamento_ser_comuni</para> Chiave Origine: cod_comune</summary>
     public virtual ICollection<anagrafica_ente_riversamento> anagrafica_ente_riversamento { get; set; }
        ///<summary><para>Relazione: FK_join_risorse_ser_comuni_ser_comuni</para> Chiave Origine: cod_comune</summary>
     public virtual ICollection<join_risorse_ser_comuni> join_risorse_ser_comuni { get; set; }
        ///<summary><para>Relazione: FK_ser_comuni_ser_regioni</para> Chiave Origine: cod_regione</summary>
     public virtual ser_regioni ser_regioni { get; set; }
        ///<summary><para>Relazione: FK_tab_anagrafe_ser_comuni</para> Chiave Origine: cod_comune</summary>
     public virtual ICollection<tab_anagrafe> tab_anagrafe { get; set; }
        ///<summary><para>Relazione: FK_tab_autorita_giudiziaria_ser_comuni</para> Chiave Origine: cod_comune</summary>
     public virtual ICollection<tab_autorita_giudiziaria> tab_autorita_giudiziaria { get; set; }
        ///<summary><para>Relazione: FK_tab_dett_lista_sped_not_ser_comuni</para> Chiave Origine: cod_comune</summary>
     public virtual ICollection<tab_dett_lista_sped_not> tab_dett_lista_sped_not { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_ser_comuni</para> Chiave Origine: cod_comune</summary>
     public virtual ICollection<tab_sped_not> tab_sped_not { get; set; }
        ///<summary><para>Relazione: FK_tab_storico_anagrafe_ser_comuni</para> Chiave Origine: cod_comune</summary>
     public virtual ICollection<tab_storico_anagrafe> tab_storico_anagrafe { get; set; }
        ///<summary><para>Relazione: FK_tab_toponimi_ser_comuni</para> Chiave Origine: cod_comune_toponimo</summary>
     public virtual ICollection<tab_toponimi> tab_toponimi { get; set; }
        ///<summary><para>Relazione: FK_tab_zone_ser_comuni</para> Chiave Origine: cod_comune</summary>
     public virtual ICollection<tab_zone> tab_zone { get; set; }
        ///<summary><para>Relazione: FK_ser_comuni_ser_province</para> Chiave Origine: cod_provincia</summary>
     public virtual ser_province ser_province { get; set; }
        ///<summary><para>Relazione: FK_tab_contribuente_ser_comuni</para> Chiave Origine: cod_comune_nas</summary>
     public virtual ICollection<tab_contribuente> tab_contribuente { get; set; }
        ///<summary><para>Relazione: FK_tab_contribuente_ser_comuni1</para> Chiave Origine: cod_citta</summary>
     public virtual ICollection<tab_contribuente> tab_contribuente1 { get; set; }
        ///<summary><para>Relazione: FK_tab_referente_ser_comuni</para> Chiave Origine: cod_citta</summary>
     public virtual ICollection<tab_referente> tab_referente { get; set; }
        ///<summary><para>Relazione: FK_anagrafica_risorse_ser_comuni</para> Chiave Origine: cod_comune_nas</summary>
     public virtual ICollection<anagrafica_risorse> anagrafica_risorse { get; set; }
        ///<summary><para>Relazione: FK_tab_enti_esclusi_emissione_ser_comuni</para> Chiave Origine: cod_comune</summary>
     public virtual ICollection<tab_ente_escluso_emissione> tab_ente_escluso_emissione { get; set; }
        ///<summary><para>Relazione: FK_anagrafica_ente_ser_comuni</para> Chiave Origine: cod_comune</summary>
     public virtual ICollection<anagrafica_ente> anagrafica_ente { get; set; }
    }
}