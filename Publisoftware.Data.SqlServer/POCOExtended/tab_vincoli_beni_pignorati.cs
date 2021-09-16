using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_vincoli_beni_pignorati : ISoftDeleted
    {
        public const string SEQUESTRO = "Sequestro";
        public const string CESSIONE = "Cessione";
        public const string PIGNORAMENTO = "Pignoramento";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public string data_accensione_vincolo_String
        {
            get
            {
                if (data_accensione_vincolo.HasValue)
                {
                    return data_accensione_vincolo.Value.ToShortDateString();
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
                    data_accensione_vincolo = DateTime.Parse(value);
                }
                else
                {
                    data_accensione_vincolo = null;
                }
            }
        }

        public string data_scadenza_vincolo_String
        {
            get
            {
                if (data_scadenza_vincolo.HasValue)
                {
                    return data_scadenza_vincolo.Value.ToShortDateString();
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
                    data_scadenza_vincolo = DateTime.Parse(value);
                }
                else
                {
                    data_scadenza_vincolo = null;
                }
            }
        }
    }
}
