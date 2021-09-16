using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.SqlServer.POCOExtended.Interfaces
{
    public interface ISoftDeleted
    {
        bool IsSoftDeletable { get; }
    }
}
