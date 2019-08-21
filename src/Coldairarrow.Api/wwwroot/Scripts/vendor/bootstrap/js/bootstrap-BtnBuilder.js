//BtnBuilder类，用于构建表格中的按钮组
function BtnBuilder() {
    BtnBuilder.prototype.btnList = ['<div class="btn-group btn-group-sm">'];
};
BtnBuilder.prototype.btnList = [];
BtnBuilder.prototype._btnConfig = {
    name: '',
    icon: '',
    function: '',
    param: []
};
BtnBuilder.prototype.AddBtn = function (options) {
    var _options = $.extend({}, this._btnConfig, options);

    var clickHtml = '';
    if (_options.function) {
        for (var i = 0; i < _options.param.length; i++) {
            _options.param[i] = "'" + _options.param[i] + "'";
        }
        clickHtml = 'onclick="' + _options.function + '(' + _options.param.join(',') + ')"';
    }

    var iconHtml = '';
    if (_options.icon) {
        iconHtml = '<span class="glyphicon ' + _options.icon + '" aria-hidden="true"></span>';
    }

    this.btnList.push('<button ' + clickHtml + ' type="button" class="btn btn-default"  singleSelected=true>' + iconHtml + _options.name + '</button>');
};
BtnBuilder.prototype.build = function () {
    this.btnList.push('</div>');
    return this.btnList.join('');
};
