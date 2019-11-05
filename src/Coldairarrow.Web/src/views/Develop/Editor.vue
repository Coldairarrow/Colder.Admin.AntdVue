<template>
  <a-card :bordered="false">
    <a-button @click="getContent()">获取内容</a-button>
    <a-button @click="setContent()">清空内容</a-button>
    <wang-editor v-model="content"></wang-editor>
  </a-card>
</template>
<script>
import WangEditor from '@/components/WangEditor/WangEditor'
export default {
  components: {
    WangEditor
  },
  data() {
    return {
      content: ''
    }
  },
  methods: {
    getContent() {
      console.log(this.content)
    },
    setContent() {
      this.content = ''
    }
  }
}
</script>