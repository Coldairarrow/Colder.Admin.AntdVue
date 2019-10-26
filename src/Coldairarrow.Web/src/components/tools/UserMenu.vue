<template>
  <div class="user-wrapper">
    <div class="content-box">
      <!-- <a href="https://pro.loacg.com/docs/getting-started" target="_blank">
        <span class="action">
          <a-icon type="question-circle-o"></a-icon>
        </span>
      </a> -->
      <!-- <notice-icon class="action" /> -->
      <a-dropdown>
        <span class="action ant-dropdown-link user-dropdown-menu">
          <a-avatar size="small" icon="user" />
          <span>{{ op().UserName }}</span>
        </span>
        <a-menu slot="overlay" class="user-dropdown-menu-wrapper">
          <a-menu-item key="1">
            <a href="javascript:;" @click="handleChangePwd()">
              <a-icon type="lock" />
              <span>修改密码</span>
            </a>
            <change-pwd-form ref="changePwd"></change-pwd-form>
          </a-menu-item>
          <a-menu-divider />
          <a-menu-item key="3">
            <a href="javascript:;" @click="handleLogout()">
              <a-icon type="logout" />
              <span>退出登录</span>
            </a>
          </a-menu-item>
        </a-menu>
      </a-dropdown>
    </div>
  </div>
</template>

<script>
// import NoticeIcon from '@/components/NoticeIcon'
// import { mapActions, mapGetters } from 'vuex'
import OperatorCache from '@/utils/cache/OperatorCache'
import TokenCache from '@/utils/cache/TokenCache'
import ChangePwdForm from './ChangePwdForm'

export default {
  name: 'UserMenu',
  components: {
    // NoticeIcon
    ChangePwdForm
  },
  methods: {
    op() {
      return OperatorCache.info
    },
    // ...mapActions(['Logout']),
    // ...mapGetters(['nickname', 'avatar']),
    handleLogout() {
      const that = this

      this.$confirm({
        title: '提示',
        content: '真的要注销登录吗 ?',
        onOk() {
          TokenCache.deleteToken()
          OperatorCache.clear()
          location.reload()
          // that.$router.push({ path: '/user/login' })
        }
      })
    },
    handleChangePwd() {
      this.$refs.changePwd.open()
    }
  }
}
</script>
