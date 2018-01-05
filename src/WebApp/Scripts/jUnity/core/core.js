//字符串连接
//var buffer = new StringBuffer ();
//buffer.append("hello ");
//buffer.append("world");
//var result = buffer.toString();
function StringBuffer() {
    this._strings_ = new Array();
}
StringBuffer.prototype.append = function (str) {
    this._strings_.push(str);
};
StringBuffer.prototype.toString = function () {
    return this._strings_.join("");
};

/**************
Namespace：
模拟命名空间机制的完整实现。
用法：
NameSpace.register("sobne.cn");
例子:
sobne.cn.funcname = function(){};
调用:
new sobne.cn.funcname();
**************/
var Namespace = new Object();
Namespace.register = function (path) {
    var arr = path.split(".");
    var ns = "";
    for (var i = 0; i < arr.length; i++) {
        if (i > 0) ns += ".";
        ns += arr[i];
        eval("if(typeof(" + ns + ") == 'undefined') " + ns + " = new Object();");
    }
}
//test
Namespace.register("easyui.messager");
//   var msg = new easyui.messager.show("提示", "删除成功!");
//   msg.fade();
easyui.messager.show =function(title, msg) {
    var p = {
        title: title,
        msg: msg
    }
    this.fade = function () {
        p.showType = 'fade';
        $.messager.show(p);
    }
}

//new method
var easyui = easyui || {};
easyui.datagrid = easyui.datagrid || {};
easyui.datagrid.headcenter = function () {
    $(".datagrid-header .datagrid-cell").css('text-align', 'center').css('color', '#173967');
}