window.onerror = function () { return true; }

//customer functions
jQuery.everbond = {
    ArrowUp: function () {

    }

}

//my plugin
;(function ($) {
    /*鼠标滑到的图片高亮显示,其他兄弟图片暗色
	<div class="imgList">
		<a><img src=""/></a>
		<a><img src=""/></a>
	</div>
    $(".imgList").hilightIMG();
    */
    $.fn.hilightIMG = function () {
        $(this).find("a").each(function () {
            $(this).hover(function () { //当鼠标移上去时
                //它的兄弟图片透明度降到0.5
                $(this).siblings().find("img").stop().fadeTo(400, 0.5);
            },
			function () { //当鼠标移出的时候
			    //它的兄弟图片透明度回到1
			    $(this).siblings().find("img").stop().fadeTo(400, 1);
			});
        });
    };





})(jQuery);
