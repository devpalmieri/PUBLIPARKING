using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(join_applicazioni_link.Metadata))]
    public partial class join_applicazioni_link : ISoftDeleted
    {
        public join_applicazioni_link(int p_id_tab_applicazioni, int p_id_tab_applicazioni_link, string p_label, int? p_ordine)
        {
            id_tab_applicazioni = p_id_tab_applicazioni;
            id_tab_applicazioni_link = p_id_tab_applicazioni_link;
            label = p_label;
            ordine = p_ordine;
        }

        public join_applicazioni_link()
        {

        }
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public string Applicazione
        {
            get { return tab_applicazioni.codice + " - " + tab_applicazioni.descrizione; }
        }

        public string FullCodeApplicazione
        {
            get { return tab_applicazioni.FullCode + " - " + tab_applicazioni.descrizione; }
        }

        public string Link
        {
            get { return tab_applicazioni1.codice + " - " + tab_applicazioni1.descrizione; }
        }

        public string FullCodeLink
        {
            get { return tab_applicazioni1.FullCode + " - " + tab_applicazioni1.descrizione; }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Applicazione Destinazione")]
            public int id_tab_applicazioni_link { get; set; }
        }
    }
}
