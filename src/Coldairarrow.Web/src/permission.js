import router from '@/router'

import NProgress from 'nprogress' // progress bar
import 'nprogress/nprogress.css' // progress bar style
import { setDocumentTitle, domTitle } from '@/utils/domUtil'
import TokenCache from '@/utils/cache/TokenCache'
import OperatorCache from '@/utils/cache/OperatorCache'
import { initRouter } from '@/utils/routerUtil'

NProgress.configure({ showSpinner: false }) // NProgress Configuration

const whiteList = ['login', 'register', 'registerResult'] // no redirect whitelist

router.beforeEach((to, from, next) => {
  NProgress.start() // start progress bar
  to.meta && (typeof to.meta.title !== 'undefined' && setDocumentTitle(`${to.meta.title} - ${domTitle}`))
  // 已授权
  if (TokenCache.getToken()) {
    OperatorCache.init(() => {
      if (to.path === '/user/login') {
        next({ path: '/' })
        NProgress.done()
      } else {
        initRouter(to, from, next).then(() => {
          const redirect = decodeURIComponent(from.query.redirect || to.path)
          if (to.path === redirect) {
            next()
          } else {
            // 跳转到目的路由
            next({ path: redirect })
          }
        })
      }
    })
  } else {
    if (whiteList.includes(to.name)) {
      // 在免登录白名单，直接进入
      next()
    } else {
      next({ path: '/user/login', query: { redirect: to.fullPath } })
      NProgress.done() // if current page is login will not trigger afterEach hook, so manually handle it
    }
  }
})

router.afterEach(() => {
  NProgress.done() // finish progress bar
})
