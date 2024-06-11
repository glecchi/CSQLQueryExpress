using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using CSQLQueryExpress;
using CSQLQueryExpress.Fragments;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    [SQLQueryCommand(8)]
    internal class ExecuteTenMostExpensiveProducts : SQLStoredProcedureCommand<Proc_Ten_Most_Expensive_Products_Result>
    {
        public ExecuteTenMostExpensiveProducts(ISQLQueryCommandFactory commandFactory) : base(commandFactory)
        {
        }

        protected override SQLQueryStoredProcedure<Proc_Ten_Most_Expensive_Products_Result> GetExecuteProcedure()
        {
            var executeProcedure = new SQLQuery()
                .StoredProcedure(new dbo.Proc_Ten_Most_Expensive_Products());

            return executeProcedure;
        }
    }
}
