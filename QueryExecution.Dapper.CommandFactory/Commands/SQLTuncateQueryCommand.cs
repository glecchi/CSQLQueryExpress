using CSQLQueryExpress;
using CSQLQueryExpress.Fragments;

namespace QueryExecution.Dapper.CommandFactory.Commands
{
    public abstract class SQLTuncateQueryCommand<T> : SQLQueryExecutionCommand where T : ISQLQueryEntity
    {
        protected SQLTuncateQueryCommand(ISQLQueryCommandFactory commandFactory) 
            : base(commandFactory)
        {
        }

        protected abstract SQLQueryTruncate<T> GetQueryTuncate();

        protected sealed override int GetResult(ISQLQueryCommandFactory commandFactory, out SQLQueryCompiled queryCompiled)
        {
            return commandFactory.GetResult(GetQueryTuncate(), out queryCompiled);
        }
    }
}
