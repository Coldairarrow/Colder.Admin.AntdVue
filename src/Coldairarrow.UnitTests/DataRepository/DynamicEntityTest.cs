using Coldairarrow.DataRepository;
using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.UnitTests
{
    [TestClass]
    public class DynamicEntityTest : BaseTest
    {
        #region 构造函数

        #endregion

        #region 私有成员

        protected override void Clear()
        {
            _db.DeleteAll<Base_UnitTest>();
        }
        protected IRepository _db { get; } = DbFactory.GetRepository();

        private Type MapTable(Type absTable, string targetTableName)
        {
            return ShardingHelper.MapTable(absTable, targetTableName);
        }

        #endregion

        #region 测试用例

        [TestMethod]
        public void TransactionTest()
        {
            Type targetType = MapTable(typeof(Base_UnitTest), "Base_UnitTest_0");
            //失败事务,Base_UnitTest成功,Base_UnitTest_0失败
            Clear();
            using (var transaction = _db.BeginTransaction())
            {
                //Base_UnitTest成功
                new Action(() =>
                {
                    _db.ExecuteSql("insert into Base_UnitTest(Id) values('10') ");

                    _db.Insert(_newData);

                    var newData2 = _newData.DeepClone();
                    newData2.Id = IdHelper.GetId();
                    newData2.UserId = IdHelper.GetId();
                    _db.Insert(newData2);
                })();

                //Base_UnitTest_0失败
                new Action(() =>
                {
                    _db.ExecuteSql("insert into Base_UnitTest_0(Id) values('10') ");

                    _db.Insert(_newData.ChangeType(targetType));

                    var newData2 = _newData.DeepClone();
                    newData2.Id = IdHelper.GetId();

                    //UserId唯一导致异常
                    //newData2.UserId = IdHelper.GetId();
                    _db.Insert(newData2.ChangeType(targetType));
                })();

                var (Success, ex) = _db.EndTransaction();
                Assert.AreEqual(Success, false);
                Assert.AreEqual(true, ex is DbUpdateException);
                Assert.AreEqual(_db.GetIQueryable<Base_UnitTest>().Count(), 0);
                Assert.AreEqual(_db.GetIQueryable(targetType).Count(), 0);
            }

            //成功事务,Base_UnitTest成功,Base_UnitTest_0成功
            Clear();
            using (var transaction = _db.BeginTransaction())
            {
                //Base_UnitTest成功
                new Action(() =>
                {
                    _db.ExecuteSql("insert into Base_UnitTest(Id) values('10') ");

                    _db.Insert(_newData);

                    var newData2 = _newData.DeepClone();
                    newData2.Id = IdHelper.GetId();
                    newData2.UserId = IdHelper.GetId();
                    _db.Insert(newData2);
                })();

                //Base_UnitTest_0成功
                new Action(() =>
                {
                    _db.ExecuteSql("insert into Base_UnitTest_0(Id) values('10') ");

                    _db.Insert(_newData.ChangeType(targetType));

                    var newData2 = _newData.DeepClone();
                    newData2.Id = IdHelper.GetId();
                    newData2.UserId = IdHelper.GetId();
                    _db.Insert(newData2.ChangeType(targetType));
                })();

                bool succcess = _db.EndTransaction().Success;
                Assert.AreEqual(succcess, true);
                Assert.AreEqual(_db.GetIQueryable<Base_UnitTest>().Count(), 3);
                Assert.AreEqual(_db.GetIQueryable(targetType).Count(), 3);
            }

            void Clear()
            {
                _db.DeleteAll<Base_UnitTest>();
                _db.DeleteAll(targetType);
            }
        }

        #endregion
    }
}
