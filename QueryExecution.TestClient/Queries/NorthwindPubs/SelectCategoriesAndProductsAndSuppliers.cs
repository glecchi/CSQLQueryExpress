﻿using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using CSQLQueryExpress;
using CSQLQueryExpress.Fragments;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    [SQLQueryCommand(8)]
    internal class SelectCategoriesAndProductsAndSuppliers : SQLMultipleResultSetsQueryCommand
	{
		public SelectCategoriesAndProductsAndSuppliers(ISQLQueryCommandFactory commandFactory) : base(commandFactory)
		{
		}

		protected override SQLQueryMultipleResultSets GetQueryMultipleResultSets()
		{
			var categories = new SQLQuery()
				.From<dbo.Categories>()
				.Select(c => c.CategoryID, c => c.CategoryName)
				.Top(10);

			var products = new SQLQuery()
				.From<dbo.Products>()
				.Select(p => p.ProductID, p => p.ProductName)
				.Top(10);

			var suppliers = new SQLQuery()
				.From<dbo.Suppliers>()
				.Select(p => p.All())
				.Top(10);

			var query = new SQLQuery()
				.MultipleResultSets(categories, products, suppliers);

			return query;
		}
	}
}
