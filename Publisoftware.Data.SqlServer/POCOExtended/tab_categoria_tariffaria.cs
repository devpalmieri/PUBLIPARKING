using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_categoria_tariffaria.Metadata))]
    public partial class tab_categoria_tariffaria : ISoftDeleted
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public string Descrizione_categoria
        {
            get
            {
                return this.anagrafica_categoria.des_cat_contr;
            }
        }

        public string TariffaFissa
        {
            get
            {
                return String.Format("{0:0.00000}", imp_unitario_base_std) + " €/" + um_base_std;
            }
        }

        public string TariffaVariabile
        {
            get
            {
                return String.Format("{0:0.00000}", imp_annuo_quota_fissa_fascia1) + " €/" + um_base_std;
            }
        }

        public string QuantitaBase
        {
            get
            {
                if (flag_impegno_base_std == "0" || flag_impegno_base_std == null)
                {
                    return QuantitaAgevolata;
                }
                else
                {
                    return String.Format("{0:0.00000}", quantita_base_std.HasValue ? quantita_base_std.Value : 0) + " mc";
                }
            }
        }

        public string ImportoUnitarioBase
        {
            get
            {
                if (flag_impegno_base_std == "0" || flag_impegno_base_std == null)
                {
                    return ImportoUnitarioAgevolato;
                }
                else
                {
                    return String.Format("{0:0.00000}", imp_unitario_base_std) + " €/mc";
                }
            }
        }

        public string QuantitaAgevolata
        {
            get
            {
                return String.Format("{0:0.00000}", quantita_agevolata.HasValue ? quantita_agevolata.Value : 0) + " mc";
            }
        }

        public string ImportoUnitarioAgevolato
        {
            get
            {
                return String.Format("{0:0.00000}", imp_unitario_agevolata.HasValue ? imp_unitario_agevolata.Value : 0) + " €/mc";
            }
        }

        public bool isEssenzialeVisible
        {
            get
            {
                return !(flag_essenziali == "0" || flag_essenziali == null);
            }
        }

        public string QuantitaAliquotaEssenziale
        {
            get
            {
                if (isEssenzialeVisible)
                {
                    if (tipo_essenziali == "1")
                    {
                        return String.Format("{0:0.00000}", quantita_essenziali.HasValue ? quantita_essenziali.Value : 0) + " mc";
                    }
                    else
                    {
                        return String.Format("{0:0.00000}", quantita_essenziali.HasValue ? aliquota_essenziali.Value : 0) + " %";
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string ImportoUnitarioEssenziale
        {
            get
            {
                if (isEssenzialeVisible)
                {
                    return String.Format("{0:0.00000}", imp_unitario_essenziali.HasValue ? imp_unitario_essenziali.Value : 0) + " €/mc";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public bool isPrimaEccedenzaVisible
        {
            get
            {
                return !(tipo_ecc1_std == "0" || tipo_ecc1_std == null);
            }
        }

        public string QuantitaAliquotaPrimaEccedenza
        {
            get
            {
                if (isPrimaEccedenzaVisible)
                {
                    if (tipo_quantita_ecc1_std == "1")
                    {
                        return String.Format("{0:0.00000}", quantita_ecc1_std.HasValue ? quantita_ecc1_std.Value : 0) + " mc";
                    }
                    else
                    {
                        return String.Format("{0:0.00000}", aliquota_ecc1_std.HasValue ? aliquota_ecc1_std.Value : 0) + " %";
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string ImportoUnitarioPrimaEccedenza
        {
            get
            {
                if (isPrimaEccedenzaVisible)
                {
                    return String.Format("{0:0.00000}", imp_unitario_ecc1_std.HasValue ? imp_unitario_ecc1_std.Value : 0) + " €/mc";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public bool isSecondaEccedenzaVisible
        {
            get
            {
                return !(tipo_ecc2_std == "0" || tipo_ecc2_std == null);
            }
        }

        public string QuantitaAliquotaSecondaEccedenza
        {
            get
            {
                if (isSecondaEccedenzaVisible)
                {
                    if (tipo_quantita_ecc2_std == "1")
                    {
                        return String.Format("{0:0.00000}", quantita_ecc2_std.HasValue ? quantita_ecc2_std.Value : 0) + " mc";
                    }
                    else
                    {
                        return String.Format("{0:0.00000}", aliquota_ecc2_std.HasValue ? aliquota_ecc2_std.Value : 0) + " %";
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string ImportoUnitarioSecondaEccedenza
        {
            get
            {
                if (isSecondaEccedenzaVisible)
                {
                    return String.Format("{0:0.00000}", imp_unitario_ecc2_std.HasValue ? imp_unitario_ecc2_std.Value : 0) + " €/mc";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public bool isTerzaEccedenzaVisible
        {
            get
            {
                return !(tipo_ecc3_std == "0" || tipo_ecc3_std == null);
            }
        }

        public string QuantitaAliquotaTerzaEccedenza
        {
            get
            {
                if (isTerzaEccedenzaVisible)
                {
                    if (tipo_quantita_ecc3_std == "1")
                    {
                        return String.Format("{0:0.00000}", quantita_ecc3_std.HasValue ? quantita_ecc3_std.Value : 0) + " mc";
                    }
                    else
                    {
                        return String.Format("{0:0.00000}", aliquota_ecc3_std.HasValue ? aliquota_ecc3_std.Value : 0) + " %";
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string ImportoUnitarioTerzaEccedenza
        {
            get
            {
                if (isTerzaEccedenzaVisible)
                {
                    return String.Format("{0:0.00000}", imp_unitario_ecc3_std.HasValue ? imp_unitario_ecc3_std.Value : 0) + " €/mc";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string QuotaFissaPrimaFascia
        {
            get
            {
                return String.Format("{0:0.00000}", quota_fissa_fascia1.HasValue ? quota_fissa_fascia1.Value : 0) + " mc";
            }
        }

        public string ImportoAnnuoQuotaFissaPrimaFascia
        {
            get
            {
                return String.Format("{0:0.00000}", imp_annuo_quota_fissa_fascia1.HasValue ? imp_annuo_quota_fissa_fascia1.Value : 0) + " €";
            }
        }

        public string QuotaFissaSecondaFascia
        {
            get
            {
                return String.Format("{0:0.00000}", quota_fissa_fascia2.HasValue ? quota_fissa_fascia2.Value : 0) + " mc";
            }
        }

        public string ImportoAnnuoQuotaFissaSecondaFascia
        {
            get
            {
                return String.Format("{0:0.00000}", imp_annuo_quota_fissa_fascia2.HasValue ? imp_annuo_quota_fissa_fascia2.Value : 0) + " €";
            }
        }

        public string QuotaFissaTerzaFascia
        {
            get
            {
                return String.Format("{0:0.00000}", quota_fissa_fascia3.HasValue ? quota_fissa_fascia3.Value : 0) + " mc";
            }
        }

        public string ImportoAnnuoQuotaFissaTerzaFascia
        {
            get
            {
                return String.Format("{0:0.00000}", imp_annuo_quota_fissa_fascia3.HasValue ? imp_annuo_quota_fissa_fascia3.Value : 0) + " €";
            }
        }

        public string QuotaFissaQuartaFascia
        {
            get
            {
                return String.Format("{0:0.00000}", quota_fissa_fascia4.HasValue ? quota_fissa_fascia4.Value : 0) + " mc";
            }
        }

        public string ImportoAnnuoQuotaFissaQuartaFascia
        {
            get
            {
                return String.Format("{0:0.00000}", imp_annuo_quota_fissa_fascia4.HasValue ? imp_annuo_quota_fissa_fascia4.Value : 0) + " €";
            }
        }

        public string ImportoUnitarioFognatura
        {
            get
            {
                return String.Format("{0:0.00000}", imp_unitario_fognatura.HasValue ? imp_unitario_fognatura.Value : 0) + " €/mc";
            }
        }

        public string ImportoUnitarioDepurazione
        {
            get
            {
                return String.Format("{0:0.00000}", imp_unitario_depurazione.HasValue ? imp_unitario_depurazione.Value : 0) + " €/mc";
            }
        }

        [Required]
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

        [Required]
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

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID")]
            public int id_categoria_tariffaria { get; set; }

            [Required]
            [DisplayName("Ente")]
            public int id_ente { get; set; }

            [Required]
            [DisplayName("Ente Gestito")]
            public int id_ente_gestito { get; set; }

            [Required]
            [DisplayName("Entrata")]
            public int id_entrata { get; set; }

            [Required]
            [DisplayName("Anno")]
            public int anno { get; set; }

            [Required]
            [DisplayName("Categoria")]
            public int id_anagrafica_categoria { get; set; }

            [Required]
            [DisplayName("Impegno base")]
            public string flag_impegno_base_std { get; set; }

            [Required]
            [DisplayName("Unità di misura base")]
            public string um_base_std { get; set; }

            [Required]
            [DisplayName("Importo unitario base")]
            public decimal imp_unitario_base_std { get; set; }

            [DisplayName("Flag essenziali")]
            public string flag_essenziali { get; set; }

            [DisplayName("Tipo essenziali")]
            public string tipo_essenziali { get; set; }

            [DisplayName("Aliquota essenziali")]
            public Nullable<decimal> aliquota_essenziali { get; set; }

            [Required]
            [DisplayName("Unità di misura agevolata")]
            public string um_agevolata { get; set; }

            [Required]
            [DisplayName("Periodo di riferimento da")]
            public System.DateTime periodo_rif_da { get; set; }

            [Required]
            [DisplayName("Periodo di riferimento a")]
            public System.DateTime periodo_rif_a { get; set; }
        }
    }
}
