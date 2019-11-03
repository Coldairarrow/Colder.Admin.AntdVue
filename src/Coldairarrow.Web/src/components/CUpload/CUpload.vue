<template>
  <div class="clearfix">
    <a-upload
      :action="`${$rootUrl}/Base_Manage/Upload/UploadFileByForm`"
      listType="picture-card"
      :fileList="fileList"
      @preview="handlePreview"
      @change="handleChange"
    >
      <div v-if="fileList.length < 3">
        <a-icon type="plus" />
        <div class="ant-upload-text">选择</div>
      </div>
    </a-upload>
    <a-modal :visible="previewVisible" :footer="null" @cancel="handleCancel">
      <img alt="example" style="width: 100%" :src="previewImage" />
    </a-modal>
    <!-- <a-button @click="getImgs()">获取图片链接</a-button> -->
  </div>
</template>
<script>
export default {
  props: {
    value: {},
    maxCount: Number
  },
  mounted() {
    if (maxCount < 1) {
      throw 'maxCount必须>=1'
    }
  },
  data() {
    return {
      previewVisible: false,
      previewImage: '',
      fileList: [
        {
          uid: '-1',
          name: 'xxx.png',
          status: 'done',
          url: 'https://zos.alipayobjects.com/rmsportal/jkjgkEfvpUPVyRjUImniVslZfWPnJuuZ.png'
        }
      ],
      obj: {}
    }
  },
  methods: {
    handleCancel() {
      this.previewVisible = false
    },
    handlePreview(file) {
      this.previewImage = file.url || file.thumbUrl
      this.previewVisible = true
    },
    handleChange({ fileList }) {
      this.fileList = fileList
    },
    getImgs() {
      var imgUrls = this.fileList.map(x => x.url || x.response.url)
      console.log(imgUrls)
      console.log('name', this.obj)
    }
  }
}
</script>
<style>
/* you can make up upload button and sample style by using stylesheets */
.ant-upload-select-picture-card i {
  font-size: 32px;
  color: #999;
}

.ant-upload-select-picture-card .ant-upload-text {
  margin-top: 8px;
  color: #666;
}
</style>