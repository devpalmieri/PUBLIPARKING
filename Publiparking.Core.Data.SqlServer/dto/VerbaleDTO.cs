using System;
using System.Collections.Generic;
using System.Text;

namespace Publiparking.Core.Data.SqlServer.dto
{
    public class VerbaleDTO : VerbaleLightDTO
    {

        /// <summary>
        ///         ''' Elenco delle foto associate al verbale
        ///         ''' </summary>
        public List<string> foto { get; set; } = new List<string>();
        public List<int> codiciViolati { get; set; } = new List<int>();
        //private IdCollection m_codiciViolati = new IdCollection();
        ///// <summary>
        /////         ''' Elenco dei codici violati
        /////         ''' </summary>
        //public IdCollection codiciViolati
        //{
        //    get
        //    {
        //        return m_codiciViolati;
        //    }
        //    set
        //    {
        //        // In fase di modifica, riaggiorna il totale del verbale
        //        m_codiciViolati = value;

        //        decimal vTotale = 0;

        //        foreach (var vIdCausale in value)
        //        {
        //            CausaleDTO vCausale = CausaleDAO.loadById(vIdCausale);

        //            if (vCausale != null)
        //                vTotale = vTotale + vCausale.importo;
        //        }

        //        this.totale = vTotale;
        //    }
        //}
    }
}
