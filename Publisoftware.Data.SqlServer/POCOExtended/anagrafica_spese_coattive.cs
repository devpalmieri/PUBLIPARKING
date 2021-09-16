using Publisoftware.Data.CustomValidationAttrs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_spese_coattive.Metadata))]
    public partial class anagrafica_spese_coattive : ISoftDeleted
    {
        [DisplayName("Da")]
        public string periodoDa_String
        {
            get
            {
                return this.periodo_rif_da.ToShortDateString();
            }
            set
            {
                this.periodo_rif_da = DateTime.Parse(value);
            }
        }
        [DisplayName("A")]
        public string periodoA_String
        {
            get 
            { 
                return this.periodo_rif_a.ToShortDateString(); 
            }
            set
            {
                this.periodo_rif_a = DateTime.Parse(value);
            }
        }

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Imp. Unitario")]
            public decimal imp_unitario_spese_coattive { get; set; }

            [DisplayName("Aliquota Base")]
            public decimal aliquota_base_spese_coattive { get; set; }

            [DisplayName("Q.ta Base Da")]
            public decimal quantita_base_spese_coattive_da { get; set; }

            [DisplayName("Q.ta Base A")]
            public decimal quantita_base_spese_coattive_a { get; set; }

            [DisplayName("Ente")]
            public int id_ente { get; set; }
            [DisplayName("Entrata")]
            public int id_entrata { get; set; }

            [DisplayName("Periodo Rif. Da")]
            public System.DateTime periodo_rif_da { get; set; }
            [DisplayName("Periodo Rif. A")]
            [IsDateAfter("periodo_rif_da", false, ErrorMessage="Data Fine antecedente la data Inizio")]
            public System.DateTime periodo_rif_a { get; set; }

            [DisplayName("Voce Contribuzione")]
            public int id_anag_voce_spese_coattive { get; set; }
            [DisplayName("Tipo Voce Contribuzione")]
            public int id_tipo_voce_spese_coattive { get; set; }


        }
    }
}
