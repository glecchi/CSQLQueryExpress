using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using CSQLQueryExpress;
using CSQLQueryExpress.Fragments;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    [SQLQueryCommand(3)]
    internal class DeleteNewShippersWithCte : SQLDeleteQueryCommand<dbo.Shippers>
    {
        public DeleteNewShippersWithCte(ISQLQueryCommandFactory commandFactory)
            : base(commandFactory)
        {
        }

        protected override SQLQueryDelete<dbo.Shippers> GetQueryDelete()
        {
            var cte = new SQLQuery()
                .From<dbo.Shippers>()
                .Where(s => s.CompanyName == "World Trust")
                .Select(s => s.All())
                .Top(1)
                .ToCteTable();

            var query = new SQLQuery()
                .From(cte)
                .Delete();

            return query;
        }
    }

}
