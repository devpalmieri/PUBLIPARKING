using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_unita_contribuzione_light : BaseEntity<tab_unita_contribuzione_light>
    {
        public int? id_unita_contribuzione { get; set; }
        public string anno_rif { get; set; }
        public string periodo_contribuzione_da_String { get; set; }
        public string periodo_contribuzione_a_String { get; set; }
        public string periodo_contribuzione_String { get; set; }
        public string periodo_rif_da_String { get; set; }
        public string periodo_rif_a_String { get; set; }
        public string periodo_rif_String { get; set; }
        public string um_unita { get; set; }
        public string QuantitaUnitaContribuzione { get; set; }
        public decimal? quantita_unita_contribuzione { get; set; }
        public string importo_unitario_contribuzione_Euro { get; set; }
        public decimal importo_unita_contribuzione { get; set; }
        public string importo_unita_contribuzione_Euro { get; set; }
        public string SommatoriaAgevolazioni_Euro { get; set; }
        public string SommatoriaAgevolazioni { get; set; }
        public string Ubicazione { get; set; }
        public string DescrizioneVoceContribuzione { get; set; }
        public string descrizioneCategoria { get; set; }
        public string codiceCategoria { get; set; }
        public string PercentualePossesso { get; set; }
        public string numTotOccupanti { get; set; }
        public string descrizioneUtilizzo { get; set; }
        public string descrizioneDiritto { get; set; }
        public string Rendita { get; set; }
        public string OggettoDes { get; set; }
        public bool OggettoUbicazione { get; set; }
        public int? id_avv_pag_collegato { get; set; }
        public int? id_avv_pag_generato { get; set; }
        public int? id_unita_contribuzione_collegato { get; set; }
        public bool isUnitaSemplice { get; set; }
        public int? num_riga_avv_pag_generato { get; set; }
        public DateTime? periodo_contribuzione_da { get; set; }
        public Decimal? id_oggetto_contribuzione { get; set; }
        public Decimal? id_oggetto { get; set; }
        public int id_entrata { get; set; }
        public int? id_anagrafica_voce_contribuzione { get; set; }
        public string codice_tributo_ministeriale { get; set; }
        public int id_tab_macroentrate { get; set; }
        public string flag_segno { get; set; }
        public string flag_tipo_composto { get; set; }
        public string color { get; set; }
        public string oggettoGroupBy { get; set; }
        public decimal importo_sgravio { get; set; }
        public string importo_sgravio_Euro { get; set; }
        public decimal importo_sgravato { get; set; }
        public string importo_sgravato_Euro { get; set; }
        public bool IsAvvisoStatoAnnRetDanDar { get; set; }
        public bool IsAvvisoStatoAnnDan { get; set; }
        public bool IsAttoSuccessivo { get; set; }
        public string AttoSuccessivo { get; set; }
        public string statoAvvisoCollegato { get; set; }
        public string flag_natura_avv_collegati { get; set; }
        public string cod_stato { get; set; }
        public string AvvisoCollegato { get; set; }
        public string AvvisoCollegatoStato { get; set; }
        public decimal? importo_collegato { get; set; }
        public string importo_collegato_Euro { get; set; }
        public string importo_collegato_nuovo_Euro { get; set; }
        public string importo_unita_contribuzione_nuova_Euro { get; set; }
        public bool IsAttoComposto { get; set; }
        public string imponibile_unita_contribuzione_Euro { get; set; }
        public string iva { get; set; }
        public bool IsBottoneAvvisoCollegatoVisibile { get; set; }
        public string affissione { get; set; }
    }
}
