using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using SQLQueryBuilder;
using SQLQueryBuilder.Fragments;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    [SQLQueryCommand(1)]
    internal class InsertNewShipperFromExistingWithSubQuery : SQLInsertQueryCommand<dbo.Shippers>
    {
        public InsertNewShipperFromExistingWithSubQuery(ISQLQueryCommandFactory commandFactory)
            : base(commandFactory)
        {
        }

        protected override SQLQueryInsert<dbo.Shippers> GetQueryInsert()
        {
            var cte = new SQLQuery()
                .From<dbo.Shippers>()
                .Where(c => c.CompanyName == "World Trust")
                .Select(
                    c => c.CompanyName,
                    c => c.Phone);

            var query = new SQLQuery()
                .From(cte)
                .Insert();

            return query;
        }
    }
}
