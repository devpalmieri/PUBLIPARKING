using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_allegati_digitali_light
    {
        public string contatoreFile { get; set; }
        public int id_tab_allegati_digitali { get; set; }
        public string nome_file { get; set; }
        public string formato_file { get; set; }
        public string path_file { get; set; }
        public string data_creazione_String { get; set; }
        public DateTime data_creazione { get; set; }        
    }
}
