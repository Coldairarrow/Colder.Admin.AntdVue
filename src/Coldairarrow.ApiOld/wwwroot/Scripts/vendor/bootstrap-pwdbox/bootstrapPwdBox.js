/* jQuery插件bootstrapPwdBox
 * 根本上解决谷歌浏览器、IE、火狐、360浏览器等自动密码记住问题
 * 用法:$('id').bootstrapPwdBox()
 * $('id').val()
 */
(function ($) {
    if ($.fn.bootstrapPwdBox)
        return;

    $.fn.bootstrapPwdBox = function (option) {
        var defaults = {
            value:''
        };
        var options = $.extend(defaults, option);

        var id = this[0].id;
        var $hidObj = $(this);
        var required = this[0].required ? ' required ' : '';
        //var placeholder = '';
        //var thePlaceholder = $(this).attr('placeholder');
        //if (thePlaceholder) {
        //    placeholder = ' placeholder="' + thePlaceholder + '" ';
        //}
        var placeholder = $(this).attr('placeholder') ? ' placeholder="' + $(this).attr('placeholder') + '" ' : '';

        var $showObj = $('<input id="_ ' + id + '" type="text" autocomplete="off" class="' + $hidObj.attr('class') + '"' + required + placeholder + '>');
        $hidObj.after($showObj);

        $hidObj.css('display', 'none');

        $showObj.bind('input propertychange', function () {
            var p = getCursor(this);

            var real = $hidObj.val();
            var show = $showObj.val();

            var reg = /(●*)([^●]*)(●*)/;
            reg.test(show);

            var showL1 = RegExp.$1;
            var showL2 = RegExp.$2;
            var showL3 = RegExp.$3;

            //增加或替换
            if (showL2.length > 0) {
                var realL1 = real.substr(0, showL1.length);
                var realL2 = showL2;
                var realL3 = real.substr(real.length - showL3.length, showL3.length);
                real = realL1 + realL2 + realL3;
            } else {//仅删除
                var realL1 = real.substr(0, p);
                var realL2 = real.substr(p + (real.length - show.length));
                real = realL1 + realL2;
            }

            show = real.replace(/./g, '●');
            $hidObj.val(real);
            $showObj.val(show);
            setCursor(this, p);
        });

        if (options.value) {
            var real = options.value;
            var show = real.replace(/./g, '●');
            $hidObj.val(real);
            $showObj.val(show);
        }
    }

    function getCursor(elem) {
        //IE 9 ，10，其他浏览器
        if (elem.selectionStart != undefined) {
            return elem.selectionStart;
        } else { //IE 6,7,8
            var range = document.selection.createRange();
            range.moveStart("character", -elem.value.length);
            var len = range.text.length;
            return len;
        }
    }
    // 设置光标位置
    function setCursor(textDom, pos) {
        if (textDom.setSelectionRange) {
            // IE Support
            textDom.focus();
            textDom.setSelectionRange(pos, pos);
        } else if (textDom.createTextRange) {
            // Firefox support
            var range = textDom.createTextRange();
            range.collapse(true);
            range.moveEnd('character', pos);
            range.moveStart('character', pos);
            range.select();
        }
    }
})($);
