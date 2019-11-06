<template>
  <a-card :bordered="false">
    <a-form :form="form">
      <a-form-item label="用户选择" :labelCol="labelCol" :wrapperCol="wrapperCol">
        <c-select
          :form="form"
          v-model="entity.UserList"
          multiple
          required
          url="/Base_Manage/Base_User/GetOptionList"
          searchMode="server"
        ></c-select>
      </a-form-item>
      <a-form-item label="提交" :labelCol="labelCol" :wrapperCol="wrapperCol">
        <a-button type="primary" @click="handleSubmit()">提交数据</a-button>
      </a-form-item>
    </a-form>
  </a-card>
</template>

<script>
import CSelect from '@/components/CSelect/CSelect'

export default {
  components: {
    CSelect
  },
  data() {
    return {
      form: this.$form.createForm(this),
      labelCol: { xs: { span: 24 }, sm: { span: 7 } },
      wrapperCol: { xs: { span: 24 }, sm: { span: 13 } },
      entity: {}
    }
  },
  mounted() {
    // console.log(11)
    // var initValue = { UserList: ['Admin'] }
    // this.$nextTick(() => {
    //   this.form.setFieldsValue(initValue)
    //   this.reloadUser(null, ['Admin'])
    // })
  },
  methods: {
    handleSubmit() {
      console.log('当前值:', this.entity)
      this.form.validateFields((errors, values) => {
        //校验成功
        if (!errors) {
          console.log('校验通过')
          // console.log('表单数据:', this.form.getFieldsValue())
        }
      })
    }
  }
}
</script>
