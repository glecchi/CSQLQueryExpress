﻿using CSQLQueryExpress.Fragments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace CSQLQueryExpress
{
    internal static class SQLQueryUtils
    {
        public static bool IsHierarchicalSelectFromCte(this SQLQuerySelect select)
        {
            foreach (var fragment in select)
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

        private static Dictionary<Type, IList<PropertyInfo>> _readableProperties = new Dictionary<Type, IList<PropertyInfo>>();

        public static IList<PropertyInfo> GetReadableProperties(this Type type)
        {
            if (!_readableProperties.TryGetValue(type, out IList<PropertyInfo> properties))
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

                _writableColumnsProperties.Add(type, properties);   
            }

            return properties;
        }
    }
}
