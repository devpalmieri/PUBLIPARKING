using Publiparking.Data.BD;
using Publisoftware.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Utility.Log;

namespace Publiparking.Service.Base
{
    public class AdoHelper
    {
        public AdoHelper()
        {

        }

        public List<DBInfos> getDbSettorialiParkInfo(string p_server, string p_dbName, string p_userName, string p_password)
        {           
            SqlDataReader rdr = null;
            SqlConnectionStringBuilder builder =new SqlConnectionStringBuilder();
            builder.InitialCatalog = p_dbName;
            builder.DataSource = p_server;
            builder.UserID = p_userName;
            builder.Password = CryptMD5.Decrypt(p_password); ;

            SqlConnection conn = new SqlConnection(builder.ConnectionString);

            SqlCommand cmd = new SqlCommand("select nome_db,indirizzo_ip_db,user_name_db,password_db from anagrafica_ente where flag_sosta = 1 " +
                                            "and nome_db is not null and indirizzo_ip_db is not null and user_name_db is not null and password_db is not null", conn);

            List<DBInfos> v_Entilist = new List<DBInfos>();
            DBInfos db_info = null;
            try
            {               
                conn.Open();
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    string v_nomeDb = (string)rdr["nome_db"];
                    string v_serverDb = (string)rdr["indirizzo_ip_db"];
                    string v_userNameDb = (string)rdr["user_name_db"];
                    string v_passwordDb = (string)rdr["password_db"]; //la password prelevata da anagrafica_ente è criptata
                    db_info = new DBInfos(v_serverDb, v_nomeDb, v_userNameDb, v_passwordDb);

                    if (!string.IsNullOrEmpty(db_info.PasswordInChiaro))
                    {
                        v_Entilist.Add(db_info);
                    }
                    else
                    {
                        LoggerFactory.getInstance().getLogger<NLogger>(this).LogMessage(String.Format("Database {0} con password non corretta", db_info.DbName), EnLogSeverity.Info);
                    }
                }
               
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return v_Entilist;
        }

    }
}
