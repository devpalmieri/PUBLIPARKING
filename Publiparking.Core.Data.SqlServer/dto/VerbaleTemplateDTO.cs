using System;
using System.Collections.Generic;
using System.Text;

namespace Publiparking.Core.Data.SqlServer.dto
{
    public class VerbaleTemplateDTO
    {
        /// <summary>
        ///         ''' Identificatore univoco del template
        ///         ''' </summary>
        public Int32 id { get; set; }

        /// <summary>
        ///         ''' Nome del template
        ///         ''' </summary>
        public string nome { get; set; }

        /// <summary>
        ///         ''' Contenuto del template
        ///         ''' </summary>
        public string testo { get; set; }
    }

}
