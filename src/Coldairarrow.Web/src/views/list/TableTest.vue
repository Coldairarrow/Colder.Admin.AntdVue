<template>
  <a-card :bordered="false">
    <a-table
      :columns="columns"
      :rowKey="id"
      :dataSource="data"
      :pagination="pagination"
      :loading="loading"
      @change="handleTableChange"
    >
    </a-table>
  </a-card>
</template>

<script>
import reqwest from 'reqwest'
import { GetTestData } from '@/api/manage'

const columns = [
  {
    title: 'Name',
    dataIndex: 'Name',
    sorter: true,
    width: '20%'
  },
  {
    title: 'Gender',
    dataIndex: 'Gender',
    width: '20%'
  },
  {
    title: 'Email',
    dataIndex: 'Email'
  }
]

export default {
  mounted() {
    this.fetch()
  },
  data() {
    return {
      data: [],
      pagination: {},
      loading: false,
      columns
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
    fetch(params = {}) {
      // console.log('params:', params)
      this.loading = true
      GetTestData(params).then(resjson=>{
        console.log(resjson)
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


    }
  }
}
</script>