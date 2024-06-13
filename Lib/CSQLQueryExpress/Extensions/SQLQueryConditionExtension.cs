using CSQLQueryExpress.Fragments;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CSQLQueryExpress
{
    public static class SQLQueryConditionExtension
    {
        public static bool IsNull<T>(this T obj) where T : struct
        {
            return default;
        }

        public static bool IsNull<T>(this T? obj) where T : struct
        {
            return default;
        }

        public static bool IsNull(this string obj)
        {
            return default;
        }

        public static bool IsNotNull<T>(this T obj) where T : struct
        {
            return default;
        }

        public static bool IsNotNull<T>(this T? obj) where T : struct
        {
            return default;
        }

        public static bool IsNotNull(this string obj)
        {
            return default;
        }

        public static bool In<T>(this T obj, IList<T> collection) where T : struct
        {
            return default;
        }

        public static bool In<T>(this T? obj, IList<T?> collection) where T : struct
        {
            return default;
        }

        public static bool In(this string obj, IList<string> collection)
        {
            return default;
        }

        public static bool NotIn<T>(this T obj, IList<T> collection) where T : struct
        {
            return default;
        }

        public static bool NotIn<T>(this T? obj, IList<T?> collection) where T : struct
        {
            return default;
        }

        public static bool NotIn(this string obj, IList<string> collection)
        {
            return default;
        }

        public static ICase<T> Case<T>(this T obj)
        {
            return default;
        }

        public static T IsNull<T>(this T obj, T replacementValue) where T : struct
        {
            return default;
        }

        public static T? IsNull<T>(this T? obj, T? replacementValue) where T : struct
        {
            return default;
        }

        public static T IsNull<T>(this T? obj, T replacementValue) where T : struct
        {
            return default;
        }

        public static string IsNull(this string obj, string replacementValue)
        {
            return default;
        }

        public static bool Exists<T>(this T obj, SQLQuerySelect query)
        {
            return default;
        }

        public static bool NotExists<T>(this T obj, SQLQuerySelect query)
        {
            return default;
        }

        public static bool Between<T>(this T obj, T min, T max) where T : struct
        {
            return default;
        }

        public static bool Between<T>(this T? obj, T min, T max) where T : struct
        {
            return default;
        }

        public static bool NotBetween<T>(this T obj, T min, T max) where T : struct
        {
            return default;
        }

        public static bool NotBetween<T>(this T? obj, T min, T max) where T : struct
        {
            return default;
        }
    }
}
