
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using WebProxy.Models;

namespace Bts.Website.Controllers.Api
{
    /// <summary>
    /// 行李再确认系统webapi
    /// </summary>
    [RoutePrefix("api/brs")]
    public class ReconciliationController : BaseApiController
    {
        public ReconciliationController()
        {

        }
        
        /// <summary>
        /// 获取行李信息,LED显示信息
        /// </summary>
        /// <param name="APCD">机场三字码</param>
        /// <param name="BTN">行李标签</param>
        /// <returns></returns>
        [Route("Baggage4Led")]
        [HttpGet]
        public HttpResponseMessage GetSingleBaggage4Led(string APCD, string BTN)
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
        /// <summary>
        /// 获取行李详细信息,可用于扫描查询调用,根据详情处理前端逻辑
        /// </summary>
        /// <param name="APCD">机场三字码</param>
        /// <param name="BTN">行李标签</param>
        /// <returns></returns>
        [Route("Baggage")]
        [HttpGet]
        public HttpResponseMessage GetSingleBaggage(string APCD, string BTN)
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
        

        /*
        [
        {
	        "Mandatory":true,
	        "APCD":"CKG",
	        "ClientId":"",
	        "UserName":"",
	        "AddedAttri":"",
	        "BTN":"",
	        "TrackingPoint":{
		        "APCD":"",
		        "TPNO":"",
		        "TPNM":"",
		        "TP":"INFL"
	        },
	        "SlotNo":"qq",
	        "Flight":{
		        "FLID":12134,
		        "AWCDFLNO":"",
		        "ORAP":"",
		        "DEAP":"",
		        "FLDT":"2019-09-09",
		        "FLIO":"A"
	        }
        },{
	        "Mandatory":true,
	        "APCD":"CKG",
	        "ClientId":"",
	        "UserName":"",
	        "AddedAttri":"",
	        "BTN":"",
	        "TrackingPoint":{
		        "APCD":"",
		        "TPNO":"",
		        "TPNM":"",
		        "TP":"OFFLOAD"
	        },
	        "SlotNo":"qq",
	        "Flight":{
		        "FLID":12133,
		        "AWCDFLNO":"",
		        "ORAP":"",
		        "DEAP":"",
		        "FLDT":"2019-09-09",
		        "FLIO":"A"
	        }
        }
        ]
        [{"APCD":"XNN","AddedAttri":"","BTN":"3781503417","CCode":"","ClientId":"2号离港转盘","ContainerID":"","Flight":{"AWCDFLNO":"","DEAP":"","FLIO":"D","ORAP":""},"GUID":"","IMAGES":"","Mandatory":false,"Memo":"","Passenger":"","SlotNo":"","TrackingPoint":{"APCD":"","TP":"SORT","TPNM":"2号离港转盘","TPNO":"2号离港转盘"},"UserName":""},{"APCD":"XNN","AddedAttri":"","BTN":"3781836958","CCode":"","ClientId":"2号离港转盘","ContainerID":"","Flight":{"AWCDFLNO":"","DEAP":"","FLIO":"D","ORAP":""},"GUID":"","IMAGES":"","Mandatory":false,"Memo":"","Passenger":"","SlotNo":"","TrackingPoint":{"APCD":"","TP":"SORT","TPNM":"2号离港转盘","TPNO":"2号离港转盘"},"UserName":""}]
        [{"Mandatory":false,"APCD":"XNN","ClientId":null,"UserName":null,"AddedAttri":null,"BTN":"3781602821","TrackingPoint":{"APCD":"XNN","TPNO":null,"TPNM":"RFID_XNN_Check5","TP":"ACCEPTANCE"},"Flight":{"AWCDFLNO":null,"ORAP":null,"DEAP":null,"FLDT":"0001-01-01","FLIO":"D"},"IMAGES":"/wb/CheckTicket/0001-01-01/RFID_XNN_Check5/3781602821_48936_20200926155500.jpg"}]
        */
        /// <summary>
        /// 行李再确认通用接口
        /// </summary>
        /// <param name="arrayObj">跟踪节点再确认信息,TracerReconciliation[]类的子类json序列化</param>
        /// <returns></returns>
        [Route("TracerReconciliate"),HttpPost]
        public HttpResponseMessage TracerReconciliate([FromBody]JArray arrayObj)
        {
            var json = JsonConvert.SerializeObject(arrayObj);
            if (!string.IsNullOrEmpty(json)) json = json.Replace("\"Mandatory\":false", "\"Mandatory\":true");

            string serverHost = ConfigurationManager.AppSettings["ServerUrl"].ToString();
            string url = Url.Request.RequestUri.PathAndQuery;
            string forWardUrl = serverHost + url;

            string result = HttpRequestHelper.HttpRequest(forWardUrl, "post", json);
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(result, Encoding.UTF8, "text/plain")
            };

        }
       

    }
}
