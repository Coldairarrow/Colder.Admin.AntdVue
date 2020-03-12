using Coldairarrow.Business.Base_Manage;
using Coldairarrow.DataRepository;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers
{
    [Route("/[controller]/[action]")]
    public class TestController : BaseController
    {
        /// <summary>
        /// 压力测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CheckJWT]
        [ApiPermission("aaa")]
        public async Task<AjaxResult> PressTest1()
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
            //    db.Update(data);
            //    db.GetIQueryable<Base_UnitTest>().FirstOrDefault();
            //    db.Delete(data);
            //}

            return await Task.FromResult(new AjaxResult { Success = true });
        }

        /// <summary>
        /// 压力测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task PressTest2()
        {
            var bus = AutofacHelper.GetScopeService<IBase_UserBusiness>();
            using (var db = DbFactory.GetRepository())
            {
                Base_UnitTest data = new Base_UnitTest
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = Guid.NewGuid().ToString(),
                    Age = 10,
                    UserName = Guid.NewGuid().ToString()
                };
                await db.InsertAsync(data);
                await db.UpdateAsync(data);
                await db.GetIQueryable<Base_UnitTest>().FirstOrDefaultAsync();
                await db.DeleteAsync(data);
            }
        }
    }
}