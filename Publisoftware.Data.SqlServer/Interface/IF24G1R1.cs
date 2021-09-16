using System;
using System.Dynamic;

namespace Publisoftware.Data
{
    // ReSharper disable InconsistentNaming
    public interface IF24G1R1
    {
        string codice_fiscale { get; }

        DateTime? data_riscossione { get; }
        string codice_ente { get; }
        string cab { get; }
    }

    /// <summary>
    /// Usare nelle proiezioni per limitare il numero di campi
    /// </summary>
    public class F24G1R1Impl : IF24G1R1
    {
        public string codice_fiscale { get; set; }
        public DateTime? data_riscossione { get; set; }
        public string codice_ente { get; set; }
        public string cab { get; set; }
    }
}