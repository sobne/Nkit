using System.Collections.Generic;

namespace Nkit.Net.Http
{
    public interface IHttpProvider
    {
        WebResponseParameter Excute(WebRequestParameter requestParameter);

        WebResponseParameter Get(string Url);
        WebResponseParameter Post(string Url, IDictionary<string, string> postData);
        WebResponseParameter Post(string Url, string json);
    }
}
