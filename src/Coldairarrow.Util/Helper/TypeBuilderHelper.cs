using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 运行时创建类型
    /// </summary>
    public static class TypeBuilderHelper
    {
        #region 外部接口

        /// <summary>
        /// 创建类型
        /// </summary>
        /// <param name="typeFullName">类型完全名,包括命名空间</param>
        /// <param name="assemblyName">类型程序集名</param>
        /// <param name="properties">类型属性配置</param>
        /// <returns></returns>
        public static Type BuildType(TypeConfig typeConfig)
        {
            TypeBuilder tb = GetTypeBuilder(typeConfig.FullName, typeConfig.AssemblyName);
            typeConfig.Attributes.ForEach(aAttribute =>
            {
                tb.SetCustomAttribute(GetCustomAttributeBuilder(aAttribute));
            });
            ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
            typeConfig.Properties.ForEach(aProperty =>
            {
                AddProperty(tb, aProperty.PropertyName, aProperty.PropertyType, aProperty.Attributes);
            });

            return tb.CreateTypeInfo();
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="type">原类型</param>
        /// <returns></returns>
        public static TypeConfig GetConfig(Type type)
        {
            TypeConfig typeConfig = new TypeConfig
            {
                FullName = type.FullName,
                AssemblyName = type.Assembly.FullName,
                Attributes = GetAttributeConfigs(type),
                Properties = type.GetProperties().Select(x => new PropertyConfig
                {
                    PropertyName = x.Name,
                    Attributes = GetAttributeConfigs(x),
                    PropertyType = x.PropertyType
                }).ToList()
            };

            return typeConfig;

            List<AttributeConfig> GetAttributeConfigs(MemberInfo theType)
            {
                return theType.GetCustomAttributesData().Select(y => new AttributeConfig
                {
                    Attribute = y.AttributeType,
                    ConstructorArgs = y.ConstructorArguments.Select(x => x.Value).ToList(),
                    Properties = y.NamedArguments.Select(x => (x.MemberName, x.TypedValue.Value)).ToList()
                }).ToList();
            }
        }

        #endregion

        #region 私有成员

        private static TypeBuilder GetTypeBuilder(string typeFullName, string assemblyName)
        {
            var an = new AssemblyName(assemblyName);
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(an, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
            TypeBuilder tb = moduleBuilder.DefineType(typeFullName,
                    TypeAttributes.Public |
                    TypeAttributes.Class |
                    TypeAttributes.AutoClass |
                    TypeAttributes.AnsiClass |
                    TypeAttributes.BeforeFieldInit |
                    TypeAttributes.AutoLayout,
                    null);
            return tb;
        }
        private static void AddProperty(TypeBuilder tb, string propertyName, Type propertyType, List<AttributeConfig> attributes)
        {
            FieldBuilder fieldBuilder = tb.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

            PropertyBuilder propertyBuilder = tb.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
            MethodBuilder getPropMthdBldr = tb.DefineMethod("get_" + propertyName, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, propertyType, Type.EmptyTypes);
            ILGenerator getIl = getPropMthdBldr.GetILGenerator();

            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fieldBuilder);
            getIl.Emit(OpCodes.Ret);

            MethodBuilder setPropMthdBldr =
                tb.DefineMethod("set_" + propertyName,
                  MethodAttributes.Public |
                  MethodAttributes.SpecialName |
                  MethodAttributes.HideBySig,
                  null, new[] { propertyType });

            ILGenerator setIl = setPropMthdBldr.GetILGenerator();
            Label modifyProperty = setIl.DefineLabel();
            Label exitSet = setIl.DefineLabel();

            setIl.MarkLabel(modifyProperty);
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, fieldBuilder);

            setIl.Emit(OpCodes.Nop);
            setIl.MarkLabel(exitSet);
            setIl.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getPropMthdBldr);
            propertyBuilder.SetSetMethod(setPropMthdBldr);

            //设置特性
            attributes?.ForEach(aAttribute =>
            {
                propertyBuilder.SetCustomAttribute(GetCustomAttributeBuilder(aAttribute));
            });
        }
        private static CustomAttributeBuilder GetCustomAttributeBuilder(AttributeConfig attributeConfig)
        {
            var attributeType = attributeConfig.Attribute;
            var attributeConstructor = attributeType.GetConstructor(attributeConfig.ConstructorArgs.Select(x => x.GetType()).ToArray());
            List<(PropertyInfo PropertyInfo, object Value)> properties = new List<(PropertyInfo, object)>();
            var allProperties = attributeType.GetProperties().ToList();
            attributeConfig.Properties.ForEach(aProperty =>
            {
                var theProperty = allProperties.Where(x => x.Name == aProperty.PropertyName).FirstOrDefault();
                if (theProperty != null)
                    properties.Add((theProperty, aProperty.Value));
            });
            var attributeBuilder = new CustomAttributeBuilder(
                attributeConstructor,
                attributeConfig.ConstructorArgs.ToArray(),
                properties.Select(x => x.PropertyInfo).ToArray(),
                properties.Select(x => x.Value).ToArray()
            );

            return attributeBuilder;
        }

        #endregion
    }

    /// <summary>
    /// 类型配置
    /// </summary>
    public class TypeConfig
    {
        /// <summary>
        /// 类型名
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 程序集名
        /// </summary>
        public string AssemblyName { get; set; } = Assembly.GetExecutingAssembly().GetName().FullName;

        /// <summary>
        /// 拥有的属性
        /// </summary>
        public List<PropertyConfig> Properties { get; set; } = new List<PropertyConfig>();

        /// <summary>
        /// 拥有的特性
        /// </summary>
        public List<AttributeConfig> Attributes { get; set; } = new List<AttributeConfig>();
    }

    /// <summary>
    /// 属性配置
    /// </summary>
    public class PropertyConfig
    {
        /// <summary>
        /// 属性名
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 属性类型
        /// </summary>
        public Type PropertyType { get; set; }

        /// <summary>
        /// 属性包含的自定义特性
        /// </summary>
        public List<AttributeConfig> Attributes { get; set; } = new List<AttributeConfig>();
    }

    /// <summary>
    /// 特性配置
    /// </summary>
    public class AttributeConfig
    {
        /// <summary>
        /// 特性类型
        /// </summary>
        public Type Attribute { get; set; }

        /// <summary>
        /// 构造函数参数
        /// </summary>
        public List<object> ConstructorArgs { get; set; } = new List<object>();

        /// <summary>
        /// 初始化属性
        /// </summary>
        public List<(string PropertyName, object Value)> Properties { get; set; } = new List<(string PropertyName, object Value)>();
    }
}