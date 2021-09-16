using System;
using System.Collections.Generic;
using System.Text;

namespace Publiparking.Core.Data.SqlServer.dto
{
    public class PenaleDTO : SanzioneDTO
    {

        /// <summary>
        ///         ''' Elenco delle foto associate alla penale
        ///         ''' </summary>
        public List<string> foto { get; set; } = new List<string>();

        /// <summary>
        ///         ''' Codice per il pagamento della penale
        ///         ''' </summary>
        public string codice { get; set; } = "";

        /// <summary>
        ///         ''' Stato pagamento della penale (SI (true), NO (false), NON ELABORATA (null))
        ///         ''' </summary>
        public bool? pagata { get; set; }

        /// <summary>
        ///         ''' Data elaboraziione dello stato della penale (null = non elaborata)
        ///         ''' </summary>
        public DateTime? dataElaborazione { get; set; }

        /// <summary>
        ///         ''' Identificatore univoco del verbale emesso per le penali non pagate
        ///         ''' </summary>
        public Int32? idVerbale { get; set; }

        /// <summary>
        ///         ''' Data di pagamento della penale
        ///         ''' </summary>
        public DateTime? dataPagamento { get; set; }

        /// <summary>
        ///         ''' Codice del titolo con cui è stata pagata la penale
        ///         ''' </summary>
        public string codiceTitoloPagante { get; set; } = null;
    }
}
