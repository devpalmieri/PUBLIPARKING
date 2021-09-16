using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_area_mercato.Metadata))]
    public partial class tab_area_mercato : ISoftDeleted, IGestioneStato
    {
        public const string ATT_ATT = "ATT-ATT";
        public const string ATT_CES = "ATT-CES";
        public const string ANN_ANN = "ANN-ANN";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }

        public string data_inizio_validita_String
        {
            get
            {
                if (data_inizio_validita.HasValue)
                {
                    return data_inizio_validita.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    data_inizio_validita = DateTime.Parse(value);
                }
                else
                {
                    data_inizio_validita = null;
                }
            }
        }

        public string data_fine_validita_String
        {
            get
            {
                if (data_fine_validita.HasValue)
                {
                    return data_fine_validita.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    data_fine_validita = DateTime.Parse(value);
                }
                else
                {
                    data_fine_validita = null;
                }
            }
        }

        public string canone_mq_concessioni_tipo_a_Euro
        {
            get
            {
                if (canone_mq_concessioni_tipo_a.HasValue)
                {
                    return canone_mq_concessioni_tipo_a.Value.ToString("C");
                }
                else
                {
                    return 0.ToString("C");
                }
            }
        }

        public string canone_mq_concessioni_tipo_b_Euro
        {
            get
            {
                if (canone_mq_concessioni_tipo_b.HasValue)
                {
                    return canone_mq_concessioni_tipo_b.Value.ToString("C");
                }
                else
                {
                    return 0.ToString("C");
                }
            }
        }

        public string canone_mq_piazzole_spuntisti_Euro
        {
            get
            {
                if (canone_mq_piazzole_spuntisti.HasValue)
                {
                    return canone_mq_piazzole_spuntisti.Value.ToString("C");
                }
                else
                {
                    return 0.ToString("C");
                }
            }
        }

        public string GiorniSettimanaTenutaMercato
        {
            get
            {
                string v_return = string.Empty;

                if (gg_settimana_tenuta_mercato[0] == '1')
                {
                    v_return = v_return + "Lun";
                }

                if (gg_settimana_tenuta_mercato[1] == '1')
                {
                    if (v_return.Length > 0)
                    {
                        v_return = v_return + "-";
                    }

                    v_return = v_return + "Mar";
                }

                if (gg_settimana_tenuta_mercato[2] == '1')
                {
                    if (v_return.Length > 0)
                    {
                        v_return = v_return + "-";
                    }

                    v_return = v_return + "Mer";
                }

                if (gg_settimana_tenuta_mercato[3] == '1')
                {
                    if (v_return.Length > 0)
                    {
                        v_return = v_return + "-";
                    }

                    v_return = v_return + "Gio";
                }

                if (gg_settimana_tenuta_mercato[4] == '1')
                {
                    if (v_return.Length > 0)
                    {
                        v_return = v_return + "-";
                    }

                    v_return = v_return + "Ven";
                }

                if (gg_settimana_tenuta_mercato[5] == '1')
                {
                    if (v_return.Length > 0)
                    {
                        v_return = v_return + "-";
                    }

                    v_return = v_return + "Sab";
                }

                if (gg_settimana_tenuta_mercato[6] == '1')
                {
                    if (v_return.Length > 0)
                    {
                        v_return = v_return + "-";
                    }

                    v_return = v_return + "Dom";
                }

                return v_return;
            }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }
        }
    }
}
