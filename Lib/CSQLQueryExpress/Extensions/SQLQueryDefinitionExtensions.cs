using System.Data;

namespace CSQLQueryExpress
{
    public static class SQLQueryDefinitionExtensions
    {
        public static SqlDbType Size(this SqlDbType type, int size)
        {
            return default;
        }

        public static SqlDbType Max(this SqlDbType type)
        {
            return default;
        }

        public static T Asc<T>(this T obj) where T : struct
        {
            return default;
        }

        public static T? Asc<T>(this T? obj) where T : struct
        {
            return default;
        }

        public static string Asc(this string obj)
        {
            return default;
        }

        public static T Desc<T>(this T obj) where T : struct
        {
            return default;
        }

        public static T? Desc<T>(this T? obj) where T : struct
        {
            return default;
        }

        public static string Desc(this string obj)
        {
            return default;
        }
    }
}
