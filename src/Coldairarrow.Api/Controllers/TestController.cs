using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers
{
    [Route("/[controller]/[action]")]
    public class TestController : BaseController
    {
        readonly ILogger _logger;
        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 压力测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Transactional]
        public virtual async Task PressTest1()
        {
            _logger.LogInformation("name:{name} body:{body}", "小明", new Base_User().ToJson());
            //var bus = AutofacHelper.GetScopeService<IBase_UserBusiness>();
            //using (var db = DbFactory.GetRepository())
            //{
            //    Base_UnitTest data = new Base_UnitTest
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        UserId = Guid.NewGuid().ToString(),
            //        Age = 10,
            //        UserName = Guid.NewGuid().ToString()
            //    };
            //    await db.InsertAsync(data);
            //    db.Update(data);
            //    db.GetIQueryable<Base_UnitTest>().FirstOrDefault();
            //    db.Delete(data);
            //}

            await Task.CompletedTask;
        }

        /// <summary>
        /// 压力测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task PressTest2()
        {
            //var bus = AutofacHelper.GetScopeService<IBase_UserBusiness>();
            //using (var db = DbFactory.GetRepository())
            //{
            //    Base_UnitTest data = new Base_UnitTest
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        UserId = Guid.NewGuid().ToString(),
            //        Age = 10,
            //        UserName = Guid.NewGuid().ToString()
            //    };
            //    await db.InsertAsync(data);
            //    await db.UpdateAsync(data);
            //    await db.GetIQueryable<Base_UnitTest>().FirstOrDefaultAsync();
            //    await db.DeleteAsync(data);
            //}
            await Task.CompletedTask;
        }
    }
}