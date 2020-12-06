<template>
  <a-card :bordered="false">
    <a-form :form="form">
      <a-form-item label="文件上传" :labelCol="labelCol" :wrapperCol="wrapperCol">
        <!--v-model为图片连接地址(可传单个或数组),maxCount为最大上传数:默认为1-->
        <c-upload-file v-model="entity.File" :maxCount="5"></c-upload-file>
      </a-form-item>
      <a-form-item label="获取图片链接" :labelCol="labelCol" :wrapperCol="wrapperCol">
        <a-button @click="setFile()">设置文件链接</a-button>
        <a-button @click="getFileUrls()">获取文件链接</a-button>
      </a-form-item>
    </a-form>
  </a-card>
</template>

<script>
import CUploadFile from '@/components/CUploadFile/CUploadFile'

export default {
  components: {
    CUploadFile,
  },
  data() {
    return {
      form: this.$form.createForm(this),
      labelCol: { xs: { span: 24 }, sm: { span: 7 } },
      wrapperCol: { xs: { span: 24 }, sm: { span: 13 } },
      entity: { File: [require('@/assets/logo.png')] },
    }
  },
  methods: {
    getFileUrls() {
      console.log(this.entity)
    },
    setFile() {
      this.entity = { File: require('@/assets/logo.png') }
    },
  },
}
</script>
