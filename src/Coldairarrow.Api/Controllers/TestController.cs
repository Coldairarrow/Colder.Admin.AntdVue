using Coldairarrow.Util;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Coldairarrow.Api.Controllers
{
    [Route("/[controller]/[action]")]
    public class TestController : BaseController
    {
        static TestController()
        {
            var projectPath = PathHelper.GetProjectRootpath();
            _solutionPath = Directory.GetParent(projectPath).ToString();
        }
        private static readonly string _solutionPath;

        [HttpGet]
        public IActionResult Test()
        {
            var obj1 = AutofacHelper.GetService<IHostingEnvironment>();
            var obj2 = AutofacHelper.GetService<IHostingEnvironment>();
            bool equal = obj1 == obj2;

            //var projectPath = AutofacHelper.GetService<IHostingEnvironment>().ContentRootPath;
            //string solutionPath = Directory.GetParent(projectPath).ToString();

            return HtmlContent(_solutionPath);
        }
    }
}