using CSQLQueryExpress;
using CSQLQueryExpress.Fragments;

namespace QueryExecution.Dapper.CommandFactory.Commands
{

    public abstract class SQLSelectIntoQueryCommand<T> : SQLQueryExecutionCommand where T : ISQLQueryEntity
    {
        protected SQLSelectIntoQueryCommand(ISQLQueryCommandFactory commandFactory)
            : base(commandFactory)
        {

        }
                
        protected abstract SQLQueryInto<T> GetQuerySelectInto();

        protected override int GetResult(ISQLQueryCommandFactory commandFactory, out SQLQueryCompiled queryCompiled)
        {
            return commandFactory.GetResult(GetQuerySelectInto(), out queryCompiled);
        }
    }
}
