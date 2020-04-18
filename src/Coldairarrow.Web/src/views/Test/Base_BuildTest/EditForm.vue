<template>
  <a-modal
    :title="title"
    width="40%"
    :visible="visible"
    :confirmLoading="loading"
    @ok="handleSubmit"
    @cancel="()=>{this.visible=false}"
  >
    <a-spin :spinning="loading">
      <a-form-model ref="form" :model="entity" :rules="rules" v-bind="layout">
        <a-form-model-item label="列1" prop="Column1">
          <a-input v-model="entity.Column1" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="列2" prop="Column2">
          <a-input v-model="entity.Column2" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="列3" prop="Column3">
          <a-input v-model="entity.Column3" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="列4" prop="Column4">
          <a-input v-model="entity.Column4" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="列5" prop="Column5">
          <a-input v-model="entity.Column5" autocomplete="off" />
        </a-form-model-item>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
export default {
  props: {
    parentObj: Object
  },
  data() {
    return {
      layout: {
        labelCol: { span: 5 },
        wrapperCol: { span: 15 }
      },
      visible: false,
      loading: false,
      entity: {},
      rules: {},
      title: ''
    }
  },
  methods: {
    init() {
      this.visible = true
      this.entity = {}
      this.$nextTick(() => {
        this.$refs['form'].clearValidate()
      })
    },
    openForm(id, title) {
      this.init()

      if (id) {
        this.loading = true
        this.$http.post('/Test/Base_BuildTest/GetTheData', { id: id }).then(resJson => {
          this.loading = false

          this.entity = resJson.Data
        })
      }
    },
    handleSubmit() {
      this.$refs['form'].validate(valid => {
        if (!valid) {
          return
        }
        this.loading = true
        this.$http.post('/Test/Base_BuildTest/SaveData', this.entity).then(resJson => {
          this.loading = false

          if (resJson.Success) {
            this.$message.success('操作成功!')
            this.visible = false

            this.parentObj.getDataList()
          } else {
            this.$message.error(resJson.Msg)
          }
        })
      })
    }
  }
}
</script>
