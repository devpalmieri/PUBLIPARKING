using System;
using System.Collections.Generic;
using System.Text;

namespace Publiparking.Core.Data.SqlServer.dto
{
    using System;

    public class StalloLightDTO
    {
        /// <summary>
        ///         ''' Identificatore univoco dello stallo
        ///         ''' </summary>
        public Int32 id { get; set; }

        /// <summary>
        ///         ''' Numero dello stallo
        ///         ''' </summary>
        public string numero { get; set; }

        /// <summary>
        ///         ''' Latitudine dello stallo
        ///         ''' </summary>
        public double X { get; set; }

        /// <summary>
        ///         ''' Longitudine dello stallo
        ///         ''' </summary>
        public double Y { get; set; }

        /// <summary>
        ///         ''' Indirizzo dello stallo (Utilizzato nella stampa del verbale)
        ///         ''' </summary>
        public string ubicazione { get; set; }

        /// <summary>
        ///         ''' Indica se per la prossima operazione sullo stallo è richiesta la foto
        ///         ''' </summary>
        public bool fotoRichiesta { get; set; }

        /// <summary>
        ///         ''' Identificatore del toponimo dalla tabella tab_toponimi
        ///         ''' </summary>
        public Int32 idToponimo { get; set; }

        /// <summary>
        ///         ''' Identificatore della tariffa applicata allo stallo
        ///         ''' </summary>
        public Int32 idTariffa { get; set; }
    }

}
