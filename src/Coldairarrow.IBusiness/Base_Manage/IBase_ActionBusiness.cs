using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Coldairarrow.Entity.Base_Manage.EnumType;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_ActionBusiness
    {
        Task<List<Base_Action>> GetDataListAsync(Pagination pagination, string keyword = null, string parentId = null, List<int> types = null, IQueryable<Base_Action> q = null);
        Task<List<Base_ActionDTO>> GetTreeDataListAsync(string keyword, List<int> types, bool selectable, IQueryable<Base_Action> q = null, bool checkEmptyChildren = false);
        Task<Base_Action> GetTheDataAsync(string id);
        Task AddDataAsync(Base_Action newData, List<Base_Action> permissionList);
        Task UpdateDataAsync(Base_Action theData, List<Base_Action> permissionList);
        Task DeleteDataAsync(List<string> ids);
    }

    public class Base_ActionDTO : TreeModel
    {
        /// <summary>
        /// 类型,菜单=0,页面=1,权限=2
        /// </summary>
        public Int32 Type { get; set; }

        /// <summary>
        /// 菜单地址
        /// </summary>
        public String Url { get; set; }

        public string path { get => Url; }

        /// <summary>
        /// 是否需要权限(仅页面有效)
        /// </summary>
        public bool NeedAction { get; set; }

        public string TypeText { get => ((ActionType)Type).ToString(); }
        public string NeedActionText { get => NeedAction ? "是" : "否"; }
        public object children { get => Children; }

        public string title { get => Text; }
        public string value { get => Id; }
        public string key { get => Id; }
        public bool selectable { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [JsonIgnore]
        public string Icon { get; set; }

        public string icon { get => Icon; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        public List<string> PermissionValues { get; set; }
    }

}