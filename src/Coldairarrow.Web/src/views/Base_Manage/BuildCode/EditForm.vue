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
        <a-form-model-item label="生成类型" prop="buildTypes">
          <a-checkbox-group v-model="entity.buildTypes">
            <a-checkbox value="0" name="buildTypes">实体层</a-checkbox>
            <a-checkbox value="1" name="buildTypes">业务层</a-checkbox>
            <a-checkbox value="2" name="buildTypes">接口层</a-checkbox>
            <a-checkbox value="3" name="buildTypes">页面层</a-checkbox>
          </a-checkbox-group>
        </a-form-model-item>
        <a-form-model-item label="生成区域" prop="areaName">
          <a-input v-model="entity.areaName" autocomplete="off" />
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
      entity: {
        buildTypes: []
      },
      rules: {
        buildTypes: [{ required: true, message: '必填' }],
        areaName: [{ required: true, message: '必填' }]
      }
    }
  },
  methods: {
    init() {
      this.visible = true
      this.entity = { buildTypes: ['0', '1', '2', '3'] }
      this.$nextTick(() => {
        this.$refs['form'].clearValidate()
      })
    },
    openForm(tables, linkId) {
      this.init()
      this.entity.tables = tables
      this.entity.linkId = linkId
    },
    handleSubmit() {
      this.$refs['form'].validate(valid => {
        if (!valid) {
          return
        }
        this.confirmLoading = true
        this.$http.post('/Base_Manage/BuildCode/Build', this.entity).then(resJson => {
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
