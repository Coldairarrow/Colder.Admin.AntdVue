<template>
  <a-form :form="form">
    <a-form-item label="图片上传" :labelCol="labelCol" :wrapperCol="wrapperCol">
      <c-upload></c-upload>
    </a-form-item>
  </a-form>
</template>

<script>
import CUpload from '@/components/CUpload/CUpload'

export default {
  components: {
    CUpload
  },
  data() {
    return {
      form: this.$form.createForm(this),
      labelCol: { xs: { span: 24 }, sm: { span: 7 } },
      wrapperCol: { xs: { span: 24 }, sm: { span: 13 } },
      entity: {}
    }
  },
  methods: {}
}
</script>
