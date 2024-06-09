using System;

namespace SQLQueryBuilder.Scaffolding
{
    public sealed class SQLDataModelCodeGeneratorError
    {
        internal SQLDataModelCodeGeneratorError(string entityName, Exception error)
        {
            EntityName = entityName;
            Error = error;
        }

        public string EntityName { get; }
        public Exception Error { get; }
    }

}
