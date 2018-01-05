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

Namespace.register("easyui.messager");
//$.messager.alert
//      var msg = new easyui.messager.alert("警告", '不能增加记录。');
//      msg.warning();
easyui.messager.alert = function (title, msg) {
    this.title = title;
    this.msg = msg;
    //实例方法,可以放到外面
//    easyui.messager.alert.prototype.error = function () {
//        $.messager.alert(this.title, this.msg, 'error');
//    }
//    easyui.messager.alert.prototype.question = function () {
//        $.messager.alert(this.title, this.msg, 'question');
//    }
//    easyui.messager.alert.prototype.info = function () {
//        $.messager.alert(this.title, this.msg, 'info');
//    }
//    easyui.messager.alert.prototype.warning = function () {
//        $.messager.alert(this.title, this.msg, 'warning');
//    }
}
easyui.messager.alert.prototype = {
    error:function () {
        $.messager.alert(this.title, this.msg, 'error');
    },
    question:function () {
        $.messager.alert(this.title, this.msg, 'question');
    },
    info:function () {
        $.messager.alert(this.title, this.msg, 'info');
    },
    warning:function () {
        $.messager.alert(this.title, this.msg, 'warning');
    }
}
//messager.show
//   var msg = new easyui.messager.show("提示", "删除成功!");
//   msg.fade();
easyui.messager.show =function(title, msg, timeout, showSpeed, width, height) {
    var p = {
        title: title,
        msg: msg
    }
    if (arguments.length > 2) { p.timeout = timeout; }
    if (arguments.length > 3) { p.showSpeed = showSpeed; }
    if (arguments.length > 4) { p.width = width; }
    if (arguments.length > 5) { p.height = height; }
    //静态方法
    this.show = function () {
        p.showType = 'show';
        $.messager.show(p);
    }
    this.slide = function () {
        p.showType = 'slide';
        $.messager.show(p);
    }
    this.fade = function () {
        p.showType = 'fade';
        $.messager.show(p);
    }
}

var easyui = easyui || {};
easyui.datagrid = easyui.datagrid || {};
easyui.datagrid.headcenter = function () {
    $(".datagrid-header .datagrid-cell").css('text-align', 'center').css('color', '#173967');
}