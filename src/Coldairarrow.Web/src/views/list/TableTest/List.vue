<template>
  <a-card :bordered="false">
    <div class="table-operator">
      <a-button type="primary" icon="plus" @click="handleEdit()">新建</a-button>
      <a-button type="primary" icon="minus" :disabled="!hasSelected()" :loading="loading">
        删除
      </a-button>
    </div>

    <div class="table-page-search-wrapper">
      <a-form layout="inline">
        <a-row :gutter="48">
          <a-col :md="6" :sm="24">
            <a-form-item label="规则编号">
              <a-input v-model="queryParam.id" placeholder="" />
            </a-form-item>
          </a-col>
          <a-col :md="6" :sm="24">
            <a-form-item label="使用状态">
              <a-select v-model="queryParam.status" required placeholder="请选择" default-value="0">
                <a-select-option value="0">全部</a-select-option>
                <a-select-option value="1">关闭</a-select-option>
                <a-select-option value="2">运行中</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="6" :sm="24">
            <a-form-item label="调用次数">
              <a-input-number v-model="queryParam.callNo" style="width: 100%" />
            </a-form-item>
          </a-col>
          <a-col :md="6" :sm="24">
            <a-form-item label="更新日期">
              <a-date-picker v-model="queryParam.date" style="width: 100%" placeholder="请输入更新日期" />
            </a-form-item>
          </a-col>
          <a-col :md="6" :sm="24">
            <a-form-item label="使用状态">
              <a-select v-model="queryParam.useStatus" placeholder="请选择" default-value="0">
                <a-select-option value="0">全部</a-select-option>
                <a-select-option value="1">关闭</a-select-option>
                <a-select-option value="2">运行中</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="6" :sm="24">
            <a-button type="primary" @click="fetch">查询</a-button>
            <a-button style="margin-left: 8px" @click="() => (queryParam = {})">重置</a-button>
          </a-col>
        </a-row>
      </a-form>
    </div>

    <a-table
      ref="table"
      :columns="columns"
      :rowKey="row => row.id"
      :dataSource="data"
      :pagination="pagination"
      :loading="loading"
      @change="handleTableChange"
      :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }"
    >
    </a-table>

    <edit-form ref="editForm" :afterSubmit="fetch"></edit-form>
  </a-card>
</template>

<script>
import reqwest from 'reqwest'
import { GetTestData } from '@/api/manage'
import EditForm from './EditForm'

const columns = [
  { title: 'Name', dataIndex: 'Name', width: '20%' },
  { title: 'Gender', dataIndex: 'Gender', width: '20%' },
  { title: 'Email', dataIndex: 'Email' }
]

export default {
  components: {
    EditForm
  },
  mounted() {
    this.fetch()
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
      this.fetch({
        results: pagination.pageSize,
        page: pagination.current,
        sortField: sorter.field,
        sortOrder: sorter.order,
        ...filters
      })
    },
    fetch(params) {
      // console.log('获取列表')
      // console.log('queryParam:', this.queryParam)
      this.loading = true
      GetTestData(params).then(resjson => {
        // console.log(resjson)
        const pagination = { ...this.pagination }
        // Read total count from server
        // pagination.total = data.totalCount;
        pagination.total = 200
        this.loading = false
        this.data = resjson.result.data
        this.pagination = pagination
      })

      // reqwest({
      //   url: 'https://randomuser.me/api',
      //   method: 'get',
      //   data: {
      //     results: 10,
      //     ...params
      //   },
      //   type: 'json'
      // }).then(data => {
      //   const pagination = { ...this.pagination }
      //   // Read total count from server
      //   // pagination.total = data.totalCount;
      //   pagination.total = 200
      //   this.loading = false
      //   this.data = data.results
      //   this.pagination = pagination
      // })
    },
    onSelectChange(selectedRowKeys) {
      this.selectedRowKeys = selectedRowKeys
    },
    hasSelected() {
      return this.selectedRowKeys.length > 0
    },
    handleEdit() {
      this.$refs.editForm.add()
    }
  }
}
</script>