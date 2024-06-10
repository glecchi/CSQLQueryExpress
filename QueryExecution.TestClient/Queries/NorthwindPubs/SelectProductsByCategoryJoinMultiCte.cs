using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using SQLQueryBuilder;
using SQLQueryBuilder.Extensions;
using SQLQueryBuilder.Fragments;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    [SQLQueryCommand(5)]
    internal class SelectProductsByCategoryJoinMultiCte : SQLSelectQueryCommand<dbo.Products>
    {
        public SelectProductsByCategoryJoinMultiCte(ISQLQueryCommandFactory commandFactory) : base(commandFactory)
        {
        }

        protected override SQLQuerySelect<dbo.Products> GetQuerySelect()
        {
            var cte1 = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => p.CategoryID == 1)
                .Select(p => p.All())
                .ToCteTable();

            var cte2 = new SQLQuery()
                .From<dbo.Categories>()
                .Where(p => p.CategoryID == 2)
                .Select(p => p.All())
                .ToCteTable();

            var query = new SQLQuery()
                .From(cte1)
                .InnerJoin(cte2, (c1, c2) => c1.CategoryID == c2.CategoryID)
                .Select((c1, c2) => c1.All());

            return query;
        }
    }
}
