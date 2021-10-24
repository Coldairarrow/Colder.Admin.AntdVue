/* eslint-disable no-throw-literal */
<template>
  <div class="clearfix">
    <a-upload
      :action="`${$rootUrl}/Base_Manage/Upload/UploadFileByForm`"
      listType="picture-card"
      :headers="headers"
      :fileList="fileList"
      @preview="handlePreview"
      @change="handleChange"
      accept="image/*"
      :multiple="this.multiple()"
      :before-upload="beforeUpload"
    >
      <div v-if="fileList.length < maxCount">
        <a-icon type="plus" />
        <div class="ant-upload-text">选择</div>
      </div>
    </a-upload>
    <a-modal :visible="previewVisible" :footer="null" @cancel="handleCancel">
      <img alt="example" style="width: 100%" :src="previewImage" />
    </a-modal>
  </div>
</template>
<script>
import TypeHelper from '@/utils/helper/TypeHelper'
import TokenCache from '@/utils/cache/TokenCache'

import defaultSettings from '@/config/defaultSettings'
const uuid = require('uuid')

export default {
  props: {
    maxCount: {
      type: Number,
      default: 1
    },
    value: undefined // 字符串或字符串数组
  },
  mounted () {
    // cbc:修改前端错误
    // if (this.maxCount == 1) {
    //   this.value = this.value || ''
    // } else {
    //   this.value = this.value || []
    // }

    // console.log('  mounted  this.value:', this.value)

    this.value = this.value

    this.checkType(this.value)

    this.refresh()
  },
  data () {
    return {
      previewVisible: false,
      previewImage: '',
      fileList: [],
      internelValue: {},
      headers: { Authorization: 'Bearer ' + TokenCache.getToken() }
    }
  },
  watch: {
    value (val) {
      // console.log('  watch  val:', val)

      if (!this.value) {
        this.previewImage = ''
        this.fileList = []
        this.previewVisible = false
        this.internelValue = {}
      }

      // 内部触发事件不处理,仅回传数据
      // eslint-disable-next-line eqeqeq
      if (val == this.internelValue) {
        return
      }

      // console.log('  watch  this.value:', this.value)

      this.checkType(val)
      this.value = val
      this.refresh()
    }
  },
  methods: {
    multiple () {
      return this.maxCount > 1
    },
    checkType (val) {
      // eslint-disable-next-line eqeqeq
      if (this.maxCount == 1 && TypeHelper.isArray(val)) {
        // eslint-disable-next-line no-throw-literal
        throw 'maxCount=1时model不能为Array'
      }
      if (this.maxCount > 1 && !TypeHelper.isArray(val)) {
        // eslint-disable-next-line no-throw-literal
        throw 'maxCount>1时model必须为Array<String>'
      }
    },

    beforeUpload (file) {
      // const isJpgOrPng = file.type === 'image/jpeg' || file.type === 'image/png'
      // if (!isJpgOrPng) {
      //   this.$message.error('You can only upload JPG file!')
      // }
      // const isLt2M = file.size / 1024 / 1024 < 2
      // 限制200KB
      const isLt2M = file.size / 1024 < 300
      if (!isLt2M) {
        this.$message.error('图片大小不能超过300KB!')
      }
      // return isJpgOrPng && isLt2M
      return isLt2M
    },

    refresh () {
      if (this.maxCount < 1) {
        // eslint-disable-next-line no-throw-literal
        throw 'maxCount必须>=1'
      }
      if (this.value) {
        // console.log('  refresh  this.value:', this.value)

        const urls = []
        if (TypeHelper.isString(this.value)) {
          // 拼接图片全路径，框架中没有
          urls.push(defaultSettings.publishRootUrl + this.value)
        } else if (TypeHelper.isArray(this.value)) {
          urls.push(...this.value)
        } else {
          // eslint-disable-next-line no-throw-literal
          throw 'value必须为字符串或数组'
        }

        this.fileList = urls.map((x) => {
          return { name: x, uid: uuid.v4(), status: 'done', url: x }
        })
      }
    },
    handleCancel () {
      this.previewVisible = false
    },
    handlePreview (file) {
      this.previewImage = file.url || file.thumbUrl
      this.previewVisible = true
    },
    handleChange ({ file, fileList }) {
      this.fileList = fileList

      // eslint-disable-next-line eqeqeq
      if (file.status == 'done' || file.status == 'removed') {
        // eslint-disable-next-line eqeqeq
        var urls = this.fileList.filter((x) => x.status == 'done').map((x) => x.url || x.response.url)
        // eslint-disable-next-line eqeqeq
        var newValue = this.maxCount == 1 ? urls[0] : urls
        this.internelValue = newValue
        // 双向绑定
        this.$emit('input', newValue)
      }
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
