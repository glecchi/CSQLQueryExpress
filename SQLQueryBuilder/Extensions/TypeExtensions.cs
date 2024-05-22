using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SQLQueryBuilder.Extensions
{
    public static class TypeExtensions
    {
        public static IList<PropertyInfo> GetReadableProperties(this Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
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
                 .ToList();
        }

        public static IList<PropertyInfo> GetAllColumnsProperties(this Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
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
        }

        public static IList<PropertyInfo> GetWritableColumnsProperties(this Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                 .Where(p =>
                     p.CanWrite &&
                     p.CanRead &&
                     p.GetCustomAttribute<DatabaseGeneratedAttribute>() == null &&
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
        }
    }
}
