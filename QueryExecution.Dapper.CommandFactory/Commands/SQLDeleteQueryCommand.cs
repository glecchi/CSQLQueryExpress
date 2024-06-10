using SQLQueryBuilder;
using SQLQueryBuilder.Fragments;

namespace QueryExecution.Dapper.CommandFactory.Commands
{
    public abstract class SQLDeleteQueryCommand<T> : SQLQueryExecutionCommand where T : ISQLQueryEntity
    {
        protected SQLDeleteQueryCommand(ISQLQueryCommandFactory commandFactory) 
            : base(commandFactory)
        {
        }

        protected abstract SQLQueryDelete<T> GetQueryDelete();

        protected sealed override int GetResult(ISQLQueryCommandFactory commandFactory, out SQLQueryCompiled queryCompiled)
        {
            return commandFactory.GetResult(GetQueryDelete(), out queryCompiled);
        }
    }
}
