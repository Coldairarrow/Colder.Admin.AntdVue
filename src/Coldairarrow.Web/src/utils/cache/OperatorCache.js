import { Axios } from "@/utils/plugin/axios-plugin"

let permissions = []
let inited = false

let OperatorCache = {
    info: {},
    inited() {
        return inited
    },
    init(callBack) {
        if (inited)
            callBack()
        else {
            Axios.post('/Base_Manage/Home/GetOperatorInfo').then(resJson => {
                this.info = resJson.Data.UserInfo
                permissions = resJson.Data.Permissions
                inited = true
                callBack()
            })
        }
    },
    hasPermission(thePermission) {
        return permissions.includes(thePermission)
    },
    clear() {
        inited = false
        permissions = []
        this.info = {}
    }
}

export default OperatorCache