import axios from 'axios'

const rootUrl = 'http://localhost:40000'
const timeout = 10000

export const Axios = axios.create({
    baseURL: rootUrl,
    timeout: timeout,
})

//POST传参序列化(添加请求拦截器)
// 在发送请求之前做某件事
Axios.interceptors.request.use(config => {
    // 设置以 form 表单的形式提交参数，如果以 JSON 的形式提交表单，可忽略
    // if (config.method === 'post') {
    //     // JSON 转换为 FormData
    //     const formData = new FormData()
    //     Object.keys(config.data).forEach(key => formData.append(key, config.data[key]))
    //     config.data = formData
    // }

    // 下面会说在什么时候存储 token
    // if (localStorage.token) {
    //     config.headers.Authorization = 'JWT ' + localStorage.token
    // }
    return config
}, error => {
    // alert("错误的传参", 'fail')
    return Promise.reject(error)
})

//返回状态判断(添加响应拦截器)
Axios.interceptors.response.use(res => {
    //对响应数据做些事
    // if (!res.data.success) {
    //     alert(res.error_msg)
    //     return Promise.reject(res)
    // }
    return res.data
}, error => {
    // if (error.response.status === 401) {
    //     // 401 说明 token 验证失败
    //     // 可以直接跳转到登录页面，重新登录获取 token
    //     location.href = '/login'
    // } else if (error.response.status === 500) {
    //     // 服务器错误
    //     // do something
    //     return Promise.reject(error.response.data)
    // }
    // // 返回 response 里的错误信息
    return Promise.reject(error.response.data)
})

export default {
    install(Vue) {
        Object.defineProperty(Vue.prototype, '$http', { value: Axios })
    }
}