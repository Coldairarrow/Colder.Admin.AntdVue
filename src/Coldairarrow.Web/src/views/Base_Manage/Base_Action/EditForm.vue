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
            allowClear
            :dropdownStyle="{ maxHeight: '400px', overflow: 'auto' }"
            :treeData="ParentIdTreeData"
            placeholder="请选择上级菜单"
            treeDefaultExpandAll
            v-decorator="['ParentId', { rules: [{ required: false }] }]"
          ></a-tree-select>
        </a-form-item>
        <a-form-item label="类型" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-radio-group v-decorator="['Type', { rules: [{ required: false }], initialValue: 0 }]">
            <a-radio :value="0">菜单</a-radio>
            <a-radio :value="1">页面</a-radio>
          </a-radio-group>
        </a-form-item>
        <a-form-item label="路径(页面必须配置)" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['Url', { rules: [{ required: false, message: '请输入路径' }] }]" />
        </a-form-item>
        <a-form-item label="是否需权限" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-radio-group v-decorator="['NeedAction', { rules: [{ required: false }], initialValue: false }]">
            <a-radio :value="false">否</a-radio>
            <a-radio :value="true">是</a-radio>
          </a-radio-group>
        </a-form-item>
        <a-form-item label="图标" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['Icon', { rules: [{ required: false, message: '请输入图标' }] }]" />
        </a-form-item>
        <a-form-item label="排序" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['Sort', { rules: [{ required: false, message: '请输入排序' }] }]" />
        </a-form-item>
        <a-card title="页面权限" :bordered="false">
          <Permission-List ref="permissionList" :parentObj="this"></Permission-List>
        </a-card>
      </a-form>
    </a-spin>
  </a-modal>
</template>

<script>
import PermissionList from './PermissionList'

export default {
  props: {
    afterSubmit: {
      type: Function,
      default: null
    }
  },
  components: {
    PermissionList
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
      ParentIdTreeData: []
    }
  },
  methods: {
    add() {
      this.entity = {}
      this.visible = true
      this.form.resetFields()
      this.init()
      this.$refs.permissionList.init()
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
          this.$refs.permissionList.init(id)
        })
      })
    },
    handleSubmit() {
      this.form.validateFields((errors, values) => {
        //校验成功
        if (!errors) {
          this.entity = Object.assign(this.entity, this.form.getFieldsValue())

          this.confirmLoading = true
          this.entity.permissionListJson = JSON.stringify(this.$refs.permissionList.getPermissionList())
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
      this.$http.post('/Base_Manage/Base_Action/GetMenuTreeList').then(resJson => {
        if (resJson.Success) {
          this.ParentIdTreeData = resJson.Data
        }
      })
    }
  }
}
</script>
