using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using SQLQueryBuilder;
using SQLQueryBuilder.Extensions;
using SQLQueryBuilder.Fragments;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    [SQLQueryCommand(3)]
    internal class DeleteNewShippersWithSubQuery : SQLDeleteQueryCommand<dbo.Shippers>
    {
        public DeleteNewShippersWithSubQuery(ISQLQueryCommandFactory commandFactory)
            : base(commandFactory)
        {
        }

        protected override SQLQueryDelete<dbo.Shippers> GetQueryDelete()
        {
            var cte = new SQLQuery()
                .From<dbo.Shippers>()
                .Where(s => s.CompanyName == "World Trust")
                .Select(s => s.All())
                .Top(1);

            var query = new SQLQuery()
                .From(cte)
                .Delete();

            return query;
        }
    }

}
