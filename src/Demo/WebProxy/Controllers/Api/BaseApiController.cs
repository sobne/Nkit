using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Bts.Website.Controllers.Api
{
    //[ApiAuth]
    public class BaseApiController : ApiController
    {
        public HttpResponseMessage Json(object obj)
        {
            String str;
            if (obj is String || obj is Char)
            {
                str = obj.ToString();
            }
            else
            {
                str = JsonConvert.SerializeObject(obj, new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss" });
            }
            return new HttpResponseMessage { Content = new StringContent(str, Encoding.GetEncoding("UTF-8"), "application/json") };

        }
    }
}
