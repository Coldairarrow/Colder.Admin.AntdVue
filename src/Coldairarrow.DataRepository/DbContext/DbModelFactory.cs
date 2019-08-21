using Coldairarrow.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.Conventions;
using Oracle.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace Coldairarrow.DataRepository
{
    public static class DbModelFactory
    {
        #region 构造函数

        static DbModelFactory()
        {
            InitModelType();
        }

        #endregion

        #region 外部接口

        public static void AddObserver(IRepositoryDbContext observer)
        {
            _observers.Add(observer);
        }

        public static void RemoveObserver(IRepositoryDbContext observer)
        {
            _observers.Remove(observer);
        }

        /// <summary>
        /// 获取DbCompiledModel
        /// </summary>
        /// <param name="conStr">数据库连接名或字符串</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public static IModel GetDbCompiledModel(string conStr, DatabaseType dbType)
        {
            string modelInfoId = GetCompiledModelIdentity(conStr, dbType);
            if (_dbCompiledModel.ContainsKey(modelInfoId))
                return _dbCompiledModel[modelInfoId].Model;
            else
            {
                var theModelInfo = BuildDbCompiledModelInfo(conStr, dbType);
                _dbCompiledModel[modelInfoId] = theModelInfo;
                return theModelInfo.Model;
            }
        }

        /// <summary>
        /// 获取模型
        /// </summary>
        /// <param name="type">原类型</param>
        /// <returns></returns>
        public static Type GetModel(Type type)
        {
            string modelName = type.Name;

            if (_modelTypeMap.ContainsKey(modelName))
                return _modelTypeMap[modelName];
            else
            {
                _modelTypeMap[modelName] = type;
                RefreshModel();

                return type;
            }
        }

        #endregion

        #region 私有成员

        private static void InitModelType()
        {
            var assemblies = new Assembly[] { Assembly.Load("Coldairarrow.Entity") };
            List<Type> allTypes = new List<Type>();
            assemblies.ForEach(aAssembly =>
            {
                allTypes.AddRange(aAssembly.GetTypes());
            });
            List<Type> types = allTypes
                .Where(x => x.GetCustomAttribute(typeof(TableAttribute), false) != null)
                .ToList();

            types.ForEach(aType =>
            {
                _modelTypeMap[aType.Name] = aType;
            });
        }
        private static SynchronizedCollection<IRepositoryDbContext> _observers { get; } = new SynchronizedCollection<IRepositoryDbContext>();
        private static ConcurrentDictionary<string, Type> _modelTypeMap { get; } = new ConcurrentDictionary<string, Type>();
        private static ConcurrentDictionary<string, DbCompiledModelInfo> _dbCompiledModel { get; } = new ConcurrentDictionary<string, DbCompiledModelInfo>();
        private static DbCompiledModelInfo BuildDbCompiledModelInfo(string nameOrConStr, DatabaseType dbType)
        {
            lock (_buildCompiledModelLock)
            {
                ConventionSet conventionSet = null;
                switch (dbType)
                {
                    case DatabaseType.SqlServer: conventionSet = SqlServerConventionSetBuilder.Build(); break;
                    case DatabaseType.MySql: conventionSet = MySqlConventionSetBuilder.Build(); break;
                    case DatabaseType.PostgreSql: conventionSet = NpgsqlConventionSetBuilder.Build(); break;
                    case DatabaseType.Oracle: conventionSet = OracleConventionSetBuilder.Build(); break;
                    default: throw new Exception("暂不支持该数据库!");
                }
                ModelBuilder modelBuilder = new ModelBuilder(conventionSet);
                _modelTypeMap.Values.ForEach(x =>
                {
                    modelBuilder.Model.AddEntityType(x);
                });

                DbCompiledModelInfo newInfo = new DbCompiledModelInfo
                {
                    ConStr = nameOrConStr,
                    DatabaseType = dbType,
                    Model = modelBuilder.FinalizeModel()
                };
                return newInfo;
            }
        }
        private static string GetCompiledModelIdentity(string conStr, DatabaseType dbType)
        {
            return $"{dbType.ToString()}{conStr}";
        }
        private static object _buildCompiledModelLock { get; } = new object();
        private static void RefreshModel()
        {
            _dbCompiledModel.Values.ForEach(aModelInfo =>
            {
                aModelInfo.Model = BuildDbCompiledModelInfo(aModelInfo.ConStr, aModelInfo.DatabaseType).Model;
            });

            _observers.ForEach(x => x.RefreshDb());
        }

        #endregion

        #region 数据结构

        class DbCompiledModelInfo
        {
            public IModel Model { get; set; }
            public string ConStr { get; set; }
            public DatabaseType DatabaseType { get; set; }
        }

        #endregion
    }
}
