using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Utility
{
    public static class AppSettings
    {

        public static T Get<T>(string key)
        {
            var appSetting = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(appSetting)) throw new Exception(key);

            var converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)(converter.ConvertFromInvariantString(appSetting));
        }
    }
}
//Sandro: utilizzo della classe
//<add key = "IsDebug" value="true" />
//  <add key = "MyEnumValue" value="1" />
//  <add key = "MyEnumValue2" value="A" />
//  <add key = "ServiceEndpoint" value="http://www.example.com" />
//  <add key = "MyFavColor" value="Red" />
//</appSettings>

//var isDebug = AppSettings.Get<bool>("IsDebug");
//var myEnumValue = AppSettings.Get<TestEnum>("MyEnumValue");
//var myEnumValue2 = AppSettings.Get<TestEnum>("MyEnumValue2");
//var myFavColor = AppSettings.Get<Color>("MyFavColor");
//var serviceEndpoint = AppSettings.Get<Uri>("ServiceEndpoint");
