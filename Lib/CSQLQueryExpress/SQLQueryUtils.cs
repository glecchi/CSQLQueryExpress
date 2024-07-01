using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CSQLQueryExpress
{
    internal static class SQLQueryUtils
    {
        public static bool IsHierarchicalSelectFromCte(this ISQLQuery query)
        {
            foreach (var fragment in query)
            {
                if (fragment.FragmentType == SQLQueryFragmentType.FromBySelect ||
                    fragment.FragmentType == SQLQueryFragmentType.JoinBySelect)
                {
                    var hSelect = ((ISQLQueryFragmentFromSelect)fragment).FromSelect;
                    if (hSelect.FragmentType == SQLQueryFragmentType.Select)
                    {
                        return IsHierarchicalSelectFromCte(hSelect);
                    }
                    else if (hSelect.FragmentType == SQLQueryFragmentType.SelectCte) 
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static T[] Merge<T>(this T first, T second, params T[] others)
        {
            var list = new List<T>
            {
                first,
                second
            };

            if (others != null && others.Length > 0)
            {
                list.AddRange(others);
            }

            return list.ToArray();
        }

        public static T[] Merge<T>(this T first, params T[] others)
        {
            var list = new List<T>
            {
                first
            };

            if (others != null && others.Length > 0)
            {
                list.AddRange(others);
            }

            return list.ToArray();
        }

        public static IEnumerable<Enum> GetFlags(this Enum input)
        {
            foreach (Enum value in Enum.GetValues(input.GetType()))
            {
                if (input.HasFlag(value))
                {
                    yield return value;
                }
            }
        }

        private static Dictionary<Type, IList<ReadablePropertyInfo>> _readableProperties = new Dictionary<Type, IList<ReadablePropertyInfo>>();

        public static IList<ReadablePropertyInfo> GetReadableProperties(this Type type)
        {
            if (!_readableProperties.TryGetValue(type, out IList<ReadablePropertyInfo> properties))
            {
                properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                 .Where(p =>
                     p.CanRead &&
                     (
                         p.PropertyType.IsValueType ||
                         p.PropertyType == typeof(string) ||

                             p.PropertyType.IsGenericType &&
                             p.PropertyType.IsAssignableFrom(typeof(Nullable<>)) &&
                             (p.PropertyType.GetGenericTypeDefinition().IsValueType || p.PropertyType.GetGenericTypeDefinition() == typeof(string))
                          ||

                             p.PropertyType.IsArray &&
                             (
                                 p.PropertyType.GetElementType().IsValueType ||
                                 p.PropertyType.GetElementType() == typeof(string) ||

                                     p.PropertyType.GetElementType().IsGenericType &&
                                     p.PropertyType.GetElementType().IsAssignableFrom(typeof(Nullable<>)) &&
                                     (p.PropertyType.GetElementType().GetGenericTypeDefinition().IsValueType || p.PropertyType.GetElementType().GetGenericTypeDefinition() == typeof(string))

                             )

                     ))
                 .Select(p => new ReadablePropertyInfo(p))
                 .ToList();

                _readableProperties.Add(type, properties);
            }

            return properties;
        }

        private static Dictionary<Type, IList<PropertyInfo>> _allColumnsProperties = new Dictionary<Type, IList<PropertyInfo>>();

        public static IList<PropertyInfo> GetAllColumnsProperties(this Type type)
        {
            if (!_allColumnsProperties.TryGetValue(type, out IList<PropertyInfo> properties))
            {
                properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p =>
                    p.CanWrite &&
                    p.CanRead &&
                    (
                        p.PropertyType.IsValueType ||
                        p.PropertyType == typeof(string) ||

                            p.PropertyType.IsGenericType &&
                            p.PropertyType.IsAssignableFrom(typeof(Nullable<>)) &&
                            (p.PropertyType.GetGenericTypeDefinition().IsValueType || p.PropertyType.GetGenericTypeDefinition() == typeof(string))
                         ||

                            p.PropertyType.IsArray &&
                            (
                                p.PropertyType.GetElementType().IsValueType ||
                                p.PropertyType.GetElementType() == typeof(string) ||

                                    p.PropertyType.GetElementType().IsGenericType &&
                                    p.PropertyType.GetElementType().IsAssignableFrom(typeof(Nullable<>)) &&
                                    (p.PropertyType.GetElementType().GetGenericTypeDefinition().IsValueType || p.PropertyType.GetElementType().GetGenericTypeDefinition() == typeof(string))

                            )

                    ))
                .ToList();

                _allColumnsProperties.Add(type, properties);
            }

            return properties;
        }

        private static Dictionary<Type, IList<PropertyInfo>> _writableColumnsProperties = new Dictionary<Type, IList<PropertyInfo>>();

        public static IList<PropertyInfo> GetWritableColumnsProperties(this Type type)
        {
            if (!_writableColumnsProperties.TryGetValue(type, out IList<PropertyInfo> properties))
            {
                properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                 .Where(p =>
                     p.CanWrite &&
                     p.CanRead &&
                     (p.GetCustomAttribute<DatabaseGeneratedAttribute>() == null || p.GetCustomAttribute<DatabaseGeneratedAttribute>().DatabaseGeneratedOption == DatabaseGeneratedOption.None) &&
                     (
                         p.PropertyType.IsValueType ||
                         p.PropertyType == typeof(string) ||

                             p.PropertyType.IsGenericType &&
                             p.PropertyType.IsAssignableFrom(typeof(Nullable<>)) &&
                             (p.PropertyType.GetGenericTypeDefinition().IsValueType || p.PropertyType.GetGenericTypeDefinition() == typeof(string))
                          ||

                             p.PropertyType.IsArray &&
                             (
                                 p.PropertyType.GetElementType().IsValueType ||
                                 p.PropertyType.GetElementType() == typeof(string) ||

                                     p.PropertyType.GetElementType().IsGenericType &&
                                     p.PropertyType.GetElementType().IsAssignableFrom(typeof(Nullable<>)) &&
                                     (p.PropertyType.GetElementType().GetGenericTypeDefinition().IsValueType || p.PropertyType.GetElementType().GetGenericTypeDefinition() == typeof(string))

                             )

                     ))
                 .ToList();

                _writableColumnsProperties.Add(type, properties);   
            }

            return properties;
        }
    }
}

internal class ReadablePropertyInfo
{
    private readonly PropertyInfo _property;
    private readonly Func<object, object> _propertyGetter;

    public ReadablePropertyInfo(PropertyInfo property)
    {
        _property = property;
        _propertyGetter = GenerateGetterLambda(property);
    }

    public string Name { get { return _property.Name; } }

    public T GetCustomAttribute<T>() where T : Attribute
    {
        return _property.GetCustomAttribute<T>();
    }

    public object GetValue(object obj)
    {
        return _propertyGetter(obj);
    }

    /// <summary>
    /// Source: https://blog.zhaytam.com/2020/11/17/expression-trees-property-getter/
    /// </summary>
    /// <param name="property">Property of type</param>
    /// <returns>Delegate Getter</returns>
    private static Func<object, object> GenerateGetterLambda(PropertyInfo property)
    {
        // Define our instance parameter, which will be the input of the Func
        var objParameterExpr = Expression.Parameter(typeof(object), "instance");
        // 1. Cast the instance to the correct type
        var instanceExpr = Expression.TypeAs(objParameterExpr, property.DeclaringType);
        // 2. Call the getter and retrieve the value of the property
        var propertyExpr = Expression.Property(instanceExpr, property);
        // 3. Convert the property's value to object
        var propertyObjExpr = Expression.Convert(propertyExpr, typeof(object));
        // Create a lambda expression of the latest call & compile it
        return Expression.Lambda<Func<object, object>>(propertyObjExpr, objParameterExpr).Compile();
    }
}
