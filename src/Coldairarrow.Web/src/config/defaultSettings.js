/**
 * 项目默认配置项
 * primaryColor - 默认主题色, 如果修改颜色不生效，请清理 localStorage
 * navTheme - sidebar theme ['dark', 'light'] 两种主题
 * colorWeak - 色盲模式
 * layout - 整体布局方式 ['sidemenu', 'topmenu'] 两种布局
 * fixedHeader - 固定 Header : boolean
 * fixSiderbar - 固定左侧菜单栏 ： boolean
 * autoHideHeader - 向下滚动时，隐藏 Header : boolean
 * contentWidth - 内容区布局： 流式 |  固定
 *
 * storageOptions: {} - Vue-ls 插件配置项 (localStorage/sessionStorage)
 *
 */

export default {
  primaryColor: '#1890FF', // primary color of ant design
  navTheme: 'dark', // theme for nav menu
  layout: 'sidemenu', // nav menu position: sidemenu or topmenu
  contentWidth: 'Fixed', // layout of content: Fluid or Fixed, only works when layout is topmenu
  fixedHeader: false, // sticky header
  fixSiderbar: true, // sticky siderbar
  autoHideHeader: false, //  auto hide header
  colorWeak: false,
  multiTab: true,
  production: process.env.NODE_ENV === 'production' && process.env.VUE_APP_PREVIEW !== 'true',
  // vue-ls options
  storageOptions: {
    namespace: 'pro__',
    name: 'ls',
    storage: 'local'
  },
  desktopPath: '/Base_Manage/Base_User/List',//首页路径
  publishRootUrl: 'http://localhost:40000',//发布后接口根地址
  localRootUrl: 'http://localhost:40000',//本地调试接口根地址
  apiTimeout: 10000,//接口超时时间ms
  devPort: 9000//本地开发启动端口
}