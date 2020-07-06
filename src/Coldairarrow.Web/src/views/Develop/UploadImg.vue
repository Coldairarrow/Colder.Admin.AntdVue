<template>
  <a-card :bordered="false">
    <a-form :form="form">
      <a-form-item label="单张图片上传" :labelCol="labelCol" :wrapperCol="wrapperCol">
        <!--v-model为图片连接地址(可传单个或数组),maxCount为最大上传数:默认为1-->
        <c-upload-img v-model="entity.Img" :maxCount="1"></c-upload-img>
      </a-form-item>
      <a-form-item label="多张图片上传" :labelCol="labelCol" :wrapperCol="wrapperCol">
        <!--v-model为图片连接地址(可传单个或数组),maxCount为最大上传数:默认为1-->
        <c-upload-img v-model="entity.Imgs" :maxCount="100"></c-upload-img>
      </a-form-item>
      <a-form-item label="获取图片链接" :labelCol="labelCol" :wrapperCol="wrapperCol">
        <a-button @click="setImg()">设置图片链接</a-button>
        <a-button @click="getImgUrls()">获取图片链接</a-button>
      </a-form-item>
    </a-form>
  </a-card>
</template>

<script>
import CUploadImg from '@/components/CUploadImg/CUploadImg'

export default {
  components: {
    CUploadImg
  },
  data() {
    return {
      form: this.$form.createForm(this),
      labelCol: { xs: { span: 24 }, sm: { span: 7 } },
      wrapperCol: { xs: { span: 24 }, sm: { span: 13 } },
      entity: {
        Img: require('@/assets/logo.png'),
        Imgs: [require('@/assets/logo.png')]
      }
    }
  },
  methods: {
    getImgUrls() {
      console.log(this.entity)
    },
    setImg() {
      this.entity = {
        Img: require('@/assets/logo.png'),
        Imgs: [require('@/assets/logo.png')]
      }
    }
  }
}
</script>
