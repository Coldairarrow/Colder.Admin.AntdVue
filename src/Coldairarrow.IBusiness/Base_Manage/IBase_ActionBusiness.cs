using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_ActionBusiness
    {
        Task<List<Base_Action>> GetDataListAsync(Base_ActionsInputDTO input);
        Task<List<Base_ActionDTO>> GetTreeDataListAsync(Base_ActionsTreeInputDTO input);
        Task<Base_Action> GetTheDataAsync(string id);
        Task AddDataAsync(ActionEditInputDTO input);
        Task UpdateDataAsync(ActionEditInputDTO input);
        Task DeleteDataAsync(List<string> ids);
    }

    [Map(typeof(Base_Action))]
    public class ActionEditInputDTO : Base_Action
    {
        public List<Base_Action> permissionList { get; set; } = new List<Base_Action>();
    }

    public class Base_ActionsInputDTO
    {
        public string keyword { get; set; }
        public string parentId { get; set; }
        public List<int> types { get; set; }
        public IQueryable<Base_Action> q { get; set; }
    }

    public class Base_ActionsTreeInputDTO
    {
        public string keyword { get; set; }
        public List<int> types { get; set; }
        public bool selectable { get; set; }
        public IQueryable<Base_Action> q { get; set; }
        public bool checkEmptyChildren { get; set; }
    }

    public class Base_ActionDTO : TreeModel
    {
        /// <summary>
        /// 类型,菜单=0,页面=1,权限=2
        /// </summary>
        public Int32 Type { get; set; }
        public String Url { get; set; }
        public string path { get => Url; }
        public bool NeedAction { get; set; }
        public string TypeText { get => ((ActionType)Type).ToString(); }
        public string NeedActionText { get => NeedAction ? "是" : "否"; }
        public object children { get => Children; }
        public string title { get => Text; }
        public string value { get => Id; }
        public string key { get => Id; }
        public bool selectable { get; set; }
        [JsonIgnore]
        public string Icon { get; set; }
        public string icon { get => Icon; }
        public int Sort { get; set; }
        public List<string> PermissionValues { get; set; }
    }
}