using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using CSQLQueryExpress;
using CSQLQueryExpress.Fragments;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    [SQLQueryCommand(5)]
    internal class SelectProductsWhereMultiConditions : SQLSelectQueryCommand<dbo.Products>
    {
        public SelectProductsWhereMultiConditions(ISQLQueryCommandFactory commandFactory) : base(commandFactory)
        {
        }

        protected override SQLQuerySelect<dbo.Products> GetQuerySelect()
        {
            var query = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => p.CategoryID == 1 || p.CategoryID == 2)
                .Or(p => p.CategoryID == 3 || p.CategoryID == 4)
                .And(p => p.SupplierID == 1)
                .Select(p => p.All())
                .Top(10);

            return query;
        }
    }
}
