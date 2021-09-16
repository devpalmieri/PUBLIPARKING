using System;
using System.Collections.Generic;
using System.Text;

namespace Publiparking.Data.dto
{
    public class VerbaleLightDTO : SanzioneDTO
    {

        /// <summary>
        ///         ''' BarCode indicato sul bollettino assegnato al verbale (Corrisponde al numero del verbale)
        ///         ''' </summary>
        public string codiceBollettino { get; set; }

        /// <summary>
        ///         ''' Serie per la numerazione del verbale
        ///         ''' </summary>
        public string serie { get; set; }
    }
}
