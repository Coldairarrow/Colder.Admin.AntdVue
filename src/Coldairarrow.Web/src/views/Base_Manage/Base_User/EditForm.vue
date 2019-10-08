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
            v-if="actionsTreeData && actionsTreeData.length"
            checkable
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
      autoExpandParent: true,
      checkedKeys: []
    }
  },
  methods: {
    onCheck(checkedKeys) {
      this.checkedKeys = checkedKeys
    },
    onSelect(selectedKeys, info) {
      this.selectedKeys = selectedKeys
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
          this.checkedKeys = this.entity['Actions']
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
      this.checkedKeys = []
    },
    init() {
      this.$http.post('/Base_Manage/Base_Action/GetActionTreeList').then(resJson => {
        if (resJson.Success) {
          this.actionsTreeData = resJson.Data
        }
      })
    }
  }
}
</script>
