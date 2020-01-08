import router from '@/router'
import NProgress from 'nprogress' // progress bar
import 'nprogress/nprogress.css' // progress bar style
import { setDocumentTitle, domTitle } from '@/utils/domUtil'
import TokenCache from '@/utils/cache/TokenCache'
import OperatorCache from '@/utils/cache/OperatorCache'
import { initRouter } from '@/utils/routerUtil'
import defaultSettings from '@/config/defaultSettings'

NProgress.configure({ showSpinner: false }) // NProgress Configuration

const whiteList = ['Login'] // no redirect whitelist

router.beforeEach((to, from, next) => {
  NProgress.start() // start progress bar
  to.meta && (typeof to.meta.title !== 'undefined' && setDocumentTitle(`${to.meta.title} - ${domTitle}`))
  // 已授权
  if (TokenCache.getToken()) {
    OperatorCache.init(() => {
      if (to.path === '/Home/Login') {
        next({ path: '/' })
        NProgress.done()
      } else {
        initRouter(to, from, next).then(() => {
          const redirect = decodeURIComponent(from.query.redirect || to.path)
          //桌面特殊处理
          if (to.path == defaultSettings.desktopPath || to.path == '/404') {
            next()
          } else {
            if (to.path === redirect) {
              next()
            } else {
              // 跳转到目的路由
              next({ path: redirect })
            }
          }
        })
      }
    })
  } else {
    if (whiteList.includes(to.name)) {
      // 在免登录白名单，直接进入
      next()
    } else {
      next({ path: '/Home/Login', query: { redirect: to.fullPath } })
      NProgress.done() // if current page is login will not trigger afterEach hook, so manually handle it
    }
  }
})

router.afterEach(() => {
  NProgress.done() // finish progress bar
})
