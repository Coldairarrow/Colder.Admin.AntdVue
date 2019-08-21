using Coldairarrow.Business;
using Coldairarrow.DataRepository;
using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.UnitTests
{
    [TestClass]
    public class Base_BusinessTests:BaseTest
    {
        protected override void Clear()
        {
            _baseBus.DeleteAll();
        }

        #region 私有成员

        private BaseBusiness<Base_UnitTest> _baseBus { get; } = new BaseBusiness<Base_UnitTest>();

        #endregion

        #region 测试用例

        /// <summary>
        /// 插入数据测试
        /// </summary>
        [TestMethod]
        public void InsertTest()
        {
            //单条数据
            _baseBus.Insert(_newData);
            var theData = _baseBus.GetIQueryable().FirstOrDefault();
            Assert.AreEqual(_newData.ToJson(), theData.ToJson());

            //多条数据
            _baseBus.DeleteAll();
            _baseBus.Insert(_insertList);
            var theList = _baseBus.GetList();
            Assert.AreEqual(_insertList.OrderBy(X => X.Id).ToJson(), theList.OrderBy(X => X.Id).ToJson());
        }

        /// <summary>
        /// 删除数据测试
        /// </summary>
        [TestMethod]
        public void DeleteTest()
        {
            _baseBus.DeleteAll();
            //删除表所有数据
            _baseBus.Insert(_insertList);
            _baseBus.DeleteAll();
            int count = _baseBus.GetIQueryable().Count();
            Assert.AreEqual(0, count);

            //删除单条数据,对象形式
            _baseBus.DeleteAll();
            _baseBus.Insert(_newData);
            _baseBus.Delete(_newData);
            count = _baseBus.GetIQueryable().Count();
            Assert.AreEqual(0, count);

            //删除单条数据,主键形式
            _baseBus.DeleteAll();
            _baseBus.Insert(_newData);
            _baseBus.Delete(_newData.Id);
            count = _baseBus.GetIQueryable().Count();
            Assert.AreEqual(0, count);

            //删除多条数据
            _baseBus.DeleteAll();
            _baseBus.Insert(_insertList);
            _baseBus.Delete(_insertList);
            count = _baseBus.GetIQueryable().Count();
            Assert.AreEqual(0, count);

            //删除多条数据,主键形式
            _baseBus.DeleteAll();
            _baseBus.Insert(_insertList);
            _baseBus.Delete(_insertList.Select(x => x.Id).ToList());
            count = _baseBus.GetIQueryable().Count();
            Assert.AreEqual(0, count);

            //删除指定数据
            _baseBus.DeleteAll();
            _baseBus.Insert(_insertList);
            _baseBus.Delete(x => x.UserId == "Admin2");
            count = _baseBus.GetIQueryable().Count();
            Assert.AreEqual(1, count);
        }

        /// <summary>
        /// 更新数据测试
        /// </summary>
        [TestMethod]
        public void UpdateTest()
        {
            //更新单条数据
            _baseBus.DeleteAll();
            _baseBus.Insert(_newData);
            var updateData = _newData.DeepClone();
            updateData.UserId = "Admin_Update";
            _baseBus.Update(updateData);
            var dbUpdateData = _baseBus.GetIQueryable().FirstOrDefault();
            Assert.AreEqual(updateData.ToJson(), dbUpdateData.ToJson());

            //更新多条数据
            _baseBus.DeleteAll();
            _baseBus.Insert(_insertList);
            var updateList = _insertList.DeepClone();
            updateList[0].UserId = "Admin3";
            updateList[1].UserId = "Admin4";
            _baseBus.Update(updateList);
            int count = _baseBus.GetIQueryable().Where(x => x.UserId == "Admin3" || x.UserId == "Admin4").Count();
            Assert.AreEqual(2, count);

            //更新单条数据指定属性
            _baseBus.DeleteAll();
            _baseBus.Insert(_newData);
            var newUpdateData = _newData.DeepClone();
            newUpdateData.UserName = "普通管理员";
            newUpdateData.UserId = "xiaoming";
            newUpdateData.Age = 100;
            _baseBus.UpdateAny(newUpdateData, new List<string> { "UserName", "Age" });
            var dbSingleData = _baseBus.GetIQueryable().FirstOrDefault();
            newUpdateData.UserId = "Admin";
            Assert.AreEqual(newUpdateData.ToJson(), dbSingleData.ToJson());

            //更新多条数据指定属性
            _baseBus.DeleteAll();
            _baseBus.Insert(_insertList);
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

            _baseBus.UpdateAny(newList1, new List<string> { "UserName", "Age" });
            var dbData = _baseBus.GetList();
            Assert.AreEqual(newList2.OrderBy(x => x.Id).ToJson(), dbData.OrderBy(x => x.Id).ToJson());

            //更新指定条件数据
            _baseBus.DeleteAll();
            _baseBus.Insert(_newData);
            _baseBus.UpdateWhere(x => x.UserId == "Admin", x =>
            {
                x.UserId = "Admin2";
            });

            Assert.IsTrue(_baseBus.GetIQueryable().Any(x => x.UserId == "Admin2"));
        }

        /// <summary>
        /// 查找数据测试
        /// </summary>
        [TestMethod]
        public void SearchTest()
        {
            //GetEntity获取实体
            _baseBus.DeleteAll();
            _baseBus.Insert(_newData);
            var theData = _baseBus.GetEntity(_newData.Id);
            Assert.AreEqual(_newData.ToJson(), theData.ToJson());

            //GetList获取表的所有数据
            _baseBus.DeleteAll();
            _baseBus.Insert(_insertList);
            var dbList = _baseBus.GetList();
            Assert.AreEqual(_insertList.OrderBy(x => x.Id).ToJson(), dbList.OrderBy(x => x.Id).ToJson());

            //GetIQueryable获取实体对应的表，延迟加载，主要用于支持Linq查询操作
            int count = _baseBus.GetIQueryable().Where(x => x.UserId == "Admin1").Count();
            Assert.AreEqual(1, count);

            //GetIQPagination获取分页后的数据
            _baseBus.DeleteAll();
            _baseBus.Insert(_dataList);
            Pagination pagination = new Pagination
            {
                SortField = "Age",
                SortType = "asc",
                PageIndex = 2,
                PageRows = 20
            };
            dbList = _baseBus.GetPagination(_baseBus.GetIQueryable(), pagination);
            var dataList = _dataList.GetPagination(pagination);
            Assert.AreEqual(dbList.ToJson(), dataList.ToJson());

            //GetIQPagination获取分页后的数据
            int pages = 0;
            dbList = _baseBus.GetPagination(_baseBus.GetIQueryable(), pagination.PageIndex, pagination.PageRows, pagination.SortField, pagination.SortType, ref count, ref pages);
            Assert.AreEqual(dbList.ToJson(), dataList.ToJson());

            //GetDataTableWithSql通过Sql查询返回DataTable
            _baseBus.DeleteAll();
            _baseBus.Insert(_insertList);
            var table = _baseBus.GetDataTableWithSql("select * from Base_UnitTest order by Id asc");
            Assert.AreEqual(_insertList.OrderBy(x => x.Id).ToJson(), table.ToList<Base_UnitTest>().OrderBy(x => x.Id).ToJson());

            //GetDataTableWithSql通过Sql查询返回DataTable
            _baseBus.DeleteAll();
            _baseBus.Insert(_insertList);

            List<DbParameter> paramters = new List<DbParameter>()
            {
                new SqlParameter("@userId","Admin1")
            };
            table = _baseBus.GetDataTableWithSql("select * from Base_UnitTest where UserId = @userId", paramters);
            Assert.AreEqual(_insertList.Where(x => x.UserId == "Admin1").OrderBy(x => x.Id).ToJson(), table.ToList<Base_UnitTest>().OrderBy(x => x.Id).ToJson());

            //GetListBySql通过sql返回List
            _baseBus.DeleteAll();
            _baseBus.Insert(_insertList);
            var list = _baseBus.GetListBySql<Base_UnitTest>("select * from Base_UnitTest order by Id asc");
            Assert.AreEqual(_insertList.OrderBy(x => x.Id).ToJson(), list.OrderBy(x => x.Id).ToJson());

            //GetListBySql通过sql返回List
            _baseBus.DeleteAll();
            _baseBus.Insert(_insertList);

            paramters = new List<DbParameter>()
            {
                new SqlParameter("@userId","Admin1")
            };
            list = _baseBus.GetListBySql<Base_UnitTest>("select * from Base_UnitTest where UserId = @userId", paramters);
            Assert.AreEqual(_insertList.Where(x => x.UserId == "Admin1").OrderBy(x => x.Id).ToJson(), list.OrderBy(x => x.Id).ToJson());
        }

        /// <summary>
        /// 执行SQL语句测试
        /// </summary>
        [TestMethod]
        public void ExcuteSqlTest()
        {
            //ExcuteBySql执行Sql语句
            _baseBus.DeleteAll();
            _baseBus.Insert(_newData);
            string sql = "delete from Base_UnitTest";
            _baseBus.ExecuteSql(sql);
            int count = _baseBus.GetIQueryable().Count();
            Assert.AreEqual(0, count);

            //ExcuteBySql通过参数执行Sql语句
            _baseBus.DeleteAll();
            _baseBus.Insert(_newData);
            sql = "delete from Base_UnitTest where UserName like '%'+@name+'%'";
            SqlParameter parameter = new SqlParameter("@name", "管理员");
            _baseBus.ExecuteSql(sql, new List<DbParameter> { parameter });
            count = _baseBus.GetIQueryable().Count();
            Assert.AreEqual(0, count);
        }

        /// <summary>
        /// 事务提交测试（单库）
        /// </summary>
        [TestMethod]
        public void TransactionTest()
        {
            //失败事务,默认级别
            new Action(() =>
            {
                using (var transaction = _baseBus.BeginTransaction())
                {
                    _baseBus.Insert(_newData);
                    var newData2 = _newData.DeepClone();
                    newData2.Id = IdHelper.GetId();
                    _baseBus.Insert(newData2);
                    bool succcess = _baseBus.EndTransaction().Success;
                    Assert.AreEqual(succcess, false);
                }
            })();

            //成功事务,默认级别
            new Action(() =>
            {
                Clear();
                using (var transaction = _baseBus.BeginTransaction())
                {
                    var newData = _newData.DeepClone();
                    newData.Id = Guid.NewGuid().ToString();
                    newData.UserId = IdHelper.GetId();
                    newData.UserName = IdHelper.GetId();
                    _baseBus.Insert(_newData);
                    _baseBus.Insert(newData);
                    bool succcess = _baseBus.EndTransaction().Success;
                    int count = _baseBus.GetIQueryable().Count();
                    Assert.AreEqual(succcess, true);
                    Assert.AreEqual(count, 2);
                }
            })();

            //隔离级别:RepeatableRead
            new Action(() =>
            {
                Clear();
                var db1 = DbFactory.GetRepository();
                var db2 = DbFactory.GetRepository();
                db1.Insert(_newData);
                using (db1.BeginTransaction(IsolationLevel.RepeatableRead))
                {
                    //db1读=>db2写(阻塞)=>db1读=>db1提交
                    var db1Data_1 = db1.GetIQueryable<Base_UnitTest>().Where(x => x.Id == _newData.Id).FirstOrDefault();

                    var updateData = _newData.DeepClone();
                    updateData.UserName = IdHelper.GetId();
                    var task = Task.Run(() =>
                    {
                        db2.Update(updateData);
                    });

                    var db1Data_2 = db1.GetIQueryable<Base_UnitTest>().Where(x => x.Id == _newData.Id).FirstOrDefault();
                    Assert.AreEqual(db1Data_1.ToJson(), db1Data_2.ToJson());

                    db1.EndTransaction();
                    task.Wait();
                    var db1Data_3 = db1.GetIQueryable<Base_UnitTest>().Where(x => x.Id == _newData.Id).FirstOrDefault();
                    Assert.AreEqual(updateData.ToJson(), db1Data_3.ToJson());
                }
            })();
        }

        /// <summary>
        /// 分布式事务提交测试（跨库）
        /// </summary>
        [TestMethod]
        public void DistributedTransactionTest()
        {
            //失败事务
            BaseBusiness<Base_UnitTest> _bus1 = new BaseBusiness<Base_UnitTest>();
            BaseBusiness<Base_UnitTest> _bus2 = new BaseBusiness<Base_UnitTest>("BaseDb_Test");
            _bus1.DeleteAll();
            _bus2.DeleteAll();
            Base_UnitTest data1 = new Base_UnitTest
            {
                Id = Guid.NewGuid().ToString(),
                UserId = "1",
                UserName = Guid.NewGuid().ToString()
            };
            Base_UnitTest data2 = new Base_UnitTest
            {
                Id = Guid.NewGuid().ToString(),
                UserId = "1",
                UserName = Guid.NewGuid().ToString()
            };
            Base_UnitTest data3 = new Base_UnitTest
            {
                Id = Guid.NewGuid().ToString(),
                UserId = "2",
                UserName = Guid.NewGuid().ToString()
            };
            using (var distributedTransaction = new DistributedTransaction(_bus1.Service, _bus2.Service))
            {
                distributedTransaction.BeginTransaction();
                _bus1.ExecuteSql("insert into Base_UnitTest(Id) values('10') ");
                _bus1.Insert(data1);
                _bus1.Insert(data2);
                _bus2.Insert(data1);
                _bus2.Insert(data3);
                var succcess = distributedTransaction.EndTransaction();
                Assert.AreEqual(succcess.Success, false);
                Assert.AreEqual(_bus1.GetIQueryable().Count(), 0);
                Assert.AreEqual(_bus2.GetIQueryable().Count(), 0);
            }

            //成功事务
            using (var distributedTransaction = new DistributedTransaction(_bus1.Service, _bus2.Service))
            {
                distributedTransaction.BeginTransaction();
                _bus1.ExecuteSql("insert into Base_UnitTest(Id) values('10') ");
                _bus1.Insert(data1);
                _bus1.Insert(data3);
                _bus2.Insert(data1);
                _bus2.Insert(data3);
                var succcess = distributedTransaction.EndTransaction();
                int count1 = _bus1.GetIQueryable().Count();
                int count2 = _bus2.GetIQueryable().Count();
                Assert.AreEqual(succcess.Success, true);
                Assert.AreEqual(count1, 3);
                Assert.AreEqual(count2, 2);
            }
        }

        #endregion
    }
}
