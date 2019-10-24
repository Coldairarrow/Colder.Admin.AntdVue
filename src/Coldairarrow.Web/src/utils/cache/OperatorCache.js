import { Axios } from "@/utils/plugin/axios-plugin"

let permissions = []
let inited = false

let OperatorCache = {
    info: {},
    inited() {
        return inited
    },
    init() {
        return new Promise((res, rej) => {
            Axios.post('/')
        })
    },
    hasPermission(thePermission) {
        return permissions.includes(thePermission)
    }
}

export default OperatorCache