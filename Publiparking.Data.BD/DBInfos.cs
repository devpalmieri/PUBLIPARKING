using Publisoftware.Data;
using Publisoftware.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Publiparking.Data.BD
{
    public class DBInfos
    {
        public DBInfos(string p_server,string p_dbName,string p_userName,string p_password)
        {
            Server = p_server;
            DbName = p_dbName;
            UserName = p_userName;          
            Password = p_password;

            try
            {
                PasswordInChiaro = CryptMD5.Decrypt(p_password); ;
            }
            catch (Exception)
            {
                PasswordInChiaro = "";
            }
        }

        public string Server { get; set; }
        public string DbName { get; set; }
        public string UserName { get; set; }
       

        public string Password { get;  }

        public string PasswordInChiaro { get; }

        public DbParkCtx GetParkCtx(bool p_pooling = true)
        {
            try
            {
                return DbParkContextFactory.getContext(this.Server, this.DbName, this.UserName, this.PasswordInChiaro, -1, p_pooling);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public dbEnte GetEnteCtx()
        {
            try
            {
                //utilizzo struttura e risorsa di default
                return EnteContextFactory.getContext(this.Server, this.DbName, this.UserName, this.PasswordInChiaro,99,2037);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public void DisposeEntectx(dbEnte ctx)
        {
            ctx.Dispose();
        }

        public void DisposeParkctx(DbParkCtx ctx)
        {
            ctx.Dispose();
        }


    }
}
