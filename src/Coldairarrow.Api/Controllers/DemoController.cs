using Microsoft.AspNetCore.Mvc;

namespace Coldairarrow.Web.Controllers
{
    public class DemoController : BaseController
    {
        #region 视图

        public ActionResult UMEditor()
        {
            return View();
        }

        public ActionResult TreeGrid()
        {
            return View();
        }

        public ActionResult ZTree()
        {
            return View();
        }

        public ActionResult ZTreeSelect()
        {
            return View();
        }

        public ActionResult UploadFile()
        {
            return View();
        }

        public ActionResult DownloadFile()
        {
            return View();
        }

        public ActionResult SelectSearch()
        {
            return View();
        }

        public ActionResult BootstrapPwdBox()
        {
            return View();
        }

        public ActionResult ApiSignDemo()
        {
            return View();
        }

        public IActionResult Tab()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }

        #endregion

        #region 接口

        [CheckSign]
        public IActionResult ApiSign(string userId)
        {
            return Success(userId);
        }

        #endregion
    }
}