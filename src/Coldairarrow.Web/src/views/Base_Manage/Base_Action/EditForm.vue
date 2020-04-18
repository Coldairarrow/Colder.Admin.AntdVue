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
        <a-form-model-item label="菜单名" prop="Name">
          <a-input v-model="entity.Name" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="上级菜单" prop="ParentId">
          <a-tree-select
            v-model="entity.ParentId"
            allowClear
            :treeData="ParentIdTreeData"
            placeholder="请选择上级菜单"
            treeDefaultExpandAll
          ></a-tree-select>
        </a-form-model-item>
        <a-form-model-item label="类型" prop="Type">
          <a-radio-group v-model="entity.Type">
            <a-radio :value="0">菜单</a-radio>
            <a-radio :value="1">页面</a-radio>
          </a-radio-group>
        </a-form-model-item>
        <a-form-model-item label="路径" prop="Url">
          <a-input v-model="entity.Url" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="是否需要权限" prop="NeedAction">
          <a-radio-group v-model="entity.NeedAction">
            <a-radio :value="false">否</a-radio>
            <a-radio :value="true">是</a-radio>
          </a-radio-group>
        </a-form-model-item>
        <a-form-model-item label="图标" prop="Icon">
          <a-input v-model="entity.Icon" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="排序" prop="Sort">
          <a-input v-model="entity.Sort" type="number" autocomplete="off" />
        </a-form-model-item>
        <a-card title="页面权限" :bordered="false">
          <Permission-List ref="permissionList" :parentObj="this"></Permission-List>
        </a-card>
      </a-form-model>
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
      layout: {
        labelCol: { span: 5 },
        wrapperCol: { span: 18 }
      },
      visible: false,
      confirmLoading: false,
      entity: {},
      ParentIdTreeData: [],
      rules: {
        Name: [{ required: true, message: '必填' }],
        Type: [{ required: true, message: '必填' }],
        NeedAction: [{ required: true, message: '必填' }]
      }
    }
  },
  methods: {
    init(id) {
      this.visible = true
      this.entity = {}
      this.$nextTick(() => {
        this.$refs.permissionList.init(id)
        this.$refs['form'].clearValidate()
      })

      this.$http.post('/Base_Manage/Base_Action/GetMenuTreeList', {}).then(resJson => {
        if (resJson.Success) {
          this.ParentIdTreeData = resJson.Data
        }
      })
    },
    openForm(id) {
      this.init(id)

      if (id) {
        this.$http.post('/Base_Manage/Base_Action/GetTheData', { id: id }).then(resJson => {
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
        this.entity.permissionList = this.$refs.permissionList.getPermissionList()
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
      })
    }
  }
}
</script>
