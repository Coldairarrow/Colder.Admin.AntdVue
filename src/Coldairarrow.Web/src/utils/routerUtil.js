import { Axios } from '@/utils/plugin/axios-plugin'
// eslint-disable-next-line
import { BasicLayout, PageView } from '@/layouts'
var uuid = require('node-uuid');

const desktopPath = '/Base_Manage/Base_AppSecret/List'

// 前端未找到页面路由（固定不用改）
const notFoundRouter = {
  path: '*', redirect: '/404', hidden: true
}

/**
 * 获取后端路由信息的 axios API
 * @returns {Promise}
 */
export const getRouterByUser = () => {
  // return Axios.post('/Base_Manage/Base_Action/GetMenuTreeList')
  return new Promise((resolve, reject) => {
    Axios.post('/Base_Manage/Home/GetOperatorMenuList', {}).then(resJson => {
      if (resJson.Success) {
        resolve(resJson.Data)
      }
    })
  })

  // let menus = [
  //   {
  //     "title": "系统管理",
  //     "icon": "setting",
  //     "children": [
  //       {
  //         "title": "用户管理",
  //         "path": '/Base_Manage/Base_User/List'
  //       },
  //       {
  //         "title": "角色管理",
  //         "path": '/Base_Manage/Base_Role/List'
  //       },
  //       {
  //         "title": "部门管理",
  //         "path": '/Base_Manage/Base_Department/List'
  //       },
  //       {
  //         "title": "密钥管理",
  //         "path": '/Base_Manage/Base_AppSecret/List'
  //       },
  //       {
  //         "title": "权限管理",
  //         "path": '/Base_Manage/Base_Action/List'
  //       },
  //       {
  //         "title": "系统日志",
  //         "path": '/Base_Manage/Base_Log/List'
  //       }
  //     ]
  //   },
  //   {
  //     "title": "开发",
  //     "icon": "code",
  //     "children": [
  //       {
  //         "title": "图标选择",
  //         "path": '/Develop/IconSelectorView'
  //       }
  //     ]
  //   }
  // ]
  // return new Promise((resolve, reject) => {
  //   resolve(menus)
  // })
}

/**
 * 获取路由菜单信息
 *
 * 1. 调用 getRouterByUser() 访问后端接口获得路由结构数组
 *    @see https://github.com/sendya/ant-design-pro-vue/blob/feature/dynamic-menu/public/dynamic-menu.json
 * 2. 调用
 * @returns {Promise<any>}
 */
export const generatorDynamicRouter = () => {
  return new Promise((resolve, reject) => {
    // ajax
    getRouterByUser().then(res => {
      // console.log('菜单:', res)
      let allRouters = []

      //首页根路由
      let rootRouter = {
        // 路由地址 动态拼接生成如 /dashboard/workplace
        path: '/',
        redirect: desktopPath,
        // 路由名称，建议唯一
        name: uuid.v4(),
        // 该路由对应页面的 组件
        component: BasicLayout,
        // meta: 页面标题, 菜单图标, 页面权限(供指令权限用，可去掉)
        meta: { title: '首页' },
        children: []
      }
      allRouters.push(rootRouter)
      rootRouter.children = generator(res)
      allRouters.push(notFoundRouter)
      resolve(allRouters)
    }).catch(err => {
      reject(err)
    })
  })
}

/**
 * 格式化 后端 结构信息并递归生成层级路由表
 *
 * @param routerMap
 * @param parent
 * @returns {*}
 */
export const generator = (routerMap, parent) => {
  return routerMap.map(item => {
    let hasChildren = item.children && item.children.length > 0
    let component = {}
    if (hasChildren) {
      component = PageView
    } else if (item.path) {
      component = () => import(`@/views${item.path}`)
    }
    let currentRouter = {
      // 路由名称，建议唯一
      name: uuid.v4(),
      // 该路由对应页面的 组件
      component: component,
      // meta: 页面标题, 菜单图标, 页面权限(供指令权限用，可去掉)
      meta: { title: item.title, icon: item.icon || undefined }
    }

    //有子菜单
    if (hasChildren) {
      currentRouter.path = `/${uuid.v4()}`
    } else if (item.path) {//页面
      currentRouter.path = item.path
      currentRouter.path = currentRouter.path.replace('//', '/')
    }

    // 重定向
    item.redirect && (currentRouter.redirect = item.redirect)
    // 是否有子菜单，并递归处理
    if (hasChildren) {
      // Recursion
      currentRouter.children = generator(item.children, currentRouter)
    }
    return currentRouter
  })
}
