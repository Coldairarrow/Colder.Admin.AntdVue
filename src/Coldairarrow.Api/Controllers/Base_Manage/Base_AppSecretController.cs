using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    /// <summary>
    /// 应用密钥
    /// </summary>
    /// <seealso cref="Coldairarrow.Api.BaseApiController" />
    [Route("Api/Base_Manage/[controller]/[action]")]
    public class Base_AppSecretController : BaseApiController
    {
        #region DI

        public Base_AppSecretController(IBase_AppSecretBusiness appSecretBus)
        {
            _appSecretBus = appSecretBus;
        }

        IBase_AppSecretBusiness _appSecretBus { get; }

        #endregion

        #region 获取

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult<List<Base_AppSecret>>> GetDataList(Pagination pagination, string keyword)
        {
            var dataList = _appSecretBus.GetDataList(pagination, keyword);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        [HttpPost]
        public ActionResult SaveData(Base_AppSecret theData)
        {
            AjaxResult res;
            if (theData.Id.IsNullOrEmpty())
            {
                theData.Id = IdHelper.GetId();

                res = _appSecretBus.AddData(theData);
            }
            else
            {
                res = _appSecretBus.UpdateData(theData);
            }

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ids">id数组,JSON数组</param>
        [HttpPost]
        public ActionResult DeleteData(string ids)
        {
            var res = _appSecretBus.DeleteData(ids.ToList<string>());

            return JsonContent(res.ToJson());
        }

        #endregion
    }
}