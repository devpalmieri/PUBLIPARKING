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
    
    public partial class tab_tipo_toponimo
    {
        public tab_tipo_toponimo()
        {
            this.tab_toponimi = new HashSet<tab_toponimi>();
            this.tab_rilevazione_acquedotto_smart = new HashSet<tab_rilevazione_acquedotto_smart>();
        }
    
        public int id_tipo_toponimo { get; set; }
        public string descrizione_tipo_toponimo { get; set; }
        public Nullable<int> id_tipo_toponimo_ente { get; set; }
    
        ///<summary><para>Relazione: FK_tab_toponimi_tab_tipo_toponimo</para> Chiave Origine: id_tipo_toponimo</summary>
     public virtual ICollection<tab_toponimi> tab_toponimi { get; set; }
        ///<summary><para>Relazione: FK_tab_rilevazione_acquedotto_smart_tab_tipo_toponimo</para> Chiave Origine: id_tipo_toponimo</summary>
     public virtual ICollection<tab_rilevazione_acquedotto_smart> tab_rilevazione_acquedotto_smart { get; set; }
    }
}