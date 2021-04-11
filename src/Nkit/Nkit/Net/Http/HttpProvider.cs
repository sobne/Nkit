using System.Collections.Generic;

namespace Nkit.Net.Http
{
    public class HttpProvider:IHttpProvider
    {
        public WebResponseParameter Excute(WebRequestParameter requestParameter)
        {
            return HttpUtil.Excute(requestParameter);
        }
        public WebResponseParameter Get(string Url)
        {
            WebResponseParameter response = HttpUtil.Excute(new WebRequestParameter
            {
                Url = Url,
                Method =  HttpMethod.GET
            });
            return response;
        }
        /*
         IDictionary<string, string> postData = new Dictionary<string, string>();
            postData.Add("userName", "登录用户名");
            postData.Add("userPwd", "用户名密码");
        */
        public WebResponseParameter Post(string Url, IDictionary<string, string> postData)
        {
            WebResponseParameter response = HttpUtil.Excute(new WebRequestParameter
            {
                Url = Url,
                Method = HttpMethod.POST,
                ContentType = HttpContentType.json,
                Parameters = postData
            });
            return response;
        }
        public WebResponseParameter Post(string Url, string json)
        {
            WebResponseParameter response = HttpUtil.Excute(new WebRequestParameter
            {
                Url = Url,
                Method = HttpMethod.POST,
                ContentType = HttpContentType.json,
                Json = json
            });
            return response;
        }
    }
}
