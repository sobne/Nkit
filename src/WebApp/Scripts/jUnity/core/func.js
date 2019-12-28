
function addScript(url){
	document.write("<script language=javascript src="+url+"></script>");
}
function createScript(url){
	var script = document.createElement('script');
	script.setAttribute('type','text/javascript');
	script.setAttribute('src',url);
	document.getElementsByTagName('head')[0].appendChild(script);
}



//screenresize
function screenresize() {
    //    self.moveTo(0,0);
    //    self.resizeTo(screen.availWidth,screen.availHeight);
    window.moveTo(0, 0);
    window.resizeTo(screen.width, screen.height - 30);
}
//isEmptyObject
function isEmptyObject(obj) {
    for (var name in obj) {
        return false;
    }
    return true;
}


//Request BEGIN-------------------------------------------------------------------

//GET parameter of url
function getUrlParameter(url, name) {
    if (url.indexOf("?") == -1)
        return null;
    var paramStr = url.substring(url.indexOf("?"));
    if (paramStr.length == 0)
        return null;
    if (paramStr.charAt(0) != '?')
        return null;
    paramStr = unescape(paramStr);
    paramStr = paramStr.substring(1);
    if (paramStr.length == 0)
        return null;
    var params = paramStr.split('&');
    for (var i = 0; i < params.length; i++) {
        var parts = params[i].split('=', 2);
        if (parts[0] == name) {
            if (parts.length < 2 || typeof (parts[1]) == "undefined" || parts[1] == "undefined" || parts[1] == "null")
                return "";
            return parts[1];
        }
    }
    return null;
}
//GET request parameter
function getParameter(name) {
    var paramStr = location.search;
    if (paramStr.length == 0)
        return null;
    if (paramStr.charAt(0) != '?')
        return null;
    paramStr = unescape(paramStr);
    paramStr = paramStr.substring(1);
    if (paramStr.length == 0)
        return null;
    var params = paramStr.split('&');
    for (var i = 0; i < params.length; i++) {
        var parts = params[i].split('=', 2);
        if (parts[0] == name) {
            if (parts.length < 2 || typeof (parts[1]) == "undefined" || parts[1] == "undefined" || parts[1] == "null")
                return "";
            return parts[1];
        }
    }
    return null;
}
//var id = getUrlParam("id");
function getUrlParam(name) {
	var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
	var r = window.location.search.substr(1).match(reg);
	if (r != null) return unescape(r[2]); return null;
}
//var id = getQueryVariable("id");
function getQueryVariable(variable) {
	var query = window.location.search.substring(1);
	var vars = query.split("&");
	for (var i = 0; i < vars.length; i++) {
		var pair = vars[i].split("=");
		if (pair[0] == variable) { return pair[1]; }
	}
	return (false);
}

//getLocalIP(function(ip){});
function getLocalIP(callback){
	var RTCPeerConnection = window.RTCPeerConnection || window.webkitRTCPeerConnection || window.mozRTCPeerConnection;
	if (RTCPeerConnection) (function () {
		var rtc = new RTCPeerConnection({iceServers:[]});
		if (1 || window.mozRTCPeerConnection) {    
			rtc.createDataChannel('', {reliable:false});
		};
		 
		rtc.onicecandidate = function (evt) {
			if (evt.candidate) grepSDP("a="+evt.candidate.candidate);
		};
		rtc.createOffer(function (offerDesc) {
			grepSDP(offerDesc.sdp);
			rtc.setLocalDescription(offerDesc);
		}, function (e) { console.warn("offer failed", e); });
		 
		 
		var addrs = Object.create(null);
		addrs["0.0.0.0"] = false;
		function updateDisplay(newAddr) {
			if (newAddr in addrs) return;
			else addrs[newAddr] = true;
			var displayAddrs = Object.keys(addrs).filter(function (k) { return addrs[k]; });
			for(var i = 0; i < displayAddrs.length; i++){
				if(displayAddrs[i].length > 16){
					displayAddrs.splice(i, 1);
					i--;
				}
			}
			//document.getElementById(fieldId).textContent = displayAddrs[0];
			if(callback){
				callback(displayAddrs[0]);
			}
			console.log(displayAddrs[0]);
		}
		 
		function grepSDP(sdp) {
			var hosts = [];
			sdp.split('\r\n').forEach(function (line, index, arr) {
			   if (~line.indexOf("a=candidate")) {   
					var parts = line.split(' '),      
						addr = parts[4],
						type = parts[7];
					if (type === 'host') updateDisplay(addr);
				} else if (~line.indexOf("c=")) {      
					var parts = line.split(' '),
						addr = parts[2];
					updateDisplay(addr);
				}
			});
		}
	})();
	else{
		console.log("请使用主流浏览器：chrome,firefox,opera,safari");
	}
}
//getBrowserInfo
function getBrowserInfo(){
	var agent = navigator.userAgent.toLowerCase() ;
	console.log(agent);
	var arr = [];
	var system = agent.split(' ')[1].split(' ')[0].split('(')[1];
	arr.push(system);
	var regStr_edge = /edge\/[\d.]+/gi;
	var regStr_ie = /trident\/[\d.]+/gi ;
	var regStr_ff = /firefox\/[\d.]+/gi;
	var regStr_chrome = /chrome\/[\d.]+/gi ;
	var regStr_saf = /safari\/[\d.]+/gi ;
	var regStr_opera = /opr\/[\d.]+/gi;
	//IE
	if(agent.indexOf("trident") > 0){
		arr.push(agent.match(regStr_ie)[0].split('/')[0]);
		arr.push(agent.match(regStr_ie)[0].split('/')[1]);
		return arr;
	}
	//Edge
	if(agent.indexOf('edge') > 0){
		arr.push(agent.match(regStr_edge)[0].split('/')[0]);
		arr.push(agent.match(regStr_edge)[0].split('/')[1]);
		return arr;
	}
	//firefox
	if(agent.indexOf("firefox") > 0){
		arr.push(agent.match(regStr_ff)[0].split('/')[0]);
		arr.push(agent.match(regStr_ff)[0].split('/')[1]);
		return arr;
	}
	//Opera
	if(agent.indexOf("opr")>0){
		arr.push(agent.match(regStr_opera)[0].split('/')[0]);
		arr.push(agent.match(regStr_opera)[0].split('/')[1]);
		return arr;
	}
	//Safari
	if(agent.indexOf("safari") > 0 && agent.indexOf("chrome") < 0){
		arr.push(agent.match(regStr_saf)[0].split('/')[0]);
		arr.push(agent.match(regStr_saf)[0].split('/')[1]);
		return arr;
	}
	//Chrome
	if(agent.indexOf("chrome") > 0){
		arr.push(agent.match(regStr_chrome)[0].split('/')[0]);
		arr.push(agent.match(regStr_chrome)[0].split('/')[1]);
		return arr;
	}else{
		arr.push('请更换主流浏览器，例如chrome,firefox,opera,safari,IE,Edge!')
		return arr;
	}
}
//Request END-------------------------------------------------------------------


//String BEGIN-------------------------------------------------------------------

//Replace全部字符
function replaceAll(originStr,searchStr,replaceStr){
	var regExp = new RegExp(searchStr,"g");
	return originStr.replace(regExp,replaceStr);
}
//Trim前后空格
function Trim(str)
{
    return str.replace(/(^\s*)|(\s*$)/g,"");
}
//Get字符长度,1中文2个长度
function GetStrLength(str){
	//return str.replace.(/[\u4e00-\u9fa5]+/g,"**").length;
    var len=0;
    if(str==null||str.length==0)
        return 0;
    str=Trim(str);
    for(i=0;i<str.length;i++){
        if(str.charCodeAt(i)>0&&str.charCodeAt(i)<128)
            len++;
        else 
            len+=2;
    }
    return len;
} 
function GetLengthOfStr(str, bChinese) {
	if (bChinese != undefined && bChinese === false) {
		return str.length;
	}
	else {
		var chineseRegex = /[\u4e00-\u9fa5]/g;
		if (chineseRegex.test(str)) {
			return str.replace(chineseRegex, "xx").length;
		}
		return str.length;
	}
}
//StartWith
function StartWith(str,searchStr){
    if(searchStr==null||searchStr==""||str.length==0||searchStr.length>str.length)
        return false;
    if(str.substr(0,searchStr.length)==searchStr)
        return true;
    else
        return false;
 return true;
}
//EndWith
function EndWith(str,searchStr) {
    if(searchStr==null||searchStr==""||str.length==0||searchStr.length>str.length)
        return false;
    if (str.substring(str.length - searchStr.length) == searchStr)
        return true;
    else
        return false;
    return true;
}

//html编码:把<>和空格符转换成html编码
function HTMLEncode(str) {
    var s = "";
    if (str.length == 0) return "";
    s = str.replace(/&/g, "&amp;");
    s = s.replace(/</g, "&lt;");
    s = s.replace(/>/g, "&gt;");
    s = s.replace(/ /g, "&nbsp;");
    s = s.replace(/\'/g, "&#39;").replace(/\"/g,"&#34;");
    s = s.replace(/\"/g, "&quot;");
    return s;
}
//html解码
function HTMLDecode(str) {
    var s = "";
    if (str.length == 0) return "";
    s = str.replace(/&amp;/g, "&");
    s = s.replace(/&lt;/g, "<");
    s = s.replace(/&gt;/g, ">");
    s = s.replace(/&nbsp;/g, " ");
    s = s.replace(/&#39;/g, "\'");
    s = s.replace(/&quot;/g, "\"");
    return s;
}
//
function replaceURiWithHTMLLinks(sText, bBlank) {
	var pattern = /(\b(https?|ftp|file):\/\/[-A-Z0-9+&@#\/%?=~_|!:,.;]*[-A-Z0-9+&@#\/%=~_|])/ig;
	if (bBlank) {
		sText = sText.replace(pattern, "<a target='_blank' href='$1'>$1</a>");
	}
	else {
		sText = sText.replace(pattern, "<a href='$1'>$1</a>");
	}
	return sText;
}

//String END-------------------------------------------------------------------



// Number BEGIN----------------------------------------------------------------
//accDiv: arg1/arg2
//return: arg1除以arg2的精确结果
function accDiv(arg1,arg2){ 
    var t1=0,t2=0,r1,r2; 
    try{t1=arg1.toString().split(".")[1].length}catch(e){} 
    try{t2=arg2.toString().split(".")[1].length}catch(e){} 
    with(Math){ 
        r1=Number(arg1.toString().replace(".","")) 
        r2=Number(arg2.toString().replace(".","")) 
        return (r1/r2)*pow(10,t2-t1); 
    } 
} 
//accMul: arg1*arg2
//return: arg1乘以arg2的精确结果
function accMul(arg1,arg2) 
{ 
    var m=0,s1=arg1.toString(),s2=arg2.toString(); 
    try{m+=s1.split(".")[1].length}catch(e){} 
    try{m+=s2.split(".")[1].length}catch(e){} 
    return Number(s1.replace(".",""))*Number(s2.replace(".",""))/Math.pow(10,m) 
} 
//accAdd: arg1+arg2
//return：arg1加上arg2的精确结果 
function accAdd(arg1,arg2){ 
    var r1,r2,m; 
    try{r1=arg1.toString().split(".")[1].length}catch(e){r1=0} 
    try{r2=arg2.toString().split(".")[1].length}catch(e){r2=0} 
    m=Math.pow(10,Math.max(r1,r2)) 
    return (arg1*m+arg2*m)/m 
} 
//accSub: arg1-arg2
//return: 精确的减法结果 
function accSub(arg1,arg2){
     var r1,r2,m,n;
     try{r1=arg1.toString().split(".")[1].length}catch(e){r1=0}
     try{r2=arg2.toString().split(".")[1].length}catch(e){r2=0}
     m=Math.pow(10,Math.max(r1,r2));
	 
     n=(r1>=r2)?r1:r2;
     return ((arg1*m-arg2*m)/m).toFixed(n);
} 

//Number END----------------------------------------------------------------------------

//Array BEGIN------------------------------------------------------------------------------------
//数组去重
function stripArr(arrs){
	if(arrs.length<2) return [arrs[0]]||[];
	var arr=[];
	for(var i=0;i<arrs.length;i++){
		arr.push(arrs.splice(i--,1));
		for(var j=0;j<arrs.length;j++){
			if(arrs[j]==arr[arr.length-1]){
				arrs.splice(j--,1);
			}
		}
	}
	return arr;
}
//数组去重
function uniqueArr(arrs) {
    arrs.sort();
    var arr = [arrs[0]];
    for (var i = 1; i < arrs.length; i++) {
        if (arrs[i] != arr[arr.length - 1]) {
            arr.push(arrs[i]);
        }
    }
    return arr;
}
//得到数组的l-h下标的数组
function limitArr(arr,l, h) {
    var _a = arr; var ret = []; 
    l = l<0?0:l; h = h>_a.length?_a.length:h;
    for (var i=0; i<_a.length; i++) {
        if (i>=l && i<=h) ret[ret.length] = _a[i];
        if (i>h) break;
    }
    return ret;
}
//指定array是否包含指定的item
function existsArr(arr,item)
{
	for(var i = 0 ; i < arr.length ; i++)
	{
		if(item == arr[i] )
			return true;
	}
	return false;
}
//删除指定的item
function removeArr(arr, item)
{
	for( var i = 0 ; i < arr.length ; i++ )
	{
		if( item == arr[i] )
			break;
	}
	if( i == arr.length )
		return;
	for( var j = i ; j < arr.length - 1 ; j++ )
	{
		arr[ j ] = arr[ j + 1 ];
	}
	arr.length--;
}

//Array END--------------------------------------------------------------------------------------

//Date BEGIN-------------------------------------------------------------------------------
//是否闰年
function isLeapYear(d)    
{    
    return (0==d.getYear()%4&&((d.getYear()%100!=0)||(d.getYear()%400==0)));    
}
//格式化时间
function formatDate(date, sFormat, sLanguage) {
	var time = {};
	time.Year = date.getFullYear();
	time.TYear = ("" + time.Year).substr(2);
	time.Month = date.getMonth() + 1;
	time.TMonth = time.Month < 10 ? "0" + time.Month : time.Month;
	time.Day = date.getDate();
	time.TDay = time.Day < 10 ? "0" + time.Day : time.Day;
	time.Hour = date.getHours();
	time.THour = time.Hour < 10 ? "0" + time.Hour : time.Hour;
	time.hour = time.Hour < 13 ? time.Hour : time.Hour - 12;
	time.Thour = time.hour < 10 ? "0" + time.hour : time.hour;
	time.Minute = date.getMinutes();
	time.TMinute = time.Minute < 10 ? "0" + time.Minute : time.Minute;
	time.Second = date.getSeconds();
	time.TSecond = time.Second < 10 ? "0" + time.Second : time.Second;
	time.Millisecond = date.getMilliseconds();
	time.Week = date.getDay();

	var MMMArrEn = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
		MMMArr = ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
		WeekArrEn = ["Sun", "Mon", "Tue", "Web", "Thu", "Fri", "Sat"],
		WeekArr = ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"],
		oNumber = time.Millisecond / 1000;

	if (sFormat != undefined && sFormat.replace(/\s/g, "").length > 0) {
		if (sLanguage != undefined && sLanguage === "en") {
			MMMArr = MMMArrEn.slice(0);
			WeekArr = WeekArrEn.slice(0);
		}
		sFormat = sFormat.replace(/yyyy/ig, time.Year)
		.replace(/yyy/ig, time.Year)
		.replace(/yy/ig, time.TYear)
		.replace(/y/ig, time.TYear)
		.replace(/MMM/g, MMMArr[time.Month - 1])
		.replace(/MM/g, time.TMonth)
		.replace(/M/g, time.Month)
		.replace(/dd/ig, time.TDay)
		.replace(/d/ig, time.Day)
		.replace(/HH/g, time.THour)
		.replace(/H/g, time.Hour)
		.replace(/hh/g, time.Thour)
		.replace(/h/g, time.hour)
		.replace(/mm/g, time.TMinute)
		.replace(/m/g, time.Minute)
		.replace(/ss/ig, time.TSecond)
		.replace(/s/ig, time.Second)
		.replace(/fff/ig, time.Millisecond)
		.replace(/ff/ig, oNumber.toFixed(2) * 100)
		.replace(/f/ig, oNumber.toFixed(1) * 10)
		.replace(/EEE/g, WeekArr[time.Week]);
	}
	else {
		sFormat = time.Year + "-" + time.Month + "-" + time.Day + " " + time.Hour + ":" + time.Minute + ":" + time.Second;
	}
	return sFormat;
}
// 日期格式化   
// 格式 YYYY/yyyy/YY/yy 表示年份   
// MM/M 月份   
// W/w 星期   
// dd/DD/d/D 日期   
// hh/HH/h/H 时间   
// mm/m 分钟   
// ss/SS/s/S 秒   
function dateFormat(d,format)    
{    
    var o = {
    "M+" : d.getMonth()+1, //month
    "d+" : d.getDate(),    //day
    "h+" : d.getHours(),   //hour
    "m+" : d.getMinutes(), //minute
    "s+" : d.getSeconds(), //second
    "q+" : Math.floor((d.getMonth()+3)/3),  //quarter
    "S" : d.getMilliseconds() //millisecond
  }
  if(/(y+)/.test(format)) format=format.replace(RegExp.$1,
    (d.getFullYear()+"").substr(4 - RegExp.$1.length));
  for(var k in o)if(new RegExp("("+ k +")").test(format))
    format = format.replace(RegExp.$1,
      RegExp.$1.length==1 ? o[k] : 
        ("00"+ o[k]).substr((""+ o[k]).length));
  return format;  
} 
//日期计算
function dateAdd(d,strInterval, Number) {    
    var dtTmp = d;   
    switch (strInterval) {    
        case 's' :return new Date(Date.parse(dtTmp) + (1000 * Number));   
        case 'n' :return new Date(Date.parse(dtTmp) + (60000 * Number));   
        case 'h' :return new Date(Date.parse(dtTmp) + (3600000 * Number));   
        case 'd' :return new Date(Date.parse(dtTmp) + (86400000 * Number));   
        case 'w' :return new Date(Date.parse(dtTmp) + ((86400000 * 7) * Number));   
        case 'q' :return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + Number*3, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());   
        case 'm' :return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + Number, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());   
        case 'y' :return new Date((dtTmp.getFullYear() + Number), dtTmp.getMonth(), dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());   
    }   
}   
//比较日期差 dtEnd 格式为日期型或者 有效日期格式字符串   
function dateDiff(d,strInterval, dtEnd) {    
    var dtStart = d;   
    if (typeof dtEnd == 'string' )
    {    
        dtEnd = StringToDate(dtEnd);   
    }   
    switch (strInterval) {    
        case 's' :return parseInt((dtEnd - dtStart) / 1000);   
        case 'n' :return parseInt((dtEnd - dtStart) / 60000);   
        case 'h' :return parseInt((dtEnd - dtStart) / 3600000);   
        case 'd' :return parseInt((dtEnd - dtStart) / 86400000);   
        case 'w' :return parseInt((dtEnd - dtStart) / (86400000 * 7));   
        case 'm' :return (dtEnd.getMonth()+1)+((dtEnd.getFullYear()-dtStart.getFullYear())*12) - (dtStart.getMonth()+1);   
        case 'y' :return dtEnd.getFullYear() - dtStart.getFullYear();   
    }   
} 
//
function diffDate(date1, date2,strInterval) {    
    var intervalSeconds = parseInt((date2 - date1) / 1000);
    switch (strInterval) {    
        case 's' :return intervalSeconds;   
        case 'n' :return Math.floor(intervalSeconds / 60);   
        case 'h' :return Math.floor(intervalSeconds / (60 * 60));   
        case 'd' :return Math.floor(intervalSeconds / (60 * 60 * 24));   
        case 'w' :return Math.floor(intervalSeconds / (60 * 60 * 24 * 7));   
        case 'm' :return (date2.getMonth()+1)+((date2.getFullYear()-date1.getFullYear())*12) - (date1.getMonth()+1);   
        case 'y' :return date2.getFullYear() - date1.getFullYear();   
    }   
}   
//取得日期数据信息  
function datePart(d,interval)   
{    
    var myDate = d;   
    var partStr='';   
    var Week = ['日','一','二','三','四','五','六'];   
    switch (interval)   
    {    
        case 'y' :partStr = myDate.getFullYear();break;   
        case 'm' :partStr = myDate.getMonth()+1;break;   
        case 'd' :partStr = myDate.getDate();break;   
        case 'w' :partStr = Week[myDate.getDay()];break;   
        case 'ww' :partStr = myDate.WeekNumOfYear();break;   
        case 'h' :partStr = myDate.getHours();break;   
        case 'n' :partStr = myDate.getMinutes();break;   
        case 's' :partStr = myDate.getSeconds();break;   
    }   
    return partStr;   
}

//date输出字符串
function date2Str(d,showWeek)   
{    
    var myDate= d;   
    var str = myDate.toLocaleDateString();   
    if (showWeek)   
    {    
        var Week = ['日','一','二','三','四','五','六'];   
        str += ' 星期' + Week[myDate.getDay()];   
    }   
    return str;   
}
//date分割为数组
function date2Arr(d)   
{    
    var myDate = d;   
    var myArray = Array();   
    myArray[0] = myDate.getFullYear();   
    myArray[1] = myDate.getMonth();   
    myArray[2] = myDate.getDate();   
    myArray[3] = myDate.getHours();   
    myArray[4] = myDate.getMinutes();   
    myArray[5] = myDate.getSeconds();   
    return myArray;   
} 
//当月天数  
function daysOfMonth(d)   
{    
    var myDate = d;   
    var ary = myDate.toArray();   
    var date1 = (new Date(ary[0],ary[1]+1,1));   
    var date2 = date1.dateAdd(1,'m',1);   
    var result = dateDiff(date1.Format('yyyy-MM-dd'),date2.Format('yyyy-MM-dd'));   
    return result;   
}   
//当天为一年中第几周
function weekOfYear(d)   
{    
    var myDate = d;   
    var ary = myDate.toArray();   
    var year = ary[0];   
    var month = ary[1]+1;   
    var day = ary[2];      
    return result;   
}



//===========================================
//获取符合datebox格式的当前日期
function getDate() {
    var date = new Date();
    var month = date.getMonth() + 1;
    if (parseInt(month) / 10 < 1)
        return date.getFullYear() + "-" + "0" + month + "-" + date.getDate();
    else
        return date.getFullYear() + "-" + month + "-" + date.getDate();
}

//字符串转成日期类型 格式 MM/dd/YYYY MM-dd-YYYY YYYY/MM/dd YYYY-MM-dd     
function StringToDate(DateStr)   
{
    var converted = Date.parse(DateStr);   
    var myDate = new Date(converted);   
    if (isNaN(myDate))   
    {    
        //var delimCahar = DateStr.indexOf('/')!=-1?'/':'-';   
        var arys= DateStr.split('-');   
        myDate = new Date(arys[0],--arys[1],arys[2]);   
    }   
    return myDate;   
} 
//日期合法性验证 格式为：YYYY-MM-DD或YYYY/MM/DD 
function IsValidDate(DateStr)    
{    
    var sDate=DateStr.replace(/(^\s+|\s+$)/g,'');
    if(sDate=='') return true;    
    //如果格式满足YYYY-(/)MM-(/)DD或YYYY-(/)M-(/)DD或YYYY-(/)M-(/)D或YYYY-(/)MM-(/)D就替换为''    
    //数据库中，合法日期可以是:YYYY-MM/DD(2003-3/21),数据库会自动转换为YYYY-MM-DD格式    
    var s = sDate.replace(/[\d]{ 4,4 }[\-/]{ 1 }[\d]{ 1,2 }[\-/]{ 1 }[\d]{ 1,2 }/g,'');    
    if (s=='') //YYYY-MM-DD或YYYY-M-DD或YYYY-M-D或YYYY-MM-D    
    {    
        var t=new Date(sDate.replace(/\-/g,'/'));    
        var ar = sDate.split(/[-/:]/);    
        if(ar[0] != t.getYear() || ar[1] != t.getMonth()+1 || ar[2] != t.getDate())    
        {    
            return false;    
        }    
    }    
    else    
    {      
        return false;    
    }    
    return true;    
}    
    
//日期时间检查 格式为：YYYY-MM-DD HH:MM:SS   
function CheckDateTime(str)   
{    
    var reg = /^(\d+)-(\d{ 1,2 })-(\d{ 1,2 }) (\d{ 1,2 }):(\d{ 1,2 }):(\d{ 1,2 })$/;    
    var r = str.match(reg);    
    if(r==null)return false;    
    r[2]=r[2]-1;    
    var d= new Date(r[1],r[2],r[3],r[4],r[5],r[6]);    
    if(d.getFullYear()!=r[1])return false;    
    if(d.getMonth()!=r[2])return false;    
    if(d.getDate()!=r[3])return false;    
    if(d.getHours()!=r[4])return false;    
    if(d.getMinutes()!=r[5])return false;    
    if(d.getSeconds()!=r[6])return false;    
    return true;    
}    

//求两个时间的天数差 日期格式为 YYYY-MM-dd
function daysBetween(DateOne,DateTwo)   
{    
    var OneMonth = DateOne.substring(5,DateOne.lastIndexOf ('-'));   
    var OneDay = DateOne.substring(DateOne.length,DateOne.lastIndexOf ('-')+1);   
    var OneYear = DateOne.substring(0,DateOne.indexOf ('-'));   
   
    var TwoMonth = DateTwo.substring(5,DateTwo.lastIndexOf ('-'));   
    var TwoDay = DateTwo.substring(DateTwo.length,DateTwo.lastIndexOf ('-')+1);   
    var TwoYear = DateTwo.substring(0,DateTwo.indexOf ('-'));   
   
    var cha=((Date.parse(OneMonth+'/'+OneDay+'/'+OneYear)- Date.parse(TwoMonth+'/'+TwoDay+'/'+TwoYear))/86400000);    
    return Math.abs(cha);   
}

//parseDate: UTC->date
//var d=parseDate("/Date(-62135596800000)/")
//return:date
function parseDate(timeSpan){
	var timeSpan = timeSpan.replace('Date', '').replace('(', '').replace(')', '').replace(/\//g, '');
	var d = new Date(parseInt(timeSpan));
	return d;
}  

//Date End-------------------------------------------------------





