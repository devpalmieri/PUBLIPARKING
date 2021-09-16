using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace Publiparking.Core.Utility
{
    public class MailHelper
    {
        static MailHelper _instance = null;
        private static Object _lock = new Object();
        public static MailHelper Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new MailHelper();
                    }
                    return _instance;
                }
            }
        }

        public static string ConvertHtmlFileToString(string p_fileNameFullPath)
        {
            var v_reader = "";
            if (!File.Exists(p_fileNameFullPath))
            {
                return v_reader;
            }
            using (StreamReader sr = new StreamReader(p_fileNameFullPath))
            {
                v_reader = sr.ReadToEnd();
            }
            return v_reader;
        }

        //public static Stream ConvertImageToStream(string p_imageFullPath)
        //{
        //    Stream v_imgStream = null;
        //    try
        //    {
        //        System.Drawing. image = Image.FromFile(p_imageFullPath);

        //        ImageFormat format = image.RawFormat;

        //        if (format.Equals(ImageFormat.Jpeg))
        //        {
        //            image.Save(v_imgStream, ImageFormat.Jpeg);
        //        }
        //        else if (format.Equals(ImageFormat.Png))
        //        {
        //            image.Save(v_imgStream, ImageFormat.Png);
        //        }
        //        else if (format.Equals(ImageFormat.Bmp))
        //        {
        //            image.Save(v_imgStream, ImageFormat.Bmp);
        //        }
        //        else if (format.Equals(ImageFormat.Gif))
        //        {
        //            image.Save(v_imgStream, ImageFormat.Gif);
        //        }
        //        else if (format.Equals(ImageFormat.Icon))
        //        {
        //            image.Save(v_imgStream, ImageFormat.Icon);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //    return v_imgStream;
        //}

        public IMailSender GetSender(string SmtpHost, string Mail, string Password) { return new BaseMailSender(SmtpHost, Mail, Password); }
    }
}