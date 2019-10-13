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
        public ActionResult<AjaxResult<List<Base_DbLink>>> GetDataList(Pagination pagination)
        {
            var dataList = _dbLinkBus.GetDataList(pagination);

            return Content(pagination.BuildTableResult_AntdVue(dataList).ToJson());
        }

        [HttpPost]
        public ActionResult<AjaxResult<Base_DbLink>> GetTheData(string id)
        {
            var theData = _dbLinkBus.GetTheData(id) ?? new Base_DbLink();

            return Success(theData);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        [HttpPost]
        public ActionResult<AjaxResult> SaveData(Base_DbLink theData)
        {
            AjaxResult res;
            if (theData.Id.IsNullOrEmpty())
            {
                theData.InitEntity();

                res = _dbLinkBus.AddData(theData);
            }
            else
            {
                res = _dbLinkBus.UpdateData(theData);
            }

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ids">id数组,JSON数组</param>
        [HttpPost]
        public ActionResult<AjaxResult> DeleteData(string ids)
        {
            var res = _dbLinkBus.DeleteData(ids.ToList<string>());

            return JsonContent(res.ToJson());
        }

        #endregion
    }
}