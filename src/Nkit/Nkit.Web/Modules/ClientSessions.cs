using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utility;
using Utility.Log;

namespace WebBsm.App_Code
{
    public class ClientSessions
    {
        private static readonly object Locker = new object();
        private static ClientSessions _instance;
        private static ConcurrentDictionary<string, DateTime> clients = new ConcurrentDictionary<string, DateTime>();
        public static ClientSessions Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new ClientSessions();
                        }
                    }
                }
                return _instance;
            }
        }
        private ClientSessions()
        {

        }
        private double frequent = -1;
        private double getFrequent()
        {
            if(frequent == -1)
            {
                frequent = Cfg.GetDouble("frequent", 2);
            }
            //LogU.d("ClientSession - frequent:" + frequent);
            return frequent;
        }
        public bool varifyIp(string name = "")
        {
            var exits = true;
            var key = name+"_"+ getIp();
            //LogU.d("ClientSession - key:" + key);
            if (clients.ContainsKey(key))
            {
                var time = clients[key];
                var timestamp = (DateTime.Now - time).TotalSeconds;
                //LogU.d("ClientSession - timestamp:" + timestamp);
                if (timestamp < getFrequent())
                {
                    exits = false;
                }
                else {
                    clients[key] = DateTime.Now;
                }
            }
            else
            {
                LogU.d("ClientSession: key - " + key );
                clients.TryAdd(key, DateTime.Now);
            }
            return exits;
        }
        private bool verifyRequest(string requestMethod)
        {
            var verified = true;
            if (pages.Any(x => requestMethod.StartsWith(x)))
            {
                verified = varifyIp();
            }
            return verified;
        }
        private string[] pages = new string[] { "messages.asmx" };
        private static string getIp()
        {
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                return System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(new char[] { ',' })[0];
            else
                return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}