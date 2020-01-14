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
    public class Base_DbLinkController : BaseApiController
    {
        #region DI

        public Base_DbLinkController(IBase_DbLinkBusiness dbLinkBus)
        {
            _dbLinkBus = dbLinkBus;
        }

        IBase_DbLinkBusiness _dbLinkBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<AjaxResult<List<Base_DbLink>>> GetDataList(Pagination pagination)
        {
            var dataList = await _dbLinkBus.GetDataListAsync(pagination);

            return DataTable(dataList, pagination);
        }

        [HttpPost]
        public async Task<AjaxResult<Base_DbLink>> GetTheData(string id)
        {
            var theData = await _dbLinkBus.GetTheDataAsync(id) ?? new Base_DbLink();

            return Success(theData);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        [HttpPost]
        public async Task<AjaxResult> SaveData(Base_DbLink theData)
        {
            if (theData.Id.IsNullOrEmpty())
            {
                theData.InitEntity();

                await _dbLinkBus.AddDataAsync(theData);
            }
            else
            {
                await _dbLinkBus.UpdateDataAsync(theData);
            }

            return Success();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ids">id数组,JSON数组</param>
        [HttpPost]
        public async Task<AjaxResult> DeleteData(string ids)
        {
            await _dbLinkBus.DeleteDataAsync(ids.ToList<string>());

            return Success();
        }

        #endregion
    }
}