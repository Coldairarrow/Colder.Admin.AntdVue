using Coldairarrow.Entity.Base_SysManage;
using System;
using System.Collections.Generic;

namespace Coldairarrow.UnitTests
{
    public class BaseTest
    {
        public BaseTest()
        {
            for (int i = 1; i <= 100; i++)
            {
                Base_UnitTest newData = new Base_UnitTest
                {
                    Id = Guid.NewGuid().ToString(),
                    Age = i,
                    UserId = "Admin" + i,
                    UserName = "超级管理员" + i
                };
                _dataList.Add(newData);
            }

            Clear();
        }

        protected Base_UnitTest _newData { get; } = new Base_UnitTest
        {
            Id = Guid.NewGuid().ToString(),
            UserId = "Admin",
            UserName = "超级管理员",
            Age = 22
        };

        protected List<Base_UnitTest> _insertList { get; } = new List<Base_UnitTest>
        {
            new Base_UnitTest
            {
                Id = Guid.NewGuid().ToString(),
                UserId = "Admin1",
                UserName = "超级管理员1",
                Age = 22
            },
            new Base_UnitTest
            {
                Id = Guid.NewGuid().ToString(),
                UserId = "Admin2",
                UserName = "超级管理员2",
                Age = 22
            }
        };

        protected List<Base_UnitTest> _dataList { get; } = new List<Base_UnitTest>();

        protected virtual void Clear()
        {
            throw new NotImplementedException();
        }
    }
}
