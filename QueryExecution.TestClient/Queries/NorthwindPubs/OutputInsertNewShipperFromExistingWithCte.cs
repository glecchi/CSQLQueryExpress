using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using CSQLQueryExpress;
using CSQLQueryExpress.Extensions;
using CSQLQueryExpress.Fragments;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    [SQLQueryCommand(2)]
    internal class OutputInsertNewShipperFromExistingWithCte : SQLOutputQueryCommand<dbo.Shippers>
    {
        public OutputInsertNewShipperFromExistingWithCte(ISQLQueryCommandFactory commandFactory)
            : base(commandFactory)
        {
        }

        protected override SQLQueryOutput<dbo.Shippers> GetQueryOutput()
        {
            var subQuery = new SQLQuery()
                .From<dbo.Shippers>()
                .Where(c => c.CompanyName == "World Trust")
                .Select(
                    c => c.CompanyName,
                    c => c.Phone)
                .ToCteTable();

            var query = new SQLQuery()
                .From(subQuery)
                .Insert()
                .Output(s => s.All().Inserted());

            return query;
        }
    }
}
