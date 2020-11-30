import axios from 'axios'
import TokenCache from '@/utils/cache/TokenCache'
import defaultSettings from '@/config/defaultSettings'
import ProcessHelper from '@/utils/helper/ProcessHelper'
import moment from 'moment'
var uuid = require('node-uuid')
var md5 = require("md5")

const rootUrl = () => {
    if (ProcessHelper.isProduction() || ProcessHelper.isPreview()) {
        return defaultSettings.publishRootUrl
    } else {
        return defaultSettings.localRootUrl
    }
}

export const Axios = axios.create({
    baseURL: rootUrl(),
    timeout: defaultSettings.apiTimeout
})

// 在发送请求之前做某件事
Axios.interceptors.request.use(config => {
    // 设置以 form 表单的形式提交参数，如果以 JSON 的形式提交表单，可忽略
    // if (config.method === 'post') {
    //     // JSON 转换为 FormData
    //     const formData = new FormData()
    //     Object.keys(config.data).forEach(key => formData.append(key, config.data[key]))
    //     config.data = formData
    // }

    //CheckSign签名检验
    let appId = defaultSettings.appId
    let appSecret = defaultSettings.appSecret
    let guid = uuid.v4()
    let time = moment().format("YYYY-MM-DD HH:mm:ss")
    let body = ''
    if (config.data) {
        body = JSON.stringify(config.data)
    }
    let sign = md5(appId + time + guid + body + appSecret)

    config.headers.appId = appId;
    config.headers.time = time;
    config.headers.guid = guid;
    config.headers.sign = sign;

    //携带token
    if (TokenCache.getToken()) {
        config.headers.Authorization = 'Bearer ' + TokenCache.getToken()
    }
    return config
}, erroror => {
    return Promise.reject(erroror)
})

//返回状态判断(添加响应拦截器)
Axios.interceptors.response.use(res => {
    return res.data
}, error => {
    if (error && error.response) {
        switch (error.response.status) {
            case 400:
                error.message = '请求错误'
                break

            case 401:
                error.message = '未授权，请登录'
                TokenCache.deleteToken()
                location.href = '/'
                break

            case 403:
                error.message = '拒绝访问'
                break

            case 404:
                error.message = `请求地址出错: ${error.response.config.url}`
                break

            case 408:
                error.message = '请求超时'
                break

            case 500:
                error.message = '服务器内部错误'
                break

            case 501:
                error.message = '服务未实现'
                break

            case 502:
                error.message = '网关错误'
                break

            case 503:
                error.message = '服务不可用'
                break

            case 504:
                error.message = '网关超时'
                break

            case 505:
                error.message = 'HTTP版本不受支持'
                break

            default:
        }
    }

    return Promise.resolve({ Success: false, Msg: error.message })
})

export default {
    install(Vue) {
        Object.defineProperty(Vue.prototype, '$http', { value: Axios })
        Object.defineProperty(Vue.prototype, '$rootUrl', { value: rootUrl() })
    }
}