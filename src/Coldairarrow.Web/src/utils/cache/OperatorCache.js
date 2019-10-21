let OperatorCache = {
    info: {},
    permissions: [],
    hasPermission(thePermission) {
        return this.permissions.includes(thePermission)
    },
    setInfo(theInfo) {
        this.info = theInfo
    },
    setPermission(thePermissions) {
        this.permissions = thePermissions
    }
}

export default OperatorCache