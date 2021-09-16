using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Data.SqlServer
{
    public class EFConnectionStringBuilder
    {
        private static string GetSqlConnectionString()
        {
            SqlConnectionStringBuilder providerCs = new SqlConnectionStringBuilder();

            providerCs.DataSource = "10.1.1.110";
            providerCs.InitialCatalog = "db_park_meta";
            // providerCs.IntegratedSecurity = true;
            providerCs.PersistSecurityInfo = true;
            providerCs.UserID = "sa";
            providerCs.Password = "Admin1234";

            var csBuilder = new EntityConnectionStringBuilder();

            csBuilder.Provider = "System.Data.SqlClient";
            csBuilder.ProviderConnectionString = providerCs.ToString();

            csBuilder.Metadata = string.Format("res://{0}/DbParkCtx.csdl|res://{0}/DbParkCtx.ssdl|res://{0}/DbParkCtx.msl",
                typeof(DbParkCtx).Assembly.FullName);

            return csBuilder.ToString();
        }

        public string ConnectionString { get; set; }

        public EFConnectionStringBuilder()
        {
            ConnectionString = GetSqlConnectionString();
        }
    }
}
