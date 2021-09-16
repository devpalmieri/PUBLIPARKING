using System;
using System.Collections.Generic;
using System.Text;

namespace Publiparking.Core.Data.SqlServer.dto
{
    public class TariffaLightDTO
    {
        /// <summary>
        ///     ''' Identificatore univoco della tariffa (chiave primaria).
        ///     ''' </summary>
        public Int32 id { get; set; }

        /// <summary>
        ///     ''' Descrizione testuale della tariffa
        ///     ''' </summary>
        public string descrizione { get; set; }
    }

}
