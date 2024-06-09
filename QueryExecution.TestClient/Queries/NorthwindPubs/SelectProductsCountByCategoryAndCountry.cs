using SQLQueryBuilder.Fragments;
using SQLQueryBuilder;
using SQLQueryBuilder.Extensions;
using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory.Commands;
using QueryExecution.Dapper.CommandFactory;
using SQLQueryBuilder.Statements;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    [SQLQueryCommand(5)]
    internal class SelectProductsCountByCategoryAndCountry : SQLSelectQueryCommand<ProductsCountByCategory>
    {
        public SelectProductsCountByCategoryAndCountry(ISQLQueryCommandFactory commandFactory) 
            : base(commandFactory)
        {
        }

        protected override SQLQuerySelect<ProductsCountByCategory> GetQuerySelect()
        {
            var query = new SQLQuery()
                .From<dbo.Categories>()
                .InnerJoin<dbo.Products>((c, p) => c.CategoryID == p.CategoryID)
                .InnerJoin<dbo.Suppliers>((c, p, s) => p.SupplierID == s.SupplierID)
                .GroupBy(
                    (c, p, s) => c.CategoryName,
                    (c, p, s) => s.Country)
                .Select<ProductsCountByCategory>(
                    (c, p, s, res) => c.CategoryName,
                    (c, p, s, res) => s.Country,
                    (c, p, s, res) => Count.All().As(res.ProductsCount));

            return query;
        }
    }

    class ProductsCountByCategory
    {
        public string CategoryName { get; set; }

        public string Country { get; set; }

        public int ProductsCount { get; set; }
    }
}
