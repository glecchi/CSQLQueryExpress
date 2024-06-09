using System.Collections;
using CSQLQueryExpress;
using CSQLQueryExpress.Fragments;

namespace QueryExecution.Dapper.CommandFactory.Commands
{
    public abstract class SQLSelectQueryCommand<T> : SQLQueryReaderCommand
    {
        protected SQLSelectQueryCommand(ISQLQueryCommandFactory commandFactory) 
            : base(commandFactory)
        {
        }

        protected abstract SQLQuerySelect<T> GetQuerySelect();

        protected sealed override IEnumerable<T> GetReader(ISQLQueryCommandFactory commandFactory, out SQLQueryCompiled queryCompiled)
        {
            return commandFactory.GetReader(GetQuerySelect(), out queryCompiled);
        }
    }
}
