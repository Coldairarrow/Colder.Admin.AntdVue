//固定列表结构：[{Id:'xxx',ParentId:'xxx'}]
let TreeHelper = {
    getParentIds(id, allNodes) {
        let parents = []
        let theNode = this.findTheNode(id, allNodes)
        if (theNode.ParentId) {
            parents.push(theNode.ParentId)
            parents.push(...this.getParentIds(theNode.ParentId, allNodes))
        }

        return parents
    },
    getChildrenIds(id, allNodes) {
        var childrenIds = []
        let children = allNodes.filter(item => item.ParentId == id)
            .map(item => item.Id)
        if (children.length > 0) {
            childrenIds.push(...children)
            children.forEach(item => {
                childrenIds.push(...this.getChildrenIds(item, allNodes))
            })
        }
        return childrenIds
    },
    findTheNode(id, allNodes) {
        return allNodes.filter(item => item.Id == id)[0]
    }
}

export default TreeHelper