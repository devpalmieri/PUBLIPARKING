using System;
using System.Collections.Generic;
using System.Text;

namespace Publiparking.Data.dto
{
    public class TitoloDTO
    {
        /// <summary>
        ///         ''' Identificatore univoco del titolo (chiave primaria).
        ///         ''' </summary>
        public Int64 id { get; set; }

        /// <summary>
        ///         ''' Codice del titolo.
        ///         ''' </summary>
        public string codice { get; set; }

        /// <summary>
        ///         ''' Identificatore univoco dello stallo pagato.
        ///         ''' </summary>
        public Int32? idStallo { get; set; }

        /// <summary>
        ///         ''' Data di pagamento del titolo.
        ///         ''' </summary>
        public DateTime? dataPagamento { get; set; }

        /// <summary>
        ///         ''' Data di scadenza del titolo.
        ///         ''' </summary>
        public DateTime scadenza { get; set; }

        /// <summary>
        ///         ''' importo del titolo.
        ///         ''' </summary
        public decimal importo { get; set; }

    }
}
