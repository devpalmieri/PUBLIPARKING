using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Data
{
    public partial class DbParkCtx
    {
        //bool _isReadOnly = false;       
        public DbParkCtx(string connectionString) : base(connectionString)
        {

        }
        // // var conn = System.Data.Common.DbProviderFactories.GetFactory("System.Data.EntityClient").CreateConnection();
        // // conn.ConnectionString = DbParkConnectionString;
        // public DbParkCtx(System.Data.Common.DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        // {
        // }
    }
}
