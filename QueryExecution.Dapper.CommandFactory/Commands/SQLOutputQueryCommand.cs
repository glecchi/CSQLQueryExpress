using System.Collections;
using CSQLQueryExpress;
using CSQLQueryExpress.Fragments;

namespace QueryExecution.Dapper.CommandFactory.Commands
{
    public abstract class SQLOutputQueryCommand<T> : SQLQueryReaderCommand
    {
        protected SQLOutputQueryCommand(ISQLQueryCommandFactory commandFactory) 
            : base(commandFactory)
        {
        }

        protected abstract SQLQueryOutput<T> GetQueryOutput();

        protected sealed override IEnumerable<T> GetReader(ISQLQueryCommandFactory commandFactory, out SQLQueryCompiled queryCompiled)
        {
            return commandFactory.GetReader(GetQueryOutput(), out queryCompiled);
        }
    }
}
