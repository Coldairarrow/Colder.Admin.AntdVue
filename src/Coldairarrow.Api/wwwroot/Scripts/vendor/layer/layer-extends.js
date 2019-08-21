//添加id映射
function setLayerIndexMap(id, index) {
    if (!window._layerIndexMap) {
        window._layerIndexMap = {};
    }
    window._layerIndexMap[id] = index;
}

//删除id映射
function deleteLayerIndexMap(id) {
    if (window._layerIndexMap) {
        if (window._layerIndexMap[id]) {
            delete window._layerIndexMap[id];
        }
    }
}

//寻找弹框对象,返回子Iframe的window
function dialogFindWindow(dialogId) {
    var index = window._layerIndexMap[dialogId];
    var layero = $('#layui-layer' + index);
    var iframeWin = window[layero.find('iframe')[0]['name']];

    return iframeWin;
}

//寻找弹框对象,返回子Iframe的body
function dialogFindBody(dialogId) {
    var index = window._layerIndexMap[dialogId];
    var body = layer.getChildFrame('body', index);

    return body;
}

//打开弹框,传入参数对象
function dialogOpen(options) {
    var _options = {
        id: 'form',
        title: '表单',
        width: '60.1%',
        height: '80%',
        url: '',
        btn: [],
        yes: function () { }
    };
    $.extend(_options, options);
    var index = layer.open({
        type: 2,
        title: _options.title,
        maxmin: true,
        btn: _options.btn,
        area: [_options.width, _options.height],
        content: _options.url,
        yes: function (index, layero) {
            var childbody = layer.getChildFrame('body', index);
            var childIframeWin = window[layero.find('iframe')[0]['name']]; //得到iframe页的窗口对象，执行iframe页的方法：iframeWin.method();
            _options.yes(childIframeWin, childbody);
        }
    });
    setLayerIndexMap(_options.id, index);
}

//关闭弹框,dialogId为弹框参数id,若不传dialogId则为关闭当前框
function dialogClose(dialogId) {
    if (dialogId) {
        layer.close(window._layerIndexMap[dialogId]);
        deleteLayerIndexMap(dialogId);
    } else {
        var index = parent.layer.getFrameIndex(window.name);
        parent.layer.close(index);
    }
}

//弹框成功提示
function dialogSuccess(msg) {
    msg = msg || '操作成功';
    layer.msg(msg, { icon: 1 });
}

//显示消息
function dialogMsg(msg) {
    layer.msg(msg);
}

//弹出警告消息框
function dialogError(msg) {
    msg = msg || '操作失败'
    layer.msg(msg, { icon: 5 });
}

//弹出确认框
function dialogComfirm(msg, succcess, cancel) {
    var _succcess = succcess || function () { };
    var _cancel = cancel || function () { };
    layer.confirm(msg, {
        btn: ['确认', '取消']
    }, function () {
        _succcess();
    }, function () {
        _cancel();
    });
}

//加载中
function loading(bool) {
    if (bool == false) {
        layer.close(window._layerLoadingIndex);
    } else {
        window._layerLoadingIndex = layer.load(1, {
            shade: [0.5, '#fff']
        });
    }
}