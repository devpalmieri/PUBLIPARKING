using System;
using System.Collections.Generic;
using System.Text;

namespace Publiparking.Data.dto.type
{
    public class WSReturn
    {
        private bool vEseguito = false;
        public bool eseguito
        {
            get
            {
                return vEseguito;
            }
            set
            {
                vEseguito = value;
            }
        }

        private string vMessaggio = null;
        public string messaggio
        {
            get
            {
                return vMessaggio;
            }
            set
            {
                vMessaggio = value;
            }
        }

        private bool vContinua = false;
        public bool continua
        {
            get
            {
                return vContinua;
            }
            set
            {
                vContinua = value;
            }
        }

        private string vRisultato = "";
        public string risultato
        {
            get
            {
                return vRisultato;
            }
            set
            {
                vRisultato = value;
            }
        }
    }

}
