using System;
using System.Collections.Generic;
using System.Text;

namespace Publiparking.Core.Data.SqlServer.dto
{
    public class TitoloOperatoreDTO : TitoloDTO
    {
        /// <summary>
        ///         ''' Identificatore univoco del cellulare mandante.
        ///         ''' </summary>
        public Int32 idOperatore { get; set; }

        /// <summary>
        ///         ''' Targa Pagata.
        ///         ''' </summary>
        public string targa { get; set; }

        /// <summary>
        ///         ''' Importo pagato.
        ///         ''' </summary>
       // public float importo { get; set; }
    }
}
