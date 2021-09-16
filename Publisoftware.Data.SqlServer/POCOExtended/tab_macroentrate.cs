using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_macroentrate.Metadata))]
    public partial class tab_macroentrate : ISoftDeleted
    {
        public const int RIFIUTI_ID = 4;
        public const int RIFIUTI_GIORNALIERI_ID = 21;
        public const int IMMOBILI_IMU_ID = 25;

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public bool IsIdrico
        {
            get { return join_entrate_macroentrate.Any(j => j.id_entrata == anagrafica_entrate.ACQUEDOTTO || j.id_entrata == anagrafica_entrate.ACQUEDOTTO1); }
        }

        public bool IsImmobiliare
        {
            get { return join_entrate_macroentrate.Any(j => j.id_entrata == anagrafica_entrate.ICI || j.id_entrata == anagrafica_entrate.IMU); }
        }

        public bool IsRifiuti
        {
            get { return join_entrate_macroentrate.Any(j => j.id_entrata == anagrafica_entrate.TARES_TARSU || j.id_entrata == anagrafica_entrate.TARI); }
        }

        public bool IsOccupazioneSuolo
        {
            get { return join_entrate_macroentrate.Any(j => j.id_entrata == anagrafica_entrate.TOSAP || j.id_entrata == anagrafica_entrate.COSAP); }
        }

        public bool IsTasi
        {
            get { return join_entrate_macroentrate.Any(j => j.id_entrata == anagrafica_entrate.TASI); }
        }

        public bool IsAffissione
        {
            get { return join_entrate_macroentrate.Any(j => j.id_entrata == anagrafica_entrate.ICP); }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Id")]
            public int id_tab_macroentrate { get; set; }

            [RegularExpression("[a-zA-Z0-9]{1,50}", ErrorMessage = "Formato non valido")]
            [DisplayName("Sigla")]
            public string sigla { get; set; }

            [Required]
            [RegularExpression(@"^[\w\s]{1,100}$", ErrorMessage = "Formato non valido")]
            [DisplayName("Descrizione")]
            public string descrizione { get; set; }
        }        
    }
}
