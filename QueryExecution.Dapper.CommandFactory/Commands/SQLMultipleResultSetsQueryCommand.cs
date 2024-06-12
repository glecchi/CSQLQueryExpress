using CSQLQueryExpress;
using CSQLQueryExpress.Fragments;
using System.Collections;

namespace QueryExecution.Dapper.CommandFactory.Commands
{
    public abstract class SQLMultipleResultSetsQueryCommand : SQLQueryReaderCommand
    {
        protected SQLMultipleResultSetsQueryCommand(ISQLQueryCommandFactory commandFactory)
            : base(commandFactory)
        {
        }

        protected abstract SQLQueryMultipleResultSets GetQueryMultipleResultSets();

        protected sealed override IEnumerable GetReader(ISQLQueryCommandFactory commandFactory, out SQLQueryCompiled queryCompiled)
        {
            return commandFactory.GetReader(GetQueryMultipleResultSets(), out queryCompiled);
        }
    }
}
