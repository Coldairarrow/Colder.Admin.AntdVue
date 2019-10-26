<template>
  <a-modal
    title="编辑表单"
    width="40%"
    :visible="visible"
    :confirmLoading="confirmLoading"
    @ok="handleSubmit"
    @cancel="handleCancel"
  >
    <a-spin :spinning="confirmLoading">
      <a-form :form="form">
        <a-form-item label="角色名" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['RoleName', { rules: [{ required: true, message: '请输入角色名' }] }]" />
        </a-form-item>
        <a-form-item label="权限" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-tree
            showLine
            v-if="actionsTreeData && actionsTreeData.length"
            checkable
            @check="onCheck"
            :autoExpandParent="autoExpandParent"
            v-model="checkedKeys"
            :treeData="actionsTreeData"
            :defaultExpandAll="true"
            :checkStrictly="true"
          />
        </a-form-item>
      </a-form>
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
      form: this.$form.createForm(this),
      labelCol: { xs: { span: 24 }, sm: { span: 7 } },
      wrapperCol: { xs: { span: 24 }, sm: { span: 13 } },
      visible: false,
      confirmLoading: false,
      formFields: {},
      entity: {},
      actionsTreeData: [],
      allActionList: [],
      autoExpandParent: true,
      checkedKeys: { checked: [] }
    }
  },
  methods: {
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
    add() {
      this.entity = {}
      this.visible = true
      this.form.resetFields()
      this.init()
    },
    edit(id) {
      this.visible = true

      this.$nextTick(() => {
        this.formFields = this.form.getFieldsValue()

        this.$http.post('/Base_Manage/Base_Role/GetTheData', { id: id }).then(resJson => {
          this.entity = resJson.Data
          var setData = {}
          Object.keys(this.formFields).forEach(item => {
            setData[item] = this.entity[item]
          })
          this.form.setFieldsValue(setData)
          this.checkedKeys = { checked: this.entity['Actions'] }

          this.init()
        })
      })
    },
    handleSubmit() {
      this.form.validateFields((errors, values) => {
        //校验成功
        if (!errors) {
          this.entity = Object.assign(this.entity, this.form.getFieldsValue())

          this.entity['actionsJson'] = JSON.stringify(this.checkedKeys.checked)
          this.confirmLoading = true
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
        }
      })
    },
    handleCancel() {
      this.visible = false
      this.checkedKeys.checked = []
    },
    init() {
      this.$http.post('/Base_Manage/Base_Action/GetActionTreeList').then(resJson => {
        if (resJson.Success) {
          this.actionsTreeData = resJson.Data
        }
      })
      this.$http.post('/Base_Manage/Base_Action/GetAllActionList').then(resJson => {
        if (resJson.Success) {
          this.allActionList = resJson.Data
        }
      })
    }
  }
}
</script>
