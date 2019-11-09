// eslint-disable-next-line
import { UserLayout, PageView } from '@/layouts'

/**
 * 基础路由
 * @type { *[] }
 */
export const constantRouterMap = [
  {
    path: '/Home',
    component: UserLayout,
    redirect: '/Home/Login',
    hidden: true,
    children: [
      {
        path: '/Home/Login',
        name: 'Login',
        component: () => import('@/views/Home/Login')
      }
    ]
  },
  {
    path: '/404',
    component: () => import('@/views/exception/404')
  }
]
