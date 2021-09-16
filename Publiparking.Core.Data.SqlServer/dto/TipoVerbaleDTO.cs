using System;
using System.Collections.Generic;
using System.Text;

namespace Publiparking.Core.Data.SqlServer.dto
{
    public class TipoVerbaleDTO : TipoVerbaleLightDTO
    {
        /// <summary>
        ///         ''' Elenco delle causali associate al tipo verbale
        ///         ''' </summary>
        public List<Int32> Causali { get; set; }
    }
}
