using Coldairarrow.DataRepository;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers
{
    [Route("/[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private Base_Log GetNewLog()
        {
            return new Base_Log
            {
                Id = Guid.NewGuid().ToString(),
                CreateTime = DateTime.Now
            };
        }

        [HttpGet]
        public async Task<IActionResult> SearchTest_1()
        {
            using (var db = DbFactory.GetRepository(DefaultDbContext.ConString))
            {
                var data = await db.GetIQueryable<Base_Log>().FirstOrDefaultAsync();
            }

            return Content("1");
        }

        [HttpGet]
        public async Task<IActionResult> SearchTest_2()
        {
            var data = await FreeSqlHelper.FreeSql.Select<Base_Log>().FirstAsync();

            return Content("1");
        }

        [HttpGet]
        public async Task<IActionResult> WriteTest_1()
        {
            using (var db = DbFactory.GetRepository(DefaultDbContext.ConString))
            {
                await db.InsertAsync(GetNewLog());
            }

            return Content("1");
        }

        [HttpGet]
        public async Task<IActionResult> WriteTest_2()
        {
            await FreeSqlHelper.FreeSql.Insert<Base_Log>().AppendData(GetNewLog()).ExecuteAffrowsAsync();

            return Content("1");
        }
    }
}