using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public class DataExceptionHelpers
    {
        public static string DbEntityValidationExceptionToString(DbEntityValidationException dbEx)
        {
            if (dbEx != null)
            {
                IEnumerable<string> dbErrors = dbEx.EntityValidationErrors.Select(e =>
                                        String.Join(Environment.NewLine, e.ValidationErrors.Select(v => $"{v.PropertyName}: {v.ErrorMessage}"))
                                        );
                return String.Join(Environment.NewLine, dbErrors);
            }
            else
            {
                // forse è meglio non lanciare eccezioni in questo metodo
                return String.Empty;
            }
        }
    }
}
