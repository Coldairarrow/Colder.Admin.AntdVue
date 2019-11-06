<template>
  <!-- <a-form :form="form"> -->
  <!-- <a-form-item label="" :labelCol="labelCol" :wrapperCol="wrapperCol"> -->
  <a-select
    id="select"
    ref="select"
    :allowClear="allowClear"
    :showSearch="showSearch"
    :filterOption="filterOption"
    @search="handleSearch"
    @change="handleChange"
    :mode="mode"
    v-decorator="['UserList', { rules: [{ required: true, message: '必选' }] }]"
  >
    <a-select-option v-for="item in thisOptions" :key="item.value">{{ item.text }}</a-select-option>
  </a-select>
  <!-- </a-form-item>
  </a-form> -->
</template>

<script>
import Select from 'ant-design-vue/es/select'

export default {
  extends: Select,
  props: {
    value: null,
    form: null,
    url: {
      //远程获取选项接口地址,接口返回数据结构:[{value:'',text:''}]
      type: String,
      default: null
    },
    allowClear: {
      //允许清空
      type: Boolean,
      default: true
    },
    searchMode: {
      //搜索模式,'':关闭搜索,'local':本地搜索,'server':服务端搜索
      type: String,
      default: ''
    },
    options: {
      //下拉项配置,若无url则必选,结构:[{value:'',text:''}]
      type: Array,
      default: () => []
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
  mounted() {
    this.mode = this.multiple ? 'multiple' : 'default'
    if (this.searchMode) {
      this.showSearch = true
      if (this.searchMode == 'local') {
        this.filterOption = true
      } else {
        this.filterOption = false
      }
    }
    if (!this.url && this.options.length > 0) {
      this.thisOptions = this.options
    }
    // this.form.getFieldDecorator('select', { rules: [{ required: true, message: '必选' }] })
    this.reload()
    // var initValue = { UserList: ['Admin'] }
    // this.$nextTick(() => {
    //   this.form.setFieldsValue(initValue)
    //   this.reloadUser(null, ['Admin'])
    // })
  },
  data() {
    return {
      // form: this.$form.createForm(this),
      labelCol: { xs: { span: 24 }, sm: { span: 7 } },
      wrapperCol: { xs: { span: 24 }, sm: { span: 13 } },
      filterOption: false, //本地搜索,非远程搜索
      thisOptions: [],
      mode: '',
      showSearch: false,
      isInnerchange: false
    }
  },
  watch: {
    value(value) {
      if (!this.isInnerchange) {
        this.$refs.select.$refs.vcSelect.fireChange(value)
      }
      this.isInnerchange = false
    }
  },
  methods: {
    reload(q) {
      if (!this.url) {
        return
      }
      let selected = []
      if (this.multiple) {
        selected = this.$refs.select.value
      }
      this.$http
        .post(this.url, {
          q: q || '',
          selectedValueJson: JSON.stringify(selected || [])
        })
        .then(resJson => {
          if (resJson.Success) {
            this.thisOptions = resJson.Data
          }
        })
    },
    handleSearch(value) {
      this.reload(value)
    },
    handleChange(value) {
      this.isInnerchange = true
      this.$emit('input', value)
    }
  }
}
</script>
