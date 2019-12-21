using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        static BaseTest()
        {
            InitId();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            string tmp = string.Empty;
        }

        private static void InitId()
        {
            new IdHelperBootstrapper()
                //设置WorkerId
                .SetWorkderId(ConfigHelper.GetValue("WorkerId").ToLong())
                //使用Zookeeper
                //.UseZookeeper("127.0.0.1:2181", 200, GlobalSwitch.ProjectName)
                .Boot();
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
