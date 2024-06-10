using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using SQLQueryBuilder;
using SQLQueryBuilder.Extensions;
using SQLQueryBuilder.Fragments;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    [SQLQueryCommand(5)]
    internal class SelectUnionProductsJoinCategoriesByCte : SQLSelectQueryCommand<dbo.Products>
    {
        public SelectUnionProductsJoinCategoriesByCte(ISQLQueryCommandFactory commandFactory) : base(commandFactory)
        {
        }

        protected override SQLQuerySelect<dbo.Products> GetQuerySelect()
        {
            var cte = new SQLQuery()
                .From<dbo.Categories>()
                .Where(c => c.CategoryID.In(new List<int> { 1, 2 }))
                .Select(c => c.All())
                .ToCteTable();

            var query1 = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => p.CategoryID == 1)
                .Select(p => p.All());

            var query2 = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => p.CategoryID == 2)
                .Select(p => p.All());

            var union = new SQLQuery()
                .Union(query1, query2);

            var select = new SQLQuery()
                .From(union)
                .Select(f => f.All());

            var query = new SQLQuery()
                .From(cte)
                .InnerJoin(select, (c, pr) => c.CategoryID == pr.CategoryID)
                .Select((p, pr) => pr.All());

            return query;
        }
    }
}
