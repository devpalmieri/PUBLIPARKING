using Publisoftware.Utility;
using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD.Helper
{
    public class DbHelper
    {
        public DbHelper()
        {

        }
        private DBInfos getDbSettorialeByCodPagoPaInfo(string p_server, string p_dbName, string p_userName, string p_password, string cod_pagopa)
        {
            SqlDataReader rdr = null;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.InitialCatalog = p_dbName;
            builder.DataSource = p_server;
            builder.UserID = p_userName;
            builder.Password = CryptMD5.Decrypt(p_password); ;

            SqlConnection conn = new SqlConnection(builder.ConnectionString);
            anagrafica_ente e;
            SqlCommand cmd = new SqlCommand("select nome_db,indirizzo_ip_db,user_name_db,password_db from anagrafica_ente where flag_on_off='1' and codice_ente_pagopa like '%" + cod_pagopa + "%' " +
                                            "and nome_db is not null and indirizzo_ip_db is not null and user_name_db is not null and password_db is not null", conn);

            DBInfos v_Ente = null;

            try
            {
                conn.Open();
                rdr = cmd.ExecuteReader(CommandBehavior.SingleResult);

                while (rdr.Read())
                {
                    string v_nomeDb = (string)rdr["nome_db"];
                    string v_serverDb = (string)rdr["indirizzo_ip_db"];
                    string v_userNameDb = (string)rdr["user_name_db"];
                    string v_passwordDb = (string)rdr["password_db"]; //la password prelevata da anagrafica_ente è criptata
                    DBInfos db_info = new DBInfos(v_serverDb, v_nomeDb, v_userNameDb, v_passwordDb);

                    if (!string.IsNullOrEmpty(db_info.Password))
                    {
                        v_Ente = db_info;
                    }
                    else
                    {
                        LoggerFactory.getInstance().getLogger<NLogger>(this).LogMessage(String.Format("Database {0} con password non corretta", db_info.DbName), EnLogSeverity.Info);
                    }
                }

            }
            catch (Exception exception)
            {

            }

            return v_Ente;
        }
        private DBInfos getDbSettorialeByPIVAInfo(string p_server, string p_dbName, string p_userName, string p_password, string p_piva)
        {
            SqlDataReader rdr = null;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.InitialCatalog = p_dbName;
            builder.DataSource = p_server;
            builder.UserID = p_userName;
            builder.Password = CryptMD5.Decrypt(p_password); ;

            SqlConnection conn = new SqlConnection(builder.ConnectionString);

            //SqlCommand cmd = new SqlCommand("select nome_db,indirizzo_ip_db,user_name_db,password_db from anagrafica_ente where flag_on_off='1' and p_iva like '%" + p_piva + "%' " +
            //                                "and nome_db is not null and indirizzo_ip_db is not null and user_name_db is not null and password_db is not null", conn);
            string sqlCommand = "select nome_db,indirizzo_ip_db,user_name_db,password_db from anagrafica_ente where flag_on_off='1' and ";
            sqlCommand += "((cod_fiscale is not null and cod_fiscale like '%{0}%') OR (p_iva is not null and p_iva like '%{0}%'))  and ";
            sqlCommand += "nome_db is not null and indirizzo_ip_db is not null and user_name_db is not null and password_db is not null";
            SqlCommand cmd = new SqlCommand(string.Format(sqlCommand, p_piva), conn);

            DBInfos v_Ente = null;

            try
            {
                conn.Open();
                rdr = cmd.ExecuteReader(CommandBehavior.SingleResult);

                while (rdr.Read())
                {
                    string v_nomeDb = (string)rdr["nome_db"];
                    string v_serverDb = (string)rdr["indirizzo_ip_db"];
                    string v_userNameDb = (string)rdr["user_name_db"];
                    string v_passwordDb = (string)rdr["password_db"]; //la password prelevata da anagrafica_ente è criptata
                    DBInfos db_info = new DBInfos(v_serverDb, v_nomeDb, v_userNameDb, v_passwordDb);

                    if (!string.IsNullOrEmpty(db_info.Password))
                    {
                        v_Ente = db_info;
                    }
                    else
                    {
                        LoggerFactory.getInstance().getLogger<NLogger>(this).LogMessage(String.Format("Database {0} con password non corretta", db_info.DbName), EnLogSeverity.Info);
                    }
                }

            }
            catch (Exception exception)
            {

            }

            return v_Ente;
        }
        /// <summary>
        /// Metodo modificato il 15/06/2021
        ///il context veniva generato in modo errato
        /// </summary>
        /// <param name="p_server"></param>
        /// <param name="p_dbName"></param>
        /// <param name="p_userName"></param>
        /// <param name="p_password"></param>
        /// <param name="p_piva"></param>
        /// <param name="p_idStruttura"></param>
        /// <param name="p_idRisorsa"></param>
        /// <returns></returns>
        public dbEnte getEnteContextPIVA_old(string p_server, string p_dbName, string p_userName, string p_password, string p_piva, int p_idStruttura = anagrafica_strutture_aziendali.STRUTTURA_ADMIN, int p_idRisorsa = anagrafica_risorse.RISORSA_ADMIN)
        {

            DBInfos dBInfo = getDbSettorialeByPIVAInfo(p_server, p_dbName, p_userName, p_password, p_piva);
            if (dBInfo != null)
            {
                return dBInfo.GetCtx(p_idStruttura, p_idRisorsa);
            }
            else
            {
                LoggerFactory.getInstance().getLogger<NLogger>(this).LogMessage("Database non trovato", EnLogSeverity.Info);
                return null;
            }

        }
        public dbEnte getEnteContextPIVA(string p_server, string p_dbName, string p_userName, string p_password, string p_piva, int p_idStruttura = anagrafica_strutture_aziendali.STRUTTURA_ADMIN, int p_idRisorsa = anagrafica_risorse.RISORSA_ADMIN)
        {

            DBInfos dBInfo = getDbSettorialeByPIVAInfo(p_server, p_dbName, p_userName, p_password, p_piva);
            if (dBInfo != null)
            {
                dbEnte ctx_settoriale = EnteContextFactory.getContext(p_server, dBInfo.DbName, dBInfo.UserName, dBInfo.Password, p_idStruttura, p_idRisorsa);
                if (ctx_settoriale != null)
                    return ctx_settoriale;
                else
                {
                    LoggerFactory.getInstance().getLogger<NLogger>(this).LogMessage("Context del settoriale non trovato.", EnLogSeverity.Info);
                    return null;
                }
            }
            else
            {
                LoggerFactory.getInstance().getLogger<NLogger>(this).LogMessage("Database non trovato.", EnLogSeverity.Info);
                return null;
            }

        }
        /// <summary>
        /// Metodo modificato il 15/06/2021
        ///il context veniva generato in modo errato
        ///il context veniva generato in modo errato
        /// </summary>
        /// <param name="p_server"></param>
        /// <param name="p_dbName"></param>
        /// <param name="p_userName"></param>
        /// <param name="p_password"></param>
        /// <param name="cod_pagopa"></param>
        /// <param name="p_idStruttura"></param>
        /// <param name="p_idRisorsa"></param>
        /// <returns></returns>
        public dbEnte getEnteContextCodPagoPA_Old(string p_server, string p_dbName, string p_userName, string p_password, string cod_pagopa, int p_idStruttura = anagrafica_strutture_aziendali.STRUTTURA_ADMIN, int p_idRisorsa = anagrafica_risorse.RISORSA_ADMIN)
        {

            DBInfos dBInfo = getDbSettorialeByCodPagoPaInfo(p_server, p_dbName, p_userName, p_password, cod_pagopa);
            if (dBInfo != null)
            {
                return dBInfo.GetCtx(p_idStruttura, p_idRisorsa);
            }
            else
            {
                LoggerFactory.getInstance().getLogger<NLogger>(this).LogMessage("Database non trovato", EnLogSeverity.Info);
                return null;
            }

        }
        public dbEnte getEnteContextCodPagoPA(string p_server, string p_dbName, string p_userName, string p_password, string cod_pagopa, int p_idStruttura = anagrafica_strutture_aziendali.STRUTTURA_ADMIN, int p_idRisorsa = anagrafica_risorse.RISORSA_ADMIN)
        {

            DBInfos dBInfo = getDbSettorialeByCodPagoPaInfo(p_server, p_dbName, p_userName, p_password, cod_pagopa);
            if (dBInfo != null)
            {
                dbEnte ctx_settoriale = EnteContextFactory.getContext(p_server, dBInfo.DbName, dBInfo.UserName, dBInfo.Password, p_idStruttura, p_idRisorsa);
                if (ctx_settoriale != null)
                    return ctx_settoriale;
                else
                {
                    LoggerFactory.getInstance().getLogger<NLogger>(this).LogMessage("Context del settoriale non trovato.", EnLogSeverity.Info);
                    return null;
                }
            }
            else
            {
                LoggerFactory.getInstance().getLogger<NLogger>(this).LogMessage("Database non trovato", EnLogSeverity.Info);
                return null;
            }

        }

    }
}
