<template>
  <a-card :bordered="false">
    <div class="table-operator">
      <a-button type="primary" icon="plus" @click="hanldleAdd()">新建</a-button>
      <a-button type="primary" icon="minus" @click="handleDelete()" :disabled="!hasSelected()" :loading="loading">
        删除
      </a-button>
    </div>

    <div class="table-page-search-wrapper">
      <a-form layout="inline">
        <a-row :gutter="48">
          <a-col :md="6" :sm="24">
            <a-form-item label="关键字">
              <a-input v-model="queryParam.keyword" placeholder="" />
            </a-form-item>
          </a-col>
          <a-col :md="6" :sm="24">
            <a-button type="primary" @click="getDataList">查询</a-button>
            <a-button style="margin-left: 8px" @click="() => (queryParam = {})">重置</a-button>
          </a-col>
        </a-row>
      </a-form>
    </div>

    <a-table
      ref="table"
      :columns="columns"
      :rowKey="row => row.Id"
      :dataSource="data"
      :pagination="pagination"
      :loading="loading"
      @change="handleTableChange"
      :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }"
      :bordered="true"
    >
      <span slot="action" slot-scope="text, record">
        <template>
          <a @click="handleEdit(record)">编辑</a>
          <a-divider type="vertical" />
          <a @click="handleDelete(record)">删除</a>
        </template>
      </span>
    </a-table>

    <edit-form ref="editForm" :afterSubmit="getDataList"></edit-form>
  </a-card>
</template>

<script>
import reqwest from 'reqwest'
import EditForm from './EditForm'

const columns = [
  { title: 'AppId', dataIndex: 'AppId', width: '20%' },
  { title: '密钥', dataIndex: 'AppSecret', width: '20%' },
  { title: '应用名', dataIndex: 'AppName' },
  { title: '操作', dataIndex: 'action', scopedSlots: { customRender: 'action' } }
]

export default {
  components: {
    EditForm
  },
  mounted() {
    this.getDataList()
  },
  data() {
    return {
      data: [],
      pagination: {},
      loading: false,
      columns,
      // 查询参数
      queryParam: {},
      visible: false,
      selectedRowKeys: [] // Check here to configure the default column,
    }
  },
  methods: {
    handleTableChange(pagination, filters, sorter) {
      // console.log(pagination)
      const pager = { ...this.pagination }
      pager.current = pagination.current
      this.pagination = pager
      this.getDataList({
        results: pagination.pageSize,
        page: pagination.current,
        sortField: sorter.field,
        sortOrder: sorter.order,
        ...filters
      })
    },
    getDataList(params) {
      this.loading = true

      reqwest({
        url: 'http://localhost:40000/Api/Base_Manage/Base_AppSecret/GetDataList',
        method: 'post',
        data: {
          results: 10,
          ...params
        },
        type: 'json'
      }).then(resJson => {
        this.loading = false

        const pagination = { ...this.pagination }
        // Read total count from server
        // pagination.total = data.totalCount;
        pagination.total = resJson.Total
        this.data = resJson.Data
        this.pagination = pagination
      })
    },
    onSelectChange(selectedRowKeys) {
      this.selectedRowKeys = selectedRowKeys
    },
    hasSelected() {
      return this.selectedRowKeys.length > 0
    },
    hanldleAdd() {
      this.$refs.editForm.add()
    },
    handleEdit() {
      this.$refs.editForm.edit()
    },
    handleDelete() {
      console.log('删除数据')
    }
  }
}
</script>