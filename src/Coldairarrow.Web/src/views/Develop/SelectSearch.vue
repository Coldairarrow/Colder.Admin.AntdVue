<template>
  <a-card :bordered="false">
    <a-form :form="form">
      <a-form-item label="远程搜索多选下拉" :labelCol="labelCol" :wrapperCol="wrapperCol">
        <!--
    value: null,
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
    multiple: {
      type: Boolean,
      default: false
    }
        -->
        <c-select
          v-model="entity.UserList"
          multiple
          url="/Base_Manage/Base_User/GetOptionList"
          searchMode="server"
        ></c-select>
      </a-form-item>
      <a-form-item label="本地搜索下拉" :labelCol="labelCol" :wrapperCol="wrapperCol">
        <c-select v-model="entity.UserList1" multiple searchMode="local" :options="options"></c-select>
      </a-form-item>
      <a-form-item label="设置值" :labelCol="labelCol" :wrapperCol="wrapperCol">
        <a-button type="primary" @click="setValue()">设置数据</a-button>
      </a-form-item>
      <a-form-item label="提交" :labelCol="labelCol" :wrapperCol="wrapperCol">
        <a-button type="primary" @click="handleSubmit()">提交数据</a-button>
      </a-form-item>
    </a-form>
  </a-card>
</template>

<script>
import CSelect from '@/components/CSelect/CSelect'
const options = [
  { value: '1181928860648738816', text: '小花' },
  { value: '1183363221872971776', text: 'aaa' },
  { value: '1191969358797082624', text: '小刘' },
  { value: '1191969390925451264', text: '小李' },
  { value: '1191969540813099008', text: '小王' },
  { value: 'Admin', text: '超级管理员' }
]

export default {
  components: {
    CSelect
  },
  data() {
    return {
      form: this.$form.createForm(this),
      labelCol: { xs: { span: 24 }, sm: { span: 7 } },
      wrapperCol: { xs: { span: 24 }, sm: { span: 13 } },
      entity: {},
      options: options
    }
  },
  methods: {
    setValue() {
      this.entity = { UserList: ['Admin'] }
    },
    handleSubmit() {
      console.log('当前值:', this.entity)
      this.form.validateFields((errors, values) => {
        //c-select组件若需要校验则必须手动校验
        if (!this.entity.UserList || this.entity.UserList.length == 0) {
          this.$message.error('请选择用户')
          return
        }
        if (!errors) {
          //校验成功
          console.log('校验通过')
        }
      })
    }
  }
}
</script>
