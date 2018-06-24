using System;
using System.Configuration;

namespace Nkit.Db.Ado
{
    public class ConfigHelper
    {
        public static string dbtype
        {
            get { return GetAppSetting("dbtype"); }
        }
        public static string ConnectionString
        {
            get { return GetConnectionString(dbtype); }
        }
        /// <summary>
        /// 取AppSettings键值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        private static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        /// <summary>
        /// 取ConnectionStrings键值
        /// </summary>
        /// <param name="name">键</param>
        /// <returns>值</returns>
        private static string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
