using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using CSQLQueryExpress;
using CSQLQueryExpress.Fragments;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    [SQLQueryCommand(6)]
    internal class OutputUpdateNewShipperWithCte : SQLOutputQueryCommand<dbo.Shippers>
    {
        public OutputUpdateNewShipperWithCte(ISQLQueryCommandFactory commandFactory)
            : base(commandFactory)
        {
        }

        protected override SQLQueryOutput<dbo.Shippers> GetQueryOutput()
        {
            var newShipper = new dbo.Shippers();
            newShipper.CompanyName = "World Trust";
            newShipper.Phone = "999-999-000";

            var cte = new SQLQuery()
                .From<dbo.Shippers>()
                .Where(s => s.CompanyName == "World Trust")
                .Select(s => s.All())
                .Top(1)
                .ToCteTable();

            var query = new SQLQuery()
                .From(cte)
                .Update(new Dictionary<string, object> { { nameof(dbo.Shippers.Phone), newShipper.Phone } })
                .Output(s => s.All().Inserted());

            return query;
        }
    }
}
