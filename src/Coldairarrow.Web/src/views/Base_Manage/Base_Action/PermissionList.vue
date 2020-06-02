<template>
  <a-spin :spinning="loading">
    <div class="table-operator">
      <a-button class="editable-add-btn" icon="plus" type="default" @click="handleAdd">添加权限</a-button>
      <!-- <a-button class="editable-add-btn" icon="check" type="primary" @click="handleSave">保存</a-button> -->
    </div>
    <a-table :columns="columns" :dataSource="data" bordered size="small" :pagination="false">
      <template v-for="col in ['Name', 'Value']" :slot="col" slot-scope="text, record">
        <div :key="col">
          <a-input
            v-if="record.editable"
            style="margin: -5px 0"
            :value="text"
            @change="e => handleChange(e.target.value, record.key, col)"
          />
          <template v-else>{{ text }}</template>
        </div>
      </template>
      <template slot="operation" slot-scope="text, record">
        <div class="editable-row-operations">
          <span v-if="record.editable">
            <a @click="() => save(record.key)">保存</a>
            <br />
            <a-popconfirm title="确认取消吗?" @confirm="() => cancel(record.key)">
              <a>取消</a>
            </a-popconfirm>
          </span>
          <span v-else>
            <a @click="() => edit(record.key)">编辑</a>
            <a-popconfirm v-if="data.length" title="确认删除吗?" @confirm="() => onDelete(record.key)">
              <a href="javascript:;">删除</a>
            </a-popconfirm>
          </span>
        </div>
      </template>
    </a-table>
  </a-spin>
</template>
<script>
var uuid = require('node-uuid')

const columns = [
  { title: '权限名', dataIndex: 'Name', width: '30%', scopedSlots: { customRender: 'Name' } },
  { title: '权限值(唯一)', dataIndex: 'Value', width: '50%', scopedSlots: { customRender: 'Value' } },
  { title: '操作', dataIndex: 'operation', scopedSlots: { customRender: 'operation' } }
]
export default {
  data() {
    return {
      data: [],
      columns,
      loading: false,
      parentId: null
    }
  },
  methods: {
    handleChange(value, key, column) {
      const newData = [...this.data]
      const target = newData.filter(item => key === item.key)[0]
      if (target) {
        target[column] = value
        this.data = newData
      }
    },
    edit(key) {
      const newData = [...this.data]
      const target = newData.filter(item => key === item.key)[0]
      if (target) {
        target.editable = true
        this.data = newData
      }
    },
    save(key) {
      const newData = [...this.data]
      const target = newData.filter(item => key === item.key)[0]
      if (target) {
        delete target.editable
        this.data = newData
        this.resetCache(newData)
      }
    },
    cancel(key) {
      const newData = [...this.data]
      const target = newData.filter(item => key === item.key)[0]
      if (target) {
        Object.assign(target, this.cacheData.filter(item => key === item.key)[0])
        delete target.editable
        this.data = newData
      }
    },
    onDelete(key) {
      const data = [...this.data]
      this.data = data.filter(item => item.key !== key)
    },
    handleAdd() {
      const newData = {
        key: uuid.v4(),
        Name: '权限名',
        Value: '权限值',
        Type: 2,
        ParentId: this.parentId
      }
      this.data = [...this.data, newData]
    },
    getPermissionList() {
      return this.data
    },
    handleSave() {
      this.loading = true
      this.$http
        .post('/Base_Manage/Base_Action/SavePermission', {
          parentId: this.parentId,
          permissionListJson: JSON.stringify(this.data)
        })
        .then(resJson => {
          this.loading = false
          if (resJson.Success) {
            this.$message.success('权限设置成功')
            this.getDataList()
          } else {
            this.$message.error('操作失败')
          }
        })
    },
    resetCache(dataSource) {
        this.cacheData = dataSource.map(item => ({ ...item }))
    },
    getDataList() {
      this.loading = true
      this.$http
        .post('/Base_Manage/Base_Action/GetPermissionList', {
          parentId: this.parentId
        })
        .then(resJson => {
          this.loading = false
          resJson.Data.forEach(x => (x['key'] = uuid.v4()))
          this.data = resJson.Data
          this.resetCache(this.data)
        })
    },
    init(parentId) {
      this.parentId = parentId
      this.data = []
      if (this.parentId) {
        this.getDataList()
      }
    }
  }
}
</script>
<style scoped>
.editable-row-operations a {
  margin-right: 8px;
}
.editable-add-btn {
  margin-bottom: 8px;
}
</style>
