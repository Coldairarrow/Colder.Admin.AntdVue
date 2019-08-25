<template>
  <a-modal
    title="编辑表单"
    width="40%"
    :visible="visible"
    :confirmLoading="confirmLoading"
    @ok="handleSubmit"
    @cancel="handleCancel"
    :destroyOnClose="true"
  >
    <a-spin :spinning="confirmLoading">
      <!-- <a-form :form="form">
        <a-row :gutter="48">
          <a-col :md="12" :sm="24">
            <a-form-item label="任务名">
              <a-input
                placeholder="请输入任务名称"
                v-decorator="['name2', { rules: [{ required: true, message: '请输入任务名称', whitespace: true }] }]"
              />
            </a-form-item>
          </a-col>
        </a-row>
        <a-form-item label="描述" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['desc', { rules: [{ required: true }] }]" />
        </a-form-item>

        <a-form-item :labelCol="labelCol" :wrapperCol="wrapperCol" label="唯一识别码">
          <a-input placeholder="唯一识别码" v-model="mdl.id" />
        </a-form-item>

        <a-form-item :labelCol="labelCol" :wrapperCol="wrapperCol" label="角色名称">
          <a-input placeholder="起一个名字" v-model="mdl.name" />
        </a-form-item>

        <a-form-item :labelCol="labelCol" :wrapperCol="wrapperCol" label="状态" validateStatus="warning">
          <a-select v-model="mdl.status" defaultValue="1">
            <a-select-option value="1">正常</a-select-option>
            <a-select-option value="2">禁用</a-select-option>
          </a-select>
        </a-form-item>

        <a-form-item :labelCol="labelCol" :wrapperCol="wrapperCol" label="描述">
          <a-textarea :rows="5" v-model="mdl.describe" placeholder="..." id="describe" />
        </a-form-item>
      </a-form> -->
      <div class="table-page-search-wrapper">
        <a-form layout="inline" :form="form">
          <a-row :gutter="48">
            <a-col :md="12" :sm="24">
              <a-form-item label="规则编号">
                <a-input
                  placeholder="zxzxz"
                  v-decorator="['username', { rules: [{ required: false, message: 'Please input your name' }] }]"
                />
              </a-form-item>
            </a-col>
            <a-col :md="12" :sm="24">
              <a-form-item label="使用状态">
                <a-select required placeholder="请选择" default-value="0">
                  <a-select-option value="0">全部</a-select-option>
                  <a-select-option value="1">关闭</a-select-option>
                  <a-select-option value="2">运行中</a-select-option>
                </a-select>
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="48">
            <a-col :md="12" :sm="24">
              <a-form-item label="规则编号">
                <a-input placeholder="" />
              </a-form-item>
            </a-col>
            <a-col :md="12" :sm="24">
              <a-form-item label="使用状态">
                <a-select required placeholder="请选择" default-value="0">
                  <a-select-option value="0">全部</a-select-option>
                  <a-select-option value="1">关闭</a-select-option>
                  <a-select-option value="2">运行中</a-select-option>
                </a-select>
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="48">
            <a-col :md="12" :sm="24">
              <a-form-item label="规则编号">
                <a-input placeholder="" />
              </a-form-item>
            </a-col>
            <a-col :md="12" :sm="24">
              <a-form-item label="使用状态">
                <a-select required placeholder="请选择" default-value="0">
                  <a-select-option value="0">全部</a-select-option>
                  <a-select-option value="1">关闭</a-select-option>
                  <a-select-option value="2">运行中</a-select-option>
                </a-select>
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="48">
            <a-col :md="12" :sm="24">
              <a-form-item label="规则编号">
                <a-input placeholder="" />
              </a-form-item>
            </a-col>
            <a-col :md="12" :sm="24">
              <a-form-item label="使用状态">
                <a-select required placeholder="请选择" default-value="0">
                  <a-select-option value="0">全部</a-select-option>
                  <a-select-option value="1">关闭</a-select-option>
                  <a-select-option value="2">运行中</a-select-option>
                </a-select>
              </a-form-item>
            </a-col>
          </a-row>
        </a-form>
      </div>
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
      mdl: {},
      labelCol: {
        xs: { span: 24 },
        sm: { span: 7 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 13 }
      },
      visible: false,
      confirmLoading: false,

      form: this.$form.createForm(this)
    }
  },
  methods: {
    add() {
      this.visible = true
    },
    edit() {
      this.visible = true
    },
    handleSubmit() {
      //   const {
      //     form: { validateFields }
      //   } = this
      //   this.confirmLoading = true
      this.form.validateFields((errors, values) => {
        if (!errors) {
          console.log('values', this.mdl)

          this.afterSubmit()
          this.visible = false
        }

        this.confirmLoading = false
      })
      //   validateFields((errors, values) => {
      //     if (!errors) {
      //       console.log('values', this.mdl)

      //       this.visible = false
      //     }
      //     this.confirmLoading = false
      //   })
    },
    handleCancel() {
      this.visible = false
    }
  }
}
</script>
