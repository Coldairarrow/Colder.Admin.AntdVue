//构建请求参数，使用默认签名
function buildRequestParam(businessParam) {
    var appId = config.appId;
    var appSecret = config.appSecret;

    var requestParam = {};
    Object.keys(businessParam).forEach(function (aKey) {
        requestParam[aKey] = businessParam[aKey];
    });
    requestParam["appId"] = appId;
    requestParam["time"] = (new Date()).format("yyyy-MM-dd hh:mm:ss");

    var signBuilder = "";
    Object.keys(requestParam).sort().forEach(function (aKey) {
        var value = "";
        if (requestParam[aKey]) {
            value = requestParam[aKey];
        }
        signBuilder += (aKey + value)
    });
    signBuilder += appSecret;
    var sign = md5(signBuilder).toUpperCase();
    requestParam["sign"] = sign;

    return requestParam;
}