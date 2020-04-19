<template>
  <a-modal
    title="编辑表单"
    width="40%"
    :visible="visible"
    :confirmLoading="confirmLoading"
    @ok="handleSubmit"
    @cancel="()=>{this.visible=false}"
  >
    <a-spin :spinning="confirmLoading">
      <a-form-model ref="form" :model="entity" :rules="rules" v-bind="layout">
        <a-form-model-item label="应用Id" prop="AppId">
          <a-input v-model="entity.AppId" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="密钥" prop="AppSecret">
          <a-input v-model="entity.AppSecret" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="应用名" prop="AppName">
          <a-input v-model="entity.AppName" autocomplete="off" />
        </a-form-model-item>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
export default {
  props: {
    afterSubmit: {
      type: Function,
      default: null
    }
  },
  data() {
    return {
      layout: {
        labelCol: { span: 5 },
        wrapperCol: { span: 18 }
      },
      visible: false,
      confirmLoading: false,
      entity: {},
      rules: {
        AppId: [{ required: true, message: '必填' }],
        AppSecret: [{ required: true, message: '必填' }],
        AppName: [{ required: true, message: '必填' }]
      }
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
    openForm(id) {
      this.init()

      if (id) {
        this.$http.post('/Base_Manage/Base_AppSecret/GetTheData', { id: id }).then(resJson => {
          this.entity = resJson.Data
          if (this.entity['Birthday']) {
            this.entity['Birthday'] = moment(this.entity['Birthday'])
          }
        })
      }
    },
    handleSubmit() {
      this.$refs['form'].validate(valid => {
        if (!valid) {
          return
        }
        this.confirmLoading = true
        this.$http.post('/Base_Manage/Base_AppSecret/SaveData', this.entity).then(resJson => {
          this.confirmLoading = false

          if (resJson.Success) {
            this.$message.success('操作成功!')
            this.afterSubmit()
            this.visible = false
          } else {
            this.$message.error(resJson.Msg)
          }
        })
      })
    }
  }
}
</script>
