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
function screenresize() {
    //    self.moveTo(0,0);
    //    self.resizeTo(screen.availWidth,screen.availHeight);
    window.moveTo(0, 0);
    window.resizeTo(screen.width, screen.height - 30);
}
function isEmptyObject(obj) {
    for (var name in obj) {
        return false;
    }
    return true;
}

//通过把正则表达式把<>和空格符转换成html编码,
//由于这种方式不是系统内置的所以很容易出现有些特殊标签没有替换的情况，而且效率低下
function HTMLEncode(str) {
    var s = "";
    if (str.length == 0) return "";
    s = str.replace(/&/g, "&amp;");
    s = s.replace(/</g, "&lt;");
    s = s.replace(/>/g, "&gt;");
    s = s.replace(/ /g, "&nbsp;");
    s = s.replace(/\'/g, "&#39;");
    s = s.replace(/\"/g, "&quot;");
    return s;
}

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


//获取符合datebox格式的当前日期
function getDate() {
    var date = new Date();
    var month = date.getMonth() + 1;
    if (parseInt(month) / 10 < 1)
        return date.getFullYear() + "-" + "0" + month + "-" + date.getDate();
    else
        return date.getFullYear() + "-" + month + "-" + date.getDate();
}

//+---------------------------------------------------   
//| 字符串转成日期类型    
//| 格式 MM/dd/YYYY MM-dd-YYYY YYYY/MM/dd YYYY-MM-dd   
//+---------------------------------------------------   
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
//+---------------------------------------------------   
//| 日期合法性验证   
//| 格式为：YYYY-MM-DD或YYYY/MM/DD   
//+---------------------------------------------------   
function IsValidDate(DateStr)    
{    
    var sDate=DateStr.replace(/(^\s+|\s+$)/g,''); //去两边空格;    
    if(sDate=='') return true;    
    //如果格式满足YYYY-(/)MM-(/)DD或YYYY-(/)M-(/)DD或YYYY-(/)M-(/)D或YYYY-(/)MM-(/)D就替换为''    
    //数据库中，合法日期可以是:YYYY-MM/DD(2003-3/21),数据库会自动转换为YYYY-MM-DD格式    
    var s = sDate.replace(/[\d]{ 4,4 }[\-/]{ 1 }[\d]{ 1,2 }[\-/]{ 1 }[\d]{ 1,2 }/g,'');    
    if (s=='') //说明格式满足YYYY-MM-DD或YYYY-M-DD或YYYY-M-D或YYYY-MM-D    
    {    
        var t=new Date(sDate.replace(/\-/g,'/'));    
        var ar = sDate.split(/[-/:]/);    
        if(ar[0] != t.getYear() || ar[1] != t.getMonth()+1 || ar[2] != t.getDate())    
        {    
            //alert('错误的日期格式！格式为：YYYY-MM-DD或YYYY/MM/DD。注意闰年。');    
            return false;    
        }    
    }    
    else    
    {    
        //alert('错误的日期格式！格式为：YYYY-MM-DD或YYYY/MM/DD。注意闰年。');    
        return false;    
    }    
    return true;    
}    
   
//+---------------------------------------------------   
//| 日期时间检查   
//| 格式为：YYYY-MM-DD HH:MM:SS   
//+---------------------------------------------------   
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

//+---------------------------------------------------   
//| 求两个时间的天数差 日期格式为 YYYY-MM-dd    
//+---------------------------------------------------   
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



//除法函数，用来得到精确的除法结果 
//说明：javascript的除法结果会有误差，在两个浮点数相除的时候会比较明显。这个函数返回较为精确的除法结果。 
//调用：accDiv(arg1,arg2) 
//返回值：arg1除以arg2的精确结果 
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
//乘法函数，用来得到精确的乘法结果 
//说明：javascript的乘法结果会有误差，在两个浮点数相乘的时候会比较明显。这个函数返回较为精确的乘法结果。 
//调用：accMul(arg1,arg2) 
//返回值：arg1乘以arg2的精确结果 
function accMul(arg1,arg2) 
{ 
    var m=0,s1=arg1.toString(),s2=arg2.toString(); 
    try{m+=s1.split(".")[1].length}catch(e){} 
    try{m+=s2.split(".")[1].length}catch(e){} 
    return Number(s1.replace(".",""))*Number(s2.replace(".",""))/Math.pow(10,m) 
} 
//加法函数，用来得到精确的加法结果 
//说明：javascript的加法结果会有误差，在两个浮点数相加的时候会比较明显。这个函数返回较为精确的加法结果。 
//调用：accAdd(arg1,arg2) 
//返回值：arg1加上arg2的精确结果 
function accAdd(arg1,arg2){ 
    var r1,r2,m; 
    try{r1=arg1.toString().split(".")[1].length}catch(e){r1=0} 
    try{r2=arg2.toString().split(".")[1].length}catch(e){r2=0} 
    m=Math.pow(10,Math.max(r1,r2)) 
    return (arg1*m+arg2*m)/m 
} 
//减法函数，用来得到精确的减法结果 
function Subtr(arg1,arg2){
     var r1,r2,m,n;
     try{r1=arg1.toString().split(".")[1].length}catch(e){r1=0}
     try{r2=arg2.toString().split(".")[1].length}catch(e){r2=0}
     m=Math.pow(10,Math.max(r1,r2));
     //last modify by deeka
     //动态控制精度长度
     n=(r1>=r2)?r1:r2;
     return ((arg1*m-arg2*m)/m).toFixed(n);
} 

