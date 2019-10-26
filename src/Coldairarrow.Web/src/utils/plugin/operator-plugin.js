import OperatorCache from "@/utils/cache/OperatorCache"

export default {
    install(Vue) {
        Object.defineProperty(Vue.prototype, '$op', { value: OperatorCache })
        Object.defineProperty(Vue.prototype, 'hasPerm', { value: OperatorCache.hasPermission })
    }
}