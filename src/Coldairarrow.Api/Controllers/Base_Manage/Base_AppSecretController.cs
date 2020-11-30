using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    /// <summary>
    /// 应用密钥
    /// </summary>
    /// <seealso cref="Coldairarrow.Api.BaseApiController" />
    [Route("/Base_Manage/[controller]/[action]")]
    [OpenApiTag("应用密钥")]
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

        [HttpPost]
        public async Task<PageResult<Base_AppSecret>> GetDataList(PageInput<AppSecretsInputDTO> input)
        {
            return await _appSecretBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<Base_AppSecret> GetTheData(IdInputDTO input)
        {
            return await _appSecretBus.GetTheDataAsync(input.id) ?? new Base_AppSecret();
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        [HttpPost]
        public async Task SaveData(Base_AppSecret theData)
        {
            if (theData.Id.IsNullOrEmpty())
            {
                InitEntity(theData);

                await _appSecretBus.AddDataAsync(theData);
            }
            else
            {
                await _appSecretBus.UpdateDataAsync(theData);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ids">id数组,JSON数组</param>
        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _appSecretBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}