using System;

namespace CSQLQueryExpress.Extensions
{
    public static class SQLQueryAssignmentExtensions
    {
        public static void Set<T>(this T obj, T value) where T : struct
        {

        }

        public static void Set<T>(this T? obj, T? value) where T : struct
        {

        }

        public static void Set(this string obj, string value)
        {

        }
    }
}