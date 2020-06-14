<template>
  <a-modal
    title="编辑表单"
    width="40%"
    :visible="visible"
    :confirmLoading="confirmLoading"
    @ok="handleSubmit"
    @cancel="()=>{this.visible=false}"
  >
    <a-spin :spinning="confirmLoading">
      <a-form-model ref="form" :model="entity" :rules="rules" v-bind="layout">
        <a-form-model-item label="角色名" prop="RoleName">
          <a-input v-model="entity.RoleName" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="权限" prop="AppSecret">
          <a-tree
            showLine
            v-if="actionsTreeData && actionsTreeData.length"
            checkable
            @check="onCheck"
            :autoExpandParent="true"
            v-model="checkedKeys"
            :treeData="actionsTreeData"
            :defaultExpandAll="true"
            :checkStrictly="true"
          />
        </a-form-model-item>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
import TreeHelper from '@/utils/helper/TreeHelper'

export default {
  props: {
    afterSubmit: {
      type: Function,
      default: null
    }
  },
  data() {
    return {
      layout: {
        labelCol: { span: 5 },
        wrapperCol: { span: 18 }
      },
      visible: false,
      confirmLoading: false,
      entity: {},
      actionsTreeData: [],
      allActionList: [],
      checkedKeys: { checked: [] },
      rules: {
        RoleName: [{ required: true, message: '必填' }]
      }
    }
  },
  methods: {
    init() {
      this.visible = true
      this.entity = {}
      this.$nextTick(() => {
        this.$refs['form'].clearValidate()
      })

      this.$http.post('/Base_Manage/Base_Action/GetActionTreeList', {}).then(resJson => {
        if (resJson.Success) {
          this.actionsTreeData = resJson.Data
        }
      })
      this.$http.post('/Base_Manage/Base_Action/GetAllActionList', {}).then(resJson => {
        if (resJson.Success) {
          this.allActionList = resJson.Data
        }
      })
    },
    openForm(id) {
      this.init()

      if (id) {
        this.$http.post('/Base_Manage/Base_Role/GetTheData', { id: id }).then(resJson => {
          this.entity = resJson.Data

          this.checkedKeys = { checked: this.entity['Actions'] }
        })
      }
    },
    onCheck(checkedKeys, e) {
      // console.log('勾选')
      // console.log(checkedKeys)
      // console.log(this.checkedKeys)
      //勾选事件,勾选节点时同时勾选所有父节点和子节点
      var value = e.node.value
      var newChecked = []
      if (e.checked) {
        var parentIds = TreeHelper.getParentIds(value, this.allActionList)
        var children = TreeHelper.getChildrenIds(value, this.allActionList)
        var addNodes = parentIds.concat(children).filter(item => !this.checkedKeys.checked.includes(item))
        newChecked = this.checkedKeys.checked.concat(addNodes)
      } else {
        //取消勾选事件,取消勾选所有子节点
        var children = TreeHelper.getChildrenIds(value, this.allActionList)
        children.push(value)
        newChecked = this.checkedKeys.checked.filter(item => !children.includes(item))
      }

      this.checkedKeys = { checked: newChecked }
    },
    handleSubmit() {
      this.$refs['form'].validate(valid => {
        if (!valid) {
          return
        }
        this.confirmLoading = true
        this.entity['Actions'] = this.checkedKeys.checked
        this.$http.post('/Base_Manage/Base_Role/SaveData', this.entity).then(resJson => {
          this.confirmLoading = false

          if (resJson.Success) {
            this.$message.success('操作成功!')
            this.afterSubmit()
            this.visible = false
          } else {
            this.$message.error(resJson.Msg)
          }
        })
      })
    }
  }
}
</script>
