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
    
    public partial class tab_ente_escluso_emissione
    {
        public int id_ente_escluso_emissione { get; set; }
        public int cod_comune { get; set; }
        public System.DateTime periodo_validita_da { get; set; }
        public System.DateTime periodo_validita_a { get; set; }
    
        ///<summary><para>Relazione: FK_tab_enti_esclusi_emissione_ser_comuni</para> Chiave Origine: cod_comune</summary>
     public virtual ser_comuni ser_comuni { get; set; }
    }
}