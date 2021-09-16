using System;
using System.Collections.Generic;
using System.Text;

namespace Publiparking.Data.dto
{
    public class ComuneDTO
    {
        /// <summary>
        ///         ''' Identificatore univoco del comune
        ///         ''' </summary>
        public Int32 id { get; set; }

        private string m_descrizione;
        /// <summary>
        ///         ''' Nome del comune
        ///         ''' </summary>
        public string descrizione { get; set; }
    }
}
