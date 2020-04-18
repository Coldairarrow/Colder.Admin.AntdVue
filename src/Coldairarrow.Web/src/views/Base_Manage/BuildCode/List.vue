<template>
  <a-card :bordered="false">
    <div class="table-operator">
      <a-button type="primary" icon="redo" @click="init()">刷新</a-button>
      <a-button
        type="primary"
        icon="plus"
        @click="openForm(selectedRowKeys)"
        :disabled="!hasSelected()"
        :loading="loading"
      >生成代码</a-button>
    </div>

    <div class="table-page-search-wrapper">
      <a-form layout="inline">
        <a-row :gutter="48">
          <a-col :md="6" :sm="24">
            <a-form-item label="选择数据库">
              <a-select v-model="linkId" @change="onLinkChange()">
                <a-select-option v-for="item in dbs" :key="item.Id">{{ item.LinkName }}</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
        </a-row>
      </a-form>
    </div>

    <a-table
      ref="table"
      :columns="columns"
      :rowKey="row => row.TableName"
      :dataSource="data"
      :pagination="false"
      :loading="loading"
      :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }"
      :bordered="true"
      size="small"
    ></a-table>

    <edit-form ref="editForm"></edit-form>
  </a-card>
</template>

<script>
import EditForm from './EditForm'

const columns = [
  { title: '表名', dataIndex: 'TableName', width: '20%' },
  { title: '描述', dataIndex: 'Description', width: '20%' },
  { title: '', dataIndex: 'action' }
]

export default {
  components: {
    EditForm
  },
  mounted() {
    this.init()
  },
  data() {
    return {
      data: [],
      filters: {},
      sorter: { field: 'Id', order: 'asc' },
      loading: false,
      columns,
      queryParam: {},
      visible: false,
      selectedRowKeys: [],
      dbs: [],
      linkId: ''
    }
  },
  methods: {
    init() {
      this.$http
        .post('/Base_Manage/BuildCode/GetAllDbLink', {})
        .then(resJson => {
          this.dbs = resJson.Data
          if (this.dbs && this.dbs.length > 0) {
            this.linkId = this.dbs[0].Id
          }
        })
        .then(() => {
          this.getDataList()
        })
    },
    getDataList() {
      this.selectedRowKeys = []
      this.loading = true
      this.$http
        .post('/Base_Manage/BuildCode/GetDbTableList', {
          linkId: this.linkId
        })
        .then(resJson => {
          this.loading = false
          this.data = resJson.Data
        })
    },
    onLinkChange(value) {
      this.getDataList()
    },
    onSelectChange(selectedRowKeys) {
      this.selectedRowKeys = selectedRowKeys
    },
    hasSelected() {
      return this.selectedRowKeys.length > 0
    },
    openForm() {
      this.$refs.editForm.openForm(this.selectedRowKeys, this.linkId)
    }
  }
}
</script>