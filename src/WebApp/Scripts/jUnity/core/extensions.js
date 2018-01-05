/*
include "func.js"
*/

/*
收集了常用的js函数和封装代码。
v1.0  2012.12.12
1 String 扩展
2 Number 扩展
3 Array  扩展
4 Date	 扩展
*/

//1 String 扩展
/**************
replaceAll：
替换字符串中的字符。
用法：
yourstring.replaceAll("要替换的字符", "替换成什么");
例子:
"cssrain".replaceAll("s", "a");
"   cs   sr   ai   n".replaceAll(" ", "");
**************/
String.prototype.replaceAll = function (AFindText,ARepText){
                raRegExp = new RegExp(AFindText,"g");
                return this.replace(raRegExp,ARepText);
}


/**************
 * 字符串前后空格处理。
 * 如果想替换中间的空格，请用replaceAll方法。
 * 用法：
 * "  cssrain   ".trim();
**************/
String.prototype.trim=function()
{
    return this.replace(/(^\s*)|(\s*$)/g,"");//将字符串前后空格,用空字符串替代。
}


/**************
* 计算字符串的真正长度
//String有个属性length，但是它不能区分英文字符，
//计算中文字符和全角字符。但是在数据存储的时候中文和全角都是用两个字节来存储的，
//所有需要额外处理一下。自己写了个函数，返回String正真的长度.
用法：
<input type="text" name="rain" id="rain" />
<input type="button" id="test" value="test" onclick="alert(  document.getElementById('rain').value.codeLength()  )"/>
**************/
String.prototype.codeLength=function(){
    var len=0;
    if(this==null||this.length==0)
        return 0;
    var str=this.trim();//去掉空格
    for(i=0;i<str.length;i++){
        if(str.charCodeAt(i)>0&&str.charCodeAt(i)<128)
            len++;
        else 
            len+=2;
    }
    return len;
} 
/**************
检测字符串是否是以某个字符开始
用法:if ("sobne".startWith("s")) return true;
**************/
String.prototype.startWith=function(str){
    if(str==null||str==""||this.length==0||str.length>this.length)
        return false;
    if(this.substr(0,str.length)==str)
        return true;
    else
        return false;
 return true;
}
/**************
检测字符串是否是以某个字符结束
用法:if ("sobne".endWith("e")) return true;
**************/
String.prototype.endWith = function (str) {
    if (str == null || str == "" || this.length == 0 || str.length > this.length)
        return false;
    if (this.substring(this.length - str.length) == str)
        return true;
    else
        return false;
    return true;
}
////JS获取字符串的实际长度，用来代替 String的length属性
//String.prototype.length = function(){
//    return this.replace.(/[\u4e00-\u9fa5]+/g,"**").length;
//}



/**************
//编码HTML  和  解码Html。
//在评论的时候为了防止用户提交带有恶意的脚本，可以先过滤HTML标签，过滤掉双引号，单引号，符号&，符号<，符号
用法：
<input type="text" name="rain" id="rain" />
<input type="button" value="test" onclick=" document.getElementById('rain2').value= document.getElementById('rain').value.htmlEncode()  "/>
<input type="text" name="rain2" id="rain2" />
<input type="button" value="test" onclick=" document.getElementById('rain').value= document.getElementById('rain2').value.htmlDecode()  "/>
**************/

String.prototype.htmlEncode=function(){
    return this.replace(/&/g,"&amp;").replace(/</g,"&lt;").replace(/>/g,"&gt;").replace(/\"/g,"&#34;").replace(/\'/g,"&#39;");
}
String.prototype.htmlDecode=function(){
    return this.replace(/\&amp\;/g, '\&').replace(/\&gt\;/g, '\>').replace(/\&lt\;/g, '\<').replace(/\&quot\;/g, '\'').replace(/\&\#39\;/g, '\'');
}
//String END



//2 Number 扩展

//给Number类型增加一个div方法
//用法： (37).div(3);
Number.prototype.div = function (arg){ 
    return accDiv(this, arg); 
} 


//给Number类型增加一个mul方法
//用法： (37).mul(3);
Number.prototype.mul = function (arg){ 
    return accMul(arg, this); 
} 


//给Number类型增加一个add方法
//用法： (37).add(3);
Number.prototype.add = function (arg){ 
    return accAdd(arg,this); 
}


//给Number类型增加一个sub方法
//用法：  (5.5).sub(37.5) 
Number.prototype.sub = function (arg){ 
    return Subtr(arg,this); 
}
//Number END


//3 Array  扩展

//去掉数组中重复的元素
//用法：
//var arr=["abc",85,"abc",85,8,8,1,2,5,4,7,8];
//alert(  arr.strip()  );
Array.prototype.strip=function(){
	if(this.length<2) return [this[0]]||[];
	var arr=[];
	for(var i=0;i<this.length;i++){
		arr.push(this.splice(i--,1));
		for(var j=0;j<this.length;j++){
			if(this[j]==arr[arr.length-1]){
				this.splice(j--,1);
			}
		}
	}
	return arr;
}



//得到l-h下标的数组
//用法：
//var arr=["abc",85,"abc",85,8,8,1,2,5,4,7,8];
//alert( arr.limit(2,4) ); //输出  abc , 85 ,8  
Array.prototype.limit = function(l, h) {
    var _a = this; var ret = []; 
    l = l<0?0:l; h = h>_a.length?_a.length:h;
    for (var i=0; i<_a.length; i++) {
        if (i>=l && i<=h) ret[ret.length] = _a[i];
        if (i>h) break;
    }
    return ret;
}

//指定array是否包含指定的item
//array.exists( item ) 包含true;不包含false;
//用法：
//var array1  = [1,2,3,4,5,"a","b"];
//var b1  =  array1.exists("b") // 包含true;不包含false;//alert(b1)
Array.prototype.exists( item )
{
	for( var i = 0 ; i < this.length ; i++ )
	{
		if( item == this[i] )
			return true;
	}
	return false;
}

//删除指定的item
//array.remove(item) 删除item
//用法：
//var array1  = [1,2,3,4,5,"a","b"];
//array1.remove("2");
//alert( array1[1] );
Array.prototype.remove( item )
{
	for( var i = 0 ; i < this.length ; i++ )
	{
		if( item == this[i] )
			break;
	}
	if( i == this.length )
		return;
	for( var j = i ; j < this.length - 1 ; j++ )
	{
		this[ j ] = this[ j + 1 ];
	}
	this.length--;
}
//Array END



//4 Date 扩展

/*
Date.prototype.isLeapYear 判断闰年
Date.prototype.Format 日期格式化
Date.prototype.DateAdd 日期计算
Date.prototype.DateDiff 比较日期差
Date.prototype.toString 日期转字符串
Date.prototype.toArray 日期分割为数组
Date.prototype.DatePart 取日期的部分信息
Date.prototype.MaxDayOfDate 取日期所在月的最大天数
Date.prototype.WeekNumOfYear 判断日期所在年的第几周
*/
// 判断闰年   
//---------------------------------------------------   
Date.prototype.isLeapYear = function()    
{    
    return (0==this.getYear()%4&&((this.getYear()%100!=0)||(this.getYear()%400==0)));    
}    
   
//---------------------------------------------------   
// 日期格式化   
// 格式 YYYY/yyyy/YY/yy 表示年份   
// MM/M 月份   
// W/w 星期   
// dd/DD/d/D 日期   
// hh/HH/h/H 时间   
// mm/m 分钟   
// ss/SS/s/S 秒   
//---------------------------------------------------   
Date.prototype.Format = function(format)    
{    
    var o = {
    "M+" : this.getMonth()+1, //month
    "d+" : this.getDate(),    //day
    "h+" : this.getHours(),   //hour
    "m+" : this.getMinutes(), //minute
    "s+" : this.getSeconds(), //second
    "q+" : Math.floor((this.getMonth()+3)/3),  //quarter
    "S" : this.getMilliseconds() //millisecond
  }
  if(/(y+)/.test(format)) format=format.replace(RegExp.$1,
    (this.getFullYear()+"").substr(4 - RegExp.$1.length));
  for(var k in o)if(new RegExp("("+ k +")").test(format))
    format = format.replace(RegExp.$1,
      RegExp.$1.length==1 ? o[k] : 
        ("00"+ o[k]).substr((""+ o[k]).length));
  return format;  
}    
   

//+---------------------------------------------------   
//| 日期计算   
//+---------------------------------------------------   
Date.prototype.DateAdd = function(strInterval, Number) {    
    var dtTmp = this;   
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
   
//+---------------------------------------------------   
//| 比较日期差 dtEnd 格式为日期型或者 有效日期格式字符串   
//+---------------------------------------------------   
Date.prototype.DateDiff = function(strInterval, dtEnd) {    
    var dtStart = this;   
    if (typeof dtEnd == 'string' )//如果是字符串转换为日期型   
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
   
//+---------------------------------------------------   
//| 日期输出字符串，重载了系统的toString方法   
//+---------------------------------------------------   
Date.prototype.toString = function(showWeek)   
{    
    var myDate= this;   
    var str = myDate.toLocaleDateString();   
    if (showWeek)   
    {    
        var Week = ['日','一','二','三','四','五','六'];   
        str += ' 星期' + Week[myDate.getDay()];   
    }   
    return str;   
}   
   

//+---------------------------------------------------   
//| 把日期分割成数组   
//+---------------------------------------------------   
Date.prototype.toArray = function()   
{    
    var myDate = this;   
    var myArray = Array();   
    myArray[0] = myDate.getFullYear();   
    myArray[1] = myDate.getMonth();   
    myArray[2] = myDate.getDate();   
    myArray[3] = myDate.getHours();   
    myArray[4] = myDate.getMinutes();   
    myArray[5] = myDate.getSeconds();   
    return myArray;   
}   
   
//+---------------------------------------------------   
//| 取得日期数据信息   
//| 参数 interval 表示数据类型   
//| y 年 m月 d日 w星期 ww周 h时 n分 s秒   
//+---------------------------------------------------   
Date.prototype.DatePart = function(interval)   
{    
    var myDate = this;   
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
   
//+---------------------------------------------------   
//| 取得当前日期所在月的最大天数   
//+---------------------------------------------------   
Date.prototype.MaxDayOfDate = function()   
{    
    var myDate = this;   
    var ary = myDate.toArray();   
    var date1 = (new Date(ary[0],ary[1]+1,1));   
    var date2 = date1.dateAdd(1,'m',1);   
    var result = dateDiff(date1.Format('yyyy-MM-dd'),date2.Format('yyyy-MM-dd'));   
    return result;   
}   
   
//+---------------------------------------------------   
//| 取得当前日期所在周是一年中的第几周   
//+---------------------------------------------------   
Date.prototype.WeekNumOfYear = function()   
{    
    var myDate = this;   
    var ary = myDate.toArray();   
    var year = ary[0];   
    var month = ary[1]+1;   
    var day = ary[2];      
    return result;   
}   
//Date END