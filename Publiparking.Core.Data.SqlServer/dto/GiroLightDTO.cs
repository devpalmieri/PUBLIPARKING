using System;
using System.Collections.Generic;
using System.Text;

namespace Publiparking.Core.Data.SqlServer.dto
{
    public class GiroLightDTO
    {
        private Int32 m_id;
        /// <summary>
        ///         ''' Identificatore univoco del giro (chiave primaria).
        ///         ''' </summary>
        public Int32 id { get; set; }

        /// <summary>
        ///         ''' Descrizione testuale del giro.
        ///         ''' </summary>
        public string descrizione { get; set; }

        /// <summary>
        ///         ''' Data di ultima modifica del giro.
        ///         ''' </summary>
        ///         ''' <remarks>Questa data viene utilizzata dall'applicativo 'Mobile' per stabilire quando aggiornare la copia locale del giro</remarks>
        public DateTime ultimaModifica { get; set; }

        public override bool Equals(object obj)
        {
            bool risp = false;

            if (obj != null)
            {
                GiroLightDTO vGiro = (GiroLightDTO)obj;

                if (vGiro.id == this.id & vGiro.descrizione == this.descrizione & vGiro.ultimaModifica == this.ultimaModifica)
                    risp = true;
            }

            return risp;
        }
    }
}
