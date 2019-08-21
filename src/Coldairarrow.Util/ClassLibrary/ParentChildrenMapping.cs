using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 父子数据相互映射类
    /// 注：父子数据都必须唯一,即所有父键唯一,所有子键唯一,唯一的子键能确定对应的父键,唯一的父键能确定其拥有的子键集合,
    /// </summary>
    public class ParentChildrenMapping
    {
        #region 私有成员

        private ConcurrentDictionary<string, string> _cToP { get; } = new ConcurrentDictionary<string, string>();
        private ConcurrentDictionary<string, ConcurrentBag<string>> _pToC { get; } = new ConcurrentDictionary<string, ConcurrentBag<string>>();

        #endregion

        #region 外部接口

        /// <summary>
        /// 添加子键
        /// </summary>
        /// <param name="parentId">所属父键</param>
        /// <param name="childId">子键</param>
        public void AddChild(string parentId,string childId)
        {
            if (string.IsNullOrEmpty(parentId))
                throw new Exception("父键不能为Null或空");
            if(string.IsNullOrEmpty(childId))
                throw new Exception("子键不能为Null或空");

            if (_cToP.ContainsKey(childId))
                throw new Exception("该子键已存在！");

            ConcurrentBag<string> children = null;
            if (!_pToC.ContainsKey(parentId))
            {
                children = new ConcurrentBag<string>();
                _pToC[parentId] = children;
            }
            else
                children = _pToC[parentId];

            children.Add(childId);
            _cToP[childId] = parentId;
        }

        /// <summary>
        /// 删除子键
        /// </summary>
        /// <param name="parentId">所属父键</param>
        /// <param name="childId">子键</param>
        public void RemoveChild(string parentId, string childId)
        {
            if (string.IsNullOrEmpty(parentId))
                throw new Exception("父键不能为Null或空");
            if (string.IsNullOrEmpty(childId))
                throw new Exception("子键不能为Null或空");

            if (!_pToC.ContainsKey(parentId))
                throw new Exception("该父键不存在");
            if (!_cToP.ContainsKey(childId))
                throw new Exception("该子键不存在");

            var children = _pToC[parentId];
            if (children == null)
                throw new Exception("该父键不存在该子键");
            if(!children.TryTake(out childId))
                throw new Exception("该父键不存在该子键");
            _cToP.TryRemove(childId, out string value);
        }

        /// <summary>
        /// 删除父键
        /// 注:会删除该父键以及该父键下面的所有子键
        /// </summary>
        /// <param name="parentId"></param>
        public void RemoveParent(string parentId)
        {
            if (string.IsNullOrEmpty(parentId))
                throw new Exception("父键不能为Null或空");

            if (!_pToC.ContainsKey(parentId))
                throw new Exception("父键不存在");

            _pToC.TryRemove(parentId, out ConcurrentBag<string> children);
            if (children != null)
            {
                var enumerator = children.GetEnumerator();
                do
                {
                    _cToP.TryRemove(enumerator.Current, out string value);
                } while (enumerator.MoveNext());
            }
        }

        /// <summary>
        /// 父键是否存在
        /// </summary>
        /// <param name="parentId">父键</param>
        /// <returns></returns>
        public bool ExistsParent(string parentId)
        {
            if (string.IsNullOrEmpty(parentId))
                throw new Exception("父键不能为Null或空");

            return _pToC.ContainsKey(parentId);
        }

        /// <summary>
        /// 子键是否存在
        /// </summary>
        /// <param name="childId">子键</param>
        /// <returns></returns>
        public bool ExistsChild(string childId)
        {
            if (string.IsNullOrEmpty(childId))
                throw new Exception("子键不能为Null或空");

            return _cToP.ContainsKey(childId);
        }

        /// <summary>
        /// 获取父键拥有的所有子键
        /// </summary>
        /// <param name="parentId">父键</param>
        /// <returns></returns>
        public List<string> GetChildren(string parentId)
        {
            if (string.IsNullOrEmpty(parentId))
                throw new Exception("父键不能为Null或空");
            if (!_pToC.ContainsKey(parentId))
                throw new Exception("父键不存在");

            return _pToC[parentId]?.ToList() ?? new List<string>();
        }
        
        /// <summary>
        /// 获取所有父键
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllParents()
        {
            return _pToC.Keys.ToList();
        }

        /// <summary>
        /// 获取所有子键
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllChildren()
        {
            return _cToP.Keys.ToList();
        }

        /// <summary>
        /// 获取父键
        /// </summary>
        /// <param name="childId">子键</param>
        /// <returns></returns>
        public string GetParent(string childId)
        {
            if (string.IsNullOrEmpty(childId))
                throw new Exception("子键不能为Null或空");

            if (!_cToP.ContainsKey(childId))
                throw new Exception("该子键不存在");

            return _cToP[childId];
        }

        #endregion
    }
}
