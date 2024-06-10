using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using SQLQueryBuilder;
using SQLQueryBuilder.Fragments;
using SQLQueryBuilder.Extensions;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    [SQLQueryCommand(5)]
    internal class SelectProductsPagination : SQLSelectQueryCommand<dbo.Products>
    {
        public SelectProductsPagination(ISQLQueryCommandFactory commandFactory) : base(commandFactory)
        {
        }

        protected override SQLQuerySelect<dbo.Products> GetQuerySelect()
        {
            var cte = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => p.CategoryID == 1)
                .Select(p => p.All())
                .ToCteTable();

            var query = new SQLQuery()
                .From(cte)
                .OrderBy(c => c.ProductName)
                .Page(p => p.Offset(5).Fetch(5))
                .Select(s => s.All());

            return query;
        }
    }
}
