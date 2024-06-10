using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using SQLQueryBuilder;
using SQLQueryBuilder.Extensions;
using SQLQueryBuilder.Fragments;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    [SQLQueryCommand(4)]
    internal class OutputDeleteNewShippersWithSubQuery : SQLOutputQueryCommand<dbo.Shippers>
    {
        public OutputDeleteNewShippersWithSubQuery(ISQLQueryCommandFactory commandFactory)
            : base(commandFactory)
        {
        }

        protected override SQLQueryOutput<dbo.Shippers> GetQueryOutput()
        {
            var cte = new SQLQuery()
                .From<dbo.Shippers>()
                .Where(s => s.CompanyName == "World Trust")
                .Select(s => s.All());

            var query = new SQLQuery()
                .From(cte)
                .Delete()
                .Top(1)
                .Output(s => s.All().Deleted());

            return query;
        }
    }
}
