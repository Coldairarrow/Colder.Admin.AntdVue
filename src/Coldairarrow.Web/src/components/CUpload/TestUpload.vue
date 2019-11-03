<template>
  <div>{{ value }}<a-button @click="change()">修改</a-button></div>
</template>
<script>
export default {
  props: {
    value: {}
  },
  methods: {
    change() {
      this.$emit('input', new Date().getTime().toString())
      //   console.log('当前值', this.value)
    }
  }
}
</script>