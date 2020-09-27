using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using WebProxy.Models;

namespace Bts.Website.Controllers.Api
{
    [RoutePrefix("api/sys")]
    public class SysController : ApiController
    {
        /// <summary>
        /// 获取Token凭证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [Route("user/GetToken")]
        [HttpGet]
        public HttpResponseMessage GetToken(string username, string password)
        {
            string serverHost = ConfigurationManager.AppSettings["ServerUrl"].ToString();
            string url = Url.Request.RequestUri.PathAndQuery;
            string forWardUrl = serverHost + url;

            string result = HttpRequestHelper.HttpRequest(forWardUrl, "get", string.Empty);
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(result, Encoding.UTF8, "text/plain")
            };

        }
    }
}
