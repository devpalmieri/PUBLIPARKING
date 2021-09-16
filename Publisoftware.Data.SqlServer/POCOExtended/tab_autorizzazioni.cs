using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_autorizzazioni : ISoftDeleted, IGestioneStato
    {
        public const string ACQ_ACQ = "ACQ-ACQ";
        public const string ATT_ATT = "ATT-ATT";
        public const string ANN_ANN = "ANN-ANN";
        public const string RIC_VER = "RIC-VER";
        public const string VER_VER = "VER-VER";

        public const string TIPO_RICHIEDENTE_CONTRIBUENTE = "CO";
        public const string TIPO_RICHIEDENTE_EREDE = "RE";
        public const string TIPO_RICHIEDENTE_RAPPRESENTANTE = "RC";
        public const string TIPO_RICHIEDENTE_TERZO = "TF";
        public const string TIPO_RICHIEDENTE_RAPPRESENTANTE_TERZO = "TR";

        public const string TIPO_AUTORIZZAZIONE_CONTRIBUENTE = "CO";
        public const string TIPO_AUTORIZZAZIONE_DELEGATO_CONTRIBUENTE = "DF";
        public const string TIPO_AUTORIZZAZIONE_EREDE = "EF";
        public const string TIPO_AUTORIZZAZIONE_RAPPRESENTANTE = "LG";
        public const string TIPO_AUTORIZZAZIONE_DELEGATO_IMPRESA = "DG";
        public const string TIPO_AUTORIZZAZIONE_TERZO = "TF";
        public const string TIPO_AUTORIZZAZIONE_RAPPRESENTANTE_TERZO = "TR";
        public const string TIPO_AUTORIZZAZIONE_DELEGATO_TERZO = "TD";

        public const string MESSAGGIO_DOCUMENTO_IDENTITA_MANCANTE = "Documento d'identità mancante";
        public const string MESSAGGIO_DOCUMENTO_IDENTITA_ERRATO = "Documento d'identità non conforme al soggetto richiedente";
        public const string MESSAGGIO_DOCUMENTO_IDENTITA_SCADUTO = "Documento d'identità non in corso di validità";

        public const string MESSAGGIO_CERTIFICATO_MORTE_MANCANTE = "Certificato di morte mancante";
        public const string MESSAGGIO_CERTIFICATO_MORTE_ERRATO = "Certificato di morte non conforme al soggetto deceduto";

        public const string MESSAGGIO_CERTIFICATO_REFERENTE_MANCANTE = "Mancanza di documento e/o dichiarazione in autotutela attestante il proprio ruolo di rappresentante legale/procuratore nell'ambito dell'Impresa/Ente";
        public const string MESSAGGIO_CERTIFICATO_REFERENTE_ERRATO = "Documento e/o dichiarazione in autotutela non sufficiente ad attestare il proprio ruolo di rappresentante legale/procuratore nell'ambito dell'Impresa/Ente";

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

        public string UtenteRichiedente
        {
            get
            {
                return tab_utenti != null ? tab_utenti.utenteDisplay : string.Empty;
            }
        }

        public string ContribuenteRichiedente
        {
            get
            {
                return tab_contribuente != null ? tab_contribuente.contribuenteDisplay : string.Empty;
            }
        }

        public string ContribuenteRichiestaAccesso
        {
            get
            {
                return tab_contribuente1 != null ? tab_contribuente1.contribuenteDisplay : string.Empty;
            }
        }

        public string ReferenteRichiedente
        {
            get
            {
                return tab_referente != null ? tab_referente.referenteDisplay : string.Empty;
            }
        }

        public string TerzoRichiestaAccesso
        {
            get
            {
                return tab_terzo != null ? tab_terzo.terzoDisplay : string.Empty;
            }
        }

        public string Risorsa
        {
            get
            {
                return anagrafica_risorse != null ? anagrafica_risorse.CognomeNome : string.Empty;
            }
        }

        public string Stato
        {
            get
            {
                switch (cod_stato)
                {
                    case ACQ_ACQ:
                        return "Acquisita";
                    case ATT_ATT:
                        return "Attiva";
                    case ANN_ANN:
                        return "Annullata";
                    case RIC_VER:
                        return "Sottoposta ad integrazione";
                    case VER_VER:
                        return "Integrata da utente";
                    default:
                        return string.Empty;
                }
            }
        }

        public string TipoRichiedente
        {
            get
            {
                switch (tipo_richiedente)
                {
                    case TIPO_RICHIEDENTE_CONTRIBUENTE:
                        return "Contribuente";
                    case TIPO_RICHIEDENTE_EREDE:
                        return "Erede";
                    case TIPO_RICHIEDENTE_RAPPRESENTANTE:
                        return "Rappresentante";
                    case TIPO_RICHIEDENTE_TERZO:
                        return "Terzo";
                    case TIPO_RICHIEDENTE_RAPPRESENTANTE_TERZO:
                        return "Rappresentante Terzo";
                    default:
                        return string.Empty;
                }
            }
        }

        public string TipoAutorizzazione
        {
            get
            {
                switch (tipo_autorizzazione)
                {
                    case TIPO_AUTORIZZAZIONE_CONTRIBUENTE:
                        return "Contribuente";
                    case TIPO_AUTORIZZAZIONE_DELEGATO_CONTRIBUENTE:
                        return "Delegato contribuente";
                    case TIPO_AUTORIZZAZIONE_EREDE:
                        return "Erede";
                    case TIPO_AUTORIZZAZIONE_RAPPRESENTANTE:
                        return "Rappresentante legale/procuratore";
                    case TIPO_AUTORIZZAZIONE_DELEGATO_IMPRESA:
                        return "Delegato impresa";
                    case TIPO_AUTORIZZAZIONE_TERZO:
                        return "Terzo";
                    case TIPO_AUTORIZZAZIONE_RAPPRESENTANTE_TERZO:
                        return "Rappresentante legale/procuratore terzo";
                    case TIPO_AUTORIZZAZIONE_DELEGATO_TERZO:
                        return "Delegato terzo";
                    default:
                        return string.Empty;
                }
            }
        }

        public string data_richiesta_String
        {
            get
            {
                if (data_richiesta.HasValue)
                {
                    return data_richiesta.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_assegnazione_richiesta_String
        {
            get
            {
                if (data_assegnazione_richiesta.HasValue)
                {
                    return data_assegnazione_richiesta.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_richiesta_integrazione_String
        {
            get
            {
                if (data_richiesta_integrazione.HasValue)
                {
                    return data_richiesta_integrazione.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_integrazione_String
        {
            get
            {
                if (data_integrazione.HasValue)
                {
                    return data_integrazione.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_approvazione_richiesta_String
        {
            get
            {
                if (data_approvazione_richiesta.HasValue)
                {
                    return data_approvazione_richiesta.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string Note
        {
            get
            {
                return note.Replace("|", "<p>- ").Replace(";", "</p>");
            }
        }
    }
}
