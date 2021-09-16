using Publiparking.Data.dto;
using Publiparking.Data.dto.type;
using Publiparking.Data.LinqExtended;
using Publisoftware.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Publiparking.Data.BD
{
    public class OperatoriBD : EntityBD<Operatori>
    {
        public OperatoriBD()
        {

        }

        public static bool isAttivo(string pUserName, DbParkCtx v_context)
        {
            bool risp = false;

            Operatori v_operatore = GetList(v_context).Where(o => o.username.Equals(pUserName))
                .OrderByDescending(o=> o.idOperatore)
                .FirstOrDefault();
            if (v_operatore != null && v_operatore.attivo)
            {
                risp = true;
            }

            return risp;
        }

        public static bool verifyPassword(string pUserName, string pPassword, DbParkCtx v_context)
        {
            bool risp = false;

            Operatori v_operatore = GetList(v_context).Where(o => o.username.Equals(pUserName))
                .OrderByDescending(o => o.idOperatore)
                .FirstOrDefault();
            if (v_operatore != null)
            {              
                if (CryptMD5.VerifyMd5Hash(pPassword, v_operatore.password))
                {
                    risp = true;
                }
            }

            return risp;
        }
        public static bool changePassword(string pUserName, string pOldPassword, string pNewPassword, DbParkCtx v_context)
        {
            bool risp = false;

            if (verifyPassword(pUserName, pOldPassword,v_context))
            {
                Operatori v_operatore = GetList(v_context).Where(o => o.username.Equals(pUserName))
                .OrderByDescending(o => o.idOperatore)
                .FirstOrDefault();

                string v_nuovapasswordCrypt = CryptMD5.getMD5(pNewPassword);

                v_operatore.password = v_nuovapasswordCrypt;
                v_operatore.dataCambioPassword = DateTime.Now;
                v_context.SaveChanges();
                risp = true;
            }

            return risp;
        }

        public static bool isExpiredPassword(string pUserName,int pnumGiorniValiditaPassword, DbParkCtx v_context)
        {
            bool risp = false;

            Operatori v_operatore = GetList(v_context).Where(o => o.username.Equals(pUserName))
                .OrderByDescending(o => o.idOperatore)
                .FirstOrDefault();
            if (v_operatore != null)
            {
                if (v_operatore.dataCambioPassword.AddDays(pnumGiorniValiditaPassword) < DateTime.Now)
                {
                    risp = true;
                }                
            }

            return risp;
        }

        public static OperazioneDTO loadLastByIdOperatore(Int32 pIdOperatore, DbParkCtx v_context)
        {
            OperazioneDTO risp = null;
            
            risp = OperazioniLocalBD.GetList(v_context)
                        .Where(o => o.idOperatore.Equals(pIdOperatore))
                        .OrderByDescending(o => o.data).FirstOrDefault().ToOperazioneDTO();

            return risp;


        }

        public static OperazioneParamRequired nextOperazioneParamRequired(Int32 pIdOperatore, Int32 pIdStallo, DbParkCtx v_context)
        {
            OperazioneParamRequired risp = new OperazioneParamRequired();

            Operatori vOperatore = OperatoriBD.GetById(pIdOperatore, v_context);
            Stalli vStallo = StalliBD.GetById(pIdStallo,v_context);
            if (vOperatore != null)
            {
                if (vOperatore.noGpsOperazioni == 0)
                {
                    risp.GPS = true;
                }
                else
                {
                    risp.GPS = false;
                }

                risp.foto = vStallo.fotoRichiesta;
            }

            return risp;
        }

        public static void decreaseNoGps(Int32 pIdOperatore, DbParkCtx v_context)
        {
            Operatori v_operatore = OperatoriBD.GetById(pIdOperatore, v_context);
            if (v_operatore != null && v_operatore.noGpsOperazioni > 0)
            {
                v_operatore.noGpsOperazioni = v_operatore.noGpsOperazioni - 1;
            }
        }
    }
}
