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
        <a-form-model-item label="连接名" prop="LinkName">
          <a-input v-model="entity.LinkName" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="连接字符串" prop="ConnectionStr">
          <a-input v-model="entity.ConnectionStr" type="textarea" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="数据库类型" prop="DbType">
          <a-select v-model="entity.DbType">
            <a-select-option key="SqlServer">SqlServer</a-select-option>
            <a-select-option key="MySql">MySql</a-select-option>
            <a-select-option key="Oracle">Oracle</a-select-option>
            <a-select-option key="PostgreSql">PostgreSql</a-select-option>
          </a-select>
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
        LinkName: [{ required: true, message: '必填' }],
        ConnectionStr: [{ required: true, message: '必填' }],
        DbType: [{ required: true, message: '必填' }]
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
        this.$http.post('/Base_Manage/Base_DbLink/GetTheData', { id: id }).then(resJson => {
          this.entity = resJson.Data
        })
      }
    },
    handleSubmit() {
      this.$refs['form'].validate(valid => {
        if (!valid) {
          return
        }
        this.confirmLoading = true
        this.$http.post('/Base_Manage/Base_DbLink/SaveData', this.entity).then(resJson => {
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
