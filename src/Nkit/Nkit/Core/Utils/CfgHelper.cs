using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Nkit.Core.Utils
{
    public class CfgHelper
    {
        public static class AppSetting
        {
            public static string GetValue(string key)
            {
                return ConfigurationManager.AppSettings[key].ToString();
            }
            public static T Get<T>(string key)
            {
                var val = GetValue(key);
                if (val != null)
                    return (T)Convert.ChangeType(val,typeof(T));
                else
                    return default(T);
            }
            public static T TryGet<T>(string key, T defaultValue)
            {
                try
                {
                    object obj = GetValue(key);
                    if (obj != null)
                        return (T)obj;
                }
                catch (Exception)
                {

                }
                return defaultValue;
            }
        }

        public static string GetConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }
    }
    
}
