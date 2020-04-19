<template>
  <!-- <a-row :gutter="16">
  <a-col :span="16">-->
  <a-card :bordered="false">
    <div class="table-operator">
      <a-button type="primary" icon="plus" @click="hanldleAdd()">新建</a-button>
      <a-button
        type="primary"
        icon="minus"
        @click="handleDelete(selectedRowKeys)"
        :disabled="!hasSelected()"
        :loading="loading"
      >删除</a-button>
      <a-button type="primary" icon="redo" @click="getDataList()">刷新</a-button>
    </div>

    <!-- <div class="table-page-search-wrapper">
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
    </div>-->

    <a-table
      v-if="data && data.length"
      ref="table"
      :columns="columns"
      :rowKey="row => row.Id"
      :dataSource="data"
      :pagination="false"
      :loading="loading"
      @change="handleTableChange"
      :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }"
      :bordered="true"
      :defaultExpandAllRows="true"
      size="small"
    >
      <span slot="action" slot-scope="text, record">
        <template>
          <a @click="handleEdit(record.Id)">编辑</a>
          <a-divider type="vertical" />
          <a @click="handleDelete([record.Id])">删除</a>
          <!-- <template v-if="record.Type == 1">
            <a-divider type="vertical" />
            <a @click="managePermission(record)">权限</a>
          </template>-->
        </template>
      </span>
      <span slot="icon" slot-scope="text, record">
        <template>
          <a-icon v-if="record.icon" :type="record.icon" />
        </template>
      </span>
      <span slot="permissions" slot-scope="text, record">
        <template v-for="(item, index) in record.PermissionValues">
          <br v-if="index != 0" :key="index" />
          {{ item }}
        </template>
      </span>
    </a-table>

    <edit-form ref="editForm" :afterSubmit="getDataList"></edit-form>
  </a-card>
  <!-- </a-col> -->
  <!-- <a-col :span="8">
      <a-card :title="menuName" :bordered="false">
        <Permission-List ref="permissionList" :parentObj="this"></Permission-List>
      </a-card>
  </a-col>-->
  <!-- </a-row> -->
</template>

<script>
import EditForm from './EditForm'
import PermissionList from './PermissionList'
const columns = [
  { title: '菜单名', dataIndex: 'Text', width: '15%' },
  { title: '类型', dataIndex: 'TypeText', width: '5%' },
  { title: '路径', dataIndex: 'Url', width: '25%' },
  { title: '需要权限', dataIndex: 'NeedActionText', width: '10%' },
  { title: '页面权限', dataIndex: 'PermissionValuesText', width: '20%', scopedSlots: { customRender: 'permissions' } },
  { title: '图标', dataIndex: 'icon', width: '5%', scopedSlots: { customRender: 'icon' } },
  { title: '排序', dataIndex: 'Sort', width: '5%' },
  { title: '操作', dataIndex: 'action', scopedSlots: { customRender: 'action' } }
]

export default {
  components: {
    EditForm,
    PermissionList
  },
  mounted() {
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
      visible: false,
      selectedRowKeys: [],
      menuName: ''
    }
  },
  methods: {
    handleTableChange(pagination, filters, sorter) {
      this.pagination = { ...pagination }
      this.filters = { ...filters }
      this.sorter = { ...sorter }
      this.getDataList()
    },
    getDataList() {
      this.selectedRowKeys = []

      this.loading = true
      this.$http
        .post('/Base_Manage/Base_Action/GetMenuTreeList', {
          PageIndex: this.pagination.current,
          PageRows: this.pagination.pageSize,
          SortField: this.sorter.field || 'Id',
          SortType: this.sorter.order == 'ascend' ? 'asc' : 'desc',
          ...this.filters
        })
        .then(resJson => {
          this.loading = false
          this.data = resJson.Data
          const pagination = { ...this.pagination }
          pagination.total = resJson.Total
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
      this.$refs.editForm.openForm()
    },
    handleEdit(id) {
      this.$refs.editForm.openForm(id)
    },
    handleDelete(ids) {
      var thisObj = this
      this.$confirm({
        title: '确认删除吗?',
        onOk() {
          return new Promise((resolve, reject) => {
            thisObj.submitDelete(ids, resolve, reject)
          })
        }
      })
    },
    submitDelete(ids, resolve, reject) {
      this.$http.post('/Base_Manage/Base_Action/DeleteData', ids).then(resJson => {
        resolve()

        if (resJson.Success) {
          this.$message.success('操作成功!')

          this.getDataList()
        } else {
          this.$message.error(resJson.Msg)
        }
      })
    },
    managePermission(row) {
      this.menuName = `【${row.Text}】页面权限`
      this.$nextTick(() => {
        this.$refs.permissionList.setParentId(row.Id)
        this.$refs.permissionList.getDataList()
      })
    }
  }
}
</script>