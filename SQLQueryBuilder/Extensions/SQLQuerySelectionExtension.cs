using System;
using System.Data.SqlTypes;

namespace SQLQueryBuilder.Extensions
{
    public static class SQLQuerySelectionExtension
    {
        public static T All<T>(this T obj) where T : class
        {
            return default;
        }

        public static T As<T>(this T? obj, T alias) where T : struct
        {
            return default;
        }

        public static T As<T>(this T obj, T? alias) where T : struct
        {
            return default;
        }

        public static T As<T>(this T obj, T alias) where T : struct
        {
            return default;
        }

        public static string As(this string obj, string alias)
        {
            return default;
        }
    }
}