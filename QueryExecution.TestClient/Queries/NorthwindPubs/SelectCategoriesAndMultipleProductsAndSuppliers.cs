using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using CSQLQueryExpress;
using CSQLQueryExpress.Fragments;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    [SQLQueryCommand(8)]
	internal class SelectCategoriesAndMultipleProductsAndSuppliers : SQLMultipleResultSetsQueryCommand
	{
		public SelectCategoriesAndMultipleProductsAndSuppliers(ISQLQueryCommandFactory commandFactory) : base(commandFactory)
		{
		}

		protected override SQLQueryMultipleResultSets GetQueryMultipleResultSets()
		{
			var categories = new SQLQuery()
				.From<dbo.Categories>()
				.Select(c => c.CategoryID, c => c.CategoryName)
				.Top(10);

            var products1 = new SQLQuery()
                .From<dbo.Products>()
                .Select(p => p.ProductID, p => p.ProductName)
                .Top(10);

            var products2 = new SQLQuery()
                .From<dbo.Products>()
                .Select(p => p.ProductID, p => p.ProductName)
				.Top(10);

			var products3 = new SQLQuery()
				.From<dbo.Products>()
				.Select(p => p.ProductID, p => p.ProductName)
				.Top(10);

			var suppliers = new SQLQuery()
				.From<dbo.Suppliers>()
				.Select(p => p.All())
				.Top(10);

			var query = new SQLQuery()
				.MultipleResultSets(categories, products1, products2, products3, suppliers);

			return query;
		}
	}
}
