using Publiparking.Core.Data.BD.Base;
using Publiparking.Core.Data.SqlServer;
using Publiparking.Core.Data.SqlServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.BD
{
    public class TabRegistroCookieBD : EntityBD<tab_registro_cookie>
    {
        public TabRegistroCookieBD()
        {

        }
        public static bool SaveCookieUser(tab_registro_cookie v_cookie, DbParkContext p_dbContext, out int p_id_rec_cookie)
        {
            p_id_rec_cookie = -1;

            try
            {
                if (v_cookie == null)
                {
                    return false;
                }
                p_dbContext.tab_registro_cookie.Add(v_cookie);
                p_dbContext.SaveChanges();

                p_id_rec_cookie = v_cookie.id_tab_registro_cookie;
                return true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                p_id_rec_cookie = -1;
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("L'Entità di tipo \"{0}\" in stato \"{1}\" presenta i seguenti errori di validazione:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Proprietà: \"{0}\", Errore: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                p_id_rec_cookie = -1;
                return false;
            }
        }

        public static tab_registro_cookie GetCookieByIdSessione(string p_idSessione, DbParkContext p_dbContext)
        {

            return GetList(p_dbContext).Where(d => d.session_id.TrimEnd().Trim().TrimStart() == p_idSessione).FirstOrDefault();
        }
        public static tab_registro_cookie GetCookieByIPAddress(string p_ipaddress, DbParkContext p_dbContext)
        {

            return GetList(p_dbContext).Where(d => d.indirizzo_ip.TrimEnd().Trim().TrimStart() == p_ipaddress).FirstOrDefault();
        }
    }
}
