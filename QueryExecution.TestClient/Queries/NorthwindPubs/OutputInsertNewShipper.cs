using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using CSQLQueryExpress;
using CSQLQueryExpress.Fragments;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    [SQLQueryCommand(2)]
    internal class OutputInsertNewShipper : SQLOutputQueryCommand<dbo.Shippers>
    {
        public OutputInsertNewShipper(ISQLQueryCommandFactory commandFactory) 
            : base(commandFactory)
        {
        }

        protected override SQLQueryOutput<dbo.Shippers> GetQueryOutput()
        {
            var newShipper = new dbo.Shippers();
            newShipper.CompanyName = "World Trust";
            newShipper.Phone = "999-999-000";

            var query = new SQLQuery()
                .Insert(newShipper)
                .Output(s => s.All().Inserted());

            return query;
        }
    }
}
