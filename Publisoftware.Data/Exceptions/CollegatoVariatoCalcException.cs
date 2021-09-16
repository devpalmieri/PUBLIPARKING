using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD.Exceptions
{
    [Serializable]
    public class CollegatoVariatoCalcException : Exception
    {
        public CollegatoVariatoCalcException()
        {
        }

        public CollegatoVariatoCalcException(string message) : base(message)
        {
        }

        public CollegatoVariatoCalcException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
