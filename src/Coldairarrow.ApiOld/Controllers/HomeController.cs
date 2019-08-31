using Coldairarrow.Business;
using Coldairarrow.Business.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;

namespace Coldairarrow.Web
{
    public class HomeController : BaseMvcController
    {
        public HomeController(IHomeBusiness homeBus, IOperator @operator)
        {
            _homeBus = homeBus;
            _operator = @operator;
        }

        IHomeBusiness _homeBus { get; }
        IOperator _operator { get; }

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        [IgnoreLogin]
        public ActionResult Login()
        {
            if (_operator.Logged())
            {
                string loginUrl = Url.Content("~/");
                return Redirect(loginUrl);
            }

            return View();
        }

        public ActionResult Desktop()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        #endregion

        #region 获取数据

        #endregion

        #region 提交数据

        [IgnoreLogin]
        public ActionResult SubmitLogin(string userName, string password)
        {
            AjaxResult res = _homeBus.SubmitLogin(userName, password);

            return Content(res.ToJson());
        }

        /// <summary>
        /// 注销
        /// </summary>
        public ActionResult Logout()
        {
            _operator.Logout();

            return Success("注销成功！");
        }

        #endregion
    }
}