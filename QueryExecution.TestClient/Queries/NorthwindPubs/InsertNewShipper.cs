using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using SQLQueryBuilder;
using SQLQueryBuilder.Fragments;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    [SQLQueryCommand(1)]
    internal class InsertNewShipper : SQLInsertQueryCommand<dbo.Shippers>
    {
        public InsertNewShipper(ISQLQueryCommandFactory commandFactory)
            : base(commandFactory)
        {
        }

        protected override SQLQueryInsert<dbo.Shippers> GetQueryInsert()
        {
            var newShipper = new dbo.Shippers();
            newShipper.CompanyName = "World Trust";
            newShipper.Phone = "999-999-000";

            var query = new SQLQuery()
                .Insert(newShipper);

            return query;
        }
    }
}
