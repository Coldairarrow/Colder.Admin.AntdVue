//关闭所有jquery AJAX缓存
(function ($) {
    $.ajaxSetup({
        cache: false, //关闭AJAX缓存
        dataFilter: function (res, dataType) {//登录失败过滤
            try {
                var resJson = JSON.parse(res);
                if (resJson.ErrorCode == 1)
                    top.document.location.href = '/Home/Login';
            } catch(ex) {

            }
            return res;
        }
    });
})(jQuery);

//获取请求的参数
(function (window) {

    if (window.request) return;

    window.request = function (key) {
        var reg = new RegExp("(^|&)" + key + "=([^&]*)(&|$)");
        var result = window.location.search.substr(1).match(reg);
        return result ? decodeURIComponent(result[2]) : null;
    };

})(window);

//获取根目录
(function (window) {
    if (window.getRootPath)
        return;

    window.getRootPath = function () {
        var strFullPath = window.document.location.href;
        var strPath = window.document.location.pathname;
        var pos = strFullPath.indexOf(strPath);
        var prePath = strFullPath.substring(0, pos);
        return prePath;
    }
})(window);

//拓展$.postJSON方法，使用方式和$.getJSON类似，只不过方法改为POST
(function ($) {

    if ($.postJSON) return;

    $.extend({
        postJSON: function (url, param, callBack) {
            $.ajax({
                type: "POST",
                url: url || "",
                data: param || {},
                dataType: "json",
                success: callBack || function () { }
            });
        }
    });
})(jQuery);

//拓展String的contains方法
(function () {
    if (String.prototype.contains)
        return;

    String.prototype.contains = function (subStr) {
        return this.indexOf(subStr) > -1;
    };
})();

//拓展String的toBlob方法，将base64字符串转为blob对象
(function () {
    if (String.prototype.toBlob)
        return;

    String.prototype.toBlob = function (contentType, sliceSize) {
        var b64Data = this;
        contentType = contentType || '';
        sliceSize = sliceSize || 512;

        var byteCharacters = atob(b64Data);
        var byteArrays = [];

        for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
            var slice = byteCharacters.slice(offset, offset + sliceSize);

            var byteNumbers = new Array(slice.length);
            for (var i = 0; i < slice.length; i++) {
                byteNumbers[i] = slice.charCodeAt(i);
            }

            var byteArray = new Uint8Array(byteNumbers);

            byteArrays.push(byteArray);
        }

        var blob = new Blob(byteArrays, { type: contentType });
        return blob;
    };
})();

//拓展String的toDate方法，将日期字符串转为日期
(function () {
    if (String.prototype.toDate)
        return;
    String.prototype.toDate = function () {
        var str = this;
        str = str.replace(/-/g, "/");
        return new Date(str);
    };
})();

//拓展String的getFileName方法，获取URL中的文件名
(function () {
    if (String.prototype.getFileName)
        return;

    String.prototype.getFileName = function () {
        var str = this;
        var url = str.split('?')[0];
        var pathArray = url.split('/');

        return pathArray[pathArray.length - 1];
    };
})();

//拓展Array的forEach方法，用在某些浏览器没有forEach方法
(function (Array) {
    if (Array.prototype.forEach)
        return;
    Object.defineProperty(Array.prototype, "forEach", {
        value: function (callback) {
            var d = this || [];
            if (!callback) return;

            for (var i = 0; i < d.length; i++) {
                var elem = d[i];
                callback(elem, i);
            }
        },
        enumerable: false
    });
})(Array);

//拓展Array的indexOf方法
(function (Array) {
    if (Array.prototype.indexOf)
        return;
    Object.defineProperty(Array.prototype, "indexOf", {
        value: function (val) {
            for (var i = 0; i < this.length; i++) {
                if (this[i] == val) return i;
            }
            return -1;
        },
        enumerable: false
    });
})(Array);

//拓展Array的exists方法
(function (Array) {
    if (Array.prototype.exists)
        return;

    Object.defineProperty(Array.prototype, "exists", {
        value: function (val) {
            return this.indexOf(val) > -1;
        },
        enumerable: false
    });
})(Array);

//拓展Array的removeItem方法
(function (Array) {
    if (Array.prototype.removeItem)
        return;

    Object.defineProperty(Array.prototype, "removeItem", {
        value: function (val) {
            var index = this.indexOf(val);
            if (index > -1) {
                this.splice(index, 1);
            }
        },
        enumerable: false
    });
})(Array);

//日期格式化
(function () {

    if (Date.prototype.format) return;

    Date.prototype.format = function (fmt) { //author: meizz 
        var o = {
            "M+": this.getMonth() + 1, //月份 
            "d+": this.getDate(), //日 
            "h+": this.getHours(), //小时 
            "m+": this.getMinutes(), //分 
            "s+": this.getSeconds(), //秒 
            "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
            "S": this.getMilliseconds() //毫秒 
        };
        if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        for (var k in o)
            if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        return fmt;
    };

    Date.prototype.addDays = function (d) {
        this.setDate(this.getDate() + d);
    };
    Date.prototype.addWeeks = function (w) {
        this.addDays(w * 7);
    };
    Date.prototype.addMonths = function (m) {
        var d = this.getDate();
        this.setMonth(this.getMonth() + m);
        if (this.getDate() < d)
            this.setDate(0);
    };
    Date.prototype.addYears = function (y) {
        var m = this.getMonth();
        this.setFullYear(this.getFullYear() + y);
        if (m < this.getMonth()) {
            this.setDate(0);
        }
    };
})();

//字符串格式化输入
(function () {

    if (String.prototype.format) return;

    String.prototype.format = function () {
        if (arguments.length == 0) return this;
        for (var s = this, i = 0; i < arguments.length; i++)
            s = s.replace(new RegExp("\\{" + i + "\\}", "g"), arguments[i]);
        return s;
    };

})();

//确保开始时间小于结束时间
(function ($, window) {
    if (window.checkStartEndDate)
        return;

    window.checkStartEndDate = function (startDateId, endDateId) {
        var startDate = $("#" + startDateId).val();
        var endDate = $("#" + endDateId).val();

        var _startDate = new Date(startDate);
        var _endDate = new Date(endDate);
        if (_startDate > _endDate) {
            dialogMsg("请输入选择有效的时间（结束时间必须大于或者等于开始时间！）");
            return false;
        }
        else {
            return true;
        }
    }

})(jQuery, window);

/*jQuery拓展，绑定input file并转为base64字符串
 *使用示例:
    $('#img').bindImgBase64(function (base64) {
        $('#img-display').attr('src', base64);
    });
 */
(function ($) {
    try {
        if (typeof ($) == "undefined")
            throw '缺少jQuery';
        if ($.prototype.bindImgBase64)
            return;

        $.prototype.bindImgBase64 = function (callback) {
            var _callback = callback || function () { };
            var thisElement = this[0];
            thisElement.onchange = function () {
                var img = event.target.files[0];

                // 判断是否图片
                if (!img) {
                    return;
                }
                // 判断图片格式
                if (!(img.type.indexOf('image') == 0 && img.type && /\.(?:jpg|jpeg|png|gif|bmp)$/i.test(img.name))) {
                    throw '图片只能是jpg,jpeg,gif,png,bmp';
                }
                var reader = new FileReader();
                reader.readAsDataURL(img);
                reader.onload = function (e) {
                    var imgBase64 = e.target.result;
                    $(thisElement).attr('base64', imgBase64);
                    _callback(imgBase64);
                }
            };
        };
        $.prototype.getImgBase64 = function () {
            return this.attr('base64');
        };
    } catch (e) {
        console.log(e);
    }
})($);

//拓展FileReader的readAsBinaryString方法
(function () {
    try {
        if (FileReader.prototype.readAsBinaryString)
            return;

        FileReader.prototype.readAsBinaryString = function (fileData) {
            var binary = "";
            var pt = this;
            var reader = new FileReader();
            reader.onload = function (e) {
                var bytes = new Uint8Array(reader.result);
                var length = bytes.byteLength;
                for (var i = 0; i < length; i++)
                    binary += String.fromCharCode(bytes[i]);
            }
            pt.content = binary;
            $(pt).trigger('onload');
            reader.readAsArrayBuffer(fileData);
        }
    } catch (e) {
        console.log(e);
    }
})();

//拓展文件操作，将文件转为base64
//回调参数为文件base64内容和文件名
(function () {
    try {
        if (typeof (jQuery) == 'undefined')
            throw '缺少jQuery插件';

        $.prototype.getFileBase64 = function (callBack) {
            var _callBack = callBack || function () { };
            var file = $(this)[0].files[0];
            var reader = new FileReader();
            reader.readAsBinaryString(file);
            reader.onload = function (e) {
                var bytes = e.target.result;
                var base64 = btoa(bytes);
                var fileName = file.name;
                callBack(base64, fileName);
            }
        };
    } catch (e) {
        console.log(e);
    }
})();

//获取元素中所有没有被disabled的name集合
(function () {
    if (typeof ($) == "undefined")
        throw '缺少jQuery';

    $.prototype.getNames = function () {
        var nameList = [];
        $(this).find('[name]').not('[disabled]').each(function (index, element) {
            var name = $(element).attr('name');
            nameList.push(name);
        });

        return nameList;
    };
})();

//$.fn.getValues
//取表单域的所有值
(function ($) {
    if ($.fn.getValues)
        return;

    $.fn.getValues = function (options) {
        var defaults = {
            checkbox: 'array',
            not: []
        };
        var $container = $(this);

        options = $.extend({}, defaults, options);

        function getValues() {
            var values = {}, ckbFields = [];
            $container.find(":input[name]").each(function () {
                var that = $(this),
                    type = (that.attr("type") || '').toLowerCase(),
                    name = that.attr("name");
                if (type == 'button' || type == 'submit') return true;
                if (!name.length) return true;
                if ($.inArray(name, options.not) >= 0) return true;

                if (type == "checkbox") {
                    if (values[name] != undefined) {
                        return true;
                    }
                    ckbFields.push(name);
                    values[name] = getCheckboxValues(name);
                } else if (type == "radio") {
                    if (values[name] != undefined) {
                        return true;
                    }
                    values[name] = getRadioValue(name);
                } else {
                    var value = that.val();
                    //if (!value.length) return true;

                    if (values[name] == undefined) {
                        values[name] = value;
                    } else {
                        var arr = [];
                        arr.push(values[name]);
                        arr.push(value);

                        values[name] = arr.join(',');
                    }
                }
            });

            if (options.checkbox == 'string') {
                $.each(ckbFields, function (i, v) {
                    if ($.isArray(values[v])) values[v] = values[v].join(',');
                });
            }

            return values;
        }

        function getRadioValue(name) {
            return $container.find("input:radio[name='" + name + "']").val();
        }

        function getCheckboxValues(name) {
            return $.map($container.find("input:checkbox[name='" + name + "']:checked"), function (n) {
                return n.value;
            });
        }

        return getValues();
    };
})(jQuery);

//使用文件base64下载文件
(function () {
    if (window.downloadFileBase64)
        return;

    window.downloadFileBase64 = function (base64, fileName) {
        var blob = base64.toBlob();
        var reader = new FileReader();
        reader.readAsDataURL(blob);
        reader.onload = function (e) {
            // 转换完成，创建一个a标签用于下载
            var a = document.createElement('a');
            a.hidden = true;
            a.download = fileName;
            a.href = e.target.result;
            $("body").append(a);  // 修复firefox中无法触发click
            a.click();
            $(a).remove();
        }
    };
})();

//将对象转为x-www-form-urlencoded格式字符串
(function () {
    if (window.objToFormData)
        return;

    window.objToFormData = function (element, key, list) {
        var list = list || [];
        if (typeof (element) == 'object') {
            for (var idx in element)
                objToFormData(element[idx], key ? key + '[' + idx + ']' : idx, list);
        } else {
            list.push(key + '=' + encodeURIComponent(element));
        }
        return list.join('&');
    };
})();

//使用文件Url下载文件
(function () {
    if (window.downloadFile)
        return;

    window.downloadFile = function (url, fileName, params) {
        if (params) {
            url = url + '?' + objToFormData(params);
        }
        fileName = fileName || url.getFileName();
        saveAs(url, fileName);
    }
})();

//拓展window的toDateString方法
(function () {
    if (window.toDateString)
        return;

    window.toDateString = function (date, fmt) {
        if (date) {
            return date.toDate().format(fmt);
        } else {
            return '';
        }
    };
})();

//拓展window的getType方法
(function () {
    if (window.getType)
        return;

    window.getType = function (obj) {
        if (obj == null)
            return 'null';
        
        var type = typeof (obj);
        if (type == 'object') {
            type = Object.prototype.toString.call(obj);
            if (type == '[object Array]') {
                return 'array';
            } else if (type == '[object Object]') {
                return "object";
            } else {
                return 'param is no object type';
            }
        } else {
            return type;
        }
    }
})();