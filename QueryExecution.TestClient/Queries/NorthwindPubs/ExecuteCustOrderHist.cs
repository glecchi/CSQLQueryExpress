using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using CSQLQueryExpress;
using CSQLQueryExpress.Fragments;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    internal class ExecuteCustOrderHist : SQLStoredProcedureCommand<Proc_CustOrderHist_Result>
    {
        public ExecuteCustOrderHist(ISQLQueryCommandFactory commandFactory) : base(commandFactory)
        {
        }

        protected override SQLQueryStoredProcedure<Proc_CustOrderHist_Result> GetExecuteProcedure()
        {
            var executeProcedure = new SQLQuery()
                .StoredProcedure(new dbo.Proc_CustOrderHist { CustomerID = "ALFKI" });

            return executeProcedure;
        }
    }
}
