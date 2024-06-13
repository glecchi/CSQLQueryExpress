using CSQLQueryExpress.Fragments;

namespace CSQLQueryExpress
{
    public static class SQLQueryOperationExtensions
    {
        public static T Sum<T>(this T obj) where T : struct
        {
            return default;
        }

        public static T Sum<T>(this T? obj) where T : struct
        {
            return default;
        }

        public static T Sign<T>(this T obj) where T : struct
        {
            return default;
        }

        public static T Sign<T>(this T? obj) where T : struct
        {
            return default;
        }

        public static int Count<T>(this T obj) where T : struct
        {
            return default;
        }

        public static int Count<T>(this T? obj) where T : struct
        {
            return default;
        }

        public static int Count(this string obj)
        {
            return default;
        }

        public static int CountDistinct<T>(this T obj) where T : struct
        {
            return default;
        }

        public static int CountDistinct<T>(this T? obj) where T : struct
        {
            return default;
        }

        public static int CountDistinct(this string obj)
        {
            return default;
        }

        public static T Max<T>(this T obj) where T : struct
        {
            return default;
        }

        public static T Max<T>(this T? obj) where T : struct
        {
            return default;
        }

        public static T Min<T>(this T obj) where T : struct
        {
            return default;
        }

        public static T Min<T>(this T? obj) where T : struct
        {
            return default;
        }

        public static string Left(this string obj, int count)
        {
            return default;
        }

        public static string Right(this string obj, int count)
        {
            return default;
        }

        public static int Len(this string obj)
        {
            return default;
        }

        public static T Avg<T>(this T obj) where T : struct
        {
            return default;
        }

        public static T Avg<T>(this T? obj) where T : struct
        {
            return default;
        }

        public static T AvgDistinct<T>(this T obj) where T : struct
        {
            return default;
        }

        public static T AvgDistinct<T>(this T? obj) where T : struct
        {
            return default;
        }

        public static ISQLQueryForXml Path(this ISQLQueryForXml obj)
        {
            return default;
        }

        public static ISQLQueryForXml Path(this ISQLQueryForXml obj, string path)
        {
            return default;
        }

        public static ISQLQueryForXml Root(this ISQLQueryForXml obj, string root)
        {
            return default;
        }
    }
}
