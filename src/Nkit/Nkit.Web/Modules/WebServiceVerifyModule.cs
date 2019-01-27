using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utility.Log;

namespace WebBsm.App_Code
{
    public class WebServiceVerifyModule : IHttpModule
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += Context_BeginRequest;
            //throw new NotImplementedException();
        }
        
        private void Context_BeginRequest(object sender, EventArgs e)
        {
            //LogU.d("Context_BeginRequest");
            HttpApplication application = sender as HttpApplication;
            HttpRequest request = application.Request;
            string requestMethod = "";
            if (request.UrlReferrer != null && !string.IsNullOrEmpty(request.UrlReferrer.AbsolutePath))
            {
                //requestMethod = request.Url.AbsoluteUri.Split(new char[] { '/' }).Last();
            }
            else
            {
                if (request["HTTP_SOAPACTION"] != null)
                {
                    requestMethod = request["HTTP_SOAPACTION"].Replace("\"", "").Split(new char[] { '/' }).Last();
                    //LogU.d("Context_BeginRequest:requestMethod-"+ requestMethod);
                    if (!string.IsNullOrEmpty(requestMethod))
                    {
                        requestMethod = requestMethod.ToLower();

                        if (!ClientSessions.Instance.varifyIp(requestMethod))
                        {
                            //LogU.d("Context_BeginRequest:varifyIp");
                            application.CompleteRequest();
                            //application.Context.Response.ContentType = "text/xml";
                            application.Context.Response.Write("denied:request frequently");
                            //throw new Exception("denied:request frequently");

                        }
                    }
                }
            }            
        }
        
    }
}