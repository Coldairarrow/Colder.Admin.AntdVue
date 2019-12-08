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
            InitEntityType();
        }

        #endregion

        #region 外部接口

        /// <summary>
        /// 获取DbCompiledModel
        /// </summary>
        /// <param name="conStr">数据库连接名或字符串</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public static IModel GetDbCompiledModel(string conStr, DatabaseType dbType)
        {
            string modelInfoId = GetCompiledModelIdentity(conStr, dbType);
            bool success = _dbCompiledModel.TryGetValue(modelInfoId, out IModel resModel);
            if (!success)
            {
                var theLock = _lockDic.GetOrAdd(modelInfoId, new object());
                lock (theLock)
                {
                    success = _dbCompiledModel.TryGetValue(modelInfoId, out resModel);
                    if (!success)
                    {
                        resModel = BuildDbCompiledModel(dbType);
                        _dbCompiledModel[modelInfoId] = resModel;
                    }
                }
            }

            return resModel;
        }

        /// <summary>
        /// 获取实体模型
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static Type GetEntityType(string tableName)
        {
            if (!_entityTypeMap.ContainsKey(tableName))
                throw new Exception($"表[{tableName}]缺少实体模型!");

            return _entityTypeMap[tableName];
        }

        /// <summary>
        /// 添加实体模型
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="entityType">实体模型</param>
        public static void AddEntityType(string tableName, Type entityType)
        {
            if (_entityTypeMap.ContainsKey(tableName))
                throw new Exception($"表[{tableName}]已存在实体模型!");

            _entityTypeMap[tableName] = entityType;
            _dbCompiledModel.Clear();
        }

        #endregion

        #region 私有成员

        private static void InitEntityType()
        {
            List<Type> types = GlobalData.FxAllTypes
                .Where(x => x.GetCustomAttribute(typeof(TableAttribute), false) != null)
                .ToList();

            types.ForEach(aType =>
            {
                _entityTypeMap[aType.Name] = aType;
            });
        }
        private static ConcurrentDictionary<string, Type> _entityTypeMap { get; } =
            new ConcurrentDictionary<string, Type>();
        private static ConcurrentDictionary<string, IModel> _dbCompiledModel { get; }
            = new ConcurrentDictionary<string, IModel>();
        private static IModel BuildDbCompiledModel(DatabaseType dbType)
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
            _entityTypeMap.Values.ForEach(x =>
            {
                modelBuilder.Model.AddEntityType(x);
            });

            return modelBuilder.FinalizeModel();
        }
        private static string GetCompiledModelIdentity(string conStr, DatabaseType dbType)
        {
            return $"{dbType.ToString()}{conStr}";
        }
        private static readonly ConcurrentDictionary<string, object> _lockDic
            = new ConcurrentDictionary<string, object>();

        #endregion

        #region 数据结构

        #endregion
    }
}
