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
    
    public partial class anagrafica_tipo_relazione
    {
        public anagrafica_tipo_relazione()
        {
            this.join_referente_contribuente = new HashSet<join_referente_contribuente>();
            this.join_tipo_stato_referenza = new HashSet<join_tipo_stato_referenza>();
            this.anagrafica_procedure_concorsuali = new HashSet<anagrafica_procedure_concorsuali>();
            this.tab_anagrafiche_da_rivestire = new HashSet<tab_anagrafiche_da_rivestire>();
            this.join_terzo_referente = new HashSet<join_terzo_referente>();
        }
    
        public int id_tipo_relazione { get; set; }
        public Nullable<int> flag_allineamento { get; set; }
        public string cod_tipo_relazione { get; set; }
        public string desc_tipo_relazione { get; set; }
        public string flag_fisica_giuridica { get; set; }
        public System.DateTime data_stato { get; set; }
        public int id_struttura_stato { get; set; }
        public int id_risorsa_stato { get; set; }
        public string cod_stato { get; set; }
    
        ///<summary><para>Relazione: FK_anagrafica_tipo_relazione_anagrafica_cod_stato_base</para> Chiave Origine: cod_stato</summary>
     public virtual anagrafica_cod_stato_base anagrafica_cod_stato_base { get; set; }
        ///<summary><para>Relazione: FK_join_referente_contribuente_anagrafica_tipo_relazione</para> Chiave Origine: id_tipo_relazione</summary>
     public virtual ICollection<join_referente_contribuente> join_referente_contribuente { get; set; }
        ///<summary><para>Relazione: FK_join_tipo_stato_referenza_anagrafica_tipo_relazione</para> Chiave Origine: id_tipo_relazione</summary>
     public virtual ICollection<join_tipo_stato_referenza> join_tipo_stato_referenza { get; set; }
        ///<summary><para>Relazione: FK_anagrafica_procedure_concorsuali_anagrafica_tipo_relazione</para> Chiave Origine: id_tipo_relazione_curatore_commissario</summary>
     public virtual ICollection<anagrafica_procedure_concorsuali> anagrafica_procedure_concorsuali { get; set; }
        ///<summary><para>Relazione: FK_603855E2_E206_4EA6_A7F5_925512F7E7E6</para> Chiave Origine: id_anagrafica_tipo_relazione_contr_coll</summary>
     public virtual ICollection<tab_anagrafiche_da_rivestire> tab_anagrafiche_da_rivestire { get; set; }
        ///<summary><para>Relazione: FK_join_terzo_referente_anagrafica_tipo_relazione</para> Chiave Origine: id_tipo_relazione</summary>
     public virtual ICollection<join_terzo_referente> join_terzo_referente { get; set; }
    }
}
