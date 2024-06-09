using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using CSQLQueryExpress;
using CSQLQueryExpress.Extensions;
using CSQLQueryExpress.Fragments;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    [SQLQueryCommand(6)]
    internal class UpdateNewShipperWithSubQuery : SQLUpdateQueryCommand<dbo.Shippers>
    {
        public UpdateNewShipperWithSubQuery(ISQLQueryCommandFactory commandFactory)
            : base(commandFactory)
        {
        }

        protected override SQLQueryUpdate<dbo.Shippers> GetQueryUpdate()
        {
            var newShipper = new dbo.Shippers();
            newShipper.CompanyName = "World Trust";
            newShipper.Phone = "999-999-000";

            var cte = new SQLQuery()
                .From<dbo.Shippers>()
                .Where(s => s.CompanyName == "World Trust")
                .Select(s => s.All())
                .Top(1);

            var query = new SQLQuery()
                .From(cte)
                .Update(s => s.Phone.Set(newShipper.Phone));

            return query;
        }
    }
}
