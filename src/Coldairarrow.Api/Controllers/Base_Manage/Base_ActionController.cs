using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    /// <summary>
    /// 系统权限
    /// </summary>
    /// <seealso cref="Coldairarrow.Api.BaseApiController" />
    [Route("/Base_Manage/[controller]/[action]")]
    public class Base_ActionController : BaseApiController
    {
        #region DI

        public Base_ActionController(IBase_ActionBusiness actionBus)
        {
            _actionBus = actionBus;
        }

        IBase_ActionBusiness _actionBus { get; }

        #endregion

        #region 获取

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">id主键</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Base_Action> GetTheData(string id)
        {
            return (await _actionBus.GetTheDataAsync(id)) ?? new Base_Action();
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="parentId">父级Id</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<Base_Action>> GetPermissionList(string parentId)
        {
            return await _actionBus.GetDataListAsync(new Pagination(), null, parentId, new List<int> { 2 });
        }

        [HttpPost]
        public async Task<List<Base_Action>> GetAllActionList()
        {
            return await _actionBus.GetDataListAsync(new Pagination(), null, null, new List<int> { 0, 1, 2 });
        }

        /// <summary>
        /// 获取菜单树列表
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<Base_ActionDTO>> GetMenuTreeList(string keyword)
        {
            return await _actionBus.GetTreeDataListAsync(keyword, new List<int> { 0, 1 }, true);
        }

        /// <summary>
        /// 获取权限树列表
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<Base_ActionDTO>> GetActionTreeList(string keyword)
        {
            return await _actionBus.GetTreeDataListAsync(keyword, null, false);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(Base_Action theData, string permissionListJson)
        {
            var permissionList = permissionListJson?.ToList<Base_Action>();
            if (theData.Id.IsNullOrEmpty())
            {
                theData.InitEntity();

                await _actionBus.AddDataAsync(theData, permissionList);
            }
            else
            {
                await _actionBus.UpdateDataAsync(theData, permissionList);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ids">id数组,JSON数组</param>
        [HttpPost]
        public async Task DeleteData(string ids)
        {
            await _actionBus.DeleteDataAsync(ids.ToList<string>());
        }

        #endregion
    }
}