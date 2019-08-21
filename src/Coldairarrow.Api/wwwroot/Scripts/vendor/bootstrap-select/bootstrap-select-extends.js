/*
 拓展selectpicker,功能如下：
 支持远程异步数据
 支持远程异步数据搜索
 支持数据初始化并回显
 用法:
$('#roleList').selectpicker({
    url: '/Base_SysManage/Base_User/GetDataList_NoPagin',//远程地址
    value: 'Admin',//初始化值,字符串或字符串数组
    liveSearch: true,//搜索
    valueField: 'UserId',
    textField: 'RealName'
});
 */
(function ($) {
    if (!$.fn._selectpicker) {
        $.fn._selectpicker = $.fn.selectpicker;
    }
    $.fn.selectpicker = function (options, param) {
        var _this = this;

        var methods = {
            reload: function () {
                return init(getOption());
            },
            setOption: function (newOption) {
                var oldOption = getOption();
                newOption = $.extend({}, oldOption, newOption);
                setOption(newOption);
            }
        };

        if (typeof options == 'string') {//执行方法
            if (methods[options]) {
                return methods[options](param);
            } else {
                return $(_this)._selectpicker(options, param);
            }
        } else {//初始化
            init(options);
        }


        function init(options) {
            var pleaseSelect = false;
            //只有远程并单选时添加“请选择”选项
            if (options.url) {
                if (!$(_this)[0].multiple) {
                    pleaseSelect = true;
                }
            }

            var defaults = {
                value: [],
                url: null,
                data: [],
                valueField: 'value',
                textField: 'text',
                onSelect: null,
                pleaseSelect: pleaseSelect,
                onLoadSuccess: null
            };
            var _options = $.extend(defaults, options);

            $(_this)._selectpicker(_options);
            _options.value = initValue(_options.value);

            setOption(_options);

            build();

            function build() {
                getData(function () {
                    renderHtml();
                    bindEvent();
                    if (_options.onLoadSuccess) {
                        _options.onLoadSuccess();
                    }
                });
            }

            function initValue(value) {
                var newValues = [];

                var type = getType(value);
                switch (type) {
                    case 'array': newValues = value; break;
                    case 'null':
                    case 'undefined': break;
                    default: newValues.push(value.toString());
                }

                return newValues;
            }

            function setSelected(values) {
                var newValues = initValue(values);
                getOption().value = newValues;
            }

            function getSelected() {
                return getOption().value;
            }

            function renderHtml() {
                var _options = getOption();

                var data = _options.data;
                var selected = _options.value || [];
                $(_this).empty();
                //添加请选择
                if (_options.pleaseSelect) {
                    $(_this).append('<option value="">请选择</option>');
                }
                var multiple = $(_this).prop('multiple');
                for (var i = 0; i < data.length; i++) {

                    var text = data[i][_options.textField];
                    var value = data[i][_options.valueField];

                    var selectedHtml = '';

                    if (selected.indexOf((value || '').toString()) > -1) {
                        selectedHtml = 'selected="selected"';
                    }

                    $(_this).append("<option " + selectedHtml + " value=" + value + ">" + text + "</option>");
                }
                //重新渲染
                $(_this)._selectpicker('refresh');
                $(_this)._selectpicker('render');
            }

            function buildQuery(q) {
                return {
                    values: JSON.stringify(getSelected()),
                    q: q || ''
                }
            }

            //远程搜索
            function remoteSearch(q) {
                if (getOption().url) {
                    setSelected($(_this).val() || []);

                    $.postJSON(getOption().url, buildQuery(q), function (resJson) {
                        getOption().data = resJson;
                        renderHtml();
                    });
                }
            }

            //获取渲染初始数据
            function getData(next) {

                getOption()
                if (getOption().url) {//远程数据
                    $.postJSON(getOption().url, buildQuery(), function (resJson) {
                        getOption().data = resJson || [];
                        next();
                    });
                }
                else if (getOption().data && getOption().data.length > 0) {
                    next();
                }
                else {//本地数据
                    getOption().data = [];
                    $(_this).find('option').each(function () {
                        getOption().data.push({
                            text: $(this).text(),
                            value: $(this).val()
                        });
                    });
                    next();
                }
            }

            //绑定事件
            function bindEvent() {
                //绑定选择事件
                if (getOption().onSelect) {
                    $(_this).on('changed.bs.select', function (e, clickedIndex, newValue, oldValue) {
                        var value = $(e.currentTarget).val();
                        _options.onSelect(value);
                    });
                }

                //绑定输入事件
                if (getOption().url) {
                    var inputTimeout = null;

                    $(_this).on('shown.bs.select', function (e) {
                        var $input = $(_this).parent().find('.bs-searchbox').find('input');
                        $input.unbind('input');

                        $input.keydown(function () {
                            if (inputTimeout) {
                                clearTimeout(inputTimeout);
                            }
                            inputTimeout = setTimeout(function () {
                                remoteSearch($input.val());
                            }, 500);
                        })
                    });
                }
            }
        }

        function setOption(option) {
            $(_this).data('option', option);
        }

        function getOption() {
            return $(_this).data('option');
        }

        //获取数据类型
        function getType(obj) {
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
    };
})($)