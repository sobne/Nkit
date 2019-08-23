/*
//include "../core/utils.js"
include "core/func.js"
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
	

	//jUnity.cookie ==========================================================================
    var document = win.document;
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
	
	//jUnity.request ==========================================================================
	jUnity.request = jUnity.request ? jUnity.request : {
        getParameter: function (name) {
            return getParameter(name);
        },
        getUrlParam: function (name) {
            return getUrlParam(name);
        },
        getLocalIP: function (callback) {
            getLocalIP(callback);
        },
		getBrowserInfo:function(){
			getBrowserInfo();
		}
    };

	//jUnity.string ==========================================================================
    jUnity.string = jUnity.string ? jUnity.string : {
        replaceURLWithHTMLLinks: function (sText, bBlank) {
            return replaceURiWithHTMLLinks(sText, bBlank);
        },
        getLength: function (str, bChinese) {
            return GetLengthOfStr(str, bChinese);
        }
    };

	//jUnity.number ==========================================================================
    jUnity.number = jUnity.number ? jUnity.number : {
        accDiv: function (arg1,arg2) {
            return accDiv(arg1, arg2);
        },
        accMul: function (arg1,arg2) {
            return accDiv(arg1, arg2);
        },
		accAdd: function (arg1,arg2) {
            return accDiv(arg1, arg2);
        },
		accSub: function (arg1,arg2) {
            return accDiv(arg1, arg2);
        }
    };
	
	//jUnity.array ==========================================================================
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
	
	//jUnity.date ==========================================================================
    jUnity.date = jUnity.date ? jUnity.date : {
        format: function (date, sFormat, sLanguage) {
            return formatDate(date, sFormat, sLanguage);
        },
        diff: function (date1, date2,strInterval) {
            return diffDate(date1, date2,strInterval)
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
	
	
	
	//..====================================================================================
    if (!win.jUnity) {
        win.jUnity = jUnity;
    }
})(window);