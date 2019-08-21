using Coldairarrow.Business.Base_SysManage;
using Coldairarrow.DataRepository;
using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Coldairarrow.Web
{
    public class TestController : BaseController
    {
        public TestController(IBase_UserBusiness userBus)
        {
            _userBus = userBus;
        }
        private IBase_UserBusiness _userBus { get; }
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult Test()
        {
            var db = DbFactory.GetRepository();
            Base_UnitTest base_UnitTest = new Base_UnitTest
            {
                Id = IdHelper.GetId(),
                Age = int.MaxValue,
                UserId = IdHelper.GetId(),
                UserName = IdHelper.GetId()
            };
            db.Insert(base_UnitTest);
            db.GetIQueryable<Base_UnitTest>().GetPagination(new Pagination()).ToList();
            db.Update(base_UnitTest);
            db.Delete(base_UnitTest);

            return Success();
        }
    }
}