using System;
using System.Collections.Generic;
using System.Text;

namespace Publiparking.Core.Data.SqlServer.dto.type
{
    using System;

    public class StatoStalloExt
    {
        private string m_Stato = "";
        public string Stato
        {
            get
            {
                return m_Stato;
            }
            set
            {
                m_Stato = value;
            }
        }

        private string m_Note = "";
        public string Note
        {
            get
            {
                return m_Note;
            }
            set
            {
                m_Note = value;
            }
        }

        private string m_targa = "";
        public string Targa
        {
            get
            {
                return m_targa;
            }
            set
            {
                m_targa = value;
            }
        }

        private DateTime m_data;
        public DateTime Data
        {
            get
            {
                return m_data;
            }
            set
            {
                m_data = value;
            }
        }
    }

}
