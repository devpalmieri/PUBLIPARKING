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
    
    public partial class tab_firma_digitale
    {
        public int id_tab_firma_digitale { get; set; }
        public int id_risorsa { get; set; }
        public byte[] firma_uno { get; set; }
        public byte[] firma_2 { get; set; }
        public byte[] firma_3 { get; set; }
        public byte[] firma_4 { get; set; }
        public byte[] firma_5 { get; set; }
        public byte[] timbro { get; set; }
        public byte[] sigla { get; set; }
        public string flag_on_off { get; set; }
    
        ///<summary><para>Relazione: FK_tab_firma_digitale_anagrafica_risorse</para> Chiave Origine: id_risorsa</summary>
     public virtual anagrafica_risorse anagrafica_risorse { get; set; }
    }
}
