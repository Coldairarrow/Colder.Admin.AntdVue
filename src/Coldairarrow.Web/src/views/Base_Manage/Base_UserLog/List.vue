<template>
  <a-card :bordered="false">
    <!-- <div class="table-operator">
      <a-button type="primary" icon="plus" @click="hanldleAdd()">新建</a-button>
      <a-button
        type="primary"
        icon="minus"
        @click="handleDelete(selectedRowKeys)"
        :disabled="!hasSelected()"
        :loading="loading"
      >
        删除
      </a-button>
    </div>-->

    <div class="table-page-search-wrapper">
      <a-form layout="inline">
        <a-row :gutter="20">
          <a-col :md="5" :sm="24">
            <a-form-item label="内容">
              <a-input v-model="queryParam.logContent" placeholder />
            </a-form-item>
          </a-col>
          <a-col :md="4" :sm="24">
            <a-form-item label="类别">
              <a-select v-model="queryParam.logType" allowClear>
                <a-select-option v-for="item in LogTypeList" :key="item.text">{{ item.text }}</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="4" :sm="24">
            <a-form-item label="操作人">
              <a-input v-model="queryParam.opUserName" placeholder />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="20">
          <a-col :md="10" :sm="24">
            <a-form-item label="时间">
              <a-date-picker v-model="queryParam.startTime" showTime format="YYYY-MM-DD HH:mm:ss" />~
              <a-date-picker v-model="queryParam.endTime" showTime format="YYYY-MM-DD HH:mm:ss" />
            </a-form-item>
          </a-col>
          <a-col :md="4" :sm="24">
            <a-button type="primary" @click="() => {this.pagination.current = 1; this.getDataList()}">查询</a-button>
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
      :bordered="true"
      size="small"
      style="word-break:break-all;"
    >
      <span slot="LogContent" slot-scope="value">
        <template v-for="(item,index) in (value || '').replace(/\r\n/g, '\n').split('\n')">
          {{ item }}
          <br :key="index" />
        </template>
      </span>
    </a-table>
  </a-card>
</template>

<script>
import moment from 'moment'

const columns = [
  { title: '内容', dataIndex: 'LogContent', width: '50%', scopedSlots: { customRender: 'LogContent' } },
  { title: '类别', dataIndex: 'LogType', width: '10%' },
  { title: '操作人', dataIndex: 'CreatorRealName', width: '5%' },
  { title: '时间', dataIndex: 'CreateTime', width: '10%' }
]

export default {
  mounted() {
    this.init()
    this.getDataList()
  },
  data() {
    return {
      data: [],
      pagination: {
        current: 1,
        pageSize: 10,
        showTotal: (total, range) => `总数:${total} 当前:${range[0]}-${range[1]}`
      },
      filters: {},
      sorter: { field: 'Id', order: 'asc' },
      loading: false,
      columns,
      queryParam: {},
      visible: false,
      LogTypeList: []
    }
  },
  methods: {
    handleTableChange(pagination, filters, sorter) {
      this.pagination = { ...pagination }
      this.filters = { ...filters }
      this.sorter = { ...sorter }
      this.getDataList()
    },
    init() {
      this.$http.post('/Base_Manage/Base_UserLog/GetLogTypeList').then(resJson => {
        this.LogTypeList = resJson.Data
      })
    },
    getDataList() {
      this.loading = true
      this.$http
        .post('/Base_Manage/Base_UserLog/GetLogList', {
          PageIndex: this.pagination.current,
          PageRows: this.pagination.pageSize,
          SortField: 'CreateTime',
          SortType: 'desc',
          ...this.filters,
          Search: this.queryParam
        })
        .then(resJson => {
          this.loading = false
          this.data = resJson.Data
          const pagination = { ...this.pagination }
          pagination.total = resJson.Total
          this.pagination = pagination
        })
    }
  }
}
</script>