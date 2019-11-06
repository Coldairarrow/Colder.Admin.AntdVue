<template>
  <div :class="prefixCls">
    <div ref="editor" class="editor-wrapper"></div>
  </div>
</template>

<script>
import WEditor from 'wangeditor'

export default {
  name: 'WangEditor',
  props: {
    prefixCls: {
      type: String,
      default: 'ant-editor-wang'
    },
    // eslint-disable-next-line
    value: {
      type: String
    }
  },
  data() {
    return {
      editor: null,
      editorContent: null,
      isChange: false //是否为内部触发
    }
  },
  watch: {
    value(val) {
      // 仅处理外部触发,解决光标闪烁问题
      if (!this.isChange) {
        this.editor.txt.html(val)
      }
      this.isChange = false
    }
  },
  mounted() {
    this.initEditor()
  },
  methods: {
    initEditor() {
      this.editor = new WEditor(this.$refs.editor)

      this.editor.customConfig.uploadImgShowBase64 = true // base 64 存储图片
      this.editor.customConfig.uploadImgServer = `${this.$rootUrl}/Base_Manage/Upload/UploadFileByForm` // 配置服务器端地址
      this.editor.customConfig.uploadFileName = 'file' // 后端接受上传文件的参数名
      this.editor.customConfig.uploadImgMaxSize = 10 * 1024 * 1024 // 将图片大小限制为 2M
      this.editor.customConfig.uploadImgMaxLength = 1 // 限制一次最多上传 1 张图片
      this.editor.customConfig.uploadImgTimeout = 3 * 60 * 1000 // 设置超时时间
      this.editor.customConfig.uploadImgHooks = {
        customInsert: function(insertImg, result, editor) {
          insertImg(result.url)
        }
      }
      this.editor.customConfig.onchange = html => {
        this.isChange = true
        this.$emit('input', html)
      }
      this.editor.create()
    }
  }
}
</script>

<style lang="less" scoped>
.ant-editor-wang {
  .editor-wrapper {
    text-align: left;
  }
}
</style>
