using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabRilevatoriBD: EntityBD<tab_rilevatore>
    {
        public TabRilevatoriBD()
        {

        }

        public static bool verifyPassword(string p_username, string p_password, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(r => r.nome_utente != null && r.nome_utente == p_username && r.password == p_password).Count() == 1;
        }

        public static tab_rilevatore getByNomeUtente(string p_username, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(r => r.nome_utente == p_username).SingleOrDefault();
        }
    }
}
