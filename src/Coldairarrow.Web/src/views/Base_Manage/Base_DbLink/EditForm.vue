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
        <a-form-item label="连接名" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['LinkName', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
        <a-form-item label="连接字符串" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-textarea autosize v-decorator="['ConnectionStr', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
        <a-form-item label="数据库类型" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-select v-decorator="['DbType', { rules: [{ required: true }], initialValue: 'SqlServer' }]">
            <a-select-option key="SqlServer">SqlServer</a-select-option>
            <a-select-option key="MySql">MySql</a-select-option>
            <a-select-option key="Oracle">Oracle</a-select-option>
            <a-select-option key="PostgreSql">PostgreSql</a-select-option>
          </a-select>
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
      entity: {}
    }
  },
  methods: {
    add() {
      this.entity = {}
      this.visible = true
      this.form.resetFields()
    },
    edit(id) {
      this.visible = true

      this.$nextTick(() => {
        this.formFields = this.form.getFieldsValue()

        this.$http.post('/Base_Manage/Base_DbLink/GetTheData', { id: id }).then(resJson => {
          this.entity = resJson.Data
          var setData = {}
          Object.keys(this.formFields).forEach(item => {
            setData[item] = this.entity[item]
          })
          this.form.setFieldsValue(setData)
        })
      })
    },
    handleSubmit() {
      this.form.validateFields((errors, values) => {
        //校验成功
        if (!errors) {
          this.entity = Object.assign(this.entity, this.form.getFieldsValue())

          this.confirmLoading = true
          this.$http.post('/Base_Manage/Base_DbLink/SaveData', this.entity).then(resJson => {
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
    }
  }
}
</script>
