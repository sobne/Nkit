using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Nkit.Utils
{
    public class CfgHelper
    {
        public static string GetAppSetting(string name)
        {
            return ConfigurationManager.AppSettings[name].ToString();
        }
    }
}
