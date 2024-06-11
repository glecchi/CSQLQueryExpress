using CSQLQueryExpress;
using CSQLQueryExpress.Fragments;

namespace QueryExecution.Dapper.CommandFactory.Commands
{
    public abstract class SQLBatchQueryCommand<T> : SQLQueryExecutionCommand where T : ISQLQueryEntity
    {
        protected SQLBatchQueryCommand(ISQLQueryCommandFactory commandFactory) 
            : base(commandFactory)
        {
        }

        protected abstract SQLQueryBatch<T> GetQueryBatch();

        protected sealed override int GetResult(ISQLQueryCommandFactory commandFactory, out SQLQueryCompiled queryCompiled)
        {
            return commandFactory.GetResult(GetQueryBatch(), out queryCompiled);
        }
    }
}
