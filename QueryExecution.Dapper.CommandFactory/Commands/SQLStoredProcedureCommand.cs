using SQLQueryBuilder.Fragments;
using SQLQueryBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryExecution.Dapper.CommandFactory.Commands
{
    public abstract class SQLStoredProcedureCommand : SQLQueryExecutionCommand
    {
        protected SQLStoredProcedureCommand(ISQLQueryCommandFactory commandFactory)
            : base(commandFactory)
        {
        }

        protected abstract SQLQueryStoredProcedure GetExecuteProcedure();

        protected sealed override int GetResult(ISQLQueryCommandFactory commandFactory, out SQLQueryCompiled queryCompiled)
        {
            return commandFactory.GetResult(GetExecuteProcedure(), out queryCompiled);
        }
    }

    public abstract class SQLStoredProcedureCommand<T> : SQLQueryReaderCommand
    {
        protected SQLStoredProcedureCommand(ISQLQueryCommandFactory commandFactory)
            : base(commandFactory)
        {
        }

        protected abstract SQLQueryStoredProcedure<T> GetExecuteProcedure();

        protected sealed override IEnumerable<T> GetReader(ISQLQueryCommandFactory commandFactory, out SQLQueryCompiled queryCompiled)
        {
            return commandFactory.GetReader(GetExecuteProcedure(), out queryCompiled);
        }
    }
}
