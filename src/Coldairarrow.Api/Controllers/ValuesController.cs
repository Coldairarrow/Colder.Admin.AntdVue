using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coldairarrow.Business.Base_SysManage;
using Coldairarrow.Entity.Base_SysManage;
using Microsoft.AspNetCore.Mvc;

namespace Coldairarrow.ApiTest.Controllers
{
    /// <summary>
    /// 获取数据
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<List<Base_UserDTO>> Get(string userName,string pwd)
        {
            return new JsonResult(new List<Base_UserDTO>());
        }
    }
}
