<template>
  <a-modal
    title="编辑表单"
    width="40%"
    :visible="visible"
    :confirmLoading="confirmLoading"
    @ok="handleSubmit"
    @cancel="handleCancel"
    :destroyOnClose="true"
  >
    <a-spin :spinning="confirmLoading">
      <a-form :form="form">
        <a-form-item label="用户名" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['UserName', { rules: [{ required: true, message: '请输入用户名' }] }]" />
        </a-form-item>
        <a-form-item label="密码" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input type="password" v-decorator="['Password', { rules: [{ required: false, message: '' }] }]" />
        </a-form-item>
        <a-form-item label="姓名" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['RealName', { rules: [{ required: true, message: '请输入姓名' }] }]" />
        </a-form-item>
        <a-form-item label="性别" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-select v-decorator="['Sex', { rules: [{ required: true }], initialValue: 1, message: '请输入性别' }]">
            <a-select-option value="1">男</a-select-option>
            <a-select-option value="0">女</a-select-option>
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
  mounted() {
    this.form.setFieldsValue({
      UserName:'',

    })

  },
  data() {
    return {
      form: this.$form.createForm(this),
      labelCol: { xs: { span: 24 }, sm: { span: 7 } },
      wrapperCol: { xs: { span: 24 }, sm: { span: 13 } },
      visible: false,
      confirmLoading: false
    }
  },
  methods: {
    add() {
      this.visible = true
    },
    edit() {
      this.visible = true
    },
    handleSubmit() {
      //   const {
      //     form: { validateFields }
      //   } = this
      //   this.confirmLoading = true
      this.form.validateFields((errors, values) => {
        if (!errors) {
          console.log('数据:', this.form.getFieldsValue())

          this.afterSubmit()
          this.visible = false
        }

        this.confirmLoading = false
      })
    },
    handleCancel() {
      this.visible = false
    }
  }
}
</script>
