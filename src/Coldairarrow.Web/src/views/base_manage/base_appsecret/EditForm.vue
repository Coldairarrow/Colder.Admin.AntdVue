<template>
  <a-modal
    title="编辑表单"
    width="40%"
    :visible="visible"
    :confirmLoading="confirmLoading"
    @ok="handleSubmit"
    @cancel="handleCancel"
    :destroyOnClose="false"
    @afterClose="handleClose"
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
      this.visible = true

      this.$nextTick(() => {
        this.form.resetFields()
      })
    },
    edit() {
      this.visible = true

      this.$nextTick(() => {
        this.formFields = this.form.getFieldsValue()

        var data = {
          UserName: '小明',
          Password: '密码',
          RealName: '小明',
          Sex: 1,
          aaaaa: 55,
          bbbbbbbb: 'aaaaaaaaa',
          xxxxxxxxxxxxxxxx: 'sadsadsa'
        }
        var setData = {}
        Object.keys(this.formFields).forEach(item => {
          setData[item] = data[item]
        })
        this.form.setFieldsValue(setData)

        // var obj = Object.assign({ xxxx: 'aaaaaaaaa' }, data)
        // console.log(obj)
        // this.form.setFieldsValue(obj)
      })
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
    },
    handleClose() {
      this.form.resetFields()
    }
  }
}
</script>
