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
    public class ValuesTestController : ControllerBase
    {
        // GET api/values        
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<List<Base_UserDTO>> Get()
        {
            return new JsonResult(new List<Base_UserDTO>());
        }

        // GET api/values/5        
        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
