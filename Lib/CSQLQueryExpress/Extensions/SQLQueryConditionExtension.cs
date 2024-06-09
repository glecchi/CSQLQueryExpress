using System.Collections.Generic;

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
    }
}
