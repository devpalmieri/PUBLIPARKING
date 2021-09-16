using Publiparking.Data;
using Publiparking.Data.BD;
using Publiparking.Service.Base;
using Publisoftware.Data;
using Publisoftware.Data.BD;
using Publisoftware.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;

namespace Publiparking.Web.NinoServer
{
    public static class Utility
    {
        public static char[] decifraturaSeparator = new char[] { '+' };

        public static Dictionary<string, string> errorMsg = new Dictionary<string, string> {
                { "incorrect_cvc", "Il codice di sicurezza della carta è errato" },
                { "incorrect_number", "Il numero di carta è errato" },
                { "incorrect_zip", "Il codice postale associato alla carta è errato" },
                { "invalid_cvc", "Il codice di sicurezza della carta è invalido" },
                { "invalid_expiry_month", "Il mese di scadenza della carta è invalido" },
                { "invalid_expiry_year", "L'anno di scadenza della carta è invalido" },
                { "invalid_number", "Il numero di carta non è un numero di carta valido" },
                { "card_declined", "Il pagamento è stato rifiutato" },
                { "expired_card", "La carta è scaduta" },
                { "processing_error", "Errore durante il processamento del pagamento" }
        };

        public static dbEnte getGeneraleCtx()
        {
            try
            {
                
                //string v_dbServer = ConfigurationManager.AppSettings["dbServer"].ToString();
                //string v_dbName = ConfigurationManager.AppSettings["dbName"].ToString();
                //string v_dbUserName = ConfigurationManager.AppSettings["dbUserName"].ToString();
                //string v_dbPassWord = ConfigurationManager.AppSettings["dbPassWord"].ToString();
                //Data.BD.DBInfos DbInfo = new Data.BD.DBInfos(v_dbServer, v_dbName, v_dbUserName, v_dbPassWord);

                return MvcApplication.DbInfo.GetEnteCtx();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string getValue(string p_value)
        {
            string v_return = "";
            if (!String.IsNullOrWhiteSpace(p_value))
            {
                v_return = p_value;
            }
            return v_return;
        }
        public static DbParkCtx getEnteCtx(int p_idente, dbEnte p_ctxGenerale)
        {
            try
            {
                anagrafica_ente v_ente = AnagraficaEnteBD.GetById(p_idente, p_ctxGenerale);
                


                if (v_ente != null)
                {
                    string v_dbServer = ConfigurationManager.AppSettings["dbServer"].ToString();
                    string v_dbName = ConfigurationManager.AppSettings["dbName"].ToString();
                    string v_dbUserName = ConfigurationManager.AppSettings["dbUserName"].ToString();
                    string v_dbPassWord = ConfigurationManager.AppSettings["dbPassWord"].ToString();

                    AdoHelper v_helper = new AdoHelper();
                    Data.BD.DBInfos DbInfo = null;
                    try
                    {
                        DbInfo = v_helper.getDbSettorialiParkInfo(v_dbServer, v_dbName, v_dbUserName, v_dbPassWord).Where(s => s.DbName == v_ente.nome_db).FirstOrDefault();
                    }
                    catch (Exception exception)
                    {
                        //logger.LogException("Errore nella lettura della lista degli enti", exception, EnLogSeverity.Error);
                    }
                    return DbInfo.GetParkCtx();
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

      
        public static string returnBadRequest(HttpResponseBase p_response)
        {

            p_response.StatusCode = (int)HttpStatusCode.BadRequest;
            p_response.TrySkipIisCustomErrors = true;
            return "400";
        }
        public static string getEnteStringConnection(int p_idente, dbEnte p_ctxGenerale)
        {
            string v_strConnection = "";
            string v_strIpAddress = "";
            try
            {
                anagrafica_ente v_ente = AnagraficaEnteBD.GetById(p_idente, p_ctxGenerale);
                if (v_ente != null)
                {
                    //solo debug
                    //if (v_ente.indirizzo_ip_db == "10.2.1.102") { v_strIpAddress = "10.1.1.246"; } else { v_strIpAddress = "10.1.1.254"; }
                    //v_strConnection = "Data Source=" + v_strIpAddress + ";Initial Catalog=" + v_ente.nome_db + ";Persist Security Info=True;User ID=" + v_ente.user_name_db + ";Password=" + CryptRijndael.Decode(v_ente.password_db);
                    //in esercizio
                    //v_strConnection = "Data Source=" + v_ente.indirizzo_ip_db + ";Initial Catalog=" + v_ente.nome_db + ";Persist Security Info=True;User ID=" + v_ente.user_name_db + ";Password=" + CryptRijndael.Decode(v_ente.password_db);                }
                    v_strConnection = "Data Source=" + v_ente.indirizzo_ip_db + ";Initial Catalog=" + v_ente.nome_db + ";Persist Security Info=True;User ID=" + v_ente.user_name_db + ";Password=" + CryptMD5.Decrypt(v_ente.password_db); //CryptRijndael.Decode(v_ente.password_db);
                }


                return v_strConnection;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        //converte unixtimestamp passati in secondi in datetime
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        //provare se funzione la precedente o qst commentata
        //public static DateTime UnixTimestampToDateTime(double unixTime)
        //{
        //    DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        //    long unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
        //    return new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc);
        //}
        //converte datetime in unixtimestamp 
        public static double DateTimeToUnixTimestamp(DateTime dateTime)
        {
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            long unixTimeStampInTicks = (dateTime.ToUniversalTime() - unixStart).Ticks;
            //return (double)unixTimeStampInTicks / TimeSpan.TicksPerSecond;
            return (double)unixTimeStampInTicks / TimeSpan.TicksPerSecond;
        }
        public static long DateTimeToUnixTimestampLong(DateTime dateTime)
        {
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            long unixTimeStampInTicks = (dateTime.ToUniversalTime() - unixStart).Ticks;
            //return (double)unixTimeStampInTicks / TimeSpan.TicksPerSecond;
            return (long)unixTimeStampInTicks / TimeSpan.TicksPerSecond;
        }
        public static Boolean VerificaCorrettezzaNTelefono(ref string p_nTelefono)
        {
            Boolean v_bResult = false;
            //Regex r = new Regex(@"^\+?\d{0,2}\-?\d{4,5}\-?\d{5,6}");
            //Regex r = new Regex(@"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$");
            //^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$
            Regex v_regEx = new Regex(@"^\+\d{12}");
            try
            {
                if (!String.IsNullOrEmpty(p_nTelefono) && !String.IsNullOrWhiteSpace(p_nTelefono))
                {
                    p_nTelefono = p_nTelefono.Replace("%2B", "+");
                    if (p_nTelefono.Length == 13)
                    {
                        if (p_nTelefono.Substring(0, 4) == "+393")
                        {
                            if (v_regEx.IsMatch(p_nTelefono)) { v_bResult = true; }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                v_bResult = false;
            }
            return v_bResult;
        }
        public static Boolean VerificaCorrettezzaImporto(string p_imorto)
        {
            Boolean v_bResult = false;

            try
            {
                return v_bResult;
            }
            catch (Exception ex)
            {
                return v_bResult;
            }
        }
        public static Boolean VerificaCorrettezzaNAbbonamento(string p_nAbbonamento)
        {

            Boolean v_bResult = false;
            long v_codiceAbbonamento;
            try
            {
                if (p_nAbbonamento.Length == 12)
                {
                    if (!String.IsNullOrEmpty(p_nAbbonamento) && !String.IsNullOrWhiteSpace(p_nAbbonamento))
                    {
                        if (long.TryParse(p_nAbbonamento, out v_codiceAbbonamento))
                        {
                            v_bResult = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                v_bResult = false;
            }
            return v_bResult;
        }
        public static Boolean VerificaCorrettezzaCodiceComune(string p_codiceComune)
        {

            Boolean v_bResult = false;
            long v_codiceNumerico;
            try
            {
                if (p_codiceComune.Length >= 1)
                {
                    if (!String.IsNullOrEmpty(p_codiceComune) && !String.IsNullOrWhiteSpace(p_codiceComune))
                    {
                        if (long.TryParse(p_codiceComune, out v_codiceNumerico))
                        {
                            if (v_codiceNumerico > 0)
                            {
                                v_bResult = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                v_bResult = false;
            }
            return v_bResult;
        }
        //----decifratura
        private static byte[] getKey()
        {
            byte[] salt = new byte[] { 84, 119, 25, 56, 100, 100, 120, 45, 84, 67, 96, 10, 24, 111, 112, 119, 3 };
            int iterations = 1024;
            //iv: "publiparking__iv".getBytes("UTF-8")
            var rfc2898 =
            new System.Security.Cryptography.Rfc2898DeriveBytes("PubliparkingPassword", salt, iterations);
            byte[] key = rfc2898.GetBytes(16);
            return key;
        }
        public static string getStringParameters(string p_id, string p_iv)
        {
            byte[] decoded_id = Convert.FromBase64String(p_id);
            byte[] decoded_iv = Convert.FromBase64String(p_iv);
            try
            {
                AesManaged aesCipher = new AesManaged();
                aesCipher.KeySize = 128;
                aesCipher.BlockSize = 128;
                aesCipher.Key = getKey();//  getkey();
                aesCipher.Mode = CipherMode.CBC;
                aesCipher.Padding = PaddingMode.PKCS7;
                aesCipher.IV = decoded_iv;
                ICryptoTransform decryptTransform = aesCipher.CreateDecryptor();
                byte[] plainTextBytes = decryptTransform.TransformFinalBlock(decoded_id, 0, decoded_id.Length);
                String plaintext = System.Text.Encoding.UTF8.GetString(plainTextBytes);
                System.Console.WriteLine("Decrypted: ", plaintext);
                return plaintext;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static string getModalitaPagamento(Int32 p_idEnte, dbEnte p_ctxGenerale)
        {
            string risp = "";
            try
            {
                DbParkCtx v_ctx = Utility.getEnteCtx(p_idEnte, p_ctxGenerale);           
                if (v_ctx != null)
                {                                     
                    Configurazione vconfig = ConfigurazioneBD.GetList(v_ctx).FirstOrDefault();

                    if (vconfig.pagamentoConTarga)
                    {
                        risp = "TARGA";
                    }
                    else
                    {
                        risp = "STALLO";
                    }
                }
                return risp;
            }
            catch (Exception ex)
            {
                return risp;
            }
        }
    }
}