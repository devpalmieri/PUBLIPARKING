using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_mov_pag_light : BaseEntity<tab_mov_pag_light>
    {
        public int id_tab_mov_pag { get; set; }
        public string DataOperazione { get; set; }
        public string DataAccredito { get; set; }
        public string DataValuta { get; set; }
        public string Importo { get; set; }
        public string ImportoAccoppiato { get; set; }
        public string ImportoDaAccoppiare { get; set; }
        public string TipoPagamento { get; set; }
        public string CodStato { get; set; }
        public string Stato { get; set; }
        public string MessaggioStato { get; set; }
        public string nome_file { get; set; }
        public string TipoPulsante { get; set; }
        public string Contribuente { get; set; }
        public decimal? id_contribuente { get; set; }
        public bool AvvisiPagatiVisibile { get; set; }
        public string causale_pagamento { get; set; }
        public string iuv { get; set; }
        public int ActionType { get; set; }
        public string RisorsaAssegnazione { get; set; }
        public string CfPIVAContribuente { get; set; }
        public string CfPIVAPagante { get; set; }
        public string CodTipoPagamento { get; set; }
        public string DescrizioneModalitaPagamento { get; set; }
    }
}
