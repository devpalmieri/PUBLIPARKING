using System;
using System.Collections.Generic;
using System.Text;

namespace Publiparking.Data.dto
{
    public class CausaleDTO
    {
        private Int32 m_id;
        /// <summary>
        ///         ''' Identificatore univoco della causale
        ///         ''' </summary>
        public Int32 id { get; set; }

        /// <summary>
        ///         ''' Articolo della causale
        ///         ''' </summary>
        public string articolo { get; set; }

        /// <summary>
        ///         ''' Codice
        ///         ''' </summary>
        public string codice { get; set; }

        /// <summary>
        ///         ''' SubCodice
        ///         ''' </summary>
        public string subCodice { get; set; }

        /// <summary>
        ///         ''' Descrizione della causale
        ///         ''' </summary>
        public string descrizione { get; set; }

        /// <summary>
        ///         ''' Descrizione della causale che verrà stampata
        ///         ''' </summary>
        public string descrizioneStampa { get; set; }

        /// <summary>
        ///         ''' Importo della causale
        ///         ''' </summary>
        public decimal importo { get; set; }

        /// <summary>
        ///         ''' Stato della causale
        ///         ''' </summary>
        public bool attivo { get; set; }
    }
}
