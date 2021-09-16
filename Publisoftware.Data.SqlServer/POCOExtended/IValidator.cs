using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public interface IValidator
    {
        bool IsValid { get; }
    }
}
