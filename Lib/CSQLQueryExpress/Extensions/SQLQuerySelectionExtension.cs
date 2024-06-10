namespace CSQLQueryExpress
{
    public static class SQLQuerySelectionExtension
    {
        public static T All<T>(this T obj) where T : class
        {
            return default;
        }
                
        public static T As<T>(this T obj, T alias)
        {
            return default;
        }

        public static T Inserted<T>(this T obj)
        {
            return default;
        }
        public static T Deleted<T>(this T obj)
        {
            return default;
        }
    }
}