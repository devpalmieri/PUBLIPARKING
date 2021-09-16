﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Publiparking.Core.Data.SqlServer.dto
{
    public class TitoloAbbonamentoDTO
    {
        /// <summary>
        ///         ''' Identificatore univoco del titolo (chiave primaria).
        ///         ''' </summary>
        public Int32 id { get; set; }

        /// <summary>
        ///         ''' Codice del titolo.
        ///         ''' </summary>
        public string codice { get; set; }

        /// <summary>
        ///         ''' Targa pagata.
        ///         ''' </summary>
        public string targa { get; set; }

        /// <summary>
        ///         ''' Data di pagamento del titolo.
        ///         ''' </summary>
        public DateTime dataPagamento { get; set; }

        /// <summary>
        ///         ''' Data di scadenza del titolo.
        ///         ''' </summary>
        public DateTime scadenza { get; set; }

        /// <summary>
        ///         ''' idstallo.
        /// 
        public Int32 idStallo { get; set; }

        /// <summary>
        ///         ''' Importo del titolo.
        ///         ''' </summary>
        public decimal importo { get; set; }

        public Int32 idabbonamentoperiodico { get; set; }

        public Int32 idtariffaabbonamento { get; set; }
    }
}