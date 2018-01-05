/*
include "../core/utils.js"
*/

var utils = {
    
    isDom : ( typeof HTMLElement === 'object' ) ?
        function(obj){
            return obj instanceof HTMLElement;
        } :
        function(obj){
            return obj && typeof obj === 'object' && obj.nodeType === 1 && typeof obj.nodeName === 'string';
        } ,
        
    isArray : function(obj){
        return obj.constructor.toString().indexOf('Array') != -1;
    }
    
}

;(function (win) {
    var document = win.document;
    var jUnity = jUnity ? jUnity : {
        extend : function() {
            var a = arguments;
            if(a.length < 2) {
                return;
            }
            if(a[0] != null) {
                for(var i = 1; i < a.length; i++) {
                    for(var r in a[i]) {
                        a[0][r] = a[i][r];
                    }
                }
            }
            return a[0];
        },
        mergeIfNotExist : function() {
            var a = arguments;
            if(a.length < 2) {
                return;
            }
            for(var i = 1; i < a.length; i++) {
                for(var r in a[i]) {
                    if(a[0][r] == null) {
                        a[0][r] = a[i][r];
                    }
                }
            }
            return a[0];
        },
        isDom : function(obj){
			return utils.isDom(obj);
		},
        isArray : function(obj){
            return utils.isArray(obj);
        }
    };

    jUnity.array = jUnity.array ? jUnity.array : {
        distinct: function unique(arr) {
            var i = 0,
                gid = '_' + (+new Date) + Math.random(),
                objs = [],
                hash = {
                    'string': {},
                    'boolean': {},
                    'number': {}
                },
                p,
                l = arr.length,
                ret = [];
            for (; i < l; i++) {
                p = arr[i];
                if (p == null) continue;
                tp = typeof p;
                if (tp in hash) {
                    if (!(p in hash[tp])) {
                        hash[tp][p] = 1;
                        ret.push(p);
                    }
                } else {
                    if (p[gid]) continue;
                    p[gid] = 1;
                    objs.push(p);
                    ret.push(p);
                }
            }
            for (i = 0, l = objs.length; i < l; i++) {
                p = objs[i];
                p[gid] = undefined;
                delete p[gid];
            }
            return ret;
        },
        indexOf: function (arr, obj, iStart) {
            if (Array.prototype.indexOf) {
                return arr.indexOf(obj, (iStart || 0));
            }
            else {
                for (var i = (iStart || 0), j = arr.length; i < j; i++) {
                    if (arr[i] === obj) {
                        return i;
                    }
                }
                return -1;
            }
        },
        filter: function (arr, callback) {
            var result = [];
            for (var i = 0, j = arr.length; i < j; i++) {
                if (callback.call(arr[i], i, arr[i])) {
                    result.push(arr[i]);
                }
            }
            return result;
        }
    };

    jUnity.cookie = jUnity.cookie ? jUnity.cookie : {
        get: function (sKey) {
            if (!sKey)
                return "";
            if (document.cookie.length > 0) {
                var startIndex = document.cookie.indexOf(sKey + "=")
                if (startIndex != -1) {
                    startIndex = startIndex + sKey.length + 1
                    var endIndex = document.cookie.indexOf(";", startIndex)
                    if (endIndex == -1) {
                        endIndex = document.cookie.length;
                    }
                    return decodeURIComponent(document.cookie.substring(startIndex, endIndex));
                }
            }
            return ""
        },
        set: function (sKey, sValue, iExpireSeconds) {
            if (!sKey)
                return;
            var expireDate = new Date();
            expireDate.setTime(expireDate.getTime() + iExpireSeconds * 1000);
            document.cookie = sKey + "=" + encodeURIComponent(sValue) +
            ";expires=" + expireDate.toGMTString() + ";";
        },
        delete: function (sKey) {
            if (!sKey)
                return;
            document.cookie = sKey + '=; expires=Thu, 01 Jan 1970 00:00:01 GMT;';
        }
    };

    jUnity.date = jUnity.date ? jUnity.date : {
        format: function (date, sFormat, sLanguage) {
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
        },
        diff: function (biggerDate, smallerDate) {
            var intervalSeconds = parseInt((biggerDate - smallerDate) / 1000);
            if (intervalSeconds < 60) {
                return intervalSeconds + "秒";
            }
            else if (intervalSeconds < 60 * 60) {
                return Math.floor(intervalSeconds / 60) + "分钟";
            }
            else if (intervalSeconds < 60 * 60 * 24) {
                return Math.floor(intervalSeconds / (60 * 60)) + "小时";
            }
            else if (intervalSeconds < 60 * 60 * 24 * 7) {
                return Math.floor(intervalSeconds / (60 * 60 * 24)) + "天";
            }
            else if (intervalSeconds < 60 * 60 * 24 * 31) {
                return Math.floor(intervalSeconds / (60 * 60 * 24 * 7)) + "周";
            }
            else if (intervalSeconds < 60 * 60 * 24 * 365) {
                return Math.floor(intervalSeconds / (60 * 60 * 24 * 30)) + "月";
            }
            else if (intervalSeconds < 60 * 60 * 24 * 365 * 1000) {
                return Math.floor(intervalSeconds / (60 * 60 * 24 * 365)) + "年";
            }
            else {
                return Math.floor(intervalSeconds / (60 * 60 * 24)) + "天";
            }
        },
        interval: function (biggerDate, smallerDate) {
            var intervalSeconds = parseInt((biggerDate - smallerDate) / 1000),
                day = Math.floor(intervalSeconds / (60 * 60 * 24)),
                hour = Math.floor((intervalSeconds - day * 24 * 60 * 60) / 3600),
                minute = Math.floor((intervalSeconds - day * 24 * 60 * 60 - hour * 3600) / 60),
                second = Math.floor(intervalSeconds - day * 24 * 60 * 60 - hour * 3600 - minute * 60);
            return day + "天:" + hour + "小时:" + minute + "分钟:" + second + "秒";
        }
    };

    jUnity.string = jUnity.string ? jUnity.string : {
        replaceURLWithHTMLLinks: function (sText, bBlank) {
            var pattern = /(\b(https?|ftp|file):\/\/[-A-Z0-9+&@#\/%?=~_|!:,.;]*[-A-Z0-9+&@#\/%=~_|])/ig;
            if (bBlank) {
                sText = sText.replace(pattern, "<a target='_blank' href='$1'>$1</a>");
            }
            else {
                sText = sText.replace(pattern, "<a href='$1'>$1</a>");
            }
            return sText;
        },
        getLength: function (sVal, bChineseDouble) {
            var chineseRegex = /[\u4e00-\u9fa5]/g;
            if (bChineseDouble != undefined && bChineseDouble === false) {
                return sVal.length;
            }
            else {
                if (chineseRegex.test(sVal)) {
                    return sVal.replace(chineseRegex, "zz").length;
                }
                return sVal.length;
            }
        }
    };

    if (!win.jUnity) {
        win.jUnity = jUnity;
    }
})(window);