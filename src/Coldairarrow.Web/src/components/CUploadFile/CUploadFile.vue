<template>
  <div class="clearfix">
    <a-upload
      :action="`${$rootUrl}/Base_Manage/Upload/UploadFileByForm`"
      :headers="headers"
      listType="picture"
      :fileList="fileList"
      @preview="handlePreview"
      @change="handleChange"
    >
      <div v-if="fileList.length < maxCount">
        <a-button> <a-icon type="plus" />选择 </a-button>
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
const uuid = require('uuid')

export default {
  props: {
    value: '', //字符串或字符串数组
    maxCount: {
      type: Number,
      default: 1,
    },
  },
  mounted() {
    if (this.maxCount == 1) {
      this.value = this.value || ''
    } else {
      this.value = this.value || []
    }
    this.checkType(this.value)

    this.refresh()
  },
  data() {
    return {
      previewVisible: false,
      previewImage: '',
      fileList: [],
      obj: {},
      headers: { Authorization: 'Bearer ' + TokenCache.getToken() },
    }
  },
  watch: {
    value(val) {
      this.checkType(val)

      this.value = val
      this.refresh()
    },
  },
  methods: {
    checkType(val) {
      if (this.maxCount == 1 && TypeHelper.isArray(val)) {
        throw 'maxCount=1时model不能为Array'
      }
      if (this.maxCount > 1 && !TypeHelper.isArray(val)) {
        throw 'maxCount>1时model必须为Array<String>'
      }
    },
    refresh() {
      if (this.maxCount < 1) {
        throw 'maxCount必须>=1'
      }
      if (this.value) {
        let urls = []

        if (TypeHelper.isString(this.value)) {
          urls.push(this.value)
        } else if (TypeHelper.isArray(this.value)) {
          urls.push(...this.value)
        } else {
          throw 'value必须为字符串或数组'
        }

        this.fileList = urls.map((x) => {
          return { name: this.getFileName(x), uid: uuid.v4(), status: 'done', url: x }
        })
      }
    },
    handleCancel() {
      this.previewVisible = false
    },
    handlePreview(file) {
      var url = file.url || file.response.url

      window.open(url, 'tab')
    },
    handleChange({ file, fileList }) {
      this.fileList = fileList

      if (file.status == 'done' || file.status == 'removed') {
        var urls = this.fileList.filter((x) => x.status == 'done').map((x) => x.url || x.response.url)
        var newValue = this.maxCount == 1 ? urls[0] : urls
        this.internelValue = newValue
        //双向绑定
        this.$emit('input', newValue)
      }
    },
    getFileName(url) {
      let reg = /^.*\/(.*?)$/
      let match = reg.test(url)
      if (match) {
        return RegExp.$1
      } else {
        return ''
      }
    },
  },
}
</script>