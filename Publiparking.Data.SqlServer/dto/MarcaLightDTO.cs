using System;
using System.Collections.Generic;
using System.Text;

namespace Publiparking.Data.dto
{
    public class MarcaLightDTO
    {
        /// <summary>
        ///         ''' Identificatore univoco della marca (chiave primaria).
        ///         ''' </summary>
        public Int32 id { get; set; }

        /// <summary>
        ///         ''' Nome della marca.
        ///         ''' </summary>
        public string descrizione { get; set; }
    }
}
