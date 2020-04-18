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
        <a-form-model-item label="用户名" prop="UserName">
          <a-input v-model="entity.UserName" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="密码" prop="newPwd">
          <a-input v-model="entity.newPwd" type="password" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="姓名" prop="RealName">
          <a-input v-model="entity.RealName" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="性别" prop="Sex">
          <a-radio-group v-model="entity.Sex">
            <a-radio :value="0">女</a-radio>
            <a-radio :value="1">男</a-radio>
          </a-radio-group>
        </a-form-model-item>
        <a-form-model-item label="生日" prop="Birthday">
          <a-date-picker v-model="entity.Birthday" format="YYYY-MM-DD" />
        </a-form-model-item>
        <a-form-model-item label="部门" prop="DepartmentId">
          <a-tree-select
            v-model="entity.DepartmentId"
            allowClear
            :treeData="DepartmentIdTreeData"
            placeholder="请选择部门"
            treeDefaultExpandAll
          ></a-tree-select>
        </a-form-model-item>
        <a-form-model-item label="角色" prop="RoleIdList">
          <a-select v-model="entity.RoleIdList" allowClear mode="multiple">
            <a-select-option v-for="item in RoleOptionList" :key="item.Id">{{ item.RoleName }}</a-select-option>
          </a-select>
        </a-form-model-item>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
import moment from 'moment'
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
      DepartmentIdTreeData: [],
      RoleOptionList: [],
      rules: {
        UserName: [{ required: true, message: '必填' }],
        RealName: [{ required: true, message: '必填' }],
        Sex: [{ required: true, message: '必填' }]
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
      this.$http.post('/Base_Manage/Base_Department/GetTreeDataList', {}).then(resJson => {
        if (resJson.Success) {
          this.DepartmentIdTreeData = resJson.Data
        }
      })
      this.$http.post('/Base_Manage/Base_Role/GetDataList', {}).then(resJson => {
        if (resJson.Success) {
          this.RoleOptionList = resJson.Data
        }
      })
    },
    openForm(id) {
      this.init()

      if (id) {
        this.$http.post('/Base_Manage/Base_User/GetTheData', { id: id }).then(resJson => {
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
        this.$http.post('/Base_Manage/Base_User/SaveData', this.entity).then(resJson => {
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
