<template>
  <a-modal
    title="生成配置"
    width="50%"
    :visible="visible"
    :confirmLoading="confirmLoading"
    @ok="handleSubmit"
    @cancel="handleCancel"
  >
    <a-spin :spinning="confirmLoading">
      <a-form :form="form">
        <a-form-item label="生成类型" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-checkbox-group
            :options="options"
            v-decorator="['buildType', { rules: [{ required: true, message: '必填' }], initialValue: [0, 1, 2, 3] }]"
          />
        </a-form-item>
        <a-form-item label="生成区域" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-textarea autosize v-decorator="['area', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
      </a-form>
    </a-spin>
  </a-modal>
</template>

<script>
const options = [
  { label: '实体层', value: 0 },
  { label: '业务层', value: 1 },
  { label: '接口层', value: 2 },
  { label: '页面层', value: 3 }
]
export default {
  data() {
    return {
      options: options,
      form: this.$form.createForm(this),
      labelCol: { xs: { span: 24 }, sm: { span: 7 } },
      wrapperCol: { xs: { span: 24 }, sm: { span: 13 } },
      visible: false,
      confirmLoading: false,
      formFields: {},
      entity: {},
      tables: []
    }
  },
  methods: {
    openForm(tables) {
      this.tables = tables
      this.init()
    },
    init() {
      this.entity = {}
      this.visible = true
      this.form.resetFields()
    },
    handleSubmit() {
      this.form.validateFields((errors, values) => {
        //校验成功
        if (!errors) {
          this.entity = Object.assign(this.entity, this.form.getFieldsValue())

          console.log(this.entity)
          // this.confirmLoading = true
          // this.$http.post('/Base_Manage/Base_DbLink/SaveData', this.entity).then(resJson => {
          //   this.confirmLoading = false

          //   if (resJson.Success) {
          //     this.$message.success('操作成功!')
          //     this.afterSubmit()
          //     this.visible = false
          //   } else {
          //     this.$message.error(resJson.Msg)
          //   }
          // })
        }
      })
    },
    handleCancel() {
      this.visible = false
    }
  }
}
</script>
