using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 一致性HASH,解决传统HASH的扩容难的问题
    /// 注:常用与分布式缓存与分表
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public class ConsistentHash<T>
    {
        SortedDictionary<int, T> circle { get; set; } = new SortedDictionary<int, T>();
        int _replicate = 100;    //default _replicate count
        int[] ayKeys = null;    //cache the ordered keys for better performance

        //it's better you override the GetHashCode() of T.
        //we will use GetHashCode() to identify different node.
        public void Init(IEnumerable<T> nodes)
        {
            Init(nodes, _replicate);
        }

        public void Init(IEnumerable<T> nodes, int replicate)
        {
            _replicate = replicate;

            foreach (T node in nodes)
            {
                this.Add(node, false);
            }
            ayKeys = circle.Keys.ToArray();
        }

        public void Add(T node)
        {
            Add(node, true);
        }

        private void Add(T node, bool updateKeyArray)
        {
            for (int i = 0; i < _replicate; i++)
            {
                int hash = BetterHash(node.GetHashCode().ToString() + i);
                circle[hash] = node;
            }

            if (updateKeyArray)
            {
                ayKeys = circle.Keys.ToArray();
            }
        }

        public void Remove(T node)
        {
            for (int i = 0; i < _replicate; i++)
            {
                int hash = BetterHash(node.GetHashCode().ToString() + i);
                if (!circle.Remove(hash))
                {
                    throw new Exception("can not remove a node that not added");
                }
            }
            ayKeys = circle.Keys.ToArray();
        }

        //we keep this function just for performance compare
        private T GetNode_slow(String key)
        {
            int hash = BetterHash(key);
            if (circle.ContainsKey(hash))
            {
                return circle[hash];
            }

            int first = circle.Keys.FirstOrDefault(h => h >= hash);
            if (first == new int())
            {
                first = ayKeys[0];
            }
            T node = circle[first];
            return node;
        }

        //return the index of first item that >= val.
        //if not exist, return 0;
        //ay should be ordered array.
        int First_ge(int[] ay, int val)
        {
            int begin = 0;
            int end = ay.Length - 1;

            if (ay[end] < val || ay[0] > val)
            {
                return 0;
            }

            int mid = begin;
            while (end - begin > 1)
            {
                mid = (end + begin) / 2;
                if (ay[mid] >= val)
                {
                    end = mid;
                }
                else
                {
                    begin = mid;
                }
            }

            if (ay[begin] > val || ay[end] < val)
            {
                throw new Exception("should not happen");
            }

            return end;
        }

        public T GetNode(String key)
        {
            //return GetNode_slow(key);

            int hash = BetterHash(key);

            int first = First_ge(ayKeys, hash);

            //int diff = circle.Keys[first] - hash;

            return circle[ayKeys[first]];
        }

        //default String.GetHashCode() can't well spread strings like "1", "2", "3"
        public static int BetterHash(String key)
        {
            uint hash = MurmurHash2.Hash(Encoding.ASCII.GetBytes(key));
            return (int)hash;
        }
    }
}