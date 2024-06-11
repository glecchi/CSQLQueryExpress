using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using CSQLQueryExpress;
using CSQLQueryExpress.Fragments;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    [SQLQueryCommand(7)]
    internal class BatchDeleteNewShippers : SQLBatchQueryCommand<dbo.Shippers>
    {
        public BatchDeleteNewShippers(ISQLQueryCommandFactory commandFactory)
            : base(commandFactory)
        {
        }

        protected override SQLQueryBatch<dbo.Shippers> GetQueryBatch()
        {
            var query1 = new SQLQuery()
                .From<dbo.Shippers>()
                .Where(s => s.CompanyName == "World Trust")
                .Delete()
                .Top(1);

            var query2 = new SQLQuery()
                .From<dbo.Shippers>()
                .Where(s => s.CompanyName == "World Trust")
                .Delete()
                .Top(1);

            var query3 = new SQLQuery()
                .From<dbo.Shippers>()
                .Where(s => s.CompanyName == "World Trust")
                .Delete()
                .Top(1);

            var query = new SQLQuery()
                .Batch(query1, query2, query3);

            return query;
        }
    }
}
