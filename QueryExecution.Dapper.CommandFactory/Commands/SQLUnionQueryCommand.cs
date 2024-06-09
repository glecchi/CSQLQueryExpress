using System.Collections;
using SQLQueryBuilder;
using SQLQueryBuilder.Fragments;

namespace QueryExecution.Dapper.CommandFactory.Commands
{
    public abstract class SQLUnionQueryCommand<T> : SQLQueryReaderCommand
    {
        protected SQLUnionQueryCommand(ISQLQueryCommandFactory commandFactory) 
            : base(commandFactory)
        {
        }

        protected abstract SQLQueryUnion<T> GetQueryUnion();

        protected sealed override IEnumerable<T> GetReader(ISQLQueryCommandFactory commandFactory, out SQLQueryCompiled queryCompiled)
        {
            return commandFactory.GetReader(GetQueryUnion(), out queryCompiled);
        }
    }
}
