using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using CSQLQueryExpress;
using CSQLQueryExpress.Fragments;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    [SQLQueryCommand(7)]
    internal class BatchInsertNewShipper : SQLBatchQueryCommand<dbo.Shippers>
    {
        public BatchInsertNewShipper(ISQLQueryCommandFactory commandFactory)
            : base(commandFactory)
        {
        }

        protected override SQLQueryBatch<dbo.Shippers> GetQueryBatch()
        {
            var newShipper = new dbo.Shippers();
            newShipper.CompanyName = "World Trust";
            newShipper.Phone = "999-999-000";

            var query1 = new SQLQuery()
                .Insert(newShipper);

            var query2 = new SQLQuery()
                .Insert(newShipper);

            var query3 = new SQLQuery()
                .Insert(newShipper);

            var query = new SQLQuery()
                .Batch(query1, query2, query3);

            return query;
        }
    }
}
