/*
include "func.js"
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
;