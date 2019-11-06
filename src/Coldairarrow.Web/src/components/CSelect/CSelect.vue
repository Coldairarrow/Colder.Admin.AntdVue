<template>
  <a-select
    allowClear
    showSearch
    :filterOption="false"
    @search="handleSearch"
    @change="handleChange"
    mode="multiple"
    v-decorator="[{ rules: [{ required: true,message: '必选' }] }]"
  >
    <a-select-option v-for="item in UserOptionList" :key="item.value">{{ item.text }}</a-select-option>
  </a-select>
</template>

<script>
export default {
  props: {
    url: {
      //远程获取选项接口地址,接口返回数据结构:[{value:'',text:''}]
      type: String,
      default: null
    },
    allowClear: {
      type: Boolean,
      default: true
    },
    showSearch: {
      type: Boolean,
      default: false
    },
    options: {
      //下拉项配置,若无url则必选,结构:[{value:'',text:''}]
      type: Array,
      default: []
    },
    required: {
      type: Boolean,
      default: false
    },
    multiple: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      filterOption: false, //本地搜索,非远程搜索
      options: []
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
