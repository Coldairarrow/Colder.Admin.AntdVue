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
        public List<Base_DbLink> GetAllDbLink()
        {
            return _buildCodeBus.GetAllDbLink();
        }

        [HttpPost]
        public List<DbTableInfo> GetDbTableList(string linkId)
        {
            return _buildCodeBus.GetDbTableList(linkId);
        }

        [HttpPost]
        public void Build(string linkId, string areaName, string tablesJson, string buildTypesJson)
        {
            _buildCodeBus.Build(linkId, areaName, tablesJson?.ToList<string>(), buildTypesJson?.ToList<int>());
        }
    }
}