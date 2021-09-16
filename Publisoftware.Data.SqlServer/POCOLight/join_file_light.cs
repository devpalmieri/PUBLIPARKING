using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class join_file_light : BaseEntity<join_file_light>
    {
        public int id_join_file { get; set; }
        public string nome_file { get; set; }
        public int id_riferimento { get; set; }
    }
}
