// ie polyfill
import '@babel/polyfill'

import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store/'
// import { VueAxios } from './utils/request'
import AxiosPlugin from '@/utils/plugin/axios-plugin'

// mock
// import './mock'

import bootstrap from './core/bootstrap'
import './core/use'
import './permission' // permission control
import './utils/filter' // global filter
import operatorPlugin from './utils/plugin/operator-plugin'

Vue.config.productionTip = false

// mount axios Vue.$http and this.$http
// Vue.use(VueAxios)
Vue.use(AxiosPlugin)
Vue.use(operatorPlugin)

new Vue({
  router,
  store,
  created: bootstrap,
  render: h => h(App)
}).$mount('#app')
