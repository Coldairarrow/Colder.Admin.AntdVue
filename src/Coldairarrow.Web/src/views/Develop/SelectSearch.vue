<template>
  <a-card :bordered="false">
    <a-form :form="form">
      <a-form-item label="用户选择" :labelCol="labelCol" :wrapperCol="wrapperCol">
        <a-select
          allowClear
          showSearch
          :filterOption="false"
          @search="handleSearch"
          @change="handleChange"
          mode="multiple"
          v-decorator="['UserList', { rules: [{ required: true,message: '必选' }] }]"
        >
          <a-select-option v-for="item in UserOptionList" :key="item.value">{{ item.text }}</a-select-option>
        </a-select>
      </a-form-item>
      <a-form-item label="提交" :labelCol="labelCol" :wrapperCol="wrapperCol">
        <a-button type="primary"  @click="handleSubmit()">提交数据</a-button>
      </a-form-item>
    </a-form>
  </a-card>
</template>

<script>
export default {
  data() {
    return {
      form: this.$form.createForm(this),
      labelCol: { xs: { span: 24 }, sm: { span: 7 } },
      wrapperCol: { xs: { span: 24 }, sm: { span: 13 } },
      UserOptionList: []
    }
  },
  mounted() {
    var initValue = { UserList: ['Admin'] }
    this.$nextTick(() => {
      this.form.setFieldsValue(initValue)
      this.reloadUser(null, ['Admin'])
    })
  },
  methods: {
    reloadUser(q, selected) {
      this.$http
        .post('/Base_Manage/Base_User/GetOptionList', {
          q: q || '',
          selectedValueJson: JSON.stringify(selected || [])
        })
        .then(resJson => {
          if (resJson.Success) {
            this.UserOptionList = resJson.Data
          }
        })
    },
    handleSearch(value) {
      var selected = this.form.getFieldsValue()['UserList']
      this.reloadUser(value, selected)
    },
    handleChange(value) {
      //   console.log(value)
      //   this.value = value
    },
    handleSubmit() {
      this.form.validateFields((errors, values) => {
        //校验成功
        if (!errors) {
          console.log('表单数据:', this.form.getFieldsValue())
        }
      })
    }
  }
}
</script>
