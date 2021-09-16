﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    //// Abilitare per la gestione dei controlli nelle View
    [MetadataTypeAttribute(typeof(join_tipo_voci_contribuzione_priorita_riscossione.Metadata))]
    public partial class join_tipo_voci_contribuzione_priorita_riscossione  : ISoftDeleted, IGestioneStato
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            // Gestire il salvataggio automatico dello stato
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
