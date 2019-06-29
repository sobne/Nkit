/*
include "func.js"
*/

/*
author:sobne
常用的js函数扩展
v1.1  2019.06.25
v1.0  2012.12.12
1 String 扩展
2 Number 扩展
3 Array  扩展
4 Date	 扩展
*/

//1 String 扩展----------------------------------------------------------------
/**************
replaceAll：
替换字符串中的字符。
用法：
yourstring.replaceAll("要替换的字符", "替换成什么");
例子:
"cssrain".replaceAll("s", "a");
"   cs   sr   ai   n".replaceAll(" ", "");
**************/
String.prototype.replaceAll = function (searchStr,replaceStr){
	return replaceAll(this,searchStr,replaceStr);
}


/**************
 * 字符串前后空格处理。
 * 如果想替换中间的空格，请用replaceAll方法。
 * 用法：
 * "  cssrain   ".trim();
**************/
String.prototype.trim=function()
{
    return Trim(this);
}


/**************
* 计算字符串的真正长度
//计算中文字符和全角字符。但是在数据存储的时候中文和全角都是用两个字节来存储的
**************/
String.prototype.codeLength=function(){
    return GetStrLength(this);
} 
String.prototype.GetLengthOfStr=function(bChinese){
    return GetLengthOfStr(this, bChinese);
} 
/**************
检测字符串是否是以某个字符开始
用法:if ("sobne".startWith("s")) return true;
**************/
String.prototype.startWith=function(str){
	return StartWith(this,str);
}
/**************
检测字符串是否是以某个字符结束
用法:if ("sobne".endWith("e")) return true;
**************/
String.prototype.endWith = function (str) {
    return EndWith(this,str);
}


/**************
//编码HTML  和  解码Html。
//过滤HTML标签，过滤掉双引号，单引号，符号&，符号<，符号
**************/
String.prototype.htmlEncode=function(){
    return HTMLEncode(this);
}
String.prototype.htmlDecode=function(){
    return HTMLDecode(this);
}
//String END----------------------------------------------------------------



//2 Number 扩展----------------------------------------------------------------

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
    return accSub(arg,this); 
}
//Number END----------------------------------------------------------------


//3 Array  扩展----------------------------------------------------------------

//去掉数组中重复的元素
//用法：
//var arr=["abc",85,"abc",85,8,8,1,2,5,4,7,8];
Array.prototype.strip=function(){
	return stripArr(this);
}

//unique
Array.prototype.unique = function() {
    return uniqueArr(this);
}


//得到l-h下标的数组
//用法：
//var arr=["abc",85,"abc",85,8,8,1,2,5,4,7,8];
//alert( arr.limit(2,4) ); //输出  abc , 85 ,8  
Array.prototype.limit = function(l, h) {
    return limitArr(this,l,h);
}

//指定array是否包含指定的item
//array.exists( item ) 包含true;不包含false;
//用法：
//var array1  = [1,2,3,4,5,"a","b"];
//var b1  =  array1.exists("b") // 包含true;不包含false;//alert(b1)
Array.prototype.exists( item )
{
	return existsArr(this.item);
}

//删除指定的item
//array.remove(item) 删除item
//用法：
//var array1  = [1,2,3,4,5,"a","b"];
//array1.remove("2");
Array.prototype.remove( item )
{
	removeArr(this,item);
}
//Array END----------------------------------------------------------------



//4 Date 扩展----------------------------------------------------------------

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
    return isLeapYear(this);    
}    
// language:en/zh
Date.prototype.formatDate = function(format,language)    
{    
  return formatDate(this,format,language);  
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
  return dateFormat(this,format);  
}    
   

//+---------------------------------------------------   
//| 日期计算   
//+---------------------------------------------------   
Date.prototype.DateAdd = function(strInterval, number) {    
    return dateAdd(this,strInterval,number); 
}   
   
//+---------------------------------------------------   
//| 比较日期差 dtEnd 格式为日期型或者 有效日期格式字符串   
//+---------------------------------------------------   
Date.prototype.DateDiff = function(strInterval, dtEnd) {    
    return dateDiff(this,strInterval,dtEnd)
}   
Date.prototype.DiffDate = function(strInterval, dtEnd) {    
    return diffDate(this,dtEnd,strInterval)
}    
//+---------------------------------------------------   
//| 日期输出字符串，重载了系统的toString方法   
//+---------------------------------------------------   
Date.prototype.toString = function(showWeek)   
{  
    return date2Str(this,showWeek);   
}   
   

//+---------------------------------------------------   
//| 把日期分割成数组   
//+---------------------------------------------------   
Date.prototype.toArray = function()   
{     
    return date2Arr(this);   
}   
   
//+---------------------------------------------------   
//| 取得日期数据信息   
//| 参数 interval 表示数据类型   
//| y 年 m月 d日 w星期 ww周 h时 n分 s秒   
//+---------------------------------------------------   
Date.prototype.DatePart = function(interval)   
{ 
    return datePart(this,interval);   
}   
   
//+---------------------------------------------------   
//| 取得当前日期所在月的最大天数   
//+---------------------------------------------------   
Date.prototype.MaxDayOfDate = function()   
{  
    return daysOfMonth(this);   
}   
   
//+---------------------------------------------------   
//| 取得当前日期所在周是一年中的第几周   
//+---------------------------------------------------   
Date.prototype.WeekNumOfYear = function()   
{         
    return weekOfYear(this);   
}

   
//Date END----------------------------------------------------------------



