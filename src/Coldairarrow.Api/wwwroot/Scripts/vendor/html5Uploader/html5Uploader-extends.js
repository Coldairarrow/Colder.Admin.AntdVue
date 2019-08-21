//拓展为jQuery插件
(function ($) {
    $.fn.html5Uploader = function (options, params) {
        if (typeof options != 'string') {
            var obj = new Html5Uploader('#' + $(this)[0].id, options);
            $(this).data('obj', obj);

            return this;
        } else {
            return $(this).data('obj')[options](params);
        }
    };
})(jQuery);