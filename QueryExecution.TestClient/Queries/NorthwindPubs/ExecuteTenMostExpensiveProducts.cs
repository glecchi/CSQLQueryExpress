using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using SQLQueryBuilder;
using SQLQueryBuilder.Fragments;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
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
