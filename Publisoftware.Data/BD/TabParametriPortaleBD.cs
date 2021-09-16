using Publisoftware.Data.BD.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabParametriPortaleBD : EntityBD<tab_parametri_portale>
    {
        public TabParametriPortaleBD()
        {

        }

        public static void ReparePath(ref string p_path)
        {
            if (p_path.StartsWith("\\\\\\\\"))
            {
                p_path = p_path.Replace("\\\\", "\\");
            }
            else if (!p_path.StartsWith("\\\\"))
            {
                p_path = p_path.Replace("\\\\", "\\").Replace("/", "\\");
            }
        }

        /// <summary>
        /// Restituisce la configurazione richiesta
        /// </summary>
        /// <param name="p_mode">Modalità richiesta</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static tab_parametri_portale GetByMode(string p_mode, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(p => p.mode != null && p.mode.Equals(p_mode)).SingleOrDefault();
        }

        public static String getAvviso(int p_idAllegato, string p_urlImg, string p_pathAvvisi, string p_codice_ente, dbEnte p_dbContext)
        {
            return FileBDRWInfoHelper.GetAvviso(p_idAllegato, p_urlImg, p_pathAvvisi, p_codice_ente, p_dbContext);
        }

        public static String getAvviso(int p_idAllegato, string p_mode, string p_codice_ente, dbEnte p_dbContext)
        {
            tab_parametri_portale v_parametri = GetByMode(p_mode, p_dbContext);
            return FileBDRWInfoHelper.GetAvviso(p_idAllegato, v_parametri.url_img_anagrafica, v_parametri.path_avvisi, p_codice_ente, p_dbContext);
        }

        public static String getFileAvviso(int p_idAllegato, string p_mode, string p_codice_ente, dbEnte p_dbContext)
        {
            tab_parametri_portale v_parametri = GetByMode(p_mode, p_dbContext);
            string v_pathDir = FileBDRWInfoHelper.GetAvviso(p_idAllegato, v_parametri.path_upload_file, v_parametri.path_avvisi, p_codice_ente, p_dbContext);
            ReparePath(ref v_pathDir);

            return v_pathDir;
        }

        public static String getFileEsito(tab_sped_not p_sped_not, string p_mode, string p_codice_ente, dbEnte p_dbContext)
        {
            tab_parametri_portale v_parametri = GetByMode(p_mode, p_dbContext);
            string v_nome_file = TabSpedNotBD.GetById(p_sped_not.id_sped_not, p_dbContext).barcode + ".pdf";
            string v_pathDir = getPathEsitoNew(p_sped_not, p_mode, p_codice_ente, p_dbContext);
            ReparePath(ref v_pathDir);
            v_nome_file = Path.Combine(v_pathDir, v_nome_file);
            return v_nome_file;
        }
        public static String getDocInput(int p_idTabDocInput, string p_urlImg, string p_pathDocInput, string p_codice_ente, dbEnte p_dbContext)
        {
            return FileBDRWInfoHelper.GetDocInput(p_idTabDocInput, p_urlImg, p_pathDocInput, p_codice_ente, p_dbContext);
        }

        public static String getDocInput(int p_idTabDocInput, string p_mode, string p_codice_ente, dbEnte p_dbContext)
        {
            tab_parametri_portale v_parametri = GetByMode(p_mode, p_dbContext);
            return FileBDRWInfoHelper.GetDocInput(p_idTabDocInput, v_parametri.url_img_anagrafica, v_parametri.path_avvisi, p_codice_ente, p_dbContext);
        }

        public static String getPathEsito(tab_sped_not p_spedNot, string p_mode, string p_codice_ente, dbEnte p_dbContext, bool p_createIfNotExist = false)
        {
            string v_codiceTipoAvvPag = p_spedNot.tab_avv_pag.anagrafica_tipo_avv_pag.cod_tipo_avv_pag;
            string v_annoRiferimento = p_spedNot.tab_avv_pag.anno_riferimento;

            tab_parametri_portale v_parametri = GetByMode(p_mode, p_dbContext);
            string v_pathDir = Path.Combine(v_parametri.path_upload_file, v_parametri.path_esito, p_codice_ente, v_codiceTipoAvvPag, v_annoRiferimento);

            ReparePath(ref v_pathDir);
            if (p_createIfNotExist == true && !Directory.Exists(v_pathDir))
            {
                Directory.CreateDirectory(v_pathDir);
            }

            return v_pathDir;
        }

        public static String getPathEsitoNew(tab_sped_not p_spedNot, string p_mode, string p_codice_ente, dbEnte p_dbContext, bool p_createIfNotExist = false)
        {
            string v_codiceTipoDoc = p_spedNot.tab_doc_output.tab_tipo_doc_entrate.cod_tipo_doc;
            string v_annoRiferimento = p_spedNot.tab_doc_output.anno.ToString();

            tab_parametri_portale v_parametri = GetByMode(p_mode, p_dbContext);
            string v_pathDir = Path.Combine(v_parametri.path_upload_file, v_parametri.path_esito, p_codice_ente, v_codiceTipoDoc, v_annoRiferimento);

            if (!URLExists(v_pathDir))
            {
                v_codiceTipoDoc = p_spedNot.tab_avv_pag.anagrafica_tipo_avv_pag.cod_tipo_avv_pag;
                v_annoRiferimento = p_spedNot.tab_avv_pag.anno_riferimento;
            }

            ReparePath(ref v_pathDir);

            if (p_createIfNotExist == true && !Directory.Exists(v_pathDir))
            {
                Directory.CreateDirectory(v_pathDir);
            }

            return v_pathDir;
        }

        public static String getPathEsitoPec(tab_sped_not p_spedNot, string p_mode, string p_codice_ente, dbEnte p_dbContext, bool p_createIfNotExist = false)
        {
            string v_codiceTipoAvvPag = p_spedNot.tab_avv_pag.anagrafica_tipo_avv_pag.cod_tipo_avv_pag;
            string v_annoRiferimento = p_spedNot.tab_avv_pag.anno_riferimento;

            tab_parametri_portale v_parametri = GetByMode(p_mode, p_dbContext);
            string v_pathDir = Path.Combine(v_parametri.path_upload_file, v_parametri.path_esito_pec, p_codice_ente, v_codiceTipoAvvPag, v_annoRiferimento);

            ReparePath(ref v_pathDir);
            if (p_createIfNotExist == true && !Directory.Exists(v_pathDir))
            {
                Directory.CreateDirectory(v_pathDir);
            }

            return v_pathDir;
        }
        public static string getDocumentoNotificaForUpload(tab_avv_pag v_avviso, string p_pathUploadFile, string p_pathNotifiche)
        {
            string v_filePath = p_pathNotifiche.Replace("/", "\\\\") + v_avviso.anagrafica_ente.codice_ente + "\\\\" + v_avviso.anagrafica_tipo_avv_pag.cod_tipo_avv_pag + "\\\\" + v_avviso.anno_riferimento + "\\\\";
            string v_path = p_pathUploadFile + v_filePath;

            string v_pathWithFile = v_path;

            return v_pathWithFile;
        }

        public static String getPathEsitoPecAvvisi(tab_sped_not p_spedNot, string p_mode, string p_codice_ente, dbEnte p_dbContext, bool p_createIfNotExist = false)
        {
            string v_destinationPath = string.Empty;
            tab_parametri_portale v_parametri = GetByMode(p_mode, p_dbContext);

            v_destinationPath = getDocumentoNotificaForUpload(p_spedNot.tab_avv_pag, v_parametri.path_upload_file, v_parametri.path_notifiche);

            ReparePath(ref v_destinationPath);
            if (p_createIfNotExist == true && !Directory.Exists(v_destinationPath))
            {
                Directory.CreateDirectory(v_destinationPath);
            }

            return v_destinationPath;
        }

        public static String getPathFileTrasmessi(string p_mode, int p_id_ente, int p_id_struttura, dbEnte p_dbContext, bool p_createIfNotExist = false)
        {
            tab_parametri_portale v_parametri = GetByMode(p_mode, p_dbContext);
            string v_pathDir = Path.Combine(v_parametri.path_upload_file, v_parametri.path_file_trasmessi, "Ente" + p_id_ente.ToString(), p_id_struttura.ToString());

            ReparePath(ref v_pathDir);
            if (p_createIfNotExist == true && !Directory.Exists(v_pathDir))
            {
                Directory.CreateDirectory(v_pathDir);
            }

            return v_pathDir;
        }

        public static String getPathAvvisi(string p_mode, string p_cod_ente, dbEnte p_dbContext, bool p_createIfNotExist = false)
        {
            tab_parametri_portale v_parametri = GetByMode(p_mode, p_dbContext);
            string v_pathDir = Path.Combine(v_parametri.path_upload_file, v_parametri.path_avvisi, p_cod_ente);

            ReparePath(ref v_pathDir);
            if (p_createIfNotExist == true && !Directory.Exists(v_pathDir))
            {
                Directory.CreateDirectory(v_pathDir);
            }

            return v_pathDir;
        }

        public static String getPathFileRimborsi(string p_mode, int p_id_ente, dbEnte p_dbContext, bool p_createIfNotExist = false)
        {
            tab_parametri_portale v_parametri = GetByMode(p_mode, p_dbContext);
            string v_pathDir = Path.Combine(v_parametri.path_upload_file, "bonifici", "Ente" + p_id_ente.ToString());

            ReparePath(ref v_pathDir);
            if (p_createIfNotExist == true && !Directory.Exists(v_pathDir))
            {
                Directory.CreateDirectory(v_pathDir);
            }

            return v_pathDir;
        }

        public static bool URLExists(string url)
        {
            bool result = true;

            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Timeout = 1200; // miliseconds
            webRequest.Method = "HEAD";

            try
            {
                webRequest.GetResponse();
            }
            catch
            {
                result = false;
            }

            return result;
        }
    }
}
