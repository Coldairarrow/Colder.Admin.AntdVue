using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using SafeObjectPool;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Linq;

namespace Coldairarrow.Api.Controllers
{
    [Route("/[controller]/[action]")]
    public class TestController : BaseController
    {
        static ObjectPool<DefaultDbContext> _pool = new ObjectPool<DefaultDbContext>(500,
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
                //db.Value.Database.ExecuteSqlCommandAsync
                db.Value.Base_Logs.Add(GetNewLog());
                await db.Value.SaveChangesAsync();
            }

            //_db.Base_Logs.Add(GetNewLog());
            //await _db.SaveChangesAsync();

            return Success();
        }

        [HttpGet]
        public async Task<IActionResult> WriteLogTest_EFNoPool()
        {
            using (var db = new DefaultDbContext())
            {
                //db.Value.Database.ExecuteSqlCommandAsync
                db.Base_Logs.Add(GetNewLog());
                await db.SaveChangesAsync();
            }

            //_db.Base_Logs.Add(GetNewLog());
            //await _db.SaveChangesAsync();

            return Success();
        }

        [HttpGet]
        public IActionResult WriteLogTest_EFSync()
        {
            using (var db = _pool.Get())
            {
                //db.Value.Database.ExecuteSqlCommandAsync
                db.Value.Base_Logs.Add(GetNewLog());
                db.Value.SaveChanges();
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

        [HttpGet]
        public async Task<IActionResult> StockTest()
        {
            string prizeId = "1192732318343761920";
            string sql = "UPDATE \"Bus_PrizePool\" SET \"UsedStock\"=\"UsedStock\"+1 WHERE \"Id\"=@prizeId AND \"Stock\"-\"UsedStock\">1";

            //var db = new DefaultDbContext();
            //db.Database.ExecuteSqlCommandAsync(sql, new NpgsqlParameter("@prizeId", prizeId)).ContinueWith(next =>
            //{
            //    if (next.Exception != null)
            //        Console.WriteLine(next.Exception.Message);
            //    db.Dispose();
            //    Console.WriteLine("释放成功");
            //});

            using (var db = await _pool.GetAsync())
            {
                await db.Value.Database.ExecuteSqlCommandAsync(sql, new NpgsqlParameter("@prizeId", prizeId));
            }

            return Success();
        }

        [HttpGet]
        public async Task<IActionResult> SearchStockTestAsync()
        {
            //string prizeId = "1192732318343761920";

            //var db = new DefaultDbContext();
            //var data = await db.Bus_PrizePools.Where(x => x.Id == prizeId).FirstOrDefaultAsync();

            string id = "global";

            var db = new DefaultDbContext();
            var data = await db.Bus_Configs.Where(x => x.Id == id).FirstOrDefaultAsync();

            return Success();
        }
    }
}