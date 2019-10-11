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
        <a-form-item label="部门名" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['Name', { rules: [{ required: true, message: '请输入部门名' }] }]" />
        </a-form-item>
        <a-form-item label="上级部门" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-tree-select
            style="width: 300px"
            allowClear
            :dropdownStyle="{ maxHeight: '400px', overflow: 'auto' }"
            :treeData="ParentIdTreeData"
            placeholder="请选择上级部门"
            treeDefaultExpandAll
            v-decorator="['ParentId', { rules: [{ required: false }] }]"
          ></a-tree-select>
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
      ParentIdTreeData: []
    }
  },
  methods: {
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

        this.$http.post('/Base_Manage/Base_Department/GetTheData', { id: id }).then(resJson => {
          this.entity = resJson.Data
          var setData = {}
          Object.keys(this.formFields).forEach(item => {
            setData[item] = this.entity[item]
          })
          this.form.setFieldsValue(setData)

          this.init()
        })
      })
    },
    handleSubmit() {
      this.form.validateFields((errors, values) => {
        //校验成功
        if (!errors) {
          this.entity = Object.assign(this.entity, this.form.getFieldsValue())

          this.confirmLoading = true
          this.$http.post('/Base_Manage/Base_Department/SaveData', this.entity).then(resJson => {
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
    },
    init() {
      this.$http.post('/Base_Manage/Base_Department/GetTreeDataList').then(resJson => {
        if (resJson.Success) {
          this.ParentIdTreeData = resJson.Data
        }
      })
    }
  }
}
</script>
