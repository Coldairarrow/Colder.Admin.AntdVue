const TypeHelper = {
    isString(o) { //是否字符串
        return Object.prototype.toString.call(o).slice(8, -1) === 'String'
    },
    isNumber(o) { //是否数字
        return Object.prototype.toString.call(o).slice(8, -1) === 'Number'
    },
    isBoolean(o) { //是否boolean
        return Object.prototype.toString.call(o).slice(8, -1) === 'Boolean'
    },
    isFunction(o) { //是否函数
        return Object.prototype.toString.call(o).slice(8, -1) === 'Function'
    },
    isNull(o) { //是否为null
        return Object.prototype.toString.call(o).slice(8, -1) === 'Null'
    },
    isUndefined(o) { //是否undefined
        return Object.prototype.toString.call(o).slice(8, -1) === 'Undefined'
    },
    isObj(o) { //是否对象
        return Object.prototype.toString.call(o).slice(8, -1) === 'Object'
    },
    isArray(o) { //是否数组
        return Object.prototype.toString.call(o).slice(8, -1) === 'Array'
    },
    isDate(o) { //是否时间
        return Object.prototype.toString.call(o).slice(8, -1) === 'Date'
    },
    isRegExp(o) { //是否正则
        return Object.prototype.toString.call(o).slice(8, -1) === 'RegExp'
    },
    isError(o) { //是否错误对象
        return Object.prototype.toString.call(o).slice(8, -1) === 'Error'
    },
    isSymbol(o) { //是否Symbol函数
        return Object.prototype.toString.call(o).slice(8, -1) === 'Symbol'
    },
    isPromise(o) { //是否Promise对象
        return Object.prototype.toString.call(o).slice(8, -1) === 'Promise'
    },
    isSet(o) { //是否Set对象
        return Object.prototype.toString.call(o).slice(8, -1) === 'Set'
    },
    isFalse(o) {
        if (!o || o === 'null' || o === 'undefined' || o === 'false' || o === 'NaN') return true
        return false
    },
    isTrue(o) {
        return !this.isFalse(o)
    }
}

export default TypeHelper