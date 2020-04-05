using Coldairarrow.Util;
using EFCore.Sharding;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Coldairarrow.Api.Controllers
{
    [Route("/[controller]/[action]")]
    public class TestController : BaseController
    {
        readonly IRepository _repository;
        readonly IServiceProvider _serviceProvider;
        public TestController(IRepository repository, IServiceProvider serviceProvider)
        {
            _repository = repository;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 压力测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult> PressTest1()
        {
            var newRepository = _serviceProvider.GetService<IRepository>();
            var newRepository2 = _serviceProvider.CreateScope().ServiceProvider.GetService<IRepository>();
            bool equal = newRepository == newRepository2;
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