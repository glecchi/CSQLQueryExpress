using System;
using System.Linq.Expressions;

namespace CSQLQueryExpress
{
    public static class SQLQueryAssignmentExtensions
    {
        public static void Set<T>(this T obj, T value) where T : struct
        {

        }

        public static void Set(this byte[] obj, byte[] value)
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