using Publisoftware.Data.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.BD
{
    public class AbbonamentiBD : EntityBD<Abbonamenti>
    {
        public AbbonamentiBD()
        {

        }

        public static Abbonamenti getAbbonamentoByCodice(string p_codice, DbParkCtx p_context)
        {
            return GetList(p_context).Where(a => a.codice.Equals(p_codice)).SingleOrDefault();

        }

        public static bool attivaAbbonamento(int p_idAbbonamento, DbParkCtx p_context)
        {
            bool retval = false;
            Abbonamenti v_abbonamento = GetById(p_idAbbonamento, p_context);
            if (v_abbonamento != null)
            {
                v_abbonamento.dataAbilitazione = DateTime.Now;
                retval = true;
            }
            return retval;
        }

        public static decimal getCreditoResiduo(Abbonamenti p_Abbonamento, DbParkCtx p_context)
        {
            decimal v_totaleRicarica = TranslogBD.getTotaleRicariche(p_Abbonamento.codice, p_context);
            decimal v_totaleConsumiSMS = TitoliSMSBD.getTotaleConsumi(p_Abbonamento.idAbbonamento, p_context);
            decimal v_totaleConsumiSMSTarga = TitoliSMSTargaBD.getTotaleConsumi(p_Abbonamento.idAbbonamento, p_context);

            decimal v_credito_residuo = v_totaleRicarica - v_totaleConsumiSMS - v_totaleConsumiSMSTarga;
            if (v_credito_residuo < 0) { v_credito_residuo = 0; }

            return v_credito_residuo;
        }

        public static bool existByCodice(string p_codice, DbParkCtx p_context)
        {
            return GetList(p_context).Where(a => a.codice.Equals(p_codice)).Any() ? true : false;

        }

        public static string generaCodice(DbParkCtx p_context)
        {
            Random v_random = new Random();
            string risp = "";

            while ((risp.Length < 12 || existByCodice(risp, p_context) || existByCodice(risp, p_context)))
                risp = (v_random.NextDouble() * 1000000000000).ToString("0");

            return risp;
        }
    }
}
