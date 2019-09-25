<template>
  <a-modal
    title="编辑表单"
    width="40%"
    :visible="visible"
    :confirmLoading="confirmLoading"
    @ok="handleSubmit"
    @cancel="handleCancel"
  >
    <a-spin :spinning="confirmLoading">
      <a-form :form="form">
        <a-form-item label="菜单名" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['Name', { rules: [{ required: true, message: '请输入菜单名' }] }]" />
        </a-form-item>
        <a-form-item label="上级菜单" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-tree-select
            style="width: 300px"
            :dropdownStyle="{ maxHeight: '400px', overflow: 'auto' }"
            :treeData="ParentIdTreeData"
            placeholder="请选择上级菜单"
            treeDefaultExpandAll
            v-decorator="['ParentId', { rules: [{ required: false }] }]"
          ></a-tree-select>
        </a-form-item>
        <a-form-item label="类型" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-radio-group name="Type" :defaultValue="0">
            <a-radio :value="0">菜单</a-radio>
            <a-radio :value="1">页面</a-radio>
          </a-radio-group>
        </a-form-item>
        <a-form-item label="路径(格式:/xxx/xxx)" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['Url', { rules: [{ required: true, message: '请输入路径' }] }]" />
        </a-form-item>
        <a-form-item label="是否需权限" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-radio-group name="NeedAction" :defaultValue="false">
            <a-radio :value="false">否</a-radio>
            <a-radio :value="true">是</a-radio>
          </a-radio-group>
        </a-form-item>
      </a-form>
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
      form: this.$form.createForm(this),
      labelCol: { xs: { span: 24 }, sm: { span: 7 } },
      wrapperCol: { xs: { span: 24 }, sm: { span: 13 } },
      visible: false,
      confirmLoading: false,
      formFields: {},
      entity: {},
      ParentIdTreeData: {}
    }
  },
  methods: {
    add() {
      this.entity = {}
      this.visible = true
      this.form.resetFields()
      this.init()
    },
    edit(id) {
      this.visible = true

      this.$nextTick(() => {
        this.formFields = this.form.getFieldsValue()

        this.$http.post('/Base_Manage/Base_Action/GetTheData', { id: id }).then(resJson => {
          this.entity = resJson.Data
          var setData = {}
          Object.keys(this.formFields).forEach(item => {
            setData[item] = this.entity[item]
          })
          this.form.setFieldsValue(setData)

          this.init()
        })
      })
    },
    handleSubmit() {
      this.form.validateFields((errors, values) => {
        //校验成功
        if (!errors) {
          this.entity = Object.assign(this.entity, this.form.getFieldsValue())

          this.confirmLoading = true
          this.$http.post('/Base_Manage/Base_Action/SaveData', this.entity).then(resJson => {
            this.confirmLoading = false

            if (resJson.Success) {
              this.$message.success('操作成功!')
              this.afterSubmit()
              this.visible = false
            } else {
              this.$message.error(resJson.Msg)
            }
          })
        }
      })
    },
    handleCancel() {
      this.visible = false
    },
    init() {
      this.$http.post('/Base_Manage/Base_Action/GetDataList').then(resJson => {
        if (resJson.Success) {
          this.ParentIdTreeData = resJson.Data
        }
      })
    }
  }
}
</script>
