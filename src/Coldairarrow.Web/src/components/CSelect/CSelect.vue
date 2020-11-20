<template>
  <a-select
    ref="select"
    :allowClear="allowClear"
    :showSearch="showSearch"
    :filterOption="filterOption"
    @search="handleSearch"
    @change="handleChange"
    :mode="mode"
    v-model="thisValue"
  >
    <a-select-option v-for="item in thisOptions" :key="item.value">{{ item.text }}</a-select-option>
  </a-select>
</template>

<script>
export default {
  props: {
    value: null,
    url: {
      //远程获取选项接口地址,接口返回数据结构:[{value:'',text:''}]
      type: String,
      default: null,
    },
    allowClear: {
      //允许清空
      type: Boolean,
      default: true,
    },
    searchMode: {
      //搜索模式,'':关闭搜索,'local':本地搜索,'server':服务端搜索
      type: String,
      default: '',
    },
    options: {
      //下拉项配置,若无url则必选,结构:[{value:'',text:''}]
      type: Array,
      default: () => [],
    },
    multiple: {
      type: Boolean,
      default: false,
    },
  },
  mounted() {
    this.mode = this.multiple ? 'multiple' : 'default'
    if (this.searchMode) {
      this.showSearch = true
      if (this.searchMode == 'local') {
        this.filterOption = (input, option) => {
          return option.componentOptions.children[0].text.toLowerCase().indexOf(input.toLowerCase()) >= 0
        }
      } else {
        this.filterOption = false
      }
    }
    if (!this.url && this.options.length > 0) {
      this.thisOptions = this.options
    }
    this.thisValue = this.value
    this.reload()
  },
  data() {
    return {
      filterOption: false, //本地搜索,非远程搜索
      thisOptions: [],
      mode: '',
      showSearch: false,
      isInnerchange: false,
      thisValue: '',
      timeout: null,
      qGlobal: '',
    }
  },
  watch: {
    value(value) {
      this.thisValue = value
    },
  },
  methods: {
    reload(q) {
      if (!this.url) {
        return
      }
      this.qGlobal = q
      clearTimeout(this.timeout)
      this.timeout = setTimeout(() => {
        let selected = []
        if (this.multiple) {
          selected = this.$refs.select.value
        }
        this.$http
          .post(this.url, {
            q: q || '',
            selectedValues: selected || [],
          })
          .then((resJson) => {
            if (resJson.Success && q == this.qGlobal) {
              this.thisOptions = resJson.Data
            }
          })
      }, 300)
    },
    handleSearch(value) {
      this.reload(value)
    },
    handleChange(value) {
      this.$emit('input', value)
    },
  },
}
</script>
