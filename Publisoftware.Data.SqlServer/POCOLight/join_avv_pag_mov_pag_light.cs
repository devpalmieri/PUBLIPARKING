using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class join_avv_pag_mov_pag_light : BaseEntity<join_avv_pag_mov_pag_light>
    {
        public int id_avv_pag_mov_pag { get; set; }
        public int id_mov_pag { get; set; }
        public string importo_pagato_Euro { get; set; }
        public string data_oper_acc_String { get; set; }
        public string AvvisoPagato { get; set; }
        public string AvvisoAccreditato { get; set; }
        public string NumeroCC { get; set; }
        public string nome_file { get; set; }
        public string soggettoDebitore { get; set; }
        public string ContribuenteAvvPag { get; set; }
        public string Contabilizzato { get; set; }
        public string importo_mov_pagato_Euro { get; set; }
    }
}
