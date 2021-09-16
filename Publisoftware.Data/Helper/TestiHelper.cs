#if Spostato_nella_nuova_Dll_Publisoftware_Data_Bd_Pdf
using Publisoftware.Data.BD;
using Publisoftware.Data.LinqExtended;
using Publisoftware.Utility.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD.Helper
{
    public class TestiHelper
    {
        public static string GetAsseverazioneAvvisoImportato(anagrafica_risorse p_risorsa, List<tab_avv_pag> p_avvisiList)
        {
            string v_avvisiDescrizione = string.Empty;

            foreach (tab_avv_pag v_avviso in p_avvisiList)
            {
                v_avvisiDescrizione = v_avvisiDescrizione + v_avviso.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " n. " + v_avviso.identificativo_avv_pag.Trim() + ".\n";
            }

            return "ASSEVERAZIONE DI CONFORMITA’ \n" +
                   new StringBuilder().Append(' ', 150) + "\n" +
                   "La società PUBLISERVIZI S.r.l., P.I. 03218060659, con sede legale in Roma Piazza Capranica n° 95, " +
                   "Concessionario dell'Ente " + p_avvisiList.FirstOrDefault().anagrafica_ente.descrizione_ente + " per la Riscossione Coattiva delle Entrate" +
                   "in persona del" + p_risorsa.anagrafica_ruolo_mansione.descr_ruolo_mansione + ", " + p_risorsa.CognomeNome.ToUpper() + ", assevera, ai sensi e per gli effetti dell’art 5, " +
                   "5° comma D.L. 669/1996 convertito nella L. 30/1997, in ottemperanza a quanto previsto dall'art. 543 comma 4° cpc, " +
                   "che gli Atti sotto elencati e le relative relate di notifica sono documenti informatici trasmessi dall’Ente " + p_avvisiList.FirstOrDefault().anagrafica_ente.descrizione_ente + ", " +
                   "aventi contenuto identico e conforme agli originali in formato analogico cartaceo, in possesso dell’Ente, da cui sono state estratti.\n" +
                   "Dichiara, altresì, che la presente asseverazione è posta su separato foglio che viene congiunto agli atti in esame e ne costituisce parte integrante.\n" +
                   new StringBuilder().Append(' ', 150) + "\n" +
                   v_avvisiDescrizione +
                   new StringBuilder().Append(' ', 150) + "\n" +
                   DateTime.Now.ToShortDateString() + new StringBuilder().Append(' ', 122) + "Publiservizi s.r.l \n" +
                   new StringBuilder().Append(' ', 132) + "Presidente del C.d.A. \n" +
                   new StringBuilder().Append(' ', 152 - p_risorsa.CognomeNome.Length) + p_risorsa.CognomeNome.ToUpper() + "\n" +
                   new StringBuilder().Append(' ', 150);
        }

        public static string GetAsseverazioneAvvisoEmesso(anagrafica_risorse p_risorsa, List<tab_avv_pag> p_avvisiList, tab_procure p_procura)
        {
            string v_avvisiDescrizione = string.Empty;

            foreach (tab_avv_pag v_avviso in p_avvisiList)
            {
                v_avvisiDescrizione = v_avvisiDescrizione + v_avviso.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " n. " + v_avviso.identificativo_avv_pag.Trim() + ".\n";
            }

            return "ASSEVERAZIONE DI CONFORMITA’ \n" +
                   new StringBuilder().Append(' ', 150) + "\n" +
                   "Il sottoscritto " + p_procura.titolo_procuratore + " " + p_risorsa.CognomeNome.ToUpper() + ", C.F. " + p_risorsa.cod_fiscale + ", " +
                   "procuratore in atti di PUBLISERVIZI SRL, come da " + p_procura.desc_tipo_procura + " rilasciata in data " + p_procura.data_procura.ToShortDateString() + " " +
                   "innanzi a notaio " + p_procura.redattore_procura + " " + p_procura.reportorio_raccolta + ", " +
                   "e per gli effetti del combinato disposto degli art. 3 bis comma 2 e 6 comma 1 della L. 53/94 così come modificata dalla lettera d) del comma 1 "+
                   "dell'art. 16 quater. D.L. 18/10/2012 n.179, aggiunto dal comma 19 dell'art. 1, D.L. 24/12/2012 n.228 e dell'art. 22 comma 2 del D.L. 7/3/2005 n.82 e succ.mod., " +
                   "assevera che gli Atti di seguito elencati emessi da Publiservizi srl, in qualità di Concessionario dell'Ente " + p_avvisiList.FirstOrDefault().anagrafica_ente.descrizione_ente + " " +
                   "per la Riscossione Coattiva delle Entrate, e le relative relate di notifica sono documenti informatici " + 
                   "aventi contenuto identico e conforme a quello degli originali in formato analogico cartaceo da cui sono stati estratti.\n" +
                   "Dichiara, altresì, che la presente asseverazione è posta su separato foglio che viene congiunto agli atti in esame e ne costituisce parte integrante.\n" +
                   new StringBuilder().Append(' ', 150) + "\n" +
                   v_avvisiDescrizione +
                   new StringBuilder().Append(' ', 150) + "\n" +
                   DateTime.Now.ToShortDateString() + new StringBuilder().Append(' ', 110 - p_procura.titolo_procuratore.Length - p_risorsa.CognomeNome.Length) + p_procura.titolo_procuratore + " " + p_risorsa.CognomeNome.ToUpper() + "\n" +
                   new StringBuilder().Append(' ', 150) + "\n";
        }

        public static string GetAsseverazioneNotificaImportata(anagrafica_risorse p_risorsa, tab_avv_pag p_avviso)
        {
            return "ASSEVERAZIONE DI CONFORMITA’ \n" +
                   new StringBuilder().Append(' ', 150) + "\n" +
                   "La società PUBLISERVIZI S.r.l., P.I. 03218060659, con sede legale in Roma Piazza Capranica n° 95, " +
                   "in persona del legale rappresentante p.t., " + p_risorsa.CognomeNome.ToUpper() + " assevera, ai sensi e per gli effetti dell’art 5, " +
                   "5° comma D.L. 669/1996 convertito nella L. 30/1997, che il documento sopra riportato " +
                   "relativo alla relata/cartolina di notifica dell'Atto di " + p_avviso.anagrafica_tipo_avv_pag.descr_tipo_avv_pag +
                   " n. " + p_avviso.identificativo_avv_pag.Trim() + " notificato in data " +
                   (p_avviso.data_ricezione.HasValue ? p_avviso.data_ricezione.Value.ToShortDateString() : "") +
                   " è conforme al documento trasmesso dall'Ente Creditore " + p_avviso.anagrafica_ente.descrizione_ente + ".\n" +
                   new StringBuilder().Append(' ', 150) + "\n" +
                   "Roma, " + DateTime.Now.ToShortDateString() + new StringBuilder().Append(' ', 110) + "Publiservizi s.r.l \n" +
                   new StringBuilder().Append(' ', 128) + "Il rappresentante Legale \n" +
                   new StringBuilder().Append(' ', 152 - p_risorsa.CognomeNome.Length) + p_risorsa.CognomeNome.ToUpper() + "\n" +
                   new StringBuilder().Append(' ', 150) + "\n" +
                   "La firma autografa del rappresentante legale è sostituita dall'indicazione a stampa del nominativo dello stesso ai sensi dell'art 3 " +
                   "comma 2 D.L. 39/93 e dell'art 1 comma 87, Legge 28/12/1995.";
        }

        public static string GetAsseverazioneNotificaEmessa(anagrafica_risorse p_risorsa, tab_avv_pag p_avviso)
        {
            return "ASSEVERAZIONE DI CONFORMITA’ \n" +
                   new StringBuilder().Append(' ', 150) + "\n" +
                   "La società PUBLISERVIZI S.r.l., P.I. 03218060659, con sede legale in Roma Piazza Capranica n° 95, " +
                   "in persona del legale rappresentante p.t., " + p_risorsa.CognomeNome.ToUpper() + " assevera che il documento sopra riportato " +
                   "relativo alla relata/cartolina di notifica dell'Atto di " + p_avviso.anagrafica_tipo_avv_pag.descr_tipo_avv_pag +
                   " n. " + p_avviso.identificativo_avv_pag.Trim() + " notificato in data " +
                   (p_avviso.data_ricezione.HasValue ? p_avviso.data_ricezione.Value.ToShortDateString() : "") +
                   " è conforme al documento in originale in suo possesso.\n" +
                   new StringBuilder().Append(' ', 150) + "\n" +
                   "Roma, " + DateTime.Now.ToShortDateString() + new StringBuilder().Append(' ', 110) + "Publiservizi s.r.l \n" +
                   new StringBuilder().Append(' ', 128) + "Il rappresentante Legale \n" +
                   new StringBuilder().Append(' ', 152 - p_risorsa.CognomeNome.Length) + p_risorsa.CognomeNome.ToUpper() + "\n" +
                   new StringBuilder().Append(' ', 150) + "\n" +
                   "La firma autografa del rappresentante legale è sostituita dall'indicazione a stampa del nominativo dello stesso ai sensi dell'art 3 " +
                   "comma 2 D.L. 39/93 e dell'art 1 comma 87, Legge 28/12/1995.";
        }

        public static string GetAsseverazioneGenerica(anagrafica_risorse p_risorsa)
        {
            return "ASSEVERAZIONE DI CONFORMITA’ \n" +
                   new StringBuilder().Append(' ', 150) + "\n" +
                   "La società PUBLISERVIZI S.r.l., P.I. 03218060659, con sede legale in Roma Piazza Capranica n° 95, " +
                   "in persona del legale rappresentante p.t., " + p_risorsa.CognomeNome.ToUpper() + " assevera " +
                   " che il documento sopra riportato è conforme al documento in originale in suo possesso. \n" +
                   new StringBuilder().Append(' ', 150) + "\n" +
                   "Roma, " + DateTime.Now.ToShortDateString() + new StringBuilder().Append(' ', 110) + "Publiservizi s.r.l \n" +
                   new StringBuilder().Append(' ', 128) + "Il rappresentante Legale \n" +
                   new StringBuilder().Append(' ', 152 - p_risorsa.CognomeNome.Length) + p_risorsa.CognomeNome.ToUpper() + "\n" +
                   new StringBuilder().Append(' ', 150) + "\n" +
                   "La firma autografa del rappresentante legale è sostituita dall'indicazione a stampa del nominativo dello stesso ai sensi dell'art 3 " +
                   "comma 2 D.L. 39/93 e dell'art 1 comma 87, Legge 28/12/1995.";
        }

        public static string GetProcuraCitazione(tab_procure p_procura, tab_avv_pag p_avviso)
        {
            return "PROCURA ALLE LITI \n" +
                   new StringBuilder().Append(' ', 150) + "\n" +
                   "Io sottoscritto Luigi Monti, in qualità di Rappresentante legale p. t. e Presidente del C.d.A. della società " +
                   "Publiservizi S.r.l. P. iva 03218060659 con sede in Roma Piazza Capranica n. 95, nomino e costituisco " +
                   p_procura.titolo_procuratore + " " + p_procura.anagrafica_risorse.CognomeNome + " CF: " + p_procura.anagrafica_risorse.cod_fiscale +
                   " mio procuratore e difensore nel giudizio, relativo all' Atto di " + p_avviso.anagrafica_tipo_avv_pag.descr_tipo_avv_pag +
                   " n. " + p_avviso.identificativo_avv_pag.Trim() + " in ogni fase, stato e grado e nella conseguente procedura esecutiva" +
                   " e gli conferisco ogni più ampia facoltà di legge, ivi compresa quella di nominare procuratori anche quali sostituti processuali, " +
                   "chiamare in causa, intervenire in giudizio, transigere e rilasciare quietanza, conciliare, rinunciare ed accettare rinunce negli atti.\n" +
                   "La presente procura alle liti è da intendersi apposta in calce all’atto, anche ai sensi dell’art. 18, co. 5, D. M. Giustizia n. 44/2011, " +
                   "come sostituito dal D. M. Giustizia n. 48/2013.\n" +
                   "Io sottoscritto eleggo domicilio presso la sede operativa della Publiservizi S.r.l. sita in Caserta corso Giannone, 50 " + p_procura.pec_rif_procura + ".\n" +
                   new StringBuilder().Append(' ', 150) + "\n" +
                   new StringBuilder().Append(' ', 129) + "Publiservizi s.r.l" + "\n" +
                   new StringBuilder().Append(' ', 128) + "Il rappresentante Legale \n" +
                   new StringBuilder().Append(' ', 130) + "Monti Luigi" + "\n" +
                   new StringBuilder().Append(' ', 150) + "\n" +
                   new StringBuilder().Append(' ', 130) + "Per autentica" + "\n" +
                   new StringBuilder().Append(' ', 130) + p_procura.anagrafica_risorse.CognomeNome;
        }
    }
}
#endif
