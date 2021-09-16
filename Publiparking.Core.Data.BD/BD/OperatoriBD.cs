using Publiparking.Core.Data.BD.Base;
using Publiparking.Core.Data.SqlServer;
using Publiparking.Core.Data.SqlServer.Entities;
using Publisoftware.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.BD.BD
{
    public class OperatoriBD : EntityBD<Operatori>
    {
        public OperatoriBD()
        {

        }

        public static Operatori IsAuthenticated(DbParkContext p_dbContext, string p_username, string p_password)
        {
            Operatori user = GetList(p_dbContext)
                                .Where(d => d.username.Equals(p_username) &&
                                      d.attivo == true)                                      
                                     .SingleOrDefault();

            if (user != null && CryptMD5.VerifyMd5Hash(p_password, user.password))
                return user;

            return null;
        }
        public static bool isAttivo(string pUserName, DbParkContext v_context)
        {
            bool risp = false;

            Operatori v_operatore = GetList(v_context).Where(o => o.username.Equals(pUserName))
                .OrderByDescending(o => o.idOperatore)
                .FirstOrDefault();
            if (v_operatore != null && v_operatore.attivo)
            {
                risp = true;
            }

            return risp;
        }

        public static bool verifyPassword(string pUserName, string pPassword, DbParkContext v_context)
        {
            bool risp = false;

            Operatori v_operatore = GetList(v_context).Where(o => o.username.Equals(pUserName))
                .OrderByDescending(o => o.idOperatore)
                .FirstOrDefault();
            if (v_operatore != null)
            {
                if (CryptMD5.VerifyMd5Hash(pPassword, v_operatore.password))
                {
                    risp = true;
                }
            }

            return risp;
        }
        public static bool changePassword(string pUserName, string pOldPassword, string pNewPassword, DbParkContext v_context)
        {
            bool risp = false;

            if (verifyPassword(pUserName, pOldPassword, v_context))
            {
                Operatori v_operatore = GetList(v_context).Where(o => o.username.Equals(pUserName))
                .OrderByDescending(o => o.idOperatore)
                .FirstOrDefault();

                string v_nuovapasswordCrypt = CryptMD5.getMD5(pNewPassword);

                v_operatore.password = v_nuovapasswordCrypt;
                v_operatore.dataCambioPassword = DateTime.Now;
                v_context.SaveChanges();
                risp = true;
            }

            return risp;
        }

        public static bool isExpiredPassword(string pUserName, int pnumGiorniValiditaPassword, DbParkContext v_context)
        {
            bool risp = false;

            Operatori v_operatore = GetList(v_context).Where(o => o.username.Equals(pUserName))
                .OrderByDescending(o => o.idOperatore)
                .FirstOrDefault();
            if (v_operatore != null)
            {
                if (v_operatore.dataCambioPassword.AddDays(pnumGiorniValiditaPassword) < DateTime.Now)
                {
                    risp = true;
                }
            }

            return risp;
        }   
    }
}
