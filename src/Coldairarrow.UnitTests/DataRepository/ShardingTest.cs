using Coldairarrow.DataRepository;
using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Coldairarrow.UnitTests
{
    [TestClass]
    public class ShardingTest : BaseTest
    {
        static ShardingTest()
        {
            ShardingConfigBootstrapper.Bootstrap()
                //添加数据源
                .AddDataSource("BaseDb", DatabaseType.SqlServer, dbBuilder =>
                {
                    //添加物理数据库
                    dbBuilder.AddPhsicDb("BaseDb", ReadWriteType.ReadAndWrite);
                })
                //添加抽象数据库
                .AddAbsDb("BaseDb", absTableBuilder =>
                {
                    //添加抽象数据表
                    absTableBuilder.AddAbsTable("Base_UnitTest", tableBuilder =>
                    {
                        //添加物理数据表
                        tableBuilder.AddPhsicTable("Base_UnitTest_0", "BaseDb");
                        tableBuilder.AddPhsicTable("Base_UnitTest_1", "BaseDb");
                        tableBuilder.AddPhsicTable("Base_UnitTest_2", "BaseDb");
                    }, new ModShardingRule("Base_UnitTest", "Id", 3));
                });
        }
        private IShardingRepository _db { get; } = DbFactory.GetRepository().ToSharding();
        protected override void Clear()
        {
            _db.DeleteAll<Base_UnitTest>();
        }

        /// <summary>
        /// 插入数据测试
        /// </summary>
        [TestMethod]
        public void InsertTest()
        {
            //单条数据
            _db.Insert(_newData);
            var theData = _db.GetIShardingQueryable<Base_UnitTest>().FirstOrDefault();
            Assert.AreEqual(_newData.ToJson(), theData.ToJson());

            //多条数据
            Clear();
            _db.Insert(_insertList);
            var theList = _db.GetList<Base_UnitTest>();
            Assert.AreEqual(_insertList.OrderBy(X => X.Id).ToJson(), theList.OrderBy(X => X.Id).ToJson());
        }

        /// <summary>
        /// 删除数据测试
        /// </summary>
        [TestMethod]
        public void DeleteTest()
        {
            //删除表所有数据
            _db.Insert(_insertList);
            _db.DeleteAll<Base_UnitTest>();
            int count = _db.GetIShardingQueryable<Base_UnitTest>().Count();
            Assert.AreEqual(0, count);

            //删除单条数据,对象形式
            Clear();
            _db.Insert(_newData);
            _db.Delete(_newData);
            count = _db.GetIShardingQueryable<Base_UnitTest>().Count();
            Assert.AreEqual(0, count);

            //删除多条数据
            Clear();
            _db.Insert(_insertList);
            _db.Delete(_insertList);
            count = _db.GetIShardingQueryable<Base_UnitTest>().Count();
            Assert.AreEqual(0, count);

            //删除指定数据
            Clear();
            _db.Insert(_insertList);
            _db.Delete<Base_UnitTest>(x => x.UserId == "Admin2");
            count = _db.GetIShardingQueryable<Base_UnitTest>().Count();
            Assert.AreEqual(1, count);
        }

        /// <summary>
        /// 更新数据测试
        /// </summary>
        [TestMethod]
        public void UpdateTest()
        {
            //更新单条数据
            _db.Insert(_newData);
            var updateData = _newData.DeepClone();
            updateData.UserId = "Admin_Update";
            _db.Update(updateData);
            var dbUpdateData = _db.GetIShardingQueryable<Base_UnitTest>().FirstOrDefault();
            Assert.AreEqual(updateData.ToJson(), dbUpdateData.ToJson());

            //更新多条数据
            Clear();
            _db.Insert(_insertList);
            var updateList = _insertList.DeepClone();
            updateList[0].UserId = "Admin3";
            updateList[1].UserId = "Admin4";
            _db.Update(updateList);
            int count = _db.GetIShardingQueryable<Base_UnitTest>().Where(x => x.UserId == "Admin3" || x.UserId == "Admin4").Count();
            Assert.AreEqual(2, count);

            //更新单条数据指定属性
            Clear();
            _db.Insert(_newData);
            var newUpdateData = _newData.DeepClone();
            newUpdateData.UserName = "普通管理员";
            newUpdateData.UserId = "xiaoming";
            newUpdateData.Age = 100;
            _db.UpdateAny(newUpdateData, new List<string> { "UserName", "Age" });
            var dbSingleData = _db.GetIShardingQueryable<Base_UnitTest>().FirstOrDefault();
            newUpdateData.UserId = "Admin";
            Assert.AreEqual(newUpdateData.ToJson(), dbSingleData.ToJson());

            //更新多条数据指定属性
            Clear();
            _db.Insert(_insertList);
            var newList1 = _insertList.DeepClone();
            var newList2 = _insertList.DeepClone();
            newList1.ForEach(aData =>
            {
                aData.Age = 100;
                aData.UserId = "Test";
                aData.UserName = "测试";
            });
            newList2.ForEach(aData =>
            {
                aData.Age = 100;
                aData.UserName = "测试";
            });

            _db.UpdateAny(newList1, new List<string> { "UserName", "Age" });
            var dbData = _db.GetList<Base_UnitTest>();
            Assert.AreEqual(newList2.OrderBy(x => x.Id).ToJson(), dbData.OrderBy(x => x.Id).ToJson());

            //更新指定条件数据
            Clear();
            _db.Insert(_newData);
            _db.UpdateWhere<Base_UnitTest>(x => x.UserId == "Admin", x =>
            {
                x.UserId = "Admin2";
            });

            Assert.IsTrue(_db.GetIShardingQueryable<Base_UnitTest>().Any(x => x.UserId == "Admin2"));
        }

        /// <summary>
        /// 查找数据测试
        /// </summary>
        [TestMethod]
        public void SearchTest()
        {
            _db.Insert(_dataList);

            //GetList获取表的所有数据
            new Action(() =>
            {
                var local = _dataList.OrderBy(x=>x.Id).ToJson();
                var db = _db.GetList<Base_UnitTest>().OrderBy(x => x.Id).ToJson();
                Assert.AreEqual(local, db);
            })();

            //GetIQPagination获取分页后的数据
            new Action(() =>
            {
                Pagination pagination = new Pagination
                {
                    SortField = "Age",
                    SortType = "asc",
                    PageIndex = 2,
                    PageRows = 20
                };

                var local = _dataList.GetPagination(pagination).ToJson();
                var db = _db.GetIShardingQueryable<Base_UnitTest>().GetPagination(pagination).ToJson();
                Assert.AreEqual(local, db);
            })();

            //Max
            new Action(() =>
            {
                var local = _dataList.Max(x => x.Age);
                var db = _db.GetIShardingQueryable<Base_UnitTest>().Max(x => x.Age);
                Assert.AreEqual(local, db);
            })();

            //Min
            new Action(() =>
            {
                var local = _dataList.Min(x => x.Age);
                var db = _db.GetIShardingQueryable<Base_UnitTest>().Min(x => x.Age);
                Assert.AreEqual(local, db);
            })();

            //Average
            new Action(() =>
            {
                var local = _dataList.Average(x => x.Age);
                var db = _db.GetIShardingQueryable<Base_UnitTest>().Average(x => x.Age);
                Assert.AreEqual(local, db);
            })();

            //Sum
            new Action(() =>
            {
                var local = _dataList.Sum(x => x.Age);
                var db = _db.GetIShardingQueryable<Base_UnitTest>().Sum(x => x.Age);
                Assert.AreEqual(local, db);
            })();

            //Any
            new Action(() =>
            {
                var local = _dataList.Any(x => x.Age == 99);
                var db = _db.GetIShardingQueryable<Base_UnitTest>().Any(x => x.Age == 99);
                Assert.AreEqual(local, db);
            })();

            //DynamicWhere
            new Action(() =>
            {
                var local = _dataList.AsQueryable().Where("Age > 50").Count();
                var db = _db.GetIShardingQueryable<Base_UnitTest>().Where("Age > 50").Count();
                Assert.AreEqual(local, db);
            })();
        }

        /// <summary>
        /// 事务提交测试（单库）
        /// </summary>
        [TestMethod]
        public void TransactionTest()
        {
            //失败事务
            new Action(() =>
            {
                using (var transaction = _db.BeginTransaction())
                {
                    _db.Insert(_newData);
                    var newData2 = _newData.DeepClone();
                    _db.Insert(newData2);
                    bool succcess = _db.EndTransaction().Success;
                    Assert.AreEqual(succcess, false);
                }
            })();

            //成功事务
            new Action(() =>
            {
                _db.DeleteAll<Base_UnitTest>();
                using (var transaction = _db.BeginTransaction())
                {
                    var newData = _newData.DeepClone();
                    newData.Id = Guid.NewGuid().ToString();
                    newData.UserId = IdHelper.GetId();
                    newData.UserName = IdHelper.GetId();
                    _db.Insert(_newData);
                    _db.Insert(newData);
                    bool succcess = _db.EndTransaction().Success;
                    int count = _db.GetIShardingQueryable<Base_UnitTest>().Count();
                    Assert.AreEqual(succcess, true);
                    Assert.AreEqual(count, 2);
                }
            })();

            //隔离级别:RepeatableRead
            new Action(() =>
            {
                Clear();
                var db1 = DbFactory.GetShardingRepository();
                var db2 = DbFactory.GetShardingRepository();
                db1.Insert(_newData);
                using (db1.BeginTransaction(IsolationLevel.RepeatableRead))
                {
                    //db1读=>db2写(阻塞)=>db1读=>db1提交
                    var db1Data_1 = db1.GetIShardingQueryable<Base_UnitTest>().Where(x => x.Id == _newData.Id).FirstOrDefault();

                    var updateData = _newData.DeepClone();
                    updateData.UserName = IdHelper.GetId();
                    var task = Task.Run(() =>
                    {
                        db2.Update(updateData);
                    });

                    var db1Data_2 = db1.GetIShardingQueryable<Base_UnitTest>().Where(x => x.Id == _newData.Id).FirstOrDefault();
                    Assert.AreEqual(db1Data_1.ToJson(), db1Data_2.ToJson());

                    db1.EndTransaction();
                    task.Wait();
                    var db1Data_3 = db1.GetIShardingQueryable<Base_UnitTest>().Where(x => x.Id == _newData.Id).FirstOrDefault();
                    Assert.AreEqual(updateData.ToJson(), db1Data_3.ToJson());
                }
            })();
        }
    }
}
