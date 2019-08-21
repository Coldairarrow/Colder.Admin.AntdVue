using System;
using System.Collections.Generic;

namespace Coldairarrow.Util
{
    public static class EnumHelper
    {
        /// <summary>
        /// 将枚举类型转为选项列表
        /// 注：Value为值,Text为显示内容
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static List<object> ToOptionList(Type enumType)
        {
            var values = Enum.GetValues(enumType);
            List<object> list = new List<object>();
            foreach (var aValue in values)
            {
                list.Add(new
                {
                    Value = (int)aValue,
                    Text = aValue.ToString()
                });
            }

            return list;
        }
    }
}
