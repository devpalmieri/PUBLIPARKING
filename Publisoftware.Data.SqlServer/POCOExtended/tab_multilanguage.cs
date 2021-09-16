using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_multilanguage
    {

        private Dictionary<string, string> _dictionaryValue = null;
        public Dictionary<string, string> dictionaryValue()
        {
            if (_dictionaryValue == null)
            {
                _dictionaryValue = new Dictionary<string, string>();

                string[] elenco = this.valore.Split(new string[] { ";;" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string coppia in elenco)
                {
                    string[] rigo = coppia.Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries);
                    _dictionaryValue.Add(rigo[0], rigo[1]);
                }

            }

            return _dictionaryValue;
        }
    }
}
