using Coldairarrow.Business.Base_Manage;
using Coldairarrow.DataRepository;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;
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
                Id = IdHelper.GetId(),
                CreateTime = DateTime.Now
            };
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
        public IActionResult Test1()
        {
            HttpHelper.GetData("http://www.baidu.com");

            return HtmlContent("1");
        }

        static ServiceProvider ServiceProvider { get; }
            = new ServiceCollection().AddHttpClient().BuildServiceProvider();
        [HttpGet]
        public async Task<IActionResult> Test2()
        {
            var httpClientFactory = ServiceProvider.GetService<IHttpClientFactory>();
            var client = httpClientFactory.CreateClient();
            var response = await client.SendAsync(new HttpRequestMessage(System.Net.Http.HttpMethod.Get, "http://www.baidu.com"));
            var content = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(content);

            return HtmlContent("1");
        }
    }
}