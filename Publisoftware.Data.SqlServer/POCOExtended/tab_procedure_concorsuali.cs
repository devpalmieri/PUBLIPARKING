﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_procedure_concorsuali.Metadata))]
    public partial class tab_procedure_concorsuali : ISoftDeleted, IGestioneStato
    {
        //public const string ATT_ATT = "ATT-ATT";
        public const string ACQ_DEF = "ACQ-DEF";
        //public const string ACQ_PRE = "ACQ-PRE";
        public const string DEF_DEF = "DEF-DEF";

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

        internal sealed class Metadata
        {
            private Metadata()
            {
            }
        }
    }
}