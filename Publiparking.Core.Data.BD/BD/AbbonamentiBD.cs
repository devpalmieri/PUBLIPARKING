using Publiparking.Core.Data.BD.Base;
using Publiparking.Core.Data.SqlServer;
using Publiparking.Core.Data.SqlServer.Entities;
using Publisoftware.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.BD
{
    public class AbbonamentiBD : EntityBD<Abbonamenti>
    {
        public AbbonamentiBD()
        {

        }
        public static Abbonamenti getAbbonamentoByCodice(string p_codice, DbParkContext p_dbContext)
        {
            return GetList(p_dbContext).Where(a => a.codice.Equals(p_codice)).SingleOrDefault();

        }

        public static bool attivaAbbonamento(int p_idAbbonamento, DbParkContext p_dbContext)
        {
            bool retval = false;
            Abbonamenti v_abbonamento = GetById(p_dbContext, p_idAbbonamento);
            if (v_abbonamento != null)
            {
                v_abbonamento.dataAbilitazione = DateTime.Now;
                retval = true;
            }
            return retval;
        }

        //public static decimal getCreditoResiduo(Abbonamenti p_Abbonamento, DbParkContext p_dbContext)
        //{
        //    decimal v_totaleRicarica = TranslogBD.getTotaleRicariche(p_Abbonamento.codice, p_dbContext);
        //    decimal v_totaleConsumiSMS = TitoliSMSBD.getTotaleConsumi(p_Abbonamento.idAbbonamento, p_dbContext);
        //    decimal v_totaleConsumiSMSTarga = TitoliSMSTargaBD.getTotaleConsumi(p_Abbonamento.idAbbonamento, p_dbContext);

        //    decimal v_credito_residuo = v_totaleRicarica - v_totaleConsumiSMS - v_totaleConsumiSMSTarga;
        //    if (v_credito_residuo < 0) { v_credito_residuo = 0; }

        //    return v_credito_residuo;
        //}

        public static bool existByCodice(string p_codice, DbParkContext p_dbContext)
        {
            return GetList(p_dbContext).Where(a => a.codice.Equals(p_codice)).Any() ? true : false;

        }

        public static string generaCodice(DbParkContext p_dbContext)
        {
            Random v_random = new Random();
            string risp = "";

            while ((risp.Length < 12 || existByCodice(risp, p_dbContext) || existByCodice(risp, p_dbContext)))
                risp = (v_random.NextDouble() * 1000000000000).ToString("0");

            return risp;
        }
    }

}

