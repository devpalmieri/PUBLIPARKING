using Publisoftware.Data.CustomValidationAttrs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_voci_contribuzione.Metadata))]
    public partial class anagrafica_voci_contribuzione : ISoftDeleted
    {

        public const string CODICE_TRIBUTO_IRA = "IRA";
        public const string CODICE_TRIBUTO_ILG = "ILG";
        public const string CODICE_TRIBUTO_IMO = "IMO";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public const int ID_AVVISO_COLLEGATO_GENERICO = 500;
        public const int ID_ARROTONDAMENTO = 66;
        //ID VOCI CONTRIBUZIONE IMU 
        public const int ID_IMU_AVVISO_ACCERTAMENTO= 268;

        public const int ID_IMU_IMPOSTA_ACCONTO = 199;
        public const int ID_IMU_IMPOSTA_SALDO = 200;

        public const int ID_IMU_AVVISO_ACCONTO = 201;
        public const int ID_IMU_AVVISO_SALDO = 202;

        public const int ID_TASI_IMPOSTA_ACCONTO = 214;
        public const int ID_TASI_IMPOSTA_SALDO = 215;

        public const int ID_TASI_AVVISO_ACCONTO = 282;
        public const int ID_TASI_AVVISO_SALDO = 283;


        public const int ID_IMU_SANZIONI_OMESSO_PARZIALE_VERSAMENTO_ACONTO = 203;
        public const int ID_IMU_INTERESSI_OMESSO_PARZIALE_VERSAMENTO_ACONTO = 204;
        public const int ID_IMU_SANZIONI_OMESSO_PARZIALE_VERSAMENTO_SALDO = 205;
        public const int ID_IMU_INTERESSI_OMESSO_PARZIALE_VERSAMENTO_SALDO = 206;
        public const int ID_IMU_INTERESSI_RATEIZZAZIONE = 1310;
        public const int ID_IMU_INTERESSI_MORA = 3243;
        public const int ID_IMU_SANZIONI = 81;
        public const int ID_IMU__SPESE = 2111;
        // ID VOCI CONTRIBUZIONE TARI ORDINARIA
        public const int TARSU = 26;
        public const int DIFF_TARSU = 75;

        public const int TARSU_PROV = 144;
        public const int DIFF_TARSU_PROV = 190;

        public const int TARI_IMPOSTA = 207;
        public const int TARI_QUOTA_FISSA = 232;

        public const int TEFA = 28;
        public const int DIFF_TEFA = 77;
        public const int TEFAG = 158;

        public const int SANZIONE_INF_TARSU = 71;
        public const int SANZIONE_INF_TARI = 247;

        public const int SANZIONE_OME_TARSU = 73;
        public const int SANZIONE_OME_TARI = 253;

        public const int INTERESSI_TEFA = 224;
        public const int INTERESSI_TARI = 238;
        public const int INTERESSI_TARI_QUOTA_FISSA = 239;

        public const int LMP_VOTIVE = 1314;

        public const int ICP = 155;
        public const int TOSAP = 164;
        public const int COSAP = 154;
        public const int TARIG = 156;
        //
        //ID VOCI PER CANONE UNICO PATRIMONIALE
        public const int ID_CANONE_UNICO_AFFISSIONI = 3310;
        //
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Id")]
            public int id_anagrafica_voce_contribuzione { get; set; }
            [DisplayName("Id Entrata")]
            public int id_entrata { get; set; }
            [DisplayName("Id Tipo Voce Contribuzione")]
            public int id_tipo_voce_contribuzione { get; set; }
            [DisplayName("Descrizione")]
            [MaxLength(100)]
            [Required]
            public string descr_anagrafica_voce_contribuzione { get; set; }
            [DisplayName("Codice")]
            [MaxLength(6)]
            [Required]
            public string cod_anagrafica_voce_contribuzione { get; set; }
            [DisplayName("Cod. Ministeriale")]
            [MaxLength(10)]
            public string cod_tributo_ministeriale { get; set; }
            [DisplayName("Flag IVA")]
            [RegularExpression("[0-9]{1}", ErrorMessage = "Formato non valido: 1 carattere numerico")]
            public string flag_iva { get; set; }
            [DisplayName("Aliquota IVA")]
            public Nullable<decimal> aliquota_iva { get; set; }
            [DisplayName("Flag Competenza")]
            [RegularExpression("[0-9]{1}", ErrorMessage = "Formato non valido: 1 carattere numerico")]
            public string flag_competenza { get; set; }
            [DisplayName("Priorità Pagamento")]
            [RegularExpression("[0-9]*", ErrorMessage = "Formato non valido: solo caratteri numerici")]
            public Nullable<int> priorita_pagamento { get; set; }
        }
    }
}
