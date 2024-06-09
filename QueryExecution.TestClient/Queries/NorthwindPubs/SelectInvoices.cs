using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using CSQLQueryExpress;
using CSQLQueryExpress.Fragments;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    [SQLQueryCommand(5)]
    internal class SelectInvoicesPagination : SQLSelectQueryCommand<dbo.View_Invoices>
    {
        public SelectInvoicesPagination(ISQLQueryCommandFactory commandFactory) : base(commandFactory)
        {
        }

        protected override SQLQuerySelect<dbo.View_Invoices> GetQuerySelect()
        {
            var query = new SQLQuery()
                .From<dbo.View_Invoices>()
                .Where(i => i.CustomerID == "ALFKI")
                .OrderBy(i => i.UnitPrice.Desc())
                .Select(s => s.All())
                .Top(10);

            return query;
        }
    }
}
