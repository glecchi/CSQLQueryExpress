using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using SQLQueryBuilder;
using SQLQueryBuilder.Extensions;
using SQLQueryBuilder.Fragments;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    [SQLQueryCommand(2)]
    internal class OutputInsertNewShipperFromExistingWithSubQuery : SQLOutputQueryCommand<dbo.Shippers>
    {
        public OutputInsertNewShipperFromExistingWithSubQuery(ISQLQueryCommandFactory commandFactory)
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
                    c => c.Phone);

            var query = new SQLQuery()
                .From(subQuery)
                .Insert()
                .Output(s => s.All().Inserted());

            return query;
        }
    }
}
