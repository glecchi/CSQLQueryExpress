using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSQLQueryExpress;
using CSQLQueryExpress.Fragments;

namespace QueryExecution.Dapper.CommandFactory.Commands
{

    public abstract class SQLDropQueryCommand<T> : SQLQueryExecutionCommand where T : ISQLQueryEntity
    {
        protected SQLDropQueryCommand(ISQLQueryCommandFactory commandFactory) 
            : base(commandFactory)
        {
        }

        protected abstract SQLQueryDrop<T> GetQueryDrop();

        protected sealed override int GetResult(ISQLQueryCommandFactory commandFactory, out SQLQueryCompiled queryCompiled)
        {
            return commandFactory.GetResult(GetQueryDrop(), out queryCompiled);
        }
    }
}
