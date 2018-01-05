window.onerror = function () { return true; }
jQuery(function ($) {

    var $backToTopTxt = "TOP", $backToTopEle = $('<div class="backToTop"></div>').appendTo($("body")).text($backToTopTxt).attr("title", $backToTopTxt).click(function () {
        $("html, body").animate({ scrollTop: 0 }, 120);
    }), $backToTopFun = function () {
        var st = $(document).scrollTop(), winh = $(window).height();
        (st > 0) ? $backToTopEle.show() : $backToTopEle.hide();
        //IE6下的定位
        if (!window.XMLHttpRequest) {
            $backToTopEle.css("top", st + winh - 166);
        }
    };
    $(window).bind("scroll", $backToTopFun);
    $(function () { $backToTopFun(); });

//    $(".imglist").hilightIMG();
//    //固定头部
//    dhDiv("#float");

});

function dhDiv(ee1) {
    //导航距离屏幕顶部距离
    var _defautlTop = $(ee1).offset().top - $(window).scrollTop();
    //导航距离屏幕左侧距离
    var _defautlLeft = $(ee1).offset().left - $(window).scrollLeft();
    //导航默认样式记录，还原初始样式时候需要
    var _position = $(ee1).css('position');
    var _top = $(ee1).css('top');
    var _left = $(ee1).css('left');
    var _zIndex = $(ee1).css('z-index');
    //鼠标滚动事件
    $(window).scroll(function () {
        if ($(this).scrollTop() > _defautlTop) {
            //IE6不认识position:fixed，单独用position:absolute模拟
            if ($.browser.msie && $.browser.version == "6.0") {
                $(ee1).css({ 'position': 'absolute', 'top': eval(document.documentElement.scrollTop), 'left': _defautlLeft, 'z-index': 99999, 'opacity': 0.99 });
                //防止出现抖动
                // $("html,body").css({'background-image':'url(about:blank)','background-attachment':'fixed'});
            } else {
                $(ee1).css({ 'position': 'fixed', 'top': 0, 'left': _defautlLeft, 'z-index': 99999, 'opacity': 0.99 });
            }
        } else {
            $(ee1).css({ 'position': _position, 'top': _top, 'left': _left, 'z-index': _zIndex, 'opacity': 0.99 });
        }
    });
}
