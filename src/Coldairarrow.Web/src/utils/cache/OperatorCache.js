let permissions = []

let OperatorCache = {
    info: {},
    hasPermission(thePermission) {
        return permissions.includes(thePermission)
    },
    setInfo(theInfo) {
        this.info = theInfo
    },
    setPermission(thePermissions) {
        permissions = thePermissions
    }
}

export default OperatorCache