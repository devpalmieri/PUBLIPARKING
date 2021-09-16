using System;
using System.Collections.Generic;
using System.Text;

namespace Publiparking.Data.dto
{
    public class TipoVerbaleLightDTO
    {
        private Int32 m_id;
        /// <summary>
        ///         ''' Identificatore univoco del tipo verbale (chiave primaria).
        ///         ''' </summary>
        public Int32 id { get; set; }

        private string m_descrizione;
        /// <summary>
        ///         ''' Nome del tipo verbale.
        ///         ''' </summary>
        public string descrizione { get; set; }

        /// <summary>
        ///         ''' Indica se il tipo verbale è il default per il mancato pagamento.
        ///         ''' </summary>
        public bool isDefault { get; set; }
    }
}
