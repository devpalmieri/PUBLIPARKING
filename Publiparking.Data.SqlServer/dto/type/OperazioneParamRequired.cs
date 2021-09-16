using System;
using System.Collections.Generic;
using System.Text;

namespace Publiparking.Data.dto.type
{
    public class OperazioneParamRequired
    {
        private bool m_GPS = true;
        public bool GPS
        {
            get
            {
                return m_GPS;
            }
            set
            {
                m_GPS = value;
            }
        }

        private bool m_foto = true;
        public bool foto
        {
            get
            {
                return m_foto;
            }
            set
            {
                m_foto = value;
            }
        }
    }
}
