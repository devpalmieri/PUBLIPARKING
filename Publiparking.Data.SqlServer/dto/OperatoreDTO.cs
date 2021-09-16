using System;
using System.Collections.Generic;
using System.Text;

namespace Publiparking.Data.dto
{
    public class OperatoreDTO
    {
        /// <summary>
        ///         ''' Identificatore univoco dell'Operator (chiave primaria).
        ///         ''' </summary>
        public Int32 id { get; set; }

        /// <summary>
        ///         ''' UserName utilizzata dall'operatore per connettersi all'applicativo 'Mobile'.
        ///         ''' </summary>
        public string userName { get; set; }

        /// <summary>
        ///         ''' Password utilizzata dall'operatore
        ///         ''' </summary>
        public string hashPassword { get; set; }

        /// <summary>
        ///         ''' Cognome dell'operatore
        ///         ''' </summary>
        public string cognome { get; set; }

        /// <summary>
        ///         ''' Nome dell'operatore
        ///         ''' </summary>
        public string nome { get; set; }

        /// <summary>
        ///         ''' Data di ultimo cambi della password
        ///         ''' </summary>
        public DateTime CambioPassword { get; set; }

        /// <summary>
        ///         ''' Matricola dell'operatore
        ///         ''' </summary>
        public string matricola { get; set; }

        /// <summary>
        ///         ''' Numero di Operazioni consentite senza GPS dall'operatore
        ///         ''' </summary>
        ///         ''' <remarks>Inserire -1 per non obbligare l'uso del GPS</remarks>
        public Int32 noGpsOperazioni { get; set; }

        /// <summary>
        ///         ''' Stato della causale
        ///         ''' </summary>
        public bool attivo { get; set; }

        /// <summary>
        ///         ''' Operazione stallo singolo abilitata
        ///         ''' </summary>
        public bool stalloSingolo { get; set; } = true;

        /// <summary>
        ///         ''' Obbligo di inviare le foto
        ///         ''' </summary>
        public bool inviaFoto { get; set; } = false;

        /// <summary>
        ///         ''' Forzature del prossimo stallo da controllare.
        ///         ''' </summary>
        public Int32 idProssimoStallo { get; set; } = 0;
    }
}
