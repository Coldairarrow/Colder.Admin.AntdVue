using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;

namespace Coldairarrow.Api
{
    /// <summary>
    /// 基控制器
    /// </summary>
    [JsonParamter]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 返回JSON
        /// </summary>
        /// <param name="jsonStr">json字符串</param>
        /// <returns></returns>
        protected ContentResult JsonContent(string jsonStr)
        {
            return base.Content(jsonStr, "application/json", Encoding.UTF8);
        }

        /// <summary>
        /// 返回html
        /// </summary>
        /// <param name="body">html内容</param>
        /// <returns></returns>
        protected ContentResult HtmlContent(string body)
        {
            return base.Content(body);
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <returns></returns>
        protected ContentResult Success()
        {
            AjaxResult res = new AjaxResult
            {
                Success = true,
                Msg = "请求成功！",
            };

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        protected ContentResult Success(string msg)
        {
            AjaxResult res = new AjaxResult
            {
                Success = true,
                Msg = msg,
            };

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="data">返回的数据</param>
        /// <returns></returns>
        protected ContentResult Success<T>(T data)
        {
            AjaxResult<T> res = new AjaxResult<T>
            {
                Success = true,
                Msg = "请求成功！",
                Data = data
            };

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="data">返回的数据</param>
        /// <param name="msg">返回的消息</param>
        /// <returns></returns>
        protected ContentResult Success<T>(T data, string msg)
        {
            AjaxResult<T> res = new AjaxResult<T>
            {
                Success = true,
                Msg = msg,
                Data = data
            };

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// 返回错误
        /// </summary>
        /// <returns></returns>
        protected ContentResult Error()
        {
            AjaxResult res = new AjaxResult
            {
                Success = false,
                Msg = "请求失败！",
            };

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// 返回错误
        /// </summary>
        /// <param name="msg">错误提示</param>
        /// <returns></returns>
        protected ContentResult Error(string msg)
        {
            AjaxResult res = new AjaxResult
            {
                Success = false,
                Msg = msg,
            };

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// 返回表格数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">数据列表</param>
        /// <returns></returns>
        protected ContentResult DataTable<T>(List<T> list)
        {
            return DataTable(list, new Pagination());
        }

        /// <summary>
        /// 返回表格数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">数据列表</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        protected ContentResult DataTable<T>(List<T> list, Pagination pagination)
        {
            return JsonContent(pagination.BuildTableResult_AntdVue(list).ToJson());
        }
    }
}