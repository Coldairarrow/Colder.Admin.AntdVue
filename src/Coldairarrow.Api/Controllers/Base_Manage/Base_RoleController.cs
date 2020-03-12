using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    /// <summary>
    /// 应用密钥
    /// </summary>
    /// <seealso cref="Coldairarrow.Api.BaseApiController" />
    [Route("/Base_Manage/[controller]/[action]")]
    public class Base_RoleController : BaseApiController
    {
        #region DI

        public Base_RoleController(IBase_RoleBusiness roleBus)
        {
            _roleBus = roleBus;
        }

        IBase_RoleBusiness _roleBus { get; }

        #endregion

        #region 获取

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="roleName">角色名</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<List<Base_RoleDTO>>> GetDataList(Pagination pagination, string roleName)
        {
            var dataList = await _roleBus.GetDataListAsync(pagination, null, roleName);

            return DataTable(dataList, pagination);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">id主键</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Base_RoleDTO> GetTheData(string id)
        {
            return await _roleBus.GetTheDataAsync(id) ?? new Base_RoleDTO();
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        /// <param name="actionsJson">权限值JSON</param>
        [HttpPost]
        public async Task SaveData(Base_Role theData, string actionsJson)
        {
            var actionList = actionsJson?.ToList<string>();
            if (theData.Id.IsNullOrEmpty())
            {
                theData.InitEntity();

                await _roleBus.AddDataAsync(theData, actionList);
            }
            else
            {
                await _roleBus.UpdateDataAsync(theData, actionList);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ids">id数组,JSON数组</param>
        [HttpPost]
        public async Task DeleteData(string ids)
        {
            await _roleBus.DeleteDataAsync(ids.ToList<string>());
        }

        #endregion
    }
}