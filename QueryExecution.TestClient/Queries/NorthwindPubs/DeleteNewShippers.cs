using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using SQLQueryBuilder;
using SQLQueryBuilder.Fragments;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    [SQLQueryCommand(3)]
    internal class DeleteNewShippers : SQLDeleteQueryCommand<dbo.Shippers>
    {
        public DeleteNewShippers(ISQLQueryCommandFactory commandFactory)
            : base(commandFactory)
        {
        }

        protected override SQLQueryDelete<dbo.Shippers> GetQueryDelete()
        {
            var query = new SQLQuery()
                .From<dbo.Shippers>()
                .Where(s => s.CompanyName == "World Trust")
                .Delete()
                .Top(1);

            return query;
        }
    }
}
