using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using SafeObjectPool;
using System;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers
{
    [Route("/[controller]/[action]")]
    public class TestController : BaseController
    {
        static ObjectPool<DefaultDbContext> _pool = new ObjectPool<DefaultDbContext>(1000,
            () => new DefaultDbContext(),
            obj =>
            {
                if (DateTime.Now.Subtract(obj.LastGetTime).TotalSeconds > 5)
                {
                    // 对象超过5秒未活动，进行操作
                }
            });
        //public TestController(DefaultDbContext db)
        //{
        //    _db = db;
        //}
        DefaultDbContext _db { get; }
        private Base_Log GetNewLog()
        {
            return new Base_Log
            {
                Id = Guid.NewGuid().ToString(),
                CreateTime = DateTime.Now
            };
        }

        [HttpGet]
        public IActionResult Test()
        {
            return HtmlContent("");
        }

        [HttpGet]
        public async Task<IActionResult> WriteLogTest_EFAsync()
        {
            using (var db = await _pool.GetAsync())
            {
                db.Value.Base_Logs.Add(GetNewLog());
                await db.Value.SaveChangesAsync();
            }

            //_db.Base_Logs.Add(GetNewLog());
            //await _db.SaveChangesAsync();

            return Success();
        }

        [HttpGet]
        public async Task<IActionResult> WriteLogTest_FreeSql()
        {
            await FreeSqlHelper.FreeSql.Insert<Base_Log>().AppendData(GetNewLog()).ExecuteAffrowsAsync();

            return Success();
        }
    }
}