<!--需要复杂的请参考:https://preview.pro.loacg.com/dashboard/analysis-->
<template>
  <div class="page-header-index-wide">
    <a-row :gutter="24">
      <a-col :sm="24" :md="12" :xl="6" :style="{ marginBottom: '24px' }">
        <chart-card :loading="loading" title="今日销售额" total="￥10,000">
          <a-tooltip title="指标说明" slot="action">
            <a-icon type="info-circle-o" />
          </a-tooltip>
        </chart-card>
      </a-col>
      <a-col :sm="24" :md="12" :xl="6" :style="{ marginBottom: '24px' }">
        <chart-card :loading="loading" title="昨日销售额" total="￥8,000">
          <a-tooltip title="指标说明" slot="action">
            <a-icon type="info-circle-o" />
          </a-tooltip>
        </chart-card>
      </a-col>
      <a-col :sm="24" :md="12" :xl="6" :style="{ marginBottom: '24px' }">
        <chart-card :loading="loading" title="本月销售额" total="￥300,000">
          <a-tooltip title="指标说明" slot="action">
            <a-icon type="info-circle-o" />
          </a-tooltip>
        </chart-card>
      </a-col>
      <a-col :sm="24" :md="12" :xl="6" :style="{ marginBottom: '24px' }">
        <chart-card :loading="loading" title="本年销售额" total="￥4000,000">
          <a-tooltip title="指标说明" slot="action">
            <a-icon type="info-circle-o" />
          </a-tooltip>
        </chart-card>
      </a-col>
    </a-row>
  </div>
</template>
<script>
import ChartCard from '@/components/ChartCard/ChartCard'
import Trend from '@/components/ChartCard/Trend'

export default {
  components: {
    ChartCard,
    Trend
  },
  data() {
    return {
      loading: false
    }
  }
}
</script>