using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    [Route("/Base_Manage/[controller]/[action]")]
    public class BuildCodeController : BaseApiController
    {
        #region DI

        public BuildCodeController(IBuildCodeBusiness buildCodeBus)
        {
            _buildCodeBus = buildCodeBus;
        }

        IBuildCodeBusiness _buildCodeBus { get; }

        #endregion

        [HttpPost]
        public AjaxResult<List<Base_DbLink>> GetAllDbLink()
        {
            var list = _buildCodeBus.GetAllDbLink();

            return Success(list);
        }

        [HttpPost]
        public AjaxResult<List<DbTableInfo>> GetDbTableList(string linkId)
        {
            var list = _buildCodeBus.GetDbTableList(linkId);

            return DataTable(list);
        }

        [HttpPost]
        public AjaxResult Build(string linkId, string areaName, string tablesJson, string buildTypesJson)
        {
            _buildCodeBus.Build(linkId, areaName, tablesJson?.ToList<string>(), buildTypesJson?.ToList<int>());

            return Success();
        }
    }
}