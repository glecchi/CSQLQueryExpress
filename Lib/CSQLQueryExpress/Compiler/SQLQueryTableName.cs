namespace CSQLQueryExpress
{
    public sealed class SQLQueryTableName
    {
        public SQLQueryTableName(string tableName, string tableAlias)
        {
            TableName = tableName;
            TableAlias = tableAlias;
        }

        public string TableName { get; }

        public string TableAlias { get; }
    }
}
