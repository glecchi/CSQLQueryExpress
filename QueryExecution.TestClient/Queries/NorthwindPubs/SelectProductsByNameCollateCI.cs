using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using SQLQueryBuilder;
using SQLQueryBuilder.Fragments;
using SQLQueryBuilder.Extensions;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    [SQLQueryCommand(5)]
    internal class SelectProductsByNameCollateCI : SQLSelectQueryCommand<dbo.Products>
    {
        public SelectProductsByNameCollateCI(ISQLQueryCommandFactory commandFactory) : base(commandFactory)
        {
        }

        protected override SQLQuerySelect<dbo.Products> GetQuerySelect()
        {
            var query = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => p.ProductName.Collate("Latin1_General_CS_AS").StartsWith("Genen"))
                .Select(s => s.All())
                .Top(10);

            return query;
        }
    }
}
