using Publisoftware.Data.BD;
using Publisoftware.Data.LinqExtended;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD.Helper
{
    public class FileBDRWInfoHelper
    {
        public static string BuildEntePath(int id_ente)
        {
            return string.Concat("Ente", id_ente);
        }

        public static string GetAvviso(int p_idAllegato, string p_urlImg, string p_pathAvvisi, string codice_ente, dbEnte p_dbContext)
        {
            tab_sped_not v_tabSpedNot = TabSpedNotBD.GetById(p_idAllegato, p_dbContext);

            string v_pathAvvisi = p_pathAvvisi;
            string v_codiceTipoAvvPag = v_tabSpedNot.tab_avv_pag.anagrafica_tipo_avv_pag.cod_tipo_avv_pag + "/";

            string v_annoRiferimento = "";

            if (v_tabSpedNot.tab_avv_pag.tab_liste.id_tipo_lista != tab_tipo_lista.TIPOLISTA_TRASMISSIONE_ID)
            {
                tab_lista_sped_notifiche v_lista = TabListaSpedNotificheBD.GetById(v_tabSpedNot.id_lista_sped_not.Value, p_dbContext);
                v_annoRiferimento = v_lista.anno + "/";
            }
            else
            {
                v_annoRiferimento = v_tabSpedNot.tab_avv_pag.anno_riferimento + "/";
            }

            string v_nomeFile = v_tabSpedNot.barcode;
            string v_path = p_urlImg + v_pathAvvisi + codice_ente + "/" + v_codiceTipoAvvPag + v_annoRiferimento + v_nomeFile + ".pdf";

            return v_path;
        }

        public static string getEsitoDownloadNew(int p_idAllegato, string p_uploadCheck, string p_urlImg, string p_pathAvvisi, string p_codiceEnte, dbEnte p_dbContext)
        {
            tab_sped_not v_tabSpedNot = TabSpedNotBD.GetById(p_idAllegato, p_dbContext);

            string v_pathAvvisi = p_pathAvvisi;
            string v_codiceTipoDoc = v_tabSpedNot.tab_doc_output.tab_tipo_doc_entrate.cod_tipo_doc + "/";
            string v_annoRiferimento = v_tabSpedNot.tab_doc_output.anno.ToString() + "/";

            string v_nomeFile = v_tabSpedNot.barcode;
            string v_path = p_urlImg + v_pathAvvisi + p_codiceEnte + "/" + v_codiceTipoDoc + v_annoRiferimento + v_nomeFile + ".pdf";
            string v_pathCheck = p_uploadCheck + v_pathAvvisi + p_codiceEnte + "/" + v_codiceTipoDoc + v_annoRiferimento + v_nomeFile + ".pdf";

            if (!File.Exists(v_pathCheck.Replace("/", "\\\\")))
            {
                v_pathAvvisi = p_pathAvvisi;
                
                if(v_tabSpedNot.tab_avv_pag!=null)
                {
                    string v_codiceTipoAvvPag = v_tabSpedNot.tab_avv_pag.anagrafica_tipo_avv_pag.cod_tipo_avv_pag + "/";
                    v_annoRiferimento = v_tabSpedNot.tab_avv_pag.anno_riferimento + "/";
                    v_nomeFile = v_tabSpedNot.barcode;
                    v_path = p_urlImg + v_pathAvvisi + p_codiceEnte + "/" + v_codiceTipoAvvPag + v_annoRiferimento + v_nomeFile + ".pdf";
                }
            }

            return v_path;
        }

        public static string GetRelata(int p_idAllegato, string p_urlImg, string p_pathNotifiche, string codice_ente, dbEnte p_dbContext)
        {
            join_file v_joinFile = JoinFileBD.GetById(p_idAllegato, p_dbContext);
            tab_sped_not v_tabSpedNot = TabSpedNotBD.GetById(v_joinFile.id_riferimento, p_dbContext);

            string v_pathNotifiche = p_pathNotifiche;
            string v_codiceTipoAvvPag = v_tabSpedNot.tab_avv_pag.anagrafica_tipo_avv_pag.cod_tipo_avv_pag + "/";
            string v_annoRiferimento = v_tabSpedNot.tab_avv_pag.anno_riferimento + "/";
            string v_nomeFile = v_joinFile.nome_file;
            string v_path = p_urlImg + v_pathNotifiche + codice_ente + "/" + v_codiceTipoAvvPag + v_annoRiferimento + v_nomeFile;

            return v_path;
        }

        public static string GetProcura(string p_urlImg, string p_nomeFile, dbEnte p_dbContext)
        {
            string v_path = p_urlImg + "/Procure/" + p_nomeFile + ".pdf";

            return v_path;
        }

        public static String GetDocInput(int p_idDocInput, string p_urlImg, string p_pathDocInput, string codice_ente, dbEnte p_dbContext)
        {
            string v_path = p_urlImg + p_pathDocInput + codice_ente + "/" + p_idDocInput.ToString() + ".pdf";

            return v_path;
        }

        public static bool RemoteFileExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";

                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                //Returns TRUE if the Status code == 200
                response.Close();

                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.
                return false;
            }
        }

        /// <returns>
        /// Tuple<string, int> dove Item1 contiene il path, Item2 id_tab_allegati_digitali del nuovo record di tab_allegati_digitali
        /// </returns>
        public static Tuple<string, int> AllegaFile(string p_pathWithFile, tab_parametri_portale p_parametriPortale, dbEnte dbContext)
        {
            int v_idx1 = p_pathWithFile.LastIndexOf('.');
            string v_pathWithFileNameWithoutExtension = p_pathWithFile.Substring(0, v_idx1);

            int v_idx2 = v_pathWithFileNameWithoutExtension.LastIndexOf('\\');
            if (v_idx2 == -1)
            {
                v_idx2 = v_pathWithFileNameWithoutExtension.LastIndexOf('/');
            }

            string v_path = v_pathWithFileNameWithoutExtension.Substring(0, v_idx2 + 1);

            string v_fileNameWithoutExtension = v_pathWithFileNameWithoutExtension.Substring(v_idx2 + 1);

            int v_idTabDocAnagrafici = Convert.ToInt32(v_fileNameWithoutExtension);

            List<tab_allegati_digitali> TabDocAnagraficiAllegati = TabAllegatiDigitaliBD.GetList(dbContext)
                                                                                        .WhereByIdDocumento(v_idTabDocAnagrafici)
                                                                                        .OrderByDescending(d => d.data_creazione)
                                                                                        .ToList();

            if (TabDocAnagraficiAllegati.Count == 0)
            {
                v_fileNameWithoutExtension = v_fileNameWithoutExtension + "_1";
            }
            else
            {
                int v_index = TabDocAnagraficiAllegati.FirstOrDefault()
                                                      .nome_file
                                                      .LastIndexOf('_');

                if (v_index != -1)
                {
                    string v_progressivoString = TabDocAnagraficiAllegati.FirstOrDefault()
                                                                         .nome_file
                                                                         .Substring(v_index + 1);

                    int v_progressivoNext = Convert.ToInt32(v_progressivoString) + 1;

                    v_fileNameWithoutExtension = v_fileNameWithoutExtension + "_" + v_progressivoNext.ToString();
                }
                else
                {
                    v_fileNameWithoutExtension = v_fileNameWithoutExtension + "_1";
                }
            }

            tab_allegati_digitali v_docAnagraficiAllegati = new tab_allegati_digitali();
            string v_extension = Path.GetExtension(p_pathWithFile);

            if (!System.IO.File.Exists(v_path + v_fileNameWithoutExtension + v_extension) ||
                (System.IO.File.Exists(v_path + v_fileNameWithoutExtension + v_extension) && TabAllegatiDigitaliBD.GetList(dbContext)
                                                                                                                  .WhereByFileName(v_fileNameWithoutExtension)
                                                                                                                  .Count() == 0))
            {
                v_docAnagraficiAllegati.nome_file = v_fileNameWithoutExtension;
            }

            v_docAnagraficiAllegati.id_tab_documenti = v_idTabDocAnagrafici;
            v_docAnagraficiAllegati.formato_file = v_extension;
            if (v_path.StartsWith("\\\\\\\\"))
            {
                v_docAnagraficiAllegati.path_file = v_path.Replace("\\\\", "\\").Replace(p_parametriPortale.path_upload_file.Replace("\\\\", "\\"), "") + v_fileNameWithoutExtension + v_extension;
            }
            else
            {
                v_docAnagraficiAllegati.path_file = v_path.Replace(p_parametriPortale.path_upload_file.Replace("\\\\", "\\"), "") + v_fileNameWithoutExtension + v_extension;
            }
            v_docAnagraficiAllegati.data_creazione = DateTime.Now;

            dbContext.tab_allegati_digitali.Add(v_docAnagraficiAllegati);
            dbContext.SaveChanges();

            return new Tuple<string, int>(v_path + v_fileNameWithoutExtension + v_extension, v_docAnagraficiAllegati.id_tab_allegati_digitali);
        }

        public static string GetDocumentoForUpload(int p_idEnte, string p_idDocumento, string p_pathUploadFile, string p_pathTipo)
        {
            string v_filePath = BuildEntePath(p_idEnte) + "\\\\" + DateTime.Now.Year + "\\\\" + DateTime.Now.Month + "\\\\";
            string v_path = p_pathUploadFile + p_pathTipo.Replace("/", "\\\\") + v_filePath;

            string v_pathWithFile = v_path + p_idDocumento;

            return v_pathWithFile;
        }

        public static string GetDocumentoFascicoloForUpload(int p_idEnte, string p_idDocumento, int p_anno, string p_idFascicolo, string p_pathUploadFile, string p_pathTipo)
        {
            string v_filePath = BuildEntePath(p_idEnte) + "\\\\" + p_anno + "\\\\" + p_idFascicolo + "\\\\";
            string v_path = p_pathUploadFile + p_pathTipo.Replace("/", "\\\\") + v_filePath;

            string v_pathWithFile = v_path + p_idDocumento;

            return v_pathWithFile;
        }

        public static string GetIstanzaForUpload(int p_idEnte, int p_anno, string codTipoDocEntrata, string p_idDocumento, string p_pathUploadFile, string p_pathTipo)
        {
            string v_filePath = BuildEntePath(p_idEnte) + "\\\\" + codTipoDocEntrata + "\\\\" + p_anno + "\\\\";
            string v_path = p_pathUploadFile + p_pathTipo.Replace("/", "\\\\") + v_filePath;
            
            string v_pathWithFile = v_path + p_idDocumento;

            return v_pathWithFile;
        }
        public static string GetDenunceForDownload(int p_idEnte, int p_anno, string codTipoDocEntrata, string p_idDocumento, string p_pathUploadFile, string p_pathTipo)
        {
            string v_filePath = BuildEntePath(p_idEnte) + "\\\\" + codTipoDocEntrata + "\\\\" + p_anno + "\\\\";
            string v_path = p_pathUploadFile + p_pathTipo.Replace("/", "\\\\") + v_filePath;

            string v_pathWithFile = v_path + p_idDocumento;

            return v_pathWithFile;
        }
        public static string GetRendicontiBasePath(int id_ente, string p_pathUploadFile, string p_pathRendiconti)
        {
            // N.B.: UNC Ntework Path
            //string v_filePath = BuildEntePath(id_ente) + "\\\\" + DateTime.Now.Year + "\\\\" + DateTime.Now.Month + "\\\\";
            return Path.Combine(p_pathUploadFile.Replace(@"\\", @"\"), p_pathRendiconti.Replace("/", @"\"));
        }

        public static void controllaDirectory(string p_pathWithFile)
        {
            int v_idx1 = p_pathWithFile.LastIndexOf('.');
            string v_pathWithFileNameWithoutExtension = p_pathWithFile.Substring(0, v_idx1);

            int v_idx2 = v_pathWithFileNameWithoutExtension.LastIndexOf('\\');
            if (v_idx2 == -1)
            {
                v_idx2 = v_pathWithFileNameWithoutExtension.LastIndexOf('/');
            }

            string v_path = v_pathWithFileNameWithoutExtension.Substring(0, v_idx2 + 1);

            if (!Directory.Exists(v_path.Replace("\\\\\\\\", "\\\\")))
            {
                DirectoryInfo di = Directory.CreateDirectory(v_path.Replace("\\\\\\\\", "\\\\"));
            }
        }
    }
}
