using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using CSQLQueryExpress;
using CSQLQueryExpress.Fragments;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    [SQLQueryCommand(6)]
    internal class OuputUpdateNewShipper : SQLOutputQueryCommand<dbo.Shippers>
    {
        public OuputUpdateNewShipper(ISQLQueryCommandFactory commandFactory) 
            : base(commandFactory)
        {
        }

        protected override SQLQueryOutput<dbo.Shippers> GetQueryOutput()
        {
            var newShipper = new dbo.Shippers();
            newShipper.CompanyName = "World Trust";
            newShipper.Phone = "999-999-000";

            var query = new SQLQuery()
                .From<dbo.Shippers>()
                .Where(s => s.CompanyName == "World Trust")
                .Update(s => s.Phone.Set(newShipper.Phone))
                .Top(1)
                .Output(s => s.All().Deleted());

            return query;
        }
    }
}
