using System;
using System.Collections.Generic;
using System.Text;

namespace Publiparking.Core.Data.SqlServer.dto
{
    public abstract class SanzioneDTO
    {
        /// <summary>
        ///         ''' Identificatore univoco della sanzione
        ///         ''' </summary>
        public Int32 id { get; set; }

        /// <summary>
        ///         ''' Data della sanzione
        ///         ''' </summary>
        public DateTime data { get; set; }

        /// <summary>
        ///         ''' Identificatore univoco dell'operatore che ha emesso la sanzione
        ///         ''' </summary>
        public Int32 idOperatore { get; set; }

        /// <summary>
        ///         ''' Identificatore univoco dello stallo su cui è stato emessa la sanzione
        ///         ''' </summary>
        public Int32 idStallo { get; set; }

        /// <summary>
        ///         ''' Ubicazione indicata sulla sanzione
        ///         ''' </summary>
        public string ubicazione { get; set; }

        /// <summary>
        ///         ''' Targa del veicolo sanzionato
        ///         ''' </summary>
        public string targa { get; set; }

        /// <summary>
        ///         ''' Targa Estera
        ///         ''' </summary>
        public bool targaEstera { get; set; }

        /// <summary>
        ///         ''' Tipo del veicolo sanzionato
        ///         ''' </summary>
        public string tipoVeicolo { get; set; }

        /// <summary>
        ///         ''' Marca del veicolo sanzionato
        ///         ''' </summary>
        public string marca { get; set; }

        /// <summary>
        ///         ''' Modello del veicolo sanzionato
        ///         ''' </summary>
        public string modello { get; set; }

        /// <summary>
        ///         ''' Presenza/Assenza del trasgressore
        ///         ''' </summary>
        public bool assenzaTrasgressore { get; set; }

        /// <summary>
        ///         ''' Importo totale della sanzione
        ///         ''' </summary>
        public decimal totale { get; set; }

        /// <summary>
        ///         ''' Note della sanzione
        ///         ''' </summary>
        public string note { get; set; }
    }
}
