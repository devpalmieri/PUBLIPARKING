using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_ispezioni_coattivo_new_light : BaseEntity<tab_ispezioni_coattivo_new_light>
    {
        public int id_tab_ispezione_coattivo { get; set; }
        public string tipoRelazione { get; set; }
        public string impMorosita { get; set; }
        public string esitoIsp { get; set; }
        public string nominativo { get; set; }
        public string cfiscale_piva_soggetto_ispezione { get; set; }
        public string siglaProvincia_soggetto_ispezione { get; set; }
        public string impMorositaFermo { get; set; }
        public string impMorositaIpoteca { get; set; }
        public string impMorositaAssoggettabileAttiEsecutivi { get; set; }
        public string data_rilevazione_morosita_String { get; set; }
        public string oggettoGroupBy { get; set; }
        public string fineIsp { get; set; }
        public string supervisione { get; set; }
        public string stato { get; set; }
        public string attoEmesso { get; set; }
        public string risorsaSupervisione { get; set; }
        public string nominativoDiplayContribuente { get; set; }
        public string codFiscalePivaDisplayContribuente { get; set; }
        public string statoContribuenteTotaleContribuente { get; set; }
    }
}
