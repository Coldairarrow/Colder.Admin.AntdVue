using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers
{
    [Route("/[controller]/[action]")]
    public class TestController : BaseController
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
            using (DefaultDbContext db = new DefaultDbContext())
            {
                var data = await db.Base_Logs.FirstOrDefaultAsync();
            }

            return HtmlContent("1");
        }

        [HttpGet]
        public async Task<IActionResult> SearchTest_2()
        {
            var data = await FreeSqlHelper.FreeSql.Select<Base_Log>().FirstAsync();

            return HtmlContent("1");
        }

        [HttpGet]
        public async Task<IActionResult> WriteTest_1()
        {
            using (DefaultDbContext db = new DefaultDbContext())
            {
                await db.Base_Logs.AddAsync(GetNewLog());
                await db.SaveChangesAsync();
            }

            return HtmlContent("1");
        }

        [HttpGet]
        public async Task<IActionResult> WriteTest_2()
        {
            await FreeSqlHelper.FreeSql.Insert<Base_Log>().AppendData(GetNewLog()).ExecuteAffrowsAsync();

            return HtmlContent("1");
        }
    }
}