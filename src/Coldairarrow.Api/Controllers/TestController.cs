using Coldairarrow.Business.Base_Manage;
using Coldairarrow.DataRepository;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers
{
    [Route("/[controller]/[action]")]
    public class TestController : BaseController
    {
        [HttpGet]
        public IActionResult Test()
        {
            return HtmlContent("");
        }

        /// <summary>
        /// 压力测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PressTest()
        {
            var bus = AutofacHelper.GetScopeService<IBase_UserBusiness>();
            var db = DbFactory.GetRepository();
            Base_UnitTest data = new Base_UnitTest
            {
                Id = Guid.NewGuid().ToString(),
                UserId = Guid.NewGuid().ToString(),
                Age = 10,
                UserName = Guid.NewGuid().ToString()
            };
            db.Insert(data);
            db.Update(data);
            db.GetIQueryable<Base_UnitTest>().FirstOrDefault();
            db.Delete(data);

            return Success("");
        }

        [HttpGet]
        public IActionResult WriteLogTest_EF()
        {
            var db = DbFactory.GetRepository();
            db.Insert(new Base_Log
            {
                Id = IdHelper.GetId(),
                CreateTime = DateTime.Now
            });

            return Success();
        }

        [HttpGet]
        public async Task<IActionResult> WriteLogTest_FreeSql()
        {
            string key = Guid.NewGuid().ToString();

            Task task = null;
            task = Task.Run(async () =>
            {
                try
                {
                    await FreeSqlHelper.FreeSql.Insert<Base_Log>().AppendData(new Base_Log
                    {
                        Id = IdHelper.GetId()
                    }).ExecuteAffrowsAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    CacheHelper.SystemCache.RemoveCache(key);
                }
            });
            CacheHelper.SystemCache.SetCache(key, task);

            return await Task.FromResult(Success());
        }
    }
}